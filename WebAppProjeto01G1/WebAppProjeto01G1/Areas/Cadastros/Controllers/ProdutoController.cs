﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppProjeto01G1.Models;
using System.Data.Entity;
using System.Net;
using Modelo.Cadastros;
using Modelo.Tabelas;
using Serviço.Cadastros;
using Serviço.Tabelas;
using System.IO;

namespace WebAppProjeto01G1.Areas.Cadastros.Controllers
{
    public class ProdutoController : Controller
    {
        //private EFContext context = new EFContext();
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        public ActionResult Index()
        {
            //var produtos = context.Produtos.Include(c => c.Categoria).Include(f => f.Fabricante).OrderBy(n => n.Nome);
            //return View(produtos);
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // GET: ProdutosController/Create
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
        }



        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto, HttpPostedFileBase logotipo = null, string chkRemoverImagem = null)
        {
            return GravarProduto(produto, logotipo, chkRemoverImagem);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);
                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = produtoServico.ObterProdutoPorId((long)id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(),
                "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(),
                "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(),
                "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(),
                "FabricanteId", "Nome", produto.FabricanteId);
            }
        }


        private ActionResult GravarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                PopularViewBag(produto);
                return View(produto);
            }
            catch
            {

                return View(produto);
            }
        }

        private byte[] SetLogotipo(HttpPostedFileBase logotipo)
        {
            var bytesLogotipo = new byte[logotipo.ContentLength];
            logotipo.InputStream.Read(bytesLogotipo, 0, logotipo.ContentLength);
            return bytesLogotipo;
        }

        private ActionResult GravarProduto(Produto produto, HttpPostedFileBase logotipo, string chkRemoverImagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (chkRemoverImagem != null)
                    {
                        produto.Logotipo = null;
                    }
                    if (logotipo != null)
                    {
                        produto.LogotipoMimeType = logotipo.ContentType;
                        produto.Logotipo = SetLogotipo(logotipo);
                        produto.NomeArquivo = logotipo.FileName;
                        produto.TamanhoArquivo = logotipo.ContentLength;

                        string strFileName = Server.MapPath("~/App_Data/") + Path.GetFileName(logotipo.FileName);

                        logotipo.SaveAs(strFileName);
                    }
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                PopularViewBag(produto);
                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);
                return View(produto);
            }
        }

        public FileContentResult GetLogotipo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);
            if (produto != null)
            {
                return File(produto.Logotipo, produto.LogotipoMimeType);
            }
            return null;
        }
        public FileContentResult GetLogotipo2(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);
            if (produto != null)
            {
                if (produto.NomeArquivo != null)
                {
                    var bytesLogotipo = new byte[produto.TamanhoArquivo];
                    FileStream fileStream = new FileStream(Server.MapPath("~/App_Data/" + produto.NomeArquivo), FileMode.Open, FileAccess.Read);
                    fileStream.Read(bytesLogotipo, 0, (int)produto.TamanhoArquivo);
                    return File(bytesLogotipo, produto.LogotipoMimeType);
                }
            }
            return null;
        }


        public ActionResult DownloadArquivo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);
            FileStream fileStream = new FileStream(Server.MapPath(
                "~/App_Data/" + produto.NomeArquivo), FileMode.Create,
                FileAccess.Write);
            fileStream.Write(produto.Logotipo, 0,
                Convert.ToInt32(produto.TamanhoArquivo));
            fileStream.Close();
            return File(fileStream.Name, produto.LogotipoMimeType, produto.NomeArquivo);
        }
        public ActionResult DownloadArquivo2(long id)
        {

            Produto produto = produtoServico.ObterProdutoPorId(id);
            FileStream fileStream = new FileStream(Server.MapPath("~/App_Data/" +
            produto.NomeArquivo), FileMode.Open, FileAccess.Read);
            return File(fileStream.Name, produto.LogotipoMimeType, produto.NomeArquivo);

        }
    }
}
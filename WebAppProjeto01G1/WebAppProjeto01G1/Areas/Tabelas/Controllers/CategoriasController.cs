﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppProjeto01G1.Models;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Data.Entity;
using Modelo.Cadastros;
using Modelo.Tabelas;
using Serviço.Tabelas;

namespace WebAppProjeto01G1.Areas.Tabelas.Controllers
{
    public class CategoriasController : Controller
    {
        //public EFContext context = new EFContext();
        //public ActionResult HtpNotFoundResult { get; private set; }

        private CategoriaServico categoriaServico = new CategoriaServico();

        private ActionResult ObterVisaoCategoriaId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Categoria fabricante = categoriaServico.ObterCategoriaPorId((long)id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        private ActionResult GravarCategoria(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoriaServico.GravarCategoria(categoria);
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
            catch
            {
                return View(categoria);
            }
        }



        // GET: Categorias
        public ActionResult Index()
        {
            return View(categoriaServico.ObterCategoriasClassificadasPorNome());
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // VOLTAR A PARTIR DAQUI AJKDSAD ASKABLFSBKLAFSAFSAFDSAFSAFSADVHFSADFJH

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            return GravarCategoria(categoria);
        }

        // GET: Edit
        public ActionResult Edit(long? id)
        {


            return ObterVisaoCategoriaId(id);
        }


        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            return GravarCategoria(categoria);
        }


        // GET: Details
        public ActionResult Details(long? id)
        {
            return ObterVisaoCategoriaId(id);
        }

        // GET: Delete
        public ActionResult Delete(long? id)
        {
            ////return View(categorias.Where(m => m.CategoriaId == id).First());

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Categoria categoria = context.Categorias.Find(id);
            //if (categoria == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(categoria);

            return ObterVisaoCategoriaId(id);
        }


        // POST: Categorias/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            ////categorias.Remove(
            ////categorias.Where(c => c.CategoriaId == categoria.CategoriaId).First());

            //Categoria categoria = context.Categorias.Find(id);
            //context.Categorias.Remove(categoria);
            //context.SaveChanges();
            //TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removida";

            //return RedirectToAction("Index");

            try
            {
                Categoria categoria = categoriaServico.EliminarCategoriaPorId(id);
                TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removida";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
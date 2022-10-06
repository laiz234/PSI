using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppProjeto01G1.Models;

namespace Modelo.Tabelas


{
    public class CategoriasController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(context.Categorias.OrderBy(c => c.Nome));
        }

        // GET: Categorias
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categorias)
        {
            context.Categorias.Add(categorias);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categorias = context.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categorias)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categorias).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorias);
        }
        [HttpGet]
        public ActionResult Details(long id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Where(p => p.CategoriaId == id).
                Include("Produtos.Fabricante").First();
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categorias = context.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
        }

        // POST: Categoria/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categorias = context.Categorias.Find(id);
            context.Categorias.Remove(categorias);
            context.SaveChanges();
            TempData["Message"] = "Categoria " +
              categorias.Nome.ToUpper() + " foi removido";
            return RedirectToAction("Index");
        }


    }
}
using System;
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

namespace WebAppProjeto01G1.Controllers
{
    public class CategoriasController : Controller
    {
        private EFContext context = new EFContext();
        /*private static IList<Categoria> categorias = new List<Categoria>()
        {
            new Categoria() { CategoriaId = 1, Nome = "Notebooks"},
            new Categoria() { CategoriaId = 2, Nome = "Monitores"},
            new Categoria() { CategoriaId = 3, Nome = "Impressoras"},
            new Categoria() { CategoriaId = 4, Nome = "Mouses"},
            new Categoria() { CategoriaId = 5, Nome = "Desktops"}
        };*/

        // GET: Categorias
        public ActionResult Index()
        {
            //return View(categorias);
            return View(context.Categorias.OrderBy(c => c.Nome));
        }

        // GET: Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            //categorias.Add(categoria);
            //categoria.CategoriaId = categorias.Select(m => m.CategoriaId).Max() + 1;
            context.Categorias.Add(categoria);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Categorias/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                //return RedirectToAction("PaginaErro");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Find(id);
            //Categoria categoria = categorias.Where(m => m.CategoriaId == id).First();
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);


            //return View(categorias.Where(m => m.CategoriaId == id).First());
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Fabricantes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Where(f => f.CategoriaId == id).
            Include("Fabricantes.Categoria").First();
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //GET: Categorias/Delete/5
        [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);


            //return View(categorias.Where(m => m.CategoriaId == id).First());
        }

        //POST: Categorias/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categoria = context.Categorias.Find(id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();
            TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removida";
            return RedirectToAction("Index");


            //categorias.Remove(
            //categorias.Where(c => c.CategoriaId == categoria.CategoriaId).First());
            //return RedirectToAction("Index");
        }



    }
}
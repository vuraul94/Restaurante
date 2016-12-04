using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestauranteApplication.Models;
using System.Dynamic;

namespace RestauranteApplication.Controllers
{
    public class recetasController : Controller
    {
        private RestauranteEntities db = new RestauranteEntities();
        private List<productos> ingredientes = new List<productos>();
        // GET: recetas
        public ActionResult Index()
        {
            List<RecetaMulti> listaRecetas = new List<RecetaMulti>();
            foreach (var receta in db.recetas.ToList())
            {
                RecetaMulti elementoReceta = new RecetaMulti();

                IQueryable<productos> products = db.productos.Where(productos => productos.id_producto == receta.fk_producto_padre);

                elementoReceta.recetas = receta;
                int contador = 1;
                foreach (var prod in products)
                {
                    if (contador == 1)
                    {
                        elementoReceta.productoFK = prod;
                    }
                    contador++;
                }
                var idreceta = receta.id_receta;
                // Ingredientes
                IQueryable<productos_rel_recetas> relProdRec = db.productos_rel_recetas.Where(productos_rel_recetas => productos_rel_recetas.fk_receta == receta.id_receta);

                foreach (var m in relProdRec)
                {
                    productos ingrediente = db.productos.Find(m);
                    ingredientes.Add(ingrediente);
                }

                ViewBag.ing = ingredientes;                
                // Fin Ingredientes

                listaRecetas.Add(elementoReceta);
            }
            return View(listaRecetas);
            // return View(db.recetas.ToList());
        }

        // GET: recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recetas recetas = db.recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return View(recetas);
        }

        // GET: recetas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: recetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_receta,precio,nombre,fk_producto_padre")] recetas recetas)
        {
            if (ModelState.IsValid)
            {
                db.recetas.Add(recetas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recetas);
        }

        // GET: recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recetas recetas = db.recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return View(recetas);
        }

        // POST: recetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_receta,precio,nombre,fk_producto_padre")] recetas recetas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recetas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recetas);
        }

        // GET: recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recetas recetas = db.recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return View(recetas);
        }

        // POST: recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            recetas recetas = db.recetas.Find(id);
            db.recetas.Remove(recetas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

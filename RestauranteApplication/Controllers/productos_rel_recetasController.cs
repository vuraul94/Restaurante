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
    public class productos_rel_recetasController : Controller
    {
        private RestauranteEntities db = new RestauranteEntities();      

        // GET: productos_rel_recetas
        public ActionResult Index()
        {
            var productos_rel_recetas = db.productos_rel_recetas.Include(p => p.productos).Include(p => p.recetas);
            return View(productos_rel_recetas.ToList());
        }

        // GET: productos_rel_recetas/Details/5
        public ActionResult Details(int fk_producto, int fk_receta)
        {
            //if (fk_producto == null && fk_receta == null)
            //{
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            productos_rel_recetas productos_rel_recetas = db.productos_rel_recetas.Find(fk_producto, fk_receta);
            if (productos_rel_recetas == null)
            {
                return HttpNotFound();
            }
            return View(productos_rel_recetas);
        }

        // GET: productos_rel_recetas/Create
        public ActionResult Create()
        {
            ViewBag.fk_producto = new SelectList(db.productos, "id_producto", "nombre");
            ViewBag.fk_receta = new SelectList(db.recetas, "id_receta", "nombre");            

            return View();
        }

        // POST: productos_rel_recetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fk_producto,fk_receta,cantidad,unidad")] productos_rel_recetas productos_rel_recetas)
        {
            if (ModelState.IsValid)
            {
                db.productos_rel_recetas.Add(productos_rel_recetas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_producto = new SelectList(db.productos, "id_producto", "nombre", productos_rel_recetas.fk_producto);
            ViewBag.fk_receta = new SelectList(db.recetas, "id_receta", "nombre", productos_rel_recetas.fk_receta);
            return View(productos_rel_recetas);
        }

        // GET: productos_rel_recetas/Edit/5
        public ActionResult Edit(int fk_producto, int fk_receta)
        {
            //if (id == null)
            //{
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            productos_rel_recetas productos_rel_recetas = db.productos_rel_recetas.Find(fk_producto,fk_receta);
            if (productos_rel_recetas == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_producto = new SelectList(db.productos, "id_producto", "nombre", productos_rel_recetas.fk_producto);
            ViewBag.fk_receta = new SelectList(db.recetas, "id_receta", "nombre", productos_rel_recetas.fk_receta);
            return View(productos_rel_recetas);
        }

        // POST: productos_rel_recetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fk_producto,fk_receta,cantidad,unidad")] productos_rel_recetas productos_rel_recetas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos_rel_recetas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_producto = new SelectList(db.productos, "id_producto", "nombre", productos_rel_recetas.fk_producto);
            ViewBag.fk_receta = new SelectList(db.recetas, "id_receta", "nombre", productos_rel_recetas.fk_receta);
            return View(productos_rel_recetas);
        }

        // GET: productos_rel_recetas/Delete/5
        public ActionResult Delete(int fk_producto, int fk_receta)
        {
            //if (fk_producto == null)
            //{
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            productos_rel_recetas productos_rel_recetas = db.productos_rel_recetas.Find(fk_producto, fk_receta);
            if (productos_rel_recetas == null)
            {
                return HttpNotFound();
            }
            return View(productos_rel_recetas);
        }

        // POST: productos_rel_recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int fk_producto, int fk_receta)
        {
            productos_rel_recetas productos_rel_recetas = db.productos_rel_recetas.Find(fk_producto, fk_receta);
            db.productos_rel_recetas.Remove(productos_rel_recetas);
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

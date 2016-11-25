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
    public class productosController : Controller
    {
        private RestauranteEntities db = new RestauranteEntities();

        // GET: productos
        public ActionResult Index()
        {
            List<ProductoMulti> listaProductos = new List<ProductoMulti>();
            foreach (var producto in db.productos.ToList())
            {
                ProductoMulti elementoProducto = new ProductoMulti();

                IQueryable<proveedores> proveedors = db.proveedores.Where(proveedores => proveedores.id_proveedor == producto.fk_proveedor);

                elementoProducto.productos = producto;
                int contador = 1;
                foreach (var provee in proveedors)
                {
                    if (contador == 1)
                    {
                        elementoProducto.proveedorFK = provee;
                    }
                    contador++;
                }

                listaProductos.Add(elementoProducto);
            }
            return View(listaProductos);

            // var productos = db.productos.Include(p => p.proveedores);
            // return View(productos.ToList());
        }

        // GET: productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: productos/Create
        public ActionResult Create()
        {
            ViewBag.fk_proveedor = new SelectList(db.proveedores, "id_proveedor", "nombre");
            return View();
        }

        // POST: productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto,nombre,cantidad,precio,ingreso,vencimiento,fk_proveedor")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_proveedor = new SelectList(db.proveedores, "id_proveedor", "nombre", productos.fk_proveedor);
            return View(productos);
        }

        // GET: productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_proveedor = new SelectList(db.proveedores, "id_proveedor", "nombre", productos.fk_proveedor);
            return View(productos);
        }

        // POST: productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto,nombre,cantidad,precio,ingreso,vencimiento,fk_proveedor")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_proveedor = new SelectList(db.proveedores, "id_proveedor", "nombre", productos.fk_proveedor);
            return View(productos);
        }

        // GET: productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos productos = db.productos.Find(id);
            db.productos.Remove(productos);
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

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
    public class proveedoresController : Controller
    {
        private RestauranteEntities db = new RestauranteEntities();

        // GET: proveedores
        public ActionResult Index()
        {
            List<ProveedorMulti> listaProveedores= new List<ProveedorMulti>();
            foreach (var proveedor in db.proveedores.ToList())
            {
                ProveedorMulti elementoProveedor= new ProveedorMulti();
                IQueryable<proveedor_telefonos> telefonos = db.proveedor_telefonos.Where(proveedor_telefonos => proveedor_telefonos.fk_proveedor == proveedor.id_proveedor);
                elementoProveedor.proveedores = proveedor;
                int contador = 1;
                foreach (var telefono in telefonos)
                {
                    if (contador == 1)
                    {
                        elementoProveedor.telefono1 = telefono;
                    }
                    else
                    {
                        elementoProveedor.telefono2 = telefono;
                    }
                    contador++;
                }
                listaProveedores.Add(elementoProveedor);
            }
            return View(listaProveedores);
        }

        // GET: proveedores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedores proveedores = db.proveedores.Find(id);
            IQueryable<proveedor_telefonos> telefonos = db.proveedor_telefonos.Where(proveedor_telefonos => proveedor_telefonos.fk_proveedor == id);
            ProveedorMulti proveedor = new ProveedorMulti();
            proveedor_telefonos telefono1 = new proveedor_telefonos();
            proveedor_telefonos telefono2 = new proveedor_telefonos();
            int contador = 1;
            foreach (var telefono in telefonos)
            {
                if (contador == 1)
                {
                    telefono1 = telefono;
                }
                else
                {
                    telefono2 = telefono;
                }
                contador++;
            }
            proveedor.proveedores = proveedores;
            proveedor.telefono1 = telefono1;
            proveedor.telefono2 = telefono2;
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre")] string nombre, 
                                   [Bind(Include = "direccion")] string direccion,
                                   [Bind(Include = "email")] string email,
                                   [Bind(Include = "tel1")] string tel1,
                                   [Bind(Include = "tel2")] string tel2)
        {
            if (ModelState.IsValid)
            {
                proveedores proveedor = new proveedores();
                proveedor_telefonos telefono1 = new proveedor_telefonos();
                proveedor_telefonos telefono2 = new proveedor_telefonos();
                proveedor.id_proveedor = (nombre).Substring(0, 2) + DateTime.UtcNow.ToString().Replace("/","").Replace(" ","").Replace(":","");
                proveedor.nombre = nombre;
                proveedor.direccion = direccion;
                proveedor.email = email;

                telefono1.fk_proveedor = proveedor.id_proveedor;
                telefono1.telefono = tel1;
                telefono2.fk_proveedor = proveedor.id_proveedor;
                telefono2.telefono = tel2;

                db.proveedores.Add(proveedor);
                db.proveedor_telefonos.Add(telefono1);
                db.proveedor_telefonos.Add(telefono2);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: proveedores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedores proveedores = db.proveedores.Find(id);
            IQueryable<proveedor_telefonos> telefonos = db.proveedor_telefonos.Where(proveedor_telefonos => proveedor_telefonos.fk_proveedor == id);
            ProveedorMulti proveedor= new ProveedorMulti();
            proveedor_telefonos telefono1= new proveedor_telefonos();
            proveedor_telefonos telefono2= new proveedor_telefonos();
            int contador=1;
            foreach (var telefono in telefonos)
            {
                if (contador == 1)
                {
                    telefono1 = telefono;
                }
                else
                {
                    telefono2 = telefono;
                }
                contador++;
            }
            proveedor.proveedores = proveedores;
            proveedor.telefono1 = telefono1;
            proveedor.telefono2 = telefono2;
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "proveedores_id_proveedor")] string id, 
                                   [Bind(Include = "nombre")] string nombre,
                                   [Bind(Include = "direccion")] string direccion,
                                   [Bind(Include = "email")] string email,
                                   [Bind(Include = "tel1")] string tel1,
                                   [Bind(Include = "tel2")] string tel2)
        {
            if (ModelState.IsValid)
            {
                proveedores proveedor = new proveedores();
                IEnumerable<proveedor_telefonos> telefonos = db.proveedor_telefonos.Where(proveedor_telefonos => proveedor_telefonos.fk_proveedor == id);
                proveedor_telefonos telefono1 = new proveedor_telefonos();
                proveedor_telefonos telefono2 = new proveedor_telefonos();
                proveedor.id_proveedor = id;
                proveedor.nombre = nombre;
                proveedor.direccion = direccion;
                proveedor.email = email;
                int counter = 1;
                foreach (var telefono in telefonos)
                {
                    if (counter == 1)
                    {
                        telefono.telefono = tel1;
                        telefono1 = telefono;
                    }
                    else
                    {
                        telefono.telefono = tel2;
                        telefono2 = telefono;
                    }
                    counter++;

                }
                db.Entry(telefono1).State = EntityState.Modified;
                db.Entry(telefono2).State = EntityState.Modified;
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: proveedores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedores proveedores = db.proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            proveedores proveedores = db.proveedores.Find(id);
            IEnumerable<proveedor_telefonos> telefonos = db.proveedor_telefonos.Where(proveedor_telefonos=>proveedor_telefonos.fk_proveedor==id);
            db.proveedores.Remove(proveedores);
            foreach (var telefono in telefonos)
            {
                db.proveedor_telefonos.Remove(telefono);
            }
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

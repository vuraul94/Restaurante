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
    public class CajaController : Controller
    {
        private RestauranteEntities db = new RestauranteEntities();
        private List<productos> productos_en_caja = new List<productos>();

        // GET: Caja
        public ActionResult Index()
        {
            Session["productos_caja"] = new List<productos>();
            ViewBag.total = 0;
            return View();
        }

        public ActionResult agregar_a_caja(int? id_producto)
        {
            if (id_producto != null)
            {
                productos producto = db.productos.Find(id_producto);
                productos_en_caja = (List<productos>)Session["productos_caja"];
                productos_en_caja.Add(producto);
                Session["productos_caja"] = productos_en_caja;
                ViewBag.productos_caja = Session["productos_caja"];
                ViewBag.total = 0;
                foreach (var product in (List<productos>)Session["productos_caja"])
                {
                    ViewBag.total += product.precio;
                    Session["total"] = ViewBag.total;
                }
            }

            return View("Index");
        }

        public ActionResult facturar(string id_cliente, decimal monto_pago)
        {
            string id_factura = "fact-" + DateTime.UtcNow.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace("am", "").Replace("pm", "");
            facturas factura = new facturas();
            factura.id_factura = id_factura;
            factura.fk_cliente = id_cliente;
            factura.credito = false;
            factura.total= (decimal?) Session["total"];
            db.facturas.Add(factura);
            foreach (var producto in (List<productos>)Session["productos_caja"])
            {
                factura_productos fact_pro = new factura_productos();
                fact_pro.fk_factura = id_factura;
                fact_pro.fk_producto = producto.id_producto;
                db.factura_productos.Add(fact_pro);
            }
            db.SaveChanges();

            if (id_cliente == "")
            {
                ViewBag.n_cliente = "Cliente X";
            }
            else
            {
                ViewBag.n_cliente = db.clientes.Find(id_cliente).nombre;
            }
            ViewBag.id_factura= id_factura;
            ViewBag.total = (decimal?) Session["total"];
            ViewBag.cambio = monto_pago-ViewBag.total;
            return View("Factura");
        }

        public ActionResult Factura()
        {
            return View();
        }

    }
}
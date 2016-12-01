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
            return View();
        }

        public ActionResult agregar_a_caja(int? id_producto)
        {
            if (id_producto !=null)
            {
                productos producto = db.productos.Find(id_producto);
                productos_en_caja = (List<productos>) Session["productos_caja"];
                productos_en_caja.Add(producto);
                Session["productos_caja"] = productos_en_caja;
                ViewBag.productos_caja = Session["productos_caja"];
            }

            return View("Index");
        }
    }
}
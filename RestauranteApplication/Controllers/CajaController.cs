﻿using System;
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
            ViewBag.productos_caja = productos_en_caja;
            return View();
        }

        public ActionResult agregar_a_caja(int? id_producto)
        {
            if (id_producto !=null)
            {
                productos producto = db.productos.Find(id_producto);
                productos_en_caja.Add(producto);
                ViewBag.productos_caja=productos_en_caja;
            }

            return View("Index");
        }
    }
}
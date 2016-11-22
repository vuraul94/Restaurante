using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestauranteApplication.Models;

namespace RestauranteApplication.Models
{
    public class ProveedorMulti
    {
        public proveedores proveedores { get; set; }
        public proveedor_telefonos telefono1 { get; set; }
        public proveedor_telefonos telefono2 { get; set; }
    }
}
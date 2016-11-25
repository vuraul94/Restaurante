using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestauranteApplication.Models;

namespace RestauranteApplication.Models
{
    public class ProductoMulti
    {
        public productos productos { get; set; }
        public proveedores proveedorFK { get; set; }
    }
}
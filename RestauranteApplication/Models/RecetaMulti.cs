using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteApplication.Models
{
    public class RecetaMulti
    {
        public recetas recetas { get; set; }
        public productos productoFK { get; set; }

    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestauranteApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class factura_recetas
    {
        public int id { get; set; }
        public Nullable<int> fk_factura { get; set; }
        public Nullable<int> fk_receta { get; set; }
    
        public virtual facturas facturas { get; set; }
        public virtual recetas recetas { get; set; }
    }
}
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
    
    public partial class creditos
    {
        public int id_credito { get; set; }
        public decimal monto { get; set; }
        public Nullable<System.DateTime> inicio { get; set; }
        public System.DateTime vencimiento { get; set; }
        public bool cancelado { get; set; }
        public string fk_cliente { get; set; }
    
        public virtual clientes clientes { get; set; }
    }
}

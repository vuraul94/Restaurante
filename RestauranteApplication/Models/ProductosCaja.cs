using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestauranteApplication.Models;

namespace RestauranteApplication
{
    public class ProductosCaja : IEquatable<ProductosCaja>
    {
        public int Cantidad { get; set; }
        private int _IdProducto;
        private productos _producto = null;

        public int IdProducto
        {
            get { return _IdProducto; }
            set
            {
                _producto = null;
                _IdProducto = value;
            }
        }
        public productos Producto
        {
            get
            {
                if (_producto == null)
                {
                    _producto = from producto in productos  where producto.id_
                    

                }
                return _producto;
            }
        }

        public bool Equals(ProductosCaja pItem)
        {
            return pItem.IdProducto == IdProducto;
        }
    }
}
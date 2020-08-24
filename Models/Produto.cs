using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public partial class Produto
    {
        public Produto()
        {
            ItemCompra = new HashSet<ItemCompra>();
        }

        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public int? Quantidade { get; set; }
        public double? PrecoUnitario { get; set; }
        public int? Codigo { get; set; }

        public virtual ICollection<ItemCompra> ItemCompra { get; set; }
    }
}

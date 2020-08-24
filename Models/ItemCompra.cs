using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public partial class ItemCompra
    {
        public int IdItemCompra { get; set; }
        public int? IdProduto { get; set; }
        public int? IdCarinhoCompra { get; set; }

        public virtual CarinhoCompra IdCarinhoCompraNavigation { get; set; }
        public virtual Produto IdProdutoNavigation { get; set; }
    }
}

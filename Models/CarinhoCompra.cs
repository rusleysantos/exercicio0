using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public partial class CarinhoCompra
    {
        public CarinhoCompra()
        {
            ItemCompra = new HashSet<ItemCompra>();
        }

        public int IdCarinhoCompra { get; set; }
        public string ValorTotal { get; set; }

        public virtual ICollection<ItemCompra> ItemCompra { get; set; }
    }
}

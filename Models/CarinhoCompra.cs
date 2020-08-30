using System;
using System.Collections.Generic;
using System.Linq;

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


        public static CarinhoCompra operator +(CarinhoCompra carinho1, CarinhoCompra carinho2)
        {
            int idSoma = 0;

            using (Contexto con = new Contexto())
            {
                CarinhoCompra carinho3 = new CarinhoCompra();
                carinho3.ValorTotal = (Convert.ToDouble(carinho1.ValorTotal) + Convert.ToDouble(carinho2.ValorTotal)).ToString();

                con.CarinhoCompra.Add(carinho3);
                con.SaveChanges();
                idSoma = carinho3.IdCarinhoCompra;

                var result = con.ItemCompra.Where(x => x.IdCarinhoCompra == carinho1.IdCarinhoCompra || x.IdCarinhoCompra == carinho2.IdCarinhoCompra).ToList();

                foreach (var itemCompra in result)
                {
                    itemCompra.IdCarinhoCompra = idSoma;
                    con.SaveChanges();
                }

                return con.CarinhoCompra.Where(x => x.IdCarinhoCompra == carinho3.IdCarinhoCompra).First();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentProject
{
    class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SupplierID { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }

        public Product(string n, int id, int p, int i)
        {
            this.Name = n;
            this.SupplierID = id;
            this.Price = p;
            this.Inventory = i;
        }
    }
}

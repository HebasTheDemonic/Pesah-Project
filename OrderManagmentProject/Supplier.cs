using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentProject
{
    class Supplier : User
    {
        public string CompanyName { get; set; }
        public int ID { get; set; }

        public Supplier()
        {

        }

        public Supplier(string u, string p, string c)
        {
            Username = u;
            Password = p;
            CompanyName = c;
        }
    }
}

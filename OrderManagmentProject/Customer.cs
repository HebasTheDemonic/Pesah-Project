using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentProject
{
    class Customer : User
    {
        string _firstName;
        string _lastName;
        int _creditNumber;
        int _customerID;

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        public int CreditNumber
        {
            get
            {
                return _creditNumber;
            }
            set
            {
                _creditNumber = value;
            }
        }
        public int CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                _customerID = value;
            }
        }

        public Customer()
        {

        }

        public Customer(string u, string p, string f, string l, int c)
        {
            this.Username = u;
            this.Password = p;
            this.FirstName = f;
            this.LastName = l;
            this.CreditNumber = c;
        }
    }
}

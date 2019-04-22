using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using DAL;
using HELPER;
using MODEL;

namespace MODEL.Mapper
{
    public class CustomersDB : BaseDB<Customer>
    {
        public List<Customer> customers;

        public void SelectCustomers(string sql)
        {
            Query = sql;

            try
            {
                customers = SelectList<Customer>().ToList();
            }
            catch (Exception e)
            {
                customers = null;
            }
        }

        public void SelectAllCustomers()
        {
            SelectCustomers("SELECT * FROM Customers");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectCustomerByNum(int num)
        {
            SelectCustomers("SELECT * FROM Customers WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

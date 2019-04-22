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
    public class CustomerOrdersDB : BaseDB<CustomerOrder>
    {
        public List<CustomerOrder> customerOrders;

        public void SelectCustomerOrders(string sql)
        {
            Query = sql;

            try
            {
                customerOrders = SelectList<CustomerOrder>().ToList();
            }
            catch (Exception e)
            {
                customerOrders = null;
            }
        }

        public void SelectAllCustomerOrders()
        {
            SelectCustomerOrders("SELECT * FROM CustomerOrders");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectCustomerOrderByNum(int num)
        {
            SelectCustomerOrders("SELECT * FROM CustomerOrders WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

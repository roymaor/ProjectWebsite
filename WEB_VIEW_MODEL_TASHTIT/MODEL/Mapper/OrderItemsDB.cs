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
    public class OrderItemsDB : BaseDB<OrderItem>
    {
        public List<OrderItem> orderItems;

        public void SelectOrderItems(string sql)
        {
            Query = sql;

            try
            {
                orderItems = SelectList<OrderItem>().ToList();
            }
            catch (Exception e)
            {
                orderItems = null;
            }
        }

        public void SelectAllOrderItems()
        {
            SelectOrderItems("SELECT * FROM OrderItems");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectOrderItemByNum(int num)
        {
            SelectOrderItems("SELECT * FROM OrderItems WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

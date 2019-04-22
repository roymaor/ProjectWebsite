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
    public class SalesDB : BaseDB<Sale>
    {
        public List<Sale> sales;

        public void SelectSales(string sql)
        {
            Query = sql;

            try
            {
                sales = SelectList<Sale>().ToList();
            }
            catch (Exception e)
            {
                sales = null;
            }
        }

        public void SelectAllSales()
        {
            SelectSales("SELECT * FROM Sales");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectSaleByNum(int num)
        {
            SelectSales("SELECT * FROM Sales WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

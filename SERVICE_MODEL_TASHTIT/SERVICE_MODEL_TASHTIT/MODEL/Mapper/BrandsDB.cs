using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Runtime.Serialization;

using DAL;
using HELPER;
using MODEL;

namespace MODEL.Mapper
{
    public class BrandsDB : BaseDB<Brand>
    {
        public List<Brand> brands;

        public void SelectBrands(string sql)
        {
            Query = sql;

            try
            {
                brands = SelectList<Brand>().ToList();
            }
            catch (Exception e)
            {
                brands = null;
            }
        }

        public void SelectAllBrands()
        {
            SelectBrands("SELECT * FROM Brands ORDER BY Name ");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectBrandByNum(int num)
        {
            SelectBrands("SELECT * FROM Brands WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

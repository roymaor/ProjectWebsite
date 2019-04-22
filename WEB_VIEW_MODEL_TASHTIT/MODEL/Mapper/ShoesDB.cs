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
    public class ShoesDB : BaseDB<Sho>
    {
        public List<Sho> shoes;

        public void SelectShoes(string sql)
        {
            Query = sql;

            try
            {
                shoes = SelectList<Sho>().ToList();
            }
            catch (Exception e)
            {
                shoes = null;
            }
        }

        public void SelectAllShoes()
        {
            SelectShoes("SELECT * FROM Shoes");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectShoByNum(int num)
        {
            SelectShoes("SELECT * FROM Shoes WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

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
    public class SizesDB : BaseDB<Size>
    {
        public List<Size> sizes;

        public void SelectSizes(string sql)
        {
            Query = sql;

            try
            {
                sizes = SelectList<Size>().ToList();
            }
            catch (Exception e)
            {
                sizes = null;
            }
        }

        public void SelectAllSizes()
        {
            SelectSizes("SELECT * FROM Sizes ORDER BY Name ");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectSizeByNum(int num)
        {
            SelectSizes("SELECT * FROM Sizes WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

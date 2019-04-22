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
    public class CategoriesDB : BaseDB<Category>
    {
        public List<Category> categories;

        public void SelectCategories(string sql)
        {
            Query = sql;

            try
            {
                categories = SelectList<Category>().ToList();
            }
            catch (Exception e)
            {
                categories = null;
            }
        }

        public void SelectAllCategories()
        {
            SelectCategories("SELECT * FROM Categories ORDER BY Name ");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectCategoryByNum(int num)
        {
            SelectCategories("SELECT * FROM Categories WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

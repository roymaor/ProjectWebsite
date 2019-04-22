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
    public class CitiesDB : BaseDB<City>
    {
        public List<City> cities;

        public void SelectCities(string sql)
        {
            Query = sql;

            try
            {
                cities = SelectList<City>().ToList();
            }
            catch (Exception e)
            {
                cities = null;
            }
        }

        public void SelectAllCities()
        {
            SelectCities("SELECT * FROM Cities ORDER BY Name ");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectCityByNum(int num)
        {
            SelectCities("SELECT * FROM Cities WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

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
    public class AreaCodesDB : BaseDB<AreaCode>
    {
        public List<AreaCode> areaCodes;

        public void SelectAreaCodes(string sql)
        {
            Query = sql;

            try
            {
                areaCodes = SelectList<AreaCode>().ToList();
            }
            catch (Exception e)
            {
                areaCodes = null;
            }
        }

        public void SelectAllAreaCodes()
        {
            SelectAreaCodes("SELECT * FROM AreaCodes ORDER BY Name  ");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectAreaCodeByNum(int num)
        {
            SelectAreaCodes("SELECT * FROM AreaCodes WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

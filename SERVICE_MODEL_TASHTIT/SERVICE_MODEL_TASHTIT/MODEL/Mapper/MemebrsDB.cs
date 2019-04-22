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
    public class MembersDB : BaseDB<Member>
    {
        public List<Member> members;

        public void SelectMembers(string sql)
        {
            Query = sql;

            try
            {
                members = SelectList<Member>().ToList();
            }
            catch (Exception e)
            {
                members = null;
            }
        }

        public void SelectAllMembers()
        {
            SelectMembers("SELECT * FROM Memebers");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectMemberByNum(int num)
        {
            SelectMembers("SELECT * FROM Memebers WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

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
    public class RolesDB : BaseDB<Role>
    {
        public List<Role> roles;

        public void SelectRoles(string sql)
        {
            Query = sql;

            try
            {
                roles = SelectList<Role>().ToList();
            }
            catch (Exception e)
            {
                roles = null;
            }
        }

        public void SelectAllRoles()
        {
            SelectRoles("SELECT * FROM Roles ORDER BY Name ");
            //
            // הוסף/י שדה/ות למיון
            // ORDER BY שדה למיון
            //
        }

        public void SelectRoleByNum(int num)
        {
            SelectRoles("SELECT * FROM Roles WHERE Num = " + num);
        }

        //
        // הוסף/י כאן שאילתות נוספות בהתאם לצורך
        //

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Runtime.Serialization;

using MODEL.Mapper;
using HELPER;

namespace MODEL
{
    [CollectionDataContract]
    [KnownType (typeof(BaseList<Role>))]
    [KnownType (typeof(Role))]
    public class Roles : BaseList<Role>
    {
        private RolesDB rolesDB;

        public Roles()
        {
            rolesDB = new RolesDB();
        }

        #region Select methods

        public void SelectAll()
        {
            rolesDB.SelectAllRoles();
            AddRange(rolesDB.roles);
        }

        public void SelectRoleByNum(int num)
        {
            rolesDB.SelectRoleByNum(num);
            AddRange(rolesDB.roles);
        }

        public Role GetRoleByNum(int num)
        {
            SelectRoleByNum (num);
            if (rolesDB.roles == null || rolesDB.roles.Count == 0)
                return null;
            else
                return rolesDB.roles[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Role entity)
        {
            Role role;
            return Exist(entity, out role);
        }

        protected override bool Exist(Role entity, out Role existEntity)
        {
            existEntity = Find(item => item.Name == entity.Name);   // קיים ברשימה Role הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
            return existEntity != null;
            //
            // : לדוגמא בדיקה לפי שדה אחד
            // existEntity = Find(item => item.Name == entity.Name) 
            //
            // : לדוגמא בדיקה לפי שני שדות
            // existEntity = Find(item => item.Family == entity.Family && item.Name == entity.Name) 
            //
        }

        protected override void Sort()
        {
            // Role כתוב/י כאן כיצד למיין אובייקטים מסוג 
            //
            // : דוגמא למיון עפ"י שדה אחד
            // base.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
            //
            // : דוגמא למיון עפ"י שני שדות
            // base.Sort((item1, item2) =>
            //     { 
            //         int isSame = item1.Family.CompareTo(item2.Family);
            //         return (isSame != 0) ? isSame : item1.Name.CompareTo(item2.Name);
            //     }
            //  );
        }

        public override void SetObjects(Role entity)
        {
            // אם נדרש - Role רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = rolesDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

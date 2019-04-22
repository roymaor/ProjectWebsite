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
    [KnownType(typeof(BaseList<Member>))]
    [KnownType(typeof(Member))]
    public class Members : BaseList<Member>
    {
        private MembersDB membersDB;

        public Members()
        {
            membersDB = new MembersDB();
        }

        #region Select methods

        public void SelectAll()
        {
            membersDB.SelectAllMembers();
            AddRange(membersDB.members);
        }

        public void SelectCityByNum(int num)
        {
            membersDB.SelectMemberByNum(num);
            AddRange(membersDB.members);
        }

        public Member GetMemberByNum(int num)
        {
            SelectCityByNum(num);
            if (membersDB.members == null || membersDB.members.Count == 0)
                return null;
            else
                return membersDB.members[0];
        }

        //                
        // Add here select methods as needed                
        //

        #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Member entity)
        {
            Member member;
            return Exist(entity, out member);
        }

        protected override bool Exist(Member entity, out Member existEntity)
        {
            existEntity = Find(item => item.Name == entity.Name);    // קיים ברשימה City הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            base.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));

            // City כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(Member entity)
        {
            // אם נדרש - City רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = membersDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

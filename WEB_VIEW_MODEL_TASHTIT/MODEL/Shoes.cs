using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using MODEL.Mapper;
using HELPER;

namespace MODEL
{
    public class Shoes : BaseList<Sho>
    {
        private ShoesDB shoesDB;

        public Shoes()
        {
            shoesDB = new ShoesDB();
        }

        #region Select methods

        public void SelectAll()
        {
            shoesDB.SelectAllShoes();
            AddRange(shoesDB.shoes);
        }

        public void SelectShoByNum(int num)
        {
            shoesDB.SelectShoByNum(num);
            AddRange(shoesDB.shoes);
        }

        public Sho GetShoByNum(int num)
        {
            SelectShoByNum (num);
            if (shoesDB.shoes == null || shoesDB.shoes.Count == 0)
                return null;
            else
                return shoesDB.shoes[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Sho entity)
        {
            Sho sho;
            return Exist(entity, out sho);
        }

        protected override bool Exist(Sho entity, out Sho existEntity)
        {
            existEntity = null;     // קיים ברשימה Sho הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // Sho כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(Sho entity)
        {
            // אם נדרש - Sho רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = shoesDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

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
    [KnownType (typeof(BaseList<Size>))]
    [KnownType (typeof(Size))]
    public class Sizes : BaseList<Size>
    {
        private SizesDB sizesDB;

        public Sizes()
        {
            sizesDB = new SizesDB();
        }

        #region Select methods

        public void SelectAll()
        {
            sizesDB.SelectAllSizes();
            AddRange(sizesDB.sizes);
        }

        public void SelectSizeByNum(int num)
        {
            sizesDB.SelectSizeByNum(num);
            AddRange(sizesDB.sizes);
        }

        public Size GetSizeByNum(int num)
        {
            SelectSizeByNum (num);
            if (sizesDB.sizes == null || sizesDB.sizes.Count == 0)
                return null;
            else
                return sizesDB.sizes[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Size entity)
        {
            Size size;
            return Exist(entity, out size);
        }

        protected override bool Exist(Size entity, out Size existEntity)
        {
            existEntity = Find(item => item.Name == entity.Name);   // קיים ברשימה Size הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // Size כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(Size entity)
        {
            // אם נדרש - Size רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = sizesDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

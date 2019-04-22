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
    [KnownType (typeof(BaseList<AreaCode>))]
    [KnownType (typeof(AreaCode))]
    public class AreaCodes : BaseList<AreaCode>
    {
        private AreaCodesDB areaCodesDB;

        public AreaCodes()
        {
            areaCodesDB = new AreaCodesDB();
        }

        #region Select methods

        public void SelectAll()
        {
            areaCodesDB.SelectAllAreaCodes();
            AddRange(areaCodesDB.areaCodes);
        }

        public void SelectAreaCodeByNum(int num)
        {
            areaCodesDB.SelectAreaCodeByNum(num);
            AddRange(areaCodesDB.areaCodes);
        }

        public AreaCode GetAreaCodeByNum(int num)
        {
            SelectAreaCodeByNum (num);
            if (areaCodesDB.areaCodes == null || areaCodesDB.areaCodes.Count == 0)
                return null;
            else
                return areaCodesDB.areaCodes[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(AreaCode entity)
        {
            AreaCode areaCode;
            return Exist(entity, out areaCode);
        }

        protected override bool Exist(AreaCode entity, out AreaCode existEntity)
        {
            existEntity = Find(item => item.Name == entity.Name);   // קיים ברשימה AreaCode הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // AreaCode כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(AreaCode entity)
        {
            // אם נדרש - AreaCode רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = areaCodesDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

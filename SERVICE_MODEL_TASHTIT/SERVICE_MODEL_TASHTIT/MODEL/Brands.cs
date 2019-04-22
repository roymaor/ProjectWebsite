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
    [KnownType (typeof(BaseList<Brand>))]
    [KnownType (typeof(Brand))]
    public class Brands : BaseList<Brand>
    {
        private BrandsDB brandsDB;

        public Brands()
        {
            brandsDB = new BrandsDB();
        }

        #region Select methods

        public void SelectAll()
        {
            brandsDB.SelectAllBrands();
            AddRange(brandsDB.brands);
        }

        public void SelectBrandByNum(int num)
        {
            brandsDB.SelectBrandByNum(num);
            AddRange(brandsDB.brands);
        }

        public Brand GetBrandByNum(int num)
        {
            SelectBrandByNum (num);
            if (brandsDB.brands == null || brandsDB.brands.Count == 0)
                return null;
            else
                return brandsDB.brands[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Brand entity)
        {
            Brand brand;
            return Exist(entity, out brand);
        }

        protected override bool Exist(Brand entity, out Brand existEntity)
        {
            existEntity = Find(item => item.Name == entity.Name);  // קיים ברשימה Brand הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // Brand כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(Brand entity)
        {
            // אם נדרש - Brand רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = brandsDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

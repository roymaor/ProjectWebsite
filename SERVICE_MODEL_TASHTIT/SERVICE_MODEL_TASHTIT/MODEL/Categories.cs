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
    [KnownType (typeof(BaseList<Category>))]
    [KnownType (typeof(Category))]
    public class Categories : BaseList<Category>
    {
        private CategoriesDB categoriesDB;

        public Categories()
        {
            categoriesDB = new CategoriesDB();
        }

        #region Select methods

        public void SelectAll()
        {
            categoriesDB.SelectAllCategories();
            AddRange(categoriesDB.categories);
        }

        public void SelectCategoryByNum(int num)
        {
            categoriesDB.SelectCategoryByNum(num);
            AddRange(categoriesDB.categories);
        }

        public Category GetCategoryByNum(int num)
        {
            SelectCategoryByNum (num);
            if (categoriesDB.categories == null || categoriesDB.categories.Count == 0)
                return null;
            else
                return categoriesDB.categories[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Category entity)
        {
            Category category;
            return Exist(entity, out category);
        }

        protected override bool Exist(Category entity, out Category existEntity)
        {
            existEntity = Find(item => item.Name == entity.Name);    // קיים ברשימה Category הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // Category כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(Category entity)
        {
            // אם נדרש - Category רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = categoriesDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

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
    [KnownType (typeof(BaseList<City>))]
    [KnownType (typeof(City))]
    public class Cities : BaseList<City>
    {
        private CitiesDB citiesDB;

        public Cities()
        {
            citiesDB = new CitiesDB();
        }

        #region Select methods

        public void SelectAll()
        {
            citiesDB.SelectAllCities();
            AddRange(citiesDB.cities);
        }

        public void SelectCityByNum(int num)
        {
            citiesDB.SelectCityByNum(num);
            AddRange(citiesDB.cities);
        }

        public City GetCityByNum(int num)
        {
            SelectCityByNum (num);
            if (citiesDB.cities == null || citiesDB.cities.Count == 0)
                return null;
            else
                return citiesDB.cities[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(City entity)
        {
            City city;
            return Exist(entity, out city);
        }

        protected override bool Exist(City entity, out City existEntity)
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

        public override void SetObjects(City entity)
        {
            // אם נדרש - City רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = citiesDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using MODEL.Mapper;
using HELPER;

namespace MODEL
{
    public class Sales : BaseList<Sale>
    {
        private SalesDB salesDB;
        public Shoes shoes;

        public Sales()
        {
            salesDB = new SalesDB();

            shoes = new Shoes();
            shoes.SelectAll();
        }

        #region Select methods

        public void SelectAll()
        {
            salesDB.SelectAllSales();
            AddRange(salesDB.sales);

            foreach(Sale sale in this)
                SetObjects(sale);
        }

        public void SelectSaleByNum(int num)
        {
            salesDB.SelectSaleByNum(num);
            AddRange(salesDB.sales);

            foreach(Sale sale in this)
                SetObjects(sale);
        }

        public Sale GetSaleByNum(int num)
        {
            SelectSaleByNum (num);
            if (salesDB.sales == null || salesDB.sales.Count == 0)
                return null;
            else
                return salesDB.sales[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Sale entity)
        {
            Sale sale;
            return Exist(entity, out sale);
        }

        protected override bool Exist(Sale entity, out Sale existEntity)
        {
            existEntity = null;     // קיים ברשימה Sale הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // Sale כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(Sale entity)
        {
            entity.SetSho(shoes);
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = salesDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

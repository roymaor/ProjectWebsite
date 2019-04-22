using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using MODEL.Mapper;
using HELPER;

namespace MODEL
{
    public class CustomerOrders : BaseList<CustomerOrder>
    {
        private CustomerOrdersDB customerOrdersDB;
        public Customers customers;

        public CustomerOrders()
        {
            customerOrdersDB = new CustomerOrdersDB();

            customers = new Customers();
            customers.SelectAll();
        }

        #region Select methods

        public void SelectAll()
        {
            customerOrdersDB.SelectAllCustomerOrders();
            AddRange(customerOrdersDB.customerOrders);

            foreach(CustomerOrder customerOrder in this)
                SetObjects(customerOrder);
        }

        public void SelectCustomerOrderByNum(int num)
        {
            customerOrdersDB.SelectCustomerOrderByNum(num);
            AddRange(customerOrdersDB.customerOrders);

            foreach(CustomerOrder customerOrder in this)
                SetObjects(customerOrder);
        }

        public CustomerOrder GetCustomerOrderByNum(int num)
        {
            SelectCustomerOrderByNum (num);
            if (customerOrdersDB.customerOrders == null || customerOrdersDB.customerOrders.Count == 0)
                return null;
            else
                return customerOrdersDB.customerOrders[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(CustomerOrder entity)
        {
            CustomerOrder customerOrder;
            return Exist(entity, out customerOrder);
        }

        protected override bool Exist(CustomerOrder entity, out CustomerOrder existEntity)
        {
            existEntity = null;     // קיים ברשימה CustomerOrder הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // CustomerOrder כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(CustomerOrder entity)
        {
            entity.SetCustomer(customers);
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = customerOrdersDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using MODEL.Mapper;
using MODEL.MyService;                                                                                                                                                                                                                                                          
using HELPER;

namespace MODEL
{
    
    
    public class Customers : BaseList<Customer>
    {
        private Cities cities;
        private AreaCodes areaCodes;
        private Roles roles;
        private MyServiceClient serviceClient;

        private CustomersDB customersDB;
       
        public Customers()
        {
            customersDB = new CustomersDB();
            serviceClient = new MyServiceClient();
        }

        #region Select methods

        public void SelectAll()

        {   roles= serviceClient.SelectAllRoles();
            areaCodes = serviceClient.SelectAllAreaCodes();
            cities = serviceClient.SelectAllCities();
            customersDB.SelectAllCustomers();
            AddRange(customersDB.customers);
            foreach (Customer m in this)
                SetObjects(m);
        }

        public void SelectCustomerByNum(int num)
        {
             roles= serviceClient.SelectAllRoles();
            areaCodes = serviceClient.SelectAllAreaCodes();
            cities = serviceClient.SelectAllCities();
            customersDB.SelectCustomerByNum(num);
            AddRange(customersDB.customers);
            foreach (Customer m in this)
                SetObjects(m);
        }

        public Customer GetCustomerByNum(int num)
        {
            SelectCustomerByNum (num);
            Customer customer;
            if (customersDB.customers == null || customersDB.customers.Count == 0)
                return null;
            else
                customer = customersDB.customers.First();
            SetObjects(customer);
            return customer;
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(Customer entity)
        {
            Customer customer;
            return Exist(entity, out customer);
        }

        protected override bool Exist(Customer entity, out Customer existEntity)
        {
            existEntity = Find(item => item.Name == entity.Name);     // קיים ברשימה Customer הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // Customer כתוב/י כאן כיצד למיין אובייקטים מסוג 
            //
            // : דוגמא למיון עפ"י שדה אחד
            // base.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
            //
            // : דוגמא למיון עפ"י שני שדות
            base.Sort((item1, item2) =>
                {
                    int isSame = item1.LastName.CompareTo(item2.LastName);
                    return (isSame != 0) ? isSame : item1.Name.CompareTo(item2.Name);
                }
             );
        }

        public override void SetObjects(Customer entity)
        {
            // אם נדרש - Customer רשום/י כאן קריאה למטודות להשמת אובייקטים מוכלים לאובייקט 
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = customersDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

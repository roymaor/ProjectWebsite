using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using MODEL.Mapper;
using HELPER;

namespace MODEL
{
    public class OrderItems : BaseList<OrderItem>
    {
        private OrderItemsDB orderItemsDB;
        public CustomerOrders customerOrders;
        public Shoes shoes;

        public OrderItems()
        {
            orderItemsDB = new OrderItemsDB();

            customerOrders = new CustomerOrders();
            customerOrders.SelectAll();

            shoes = new Shoes();
            shoes.SelectAll();
        }

        #region Select methods

        public void SelectAll()
        {
            orderItemsDB.SelectAllOrderItems();
            AddRange(orderItemsDB.orderItems);

            foreach(OrderItem orderItem in this)
                SetObjects(orderItem);
        }

        public void SelectOrderItemByNum(int num)
        {
            orderItemsDB.SelectOrderItemByNum(num);
            AddRange(orderItemsDB.orderItems);

            foreach(OrderItem orderItem in this)
                SetObjects(orderItem);
        }

        public OrderItem GetOrderItemByNum(int num)
        {
            SelectOrderItemByNum (num);
            if (orderItemsDB.orderItems == null || orderItemsDB.orderItems.Count == 0)
                return null;
            else
                return orderItemsDB.orderItems[0];
        }
                
//                
// Add here select methods as needed                
//

            #endregion Select methods

        #region Override BaseList methods

        protected override bool Exist(OrderItem entity)
        {
            OrderItem orderItem;
            return Exist(entity, out orderItem);
        }

        protected override bool Exist(OrderItem entity, out OrderItem existEntity)
        {
            existEntity = null;     // קיים ברשימה OrderItem הגדר/י תנאי לבדיקה אם אובייקט מהמחלקה 
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
            // OrderItem כתוב/י כאן כיצד למיין אובייקטים מסוג 
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

        public override void SetObjects(OrderItem entity)
        {
            entity.SetCustomerOrder(customerOrders);
            entity.SetSho(shoes);
        }

        public override bool Save()
        {
            isUpdateOK = false;

            if (Find(item => item.EntityStatus != EntityStatus.Unchanged) != null)
            {
                GenereteUpdateLists();
                isUpdateOK = orderItemsDB.Update(InsertList, UpdateList, DeleteList);
                base.Save();
            }

            return isUpdateOK;
        }

        #endregion Override BaseList methods

    }
}

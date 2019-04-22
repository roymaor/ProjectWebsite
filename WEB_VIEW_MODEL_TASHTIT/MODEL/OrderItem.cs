using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using HELPER;

namespace MODEL
{
    public class OrderItem : BaseEntity
    {
        #region DATA MEMBERS

        private int	orderNo;
        private int	shoeNo;
        private int	quantity;
        private decimal	price;
                
#endregion DATA MEMBERS

        #region CONSTRUCTORS


        public OrderItem() { }

        public OrderItem(bool checkValidation) : base(checkValidation) { }

                
#endregion CONSTRUCTORS

        #region PROPERTIES

        public int OrderNo
        {
            get { return orderNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "orderNo");       // לעברית orderNo שנה/י את
                //    else
                //        orderNo = value;
                //}
                //else
                    orderNo = value;
            }
        }

        public int ShoeNo
        {
            get { return shoeNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "shoeNo");       // לעברית shoeNo שנה/י את
                //    else
                //        shoeNo = value;
                //}
                //else
                    shoeNo = value;
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "quantity");       // לעברית quantity שנה/י את
                //    else
                //        quantity = value;
                //}
                //else
                    quantity = value;
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "price");       // לעברית price שנה/י את
                //    else
                //        price = value;
                //}
                //else
                    price = value;
            }
        }
                
#endregion PROPERTIES

        #region RELATION PROPERTIES


        public CustomerOrder CustomerOrder { get; set; }

        public Sho Sho { get; set; }

                
#endregion RELATION PROPERTIES

        #region METHODS

        #region SET RELATION METHODS


        public void SetCustomerOrder (CustomerOrders customerOrders)
        {
            CustomerOrder = (customerOrders == null || customerOrders.Count == 0) ? null : customerOrders.Find(item => item.Num == OrderNo);
        }

        public void SetSho (Shoes shoes)
        {
            Sho = (shoes == null || shoes.Count == 0) ? null : shoes.Find(item => item.Num == ShoeNo);
        }

        #endregion SET RELATION METHODS

        #region CALCULATED FIELDS

        //
        // הוסף/י כאן שדות מחושבים
        // שדה מחושב = צירוף/חישוב של שני שדות או יותר
        //
        // :לדוגמא
        // public string FullName
        // {
        //      get { return Family + " " + Name; } 
        // }
        //
        //     :דוגמא נוספת
        // public double Total
        // {
        //      get { return Price * Quantity; }
        // }
        //

        public string ShoName
        {
            get { return Sho.Name; }
        }

        #endregion CALCULATED FIELDS


        public override bool Equals (Object obj)
        {
            bool isEqual;

            if(this == obj)
            isEqual = true;
            else
            if (obj == null || !(obj is OrderItem))
                isEqual = false;
            else
            {
                OrderItem o = obj as OrderItem;

                isEqual = base.Equals(obj) &&
                            orderNo.Equals(o.orderNo) && 
                shoeNo.Equals(o.shoeNo) && 
                quantity.Equals(o.quantity) && 
                price.Equals(o.price) ;
        }
            return isEqual;
        }
                
#endregion METHODS

    }
}


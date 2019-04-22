using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using HELPER;

namespace MODEL
{
    public class CustomerOrder : BaseEntity
    {
        #region DATA MEMBERS

        private int	customerNo;
        private DateTime	orderDate;
        private bool	isPaied;
                
#endregion DATA MEMBERS

        #region CONSTRUCTORS


        public CustomerOrder() { }

        public CustomerOrder(bool checkValidation) : base(checkValidation) { }

                
#endregion CONSTRUCTORS

        #region PROPERTIES

        public int CustomerNo
        {
            get { return customerNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "customerNo");       // לעברית customerNo שנה/י את
                //    else
                //        customerNo = value;
                //}
                //else
                    customerNo = value;
            }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "orderDate");       // לעברית orderDate שנה/י את
                //    else
                //        orderDate = value;
                //}
                //else
                    orderDate = value;
            }
        }

        public bool IsPaied
        {
            get { return isPaied; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "isPaied");       // לעברית isPaied שנה/י את
                //    else
                //        isPaied = value;
                //}
                //else
                    isPaied = value;
            }
        }
                
#endregion PROPERTIES

        #region RELATION PROPERTIES


        public Customer Customer { get; set; }

                
#endregion RELATION PROPERTIES

        #region METHODS

        #region SET RELATION METHODS


        public void SetCustomer (Customers customers)
        {
            Customer = (customers == null || customers.Count == 0) ? null : customers.Find(item => item.Num == CustomerNo);
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

        public string CustomerName
        {
            get { return Customer.Name; }
        }

        #endregion CALCULATED FIELDS


        public override bool Equals (Object obj)
        {
            bool isEqual;

            if(this == obj)
            isEqual = true;
            else
            if (obj == null || !(obj is CustomerOrder))
                isEqual = false;
            else
            {
                CustomerOrder o = obj as CustomerOrder;

                isEqual = base.Equals(obj) &&
                            customerNo.Equals(o.customerNo) && 
                orderDate.Equals(o.orderDate) && 
                isPaied.Equals(o.isPaied) ;
        }
            return isEqual;
        }
                
#endregion METHODS

    }
}


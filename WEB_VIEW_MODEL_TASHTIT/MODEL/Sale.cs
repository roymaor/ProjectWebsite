using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using HELPER;

namespace MODEL
{
    public class Sale : BaseEntity
    {
        #region DATA MEMBERS

        private int	shoeNo;
        private DateTime	startDate;
        private DateTime	endDate;
        private decimal	price;
                
#endregion DATA MEMBERS

        #region CONSTRUCTORS


        public Sale() { }

        public Sale(bool checkValidation) : base(checkValidation) { }

                
#endregion CONSTRUCTORS

        #region PROPERTIES

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

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "startDate");       // לעברית startDate שנה/י את
                //    else
                //        startDate = value;
                //}
                //else
                    startDate = value;
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "endDate");       // לעברית endDate שנה/י את
                //    else
                //        endDate = value;
                //}
                //else
                    endDate = value;
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


        public Sho Sho { get; set; }

                
#endregion RELATION PROPERTIES

        #region METHODS

        #region SET RELATION METHODS


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
            if (obj == null || !(obj is Sale))
                isEqual = false;
            else
            {
                Sale o = obj as Sale;

                isEqual = base.Equals(obj) &&
                            shoeNo.Equals(o.shoeNo) && 
                startDate.Equals(o.startDate) && 
                endDate.Equals(o.endDate) && 
                price.Equals(o.price) ;
        }
            return isEqual;
        }
                
#endregion METHODS

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using HELPER;

namespace MODEL
{
    public class Sho : BaseEntity
    {
        #region DATA MEMBERS

        private string	name;
        private int	brandNo;
        private int	categoryNo;
        private int	colorNo;
        private int	sizeNo;
        private int	quantity;
        private decimal	price;
                
#endregion DATA MEMBERS

        #region CONSTRUCTORS


        public Sho() { }

        public Sho(bool checkValidation) : base(checkValidation) { }

                
#endregion CONSTRUCTORS

        #region PROPERTIES

        public string Name
        {
            get { return name; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "name");       // לעברית name שנה/י את
                //    else
                //        name = value;
                //}
                //else
                    name = value;
            }
        }

        public int BrandNo
        {
            get { return brandNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "brandNo");       // לעברית brandNo שנה/י את
                //    else
                //        brandNo = value;
                //}
                //else
                    brandNo = value;
            }
        }

        public int CategoryNo
        {
            get { return categoryNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "categoryNo");       // לעברית categoryNo שנה/י את
                //    else
                //        categoryNo = value;
                //}
                //else
                    categoryNo = value;
            }
        }

        public int ColorNo
        {
            get { return colorNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "colorNo");       // לעברית colorNo שנה/י את
                //    else
                //        colorNo = value;
                //}
                //else
                    colorNo = value;
            }
        }

        public int SizeNo
        {
            get { return sizeNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "sizeNo");       // לעברית sizeNo שנה/י את
                //    else
                //        sizeNo = value;
                //}
                //else
                    sizeNo = value;
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


                
#endregion RELATION PROPERTIES

        #region METHODS

        #region SET RELATION METHODS


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

        #endregion CALCULATED FIELDS


        public override bool Equals (Object obj)
        {
            bool isEqual;

            if(this == obj)
            isEqual = true;
            else
            if (obj == null || !(obj is Sho))
                isEqual = false;
            else
            {
                Sho o = obj as Sho;

                isEqual = base.Equals(obj) &&
                            name.Equals(o.name) && 
                brandNo.Equals(o.brandNo) && 
                categoryNo.Equals(o.categoryNo) && 
                colorNo.Equals(o.colorNo) && 
                sizeNo.Equals(o.sizeNo) && 
                quantity.Equals(o.quantity) && 
                price.Equals(o.price) ;
        }
            return isEqual;
        }
                
#endregion METHODS

    }
}


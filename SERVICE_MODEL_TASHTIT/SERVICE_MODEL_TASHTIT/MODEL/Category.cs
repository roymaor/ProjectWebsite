using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Runtime.Serialization;

using HELPER;

namespace MODEL
{
    [DataContract]
    [KnownType (typeof(BaseEntity))]
    public class Category : BaseEntity
    {
        #region DATA MEMBERS

        private string	name;
                
#endregion DATA MEMBERS

        #region CONSTRUCTORS


        public Category() { }

        public Category(bool checkValidation) : base(checkValidation) { }

                
#endregion CONSTRUCTORS

        #region PROPERTIES

        [DataMember]
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
        // [DataMember]
        // public string FullName
        // {
        //      set { family = value; name = ""; }
        //      get { return Family + " " + Name; } 
        // }
        //
        //     :דוגמא נוספת
        // [DataMember]
        // public double Total
        // {
        //      set { price = value; }
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
            if (obj == null || !(obj is Category))
                isEqual = false;
            else
            {
                Category o = obj as Category;

                isEqual = base.Equals(obj) &&
                            name.Equals(o.name) ;
        }
            return isEqual;
        }
                
#endregion METHODS

    }
}


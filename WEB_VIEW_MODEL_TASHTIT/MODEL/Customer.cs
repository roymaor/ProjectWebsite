using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using HELPER;
using MODEL.MyService;
using MODEL;

namespace MODEL
{
    public class Customer : BaseEntity
    {
        #region DATA MEMBERS

        private string	name;
        private string	lastName;
        private string	id;
        private int	areaCodeNo;
        private string	phone;
        private string	address;
        private int	cityNo;
        private string	postal_code;
        private DateTime	birthDate;
        private string	eMail;
        private string	psw;
        private int	roleNo;
                
#endregion DATA MEMBERS

        #region CONSTRUCTORS


        public Customer() { }

        public Customer(bool checkValidation) : base(checkValidation) { }

                
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

        public string LastName
        {
            get { return lastName; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "lastName");       // לעברית lastName שנה/י את
                //    else
                //        lastName = value;
                //}
                //else
                    lastName = value;
            }
        }

        public string Id
        {
            get { return id; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "id");       // לעברית id שנה/י את
                //    else
                //        id = value;
                //}
                //else
                    id = value;
            }
        }

        public int AreaCodeNo
        {
            get { return areaCodeNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "areaCodeNo");       // לעברית areaCodeNo שנה/י את
                //    else
                //        areaCodeNo = value;
                //}
                //else
                    areaCodeNo = value;
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "phone");       // לעברית phone שנה/י את
                //    else
                //        phone = value;
                //}
                //else
                    phone = value;
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "address");       // לעברית address שנה/י את
                //    else
                //        address = value;
                //}
                //else
                    address = value;
            }
        }

        public int CityNo
        {
            get { return cityNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "cityNo");       // לעברית cityNo שנה/י את
                //    else
                //        cityNo = value;
                //}
                //else
                    cityNo = value;
            }
        }

        public string Postal_code
        {
            get { return postal_code; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "postal_code");       // לעברית postal_code שנה/י את
                //    else
                //        postal_code = value;
                //}
                //else
                    postal_code = value;
            }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "birthDate");       // לעברית birthDate שנה/י את
                //    else
                //        birthDate = value;
                //}
                //else
                    birthDate = value;
            }
        }

        public string EMail
        {
            get { return eMail; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "eMail");       // לעברית eMail שנה/י את
                //    else
                //        eMail = value;
                //}
                //else
                    eMail = value;
            }
        }

        public string Psw
        {
            get { return psw; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "psw");       // לעברית psw שנה/י את
                //    else
                //        psw = value;
                //}
                //else
                    psw = value;
            }
        }

        public int RoleNo
        {
            get { return roleNo; }
            set
            {
                // בטל/י את ההערות ע"מ לאפשר את בדיקות התקינות 
                //if (checkValidation)
                //{
                //    status = ValidateEntry.XXXXX(value, true);      //   (CheckXXX - לפעולה המתאימה (בד"כ XXXXX שנה/י את
                //    if (status != ErrorStatus.NONE)
                //        throw new ValidationException(status, "roleNo");       // לעברית roleNo שנה/י את
                //    else
                //        roleNo = value;
                //}
                //else
                    roleNo = value;
            }
        }

        #endregion PROPERTIES

        #region RELATION PROPERTIES

        public City City { get; set; }
        public AreaCode AreaCode { get; set; }
        public Role Role { get; set; }

        #endregion RELATION PROPERTIES

        #region METHODS

        #region SET RELATION METHODS

        public void SetCity(Cities cities)
        {
            City = (cities != null && cities.Count > 0) ? cities.Find(item => item.Num == cityNo) : null;
        }

        public void SetAreaCode(AreaCodes areaCodes)
        {
            AreaCode = (areaCodes != null && areaCodes.Count > 0) ?  areaCodes.Find(item => item.Num == AreaCodeNo) : null;
        }

        public void SetRole(Roles roles)
        {
            Role = (roles != null && roles.Count > 0) ? roles.Find(item => item.Num == roleNo) : null;
        }


        #endregion SET RELATION METHODS

        #region CALCULATED FIELDS


        public string CityName
        {
            set { City.Name = value; }
            get { return (City != null) ? City.Name : "שגיאה"; }
        }
        public string AreaCodeName
        {
            set { AreaCode.Name = value; }
            get { return (AreaCode != null) ? AreaCode.Name : "שגיאה"; }
        }
        public string RoleName
        {
            set { Role.Name = value; }
            get { return (Role != null) ? Role.Name : "שגיאה"; }
        }

        //
        // הוסף/י כאן שדות מחושבים
        // שדה מחושב = צירוף/חישוב של שני שדות או יותר
        //
        // :לדוגמא
        public string FullName
        {
            get { return LastName + " " + Name; }
        }
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
            if (obj == null || !(obj is Customer))
                isEqual = false;
            else
            {
                Customer o = obj as Customer;

                isEqual = base.Equals(obj) &&
                            name.Equals(o.name) && 
                lastName.Equals(o.lastName) && 
                id.Equals(o.id) && 
                areaCodeNo.Equals(o.areaCodeNo) && 
                phone.Equals(o.phone) && 
                address.Equals(o.address) && 
                cityNo.Equals(o.cityNo) && 
                postal_code.Equals(o.postal_code) && 
                birthDate.Equals(o.birthDate) && 
                eMail.Equals(o.eMail) && 
                psw.Equals(o.psw) && 
                roleNo.Equals(o.roleNo) ;
        }
            return isEqual;
        }
                
#endregion METHODS

    }
}


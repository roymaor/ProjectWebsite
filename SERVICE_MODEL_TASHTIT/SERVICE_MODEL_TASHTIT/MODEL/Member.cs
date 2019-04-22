using System;
//using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MODEL
{
    [DataContract]
    [KnownType(typeof(BaseEntity))]
    public class Member : BaseEntity
    {
        #region DATA MEMBERS

        private string family;
        private string name;
        private int cityNo;
        private string email;
        private string psw;
        private string personid;


        #endregion DATA MEMBERS

        #region CONSTRUCTORS


        public Member() { }

        public Member(bool checkValidation) : base(checkValidation) { }


        #endregion CONSTRUCTORS

        #region PROPERTIES

        [DataMember]


        public string Family
        {
            get { return family; }
            set
            {

                family = value;
            }
        }

        [DataMember]

        public string Name
        {
            get { return name; }
            set
            {

                name = value;
            }
        }
        [DataMember]

        public int CityNo
        {
            get { return cityNo; }
            set
            {

                cityNo = value;
            }
        }
        [DataMember]

        public string Psw
        {
            get { return psw; }
            set
            {

                psw = value;
            }
        }
        [DataMember]

        public string Email
        {
            get { return email; }
            set
            {

                email = value;
            }
        }

        [DataMember]

        public string Personid
        {
            get => personid;
            set => personid = value;
        }

        public City City { get; set; }


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


        public override bool Equals(Object obj)
        {
            bool isEqual;

            if (this == obj)
                isEqual = true;
            else
            if (obj == null || !(obj is Member))
                isEqual = false;
            else
            {
                City o = obj as City;

                isEqual = base.Equals(obj) &&
                            Num.Equals(o.Num);
            }
            return isEqual;
        }

        #endregion METHODS

    }
}

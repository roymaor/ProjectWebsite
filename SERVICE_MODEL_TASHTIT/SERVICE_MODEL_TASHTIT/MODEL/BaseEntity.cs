using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using HELPER;


namespace MODEL
{
    ///// <summary>
    ///// הגדרת תכונה לסימון מאפיינים שיש להם שדה מקביל בטבלה במסד הנתונים
    ///// </summary>
    //public class Column : Attribute
    //{
    //    /// <summary>
    //    /// אפשרות להגדרת שם השדה במסד הנתונים במידה והוא שונה משם המאפיין
    //    /// </summary>
    //    public string Name { get; set; }
    //}

    /// <summary>
    /// הגדרת מצב ישות
    /// Added     -                       ישות שנוספה לרשימה
    /// Modified  -                       ישות ברשימה ששונתה
    /// Deleted   -              ישות ברשימה שמיועדת למחיקה
    /// Unchanged - (ישות ברשימה שלא שונתה (מצב ברירת מחדל
    /// </summary>
    [DataContract][Flags]
    public enum EntityStatus
    {
        [EnumMember]
        Added,
        [EnumMember]
        Modified,
        [EnumMember]
        Deleted,
        [EnumMember]
        Unchanged
    };

    /// <summary>
    /// הגדרת ישות בסיסית
    /// </summary>
    [DataContract]
    public abstract class BaseEntity
    {
        #region DATA MEMBERS
        
        private   int         num;
        [DataMember]
        protected ErrorStatus status;
        [DataMember]
        protected bool        checkValidation;

        #endregion DATA MEMBERS

        #region PROPERTIES

        [DataMember]
        public int          Num          { get { return num; } set { num = value; } }
        [DataMember]
        public EntityStatus EntityStatus { get; set; }

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public BaseEntity()
        {
            EntityStatus    = EntityStatus.Unchanged;
            checkValidation = true;
        }

        public BaseEntity(bool checkValidation) : this()
        {
            this.checkValidation = checkValidation;
        }

        #endregion CONSTRUCTORS

        #region METHODS

        public override bool Equals(object obj)
        {
            bool isEqual;

            if (this == obj)
                isEqual = true;
            else
                if (obj == null || !(obj is BaseEntity))
                    isEqual = false;
                else
                {
                    BaseEntity b = obj as BaseEntity;

                    isEqual = Num          == b.Num &&
                              EntityStatus == b.EntityStatus;
                }

            return isEqual;
        }

        #endregion METHODS
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace DAL
{
    // http://www.codeproject.com/Articles/180799/SQL-Script-Generator-Using-Generics-and-Reflection

    /// <summary>
    /// SQL מחלקה המחוללת פקודות
    /// Entities לפעולות מול מסד הנתונים עפ"י
    /// entity-מגבלה: השדות במסד הנתונים ומאפייני ה
    /// חייבים להיבות זהים
    /// 
    /// הערה:
    /// ע"י הגדרת תכונה למאפיין
    /// entity-ניתן להגדיר שם שדה במסד הנתונים שונה משם המאפיין ב
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlGenerator<T>
    {
        #region DATA MEMBERS

        private string tableName;           //     שם הטבלה
        private string keyField;            // שם שדה המפתח

        private StringBuilder  query;       //                השאילתא הנוצרת
        private Type           type;        //  עבורו נוצרת השאילתא entity-ה
        private PropertyInfo[] propInfo;    //    entity-מערך המאפיינים של ה
        private List<string> fieldList;

        #endregion DATA MEMBERS

        #region CONSTRUCTORS

        public SqlGenerator()
        {
            type     = typeof(T);
            propInfo = type.GetProperties();
        }

        public SqlGenerator(string tn, string kf, List<string> fieldList) 
            : this() 
        { 
            tableName = tn;
            keyField  = kf;
            this.fieldList = fieldList;
        }

        #endregion CONSTRUCTORS

        #region SQL GENERATE METHODS

        /// <summary>
        /// Select יוצר פקודת
        /// שולף את כל השדות וכל הרשומות ללא תנאים וללא מיון
        /// </summary>
        /// <returns>Select פקודת</returns>
        public string GenerateSelectCommand()
        {
            query = new StringBuilder();

            // entity-מעבר על כל המאפיינים של ה
            //foreach (PropertyInfo pi in propInfo)
            foreach (string field in fieldList)
            {
                // צרף לפקודה מאפיינים המוגדרים כשדות בטבלה
                /////////////////////////////////////////////////if (Attribute.IsDefined(pi, typeof(POCO.Column)))
                    if (query.ToString() == string.Empty)
                        query.AppendFormat("SELECT [{0}]", /*pi.Name*/ field);
                    else
                        query.AppendFormat(", [{0}]", /*pi.Name*/ field);
            }

            if (query.ToString() != string.Empty)
                query.AppendFormat(" FROM {0}", tableName);

            return query.ToString();
        }

        /// <summary>
        /// Insert יוצר פקודת
        /// מכניס רשומה חדשה עם ערכים לכל השדות
        /// </summary>
        /// <returns>Insert פקודת</returns>
        public string GenerateInsertCommand()
        {
            // values-משתנה לצבירת ה
            StringBuilder param   = new StringBuilder();

            query = new StringBuilder();

            // entity-מעבר על כל המאפיינים של ה
            //foreach (PropertyInfo pi in propInfo)
            foreach (string field in fieldList)
            {
                // צרף לפקודה מאפיינים המוגדרים כשדות בטבלה אל לא את שדה המפתח
                ///////////////////////////////////////////////if (pi.Name.ToUpper() != keyField.ToUpper() && Attribute.IsDefined(pi, typeof(POCO.Column)))
                if (field.ToUpper() != keyField.ToUpper())
                {
                    if (query.ToString() == string.Empty)
                        query.AppendFormat("INSERT INTO {0} ( [{1}] ", (tableName == null) ? "Table_Name_Missing" : tableName, /*pi.Name*/ field);
                    else
                    {
                        query.AppendFormat(" , [{0}]", /*pi.Name*/ field);
                        param.Append(", ");
                    }

                    // values-אוסף ה
                    param.Append("@" + /*pi.Name*/ field);
                }
            }

            if (query.ToString() != string.Empty)
                query.Append(" ) VALUES (" + param.ToString() + ")");

            return query.ToString();
        }

        /// <summary>
        /// Update יוצר פקודת
        /// מעדכן רשומה עם ערכים לכל השדות
        /// </summary>
        /// <returns>Update פקודת</returns>
        public string GenerateUpdateCommand()
        {
            query = new StringBuilder();

            // entity-מעבר על כל המאפיינים של ה
            //foreach (PropertyInfo pi in propInfo)
            foreach (string field in fieldList)
            {
                // צרף לפקודה מאפיינים המוגדרים כשדות בטבלה אל לא את שדה המפתח
                ///////////////////////////////////////////////////////////if (pi.Name.ToUpper() != keyField.ToUpper() && Attribute.IsDefined(pi, typeof(POCO.Column)))
                if (field.ToUpper() != keyField.ToUpper())
                {
                    if (query.ToString() == string.Empty)
                        query.AppendFormat("UPDATE {0} Set [{1}] = {2}", tableName, /*pi.Name*/ field, "@" + /*pi.Name*/ field);
                    else
                        query.AppendFormat(", [{0}] = {1}", /*pi.Name*/ field, "@" + /*pi.Name*/ field);
                }
            }

            if (query.ToString() != string.Empty)
            {
                // טיפול בשדה המפתח
                PropertyInfo piKeyField = Array.Find(propInfo, item => item.Name.ToUpper() == keyField.ToUpper());
                query.AppendFormat(" WHERE [{0}] = {1}", piKeyField.Name, "@" + piKeyField.Name);
            }

            return query.ToString();
        }

        /// <summary>
        /// Delete יוצר פקודת
        /// </summary>
        /// <returns>Delete פקודת</returns>
        public string GenerateDeleteCommand()
        {
            query = new StringBuilder();
            
            // שליפת שדה המפתח ויצירת פקודת המחיקה
            PropertyInfo piKeyField = Array.Find(propInfo, item => item.Name.ToUpper() == keyField.ToUpper());
            query.AppendFormat("DELETE FROM {0} WHERE [{1}] = {2}", tableName, piKeyField.Name, "@" + piKeyField.Name);

            return query.ToString();
        }

        #endregion SQL GENERATE METHODS
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Runtime.Serialization;

using HELPER;

namespace DAL
{
    [DataContract]
    public abstract class BaseDB<T> : DataBase
    {
        #region DATA MEMBERS

        protected OleDbCommand    command;
        protected OleDbDataReader reader;
        protected string          query;            // השאילתא לשליפת הנתונים
        protected PropertyInfo[]  propInfo;         //     אוסף מאפייני הטיפוס
        protected Type            type;             //          טיפוס האובייקט

        #endregion DATA MEMBERS
 
        #region PROPERTIES

        // השאילתא לשליפת הנתונים
        protected string Query     { 
            get { return query; }
            set
            {
                if (value != "")
                {
                    query     = value;
                    TableName = Global.ParseTableNameFromSQL(query);        // שליפת שם הטבלה מהשאילתא
                }
            }
                                   }

        protected int    AffectedRows { get; set; }                         // מספר השורות המושפעות מהשאילתא
        protected string TableName    { get; set; }                         //                         שם הטבלה
        protected string KeyField     { get; set; }                         //                     שם שדה המפתח

        #endregion PROPERTIES

        #region CONSTRUCTORS

        // Default Constructor
        protected BaseDB()
        {
            command             = new OleDbCommand();
            command.Connection  = connection;
            command.CommandType = CommandType.Text;

            KeyField            = "Num";
        }

        protected BaseDB(string query) : this()
        {
            Query = query;
        }

        protected BaseDB(string query, string tableName)
            : this(query)
        {
            TableName = tableName;
        }

        #endregion CONSTRUCTORS

        #region METHODS


        // משתנים הנדרשים למיפוי
        protected static List<FieldInfo> itemProperties;
        protected static List<FieldInfo> typeProperties;


        //http://www.codeproject.com/Articles/57062/Load-Any-Object-From-Most-Any-Database
        /// <summary>
        /// שליפת נתונים מהטבלה
        /// </summary>
        /// <typeparam name="T">הטיפוס לקליטת הנתונים ממסד הנתונים</typeparam>
        /// <returns>רשימת איברים מהטיפוס</returns>
        protected IList<T> SelectList<T>() where T : new()
        {
            IList<T> retval = new List<T>();

            try
            {
                command.CommandText = Query;
                connection.Open();
                reader = command.ExecuteReader();

                // האם יש שורות במסד הנתונים
                if (reader.HasRows)
                {
                    //get the type of the object, without having to create one
                    //Type type = typeof(T);

                    // Mapping Block
                    // מיפוי השדות במסד הנתונים למאפיינים של הישות
                    //////List<FieldInfo> itemProperties = new List<FieldInfo>();
                    //////List<FieldInfo> typeProperties = GetAllFields(typeof(T), false).ToList();
                    if (itemProperties == null && typeProperties == null)
                    {
                        itemProperties = new List<FieldInfo>();
                        typeProperties = GetAllFields(typeof(T), false).ToList();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            itemProperties.Add(typeProperties.Find(item => item.Name.ToLower() == reader.GetName(i).ToLower()));
                        }
                    }

                    //data block - שליפת הנתונים ממסד הנתונים
                    object[] oo = new object[itemProperties.Count];
                    while (reader.Read())
                    {
                        reader.GetValues(oo);

                        //could be threaded block                
                        T item = new T();
                        int fieldIndex = -1;

                        // השמת ערכים ממסד הנתונים לפאפייני הישות
                        foreach (var pi in itemProperties)
                        {
                            fieldIndex++;

                            if (pi != null)
                            {
                                object o = oo[fieldIndex];

                                if (DBNull.Value.Equals(o))
                                {
                                    o = null;
                                }

                                try
                                {
                                    pi.SetValue(item, o);
                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        if (pi.FieldType == typeof(System.Decimal) && o.GetType() == typeof(System.Single))
                                            pi.SetValue(item, Convert.ToDecimal(o));
                                    }
                                    catch (Exception e1)
                                    {
                                        
                                        throw;
                                    }
                                }
                            }
                        }

                        retval.Add(item);
                        //end of could be threaded block
                    }
                }
            }
            catch (OleDbException e)
            {
                retval = null;
            }
            catch(Exception e)
            {
                retval = null;
            }
            finally
            {
                connection.Close();
            }

            return retval;
        }

        /// <summary>
        /// קבלת המאפיינים מהישות
        /// </summary>
        /// <param name="t">טיפוס הישות</param>
        /// <param name="allProperties">private האם לשלוף את כל המאפיינים או רק</param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetAllFields(Type t, bool allProperties)
        {
            BindingFlags flags; 

            if (t == null)
                return Enumerable.Empty<FieldInfo>();

            if (allProperties)
                flags = BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Static | BindingFlags.Instance |
                        BindingFlags.DeclaredOnly;
            else
                flags = BindingFlags.NonPublic | BindingFlags.Instance;

            // קריאה רקורסיבית לקבלת מאפייני הישות כולל מאפייני האב בירושה
            return GetAllFields(t.BaseType, allProperties).Concat(t.GetFields(flags));
        }



        private static List<string> fieldList;
        private static string insertCommand;
        private static string updateCommand;
        private static string deleteCommand;

        
        /// <summary>
        /// עדכון הנתונים במסד הנתונים
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונים</typeparam>
        /// <param name="list">רשימת הנתונים</param>
        //////public void Update<T>(List<T> list)
        public bool Update<T>(List<T> insertList, List<T> updateList, List<T> deleteList)
        {
            List<T> iud_List;         // (רשימת נתונים לעדכון עפ"י סוג (הוספה, שינוי, ביטול
            string sqlCommand;

            connection.Open();

            if (fieldList == null)
            {
                // קריאת מבנה הטבלה
                sqlCommand = "SELECT * FROM " + TableName + " WHERE 1=0";
                command.CommandText = sqlCommand;
                reader = command.ExecuteReader();

                // בניית רשימת שדות במסד הנתונים
                fieldList = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    fieldList.Add(reader.GetName(i));
                reader.Close();
            }

            // SQL-יצירת אובייקט המחולל את פקודות ה
            SqlGenerator<T> sqlGenerator = new SqlGenerator<T>(TableName, KeyField, fieldList);

            type     = typeof(T);                   //  טיפוס הנתונים

            #region INSERT RECORDS

            // יצירת רשימה של כל הרשומות החדשות
            ///////updateList = list.Where(item => (EntityStatus)item.GetType().GetProperty("EntityStatus", getPropertyFlags).GetValue(item, null) == EntityStatus.Added).ToList();
            iud_List = insertList;

            if (iud_List.Count > 0)
            {
                // SQL-חילול פקודת ה
                if (insertCommand == null)
                    insertCommand = sqlGenerator.GenerateInsertCommand();

                sqlCommand = insertCommand;
                command.CommandText = sqlCommand;

                // הוספת הרשומות למסד הנתונים
                foreach (T entity in iud_List)
                    InsertRecord<T>(entity, sqlGenerator);
            }

            #endregion INSERT RECORDS


            #region UPDATES RECORDS

            // יצירת רשימה של כל הרשומות שהשתנו
            //////iud_List = list.Where(item => (EntityStatus)item.GetType().GetProperty("EntityStatus", getPropertyFlags).GetValue(item, null) == EntityStatus.Modified).ToList();
            iud_List = updateList;

            if (iud_List.Count > 0)
            {
                // SQL-חילול פקודת ה
                if (updateCommand == null)
                    updateCommand = sqlGenerator.GenerateUpdateCommand();

                sqlCommand = updateCommand;
                command.CommandText = sqlCommand;

                // עדכון הרשומות במסד הנתונים
                foreach (T entity in iud_List)
                    UpdateRecord<T>(entity);
            }

            #endregion UPDATE RECORDS


            #region DELETE RECORDS

            // יצירת רשימה של הרשומות למחיקה
            //////iud_List = list.Where(item => (EntityStatus)item.GetType().GetProperty("EntityStatus", getPropertyFlags).GetValue(item, null) == EntityStatus.Deleted).ToList();
            iud_List = deleteList;

            if (iud_List.Count > 0)
            {
                // SQL-חילול פקודת ה
                if (deleteCommand == null)
                    deleteCommand = sqlGenerator.GenerateDeleteCommand();

                sqlCommand = deleteCommand;
                command.CommandText = sqlCommand;

                // מחיקת הרשומות ממסד הנתונים
                foreach (T entity in iud_List)
                    DeleteRecord<T>(entity);
            }

            #endregion DELETE RECORDS

            connection.Close();

            return AffectedRows != 0;
        }

        /// <summary>
        /// הוספת רשומה למסד הנתונים
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונים</typeparam>
        /// <param name="entity">הרשומה להוספה</param>
        protected void InsertRecord<T>(T entity, SqlGenerator<T> sqlGenerator)
        {
            try
            {
                SetParameters<T>(entity);                       // יצירת הפרמטרים וקביעת הערכים להוספה
                AffectedRows = command.ExecuteNonQuery();       //             הוספת רשומה למסד הנתונים
            }
            catch (Exception e)
            {
                string s = GetSqlInsertCommand(entity);
                command.CommandText = s;
                AffectedRows = command.ExecuteNonQuery();
            }

            // Set "Num" value after insert new row
            // קביעת ערכו של שדה המפתח לאחר הוספת הרשומה למסד הנתונים
            string insertSql = command.CommandText;
            command.CommandText = "SELECT @@Identity";
            int Num = (int)command.ExecuteScalar();
            entity.GetType().GetProperty(KeyField, getPropertyFlags).SetValue(entity, Num, null);
            command.CommandText = insertSql;
        }

        /// <summary>
        /// עדכון רשומה במסד הנתונים
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונים</typeparam>
        /// <param name="entity">הרשומה לעדכון</param>
        protected void UpdateRecord<T>(T entity)
        {
            SetParameters<T>(entity);                       // יצירת הפרמטרים וקביעת הערכים לעדכון
            AffectedRows = command.ExecuteNonQuery();       //             עדכון רשומה למסד הנתונים
        }

        /// <summary>
        /// מחיקת רשומה ממסד הנתונים
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונים</typeparam>
        /// <param name="entity">הרשומה למחיקה</param>
        protected void DeleteRecord<T>(T entity)
        {
            SetParameters<T>(entity);                       // יצירת הפרמטרים וקביעת הערכים לעדכון
            AffectedRows = command.ExecuteNonQuery();       //             עדכון רשומה למסד הנתונים
        }

        /// <summary>
        /// יצירת רשימת הפרמטרים וקביעת ערכיהם
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונים</typeparam>
        /// <param name="entity">הרשומה לטיפול</param>
        private void SetParameters<T>(T entity)
        {
            command.Parameters.Clear();                                 // ניקוי רשימת הפרמטרים מהפעולה הקודמת

            string[] tokens = command.CommandText.Split();              // פיצול הפקודה למילים
                                                                        // כל מילה המוקפת ב-"[" ו-"]" היא שם שדה שיהפוך לפרמטר
            string fieldName;

            foreach (string token in tokens)
                if (token.StartsWith("[") && (token.EndsWith("]") || token.EndsWith("],")))
                {
                    // קביעת שם הפרמטר
                    fieldName = token.Replace("[", "").Replace("]", "");
                    // יצירת פרמטר חדש והוספה לרשימת הפרמטרים של הפקודה
                    command.Parameters.Add(new OleDbParameter("@" + fieldName, entity.GetType().GetProperty(fieldName, getPropertyFlags).GetValue(entity, null)));
                }
        }

        public string GetSqlInsertCommand<T>(T entity)
        {
            string[] tokens = command.CommandText.Split(',');              // פיצול הפקודה למילים
            string sql = command.CommandText;                                           // כל מילה המוקפת ב-"[" ו-"]" היא שם שדה שיהפוך לפרמטר
            string fieldName;
            string propertyName;
            object value;

            foreach(string token in tokens)
                if (token.LastIndexOf('@') > 0)
                {
                    fieldName = token.Substring(token.LastIndexOf('@'));
                    char c = Convert.ToChar(fieldName.Substring(fieldName.Length - 1));
                    if (!Char.IsLetterOrDigit(Convert.ToChar(fieldName.Substring(fieldName.Length - 1))))
                        fieldName = fieldName.Substring(0, fieldName.Length - 1);
                    propertyName = fieldName.Substring(1);
                    propertyName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);

                    if (propertyName.ToUpper().StartsWith("QUESTION"))
                    {
                    }

                    //sql.Replace(fieldName, entity.GetType().GetProperty(fieldName.Substring(1), getPropertyFlags).GetValue(entity, null));
                    value = entity.GetType().GetProperty(propertyName).GetValue(entity, null);

                    switch (value.GetType().Name)
                    {
                        case "String":
                            sql = sql.Replace(fieldName, "'" + value.ToString() + "'");
                            break;

                        case "DateTime":
                            sql = sql.Replace(fieldName, "#" + Convert.ToDateTime(value).ToShortDateString() + "#");
                            break;

                        default:
                            sql = sql.Replace(fieldName, value.ToString());
                            break;
                    }
                }

            return sql;
        }

        /// <summary>
        /// שליפת נתונים ממסד הנתונים
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונם</typeparam>
        public void Select<T>()
        {
        }

        #endregion METHODS
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;

namespace DAL
{
    [DataContract]
    public abstract class DataBase
    {
        #region STATIC MEMBERS
        
        private   static string          path;                  // מיקום התוכנה
        private   static string          dbName;                // שם מסד הנתונים
        private   static string          connectionString;
        protected static OleDbConnection connection;
        protected static BindingFlags    getPropertyFlags;      // אופן איתור המאפיין ללא התייחסות לגודל האותיות

        #endregion STATIC MEMBERS

        // static Constructor
        static DataBase()
        {
            path             = GetPath();
            dbName           = GetDbName();
            connectionString = GetConnectionString();
            connection       = new OleDbConnection(connectionString);

            getPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase;
        }

        #region STATIC METHODS

        /// <summary>
        /// קביעת מיקום מסד הנתונים
        /// </summary>
        /// <returns></returns>
        private static string GetPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\DAL\DB\";
            return path;
        }

        /// <summary>
        /// איתור מסד הנתונים לקביעת שמו
        /// </summary>
        /// <returns></returns>
        private static string GetDbName()
        {
            string[] files;

            if (path != null)
            {
                files = Directory.GetFiles(path, "*.accdb");

                if (files.Length > 0)
                    dbName = Path.GetFileName(files[0]);
                else
                    dbName = "";
            }
            else
                dbName = "";

            return dbName;
        }

        /// <summary>
        /// יצירת מחרוזת התחברות למסד הנתונים
        /// </summary>
        /// <returns>Connection string</returns>
        private static string GetConnectionString()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + dbName + ";Persist Security Info=True";
            return connectionString;
        }

        #endregion STATIC METHODS
    }
}

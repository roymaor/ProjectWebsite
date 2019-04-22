using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace HELPER
{
    public class Global
    {
        /// <summary>
        /// Get the table name involved in the sql query
        /// </summary>
        /// <param name="sql">SQL statemenr</param>
        /// <returns>Table Name</returns>
        public static string ParseTableNameFromSQL(string sql)
        {
            string[] words;
            string tableName = "";
            int index;

            words = sql.ToUpper().Split();

            if (words[0].Equals("SELECT"))
            {
                // find FROM
                index = Array.FindIndex<string>(words, s => s.ToUpper().Equals("FROM"));

                // skip all spaces
                index = Array.FindIndex<string>(words, index + 1, s => !s.Equals(""));

                tableName = words[index];
            }
            else
                if (words[0].Equals("DELETE") || words[0].Equals("INSERT"))
                tableName = words[2];
            else
                tableName = words[1];   // UPDATE

            return tableName;
        }

        /// <summary>
        /// בדיקה האם תאריך
        /// </summary>
        /// <param name="date">מחרוזת התאריך לבדיקה</param>
        /// <returns>נכון/לא נכון</returns>
        public static bool IsDate(object date)
        {
            DateTime tryDate;
            if (DateTime.TryParse(date.ToString(), out tryDate))
                return true;
            else
                return false;
        }

        /// <summary>
        /// בדיקה אם שעה
        /// </summary>
        /// <param name="time">מחרוזת השעה לבדיקה</param>
        /// <returns>נכון/לא נכון</returns>
        public static bool IsTime(object time)
        {
            TimeSpan ts;
            string t;

            if (IsDate(time))
                t = time.ToString().Substring(11);  // יש לחלץ את השעה מהתאריך
            else
                t = time.ToString();                //          התקבלה שעה בלבד  

            if (TimeSpan.TryParse(t, out ts))
                return true;
            else
                return false;
        }

        /// <summary>
        /// מחשב גיל
        /// </summary>
        /// <param name="birthDate">תאריך לידה</param>
        /// <returns>גיל</returns>
        public static double Age(DateTime birthDate)
        {
            double age;

            if (!IsDate(birthDate.ToString()))
                age = -1;
            else
            {
                TimeSpan ts = DateTime.Today - birthDate;
                age = ts.Days / 365.25;
            }

            return age;
        }

        /// <summary>
        /// מחשב גיל
        /// </summary>
        /// <param name="birthDate">תאריך לידה</param>
        /// <param name="separator">מפריד בין שנים לחודשים</param>
        /// <returns>גיל בפורמט שנים וחודשים</returns>
        public static string AgeString(DateTime birthDate, char separator = '|')
        {
            double age = Age(birthDate);
            string[] ages = age.ToString().Split('.');
            if (ages.Length == 1)
            {
                ages = new string[2];
                ages[0] = age.ToString().Split('.')[0];
                ages[1] = "0";
            }

            if ((int)(Convert.ToDouble("0." + ages[1]) * 12) == 0)
                return ages[0];
            else
                return ages[0] + separator + ((int)(Convert.ToDouble("0." + ages[1]) * 12)).ToString();
        }

        /// <summary>
        /// Get the quarter of the year for a given date
        /// </summary>
        /// <param name="date">date to calculate qurter</param>
        /// <returns>Quarter of the year</returns>
        public static int Quarter(DateTime date)
        {
            return (date.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// Get the last date in quarter for a given date
        /// </summary>
        /// <param name="date">Date to calculate end date of quarter</param>
        /// <returns>Last date in quarter</returns>
        public static DateTime QuarterEndDate(DateTime date)
        {
            int quarter = (date.Month - 1) / 3 + 1;
            return new DateTime(date.Year, (quarter * 3) + 1, 1).AddDays(-1);
        }

        /// <summary>
        /// Get the last date in quarter for a given quarter and year
        /// </summary>
        /// <param name="quarter">Quarter to calculate end date of quarter</param>
        /// <param name="year">Year to calculate end date in quarter</param>
        /// <returns>Last date in quarter</returns>
        public static DateTime QuarterEndDate(int quarter, int year)
        {
            return new DateTime(year, (quarter * 3) + 1, 1).AddDays(-1);
        }

        /// <summary>
        /// Get the first date in quarter for a given date
        /// </summary>
        /// <param name="date">Date to calculate first date of quarter</param>
        /// <returns>First date in quarter</returns>
        public static DateTime QuarterStartDate(DateTime date)
        {
            return QuarterEndDate(date).AddDays(1).AddMonths(-3);
        }

        /// <summary>
        /// Get the first date in quarter for a given quarter and year
        /// </summary>
        /// <param name="quarter">Quarter to calculate end date of quarter</param>
        /// <param name="year">Year to calculate end date in quarter</param>
        /// <returns>First date in quarter</returns>
        public static DateTime QuarterStartDate(int quarter, int year)
        {
            return QuarterEndDate(quarter, year).AddDays(1).AddMonths(-3);
        }

        /// <summary>
        /// Get start date and end date of a quarter for a given date
        /// </summary>
        /// <param name="date">Date to calculate first and end dates of quarter</param>
        /// <returns>First and Last date of quater</returns>
        public static DateTime[] QuarterDateRange(DateTime date)
        {
            DateTime[] dates = new DateTime[2];
            dates[0] = QuarterStartDate(date);
            dates[1] = QuarterEndDate(date);
            return dates;
        }

        /// <summary>
        /// Get start date and end date of a quarter for a given quarter and year
        /// </summary>
        /// <param name="quarter">Quarter to calculate</param>
        /// <param name="year">Year to calculate</param>
        /// <returns>First and last dates of quarter</returns>
        public static DateTime[] QuarterDateRange(int quarter, int year)
        {
            DateTime[] dates = new DateTime[2];
            dates[0] = QuarterStartDate(quarter, year);
            dates[1] = QuarterEndDate(quarter, year);
            return dates;
        }

        /// <summary>
        /// בדיקה אם תאריך נמצא בטווח תאריכים
        /// </summary>
        /// <param name="dateToCheck">התאריך לבדיקה</param>
        /// <param name="startDate">תאריך התחלת הטווח</param>
        /// <param name="endDate">תאריך סיום הטווח</param>
        /// <returns>בטווח/איננו בטווח</returns>
        public static bool IsDateInRange(DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            bool isok = startDate.Date <= dateToCheck.Date && dateToCheck.Date <= endDate.Date;
            return startDate <= dateToCheck && dateToCheck <= endDate;
        }

        /// <summary>
        /// בדיקה אם מספר שלם נמצא בטווח מספרים
        /// </summary>
        /// <param name="dateToCheck">המספר לבדיקה</param>
        /// <param name="startDate">מםפר התחלת הטווח</param>
        /// <param name="endDate">מספר סיום הטווח</param>
        /// <returns>בטווח/איננו בטווח</returns>
        public static bool IsNumberInRange(int numToCheck, int minValue, int maxValue)
        {
            return minValue <= numToCheck && numToCheck <= maxValue;
        }

        /// <summary>
        /// בדיקה אם מספר עשרוני נמצא בטווח מספרים
        /// </summary>
        /// <param name="dateToCheck">המספר לבדיקה</param>
        /// <param name="startDate">מםפר התחלת הטווח</param>
        /// <param name="endDate">מספר סיום הטווח</param>
        /// <returns>בטווח/איננו בטווח</returns>
        public static bool IsNumberInRange(double numToCheck, double minValue, double maxValue)
        {
            return minValue <= numToCheck && numToCheck <= maxValue;
        }

        /// <summary>
        /// בדיקה אם מספר דצימאלי נמצא בטווח מספרים
        /// </summary>
        /// <param name="dateToCheck">המספר לבדיקה</param>
        /// <param name="startDate">מםפר התחלת הטווח</param>
        /// <param name="endDate">מספר סיום הטווח</param>
        /// <returns>בטווח/איננו בטווח</returns>
        public static bool IsNumberInRange(decimal numToCheck, decimal minValue, decimal maxValue)
        {
            return minValue <= numToCheck && numToCheck <= maxValue;
        }

        /// <summary>
        /// בודקת ומתקנת כל מילה עברית שלא תתחיל באות סופית
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FixHebrewString(string s)
        {
            int i;
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] oldWords = s.Split(delimiterChars);
            string[] newWords = s.Split(delimiterChars);

            for (i = 0; i < newWords.Length; i++)
                if (newWords[i].StartsWith("ך")) newWords[i] = "כ" + newWords[i].Substring(1);
                else if (newWords[i].StartsWith("ם")) newWords[i] = "מ" + newWords[i].Substring(1);
                else if (newWords[i].StartsWith("ן")) newWords[i] = "נ" + newWords[i].Substring(1);
                else if (newWords[i].StartsWith("ף")) newWords[i] = "פ" + newWords[i].Substring(1);
                else if (newWords[i].StartsWith("ץ")) newWords[i] = "צ" + newWords[i].Substring(1);

            for (i = 0; i < oldWords.Length; i++)
                s = s.Replace(oldWords[i], newWords[i]);

            return s;
        }

        /// <summary>
        /// SetDoubleBuffered
        /// </summary>
        /// <param name="control">The control to double buffered</param>
        public static void SetDoubleBuffered(System.Windows.Forms.Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(System.Windows.Forms.Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        /// <summary>
        /// Check if a string is URL
        /// </summary>
        /// <param name="url">String to check</param>
        /// <returns>TRUE if URL otherwise FASLE</returns>
        public static bool IsUrl(string url)
        {
            return System.Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Calculate Sum of digits of a given number according to Luhn Algorithm (MOD10)
        /// https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="number">The number to calculate it's sum of digits</param>
        /// <returns>Sum of digits</returns>
        private static int SumOfDigits(string number)
        {
            int len = number.Length;
            int multiplier = 1;
            int sumOfDigits = 0;
            int digitSum;

            for (int i = len - 1; i >= 0; i--)
            {
                if (number[i] >= '0' && number[i] <= '9')
                {
                    digitSum = multiplier * (number[i] - '0');
                    sumOfDigits += digitSum / 10 + digitSum % 10;
                    multiplier = 3 - multiplier;
                }
            }

            return sumOfDigits;
        }

        /// <summary>
        /// Check if number matches Luhn Algorithm (Mod10)
        /// https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="number">Tje number to check</param>
        /// <returns>TRUE if MOD10 otherwise FALSE</returns>
        public static bool IsLuhn(string number)
        {
            return SumOfDigits(number) % 10 == 0;
        }

        /// <summary>
        /// Check if number matches Luhn Algorithm (Mod10)
        /// https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="number">Tje number to check</param>
        /// <returns>TRUE if MOD10 otherwise FALSE</returns>
        public static bool IsMod10(string number)
        {
            return IsLuhn(number);
        }

        /// <summary>
        /// Calculate check digit according to Luhn Algorithm (MOD10)
        /// https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="number">The number to calculate check digit</param>
        /// <returns>Check Digit</returns>
        public static char CalculateCheckDigit(string number)
        {
            int sumOfDigits = SumOfDigits(number);
            string s = (sumOfDigits * 9).ToString();

            return Convert.ToChar(s.Substring(s.Length - 1, 1));
        }

        /// <summary>
        /// Access-ממיר תאריך לשאילתות ב
        /// </summary>
        /// <param name="date">תאריך להמרה</param>
        /// <returns>Access תאריך מומר עבור</returns>
        public static string ToAccessDateString(DateTime date)
        {
            return date.Month.ToString() + "/" + date.Day.ToString() + "/" + date.Year.ToString();
        }

    }

    public static class WegMessageBox
    {
        public static void Show(this Page Page, String Message)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }
    }
}

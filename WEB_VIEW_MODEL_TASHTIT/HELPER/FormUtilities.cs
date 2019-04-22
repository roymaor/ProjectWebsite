using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HELPER
{
    public class FormUtilities
    {
        /// <summary>
        /// הכנת ComboBox לתצוגה
        /// </summary>
        /// <param name="cmb">Combo-ה</param>
        /// <param name="list">הרשימה לתצוגה</param>
        /// <param name="displayColumn">העמודה לתצוגה</param>
        /// <param name="valueColumn">עמודת הערך</param>
        public static void SetComboBox<T>(ComboBox cmb, List<T> list, string displayColumn, string valueColumn)
        {
            cmb.DataSource    = list;
            cmb.DisplayMember = displayColumn;
            cmb.ValueMember   = valueColumn;
        }

        /// <summary>
        /// הכנת ComboBox לתצוגה
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונים</typeparam>
        /// <param name="cmb">Cםצנם-פקד ה</param>
        /// <param name="list">הרשימה לתצוגה</param>
        /// <param name="displayColumn">העמודה לתצוגה</param>
        /// <param name="valueColumn">עמודת הערך</param>
        /// <param name="firstItem">האיבר שיתווסף לרשימה</param>
        /// <param name="firstItemColum 0nName">שם עמודת התצוגה של האיבר הנוסף</param>
        /// <param name="index">מיקום האיבר הראשון</param>
        public static void SetComboBox<T>(ComboBox cmb, List<T> list, string displayColumn, string valueColumn, string firstItem, int firstItemValue = 0, int index = 0)
        {
            if (list.Count > 0)
            {
                // Get the type of the list
                Type type = list[0].GetType();

                // Find if there is an element wich "NUM = 0"
                T t = list.Find(item => Convert.ToInt32(item.GetType().GetProperty(valueColumn).GetValue(item, null)) == firstItemValue);

                if (t == null)
                {
                    // Create a new elemnt with "NUM = 0"
                    t = (T)type.GetConstructor(new Type[] { typeof(bool) }).Invoke(new object[] { false });
                    t.GetType().GetProperty(valueColumn).SetValue(t, firstItemValue, null);
                    t.GetType().GetProperty(displayColumn).SetValue(t, firstItem, null);

                    // insert the element to the desired position
                    list.Insert(index, t);
                }
            }

            SetComboBox<T>(cmb, list, displayColumn, valueColumn);
        }

        /// <summary>
        /// הכנת ComboBox לתצוגה
        /// </summary>
        /// <typeparam name="T">טיפוס הנתונים</typeparam>
        /// <param name="cmb">Cםצנם-פקד ה</param>
        /// <param name="list">הרשימה לתצוגה</param>
        /// <param name="displayColumn">העמודה לתצוגה</param>
        /// <param name="valueColumn">עמודת הערך</param>
        /// <param name="firstItem">האיבר שיתווסף לרשימה</param>
        /// <param name="firstItemColum 0nName">שם עמודת התצוגה של האיבר הנוסף</param>
        /// <param name="index">מיקום האיבר הראשון</param>
        public static void SetComboBox<T>(ComboBox cmb, List<T> list, string displayColumn, string valueColumn, T firstItem, int firstItemValue = 0, int index = 0)
        {
            if (list.Count > 0)
            {
                // Get the type of the list
                Type type = list[0].GetType();

                // Find if there is an element wich "NUM = 0"
                T t = list.Find(item => Convert.ToInt32(item.GetType().GetProperty(valueColumn).GetValue(item, null)) == firstItemValue);

                if (t == null)
                {
                    // insert the element to the desired position
                    list.Insert(index, firstItem);
                }
            }

            SetComboBox<T>(cmb, list, displayColumn, valueColumn);
        }

        /// <summary>
        /// ניקוי כל שדות הקלט בטופס
        /// </summary>
        /// <param name="container">הפקד (טופס) לניקוי</param>
        public static void ClearForm(Control container)
        {
            ComboBox combo;

            foreach (Control ctrl in container.Controls)
            {
                if (ctrl.HasChildren)
                    ClearForm(ctrl);
                else
                    if (ctrl is TextBox)
                        ((TextBox)ctrl).Text = string.Empty;
                    else
                        if (ctrl is CheckBox)
                            ((CheckBox)ctrl).Checked = false;
                        else
                            if (ctrl is RadioButton)
                                ((RadioButton)ctrl).Checked = false;
                            else
                                if (ctrl is ComboBox)
                                {
                                    combo = (ComboBox)ctrl;
                                    if (combo.Items.Count > 0)
                                        combo.SelectedIndex = 0;
                                }
                                else
                                    if (ctrl is RichTextBox)
                                        ((RichTextBox)ctrl).Text = string.Empty;
                                    else
                                        if (ctrl is DateTimePicker)
                                        {
                                            ((DateTimePicker)ctrl).Value = DateTime.Now;
                                            ((DateTimePicker)ctrl).Text = string.Empty;
                                        }
            }
        }

        /// <summary>
        /// בדיקה האם טופס פתוח
        /// </summary>
        /// <param name="formType">טיפוס הטופס</param>
        /// <returns>אם הטופס פתוח true</returns>
        public static bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == formType.Name)
                    return true;
            return false;
        }
    }
}

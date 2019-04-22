using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace HELPER
{
    public class DataGridViewUtilities
    {
        /// <summary>
        /// קביעת העמודות המוצגות בטבלה
        /// </summary>
        /// <param name="grd">הטבלה לעיבוד העמודות</param>
        /// <param name="columns">מערך דו מימדי של מחרוזות המתארות את העמודות לתצוגה</param>
        public static void SetColumns(DataGridView grd, string[][] columns)
        {
            string columnName;
            int    displayIndex = 0;

            foreach (DataGridViewColumn column in grd.Columns)
                column.Visible = false;

            foreach (string[] column in columns)
            {
                columnName = column[0].Trim();

                grd.Columns[columnName].Visible                    = true;
                grd.Columns[columnName].HeaderText                 = column[1];
                grd.Columns[columnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grd.Columns[columnName].HeaderCell.Style.Font      = new Font(Control.DefaultFont, FontStyle.Bold);
                grd.Columns[columnName].DisplayIndex               = displayIndex++;
            }
        }

        /// <summary>
        /// קביעת העמודות המוצגות בטבלה
        /// </summary>
        /// <param name="grd">הטבלה לעיבוד העמודות</param>
        /// <param name="columns">מערך דו מימדי של מחרוזות המתארות את העמודות לתצוגה</param>
        /// <param name = "configDataGridView">האם לעצב את הטבלה</param>
        public static void SetColumns(DataGridView grd, string[][] columns, bool configDataGridView)
        {
            SetColumns(grd, columns);
            
            if (configDataGridView)
                ConfigureDataGridView(grd);
        }

        /// <summary>
        /// קביעת העמודות המוצגות בטבלה
        /// </summary>
        /// <param name="grd">הטבלה לעיבוד העמודות</param>
        /// <param name="data">המכיל את הנתונים להצגה LIST</param>
        /// <param name="columns">מערך דו מימדי של מחרוזות המתארות את העמודות לתצוגה</param>
        /// <param name = "configDataGridView">האם לעצב את הטבלה</param>
        public static void SetColumns<T>(DataGridView grd, List<T> data, string[][] columns, bool configDataGridView)
        {
            grd.DataSource = data;
            SetColumns(grd, columns, configDataGridView);
        }

        /// <summary>
        /// קינפוג הטבלה
        /// </summary>
        /// <param name="grd">הטבלה לעיבוד העכמודות</param>
        public static void ConfigureDataGridView(DataGridView grd)
        {
            grd.AllowUserToAddRows          = false;
            grd.AllowUserToDeleteRows       = false;
            grd.Anchor                      = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            grd.AutoSizeColumnsMode         = DataGridViewAutoSizeColumnsMode.Fill;
            grd.BackgroundColor             = Color.White;
            grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grd.RowHeadersVisible           = false;
            grd.ScrollBars                  = ScrollBars.Vertical;
            grd.SelectionMode               = DataGridViewSelectionMode.FullRowSelect;

            grd.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grd.AllowUserToResizeRows     = true;
            //grd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }

        /// <summary>
        /// הוספת עמודת תמונה
        /// </summary>
        /// <param name="grd">הטבלה</param>
        /// <param name="columnName">שם העמודה</param>
        /// <param name="headerText">כותרת העמודה</param>
        /// <param name="displayIndex">(מיקום העמודה (1- עמודה אחרונה</param>
        /// <param name="isVisible">האם להציג את העמודה</param>
        public static void AddImageColumn(DataGridView grd, string columnName, string headerText, int displayIndex, bool isVisible = true)
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name                       = columnName;
            imageColumn.HeaderText                 = headerText;
            imageColumn.Visible                    = isVisible;
            imageColumn.DefaultCellStyle.NullValue = null;
            imageColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageColumn.HeaderCell.Style.Font      = new Font(Control.DefaultFont, FontStyle.Bold);

            int grdNumOfColumns = grd.Columns.Count;

            if (displayIndex == -1)
                imageColumn.DisplayIndex = grdNumOfColumns;
            else
            { 
                foreach (DataGridViewColumn col in grd.Columns)
                    if (col.Visible && col.DisplayIndex >= displayIndex)
                        col.DisplayIndex++;

                imageColumn.DisplayIndex = displayIndex;
            }

            grd.Columns.Add(imageColumn);
        }

        /*
         DataGridView-הצגת תמונות ב
         1. DoubleBuffered יש להפעיל את המטודה 
         2. יש להוסיף עמודת תמונה האמצעות המטודה למעלה
         3. DatagridView של CellFormating יש להפעיל את הארוע
         3. יש לכתוב את השורות הבאות באירוע
         
        private void YOUR_GRID_NAME_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (YOUR_GRID_NAME.Columns[e.ColumnIndex].Name.Equals("YOUR_PICTURE_DATA_MEMBER"))
            {
                YOUR_GRID_NAME.Rows[e.RowIndex].Height = 52;  // יש לתת את גובה התמונה + 2

                if (YOUR_LIST_OF_DATA[e.RowIndex].YOUR_PICTURE_DATA_MEMBER != null && YOUR_LIST_OF_DATA[e.RowIndex].YOUR_PICTURE_DATA_MEMBER != "")
                    ((DataGridViewImageCell)YOUR_GRID_NAME.Rows[e.RowIndex].Cells[e.ColumnIndex]).Value = ImageUtilities.ResizeImageFromFile(YOUR_LIST_OF_DATA[e.RowIndex].YOUR_PICTURE_DATA_MEMBER, 50, 50);  // שני הפרמטרים האחרונים מהווים את גודל התמונה הרצוי
            }
        }
         
        */


        /// <summary>
        /// Double Buffer DataGridView
        /// use when display pictures on datagridview
        /// </summary>
        /// <param name="dgv">DataGridViewControl</param>
        /// <param name="setting">Always true</param>
        public static void DoubleBuffered(DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}

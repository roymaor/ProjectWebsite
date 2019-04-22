using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Web.UI.WebControls;

namespace HELPER
{
    public class GridViewUtilities
    {
        private const double EDIT_COLUMN_PERCENT = 5;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbl">אליו מוסיפים את כותרום העמודות Table-ה</param>
        /// <param name="columns">אוסף העמודות</param>
        /// <param name="gridWidth">%-רוחב הגריד ב</param>
        /// <param name="addCommandColumn">האם להוסיף עמודת עריכה</param>
        public static void SetGridviewHeader(Table tbl, GridViewColumnDefinitions columns, double gridWidth, bool addCommandColumn = false)
        {
            TableCell cell;

            double columnWidth = (gridWidth - EDIT_COLUMN_PERCENT) / columns.Count;

            if (addCommandColumn)
            {
                cell = new TableCell();
                cell.Text = "עריכה";
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Width = new Unit(EDIT_COLUMN_PERCENT, UnitType.Percentage);
                tbl.Rows[0].Cells.Add(cell);
            }

            foreach (GridViewColumnDefinition col in columns)
            {
                cell = new TableCell();
                cell.Text = col.ColumnHeader;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Width = new Unit(columnWidth, UnitType.Percentage);
                tbl.Rows[0].Cells.Add(cell);
            }
        }

        /// <summary>
        /// GridView-קביעת העמודות ב
        /// </summary>
        /// <param name="grd">אליו מוסיפים עמודות GridView-ה</param>
        /// <param name="columns">אוסף העמודות</param>
        /// <param name="gridWidth">%-רוחב הגריד ב</param>
        /// <param name="addCommandColumn">האם להוסיף עמודת עריכה</param>
        public static void SetColumns(GridView grd, GridViewColumnDefinitions columns, double gridWidth, bool addCommandColumn = false)
        {
            BoundField boundColumn;

            double columnWidth = (gridWidth - EDIT_COLUMN_PERCENT) / columns.Count;

            grd.AutoGenerateColumns = false;

            if (addCommandColumn)
            {
                CommandField cField = new CommandField();

                cField.ButtonType = ButtonType.Image;
                cField.ItemStyle.Width = new Unit(EDIT_COLUMN_PERCENT, UnitType.Percentage);
                cField.ControlStyle.Width = new Unit(15, UnitType.Pixel);

                cField.EditImageUrl = "~/Images/Edit_Icon.png";
                cField.EditText = "שינוי";
                cField.DeleteImageUrl = "~/Images/Delete_Icon.png";
                cField.EditText = "מחיקה";
                cField.UpdateImageUrl = "~/Images/OK_Icon.png";
                cField.EditText = "עדכון";
                cField.CancelImageUrl = "~/Images/Cancel_Icon.png";
                cField.EditText = "ביטול";
                cField.SelectImageUrl = "~/Images/Select_Icon.png";
                cField.EditText = "בחירה";
                cField.ShowEditButton = true;
                cField.ShowDeleteButton = true;
                //cField.ShowSelectButton = true;

                grd.Columns.Add(cField);                
            }

            foreach (GridViewColumnDefinition col in columns)
            {
                if (col.ColumnType == ColumnType.PICTURE_COLUMN)
                {
                    ImageField imageField = new ImageField();
                    imageField.DataImageUrlField = col.ColumnName;
                    imageField.HeaderText = col.ColumnHeader;
                    imageField.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    imageField.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
                    imageField.DataImageUrlFormatString = @"~\Images\Upload\{0}";
                    imageField.AlternateText = "תמונה";
                    imageField.NullDisplayText = "חסרה תמונה";
                    imageField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    imageField.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                    imageField.ItemStyle.Width = new Unit(columnWidth, UnitType.Percentage); ;
                    imageField.ControlStyle.Width = new Unit(col.PictureSize.Width, UnitType.Pixel);
                    imageField.ControlStyle.Height = new Unit(col.PictureSize.Height, UnitType.Pixel);
                    grd.Columns.Add(imageField);
                }
                else
                {
                    boundColumn = new BoundField();

                    if (col.ColumnType == ColumnType.CHECKBOX_COLUMN)
                        boundColumn = new CheckBoxField();

                    boundColumn.DataField = col.ColumnName;
                    boundColumn.HeaderText = col.ColumnHeader;
                    boundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    boundColumn.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
                    boundColumn.ItemStyle.Width = new Unit(columnWidth, /* grd.Width.Value / columns.Count, */ UnitType.Percentage);
                    boundColumn.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                    switch (col.ColumnAligment)
                    {
                        case ColumnAligment.CENTER:
                            {
                                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                break;
                            }

                        case ColumnAligment.LEFT:
                            {
                                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                                break;
                            }

                        case ColumnAligment.RIGHT:
                            {
                                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                break;
                            }
                    }

                    grd.Columns.Add(boundColumn);
                }
            }
        }

        /// <summary>
        /// קביעת העמודות המוצגות בטבלה
        /// </summary>
        /// <param name="grd">הטבלה לעיבוד העמודות</param>
        /// <param name="data">המכיל את הנתונים להצגה LIST</param>
        /// <param name="columns">מערך דו מימדי של מחרוזות המתארות את העמודות לתצוגה</param>
        /// <param name = "configDataGridView">האם לעצב את הטבלה</param>
        public static void SetColumns<T>(GridView grd, List<T> data, GridViewColumnDefinitions columns, int gridWidth, bool addCommandColumn = false)
        {
            grd.DataSource = data;
            SetColumns(grd, columns, gridWidth, addCommandColumn);
            grd.DataBind();
        }

        /// <summary>
        /// קינפוג הטבלה,,
        /// </summary>
        /// <param name="grd">הטבלה לעיבוד העכמודות</param>
        public static void ConfigureDataGridView(GridView grd)
        {
            //grd.AllowUserToAddRows = false;
            //grd.AllowUserToDeleteRows = false;
            //grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            //grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //grd.BackgroundColor = Color.White;
            //grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //grd.RowHeadersVisible = false;
            //grd.ScrollBars = ScrollBars.Vertical;
            //grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //grd.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //grd.AllowUserToResizeRows = true;
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
        public static void AddImageColumn(GridView grd, string columnName, string headerText, int displayIndex, bool isVisible = true)
        {
            //GridViewImageColumn imageColumn = new GridViewImageColumn();
            //imageColumn.Name                       = columnName;
            //imageColumn.HeaderText                 = headerText;
            //imageColumn.Visible                    = isVisible;
            //imageColumn.DefaultCellStyle.NullValue = null;
            //imageColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //imageColumn.HeaderCell.Style.Font      = new Font(Control.DefaultFont, FontStyle.Bold);

            //int grdNumOfColumns = grd.Columns.Count;

            //if (displayIndex == -1)
            //    imageColumn.DisplayIndex = grdNumOfColumns;
            //else
            //{ 
            //    foreach (GridViewColumn col in grd.Columns)
            //        if (col.Visible && col.DisplayIndex >= displayIndex)
            //            col.DisplayIndex++;

            //    imageColumn.DisplayIndex = displayIndex;
            //}

            //grd.Columns.Add(imageColumn);
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
        public static void DoubleBuffered(GridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }

    /// <summary>
    /// אוסף העמודות לתצוגה
    /// </summary>
    public class GridViewColumnDefinitions : List<GridViewColumnDefinition>
    {
    }

    /// <summary>
    /// הגדרת עמודה לתוגה
    /// </summary>
    public class GridViewColumnDefinition
    {
        private string v1;
        private string v2;
        private string v3;
        private ColumnAligment rIGHT;

        public string         ColumnName     { get; set; }          // שם העמודה
        public string         ColumnHeader   { get; set; }          // כותרת העמודה
        public ColumnType     ColumnType     { get; set; }          // סוג עמודה
        public ColumnAligment ColumnAligment { get; set; }          // אופן יישור העמודה
        public Size           PictureSize    { get; set; }          // (גודל תמונה (רוחב, גובה
          
        public GridViewColumnDefinition(string columnName, string columnHeader, ColumnType columnType, ColumnAligment columnAligment, Size pictureSize)
        {
            ColumnName      = columnName;
            ColumnHeader    = columnHeader;
            ColumnType      = columnType;
            ColumnAligment  = columnAligment;
            PictureSize     = new Size(pictureSize.Width, pictureSize.Height);
        }

        public GridViewColumnDefinition(string columnName, string columnHeader, ColumnType columnType, ColumnAligment columnAligment)
            : this(columnName, columnHeader, columnType, columnAligment, new Size())
        { }

        public GridViewColumnDefinition(string columnName, string columnHeader, ColumnAligment columnAligment)
            : this(columnName, columnHeader, ColumnType.STRING_COLUMN, columnAligment, new Size())
        { }

        public GridViewColumnDefinition(string columnName, string columnHeader)
            : this(columnName, columnHeader, ColumnAligment.NOTSET)
        { }

        public GridViewColumnDefinition(string v1, string v2, string v3, ColumnAligment rIGHT)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.rIGHT = rIGHT;
        }
    }

    /// <summary>
    /// סוגי העמודות
    /// </summary>
    public enum ColumnType     { STRING_COLUMN, CHECKBOX_COLUMN, PICTURE_COLUMN }

    /// <summary>
    /// אופן יישור הנתונים בעמודה
    /// </summary>
    public enum ColumnAligment { NOTSET, RIGHT, LEFT, CENTER }
}

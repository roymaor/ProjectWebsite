using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MODEL.MyService;
using HELPER;

namespace VIEW_GUI.Users.Admin.SystemTables
{
    public partial class frmareaCodes : System.Web.UI.Page
    {
        private MyServiceClient client;
        private AreaCodes areaCodes;
        private GridViewColumnDefinitions columns;

        protected void Page_Load(object sender, EventArgs e)
        {
            client = new MyServiceClient();
            areaCodes = client.SelectAllAreaCodes();

            // הגדרת עמודות
            columns = new GridViewColumnDefinitions();
            columns.Add(new GridViewColumnDefinition(
                                                        "Name",
                                                        "קידומת",
                                                        ColumnType.STRING_COLUMN,
                                                        ColumnAligment.CENTER
                                                     )
                      );

            if (!IsPostBack)
            {
                // גריד עם אפשרויות עריכה
                GridViewUtilities.SetColumns<AreaCode>(                //     Grid-הטיפוס המוצג ב
                                                   grdCities,     //            Grid-שם ה
                                                   areaCodes,         //           רשימת הישובים
                                                   columns,        //           רשימת העמודות 
                                                   20,             //            Grid-רוחב ה
                                                   true            //         לכלול עמודת עריכה
                                                  );
            }
        }

        protected void grdCities_RowEditing(object sender, GridViewEditEventArgs e)
        {
            AreaCode areaCode = areaCodes[e.NewEditIndex];
            pnlCity.Visible = true;
            txtCity.Text = areaCode.Name;

            Session["EDITMODE"] = true;
            Session["OLDAREACODE"] = areaCode;
            grdCities.EditIndex = -1;

        }

        protected void grdCities_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            AreaCode areaCode = areaCodes[e.RowIndex];

            client.DeleteAreaCode(areaCode);
            Response.Redirect(Request.RawUrl);

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlCity.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            AreaCode oldAreaCode = (AreaCode)Session["OLDAREACODE"];

            AreaCode newAreaCode = new AreaCode();
            newAreaCode.Name = txtCity.Text;

            if (Session["EDITMODE"] == null || !(bool)Session["EDITMODE"])
                client.AddAreaCode(newAreaCode);
            else
            {
                client.ModifyAreaCode(oldAreaCode, newAreaCode);
                Session["EDITMODE"] = false;
                Session["OLDAREACODE"] = null;
            }

            txtCity.Text = "";
            pnlCity.Visible = false;

            Response.Redirect(Request.RawUrl);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCity.Text = "";
            pnlCity.Visible = false;
        }

        protected void grdCities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // בדיקה אם השורה היא שורת נתונים
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // קח את הפקד הראשון בשורה – זהו פקד "פיקוד"
                DataControlFieldCell cell = (DataControlFieldCell)e.Row.Controls[0];

                // מתוך הפקד שנבחר, חלץ את תמונת ה"פיקוד"
                Image img = (Image)cell.Controls[2];
                // הפוך את התמונה לכפתור תמונה
                ImageButton imgBtn = (ImageButton)img;

                // אם הכפתור הוא כפתור מחיקה
                if (imgBtn.CommandName == "Delete")
                {
                    // הרכב את משפט השאלה
                    //        כולל שם הישוב
                    string message = @"האם למחוק את """ + DataBinder.Eval(e.Row.DataItem, "Name") +  @""" ?";

                    // onclick הוסף לתמונה את האירוע 
                    img.Attributes.Add("onclick",
                         "javascript:" +
                         "if (!confirm('" + message + "')){ return false;}");

                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HELPER;

namespace CONTROLS
{
    public partial class MMCreditCardValidity : UserControl
    {
        private List<Item> monthList;
        private List<Item> yearList;

        public MMCreditCardValidity()
        {
            InitializeComponent();

            // Initialize months
            monthList = new List<Item>();
            //monthList.Add(new Item() { Num = 0, Name = (((Form)this.Parent).RightToLeft == RightToLeft.Yes) ? "חודש" : "Month"});
            monthList.Add(new Item() { Num = 0, Name = "חודש" });
            for (int i = 1; i <= 12; i++)
                monthList.Add(new Item() { Num = i, Name = i.ToString() });

            yearList = new List<Item>();
            //yearList.Add(new Item() { Num = 0, Name = (RightToLeft == RightToLeft.Yes) ? "שנה" : "Year"});
            yearList.Add(new Item() { Num = 0, Name = "שנה" });
            for (int i = DateTime.Today.Year; i <= DateTime.Today.Year + 5; i++)
                yearList.Add(new Item() { Num = i, Name = i.ToString() });

            FormUtilities.SetComboBox<Item>(cmbMonth, monthList, "Name", "Num");
            FormUtilities.SetComboBox<Item>(cmbYear,  yearList,  "Name", "Num");

            cmbMonth.SelectedValue = DateTime.Today.Month;
            cmbYear.SelectedValue  = DateTime.Today.Year;
        }

        public MMCreditCardValidity(bool b) : this()
        {
        }

        public int Month
        {
            get { return Convert.ToInt32(cmbMonth.Text); }
        }

        public int Year 
        {
            get { return Convert.ToInt32(cmbYear.Text); }
        }

        public string Separator
        {
            get { return lblSeparator.Text; }
            set { lblSeparator.Text = (value == "") ? "/" : value; }
        }

        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                cmbMonth.RightToLeft = value;
                ((Item)cmbMonth.Items[0]).Name = (value == System.Windows.Forms.RightToLeft.Yes) ? "שנה" : "Month";

                cmbYear.RightToLeft  = value;
                ((Item)cmbYear.Items[0]).Name = (value == System.Windows.Forms.RightToLeft.Yes) ? "חודש" : "Year";
            }
        }

        //protected RightToLeft rightToLeftMonth;
        //public RightToLeft RightToLeftMonth
        //{
        //    get { return cmbMonth.RightToLeft; }
        //    set { cmbMonth.RightToLeft = value; }
        //}

        //protected RightToLeft rightToLeftYear;
        //public RightToLeft RightToLeftYear
        //{
        //    get { return cmbYear.RightToLeft; }
        //    set { cmbYear.RightToLeft = value; }
        //}

        public DateTime Value
        {
            get
            {
                if (IsValid)
                    return new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
                else
                    return DateTime.MinValue;
            }
            set
            {
                if (new DateTime(value.Year, value.Month, DateTime.DaysInMonth(Year, Month)) > DateTime.Today)
                {
                    cmbMonth.SelectedValue = value.Month;
                    cmbYear.SelectedValue  = value.Year;
                }
            }
        }

        public bool IsValid
        {
            get { return new DateTime(Year, Month, 1) >= new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); } 
        }

    }

    public class Item
    {
        public int Num { get; set; }
        public string Name { get; set; }
    }
}

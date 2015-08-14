﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPPatterns.Chap3.SmartUI.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal RRP = decimal.Parse(((
                                             System.Data.DataRowView) e.Row.DataItem)["RRP"].ToString());
                decimal SellingPrice = decimal.Parse(((
                                                      System.Data.DataRowView) e.Row.DataItem)["SellingPrice"].ToString());
                Label lblSavings =
                    (Label) e.Row.FindControl("lblSavings");
                Label lblDiscount =
                    (Label) e.Row.FindControl("lblDiscount");
                Label lblSellingPrice = (Label)e.Row.FindControl("lblSellingPrice");
                lblSavings.Text = DisplaySavings(RRP, ApplyExtraDiscountsTo(SellingPrice));
                lblDiscount.Text = DisplayDiscount(RRP, ApplyExtraDiscountsTo(SellingPrice));
                lblSellingPrice.Text = String.Format("{0:C}", ApplyExtraDiscountsTo(SellingPrice));
            }
        }

        protected decimal ApplyExtraDiscountsTo(decimal OriginalSalePrice)
        {
            decimal price = OriginalSalePrice;
            int discountType = Int16.Parse(this.ddlDiscountType.SelectedValue);
            if (discountType == 1)
            {
                price = price * 0.95M;
            }
            return price;
        }
        protected void ddlDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        private string DisplayDiscount(decimal RRP, decimal SalePrice)
        {
            string discountText = "";
            if (RRP > SalePrice)
                discountText = String.Format("{0:C}", (RRP - SalePrice));
            return discountText;
        }

        private string DisplaySavings(decimal RRP, decimal SalePrice)
        {
            string savingsTest = "";
            if (RRP > SalePrice)
                savingsTest = (1 - (SalePrice / RRP)).ToString("#%");
            return savingsTest;
        }
    }
}

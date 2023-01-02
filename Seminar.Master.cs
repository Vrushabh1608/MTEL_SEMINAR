using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic;
using ShoppingCart.BL;
using System.Web;

public partial class Seminar : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, System.EventArgs e)
    {
      
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            string UserId = null, UserName = null, DBName = null, UserTypeCode = null;

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                UserId = Request.Cookies["MyCookiesLoginInfo"]["UserID"];
                UserName = Request.Cookies["MyCookiesLoginInfo"]["UserName"];
                DBName = Request.Cookies["MyCookiesLoginInfo"]["DBName"];
                UserTypeCode = Request.Cookies["MyCookiesLoginInfo"]["UserTypeCode"];
                string role = Request.Cookies["MyCookiesLoginInfo"]["Role"];
                lblHeader_User_Name.Text = UserName;
                lblHeader_User_Code.Text = UserId;
            }
        }
    }

    protected void BtnLogOut_Click(object sender, System.EventArgs e)
    {
        Response.Cookies["MyCookiesLoginInfo"].Expires.TimeOfDay.ToString();
        Session.RemoveAll();
        Response.Redirect("Login.aspx", false);

    }


}

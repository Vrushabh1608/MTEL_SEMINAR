using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ThanksMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Message"] != null)
            {
                if (Request.QueryString["Message"].ToString() == "Admission")
                {
                    lblMessage.Text = "Thanks for Enrolling with Mahesh Tutorials...!";
                }
                else if (Request.QueryString["Message"].ToString() == "Enquiry")
                {
                    lblMessage.Text = "Thank You for the Enquiry....!";
                }

            }
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if ((Request.QueryString["UserId"] != null) && (Request.QueryString["DivName"] != null) && (Request.QueryString["DivCode"] != null) && (Request.QueryString["AcYear"] != null) && (Request.QueryString["AcYearCode"] != null) && (Request.QueryString["Center"] != null) && (Request.QueryString["CenterCode"] != null))
        {
            Response.Redirect("Default.aspx?UserId=" + Request.QueryString["UserId"] + "&DivName=" + Request.QueryString["DivName"] + "&DivCode=" + Request.QueryString["DivCode"] + "&AcYear=" + Request.QueryString["AcYear"] + "&AcYearCode=" + Request.QueryString["AcYearCode"] + "&Center=" + Request.QueryString["Center"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "");
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

}
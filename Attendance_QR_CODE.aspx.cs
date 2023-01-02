using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Net;
using ShoppingCart.BL;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class Attendance_QR_Code : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtSearchUID.Attributes.Add("OnChange", "javascript:return DoPostBack()");
            lblDivHead.Text = "Please Fill Following Information";

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            //string UserID = cookie.Values["UserID"];
            //string UserName = cookie.Values["UserName"];
            if (cookie == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    #region Event

    protected void txtSearchUID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //DivRedirectPage.Visible = false;
            Clear_Error_Success_Box();            
            lblStudentName.Text = "";
            lblParentName.Text = "";
            lblContactNumber.Text = "";

            DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(), "3");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Result"].ToString() == "-1")
                {
                    Show_Error_Success_Box("E", "UID Not Found");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnSaveAttendance.Visible = false;
                    BtnClose.Visible = false;
                   // DivRedirectPage.Visible = true;
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                {
                    tblSearchInfo.Visible = true;
                    tblSearchDetail.Visible = false;
                    BtnSaveAttendance.Visible = true;
                    BtnClose.Visible = true;

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        txtUID.Text = ds.Tables[1].Rows[0]["UID"].ToString();
                        lblStudentName.Text = ds.Tables[1].Rows[0]["Con_Firstname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_midname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_lastname"].ToString();
                        lblParentName.Text = ds.Tables[1].Rows[0]["ParentName"].ToString();
                        lblContactNumber.Text = ds.Tables[1].Rows[0]["StudContact"].ToString();

                        Clear_Error_Success_Box();
                        int ResultId = 0;
                        ResultId = ProductController.UpdateSeminar_Attendance(txtUID.Text.Trim(), "3");
                        if (ResultId == 1)
                        {
                            if (lblContactNumber.Text != "")
                            {
                                //string Message = "Dear Student/Parent\nWe take the pleasure to welcome you to a prestigious seminar of MT Educare for Academic Year 2017-2018.\nYour Reference UID is :" + txtUID.Text.Trim() + "\nPlease refer this UID for any further communication at the venue.\nWarm Regards\nLakshya\nBy Forum for competition";
                                //commited on 18082018
                               // string Message = "Dear Student/Parent,\nWelcome to the Lakshya Seminar.\nYour reference UID is : " + txtUID.Text.Trim() + ".\nPlease refer to this UID for any further communication at the venue. Click this link to download the Ebrochure of Lakshya.\nhttp://bit.ly/LakshyaBrochure\nGo ahead and empower yourself today to make an informed decision. Good Day!\nWarm Regards\nMT Lakshya";
                                string Message = "Dear Student/Parent,\nWelcome to the Lakshya Seminar.\nYour reference UID is : " + txtUID.Text.Trim() + ".\nPlease refer to this UID for any further communication at the venue. Good Day!\nWarm Regards\nMT Lakshya";
                                SMSSend(lblContactNumber.Text, Message);
                                //string Message1 = "Dear Student/Parent,\nPlease find our e-Brochure: http://bit.ly/LakshyaBrochure";
                                //SMSSend(lblContactNumber.Text, Message1);
                            }

                            Show_Error_Success_Box("S", "Attendance Saved Successfully....!");
                            tblSearchInfo.Visible = false;
                            // divAdddNewStud.Visible = false;
                             tblSearchDetail.Visible = true;
                            BtnSaveAttendance.Visible = false;
                            BtnClose.Visible = false;
                            txtSearchUID.Text = "";
                           // DivRedirectPage.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void lnkSearchInfo_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            tblSearchInfo.Visible = true;
            tblSearchDetail.Visible = false;
            BtnSaveAttendance.Visible = true;
            BtnClose.Visible = true;
            lblStudentName.Text = "";
            lblParentName.Text = "";
            lblContactNumber.Text = "";


            DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(), "3");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Result"].ToString() == "-1")
                {
                    Show_Error_Success_Box("E", "UID Not Found");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnSaveAttendance.Visible = false;
                    BtnClose.Visible = false;
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        txtUID.Text = ds.Tables[1].Rows[0]["UID"].ToString();
                        lblStudentName.Text = ds.Tables[1].Rows[0]["Con_Firstname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_midname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_lastname"].ToString();
                        lblParentName.Text = ds.Tables[1].Rows[0]["ParentName"].ToString();
                        lblContactNumber.Text = ds.Tables[1].Rows[0]["StudContact"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }




    protected void BtnSaveAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            int ResultId = 0;
            ResultId = ProductController.UpdateSeminar_Attendance(txtUID.Text.Trim(), "3");
            if (ResultId == 1)
            {
                if (lblContactNumber.Text != "")
                {
                    //string Message = "Dear Student/Parent\nWe take the pleasure to welcome you to a prestigious seminar of MT Educare for Academic Year 2017-2018.\nYour Reference UID is :" + txtUID.Text.Trim() + "\nPlease refer this UID for any further communication at the venue.\nWarm Regards\nLakshya\nBy Forum for competition";
                    string Message = "Dear Student/Parent,\nWelcome to the Lakshya Seminar.\nYour reference UID is : " + txtUID.Text.Trim() + ".\nPlease refer to this UID for any further communication at the venue. Click this link to download the Ebrochure of Lakshya.\nhttp://bit.ly/LakshyaBrochure\nGo ahead and empower yourself today to make an informed decision. Good Day!\nWarm Regards\nMT Lakshya";
                    SMSSend(lblContactNumber.Text, Message);
                    //string Message1 = "Dear Student/Parent,\nPlease find our e-Brochure: http://bit.ly/LakshyaBrochure";
                    //SMSSend(lblContactNumber.Text, Message1);
                }

                Show_Error_Success_Box("S", "Attendance Saved Successfully....!");
                tblSearchInfo.Visible = false;
                tblSearchDetail.Visible = true;
                BtnSaveAttendance.Visible = false;
                BtnClose.Visible = false;
                txtSearchUID.Text = "";

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        //tblSearchInfo.Visible = false;
        //tblSearchDetail.Visible = true;
        //BtnSaveAttendance.Visible = false;
        //BtnClose.Visible = false;
        //txtSearchUID.Text = "";
        Response.Redirect("Attendance_QR_Code.aspx");
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("Attendance_QR_Code.aspx");
    }

    #endregion

    #region Methods
    /// <summary>
    /// Show Error or success box on top base on boxtype and Error code
    /// </summary>
    /// <param name="BoxType">BoxType</param>
    /// <param name="Error_Code">Error_Code</param>
    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
        else
        {
            Msg_Success.Visible = true;
            Msg_Error.Visible = false;
            lblSuccess.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
    }

    /// <summary>
    /// Fill drop down list and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }






    /// <summary>
    /// Clear Error Success Box
    /// </summary>
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }


    public void SMSSend(string MobNo, string Msg)
    {
        if (MobNo.Length == 10)
        {
            //MobNo = MobNo; need to chk by jayant
            WebClient client = new WebClient();
            string baseurl = "http://api.smscountry.com/SMSCwebservice_bulk.aspx?User=Sciencetr&passwd=mtel@4321&mobilenumber=" + MobNo + "&message=" + Msg + "&sid=MTEDU&mtype=N&DR=Y";
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
    }



    #endregion
    
}
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
public partial class Attendance_Staff_QR_Code : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
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

    private void Page_Validation()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo Info = new System.IO.FileInfo(Path);
        string pageName = Info.Name;


        if (UserID == "UM100000278" || UserID =="UM100000398")
        {
            //Allow
        }
    
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    #region Event

    protected void txtSearchUID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //DivRedirectPage.Visible = false;
            Clear_Error_Success_Box();
            lblStaffRFID.Text = "";
            lblParentName.Text = "";
            lblContactNumber.Text = "";


            DataSet ds = ProductController.insert_update_Events_details("", "", "", "", "7", "", txtSearchUID.Text.Trim(), "", "");
                   
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["Result"].ToString() == "-1")
                {
                    Show_Error_Success_Box("E", "User Details are Not Found");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnSaveAttendance.Visible = false;
                    BtnClose.Visible = false;
                    // DivRedirectPage.Visible = true;
                    return;
                }
                else if (ds.Tables[1].Rows[0]["Result"].ToString() == "1")
                {
                    tblSearchInfo.Visible = true;
                    tblSearchDetail.Visible = false;
                    BtnSaveAttendance.Visible = true;
                    BtnClose.Visible = true;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtUID.Text = ds.Tables[0].Rows[0]["User_Code"].ToString();
                        lblStaffRFID.Text = ds.Tables[0].Rows[0]["RFID_CardNo"].ToString();
                       

                        Clear_Error_Success_Box();
                        
                        //ResultId = ProductController.UpdateSeminar_Attendance(txtUID.Text.Trim(), "3");
                        DataSet ds1= ProductController.insert_update_Events_details("", "", "", "", "8", "", txtUID.Text.Trim(), "", lblStaffRFID.Text.Trim());
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            
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
            txtSearchUID.Text = "";
            return;
        }
    }

    protected void lnkSearchInfo_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    Clear_Error_Success_Box();
        //    tblSearchInfo.Visible = true;
        //    tblSearchDetail.Visible = false;
        //    BtnSaveAttendance.Visible = true;
        //    BtnClose.Visible = true;
        //    lblStudentName.Text = "";
        //    lblParentName.Text = "";
        //    lblContactNumber.Text = "";


        //    DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(), "3");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["Result"].ToString() == "-1")
        //        {
        //            Show_Error_Success_Box("E", "UID Not Found");
        //            tblSearchInfo.Visible = false;
        //            tblSearchDetail.Visible = true;
        //            BtnSaveAttendance.Visible = false;
        //            BtnClose.Visible = false;
        //            return;
        //        }
        //        else if (ds.Tables[0].Rows[0]["Result"].ToString() == "1")
        //        {
        //            if (ds.Tables[1].Rows.Count > 0)
        //            {
        //                txtUID.Text = ds.Tables[1].Rows[0]["UID"].ToString();
        //                lblStudentName.Text = ds.Tables[1].Rows[0]["Con_Firstname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_midname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_lastname"].ToString();
        //                lblParentName.Text = ds.Tables[1].Rows[0]["ParentName"].ToString();
        //                lblContactNumber.Text = ds.Tables[1].Rows[0]["StudContact"].ToString();
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Show_Error_Success_Box("E", ex.ToString());
        //    return;
        //}
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





    #endregion
    
}
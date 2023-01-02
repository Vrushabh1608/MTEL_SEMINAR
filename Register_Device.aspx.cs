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

public partial class Register_Device : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDivHead.Text = "Please Fill Following Information";
            //FillDDL_Division();
            //FillDDL_CurrentSScCenter();

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];
            if ((UserID == "") || (UserID == null))
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

#region Event
    protected void lnkSearchInfo_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            tblSearchInfo.Visible = true;
            tblSearchDetail.Visible = false;
            BtnRegister.Visible = true;
            BtnClose.Visible = true;
            lblStudentName.Text = "";         
            lblParentName.Text = "";
            txtSerialNumber.Text = "";
            //txtManufacturer.Text = "";
            ddlModelNo.SelectedIndex = 0;
            ddlSDCardSize.SelectedIndex = 0;
            trViewInfoMessage.Visible = false;

            DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(),"4");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Result"].ToString() == "-1")
                {
                    Show_Error_Success_Box("E", "UID Not Found");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnRegister.Visible = false;
                    BtnClose.Visible = false;
                    trViewInfoMessage.Visible = false;
                    return;
                }
                if (ds.Tables[0].Rows[0]["Result"].ToString() == "-41")
                {
                    Show_Error_Success_Box("E", "First Take Admission");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnRegister.Visible = false;
                    BtnClose.Visible = false;
                    trViewInfoMessage.Visible = false;
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Result"].ToString() == "-4")
                {
                    if (ds.Tables[1].Rows[0]["Pay_Type"].ToString() == "01")
                    {
                        Show_Error_Success_Box("E", "Payment made by Cheque , hence Device Cannot be issued....");
                        tblSearchInfo.Visible = false;
                        tblSearchDetail.Visible = true;
                        BtnRegister.Visible = false;
                        BtnClose.Visible = false;
                        trViewInfoMessage.Visible = false;
                        return;
                    }
                    txtUID.Text = ds.Tables[1].Rows[0]["UID"].ToString();
                    lblStudentName.Text = ds.Tables[1].Rows[0]["Con_Firstname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_midname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_lastname"].ToString();
                    lblParentName.Text = ds.Tables[1].Rows[0]["ParentName"].ToString();
                    txtManufacturer.Text = ds.Tables[1].Rows[0]["Manufacturer"].ToString();
                    ddlModelNo.SelectedValue = ds.Tables[1].Rows[0]["Model_No"].ToString();
                    ddlSDCardSize.SelectedValue = ds.Tables[1].Rows[0]["SDCardSize"].ToString();
                    txtSerialNumber.Text = ds.Tables[1].Rows[0]["Serial_No"].ToString();
                    BtnRegister.Visible = false;
                    trViewInfoMessage.Visible = true;
                    ddlModelNo.Enabled = false;
                    ddlSDCardSize.Enabled = false;
                    txtSerialNumber.Enabled = false;
                    return;
                }   
                else if (ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0]["Pay_Type"].ToString() == "01")
                        {
                            Show_Error_Success_Box("E", "Payment made by Cheque , hence Device Cannot be issued....");
                            tblSearchInfo.Visible = false;
                            tblSearchDetail.Visible = true;
                            BtnRegister.Visible = false;
                            BtnClose.Visible = false;
                            trViewInfoMessage.Visible = false;
                            return;
                        }

                        txtUID.Text = ds.Tables[1].Rows[0]["UID"].ToString();
                        lblStudentName.Text = ds.Tables[1].Rows[0]["Con_Firstname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_midname"].ToString() + " " + ds.Tables[1].Rows[0]["Con_lastname"].ToString();
                        lblParentName.Text = ds.Tables[1].Rows[0]["ParentName"].ToString();
                        ddlModelNo.Enabled = true;
                        ddlSDCardSize.Enabled = true;
                        txtSerialNumber.Enabled = true;
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




    //protected void BtnSaveAttendance_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Clear_Error_Success_Box();
    //        int ResultId = 0;
    //        ResultId = ProductController.UpdateSeminar_Attendance(txtUID.Text.Trim(), "3");
    //        if (ResultId == 1)
    //        {
    //            Show_Error_Success_Box("S", "Attendance Saved Successfully....!");
    //            tblSearchInfo.Visible = false;
    //            tblSearchDetail.Visible = true;
    //            BtnRegister.Visible = false;
    //            BtnClose.Visible = false;
    //        }               
            
    //    }
    //    catch (Exception ex)
    //    {
    //        Show_Error_Success_Box("E", ex.ToString());
    //        return;
    //    }
    //}

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        tblSearchInfo.Visible = false;
        tblSearchDetail.Visible = true;
        BtnRegister.Visible = false;
        BtnClose.Visible = false;
        trViewInfoMessage.Visible = false;
        txtSearchUID.Text = "";
    }

    protected void BtnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            
            //if (txtDeviceName.Text.Trim() == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Device Name");
            //    txtDeviceName.Focus();
            //    return;
            //}
            if (ddlModelNo.SelectedValue.ToString() == "0")
            {
                Show_Error_Success_Box("E", "Select Model Number");
                ddlModelNo.Focus();
                return;
            }
            if (ddlModelNo.SelectedValue.ToString() == "0")
            {
                Show_Error_Success_Box("E", "Select Model Number");
                ddlModelNo.Focus();
                return;
            }
            if (ddlSDCardSize.SelectedValue.ToString() == "0")
            {
                Show_Error_Success_Box("E", "Select SD Card Size");
                ddlSDCardSize.Focus();
                return;
            }
            if (txtSerialNumber.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter IMEI Number");
                txtSerialNumber.Focus();
                return;
            }

            if (txtSerialNumber.Text.Length != 15)
            {
                Show_Error_Success_Box("E", "Enter 15 Digit IMEI Number");
                txtSerialNumber.Focus();
                return;
            }
            int ResultId = 0;
            ResultId = ProductController.InsertUpdateSeminar_TabDetail(txtUID.Text.Trim(), txtSerialNumber.Text.Trim(), txtManufacturer.Text.Trim(),ddlModelNo.SelectedValue.ToString(),ddlSDCardSize.SelectedValue.ToString() ,"4");
            if (ResultId == 1)
            {
                Show_Error_Success_Box("S", "UID Registered Successfully....!");
                tblSearchInfo.Visible = false;
                tblSearchDetail.Visible = true;
                BtnRegister.Visible = false;
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
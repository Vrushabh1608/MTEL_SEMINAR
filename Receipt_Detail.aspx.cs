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

public partial class Receipt_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDivHead.Text = "Please Fill Following Information";
            //FillDDL_Division();
            //FillDDL_CurrentSScCenter();
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
            BtnSave.Visible = false;
            BtnClose.Visible = true;
            txtFName.Text = "";
            txtMName.Text = "";
            txtLName.Text = "";
            txtAddress.Text = "";
            txtParentName.Text = "";
            txtParentContact.Text = "";
            txtStudentEmailId.Text = "";
            txtParentEmailId.Text = "";
            txtContactNo.Text = "";
            chkCheck.Checked = false;
            //ddlCurrentSSCCenter.SelectedIndex = 0;
            //ddlDivision.SelectedIndex = 0;b
           
           
           

            DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(),"2");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Result"].ToString() == "-1")
                {
                    Show_Error_Success_Box("E", "UID Not Found");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnSave.Visible = false;
                    BtnClose.Visible = false;
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Result"].ToString() == "-2")
                {
                    Show_Error_Success_Box("E", "Admission Already Taken");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnSave.Visible = false;
                    BtnClose.Visible = false;
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Result"].ToString() == "-3")
                {
                    Show_Error_Success_Box("E", "Parents has not yet Accepted Terms and Conditions");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnSave.Visible = false;
                    BtnClose.Visible = false;
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        txtUID.Text = ds.Tables[1].Rows[0]["UID"].ToString();
                        txtFName.Text = ds.Tables[1].Rows[0]["Con_Firstname"].ToString();
                        txtMName.Text = ds.Tables[1].Rows[0]["Con_midname"].ToString();
                        txtLName.Text = ds.Tables[1].Rows[0]["Con_lastname"].ToString();
                        txtAddress.Text = ds.Tables[1].Rows[0]["Address"].ToString();
                        txtParentName.Text = ds.Tables[1].Rows[0]["ParentName"].ToString();
                        txtParentContact.Text = ds.Tables[1].Rows[0]["ParentContact"].ToString();
                        txtStudentEmailId.Text = ds.Tables[1].Rows[0]["Emailid"].ToString();
                        txtParentEmailId.Text = ds.Tables[1].Rows[0]["ParentEmailid"].ToString();
                        txtContactNo.Text = ds.Tables[1].Rows[0]["StudContact"].ToString();
                        //ddlCurrentSSCCenter.SelectedValue = ds.Tables[1].Rows[0]["CurrentSScCenter"].ToString();
                    }
                }
            }

            //ddlProductName.SelectedIndex = 0;
            //ddlDivision.SelectedIndex = 0;

            //lblDivHead.Text = "Result";
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

   

    
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (txtFName.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter First Name");
                txtFName.Focus();
                return;
            }
            if (txtContactNo.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Student Contact Number");
                txtContactNo.Focus();
                return;
            }
            if (txtStudentEmailId.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Student Email Id");
                txtStudentEmailId.Focus();
                return;
            }
            if (txtParentContact.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Parent Contact Number");
                txtParentContact.Focus();
                return;
            }
            
            int ResultId = 0;
            string StudName = txtFName.Text.Trim() + ' ' + txtMName.Text.Trim() + ' ' + txtLName.Text;
            //ResultId = ProductController.InsertUpdateZScienceSeminar_Parent(txtFName.Text.Trim(),txtMName.Text.Trim(),txtLName.Text.Trim(), txtContactNo.Text.Trim(), txtStudentEmailId.Text.Trim(), txtAddress.Text.Trim(), txtParentName.Text.Trim(), txtParentContact.Text.Trim(), txtParentEmailId.Text.Trim(), txtUID.Text.Trim(), "1");
            if (ResultId == 1)
            {         
                Show_Error_Success_Box("S", "Student Information Updated Successfully....!");
                tblSearchInfo.Visible = false;
                tblSearchDetail.Visible = true;
                BtnSave.Visible = false;
                BtnClose.Visible = false;
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
        tblSearchInfo.Visible = false;
        tblSearchDetail.Visible = true;
        BtnSave.Visible = false;
        BtnClose.Visible = false;
    }

    protected void chkCheck_CheckedChanged(object sender, EventArgs e)
    {
        

        try
        {
            if (chkCheck.Checked == true)
            {
                divAdddNewStud.Visible = false;
                divApplyTermsCond.Visible = true;
            }
            else
            {
                BtnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    protected void btnApplyTermsCond1_Click(object sender, EventArgs e)
    {
        divAdddNewStud.Visible = true;
        divApplyTermsCond.Visible = false;
        BtnSave.Visible = true;
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
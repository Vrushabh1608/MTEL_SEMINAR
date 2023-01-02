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
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;
using System.Text;

public partial class Counseled_by : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDivHead.Text = "Please Fill Following Information";
            FillDDL_CouncilBy();

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
            BtnSaveCouncil.Visible = true;
            BtnClose.Visible = true;
            lblStudentName.Text = "";         
            lblParentName.Text = "";
            lblContactNumber.Text = "";
            ddlCouncilBy.SelectedIndex = 0;

            DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(),"5");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Result"].ToString() == "-1")
                {
                    Show_Error_Success_Box("E", "UID Not Found");
                    tblSearchInfo.Visible = false;
                    tblSearchDetail.Visible = true;
                    BtnSaveCouncil.Visible = false;
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
                        if (ds.Tables[1].Rows[0]["Council_Id"].ToString() != "")
                        {
                            ddlCouncilBy.SelectedValue = ds.Tables[1].Rows[0]["Council_Id"].ToString();
                            ddlCouncilBy.Enabled = false;
                        }
                        else
                        {
                            ddlCouncilBy.Enabled = true;
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




    protected void BtnSaveCouncil_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlCouncilBy.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Counsel..!");
                return;
            }

            int ResultId = 0;
            string UIDEncriptCode = HttpUtility.UrlEncode(Encrypt(txtUID.Text.Trim()));
            ResultId = ProductController.UpdateSeminar_Council(txtUID.Text.Trim(),ddlCouncilBy.SelectedValue,UIDEncriptCode, "5");
            if (ResultId == 1)
            {                                
                Show_Error_Success_Box("S", "Record Saved Successfully....!");
                tblSearchInfo.Visible = false;
                tblSearchDetail.Visible = true;
                BtnSaveCouncil.Visible = false;
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
        tblSearchInfo.Visible = false;
        tblSearchDetail.Visible = true;
        BtnSaveCouncil.Visible = false;
        BtnClose.Visible = false;
        txtSearchUID.Text = "";
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



    private void FillDDL_CouncilBy()
    {

        try
        {
            Clear_Error_Success_Box();
            // string UserID = txtUserId.Text.Trim();
            DataSet dsCouncilBy = ProductController.GetCouncil_Seminar();
            BindDDL(ddlCouncilBy, dsCouncilBy, "Council_Name", "Council_Id");
            ddlCouncilBy.Items.Insert(0, "Select");
            ddlCouncilBy.SelectedIndex = 0;
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


    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
			0x49,
			0x76,
			0x61,
			0x6e,
			0x20,
			0x4d,
			0x65,
			0x64,
			0x76,
			0x65,
			0x64,
			0x65,
			0x76
		});
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }


  

    
    #endregion




    
}
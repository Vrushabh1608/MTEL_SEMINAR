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

public partial class SeminarFeedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                lblDivHead.Text = "Feedback";            
                lblUID.Text = Request.QueryString["UId"];           
                FillDDL_FeedBackQue();
            }
            catch
            {
                divAdddNewStud.Visible = false;
            }
        }
    }

#region Event

    protected void BtnSave_Click(object sender, EventArgs e)
    {
       //string ResultId = HttpUtility.UrlEncode(Encrypt(lblUID.Text));
       //string a;
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        UpdatePanelMsgBox.Update();
        string Answer = "";
        string XMLData = "<Seminar>"; //ContactSource = "", Contacttitle = "", ContactFirstName = "", ContactMidName = "", ContactLastName = "", Gender = "", EmailId = "", Handphone1 = "", Country = "";
        foreach (DataListItem dtlItem in dlFeedBackQue.Items)
        {
            Label lblPointID = (Label)dtlItem.FindControl("lblPointID");
            CheckBox chkExcellent = (CheckBox)dtlItem.FindControl("chkExcellent");
            CheckBox chkVGood = (CheckBox)dtlItem.FindControl("chkVGood");
            CheckBox chkGood = (CheckBox)dtlItem.FindControl("chkGood");
            CheckBox chkSatisfactory = (CheckBox)dtlItem.FindControl("chkSatisfactory");
            if ((chkExcellent.Checked == false) && (chkVGood.Checked == false) && (chkGood.Checked == false) && (chkSatisfactory.Checked == false))
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Feedback not completely provided.";
                UpdatePanelMsgBox.Update();
                return;
            }
            else
            {                
                if (chkExcellent.Checked == true)
                    Answer = "Excellent";
                else if (chkVGood.Checked == true)
                    Answer = "Very Good";
                else if (chkGood.Checked == true)
                    Answer = "Good";
                else if (chkSatisfactory.Checked == true)
                    Answer = "Satisfactory";

                XMLData = XMLData + "<Feedback><QNo>" + lblPointID.Text + "</QNo><Ans>" + Answer + "</Ans></Feedback>";
            }
        }

        foreach (DataListItem dtlItem in dlFeedBackType2.Items)
        {
            Label lblQuestiNo = (Label)dtlItem.FindControl("lblQuestiNo");
            System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkQNo1Ans");
            if (chkActiveFlag.Checked == true)
                Answer = "Yes";
            else
                Answer = "No";

            XMLData = XMLData + "<Feedback><QNo>" + lblQuestiNo.Text + "</QNo><Ans>" + Answer + "</Ans></Feedback>";
            
        }

        XMLData = XMLData + "</Seminar>";

        DataSet dsFeedBack = ProductController.Insert_Feedback(lblUID.Text.Trim(),XMLData, "1");
        if (dsFeedBack != null)
        {
            if (dsFeedBack.Tables.Count > 0)
            {
                if (dsFeedBack.Tables[0].Rows[0]["Result"].ToString() == "1")
                {
                    divAdddNewStud.Visible = false;
                    Msg_Error.Visible = false;
                    Msg_Success.Visible = true;
                    lblSuccess.Text = "Feedback save successfully..!";
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }
        }
    }

    protected void chkExcellent_CheckedChanged(object sender, EventArgs e)
    {
        //CheckBox chkitemck = (CheckBox)e.FindControl("chkCenter");
        foreach (DataListItem dtlItem in dlFeedBackQue.Items)
        {
            CheckBox chkExcellent = (CheckBox)dtlItem.FindControl("chkExcellent");
            CheckBox chkVGood = (CheckBox)dtlItem.FindControl("chkVGood");
            CheckBox chkGood = (CheckBox)dtlItem.FindControl("chkGood");
            CheckBox chkSatisfactory = (CheckBox)dtlItem.FindControl("chkSatisfactory");
            if (chkExcellent.Checked == true)
            {
                chkVGood.Checked = false;
                chkGood.Checked = false;
                chkSatisfactory.Checked = false;
            }
        }
    }

    protected void chkVGood_CheckedChanged(object sender, EventArgs e)
    {
        //CheckBox chkitemck = (CheckBox)e.FindControl("chkCenter");
        foreach (DataListItem dtlItem in dlFeedBackQue.Items)
        {
            CheckBox chkExcellent = (CheckBox)dtlItem.FindControl("chkExcellent");
            CheckBox chkVGood = (CheckBox)dtlItem.FindControl("chkVGood");
            CheckBox chkGood = (CheckBox)dtlItem.FindControl("chkGood");
            CheckBox chkSatisfactory = (CheckBox)dtlItem.FindControl("chkSatisfactory");
            if (chkVGood.Checked == true)
            {
                chkExcellent.Checked = false;
                chkGood.Checked = false;
                chkSatisfactory.Checked = false;
            }
        }
    }

    protected void chkGood_CheckedChanged(object sender, EventArgs e)
    {
        //CheckBox chkitemck = (CheckBox)e.FindControl("chkCenter");
        foreach (DataListItem dtlItem in dlFeedBackQue.Items)
        {
            CheckBox chkExcellent = (CheckBox)dtlItem.FindControl("chkExcellent");
            CheckBox chkVGood = (CheckBox)dtlItem.FindControl("chkVGood");
            CheckBox chkGood = (CheckBox)dtlItem.FindControl("chkGood");
            CheckBox chkSatisfactory = (CheckBox)dtlItem.FindControl("chkSatisfactory");
            if (chkGood.Checked == true)
            {
                chkExcellent.Checked = false;
                chkVGood.Checked = false;
                chkSatisfactory.Checked = false;
            }
        }
    }

    protected void chkSatisfactory_CheckedChanged(object sender, EventArgs e)
    {
        //CheckBox chkitemck = (CheckBox)e.FindControl("chkCenter");
        foreach (DataListItem dtlItem in dlFeedBackQue.Items)
        {
            CheckBox chkExcellent = (CheckBox)dtlItem.FindControl("chkExcellent");
            CheckBox chkVGood = (CheckBox)dtlItem.FindControl("chkVGood");
            CheckBox chkGood = (CheckBox)dtlItem.FindControl("chkGood");
            CheckBox chkSatisfactory = (CheckBox)dtlItem.FindControl("chkSatisfactory");
            if (chkSatisfactory.Checked == true)
            {
                chkExcellent.Checked = false;
                chkVGood.Checked = false;
                chkGood.Checked = false;
            }
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

    private void FillDDL_FeedBackQue()
    {

        try
        {
            Clear_Error_Success_Box();           
            // string UserID = txtUserId.Text.Trim();
            DataSet dsFeedBackQue = ProductController.Get_SeminarDetail_ByUID(lblUID.Text.Trim(), "6");
            if (dsFeedBackQue != null)
            {
                if (dsFeedBackQue.Tables.Count > 0)
                {
                    if (dsFeedBackQue.Tables[0].Rows[0]["Result"].ToString() == "1")
                    {
                        dlFeedBackQue.DataSource = dsFeedBackQue.Tables[1];
                        dlFeedBackQue.DataBind();

                        dlFeedBackType2.DataSource = dsFeedBackQue.Tables[2];
                        dlFeedBackType2.DataBind();

                        lblStudentName.Text = dsFeedBackQue.Tables[3].Rows[0]["StudentName"].ToString();
                        lblStudentContactNo.Text = dsFeedBackQue.Tables[3].Rows[0]["StudContact"].ToString();
                        lblParentName.Text = dsFeedBackQue.Tables[3].Rows[0]["ParentName"].ToString();
                        lblParentContactNo.Text = dsFeedBackQue.Tables[3].Rows[0]["ParentContact"].ToString();
                    }
                    else if (dsFeedBackQue.Tables[0].Rows[0]["Result"].ToString() == "-1")//if the UID Not Found
                    {
                        divAdddNewStud.Visible = false;
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "UID Not Found..!";
                        UpdatePanelMsgBox.Update();
                        return;
                    }
                    else//if the feedback already save 
                    {
                        divAdddNewStud.Visible = false;
                        Msg_Error.Visible = false;
                        Msg_Success.Visible = true;
                        lblSuccess.Text = "Feedback already entered...!";
                        UpdatePanelMsgBox.Update();
                        return;
                    }
                }
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
    
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
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
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
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
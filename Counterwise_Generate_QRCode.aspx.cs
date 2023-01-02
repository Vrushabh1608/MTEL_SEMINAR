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
using iTextSharp.text.pdf;
using QRCoder;
using iTextSharp.text;

public partial class Counterwise_Generate_QRCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDivHead.Text = "Please Fill Following Information";            

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];
            if ((UserID == "") || (UserID == null))
            {
                Response.Redirect("Login.aspx");
            }

            FillDDL_TableNo();
            FillDDL_Center();
            FillDDL_Session();
        }
    }

#region Event
    protected void lnkSearchInfo_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            string TableNo = "", Center = "", Session = "";

            if (ddlTableNo.SelectedIndex == 0)
                TableNo = "%%";
            else
                TableNo = ddlTableNo.SelectedValue;

            if (ddlCenter.SelectedIndex == 0)
                Center = "%%";
            else
                Center = ddlCenter.SelectedValue;

            if (ddlSession.SelectedIndex == 0)
                Session = "%%";
            else
                Session = ddlSession.SelectedValue;

            DataSet dsGrid = ProductController.Get_Seminar_StudentList_Counterwise("6", TableNo, Center, Session);
            dlSeminarStudent.DataSource = dsGrid.Tables[0];
            dlSeminarStudent.DataBind();

            lblStudentCount.Text = dsGrid.Tables[0].Rows.Count.ToString();

            divAdddNewStud.Visible = false;
            divResult.Visible = true;            
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }



    protected void chkAllStudent_CheckedChanged(object sender, EventArgs e)
    {
        //CheckBox chkitemck = (CheckBox)e.FindControl("chkCenter");
        CheckBox s = sender as CheckBox;
        foreach (DataListItem dtlItem in dlSeminarStudent.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            chkStudent.Checked = s.Checked;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        divAdddNewStud.Visible = true;
        divResult.Visible = false;
    }


    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        try
        {
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/QRCode"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/QRCode"));

            int TotalStud = 0;
            foreach (DataListItem dtlItem in dlSeminarStudent.Items)
            {
                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                if (chkStudent.Checked == true)
                {
                    TotalStud = TotalStud + 1;
                }
            }

            if (TotalStud == 0)
            {
                Show_Error_Success_Box("E", "Select atleast one student");
                return;
            }

            //Create PDF
            // Create a Document object
            dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

            // Create a new PdfWriter object, specifying the output stream
            dynamic output = new MemoryStream();
            dynamic writer = PdfWriter.GetInstance(document, output);

            dynamic TitleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
            dynamic subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            dynamic boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            dynamic bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

            // Open the Document for writing
            document.Open();

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            PdfContentByte cb = writer.DirectContent;
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            float YPos = 800, YNextPos = YPos - 20, Col0XPos = 13, Col01XPos = 40, Col1XPos = 290, Col2XPos = 370, Col3XPos = 520, Col4XPos = 580;
            string code = "";

            cb.BeginText();

            cb.SetFontAndSize(bf, 10);
            //cb.SetTextMatrix(Col0XPos + 2, YPos - 5);            
            cb.SetTextMatrix((Col0XPos + ((Col01XPos - Col0XPos) / 2) - (cb.GetEffectiveStringWidth("Table", false) / 2)), YPos - 5);//77//302                       
            cb.ShowText("Table");
            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

            //cb.SetTextMatrix(Col01XPos + 2, YPos - 5);
            cb.SetTextMatrix((Col01XPos + ((Col1XPos - Col01XPos) / 2) - (cb.GetEffectiveStringWidth("Student", false) / 2)), YPos - 5);//77//302                       
            //cb.SetTextMatrix(((Col0XPos) + ((Col1XPos - (Col0XPos)) / 2) - ((cb.GetEffectiveStringWidth("Student", false)) / 2)), YPos);//115            
            cb.SetFontAndSize(bf, 10);
            cb.ShowText("Student");
            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

           // cb.SetTextMatrix(Col1XPos + 2, YPos - 5);
            cb.SetTextMatrix((Col1XPos + ((Col2XPos - Col1XPos) / 2) - (cb.GetEffectiveStringWidth("UID", false) / 2)), YPos - 5);//77//302                       
            //cb.SetTextMatrix(((Col1XPos) + ((Col2XPos - (Col1XPos)) / 2) - ((cb.GetEffectiveStringWidth("UID", false)) / 2)), YPos);//115
            cb.SetFontAndSize(bf, 10);
            cb.ShowText("UID");
            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

           // cb.SetTextMatrix(Col2XPos + 2, YPos - 5);
            cb.SetTextMatrix((Col2XPos + ((Col3XPos - Col2XPos) / 2) - (cb.GetEffectiveStringWidth("QRCode", false) / 2)), YPos - 5);//77//302                       
            //cb.SetTextMatrix(((Col2XPos) + ((Col3XPos - (Col2XPos)) / 2) - ((cb.GetEffectiveStringWidth("QRCode", false)) / 2)), YPos);//115
            cb.SetFontAndSize(bf, 10);
            cb.ShowText("QRCode");
            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

            cb.SetTextMatrix((Col3XPos + ((Col4XPos - Col3XPos) / 2) - (cb.GetEffectiveStringWidth("Attendance", false) / 2)), YPos - 5);//77//302                                   
            cb.SetFontAndSize(bf, 10);
            cb.ShowText("Attendance");

            cb.EndText();

            YNextPos = YPos - 20;

            //Student Top line
            cb.MoveTo(Col0XPos, YPos + 15);
            cb.LineTo(Col4XPos, YPos + 15);
            cb.Stroke();
            //Student Bottom line
            cb.MoveTo(Col0XPos, YNextPos);
            cb.LineTo(Col4XPos, YNextPos);
            cb.Stroke();
            //Table No Right line
            cb.MoveTo(Col01XPos, YPos + 15);
            cb.LineTo(Col01XPos, YNextPos);
            cb.Stroke();
            //Student left line
            cb.MoveTo(Col0XPos, YPos + 15);
            cb.LineTo(Col0XPos, YNextPos);
            cb.Stroke();
            //Student Right line
            cb.MoveTo(Col1XPos, YPos + 15);
            cb.LineTo(Col1XPos, YNextPos);
            cb.Stroke();
            
            //UID Right line
            cb.MoveTo(Col2XPos, YPos + 15);
            cb.LineTo(Col2XPos, YNextPos);
            cb.Stroke();
            //QRCode left line
            cb.MoveTo(Col3XPos, YPos + 15);
            cb.LineTo(Col3XPos, YNextPos);
            cb.Stroke();
            //Attendance left line
            cb.MoveTo(Col4XPos, YPos + 15);
            cb.LineTo(Col4XPos, YNextPos);
            cb.Stroke();

            YPos = YPos + 60;

            foreach (DataListItem dtlItem in dlSeminarStudent.Items)
            {
                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                if (chkStudent.Checked == true)
                {
                    YPos = YPos - 90;

                    Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                    Label lblMobileNo = (Label)dtlItem.FindControl("lblMobileNo");
                    Label lblUID = (Label)dtlItem.FindControl("lblUID");
                    Label lblCounterNo = (Label)dtlItem.FindControl("lblCounterNo");
                    Label lblAttendance = (Label)dtlItem.FindControl("lblAttendance");
                    
                    if (YPos < 100)
                    {
                        document.NewPage();

                        YPos = 800;
                        cb.BeginText();

                        cb.SetFontAndSize(bf, 10);
                        //cb.SetTextMatrix(Col0XPos + 2, YPos - 5);
                        cb.SetTextMatrix((Col0XPos + ((Col01XPos - Col0XPos) / 2) - (cb.GetEffectiveStringWidth("Table", false) / 2)), YPos - 5);//77//302                       
                        //cb.SetTextMatrix((Col0XPos + ((Col01XPos - Col0XPos) / 2) - (cb.GetEffectiveStringWidth("Table", false) / 2)), YPos);
                        cb.ShowText("Table");

                        //cb.SetTextMatrix(Col01XPos + 2, YPos - 5);
                        cb.SetTextMatrix((Col01XPos + ((Col1XPos - Col01XPos) / 2) - (cb.GetEffectiveStringWidth("Student", false) / 2)), YPos - 5);//77//302                       
                        //cb.SetTextMatrix((Col0XPos + ((Col1XPos - Col0XPos) / 2) - (cb.GetEffectiveStringWidth("Student", false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Student");

                        //cb.SetTextMatrix(Col1XPos + 2, YPos - 5);
                        cb.SetTextMatrix((Col1XPos + ((Col2XPos - Col1XPos) / 2) - (cb.GetEffectiveStringWidth("UID", false) / 2)), YPos - 5);//77//302                       
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("UID");

                        //cb.SetTextMatrix(Col2XPos + 2, YPos - 5);
                        cb.SetTextMatrix((Col2XPos + ((Col3XPos - Col2XPos) / 2) - (cb.GetEffectiveStringWidth("QRCode", false) / 2)), YPos - 5);//77//302                       
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("QRCode");

                        cb.SetTextMatrix((Col3XPos + ((Col4XPos - Col3XPos) / 2) - (cb.GetEffectiveStringWidth("Attendance", false) / 2)), YPos - 5);//77//302                       
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Attendance");

                        cb.EndText();

                        YNextPos = YPos - 20;

                        //Student Top line
                        cb.MoveTo(Col0XPos, YPos + 15);
                        cb.LineTo(Col4XPos, YPos + 15);
                        cb.Stroke();
                        //Student Bottom line
                        cb.MoveTo(Col0XPos, YNextPos);
                        cb.LineTo(Col4XPos, YNextPos);
                        cb.Stroke();
                        //Table Right line
                        cb.MoveTo(Col01XPos, YPos + 15);
                        cb.LineTo(Col01XPos, YNextPos);
                        cb.Stroke();
                        //Student left line
                        cb.MoveTo(Col0XPos, YPos + 15);
                        cb.LineTo(Col0XPos, YNextPos);
                        cb.Stroke();
                        //Student Right line
                        cb.MoveTo(Col1XPos, YPos + 15);
                        cb.LineTo(Col1XPos, YNextPos);
                        cb.Stroke();
                        //UID Right line
                        cb.MoveTo(Col2XPos, YPos + 15);
                        cb.LineTo(Col2XPos, YNextPos);
                        cb.Stroke();
                        //QRCode left line
                        cb.MoveTo(Col3XPos, YPos + 15);
                        cb.LineTo(Col3XPos, YNextPos);
                        cb.Stroke();
                        //Attendance left line
                        cb.MoveTo(Col4XPos, YPos + 15);
                        cb.LineTo(Col4XPos, YNextPos);
                        cb.Stroke();

                        YPos = YPos - 30;
                    }



                    cb.BeginText();

                    //cb.SetTextMatrix(Col0XPos + 4, YPos - 45);
                    cb.SetTextMatrix((Col0XPos + ((Col01XPos - Col0XPos) / 2) - (cb.GetEffectiveStringWidth(lblCounterNo.Text, false) / 2)), YPos - 45);//77//302                       
                    //cb.SetTextMatrix(((Col0XPos) + ((Col1XPos - (Col0XPos)) / 2) - ((cb.GetEffectiveStringWidth(lblStudentName.Text, false)) / 2)), YPos - 45);//115                    
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblCounterNo.Text);

                    cb.SetTextMatrix(Col01XPos + 4, YPos - 35);
                    //cb.SetTextMatrix(((Col0XPos) + ((Col1XPos - (Col0XPos)) / 2) - ((cb.GetEffectiveStringWidth(lblStudentName.Text, false)) / 2)), YPos - 45);//115                    
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblStudentName.Text);
                    //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    cb.SetTextMatrix(Col01XPos + 4, YPos - 50);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblMobileNo.Text);
                    
                    //cb.SetTextMatrix(Col1XPos + 2, YPos - 45);
                    cb.SetTextMatrix(((Col1XPos) + ((Col2XPos - (Col1XPos)) / 2) - ((cb.GetEffectiveStringWidth(lblUID.Text, false)) / 2)), YPos - 45);//115
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblUID.Text);        
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    cb.SetTextMatrix(((Col3XPos) + ((Col4XPos - (Col3XPos)) / 2) - ((cb.GetEffectiveStringWidth(lblAttendance.Text, false)) / 2)), YPos - 45);//115
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblAttendance.Text);

                    cb.EndText();
                    YNextPos = YPos - 80;

                    code = lblUID.Text;

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
                    using (System.Drawing.Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();
                            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        }
                        // plBarCode.Controls.Add(imgBarCode);

                        bitMap.Save(Server.MapPath("~/QRCode/" + lblUID.Text + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                        dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/QRCode/" + lblUID.Text + ".jpg"));
                        //dynamic logo = imgBarCode;
                        logo.SetAbsolutePosition(Col2XPos, YNextPos - 5);
                        logo.ScaleToFit(200f, 95f);
                        logo.ScaleAbsolute(150, 100);
                        //logo.ScalePercent(35);
                        document.Add(logo);

                        //cb.BeginText();
                        //cb.SetTextMatrix(Col2XPos + 90, YNextPos + 5);
                        //cb.SetFontAndSize(bf, 10);
                        //cb.ShowText(lblUID.Text);
                        //cb.EndText();
                    }


                    //var reader = new BarcodeReader();
                    ////Saving the uploaded image and reading from it var fileName =
                    //Path.Combine(Request.MapPath("~/Imgs"), "QRImage.jpg");
                    //fileUpload.SaveAs(fileName);
                    //var result = reader.Decode(new Bitmap(fileName));
                    //Response.Write(result.Text);
                                        
                    //Student Bottom line
                    cb.MoveTo(Col0XPos, YNextPos);
                    cb.LineTo(Col4XPos, YNextPos);
                    cb.Stroke();
                    //Table Right line
                    cb.MoveTo(Col01XPos, YPos + 15);
                    cb.LineTo(Col01XPos, YNextPos);
                    cb.Stroke();
                    //Student left line
                    cb.MoveTo(Col0XPos, YPos + 15);
                    cb.LineTo(Col0XPos, YNextPos);
                    cb.Stroke();
                    //Student Right line
                    cb.MoveTo(Col1XPos, YPos + 15);
                    cb.LineTo(Col1XPos, YNextPos);
                    cb.Stroke();
                    //UID Right line
                    cb.MoveTo(Col2XPos, YPos + 15);
                    cb.LineTo(Col2XPos, YNextPos);
                    cb.Stroke();
                    //QRCode left line
                    cb.MoveTo(Col3XPos, YPos + 15);
                    cb.LineTo(Col3XPos, YNextPos);
                    cb.Stroke();
                    //Attendance left line
                    cb.MoveTo(Col4XPos, YPos + 15);
                    cb.LineTo(Col4XPos, YNextPos);
                    cb.Stroke();
                }
            }

            document.Close();
            string CurTimeFrame = null;
            CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=GenerateQRCode{0}.pdf", CurTimeFrame));
            Response.BinaryWrite(output.ToArray());

            Show_Error_Success_Box("S", "PDF File generated successfully.");
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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


    private void FillDDL_TableNo()
    {
        try
        {
            Clear_Error_Success_Box();
            // string UserID = txtUserId.Text.Trim();
            DataSet dsTableNo = ProductController.Get_Seminar_TableNo();
            BindDDL(ddlTableNo, dsTableNo, "TableNo", "TableNo");
            ddlTableNo.Items.Insert(0, "Select");
            ddlTableNo.SelectedIndex = 0;
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

    private void FillDDL_Center()
    {
        try
        {
            Clear_Error_Success_Box();
            // string UserID = txtUserId.Text.Trim();
            DataSet dsCenter = ProductController.Get_Seminar_Center_Session("1");
            BindDDL(ddlCenter, dsCenter, "Center", "CenterCode");
            ddlCenter.Items.Insert(0, "Select");
            ddlCenter.SelectedIndex = 0;
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

    private void FillDDL_Session()
    {
        try
        {
            Clear_Error_Success_Box();
            // string UserID = txtUserId.Text.Trim();
            DataSet dsSession= ProductController.Get_Seminar_Center_Session("2");
            BindDDL(ddlSession, dsSession, "Session", "Session");
            ddlSession.Items.Insert(0, "Select");
            ddlSession.SelectedIndex = 0;
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

    
    #endregion






    
}
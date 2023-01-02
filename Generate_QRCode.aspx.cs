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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic;
using QRCoder;
//using System.Drawing;

public partial class Generate_QRCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewStudentList();
        }
    }

#region Event

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


    protected void BtnGenerateQRCode_Click(object sender, EventArgs e)
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

            float YPos = 800, YNextPos = YPos - 20, Col0XPos = 10, Col1XPos = 260, Col2XPos = 360, Col3XPos = 580;
            string code = "";

            cb.BeginText();

            cb.SetTextMatrix(Col0XPos + 2, YPos);
            //cb.SetTextMatrix(((Col0XPos) + ((Col1XPos - (Col0XPos)) / 2) - ((cb.GetEffectiveStringWidth("Student", false)) / 2)), YPos);//115            
            cb.SetFontAndSize(bf,10);
            cb.ShowText("Student");
            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

            cb.SetTextMatrix(Col1XPos + 2, YPos);
            //cb.SetTextMatrix(((Col1XPos) + ((Col2XPos - (Col1XPos)) / 2) - ((cb.GetEffectiveStringWidth("UID", false)) / 2)), YPos);//115
            cb.SetFontAndSize(bf, 10);
            cb.ShowText("UID");
            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

            cb.SetTextMatrix(Col2XPos + 2, YPos);
            //cb.SetTextMatrix(((Col2XPos) + ((Col3XPos - (Col2XPos)) / 2) - ((cb.GetEffectiveStringWidth("QRCode", false)) / 2)), YPos);//115
            cb.SetFontAndSize(bf, 10);
            cb.ShowText("QRCode");
            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

            cb.EndText();

            YNextPos = YPos - 20;

            //Student Top line
            cb.MoveTo(Col0XPos, YPos + 15);
            cb.LineTo(Col3XPos, YPos + 15);
            cb.Stroke();
            //Student Bottom line
            cb.MoveTo(Col0XPos, YNextPos);
            cb.LineTo(Col3XPos, YNextPos);
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

            YPos = YPos + 60;

            foreach (DataListItem dtlItem in dlSeminarStudent.Items)
            {
                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                if (chkStudent.Checked == true)
                {
                    YPos = YPos - 90;

                    Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                    Label lblUID = (Label)dtlItem.FindControl("lblUID");
                                     

                    if (YPos < 100)
                    {
                        document.NewPage();

                        YPos = 800;
                        cb.BeginText();

                        cb.SetTextMatrix(Col0XPos + 2, YPos);
                        //cb.SetTextMatrix((Col0XPos + ((Col1XPos - Col0XPos) / 2) - (cb.GetEffectiveStringWidth("Student", false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Student");

                        cb.SetTextMatrix(Col1XPos + 2, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("UID");

                        cb.SetTextMatrix(Col2XPos + 2, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("QRCode");

                        cb.EndText();

                        YNextPos = YPos - 20;

                        //Student Top line
                        cb.MoveTo(Col0XPos, YPos + 15);
                        cb.LineTo(Col3XPos, YPos + 15);
                        cb.Stroke();
                        //Student Bottom line
                        cb.MoveTo(Col0XPos, YNextPos);
                        cb.LineTo(Col3XPos, YNextPos);
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

                        YPos = YPos - 30;
                    }

                    

                    cb.BeginText();

                    cb.SetTextMatrix(Col0XPos + 4, YPos - 45);
                    //cb.SetTextMatrix(((Col0XPos) + ((Col1XPos - (Col0XPos)) / 2) - ((cb.GetEffectiveStringWidth(lblStudentName.Text, false)) / 2)), YPos - 45);//115                    
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblStudentName.Text);
                    //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    //cb.SetTextMatrix(Col1XPos + 2, YPos - 45);
                    cb.SetTextMatrix(((Col1XPos) + ((Col2XPos - (Col1XPos)) / 2) - ((cb.GetEffectiveStringWidth(lblUID.Text, false)) / 2)), YPos - 45);//115
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblUID.Text);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

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
                        logo.SetAbsolutePosition(Col2XPos + 70, YNextPos + 10);
                        logo.ScaleToFit(200f, 95f);
                        logo.ScaleAbsolute(80, 82);
                        //logo.ScalePercent(35);
                        document.Add(logo);

                        cb.BeginText();
                        cb.SetTextMatrix(Col2XPos + 90, YNextPos + 5);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblUID.Text);
                        cb.EndText();
                    }


                    //var reader = new BarcodeReader();
                    ////Saving the uploaded image and reading from it var fileName =
                    //Path.Combine(Request.MapPath("~/Imgs"), "QRImage.jpg");
                    //fileUpload.SaveAs(fileName);
                    //var result = reader.Decode(new Bitmap(fileName));
                    //Response.Write(result.Text);

                    
                    //Student Bottom line
                    cb.MoveTo(Col0XPos, YNextPos);
                    cb.LineTo(Col3XPos, YNextPos);
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
        catch(Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void btnPrintICard_Click(object sender, EventArgs e)
    {
        Print_ICard();
    }

    protected void btnIndivisualICard_Click(object sender, EventArgs e)
    {
        Print_ICard_Indivisual();
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

    private void ViewStudentList()
    {
        try
        {
            DataSet dsGrid = ProductController.Get_Seminar_StudentList("2");
            dlSeminarStudent.DataSource = dsGrid.Tables[0];
            dlSeminarStudent.DataBind();

            lblStudentCount.Text = dsGrid.Tables[0].Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }


    private void Print_ICard()
    {
        {
            Clear_Error_Success_Box();
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/QRCode"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/QRCode"));

            try
            {
                int TotalStud = 0;
                List<string> list = new List<string>();

                foreach (DataListItem dtlItem in dlSeminarStudent.Items)
                {
                    CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                    if (chkStudent.Checked == true)
                    {
                        Label lblUID = (Label)dtlItem.FindControl("lblUID");
                        list.Add(lblUID.Text);
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
                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont bf1 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false);
                BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                BaseColor LightBlue = new BaseColor(218, 238, 243);
                BaseColor DarkBlue = new BaseColor(31, 73, 125);

                PdfContentByte cb = writer.DirectContent;

                float YPos = 0;

                int mod = TotalStud % 4;
                int TotalPage = TotalStud / 4;
                if (mod > 0)
                {
                    TotalPage++;
                }

                int ActualStud = 0;
                for (int i = 0; i < TotalPage; i++)
                {
                    YPos = 800;

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/IDCard_Background.jpg"));

                    jpg.ScaleToFit(980, 810);//980, 580
                    jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    jpg.SetAbsolutePosition(-25, 5);
                    document.Add(jpg);
                    
                    for(int j = 0; j < 4; j++)
                    {
                        if (ActualStud != TotalStud)
                        {
                            string ActualUID = list[ActualStud] as string;
                            string StudentName = "", Center = "", Board = "", Rank = "", Percentile = "", YourGuide = "", CounterNo = "";

                            foreach (DataListItem dtlItem in dlSeminarStudent.Items)
                            {
                                Label lblUID = (Label)dtlItem.FindControl("lblUID");
                                if (lblUID.Text == ActualUID)
                                {
                                    Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                                    Label lblCenter = (Label)dtlItem.FindControl("lblCenter");
                                    Label lblBoard = (Label)dtlItem.FindControl("lblBoard");
                                    Label lblStudRank = (Label)dtlItem.FindControl("lblStudRank");
                                    Label lblPercentile = (Label)dtlItem.FindControl("lblPercentile");
                                    Label lblYourGuide = (Label)dtlItem.FindControl("lblYourGuide");
                                    Label lblCounterNo = (Label)dtlItem.FindControl("lblCounterNo");

                                    StudentName = lblStudentName.Text;
                                    Center = lblCenter.Text;
                                    Board = lblBoard.Text;
                                    Rank = lblStudRank.Text;
                                    Percentile = lblPercentile.Text;
                                    YourGuide = lblYourGuide.Text;
                                    CounterNo = lblCounterNo.Text;
                                    break;
                                }
                            }

                            QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(ActualUID, QRCodeGenerator.ECCLevel.Q);
                            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                            imgBarCode.Height = 150;
                            imgBarCode.Width = 150;

                            mod = j % 2;
                            if (mod == 0)
                            {
                                using (System.Drawing.Bitmap bitMap = qrCode.GetGraphic(20))
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                        byte[] byteImage = ms.ToArray();
                                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                    }
                                    // plBarCode.Controls.Add(imgBarCode);


                                    bitMap.Save(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                                    dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"));
                                    //dynamic logo = imgBarCode;
                                    //logo.SetAbsolutePosition(130, YPos - 230);
                                    logo.SetAbsolutePosition(100, YPos - 210);
                                    logo.ScaleToFit(200f, 95f);
                                    logo.ScaleAbsolute(125, 110);

                                    //logo.ScalePercent(35);
                                    document.Add(logo);
                                }

                                cb.BeginText();

                                cb.SetColorFill(BaseColor.WHITE);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix((47 + ((280 - 47) / 2) - (cb.GetEffectiveStringWidth("UID No. : " + ActualUID, false) / 2)), YPos - 235);//77//302
                                cb.ShowText("UID No. : " + ActualUID);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix((47 + ((280 - 47) / 2) - (cb.GetEffectiveStringWidth(Center, false) / 2)), YPos - 270);
                                cb.ShowText(Center);//Center
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.RED);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix((47 + ((272 - 47) / 2) - (cb.GetEffectiveStringWidth(StudentName, false) / 2)), YPos - 300);
                                cb.ShowText(StudentName);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix(110, YPos - 327);
                                cb.ShowText(YourGuide);//YourGuide
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix(145, YPos - 350);
                                cb.ShowText(CounterNo);//CounterNo
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.EndText();
                            }
                            else
                            {
                                using (System.Drawing.Bitmap bitMap = qrCode.GetGraphic(20))
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                        byte[] byteImage = ms.ToArray();
                                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                    }
                                    // plBarCode.Controls.Add(imgBarCode);


                                    bitMap.Save(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                                    dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"));
                                    //dynamic logo = imgBarCode;
                                    logo.SetAbsolutePosition(382, YPos - 210);
                                    logo.ScaleToFit(200f, 95f);
                                    logo.ScaleAbsolute(125, 110);

                                    //logo.ScalePercent(35);
                                    document.Add(logo);
                                }

                                cb.BeginText();//258

                                cb.SetColorFill(BaseColor.WHITE);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix((335 + ((550 - 335) / 2) - (cb.GetEffectiveStringWidth("UID No. : " + ActualUID, false) / 2)), YPos - 235);//77//302
                               // cb.SetTextMatrix(550, YPos - 235);//77//302
                                cb.ShowText("UID No. : " + ActualUID);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix((320 + ((565 - 320) / 2) - (cb.GetEffectiveStringWidth(Center, false) / 2)), YPos - 270);
                                cb.ShowText(Center);//Center
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.RED);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix((338 + ((563 - 338) / 2) - (cb.GetEffectiveStringWidth(StudentName, false) / 2)), YPos - 300);
                                cb.ShowText(StudentName);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix(390, YPos - 327);
                                cb.ShowText(YourGuide);//YourGuide
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                cb.SetTextMatrix(430, YPos - 350);
                                cb.ShowText(CounterNo);//CounterNo
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.EndText();

                                YPos = YPos - 403;
                            }
                                                        
                            ActualStud++;
                        }

                    }
                    document.NewPage();
                }


                document.Close();
                string CurTimeFrame = null;
                CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=SeminarICard{0}.pdf", CurTimeFrame));
                Response.BinaryWrite(output.ToArray());

                Show_Error_Success_Box("S", "PDF File generated successfully.");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }

        }
    }


    private void Print_ICard_Indivisual()
    {
        {
            Clear_Error_Success_Box();
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/QRCode"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/QRCode"));

            bool exists1 = System.IO.Directory.Exists(Server.MapPath("~/StudentICardPDF"));

            if (!exists1)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/StudentICardPDF"));
            
            try
            {
                int TotalStud = 0;
                List<string> list = new List<string>();

                foreach (DataListItem dtlItem in dlSeminarStudent.Items)
                {
                    CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                    if (chkStudent.Checked == true)
                    {
                        Label lblUID = (Label)dtlItem.FindControl("lblUID");
                        list.Add(lblUID.Text);
                        TotalStud = TotalStud + 1;
                    }
                }

                if (TotalStud == 0)
                {
                    Show_Error_Success_Box("E", "Select atleast one student");
                    return;
                }



                string ActualUID = "", SelectedUID = "", StudentName = "", Center = "", Board = "", Rank = "", Percentile = "", YourGuide = "", CounterNo = "";

               
                foreach (DataListItem dtlItem in dlSeminarStudent.Items)
                {
                    CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                    if (chkStudent.Checked == true)
                    {
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


                        Label lblUID = (Label)dtlItem.FindControl("lblUID");
                        Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                        Label lblCenter = (Label)dtlItem.FindControl("lblCenter");
                        Label lblBoard = (Label)dtlItem.FindControl("lblBoard");
                        Label lblStudRank = (Label)dtlItem.FindControl("lblStudRank");
                        Label lblPercentile = (Label)dtlItem.FindControl("lblPercentile");
                        Label lblYourGuide = (Label)dtlItem.FindControl("lblYourGuide");
                        Label lblCounterNo = (Label)dtlItem.FindControl("lblCounterNo");
                        
                        ActualUID = lblUID.Text;
                        StudentName = lblStudentName.Text;
                        Center = lblCenter.Text;
                        Board = lblBoard.Text;
                        Rank = lblStudRank.Text;
                        Percentile = lblPercentile.Text;
                        YourGuide = lblYourGuide.Text;
                        CounterNo = lblCounterNo.Text;

                        

                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        BaseFont bf1 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false);
                        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                        BaseColor LightBlue = new BaseColor(218, 238, 243);
                        BaseColor DarkBlue = new BaseColor(31, 73, 125);

                        PdfContentByte cb = writer.DirectContent;

                        float YPos = 0;


                        YPos = 800;

                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Indivisual_IDCard_Background.jpg"));

                        jpg.ScaleToFit(980, 810);//980, 580
                        jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                        jpg.SetAbsolutePosition(-5, 5);
                        document.Add(jpg);

                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(ActualUID, QRCodeGenerator.ECCLevel.Q);
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


                            bitMap.Save(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                            dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"));
                            //dynamic logo = imgBarCode;
                            logo.SetAbsolutePosition(150, YPos - 450);
                            logo.ScaleToFit(200f, 95f);
                            logo.ScaleAbsolute(265, 240);

                            //logo.ScalePercent(35);
                            document.Add(logo);
                        }


                        cb.BeginText();

                        cb.SetColorFill(BaseColor.WHITE);
                        cb.SetFontAndSize(f_cn, 35);
                        cb.SetTextMatrix((70 + ((490 - 70) / 2) - (cb.GetEffectiveStringWidth("UID No. : " + ActualUID, false) / 2)), YPos - 490);//77//302                       
                        cb.ShowText("UID No. : " + ActualUID);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.SetColorFill(BaseColor.BLACK);
                        cb.SetFontAndSize(f_cn, 30);
                        cb.SetTextMatrix((70 + ((490 - 70) / 2) - (cb.GetEffectiveStringWidth(Center, false) / 2)), YPos - 550);
                        cb.ShowText(Center);//Center
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.SetColorFill(BaseColor.RED);
                        cb.SetFontAndSize(f_cn, 20);
                        cb.SetTextMatrix((70 + ((490 - 70) / 2) - (cb.GetEffectiveStringWidth(StudentName, false) / 2)), YPos - 610);
                        cb.ShowText(StudentName);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                        cb.SetColorFill(BaseColor.BLACK);
                        cb.SetFontAndSize(f_cn, 20);
                        cb.SetTextMatrix(170, YPos - 662);
                        cb.ShowText(YourGuide);//YourGuide
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.SetColorFill(BaseColor.BLACK);
                        cb.SetFontAndSize(f_cn, 25);
                        cb.SetTextMatrix(255, YPos - 709);
                        cb.ShowText(CounterNo);//CounterNo
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.EndText();

                        document.Close();

                        string FullName = System.Web.HttpContext.Current.Server.MapPath("~/StudentICardPDF/" + ActualUID + ".pdf");// System.Web.HttpContext.Current.Server.MapPath("~/StudentICardPDF/" + "1.pdf"); ;// "localhost:2272\\MTEL_SEMINAR_2017\\StudentICardPDF\\1.PDF";//Server.MapPath("~/StudentICardPDF") + "\\" + "1.PDF";
                        string filepath = FullName;// System.Web.HttpContext.Current.Server.MapPath(FullName);

                        SelectedUID = SelectedUID + ActualUID + ",";

                        File.WriteAllBytes(filepath, output.ToArray());


                    }
                }

                string url = Request.Url.Authority + HttpContext.Current.Request.RawUrl.ToString();

                if (Request.ServerVariables["HTTPS"] == "on")
                {
                    url = "https://" + url;
                }
                else
                {
                    url = "http://" + url;
                }
                
                string Path1 = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo Info = new System.IO.FileInfo(Path1);
                string pageName = Info.Name;

                url = url.Replace(pageName, "") + "StudentICardPDF/";

                DataSet dsGrid = ProductController.Save_Seminar_Student_ICard_Link("1",url,SelectedUID);

                Show_Error_Success_Box("S", "PDF File generated successfully.");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }

        }
    }

    private void Print_ICard_Old()
    {
        {
            Clear_Error_Success_Box();
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/QRCode"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/QRCode"));

            try
            {
                int TotalStud = 0;
                List<string> list = new List<string>();

                foreach (DataListItem dtlItem in dlSeminarStudent.Items)
                {
                    CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                    if (chkStudent.Checked == true)
                    {
                        Label lblUID = (Label)dtlItem.FindControl("lblUID");
                        list.Add(lblUID.Text);
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
                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont bf1 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false);

                BaseColor LightBlue = new BaseColor(218, 238, 243);
                BaseColor DarkBlue = new BaseColor(31, 73, 125);

                PdfContentByte cb = writer.DirectContent;

                float YPos = 0;

                int mod = TotalStud % 8;
                int TotalPage = TotalStud / 8;
                if (mod > 0)
                {
                    TotalPage++;
                }

                int ActualStud = 0;
                for (int i = 0; i < TotalPage; i++)
                {
                    YPos = 800;

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/SeminarICard.jpg"));

                    jpg.ScaleToFit(980, 810);//980, 580
                    jpg.Alignment = iTextSharp.text.Image.UNDERLYING;
                    jpg.SetAbsolutePosition(5, 10);
                    document.Add(jpg);


                    for (int j = 0; j < 8; j++)
                    {
                        if (ActualStud != TotalStud)
                        {
                            string ActualUID = list[ActualStud] as string;
                            string StudentName = "", Center = "", Board = "", Rank = "", Percentile = "", YourGuide = "", CounterNo = "";

                            foreach (DataListItem dtlItem in dlSeminarStudent.Items)
                            {
                                Label lblUID = (Label)dtlItem.FindControl("lblUID");
                                if (lblUID.Text == ActualUID)
                                {
                                    Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                                    Label lblCenter = (Label)dtlItem.FindControl("lblCenter");
                                    Label lblBoard = (Label)dtlItem.FindControl("lblBoard");
                                    Label lblStudRank = (Label)dtlItem.FindControl("lblStudRank");
                                    Label lblPercentile = (Label)dtlItem.FindControl("lblPercentile");
                                    Label lblYourGuide = (Label)dtlItem.FindControl("lblYourGuide");
                                    Label lblCounterNo = (Label)dtlItem.FindControl("lblCounterNo");
                                    StudentName = lblStudentName.Text;
                                    Center = lblCenter.Text;
                                    Board = lblBoard.Text;
                                    Rank = lblStudRank.Text;
                                    Percentile = lblPercentile.Text;
                                    YourGuide = lblYourGuide.Text;
                                    CounterNo = lblCounterNo.Text;
                                    break;
                                }
                            }

                            QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(ActualUID, QRCodeGenerator.ECCLevel.Q);
                            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                            imgBarCode.Height = 150;
                            imgBarCode.Width = 150;

                            //qrCode.BackgroundColor = System.Drawing.Color.White;
                            //qrCode.ModuleColor = System.Drawing.Color.Blue;


                            mod = j % 2;
                            if (mod == 0)
                            {
                                cb.BeginText();

                                cb.SetColorFill(DarkBlue);
                                //cb.SetTextMatrix(15, YPos - 52);//280
                                cb.SetFontAndSize(bfTimes, 13);
                                cb.SetTextMatrix((15 + ((280 - 15) / 2) - (cb.GetEffectiveStringWidth(StudentName, false) / 2)), YPos - 52);
                                cb.ShowText(StudentName);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(60, YPos - 79);
                                cb.ShowText(Center);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(245, YPos - 79);
                                cb.ShowText(Board);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(60, YPos - 100);
                                cb.ShowText(Rank);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(245, YPos - 100);
                                cb.ShowText(Percentile);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.RED);
                                cb.SetFontAndSize(bfTimes, 9);
                                cb.SetTextMatrix(76, YPos - 121);
                                cb.ShowText(YourGuide);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(76, YPos - 142);
                                cb.ShowText(CounterNo);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.WHITE);
                                cb.SetFontAndSize(bf1, 13);
                                cb.SetTextMatrix(140, YPos - 165);
                                cb.ShowText(ActualUID);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.EndText();

                                using (System.Drawing.Bitmap bitMap = qrCode.GetGraphic(20))
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                        byte[] byteImage = ms.ToArray();
                                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                    }
                                    // plBarCode.Controls.Add(imgBarCode);


                                    bitMap.Save(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                                    dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"));
                                    //dynamic logo = imgBarCode;
                                    logo.SetAbsolutePosition(200, YPos - 152);
                                    logo.ScaleToFit(200f, 95f);
                                    logo.ScaleAbsolute(80, 50);

                                    //logo.ScalePercent(35);
                                    document.Add(logo);

                                    cb.SetColorFill(LightBlue);
                                    cb.MoveTo(200, YPos - 101);
                                    cb.LineTo(280, YPos - 101);
                                    cb.LineTo(280, YPos - 108);
                                    cb.LineTo(200, YPos - 108);
                                    cb.Fill();

                                    cb.MoveTo(200, YPos - 101);
                                    cb.LineTo(210, YPos - 101);
                                    cb.LineTo(210, YPos - 152);
                                    cb.LineTo(200, YPos - 152);
                                    cb.Fill();

                                    cb.SetColorFill(LightBlue);
                                    cb.MoveTo(200, YPos - 145);
                                    cb.LineTo(280, YPos - 145);
                                    cb.LineTo(280, YPos - 152);
                                    cb.LineTo(200, YPos - 152);
                                    cb.Fill();

                                    cb.MoveTo(270, YPos - 101);
                                    cb.LineTo(280, YPos - 101);
                                    cb.LineTo(280, YPos - 152);
                                    cb.LineTo(270, YPos - 152);
                                    cb.Fill();
                                }

                            }
                            else if (mod > 0)
                            {
                                cb.BeginText();

                                cb.SetColorFill(DarkBlue);
                                //cb.SetTextMatrix(305, YPos - 52);
                                cb.SetFontAndSize(bfTimes, 13);
                                cb.SetTextMatrix((305 + ((570 - 305) / 2) - ((cb.GetEffectiveStringWidth(StudentName, false)) / 2)), YPos - 52);//115                            
                                cb.ShowText(StudentName);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(345, YPos - 79);
                                cb.ShowText(Center);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(533, YPos - 79);
                                cb.ShowText(Board);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(345, YPos - 100);
                                cb.ShowText(Rank);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(533, YPos - 100);
                                cb.ShowText(Percentile);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.RED);
                                cb.SetFontAndSize(bfTimes, 9);
                                cb.SetTextMatrix(365, YPos - 121);
                                cb.ShowText(YourGuide);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(bf, 9);
                                cb.SetTextMatrix(365, YPos - 142);
                                cb.ShowText(CounterNo);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.WHITE);
                                cb.SetTextMatrix(430, YPos - 165);
                                cb.SetFontAndSize(bf1, 13);
                                cb.ShowText(ActualUID);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                                cb.EndText();

                                using (System.Drawing.Bitmap bitMap = qrCode.GetGraphic(20))
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                        byte[] byteImage = ms.ToArray();
                                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                    }
                                    // plBarCode.Controls.Add(imgBarCode);

                                    bitMap.Save(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                                    dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/QRCode/" + ActualUID + ".jpg"));
                                    //dynamic logo = imgBarCode;
                                    logo.SetAbsolutePosition(488, YPos - 152);
                                    logo.ScaleToFit(200f, 95f);
                                    logo.ScaleAbsolute(80, 50);
                                    //logo.ScalePercent(35);
                                    document.Add(logo);

                                    cb.SetColorFill(LightBlue);
                                    cb.MoveTo(488, YPos - 101);
                                    cb.LineTo(568, YPos - 101);
                                    cb.LineTo(568, YPos - 108);
                                    cb.LineTo(488, YPos - 108);
                                    cb.Fill();

                                    cb.MoveTo(488, YPos - 101);
                                    cb.LineTo(498, YPos - 101);
                                    cb.LineTo(498, YPos - 152);
                                    cb.LineTo(488, YPos - 152);
                                    cb.Fill();

                                    cb.SetColorFill(LightBlue);
                                    cb.MoveTo(488, YPos - 145);
                                    cb.LineTo(568, YPos - 145);
                                    cb.LineTo(568, YPos - 152);
                                    cb.LineTo(488, YPos - 152);
                                    cb.Fill();

                                    cb.MoveTo(558, YPos - 101);
                                    cb.LineTo(568, YPos - 101);
                                    cb.LineTo(568, YPos - 152);
                                    cb.LineTo(558, YPos - 152);
                                    cb.Fill();
                                }



                                YPos = YPos - 202;
                            }

                            ActualStud++;
                        }

                    }
                    document.NewPage();
                }


                document.Close();
                string CurTimeFrame = null;
                CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=SeminarICard{0}.pdf", CurTimeFrame));
                Response.BinaryWrite(output.ToArray());

                Show_Error_Success_Box("S", "PDF File generated successfully.");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }

        }
    }


    #endregion






    
}
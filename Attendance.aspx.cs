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
using System.Text;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;


using com.google.zxing.qrcode;
using com.google.zxing;
using com.google.zxing.common;

public partial class Attendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDivHead.Text = "Please Fill Following Information";
            //FillDDL_Division();
            //FillDDL_CurrentSScCenter();

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (cookie == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];
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
            BtnSaveAttendance.Visible = true;
            BtnClose.Visible = true;
            lblStudentName.Text = "";         
            lblParentName.Text = "";
            lblContactNumber.Text = "";
           

            DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(),"3");
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
                    //commited on 18082018
                    //string Message = "Dear Student/Parent,\nWelcome to the Lakshya Seminar.\nYour reference UID is : " + txtUID.Text.Trim() + ".\nPlease refer to this UID for any further communication at the venue. Click this link to download the Ebrochure of Lakshya.\nhttp://bit.ly/LakshyaBrochure\nGo ahead and empower yourself today to make an informed decision. Good Day!\nWarm Regards\nMT Lakshya";
                    string Message = "Dear Student/Parent,\nWelcome to the Lakshya Seminar.\nYour reference UID is : " + txtUID.Text.Trim() + ".\nPlease refer to this UID for any further communication at the venue. Good Day!\nWarm Regards\nMT Lakshya";
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
        tblSearchInfo.Visible = false;
        tblSearchDetail.Visible = true;
        BtnSaveAttendance.Visible = false;
        BtnClose.Visible = false;
        txtSearchUID.Text = "";
    }

    
    protected void btnReadQRCode_Click(object sender, EventArgs e)
    {
        try
        {
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/Images/UnCompressed_Images"));
            bool exists1 = System.IO.Directory.Exists(Server.MapPath("~/Images/QRCode_Images"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/UnCompressed_Images"));            

            if (!exists1)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/QRCode_Images"));

            if (Page.Request.Files[0].FileName != "")
            {
                string Student_id = RandomString(6) + ".bmp";
                Request.Files[0].SaveAs(Server.MapPath("~/Images/UnCompressed_Images/" + Student_id));

                Bitmap bitmap = new Bitmap(Server.MapPath("~/Images/UnCompressed_Images/" + Student_id));

               // txtSearchUID.Text = Process(bitmap);

                QRCodeDecoder dec = new QRCodeDecoder();
                txtSearchUID.Text = (dec.decode(new QRCodeBitmapImage(bitmap as Bitmap)));
                // string UnCompressed_ImagePath = "~/Images/UnCompressed_Images/" + Student_id;
               // string QRCode_ImagePath = "~/Images/QRCode_Images/" + Student_id;

               // VaryQualityLevel(UnCompressed_ImagePath, QRCode_ImagePath);

               // string APIEncode = "http://api.qrserver.com/v1/read-qr-code/?fileurl=http%3A%2F%2Foe.mteducare.com%2FSeminar%2FImages%2FQRCode_Images%2F" + Student_id;

               //// GetAPI_Json("http://api.qrserver.com/v1/read-qr-code/?fileurl=http%3A%2F%2Foe.mteducare.com%2FQRCODE_READER%2FQRCODE%2Fabc.jpg");
               // GetAPI_Json(APIEncode);
            }
            else
            {
                Show_Error_Success_Box("E","Image not found.");
                return;
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

    public string Process(Bitmap bitmap)
    {
        try
        {
            QRCodeReader reader = new QRCodeReader();
            com.google.zxing.LuminanceSource source = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
            var binarizer = new HybridBinarizer(source);
            var binBitmap = new BinaryBitmap(binarizer);
            return reader.decode(binBitmap).Text;
        }
        catch(Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return string.Empty;
        }
    }

    //public Bitmap ConvertToBitmap(string fileName)
    //{
    //    Bitmap bitmap;
    //    using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
    //    {
    //        Image image = Image.FromStream(bmpStream);

    //        bitmap = new Bitmap(image);

    //    }
    //    return bitmap;
    //}


    public static string RandomString(int length)
    {
        char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        data = new byte[length];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(length);
        foreach (byte b in data)
        { result.Append(chars[b % (chars.Length - 1)]); }
        return result.ToString();
    }

    private void GetAPI_Json(string api)
    {
        try
        {
            string url = string.Format(api);
            using (WebClient client = new WebClient())
            {
                var json = client.DownloadString(url);

                List<QRCodeObject> item = JsonConvert.DeserializeObject<List<QRCodeObject>>(json);

                for (int i = 0; i < item[0].symbol.Count; i++)
                {
                    txtSearchUID.Text = item[0].symbol[i].data.ToString();
                }
            }
        }
        catch(Exception ex)
        {
            txtSearchUID.Text = "";
            Show_Error_Success_Box("E", "UID not Found");
        }
    }

    private void VaryQualityLevel(string UnCompressed_ImagePath, string QRCode_ImagePath)
    {
        // Get a bitmap. The using statement ensures objects  
        // are automatically disposed from memory after use.  
        using (Bitmap bmp1 = new Bitmap(Server.MapPath(UnCompressed_ImagePath)))
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID  
            // for the Quality parameter category.  
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.  
            // An EncoderParameters object has an array of EncoderParameter  
            // objects. In this case, there is only one  
            // EncoderParameter object in the array.  
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 40L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bmp1.Save(@"c:\TestPhotoQualityFifty.jpg", jpgEncoder, myEncoderParameters);

            //myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(Server.MapPath(QRCode_ImagePath), jpgEncoder, myEncoderParameters);

            //// Save the bitmap as a JPG file with zero quality level compression.  
            //myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bmp1.Save(@"C:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);
        }
    }

    private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }  

    public class Symbol
    {
        public int seq { get; set; }
        public string data { get; set; }
        public object error { get; set; }
    }

    public class QRCodeObject
    {
        public string type { get; set; }
        public List<Symbol> symbol { get; set; }
    }

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
         //   string baseurl = "http://api.smscountry.com/SMSCwebservice_bulk.aspx?User=Sciencetr&passwd=mtel@4321&mobilenumber=" + MobNo + "&message=" + Msg + "&sid=MTEDU&mtype=N&DR=Y";

            // Changes by Shailesh Thakur on 28-09-2023 Remove hardcode sms Connection.
            string smsProvider = System.Configuration.ConfigurationSettings.AppSettings["smsProvider"];
            string smsUser = System.Configuration.ConfigurationSettings.AppSettings["smsUser"];
            string smspasswd = System.Configuration.ConfigurationSettings.AppSettings["smspasswd"];
            string smssid = System.Configuration.ConfigurationSettings.AppSettings["smssid"];

            string baseurl = smsProvider + "?User=" + smsUser + "&passwd=" + smspasswd + "&mobilenumber=" + MobNo + "&message=" + Msg + "&sid=" + smssid + "&mtype=N&DR=Y";
           
     
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
    }

  

    
    #endregion





    
}
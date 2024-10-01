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

public partial class SMS_Communication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnSaveAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            
            string contactno = txtMobileNo.Text.Trim();
            if (contactno != "")
            {
                string Message = txtMessage.Text.Trim();
                SMSSend(contactno, Message);
            }

           

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    public void SMSSend(string MobNo, string Msg)
    {
        if (MobNo.Length == 10)
        {
            //MobNo = MobNo; need to chk by jayant
            WebClient client = new WebClient();
          //  string baseurl = "http://api.smscountry.com/SMSCwebservice_bulk.aspx?User=Sciencetr&passwd=mtel@4321&mobilenumber=" + MobNo + "&message=" + Msg + "&sid=MTEDU&mtype=N&DR=Y";

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
            Show_Error_Success_Box("S","SMS Send Successfully");
            
        }
    }

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
}
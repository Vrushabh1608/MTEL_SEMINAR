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
            //lblContactNumber.Text = "9821072513";
            string contactno = "9821072513";
            if (contactno != "")
            {
                string Message = "Seminar Count -- 353";
                SMSSend(contactno, Message);
            }

           

        }
        catch (Exception ex)
        {
            //Show_Error_Success_Box("E", ex.ToString());
            return;
        }
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
}
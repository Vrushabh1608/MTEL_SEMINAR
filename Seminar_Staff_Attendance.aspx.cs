
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

public partial class Seminar_Staff_Attendance : System.Web.UI.Page
{
    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {

        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            
            return;
        }
        string DateRange = "";
        string fromdate, todate;
        if (id_date_range_picker_1.Value == "")
        {
            fromdate = "0001-01-01";
            todate = "9999-12-31";
        }
        else
        {
            DateRange = id_date_range_picker_1.Value;
            fromdate = DateRange.Substring(0, 10);
            todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
        }

        ControlVisibility("Result");
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;


        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string eventName = null;
        if (string.IsNullOrEmpty(txteventName.Text.Trim()))
        {
            eventName = "%";
        }


        DataSet dsGrid = ProductController.Get_Events_details(eventName, YearName, fromdate, todate, CreatedBy, "2");
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();


        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }


    protected void Btnget_Click(object sender, System.EventArgs e)
    {



        if (txtrptdate.Value == "")
        {
            Show_Error_Success_Box("E", "Kindly Select Date");

            return;
        }
        else
        {
            //DateRange = id_date_range_picker_1.Value;
            //fromdate = DateRange.Substring(0, 10);
            //todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

        }

        ControlVisibility("ReportResult");
        


        DataSet dsGrid = ProductController.Get_Events_details("", "", txtrptdate.Value, "", "", "10");
        if (dsGrid.Tables[0].Rows.Count > 0)
        {
            ddlreportgrid.DataSource = dsGrid;
            ddlreportgrid.DataBind();
            ControlVisibility("ReportResult");
            ddlreportgridexport.DataSource = dsGrid;
            ddlreportgridexport.DataBind();
            lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        }
        else {

            Show_Error_Success_Box("E", " There is no event on" +txtrptdate.Value+ " ");
            btnreport.Visible = true;
        }
    }




    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
          
            FillDDL_AcadYear();
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivEditPanelAs.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            DivaddRemove.Visible = false;
            Divaddnewstaff.Visible = false;
            DivAttendanceReport.Visible = false;
            DivResultstaff.Visible = false;
            btnreport.Visible = true;
            
        }
        else if (Mode == "Result")
        {
            DivEditPanelAs.Visible = false;
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            DivaddRemove.Visible = false;
            Divaddnewstaff.Visible = false;
            DivAttendanceReport.Visible = false;
            ddlreportgrid.Visible = false;
            DivResultstaff.Visible = false;
           
        }
        else if (Mode == "Add")
        {
            DivEditPanelAs.Visible = false;
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivaddRemove.Visible = false;
            Divaddnewstaff.Visible = false;
            DivAttendanceReport.Visible = false;
            DivResultstaff.Visible = false;
           
        }

        else if (Mode == "Edit")
        {
            DivEditPanelAs.Visible = true;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivaddRemove.Visible = false;
            Divaddnewstaff.Visible = false;
            DivAttendanceReport.Visible = false;
            DivResultstaff.Visible = false;
          
            
        }
          else if (Mode == "ADDRemove")
        {
            DivaddRemove.Visible = true;
            DivEditPanelAs.Visible = false;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            Divaddnewstaff.Visible = false;
            DivAttendanceReport.Visible = false;
            DivResultstaff.Visible = false;
          
            
        }
        else if (Mode == "Newstaffadd")
        {
            Divaddnewstaff.Visible = true;
            DivEditPanelAs.Visible = false;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAttendanceReport.Visible = false;
            DivResultstaff.Visible = false;
            

        }
        else if (Mode == "Report")
        {
            Divaddnewstaff.Visible = false;
            DivEditPanelAs.Visible = false;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAttendanceReport.Visible = true;
            BtnAdd.Visible = true;
            DivResultstaff.Visible = false;
            btnreport.Visible = false;



        }
        else if (Mode == "ReportResult")
        {
            DivEditPanelAs.Visible = false;
            DivResultstaff.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = false;
            DivaddRemove.Visible = false;
            Divaddnewstaff.Visible = false;
            DivAttendanceReport.Visible = false;
            btnreport.Visible = false;

        }
       

        
        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
       
        lblAcadYear_Add.Visible = false;
        
        
        ddlAcadYear_Add.Visible = true;
      
        lblHeader_Add.Text = "Create Events for Staff Attendance";
        lblTestPKey_Hidden.Text = "";
        Clear_AddPanel();

    }
    protected void BtnAdd_newstaffClick(object sender, System.EventArgs e)
    {
        ControlVisibility("Newstaffadd");
        lbladdnewstaff.Text = "Add New Staff Details";
        tblnewstaff.Visible = true;
    }

    protected void Btnrpt_staffAttendanceClick(object sender, System.EventArgs e)
    {
        ControlVisibility("Report");
        lbladdnewstaff.Text = "Staff Attendance Details";
        
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
        Clear_AddPanel();
    }


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try

        {

            string staffcode = null;

            if (e.CommandName == "Edit")
            {
                ControlVisibility("Edit");
                lblAcadYear_Add.Visible = true;
                ddlAcadYear_Add.Visible = false;
                BtnSave.Visible = true;
                lblPK.Text = e.CommandArgument.ToString();
                LblPkeyCode.Text = e.CommandArgument.ToString();
                lblHeader_Add.Text = "ADD Staff Details";
                lblTestPKey_Hidden.Text = "";
                FillDDL_Staffdetails();

            }
            if (e.CommandName == "Add_Edit")
            {

                
                ControlVisibility("ADDRemove");
                lblAcadYear_Add.Visible = true;
                ddlAcadYear_Add.Visible = false;
                 BtnSave.Visible = false;
                lblPK.Text = e.CommandArgument.ToString();
                LblPkeyCode.Text = e.CommandArgument.ToString();
                lblHeader_Add.Text = "Edit Staff Details";
                lblTestPKey_Hidden.Text = "";
                FillDDL_Staffdetails_Edit();
            
                
            }
            if (e.CommandName == "Remove")
            {


                ControlVisibility("ADDRemove");
                lblAcadYear_Add.Visible = true;
                ddlAcadYear_Add.Visible = false;
                 BtnSave.Visible = false;
                lblPK.Text = e.CommandArgument.ToString();
                LblPkeyCode.Text = e.CommandArgument.ToString();
                lblHeader_Add.Text = "Edit Staff Details";
                lblTestPKey_Hidden.Text = "";
               
                lblPK.Text = e.CommandArgument.ToString();
                staffcode = LblPkeyCode.Text = e.CommandArgument.ToString();

                DataSet ds = new DataSet();
                ds = ProductController.insert_update_Events_details("", "", "", "", "5", "", staffcode, lblPK.Text, "");

                Show_Error_Success_Box("S", "User Removed Successfully");

            }


            
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }

    
    protected void FillDDL_Staffdetails()
{


    DataSet dsGrid = ProductController.Get_Events_details(lblPK.Text, "", "", "", "", "3");
     Datasetstaffdata.DataSource = dsGrid;
     Datasetstaffdata.DataBind();

}

    protected void FillDDL_Staffdetails_Edit()
    {


        DataSet dsGrid = ProductController.Get_Events_details(lblPK.Text, "", "", "", "", "6");
        Datasetstaffdata_addremove.DataSource = dsGrid;
        Datasetstaffdata_addremove.DataBind();
        if (dsGrid.Tables[0].Rows.Count > 0)
        {

            for (int cnt = 0; cnt <= dsGrid.Tables[0].Rows.Count - 1; cnt++)
            {

                foreach (DataListItem dtlItem in Datasetstaffdata_addremove.Items)
                {
                    CheckBox chkCheck_Edit = (CheckBox)dtlItem.FindControl("chkCheck_Edit");
                    Label lblRowNoedite = (Label)dtlItem.FindControl("lblRowNoedite");
                    if (Convert.ToString(lblRowNoedite.Text).Trim() == Convert.ToString(dsGrid.Tables[0].Rows[cnt]["staffalloted"]).Trim())
                    {
                        chkCheck_Edit.Checked = true;
                        break;
                    }
                }

            }
        }

    }

    protected void chkCheck_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox s = sender as CheckBox;
            dynamic row = (DataListItem)s.NamingContainer;
            Label lblrifd = row.FindControl("lblrifd");
            Label lblErrorSaveMessage = row.FindControl("lblErrorSaveMessage");

            if (s.Checked == true )
            {

                lblrifd.Visible = true;
            }
            else
            {

                lblrifd.Visible = true;

                lblErrorSaveMessage.Text = "";
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox s = sender as CheckBox;
           foreach (DataListItem dtlItem in Datasetstaffdata.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                if (chkitemck.Visible == true)
                {
                    Label lblErrorSaveMessage = (Label)dtlItem.FindControl("lblErrorSaveMessage");
                    Label lblrifd = (Label)dtlItem.FindControl("lblrifd");

                    chkitemck.Checked = s.Checked;
                    if (s.Checked == true )
                    {
                        
                        lblrifd.Visible = false;
                    }
                    else
                    {
                        
                        lblrifd.Visible = true;
                        lblErrorSaveMessage.Text = "";
                    }

                }
            }
            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }


    

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

   

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;

        BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;

        //BindDDL(DDL_ReportAcadyear, dsAcadYear, "Description", "Id");
        //DDL_ReportAcadyear.Items.Insert(0, "Select");
        //DDL_ReportAcadyear.SelectedIndex = 0;

        
    }



    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }



    protected void ddlAcadYear_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
       
        Clear_Error_Success_Box();
    }


    protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

       
        if (ddlAcadYear_Add.SelectedIndex == 0 & string.IsNullOrEmpty(lblTestPKey_Hidden.Text))
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear_Add.Focus();
            return;
        }
    


        //Save
        int ResultId = 0;

       
      


        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

  


        if (string.IsNullOrEmpty(lblTestPKey_Hidden.Text))
        {
            

            string YearName = null;
            YearName = ddlAcadYear_Add.SelectedItem.ToString();


            ResultId = ProductController.Insert_Events_details(txtEventName_Add.Text, YearName, txtVenue.Text, txtChequeDate.Value, CreatedBy, "1");

        }
        else
        //{
        //    string PKeyp = lblTestPKey_Hidden.Text;
        //    string[] parts = PKeyp.Split('%');//PKeyp.Split( "%" );
        //    string DivisionCode = null;
        //    DivisionCode = parts[0];

        //    string YearName = null;
        //    YearName = parts[1];

        //    //string StandardCode = null;
        //    //StandardCode = parts[2];

        //    string TestCode = null;
        //    TestCode = parts[2];
        //    //ResultId = ProductController.Update_RCO_Test(txtTestName_Add.Text, txtTestDesc_Add.Text, YearName, QPSetCount, QuestionCount, MaxMarks, "", CreatedBy, 2);
        //    //ResultId = 1
        //}
        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }
        else if (ResultId == -1)
        {
            Show_Error_Success_Box("E", "0029");
            txtEventName_Add.Focus();
            return;
        }
        else if (ResultId == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Record not saved";
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    protected void BtnSavenewstaffdetails_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();


        if (txtFName.Text=="")
        {
            Show_Error_Success_Box("E", "Staff Name Can't Be Blank");
            
            return;
        }
        if (txtempid.Text == "")
        {
            Show_Error_Success_Box("E", "Staff Employee Code Can't Be Blank");

            return;
        }
        if (txtnewRifd.Text == "")

        {
            Show_Error_Success_Box("E", "Staff RFID crad No Can't Be Blank");
            return;
        
        }


        //Save
        int ResultId = 0;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

             string YearName = null;
            YearName = ddlAcadYear_Add.SelectedItem.ToString();


            ResultId = ProductController.Insert_Events_details(txtFName.Text, txtempid.Text, txtnewRifd.Text, txtChequeDate.Value, CreatedBy, "9");

                
            if (ResultId == 1)
            {
                ControlVisibility("Result");
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "0000");
                Clear_AddPanel();
            }
            else if (ResultId == -1)
            {
                Show_Error_Success_Box("E", "0029");
                txtEventName_Add.Focus();
                return;
            }
            else if (ResultId == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record not saved as RIFD already Exists";
                UpdatePanelMsgBox.Update();
                return;
            }
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
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

    private void Clear_AddPanel()
    {
       
        ddlAcadYear_Add.SelectedIndex = 0;
        
        txtEventName_Add.Text = "";
        
    }

    protected void ddlTestMode_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

    protected void ddlTestType_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

    

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
       
        ddlAcadYear.SelectedIndex = 0;
        
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";

    }
    protected void HLExport_Click(object sender, EventArgs e)
    {

        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=StaffAttendance.xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        ddlreportgridexport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
  

    protected void btn_save_edit_Click(object sender, EventArgs e)
    {

        string PKeyp = lblPK.Text;
        string[] parts = PKeyp.Split('%');//PKeyp.Split( "%" );
        string EventID = null;
        EventID = parts[0];

        string Venue_id = null;
        Venue_id = parts[1];
        string RFIDcard = null;
        
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        //string UserID = cookie.Values["UserID"];
      
        foreach (DataListItem dtlItem in Datasetstaffdata.Items)
        {

            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");

            if (chkCheck.Checked == true)
            {
                //i = 1;
                
                 Label lblrifd = (Label)dtlItem.FindControl("lblrifd");
                
                Label lblErrorSaveMessage = (Label)dtlItem.FindControl("lblErrorSaveMessage");
                if (lblrifd.Text=="")
                {
                    //j = -1;
                    lblErrorSaveMessage.CssClass = "red";
                    lblErrorSaveMessage.Text = "Add RFID Card Number";
                    
                }
             
                else
                {
                    lblErrorSaveMessage.Text = "";
                    Label lblRowNo = (Label)dtlItem.FindControl("lblRowNo");
                    Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                    Label LblroleCode = (Label)dtlItem.FindControl("LblroleCode");


                   
                        RFIDcard = lblrifd.Text;
               
                    


                    DataSet ds = new DataSet();
                    ds = ProductController.insert_update_Events_details(txtEventName_Add.Text, ddlAcadYear.SelectedValue, Venue_id, "", "4", EventID, lblRowNo.Text, lblStudentName.Text, RFIDcard);
                   
                    if (ds != null)
                    {
                        
                        if (ds.Tables[0].Rows[0]["ErrorSaveId"].ToString() == "2") //Error Record
                        {
                            
                            lblErrorSaveMessage.CssClass = "red";
                            lblErrorSaveMessage.Text = ds.Tables[0].Rows[0]["lblErrorSaveMessage"].ToString();
                        }
                        else if (ds.Tables[0].Rows[0]["ErrorSaveId"].ToString() == "1") //Save Record
                        {
                            
                            lblErrorSaveMessage.CssClass = "green";
                            lblErrorSaveMessage.Text = ds.Tables[0].Rows[0]["lblErrorSaveMessage"].ToString();

                        }

                    }
                }
            }
        }


    }

    protected void btnPrintICard_Click(object sender, EventArgs e)
    {
        Print_ICard();
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

                foreach (DataListItem dtlItem in Datasetstaffdata_addremove.Items)
                {
                    CheckBox chkCheck_Edit = (CheckBox)dtlItem.FindControl("chkCheck_Edit");
                    if (chkCheck_Edit.Checked == true)
                    {
                       
                        Label lblRowNoedite = (Label)dtlItem.FindControl("lblRowNoedite");
                        list.Add(lblRowNoedite.Text);
                        TotalStud = TotalStud + 1;
                    }
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

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/ID_Card_for_Staff.jpg"));

                    jpg.ScaleToFit(980, 810);//980, 580
                    jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    jpg.SetAbsolutePosition(-25, 5);
                    document.Add(jpg);

                    for (int j = 0; j < 4; j++)
                    {
                        if (ActualStud != TotalStud)
                        {
                            string ActualUID = list[ActualStud] as string;
                            string StudentName = "", RFIDcard = "", Board = "", Rank = "", Percentile = "", YourGuide = "", CounterNo = "";

                            foreach (DataListItem dtlItem in Datasetstaffdata_addremove.Items)
                            {
                               
                                Label lblRowNoedite = (Label)dtlItem.FindControl("lblRowNoedite");
                                if (lblRowNoedite.Text == ActualUID)
                                {
                                    
                                    Label LblroleCode = (Label)dtlItem.FindControl("LblroleCode");
                                   
                                    Label lblrifd = (Label)dtlItem.FindControl("lblrifd");

                                    if (lblrifd.Text == "")
                                    {
                                        RFIDcard = lblrifd.Text;
                                    }
                                    else
                                    {
                                        RFIDcard = lblrifd.Text;
                                    }
                                    Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                                    

                                    StudentName = lblStudentName.Text;
                                    
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
                                    logo.SetAbsolutePosition(100, YPos - 200);
                                    logo.ScaleToFit(200f, 95f);
                                    logo.ScaleAbsolute(125, 110);

                                    //logo.ScalePercent(35);
                                    document.Add(logo);
                                }

                                cb.BeginText();

                                //cb.SetColorFill(BaseColor.WHITE);
                                //cb.SetFontAndSize(f_cn, 12);
                                //cb.SetTextMatrix((47 + ((280 - 47) / 2) - (cb.GetEffectiveStringWidth("UID No. : " + ActualUID, false) / 2)), YPos - 235);//77//302
                                //cb.ShowText("UID No. : " + ActualUID);
                                //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                //cb.SetTextMatrix((47 + ((280 - 47) / 2) - (cb.GetEffectiveStringWidth(Center, false) / 2)), YPos - 270);
                                cb.ShowText("");//Center
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.RED);
                                cb.SetFontAndSize(f_cn, 18);
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
                                    logo.SetAbsolutePosition(382, YPos - 200);
                                    logo.ScaleToFit(200f, 95f);
                                    logo.ScaleAbsolute(125, 110);

                                    //logo.ScalePercent(35);
                                    document.Add(logo);
                                }

                                cb.BeginText();//258

                                //cb.SetColorFill(BaseColor.WHITE);
                                //cb.SetFontAndSize(f_cn, 12);
                                //cb.SetTextMatrix((335 + ((550 - 335) / 2) - (cb.GetEffectiveStringWidth("UID No. : " + ActualUID, false) / 2)), YPos - 235);//77//302
                                //// cb.SetTextMatrix(550, YPos - 235);//77//302
                                //cb.ShowText("UID No. : " + ActualUID);
                                //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetFontAndSize(f_cn, 12);
                                //cb.SetTextMatrix((320 + ((565 - 320) / 2) - (cb.GetEffectiveStringWidth(Center, false) / 2)), YPos - 270);
                                cb.ShowText("");//Center
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                cb.SetColorFill(BaseColor.RED);
                                cb.SetFontAndSize(f_cn, 18);
                                cb.SetTextMatrix((338 + ((563 - 338) / 2) - (cb.GetEffectiveStringWidth(StudentName, false) / 2)), YPos - 300);
                                cb.ShowText(StudentName);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                //cb.SetColorFill(BaseColor.BLACK);
                                //cb.SetFontAndSize(f_cn, 12);
                                //cb.SetTextMatrix(390, YPos - 327);
                                //cb.ShowText(YourGuide);//YourGuide
                                //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                                //cb.SetColorFill(BaseColor.BLACK);
                                //cb.SetFontAndSize(f_cn, 12);
                                //cb.SetTextMatrix(430, YPos - 350);
                                //cb.ShowText(CounterNo);//CounterNo
                                //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

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

    protected void btn_close_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
       
    }




    protected void BtnCloseUpload_Click1(object sender, EventArgs e)
    {
        ControlVisibility("Result");
    }

}
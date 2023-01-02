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
using System.Net.Mail;

public partial class Admission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDivHead.Text = "Please Fill Following Information";
            FillDDL_Division();
            FillDDL_CurrentSScCenter();
            FillDDL_CouncilBy();

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            //string UserID = cookie.Values["UserID"];
            //string UserName = cookie.Values["UserName"];
            if (cookie == null)
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
            BtnSave.Visible = true;
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
            ddlCurrentSSCCenter.SelectedIndex = 0;
            ddlDivision.SelectedIndex = 0;
            ddlDivision_SelectedIndexChanged(sender, e);
            ddlPrefferedScCenter_SelectedIndexChanged(sender, e);
            ddlProductName_SelectedIndexChanged(sender, e);
            ddlPayMode.SelectedIndex = 0;
            ddlPayType.SelectedIndex = 0;
            ddlPayType_SelectedIndexChanged(sender, e);

            txtCCDCAmount.Text = "";
            txtCCDCTransctionId.Text = "";
            txtChequeDate.Value = "";
            txtChequeAmount.Text = "";
            txtMICRCode.Text = "";
            lblBankName.Text = "";
            txtChequeNo.Text = "";
            ddlCouncilBy.SelectedIndex = 0;

            DataSet ds = ProductController.Get_SeminarDetail_ByUID(txtSearchUID.Text.Trim(),"1");
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
                //else if (ds.Tables[0].Rows[0]["Result"].ToString() == "-3")
                //{
                //    Show_Error_Success_Box("E", "Parents has not yet Accepted Terms and Conditions");
                //    tblSearchInfo.Visible = false;
                //    tblSearchDetail.Visible = true;
                //    BtnSave.Visible = false;
                //    BtnClose.Visible = false;
                //    return;
                //}
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
                        if (ds.Tables[1].Rows[0]["Council_Id"].ToString() != "")
                        {
                            try
                            {
                                ddlCouncilBy.SelectedValue = ds.Tables[1].Rows[0]["Council_Id"].ToString();
                            }
                            catch
                            {
                                ddlCouncilBy.SelectedIndex = 0;
                            }
                        }
                        try
                        {
                            ddlCurrentSSCCenter.SelectedValue = ds.Tables[1].Rows[0]["CurrentSScCenter"].ToString();
                        }
                        catch
                        {
                            ddlCurrentSSCCenter.SelectedIndex = 0;
                        }
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
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlPayType.SelectedValue.ToString() == "04") //Credit Card Debit Card
            {
                trCCDCAmount.Visible = true;
                trCCDCTransactionId.Visible = true;
                trChequeAmount.Visible = false;
                trMICRCode.Visible = false;
                trChequeNumber.Visible = false;
                trChequeDate.Visible = false;
            }
            else if (ddlPayType.SelectedValue.ToString() == "01")//Demanddraft/
            {
                trCCDCAmount.Visible = false;
                trCCDCTransactionId.Visible = false;
                trChequeAmount.Visible = true;
                trMICRCode.Visible = true;
                trChequeNumber.Visible = true;
                trChequeDate.Visible = true;
            }
            else if (ddlPayType.SelectedValue.ToString() == "05") //NEFT Card
            {
                trCCDCAmount.Visible = true;
                trCCDCTransactionId.Visible = true;
                trChequeAmount.Visible = false;
                trMICRCode.Visible = false;
                trChequeNumber.Visible = false;
                trChequeDate.Visible = false;
            }
            else if (ddlPayType.SelectedValue.ToString() == "02")//Cheque/
            {
                trCCDCAmount.Visible = false;
                trCCDCTransactionId.Visible = false;
                trChequeAmount.Visible = true;
                trMICRCode.Visible = true;
                trChequeNumber.Visible = true;
                trChequeDate.Visible = true;
            }
            else
            {
                trCCDCAmount.Visible = false;
                trCCDCTransactionId.Visible = false;
                trChequeAmount.Visible = false;
                trMICRCode.Visible = false;
                trChequeNumber.Visible = false;
                trChequeDate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void txtMICRCode_TextChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        string MicrCode = "";
        MicrCode = txtMICRCode.Text;
        SqlDataReader dr = AccountController.GetBanknameandAddress(MicrCode);
        if ((((dr) != null)))
        {
            if (dr.Read())
            {
                lblBankName.Text = dr["bankname"].ToString();
                //lblBankName.Text = dr["bankbranch"].ToString();
            }
            else
            {
                lblBankName.Text = "";
            }
        }
        else
        {
            lblBankName.Text = "";
        }
    }

    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
       BindOptionalSubject(ddlProductName.SelectedValue, ddlPrefferedScCenter.Text);
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_PrefScCenter();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (txtFName.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter FirstName");
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
                Show_Error_Success_Box("E", "Enter Student Email ID");
                txtStudentEmailId.Focus();
                return;
            }
            if (txtParentName.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Parent Name");
                txtParentName.Focus();
                return;
            }
            
            if (txtParentContact.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Parent Contact Number");
                txtParentContact.Focus();
                return;
            }

            if (ddlCurrentSSCCenter.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Current Center");
                return;
            }
            if (ddlDivision.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                return;
            }
            if (ddlPrefferedScCenter.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Preffered Center");
                return;
            }
            
            if (ddlProductName.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Interested Product");
                return;
            }
            int TotalRecord = 0;

            foreach (DataListItem dtlItem in dlselective.Items)
            {
                CheckBox ckhselect1 = (CheckBox)dtlItem.FindControl("ckhselect1");

                if (ckhselect1.Checked == true)
                {
                    TotalRecord = TotalRecord + 1;
                }
            }
            if (TotalRecord == 0)
            {
                Show_Error_Success_Box("E", "Select Atleast One Subject Group");
                return;
            }
            if (ddlPayMode.SelectedValue.ToString() == "0")
            {
                Show_Error_Success_Box("E", "Select Pay Mode");
                return;
            }
            if (ddlPayType.SelectedValue.ToString() == "0")
            {
                Show_Error_Success_Box("E", "Select Pay Type");
                return;
            }
            if (ddlPayType.SelectedValue.ToString() == "04")//Credit Or Debit Card
            {
                if (txtCCDCAmount.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Amount");
                    txtCCDCAmount.Focus();
                    return;
                }
                if (txtCCDCTransctionId.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Transaction Id");
                    txtCCDCTransctionId.Focus();
                    return;
                }
            }
            else if (ddlPayType.SelectedValue.ToString() == "01")//Cheque
            {
                if (txtChequeDate.Value == "")
                {
                    Show_Error_Success_Box("E", "Enter Cheque Date");
                    txtChequeDate.Focus();
                    return;
                }
                if (txtChequeAmount.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Amount");
                    txtChequeAmount.Focus();
                    return;
                }
                if (txtMICRCode.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter MICR Code");
                    txtMICRCode.Focus();
                    return;
                }
                if (lblBankName.Text == "")
                {
                    Show_Error_Success_Box("E", "Enter Correct MICR Code");
                    txtMICRCode.Focus();
                    return;
                }

               
                if (txtChequeNo.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Cheque Number");
                    txtChequeNo.Focus();
                    return;
                }
                if (txtChequeNo.Text.Length != 6)
                {
                    Show_Error_Success_Box("E", "Enter 6 Digit Cheque Number");
                    txtChequeNo.Focus();
                    return;
                }
                
            }

            object obj = null;
            object obj1 = null;
            CheckBox Chk = default(CheckBox);
            Label lblSelectmaterialcode = default(Label);
            Label lblvoucherDesc = default(Label);
            List<string> list = new List<string>();
            List<string> List1 = new List<string>();
            string Sgrcode = "",Sgr_Desc="";
            CheckBox cb = default(CheckBox);

            foreach (DataListItem li in dlselective.Items)
            {
                obj = li.FindControl("lblmaterialcodeadd");
                if (obj != null)
                {
                    lblSelectmaterialcode = (Label)obj;
                }
                obj1 = li.FindControl("lblvoucherDesc");
                if (obj1 != null)
                {
                    lblvoucherDesc = (Label)obj1;
                }
                
                cb = (CheckBox)li.FindControl("ckhselect1");
                if (cb != null)
                {
                    Chk = (CheckBox)cb;
                }

                if (Chk.Checked == true)
                {
                    list.Add(lblSelectmaterialcode.Text);
                    Sgrcode = string.Join(",", list.ToArray());
                    List1.Add(lblvoucherDesc.Text);
                    Sgr_Desc = string.Join(",", List1.ToArray());
                }
            }

            string Cheque_No="", MICR_Code="", Amount="",Cheque_Date="";
            if (ddlPayType.SelectedValue.ToString() == "04")//Credit Or Debit Card
            {
                Cheque_No=txtCCDCTransctionId.Text.Trim();
                Amount=txtCCDCAmount.Text.Trim();
                
            }
            else if (ddlPayType.SelectedValue.ToString() == "01")//Cheque
            {
                Cheque_No=txtChequeNo.Text.Trim();
                MICR_Code=txtMICRCode.Text.Trim();
                Amount=txtChequeAmount.Text.Trim();
                Cheque_Date = txtChequeDate.Value;
            }

            int ResultId = 0;
            int resultid = 0;
            string count = "";
            ResultId = ProductController.InsertUpdateSeminar_Center(txtFName.Text.Trim(), txtMName.Text.Trim(), txtLName.Text.Trim(), txtContactNo.Text.Trim(), txtStudentEmailId.Text.Trim(), txtAddress.Text.Trim(), txtParentName.Text.Trim(), txtParentContact.Text.Trim(), txtParentEmailId.Text.Trim(), txtUID.Text.Trim(), "2", ddlDivision.SelectedValue, ddlPrefferedScCenter.SelectedValue, ddlPrefferedScCenter.SelectedItem.ToString(), ddlProductName.SelectedValue, ddlProductName.SelectedItem.ToString(), ddlPayMode.SelectedItem.ToString(), Sgrcode, Cheque_No, MICR_Code, Amount, ddlPayType.SelectedValue.ToString(),Cheque_Date);
            if (ResultId == 1)
            {
                //SMS Sending
                DataSet DSChk = ProductController.Check_MesageTemplate("015", ddlDivision.SelectedValue, 1);
                if (DSChk != null)
                {
                    count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                    if (count == "0")
                    {
                        //Disable
                    }
                    else
                    {
                        string Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                        string Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                        string MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();

                        string NewTemplate = Template.Replace("%2526", "%26");
                        NewTemplate = NewTemplate.Replace("[Parent Name]", txtParentName.Text.Trim());
                        NewTemplate = NewTemplate.Replace("[Student Name]", txtFName.Text.Trim());
                        NewTemplate = NewTemplate.Replace("[Course]", ddlProductName.SelectedItem.ToString());
                        NewTemplate = NewTemplate.Replace("[Subject]", Sgr_Desc);
                        if (MsgMode == "Auto")
                        {
                           //// resultid = ProductController.Insert_SMSLog(ddlPrefferedScCenter.SelectedValue, Message_cd, txtContactNo.Text, NewTemplate, "0", "Auto", "Transactional");
                            resultid = ProductController.Insert_SMSLog(ddlPrefferedScCenter.SelectedValue, Message_cd, txtParentContact.Text, NewTemplate, "0", "Auto", "Transactional");
                        }
                        else if (MsgMode == "Manual")
                        {
                          ////  resultid = ProductController.Insert_SMSLog(ddlPrefferedScCenter.SelectedValue, Message_cd, txtContactNo.Text, NewTemplate, "0", "TAB", "Transactional");
                            resultid = ProductController.Insert_SMSLog(ddlPrefferedScCenter.SelectedValue, Message_cd, txtParentContact.Text, NewTemplate, "0", "TAB", "Transactional");
                        }
                    }
                }

                //Email Sending
                 string userid = "", Password = "", Host = "", SSL = "", MailType = "";
                 int Port = 0;
                 DataSet dsEmailInfo = ProductController.GetMailDetails_ByCenter(ddlPrefferedScCenter.SelectedValue,"Transactional",3);
                 if (dsEmailInfo.Tables[0].Rows.Count > 0)
                 {
                     userid = Convert.ToString(dsEmailInfo.Tables[0].Rows[0]["UserId"]);
                     Password = Convert.ToString(dsEmailInfo.Tables[0].Rows[0]["Password"]);
                     Host = Convert.ToString(dsEmailInfo.Tables[0].Rows[0]["Host"]);
                     Port = Convert.ToInt32(Convert.ToString(dsEmailInfo.Tables[0].Rows[0]["Port"]));
                     SSL = Convert.ToString(dsEmailInfo.Tables[0].Rows[0]["EnableSSl"]);
                     MailType = Convert.ToString(dsEmailInfo.Tables[0].Rows[0]["MailType"]);


                     //////
                     string EmailId = "";
                     if (txtParentEmailId.Text.Trim() != "")
                     {
                         EmailId = txtStudentEmailId.Text.Trim() + "," + txtParentEmailId.Text.Trim();
                     }
                     else
                     {
                         EmailId = txtStudentEmailId.Text.Trim();
                     }

                     //Rules and Regulations Email Sending
                     MailMessage Msg = new MailMessage();
                     try
                     {
                         Msg = new MailMessage(userid, EmailId);
                     }
                     catch (Exception ex)
                     {
                     }
                     string CurTimeFrame = null, Result = "";
                     CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                     // Subject of e-mail
                     Msg.Subject = "RULES & REGULATIONS";
                     if (ddlDivision.SelectedValue == "E0")
                     {
                         //Msg.Body = "Dear Student/Parents, <br/><br/> 1. Allotment of batch and session is solely at the discretion of the management.<br/><br/>2. Attendance for lectures and examinations is essential for enhancing performance of the students.<br/><br/>3. Students are discouraged from being absent from any class unless it is an unavoidable circumstances. Permission for absenteeism will be given only for genuine reasons.<br/>&nbsp;&nbsp;&nbsp;&nbsp;Every absence must be followed by parents' or guardians' phone call on the same day, followed by a note sent with the student.<br/><br/>4. Homework is compulsory. Action will be taken against defaulters.<br/><br/>5. Any damage to the institute’s property will have to be compensated by the student responsible for the same.<br/><br/>6. The Management reserves the right of taking any decision / action felt necessary against a student to maintain the decorum and morale of the institute.<br/><br/>7. The Management has the right to change batch timings, sub-divide batches or increase or decrease the number of lectures per week.<br/><br/>8. Students are strictly prohibited from carrying cell phones / any other electronic gadgets to the class.<br/><br/>9. The fees can be paid through Credit / Debit Cards or Cheque / DD <br/>&nbsp;&nbsp;&nbsp;&nbsp;The Cheque / DD should be in favour of for MHT-CET ‘MT Educare Ltd. - Science’<br/><br/>10. Fees paid for one subject / term cannot be adjusted for another subject / term or subsequent year. Fees will not be transferable to any other student or course.<br/><br/>11. In case of transfer or change in address, the student shall be accommodated in centre which is nearby to his new residence.<br/><br/>12. A penalty of Rs. 200/- will be levied in case of each dishonoured cheque, if any.<br/><br/>13. The post dated cheques will not be kept on hold under any circumstances.<br/><br/>14. Any change in the Service Tax by the government, will accordingly be made applicable.<br/><br/>15. Photograph(s) and testimonials of the students / parents may be used in any of MT Educare’s Print, Electronic, Outdoor and Digital communications and advertising in India and Internationally.<br/><br/>16. Students / Parents will receive emails / sms on latest offers / news / information.<br/><br/>17. In case of change in syllabus, portion will be covered as per the availability of text book.<br/><br/>18. Fees once paid will neither be refunded nor adjusted:<br/><br/><b>I have read the above mentioned rules and regulations and agreed to abide by the same.</b><br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>MT Educare Ltd. - Science</b>";//2015-2016 Admission
                         Msg.Body = "Dear Student/Parents, <br/><br/> 1. Allotment of batch and session is solely at the discretion of the management.<br/><br/>2. Attendance for lectures and examinations is essential for enhancing performance of the students.<br/><br/>3. Students are discouraged from being absent from any class unless it is an unavoidable circumstances. Permission for absenteeism will be given only for genuine reasons.<br/>&nbsp;&nbsp;&nbsp;&nbsp;Every absence must be followed by parents' or guardians' phone call on the same day, followed by a note sent with the student.<br/><br/>4. Homework is compulsory. Action will be taken against defaulters.<br/><br/>5. Any damage to the institute’s property will have to be compensated by the student responsible for the same.<br/><br/>6. The Management reserves the right of taking any decision / action felt necessary against a student to maintain the decorum and morale of the institute.<br/><br/>7. The Management has the right to change batch timings, sub-divide batches or increase or decrease the number of lectures per week.<br/><br/>8. In case of transfer or change in address, the student shall be accommodated in centre which is nearby to his new residence.<br/><br/>9. In case of change in syllabus, portion will be covered as per the availability of text book.<br/><br/><b>10. Fees paid for one subject/term cannot be adjustment for another subject/term or the subsequent year. Fees will not be transferable to any other student or course.</b><br/><br/><b>11.</b> First down payment can be paid through <br/> <b>• Credit or Debit Cards</b><br/><b>• DD drawn in favour of  \"MT Educare Ltd. - Science\" </b><br/><b>• NEFT Bank details for NEFT </b>is as follows:<br/>Bank Name: The Zoroastrian Co-operative Bank Ltd.<br/>Bank Account No: 013300100005454<br/>IFSC Code: ZCBL0000013</br>Branch : Charai, Thane 400601 <br/> <br/><br/>12. ECS Formalities for future EMIs has to be completed within 7 days from date of admission; else special offer benefit will be lapsed<br/><br/>13. EMI and FDP date of payment is fixed and any deviation from the same is not acceptable.<br/><br/>14. Any change in GST / Other Taxes / Cess by the Government will be made applicable.<br/><br/>15. Fees once paid will neither refunded nor transferred under any circumstances.<br/><br/>16. A penalty of Rs. 200/- will be levied in case of each dishonored ECS, if any.<br/><br/>17. Photograph(s) and testimonials of the students / parents may be used in any of MT EDUCARE’s and LAKSHYA Print, Electronic, Outdoor and Digital communications and advertising in India and Internationally.<br/><br/>18. Students / Parents will receive emails / SMS on latest offers / news / information.<br/><br/><b>I have read the above mentioned rules and regulations and agreed to abide by the same.</b><br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>MTEDUCARE - SCIENCE</b>";//2016-2017 Admission
                     }
                     else
                     {
                         //Msg.Body = "Dear Student/Parents, <br/><br/> 1. Allotment of batch and session is solely at the discretion of the management.<br><br/>2. Attendance for lectures and examinations is essential for enhancing performance of the students.<br/><br/>3. Students are discouraged from being absent from any class unless it is an unavoidable circumstances. Permission for absenteeism will be given only for genuine reasons.<br/>&nbsp;&nbsp;&nbsp;&nbsp;Every absence must be followed by parents' or guardians' phone call on the same day, followed by a note sent with the student.<br/><br/>4. Homework is compulsory. Action will be taken against defaulters.<br/><br/>5. Any damage to the institute’s property will have to be compensated by the student responsible for the same.<br/><br/>6. The Management reserves the right of taking any decision / action felt necessary against a student to maintain the decorum and morale of the institute.<br/><br/>7. The Management has the right to change batch timings, sub-divide batches or increase or decrease the number of lectures per week.<br/><br/>8. Students are strictly prohibited from carrying cell phones / any other electronic gadgets to the class.<br/><br/>9. The fees can be paid through Credit / Debit Cards or Cheque / DD <br/>&nbsp;&nbsp;&nbsp;&nbsp;The Cheque / DD should be in favour of for Advance & Foundation ‘Lakshya Educare Pvt. Ltd.’<br/><br/>10. Fees paid for one subject / term cannot be adjusted for another subject / term or subsequent year. Fees will not be transferable to any other student or course.<br/><br/>11. In case of transfer or change in address, the student shall be accommodated in centre which is nearby to his new residence.<br/><br/>12. A penalty of Rs. 200/- will be levied in case of each dishonoured cheque, if any.<br/><br/>13. The post dated cheques will not be kept on hold under any circumstances.<br/><br/>14. Any change in the Service Tax by the government, will accordingly be made applicable.<br/><br/>15. Photograph(s) and testimonials of the students / parents may be used in any of MT Educare’s Print, Electronic, Outdoor and Digital communications and advertising in India and Internationally.<br/><br/>16. Students / Parents will receive emails / sms on latest offers / news / information.<br/><br/>17. In case of change in syllabus, portion will be covered as per the availability of text book.<br/><br/>18. Fees once paid will neither be refunded nor adjusted:<br/><br/><b>I have read the above mentioned rules and regulations and agreed to abide by the same.</b><br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>Lakshya – By IITians for IITians</b>";//2015-2016
                         Msg.Body = "Dear Student/Parents, <br/><br/> 1. Allotment of batch and session is solely at the discretion of the management.<br><br/>2. Attendance for lectures and examinations is essential for enhancing performance of the students.<br/><br/>3. Students are discouraged from being absent from any class unless it is an unavoidable circumstances. Permission for absenteeism will be given only for genuine reasons.Every absence must be followed by parents' or guardians' phone call on the same day, followed by a note sent with the student.<br/><br/>4. Homework is compulsory. Action will be taken against defaulters.<br/><br/>5. Any damage to the institute’s property will have to be compensated by the student responsible for the same.<br/><br/>6. The Management reserves the right of taking any decision / action felt necessary against a student to maintain the decorum and morale of the institute.<br/><br/>7. The Management has the right to change batch timings, sub-divide batches or increase or decrease the number of lectures per week.<br/><br/>8. In case of transfer or change in address, the student shall be accommodated in centre which is nearby to his new residence.<br/><br/>9. In case of change in syllabus, portion will be covered as per the availability of text book.<br/><br/><b>10. Fees paid for one subject / term cannot be adjustment for another subject / term or the subsequent year. Fees will not be transferable to any other student or course.</b><br/><br/>11. First down payment can be paid through<br/><b>• Credit or Debit Cards</b><br/><b> • DD drawn in favour of  \"LAKSHYA EDUCARE PRIVATE LIMITED\"</b><br/><b>• NEFT Bank details for NEFT</b> is as follows:<br/>Bank Name: The Zoroastrian Co-operative Bank Ltd.<br/>Bank Account No: 013300100005459<br/>IFSC Code: ZCBL0000013<br/>Branch : Charai, Thane 400601 <br/><br/>12. ECS Formalities for future EMIs has to be completed within 7 days from date of admission; else special offer benefit will be lapsed<br/><br/>13. EMI and FDP date of payment is fixed and any deviation from the same is not acceptable.<br/><br/>14. Any change in GST / Other Taxes / Cess by the Government will be made applicable.<br/><br/>15. Fees once paid will neither refunded nor transferred under any circumstances.<br/><br/>16. A penalty of Rs. 200/- will be levied in case of any dishonored ECS.<br/><br/>17. Photograph(s) and testimonials of the students / parents may be used in any of MT EDUCARE’s and LAKSHYA Print, Electronic, Outdoor and Digital communications and advertising in India and Internationally.<br/><br/>18. Students / Parents will receive emails / SMS on latest offers / news / information.<br/><br/><b>I have read the above mentioned rules and regulations and agreed to abide by the same.</b><br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>Lakshya – Forum for Competition.</b>";//2016-2017
                     }
                     
                     Msg.IsBodyHtml = true;

                     bool value = System.Convert.ToBoolean(SSL);
                     SmtpClient smtp = new SmtpClient();
                     smtp.Host = Host;
                     smtp.EnableSsl = value;
                     NetworkCredential NetworkCred = new NetworkCredential(userid, Password);
                     smtp.UseDefaultCredentials = true;
                     smtp.Credentials = NetworkCred;
                     smtp.Port = Port;

                     //int resultid1 = 0;
                     try
                     {
                         //smtp.Timeout = 20000;
                         smtp.Send(Msg);

                         resultid = ProductController.Insert_Mailog(EmailId, Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, "", "1", "TAB", 1, "General Email", MailType);
                         count = count + 1;
                     }
                     catch (Exception ex)
                     {
                         resultid = ProductController.Insert_Mailog(EmailId, Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, "", "2", "TAB", 1, "General Email", MailType);
                     }
                     
                     //Payment Detail Email Send                      
                     MailMessage Msg1 = new MailMessage();
                     try
                     {
                         Msg1 = new MailMessage(userid, EmailId);
                     }
                     catch (Exception ex)
                     {
                     }
                     CurTimeFrame = null; Result = "";
                     CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                     // Subject of e-mail
                     Msg1.Subject = "FEE ACKNOWLEDGEMENT";
                     string word="";
                     //ddlPayType.Sele
                     if (ddlPayType.SelectedValue.ToString() == "04")//Credit Or Debit Card
                     {
                         word = ConvertNumbertoWords(Convert.ToInt32(txtCCDCAmount.Text.Trim()));
                         if (ddlDivision.SelectedValue == "E0")
                         {
                             Msg1.Body = "Dear Student/Parents, <br/><br/> Received with thanks an amount of Rs." + txtCCDCAmount.Text.Trim() + "/- <b>(RUPEES " + word + " ONLY)</b> from " + txtParentName.Text.Trim() + " towards the admission of " + txtFName.Text.Trim() + " for coaching of the academic year 2018 – 2020.<br/><br/>The course opted for is " + ddlProductName.SelectedItem.ToString() + "<br/><br/>Center Name: " + ddlPrefferedScCenter.SelectedItem.ToString() + "<br/><br/>Fees Received :" + txtCCDCAmount.Text.Trim() + "/- by Credit Card<br/><br/>Transaction ID: " + txtCCDCTransctionId.Text.Trim() + "<br/><br/>Note;<br/><br/>To avail the scholarship; kindly complete your ECS formalities at your respective center within 7 working days.<br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>Mahesh Tutorials - Science</b>";
                         }
                         else
                         {
                             Msg1.Body = "Dear Student/Parents, <br/><br/> Received with thanks an amount of Rs." + txtCCDCAmount.Text.Trim() + "/- <b>(RUPEES " + word + " ONLY)</b> from " + txtParentName.Text.Trim() + " towards the admission of " + txtFName.Text.Trim() + " for coaching of the academic year 2018 – 2020.<br/><br/>The course opted for is " + ddlProductName.SelectedItem.ToString() + "<br/><br/>Center Name: " + ddlPrefferedScCenter.SelectedItem.ToString() + "<br/><br/>Fees Received :" + txtCCDCAmount.Text.Trim() + "/- by Credit Card<br/><br/>Transaction ID: " + txtCCDCTransctionId.Text.Trim() + "<br/><br/>Note;<br/><br/>To avail the scholarship; kindly complete your ECS formalities at your respective center within 7 working days.<br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>Lakshya – Forum for Competitions</b>";
                         }
                     }
                     else if (ddlPayType.SelectedValue.ToString() == "01")//Cheque
                     {
                         word = ConvertNumbertoWords(Convert.ToInt32(txtChequeAmount.Text.Trim()));
                         if (ddlDivision.SelectedValue == "E0")
                         {
                             Msg1.Body = "Dear Student/Parents, <br/><br/> Received with thanks an amount of Rs." + txtChequeAmount.Text.Trim() + "/- <b>(RUPEES " + word + " ONLY)</b> from " + txtParentName.Text.Trim() + " towards the admission of " + txtFName.Text.Trim() + " for coaching of the academic year 2018 – 2020.<br/><br/>The course opted for is " + ddlProductName.SelectedItem.ToString() + "<br/><br/>Center Name: " + ddlPrefferedScCenter.SelectedItem.ToString() + "<br/><br/>Fees Received :" + txtChequeAmount.Text.Trim() + "/- by Cheque number " + txtChequeNo.Text.Trim() + " drawn on " + lblBankName.Text + "<br/><br/>Note;<br/><br/>To avail the scholarship; kindly complete your ECS formalities at your respective center within 7 working days.<br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>Mahesh Tutorials - Science</b>";
                         }
                         else
                         {
                             Msg1.Body = "Dear Student/Parents, <br/><br/> Received with thanks an amount of Rs." + txtChequeAmount.Text.Trim() + "/- <b>(RUPEES " + word + " ONLY)</b> from " + txtParentName.Text.Trim() + " towards the admission of " + txtFName.Text.Trim() + " for coaching of the academic year 2018 – 2020.<br/><br/>The course opted for is " + ddlProductName.SelectedItem.ToString() + "<br/><br/>Center Name: " + ddlPrefferedScCenter.SelectedItem.ToString() + "<br/><br/>Fees Received :" + txtChequeAmount.Text.Trim() + "/- by Cheque number " + txtChequeNo.Text.Trim() + " drawn on " + lblBankName.Text + "<br/><br/>Note;<br/><br/>To avail the scholarship; kindly complete your ECS formalities at your respective center within 7 working days.<br/><br/><br/><br/>This is an automated system generated Information for your reference. Please do not reply.<br/><b>Lakshya – Forum for Competitions</b>";
                         }
                     }

                     Msg1.IsBodyHtml = true;

                     bool value1 = System.Convert.ToBoolean(SSL);
                     SmtpClient smtp1 = new SmtpClient();
                     smtp1.Host = Host;
                     smtp1.EnableSsl = value1;
                     NetworkCredential NetworkCred1 = new NetworkCredential(userid, Password);
                     smtp1.UseDefaultCredentials = true;
                     smtp1.Credentials = NetworkCred1;
                     smtp1.Port = Port;

                     //int resultid1 = 0;
                     try
                     {
                         //smtp.Timeout = 20000;
                         smtp1.Send(Msg1);

                         resultid = ProductController.Insert_Mailog(EmailId, Msg1.Subject.ToString().Trim(), Msg1.Body.ToString().Trim(), 1, "", "1", "TAB", 1, "General Email", MailType);
                         count = count + 1;
                     }
                     catch (Exception ex)
                     {
                         resultid = ProductController.Insert_Mailog(EmailId, Msg1.Subject.ToString().Trim(), Msg1.Body.ToString().Trim(), 1, "", "2", "TAB", 1, "General Email", MailType);
                     }

                 }

                Show_Error_Success_Box("S", "Student Admission Saved Successfully....!");
                tblSearchInfo.Visible = false;
                tblSearchDetail.Visible = true;
                BtnSave.Visible = false;
                BtnClose.Visible = false;
                txtSearchUID.Text = "";
                return;
            }
            else
            {
                Show_Error_Success_Box("E", "Admission Not Saved.....!");
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
        Clear_Error_Success_Box();
        tblSearchInfo.Visible = false;
        tblSearchDetail.Visible = true;
        BtnSave.Visible = false;
        BtnClose.Visible = false;
        txtSearchUID.Text = "";
    }

    protected void ddlPrefferedScCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Product();
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

    private void BindOptionalSubject(string StreamName, string Center)
    {
        DataSet ds = ProductController.GetSubjectsbyStreamCode(5, StreamName, Center);
        if (ds.Tables[0] != null)
        {
            dlselective.DataSource = ds;
            dlselective.DataBind();
        }

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

    public static string ConvertNumbertoWords(int number)
    {
        if (number == 0)
            return "ZERO";
        if (number < 0)
            return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 10000000) > 0)
        {
            words += ConvertNumbertoWords(number / 10000000) + " CRORE ";
            number %= 10000000;
        }
        if ((number / 100000) > 0)
        {
            words += ConvertNumbertoWords(number / 100000) + " LAKH ";
            number %= 100000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "")
                words += "AND ";
            var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }

    private void FillDDL_Product()
    {
        try
        {
            ddlProductName.Items.Clear();
            DataSet dsProduct = ProductController.GetStreamby_Center_Seminar(ddlPrefferedScCenter.SelectedValue);
            BindDDL(ddlProductName, dsProduct, "Stream_SDesc", "Stream_Code");
            ddlProductName.Items.Insert(0, "Select");
            ddlProductName.SelectedIndex = 0;
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

    

    private void FillDDL_Division()
    {

        try
        {
            Clear_Error_Success_Box();
           // string UserID = txtUserId.Text.Trim();
            DataSet dsDivision = ProductController.GetDivision_Seminar();
            BindDDL(ddlDivision, dsDivision, "Source_Division_ShortDesc", "Source_Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
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


    private void FillDDL_CurrentSScCenter()
    {

        try
        {
            Clear_Error_Success_Box();
            DataSet dsCenter = ProductController.GetCenter_Seminar("", "1");
            BindDDL(ddlCurrentSSCCenter, dsCenter, "Source_Center_Name", "Source_Center_Code");
            ddlCurrentSSCCenter.Items.Insert(0, "Select");
            ddlCurrentSSCCenter.SelectedIndex = 0;
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

    private void FillDDL_PrefScCenter()
    {

        try
        {
            Clear_Error_Success_Box();
            DataSet dsCenter = ProductController.GetCenter_Seminar(ddlDivision.SelectedValue, "2");
            BindDDL(ddlPrefferedScCenter, dsCenter, "Source_Center_Name", "Source_Center_Code");
            ddlPrefferedScCenter.Items.Insert(0, "Select");
            ddlPrefferedScCenter.SelectedIndex = 0;
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
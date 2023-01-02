<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Customer_Verification.aspx.cs" Inherits="Customer_Verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row-fluid" style="text-align: Right;">
        <a href="Default.aspx" class="btn btn-app btn-success btn-mini radius-4">HOME</a>
    </div>
    <div class="row-fluid" id="divSeminar" runat="server">
        <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="alert alert-block alert-success" id="Msg_Success" visible="false" runat="server">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <strong><i class="icon-ok"></i></strong>
                        <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="alert alert-error" id="Msg_Error" visible="false" runat="server">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <strong><i class="icon-remove"></i>Error!</strong>
                        <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divAdddNewStud" runat="server">
            <div class="widget-header widget-header-small header-color-dark">
                <h5 class="modal-title">
                    <asp:Label runat="server" ID="lblDivHead">Please Fill Following Information</asp:Label>
                </h5>
            </div>
            <div class="row-fluid">
                <asp:UpdatePanel ID="upnlSearchInfo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table runat="server" id="tblSearchDetail" cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:Label runat="server" ID="Label2">UID</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <div class="input-append">
                                                    <asp:TextBox runat="server" ID="txtSearchUID" ToolTip="UID" Width="240px" type="number" />
                                                    <asp:LinkButton ID="lnkSearchInfo" ToolTip="Search" runat="server" Height="20px"
                                                        class="btn btn-purple icon-search" CommandName="SearchInfo" OnClick="lnkSearchInfo_Click">                                                               

                                                    </asp:LinkButton>
                                                </div>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="tblSearchInfo" visible="false" cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label3">UID</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtUID" Enabled="false" ToolTip="UId" type="number"
                                        Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label4">First Name</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label13">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtFName" CssClass="uppercase" ToolTip="First Name"
                                        ValidationGroup="UcValidate" type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                        ControlToValidate="txtFName" CssClass="red" ErrorMessage="Please Input Alphabets"
                                        ValidationGroup="UcValidate" Text="" SetFocusOnError="true" ValidationExpression="^[a-zA-Z]+[ a-zA-Z-_'.]*$" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label1">Middle Name</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtMName" CssClass="uppercase" ToolTip="Middle Name"
                                        ValidationGroup="UcValidate" type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMName"
                                        CssClass="red" ErrorMessage="Please Input Alphabets" ValidationGroup="UcValidate"
                                        Text="" SetFocusOnError="true" ValidationExpression="^[a-zA-Z]+[ a-zA-Z-_'.]*$" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label5">Last Name</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtLName" CssClass="uppercase" ValidationGroup="UcValidate"
                                        ToolTip="Last Name" type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLName"
                                        CssClass="red" ErrorMessage="Please Input Alphabets" ValidationGroup="UcValidate"
                                        Text="" SetFocusOnError="true" ValidationExpression="^[a-zA-Z]+[ a-zA-Z-_'.]*$" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label11">Student Contact No</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label15">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtContactNo" ToolTip="Contact Numbar" type="number"
                                        Width="265px" onkeypress="return NumberOnly()" ValidationGroup="UcValidate" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtContactNo"
                                        CssClass="red" ErrorMessage="Invalid Student Mobile Number" ValidationGroup="UcValidate"
                                        Text="" SetFocusOnError="true" ValidationExpression="^[0-9]{10,12}$" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label9">Student Email Id</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label17">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtStudentEmailId" CssClass="uppercase" ValidationGroup="UcValidate"
                                        ToolTip="Student Email Address" type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="EmailIDValidation" runat="server" ControlToValidate="txtStudentEmailId"
                                        ErrorMessage="Invalid Student Email Id" CssClass="red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="UcValidate" SetFocusOnError="True" Text=""></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label6">Address</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtAddress" CssClass="uppercase" ToolTip="Address"
                                        type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label7">Parent Name</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label55">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtParentName" CssClass="uppercase" ToolTip="Parents Name"
                                        type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label8">Parent Contact</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label14">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtParentContact" ToolTip="Parent Contact" type="number"
                                        onkeypress="return NumberOnly()" Width="265px" ValidationGroup="UcValidate" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtParentContact"
                                        CssClass="red" ErrorMessage="Invalid Parent Mobile Number" ValidationGroup="UcValidate"
                                        Text="" SetFocusOnError="true" ValidationExpression="^[0-9]{10,12}$" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label10">Parent Email Id</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtParentEmailId" CssClass="uppercase" ToolTip="Parent Email Address"
                                        ValidationGroup="UcValidate" type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtParentEmailId"
                                        ErrorMessage="Invalid Parent Email Id" CssClass="red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="UcValidate" SetFocusOnError="True" Text=""></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label19">Counselled By</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label39">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlCouncilBy" Width="280px" ToolTip="Council By"
                                        data-placeholder="Select Council" CssClass="chzn-select">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtParentEmailId"
                                        ErrorMessage="Invalid Parent Email Id" CssClass="red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="UcValidate" SetFocusOnError="True" Text=""></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label22">Accept Terms & Condition</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label16">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="true" Checked="false" OnCheckedChanged="chkCheck_CheckedChanged" />
                                    <span class="lbl"></span>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                        </table>
                        <div class="row-fluid" style="text-align: center;">
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                                Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_Click" Visible="false" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" runat="server"
                                Text="Close" OnClick="BtnClose_Click" Visible="false" />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkSearchInfo" />
                        <asp:PostBackTrigger ControlID="chkCheck" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="divApplyTermsCond" runat="server" visible="false">
            <div class="widget-header widget-header-small header-color-dark">
                <%--<h5 class="modal-title">                                
                            </h5>--%>
                <table cellpadding="3">
                    <tr>
                        <td class="span12" style="text-align: left">
                            <asp:Label runat="server" ID="Label12" Font-Bold="True">RULES AND REGULATIONS</asp:Label>
                            <asp:Label runat="server" ID="Label23" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row-fluid">
                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label24">                           
                                                        1.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label25">                           
                                                        Allotment of batch and session is solely at the discretion of the management.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label26">                           
                                                        2.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label27">                           
                                                        Attendance for lectures and examinations is essential for enhancing performance of the students.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label28">                           
                                                        3.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label29">                           
                                                        Students are discouraged from being absent from any class unless it is an unavoidable circumstances.
                                        </asp:Label><br />
                                        Permission for absenteeism will be given only for genuine reasons. Every absence
                                        must be followed by parents' or guardians' phone call on the same day, followed
                                        by a note sent with the student.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label30">                           
                                                        4.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label31">                           
                                                        Homework is compulsory. Action will be taken against defaulters.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label32">                           
                                                        5.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label33">                           
                                                        Any damage to the institute’s property will have to be compensated by the student responsible for the same.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label34">                           
                                                        6.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label35">                           
                                                       The Management reserves the right of taking any decision / action felt necessary against a student to maintain the decorum and morale of the institute. 
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label36">                           
                                                        7.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label37">                           
                                                        The Management has the right to change batch timings, sub-divide batches or increase or decrease the number of lectures per week.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label38">                           
                                                        8.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label40">
                                                        <%--Students are strictly prohibited from carrying cell phones / any other electronic gadgets to the class. --%>
                                                        In case of transfer or change in address, the student shall be accommodated in centre which is nearby to his new residence.
                                        </asp:Label><%--
                                        <asp:Label runat="server" ID="Label39" Font-Bold="True">                           
                                                        MHT-CET 'MT Educare Ltd.Science' Or for Advance & Foundation 'Lakshya Educare Pvt. Ltd.'
                                        </asp:Label>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label41">                           
                                                        9.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label42">                           
                                                        In case of change in syllabus, portion will be covered as per the availability of text book.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label43">                           
                                                        10.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label44" Font-Bold="true">                           
                                                        Fees paid for one subject / term cannot be adjustment for another subject / term or the subsequent year. Fees will not be transferable to any other student or course.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label45">                           
                                                        11.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        First down payment can be paid through
                                        <br />
                                        <b>• Credit or Debit Cards</b><br />
                                        <b>• DD drawn in favour of "LAKSHYA EDUCARE PRIVATE LIMITED"</b><br />
                                        <b>• NEFT Bank details for NEFT is as follows:</b><br />
                                        Bank Name: The Zoroastrian Co-operative Bank Ltd.<br />
                                        Bank Account No: 013300100005459<br />
                                        IFSC Code: ZCBL0000013<br />
                                        Branch : Charai, Thane 400601
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label47">                           
                                                        12.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label48">                           
                                                ECS Formalities for future EMIs has to be completed within 7 days from date of admission; else special offer benefit will be lapsed
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label49">                           
                                                        13.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label50">                           
                                                       EMI and FDP date of payment is fixed and any deviation from the same is not acceptable.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label20">                           
                                                        14.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        <asp:Label runat="server" ID="Label21">                           
                                                        Any change in GST / Other Taxes / Cess by the Government will be made applicable.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label46">                           
                                                        15.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        Fees once paid will neither refunded nor transferred under any circumstances
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label51">                           
                                                        16.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        A penalty of Rs. 200/- will be levied in case of any dishonored ECS
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label53">                           
                                                        17.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        Photograph(s) and testimonials of the students / parents may be used in any of MT EDUCARE’s and LAKSHYA Print, Electronic, Outdoor and Digital communications and advertising in India and Internationally.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label54">                           
                                                        18.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        Students / Parents will receive emails / SMS on latest offers / news / information.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!--<tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                        <asp:Label runat="server" ID="Label18">                           
                                                        19.
                                        </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%;">
                                        Students / Parents will receive emails / SMS on latest offers / news / information.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>-->
                    <tr>
                        <td class="span12" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: Right; width: 10%;">
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 90%; font-weight: bold;">
                                        I have read the above mentioned rules and regulations and agreed to abide by the
                                        same.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="row-fluid" style="text-align: center;">
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnApplyTermsCond1"
                        runat="server" Text="OK" OnClick="btnApplyTermsCond1_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

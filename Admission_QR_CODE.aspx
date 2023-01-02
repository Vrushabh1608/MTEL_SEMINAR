<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Admission_QR_CODE.aspx.cs" Inherits="Admission_QR_CODE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style type="text/css">
        body
        {
            width: 100%;
            text-align: center;
        }
        img
        {
            border: 0;
        }
        #main
        {
            margin: 15px auto;
            background: white;
            overflow: auto;
            width: 100%;
        }
        #header
        {
            background: white;
            margin-bottom: 15px;
        }
        #mainbody
        {
            background: white;
            width: 100%;
            display: none;
        }
        #footer
        {
            background: white;
        }
        #v
        {
            width: 300px;
            height: 240px;
        }
        #qr-canvas
        {
            display: none;
        }
        #qrfile
        {
            width: 300px;
            height: 240px;
        }
        #mp1
        {
            text-align: center;
            font-size: 35px;
        }
        #imghelp
        {
            position: relative;
            left: 0px;
            top: -160px;
            z-index: 100;
            font: 18px arial,sans-serif;
            background: #f0f0f0;
            margin-left: 35px;
            margin-right: 35px;
            padding-top: 10px;
            padding-bottom: 10px;
            border-radius: 20px;
        }
        .selector
        {
            margin: 0;
            padding: 0;
            cursor: pointer;
            margin-bottom: -5px;
        }
        #outdiv
        {
            width: 300px;
            height: 240px;
            border: solid;
            border-width: 3px 3px 3px 3px;
        }
        #result
        {
            border: solid;
            border-width: 1px 1px 1px 1px;
            padding: 20px;
            width: 70%;
        }
        
        ul
        {
            margin-bottom: 0;
            margin-right: 40px;
        }
        li
        {
            display: inline;
            padding-right: 0.5em;
            padding-left: 0.5em;
            font-weight: bold;
            border-right: 1px solid #333333;
        }
        li a
        {
            text-decoration: none;
            color: black;
        }
        
        #footer a
        {
            color: black;
        }
        .tsel
        {
            padding: 0;
        }
    </style>
    <script type="text/javascript" src="QR_JS_IMAGE/llqrcode.js"></script>
    <script type="text/javascript" src="../apis.google.com/js/plusone.js"></script>
    <script type="text/javascript" src="QR_JS_IMAGE/webqr.js"></script>
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
                                    <div id="main">
                                        <div id="mainbody">
                                            <table class="tsel" border="0" width="100%">
                                                <tr>
                                                    <td valign="top" class="span12" style="text-align: left">
                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2" >
                                                                    <table>
                                                                        <tr>
                                                                            <td style="border-style: none; text-align: left; width: 50%;">
                                                                                <img class="selector" id="webcamimg" src="QR_JS_IMAGE/vid.png" onclick="setwebcam()"
                                                                                    align="left" />
                                                                            </td>
                                                                            <td style="border-style: none; text-align: right; width: 50%;">
                                                                                <div class="input-append">
                                                                                    <img class="selector" id="qrimg" src="QR_JS_IMAGE/cam.png" onclick="setimg()" align="right" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: center; width: 60%;" colspan="2">
                                                                    <div id="outdiv">
                                                                    </div>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2">
                                                                    <img src="QR_JS_IMAGE/down.png" />
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2">
                                                                    <div id="result">
                                                                    </div>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2">
                                                                    <asp:TextBox runat="server" ID="txtSearchUID" ToolTip="UID" Width="150px" ClientIDMode="Static"
                                                                        Text="" OnTextChanged="txtSearchUID_TextChanged" AutoPostBack="true" />
                                                                    <asp:LinkButton ID="lnkSearchInfo" ToolTip="Search" runat="server" Height="20px"
                                                                        class="btn btn-purple icon-search" CommandName="SearchInfo" OnClick="lnkSearchInfo_Click"
                                                                        Visible="false">                                                               
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <canvas id="qr-canvas" width="800" height="600">
                                    </canvas>
                                </td>
                            </tr>
                        </table>
                        <script type="text/javascript">load();</script>
                        <table runat="server" id="tblSearchInfo" visible="false" cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label3">UID</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                <asp:TextBox runat="server" ID="txtUID" Enabled="false" ToolTip="UId"
                                        Width="265px" />
                                    <%--<asp:TextBox runat="server" ID="txtUID" Enabled="false" ToolTip="UId" type="number"
                                        Width="265px" />--%>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="lbl4">First Name</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label1">*</asp:Label>
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
                                    <asp:Label runat="server" ID="lbl3">Middle Name</asp:Label>
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
                                    <asp:Label runat="server" CssClass="red" ID="lbl2">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtContactNo" ToolTip="Contact Numbar" type="number"
                                        Width="265px" onkeypress="return NumberOnly()" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label9">Student Email Id</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label33">*</asp:Label>
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
                                    <asp:Label runat="server" ID="Lbl1w">Address</asp:Label>
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
                                    <asp:Label runat="server" CssClass="red" ID="Label34">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtParentName" CssClass="uppercase" ToolTip="First Name"
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
                                    <asp:Label runat="server" CssClass="red" ID="lbl6">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtParentContact" ToolTip="Parent Contact" type="number"
                                        Width="265px" onkeypress="return NumberOnly()" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
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
                                    <asp:Label runat="server" ID="Label37">Council By</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlCouncilBy" Width="280px" ToolTip="Council By"
                                        Enabled="false" data-placeholder="Select Council" CssClass="chzn-select">
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
                                    <asp:Label runat="server" ID="Label12">Current SSC Center</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label30">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlCurrentSSCCenter" Width="280px" ToolTip="Current SSC Center"
                                        data-placeholder="Select Center" CssClass="chzn-select">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label22">Division</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label31">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlDivision" Width="280px" ToolTip="Division"
                                        data-placeholder="Select Division" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        CssClass="chzn-select">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label13">Preffered Science Center</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label32">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlPrefferedScCenter" Width="280px" ToolTip="Preffered SSC Center"
                                        data-placeholder="Select Center" AutoPostBack="true" OnSelectedIndexChanged="ddlPrefferedScCenter_SelectedIndexChanged"
                                        CssClass="chzn-select">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label17">Product Interested In</asp:Label>
                                    <asp:Label runat="server" ID="Label18" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlProductName" Width="280px" ToolTip="Product"
                                        CssClass="chzn-select" data-placeholder="Select Product" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label51">Product / Item Group Selection</asp:Label>
                                    <asp:Label runat="server" ID="Label4" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DataList ID="dlselective" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                        Height="20px">
                                        <HeaderTemplate>
                                            <b>Select</b></th>
                                            <th width="80%">
                                                Item
                                            </th>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckhselect1" runat="server" /><span class="lbl"> </span>
                                            <td width="20%">
                                                <asp:Label ID="lblvoucherDesc" Text='<%#DataBinder.Eval(Container.DataItem, "SGR_DESC")%>'
                                                    runat="server"></asp:Label>
                                            </td>
                                            <asp:Label ID="lblmaterialcodeadd" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SGR_Material")%>'></asp:Label>
                                            <asp:Label ID="lblvoucher_typ" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Typ")%>'></asp:Label>
                                            <asp:Label ID="lblvoucheramt" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Amount")%>'></asp:Label>
                                            <asp:Label ID="lblbaseuom" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "Uom_Base_Name")%>'></asp:Label>
                                            <asp:Label ID="lblbaseuomid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Uom")%>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label23">Pay Mode</asp:Label>
                                    <asp:Label runat="server" ID="Label6" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlPayMode" Width="280px" ToolTip="Pay Mode"
                                        data-placeholder="Select Pay Mode" CssClass="chzn-select">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">FDP</asp:ListItem>
                                        <asp:ListItem Value="2">EMI</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label14">Pay Type</asp:Label>
                                    <asp:Label runat="server" ID="Label24" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:DropDownList runat="server" ID="ddlPayType" Width="280px" ToolTip="Pay Type"
                                        data-placeholder="Select Pay Type" AutoPostBack="true" CssClass="chzn-select"
                                        OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="04">Credit/Debit Card</asp:ListItem>
                                        <asp:ListItem Value="05">NEFT</asp:ListItem>
                                        <asp:ListItem Value="02">Demand Draft</asp:ListItem>
                                        <asp:ListItem Value="01">Cheque</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr id="trCCDCAmount" runat="server" visible="false">
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label19">Amount</asp:Label>
                                    <asp:Label runat="server" ID="Label25" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtCCDCAmount" ToolTip="CCDC Amount" type="number"
                                        onkeypress="return NumberOnly()" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr id="trCCDCTransactionId" runat="server" visible="false">
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label20">Transaction ID</asp:Label>
                                    <asp:Label runat="server" ID="Label26" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtCCDCTransctionId" ToolTip="CCDC Transaction Id"
                                        type="text" Width="265px" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr id="trChequeDate" runat="server" visible="false">
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label35">Date</asp:Label>
                                    <asp:Label runat="server" ID="Label36" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <input readonly="readonly" class="date-picker" id="txtChequeDate" runat="server"
                                        type="text" data-date-format="dd M yyyy" style="width: 260px;" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr id="trChequeAmount" runat="server" visible="false">
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label15">Amount</asp:Label>
                                    <asp:Label runat="server" ID="Label27" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtChequeAmount" ToolTip="Cheque Amount" type="number"
                                        Width="265px" onkeypress="return NumberOnly()" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                            <tr id="trMICRCode" runat="server" visible="false">
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label16">MICR Code</asp:Label>
                                    <asp:Label runat="server" ID="Label28" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtMICRCode" ToolTip="MICR Code" type="number" Width="265px"
                                        AutoPostBack="true" OnTextChanged="txtMICRCode_TextChanged" onkeypress="return NumberOnly()" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                    <asp:Label runat="server" ID="lblBankName"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trChequeNumber" runat="server" visible="false">
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label21">Number</asp:Label>
                                    <asp:Label runat="server" ID="Label29" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:TextBox runat="server" ID="txtChequeNo" ToolTip="Cheque No" type="number" Width="265px"
                                        onkeypress="return NumberOnly()" MaxLength="6" />
                                </td>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                            </tr>
                        </table>
                        <div class="row-fluid" style="text-align: center;">
                            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlSave">
                                <ProgressTemplate>
                                    <img alt="" src="WaitLoad.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="pnlSave" runat="server">
                                <ContentTemplate>--%>
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                                Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_Click" Visible="false" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" runat="server"
                                Text="Close" OnClick="BtnClose_Click" Visible="false" />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkSearchInfo" />
                        <asp:AsyncPostBackTrigger ControlID="txtMICRCode" />
                        <asp:PostBackTrigger ControlID="BtnSave" />
                        <asp:PostBackTrigger ControlID="txtSearchUID" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

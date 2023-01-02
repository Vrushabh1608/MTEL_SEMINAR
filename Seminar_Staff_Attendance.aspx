<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Seminar_Staff_Attendance.aspx.cs" Inherits="Seminar_Staff_Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Event Staff Attendance<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-10" Visible="false" runat="server"
                ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                OnClick="BtnAdd_Click" Text="AddEvnt" />
            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" runat="server" ID="Btnnewstaff"
                OnClick="BtnAdd_newstaffClick" Text="NewStaff" />
                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" runat="server" ID="btnreport"
                OnClick="Btnrpt_staffAttendanceClick" Text="Report" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->
        <div class="row-fluid">
            <!-- -->
            <!-- PAGE CONTENT BEGINS HERE -->
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
            <div id="DivSearchPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Search Options
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" ToolTip="Academic Year"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29">Date</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12">Event Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txteventName" ToolTip="Test Name" type="text" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="25px" ToolTip="Export"
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    OnItemCommand="dlGridDisplay_ItemCommand" runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Event Date</b> </th>
                        <th style="width: 20%; text-align: left;">
                            Event Name
                        </th>
                        <th style="width: 20%; text-align: left;">
                            Acad Year
                        </th>
                        <th style="width: 20%; text-align: left;">
                            Venue
                        </th>
                        <th style="width: 15%; text-align: left;">
                            Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Event_Date")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Event_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Venue")%>' />
                            <asp:Label ID="lblPK" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' />
                        </td>
                        <td style="width: 100px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="ADD Staff Details" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Edit" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"EditButtonDisplayFlag")== 1 ? true : false%>' />
                            <asp:LinkButton ID="LnkAddedit" ToolTip="Print Event ID" class="btn-small btn-primary btn-success icon-info-sign "
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Add_Edit" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"EditButtonDisplayFlag")== 1 ? true : false%>' />
                    </ItemTemplate>
                </asp:DataList>
               
                
            </div>
            <div id="DivResultstaff" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="Label17" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="LinkButton1" Font-Underline="true" Height="25px" ToolTip="Export"
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <asp:DataList ID="ddlreportgrid" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                            <b>Name</b> </th>
                            <th style="width: 20%; text-align: center;">
                             Event Date
                        </th>
                         <th style="width: 20%; text-align: center;">
                            Venue
                        </th>
                        <th style="width: 20%; text-align: center;">
                            In Time
                        </th>
                        <th style="width: 20%; text-align: center;">
                           Out Time
                        </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Name")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Log_Date")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Venue")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"INtime")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Outtime")%>' />
                    
                        </ItemTemplate>
                </asp:DataList>
                <div id="divexport" runat="server" style="display: none">
                    <asp:DataList ID="ddlreportgridexport" runat="server" HorizontalAlign="Left" CssClass="table table-striped table-bordered table-hover"
                        Width="100%">
                        <HeaderTemplate>
                            <b>Name</b> </th>
                            <th style="width: 5%; text-align: left;">
                             Event Date
                        </th>
                         <th style="width: 5%; text-align: left;">
                            Venue
                        </th>
                        <th style="width: 5%; text-align: left;">
                            In Time
                        </th>
                        <th style="width: 5%; text-align: left;">
                           Out Time
                        </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Name")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Log_Date")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Venue")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"INtime")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Outtime")%>' />
                    
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Add" runat="server" Text="Create Events For Staff Attendance" />
                    </h5>
                    <asp:Label ID="lblTestPKey_Hidden" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label23">Event Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEventName_Add" ToolTip="Test Name" type="text"
                                                                Width="205px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label4" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlAcadYear_Add" Width="215px" ToolTip="Academic Year"
                                                                data-placeholder="Select Acad Year" CssClass="chzn-select" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlAcadYear_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" class="red" ID="lblAcadYear_Add" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label2">Venue</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtVenue" ToolTip="Test Name" type="text" Width="205px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label3">Event Date</asp:Label>
                                                        </td>
                                                        <td>
                                                            <input readonly="readonly" class="date-picker" id="txtChequeDate" runat="server"
                                                                type="text" style="width: 205px;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            OnClick="BtnCloseAdd_Click" runat="server" Text="Close" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivEditPanelAs" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_edit" runat="server" Text="Add Staff Details " />
                    </h5>
                    <asp:Label ID="lblPK" runat="server" Text="" Visible="false" />
                    <asp:Label ID="LblPkeyCode" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DataList ID="Datasetstaffdata" CssClass="table table-striped table-bordered table-hover"
                                        OnItemCommand="dlGridDisplay_ItemCommand" runat="server" Width="100%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                AutoPostBack="true" />
                                            <span class="lbl"></span></th>
                                            <th align="left">
                                                Name
                                            </th>
                                            <th align="left">
                                                Employee/Partner Code
                                            </th>
                                            <th align="left">
                                                Role
                                            </th>
                                            <th align="left">
                                                RFID No
                                            </th>
                                            <th align="left">
                                                Action
                                            </th>
                                            <th align="left">
                                                ErrorSaveMessage
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCheck" runat="server" OnCheckedChanged="chkCheck_CheckedChanged"
                                                AutoPostBack="true" />
                                            <span class="lbl"></span></td>
                                            <td>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>'
                                                    Visible="false" />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblusername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Name")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblrolename" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Role_Name")%>' />
                                                <asp:Label ID="LblPkeyCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PKEY")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <%-- <asp:TextBox ID="txtrfid" runat="server" Visible="false" Width="105px" onkeypress="return NumberOnly(event);">
                                    </asp:TextBox>--%>
                                                <asp:Label ID="lblrifd" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"RFID_CardNo")%>' />
                                            </td>
                                            <td style="width: 100px; text-align: center;">
                                                <asp:LinkButton ID="lnkEditInfo" ToolTip="Remove" class="btn-small btn-primary icon-info-sign"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKEY")%>' runat="server"
                                                    CommandName="Remove" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"RemoveButtonDisplayFlag")== 1 ? true : false%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblErrorSaveMessage" runat="server" Text='' />
                                            </td>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label58" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btn_save_edit"
                            runat="server" Text="Save" ValidationGroup="UcValidate" OnClick="btn_save_edit_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btn_close" Visible="true"
                            runat="server" Text="Close" OnClick="btn_close_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivaddRemove" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="Label1" runat="server" Text="Add Staff Details " />
                    </h5>
                    <asp:Label ID="Label6" runat="server" Text="" Visible="false" />
                    <asp:Label ID="Label7" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DataList ID="Datasetstaffdata_addremove" CssClass="table table-striped table-bordered table-hover"
                                        OnItemCommand="dlGridDisplay_ItemCommand" runat="server" Width="100%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                AutoPostBack="true" />
                                            <span class="lbl"></span></th>
                                            <th align="left">
                                                Name
                                            </th>
                                            <th align="left">
                                                Employee/Partner Code
                                            </th>
                                            <th align="left">
                                                Role
                                            </th>
                                            <th align="left">
                                                RFID No
                                            </th>
                                            <th align="left">
                                                Action
                                            </th>
                                            <th align="left">
                                                ErrorSaveMessage
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCheck_Edit" runat="server" OnCheckedChanged="chkCheck_CheckedChanged"
                                                AutoPostBack="true" />
                                            <span class="lbl"></span></td>
                                            <td>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                                                <asp:Label ID="lblRowNoedite" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>'
                                                    Visible="false" />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblusername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Name")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblrolename" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Role_Name")%>' />
                                                <asp:Label ID="LblPkeyCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PKEY")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtrfid" runat="server" Visible="false" Width="105px" onkeypress="return NumberOnly(event);">
                                                </asp:TextBox>
                                                <asp:Label ID="lblrifd" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"RFID_CardNo")%>' />
                                            </td>
                                            <td style="width: 100px; text-align: center;">
                                                <asp:LinkButton ID="lnkEditInfo" ToolTip="Remove" class="btn-small btn-primary icon-info-sign"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKEY")%>' runat="server"
                                                    CommandName="Remove" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"RemoveButtonDisplayFlag")== 1 ? true : false%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblErrorSaveMessage" runat="server" Text='' />
                                            </td>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label8" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button2" runat="server"
                            Text="Print-Staff-I-Card" Width="125px" Font-Bold="True" OnClick="btnPrintICard_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button3" Visible="true"
                            runat="server" Text="Close" OnClick="btn_close_Click" />
                        <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="Divaddnewstaff" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lbladdnewstaff" runat="server" Text="Create Events For Staff Attendance" />
                    </h5>
                    <asp:Label ID="Label13" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table runat="server" id="tblnewstaff" visible="false" cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:Label runat="server" ID="Label9"> Staff Full Name</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 50%;">
                                               
                                                    <asp:TextBox runat="server" ID="txtFName" CssClass="uppercase" ToolTip="First Name"
                                                    ValidationGroup="UcValidate" type="text" Width="265px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:Label runat="server" ID="Label10">Emp ID</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 50%;">
                                                 <asp:TextBox runat="server" ID="txtempid" type="number"
                                                    Width="265px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                             <asp:Label runat="server" ID="Label14">RFID Card ID</asp:Label>
                                           
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 50%;">
                                                     <asp:TextBox runat="server" ID="txtnewRifd" 
                                                    ValidationGroup="UcValidate" type="text" Width="265px" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label20" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Btnnewstaffsave" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSavenewstaffdetails_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Btnnewstaffcloss" Visible="true"
                            OnClick="BtnCloseAdd_Click" runat="server" Text="Close" />
                        <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
         <div id="DivAttendanceReport" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="Label11" runat="server" Text="Report Staff Attendance" />
                    </h5>
                    <asp:Label ID="Label15" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <%--<tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label18" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="DDL_ReportAcadyear" Width="215px" ToolTip="Academic Year"
                                                                data-placeholder="Select Acad Year" CssClass="chzn-select"/>
                                                         
                                                        </td>
                                                    </tr>--%>
                                                     <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label25">Event Date</asp:Label>
                                                        </td>
                                                        <td>
                                                            <input readonly="readonly" class="date-picker" id="txtrptdate" runat="server"
                                                                type="text" style="width: 205px;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                   
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" Visible="false" ID="Label24">Venue</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="TextBox2" Visible="false" ToolTip="Test Name" type="text" Width="205px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                      
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="Button4"
                                    Text="Get" ToolTip="Search" OnClick="Btnget_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="Button5" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                            </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
</asp:Content>

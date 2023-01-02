<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="invitation_QR.aspx.cs" Inherits="invitation_QR" %>

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
        <asp:Button class="btn  btn-app btn-primary btn-mini radius-10" runat="server" ID="BtnAdd"
            OnClick="BtnAdd_Click" Text="Upload" />
        <asp:Button class="btn  btn-app btn-primary btn-mini radius-10" Visible="true" runat="server"
            ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
    </div>
    <div id="page-content" class="clearfix">
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
                            <asp:Label ID="lblerror" Text="Label" Visible="false" runat="server" ></asp:Label>
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
                                                                    <asp:Label ID="lblacadyearerror" CssClass="red" Text="Label" Visible="false" runat="server" ></asp:Label>
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td class="span4" style="text-align: left">
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
                                                </td>--%>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label12">Center Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="DDlcentername" Width="215px" ToolTip="center Year"
                                                                    data-placeholder="Select Center Name" CssClass="chzn-select" />
                                                                    <asp:Label ID="lblerrorcenter" CssClass="red" Text="Label" Visible="false" runat="server" ></asp:Label>
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
                <div id="divAdddNewStud" visible="false" runat="server">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Total No of Student :
                            <asp:Label runat="server" ID="lblStudentCount"></asp:Label>
                        </h5>
                    </div>
                    <div class="row-fluid">
                        <asp:UpdatePanel ID="upnlSearchInfo" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DataList ID="dlSeminarStudent" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover">
                                    <HeaderTemplate>
                                        <b style="text-align: center">
                                            <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="true" OnCheckedChanged="chkAllStudent_CheckedChanged" />
                                            <span class="lbl"></span></b></th>
                                        <th width="70%" style="text-align: center">
                                            Student Name
                                        </th>
                                        <th width="20%" style="text-align: center">
                                            UID
                                        </th>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkStudent" runat="server" />
                                        <span class="lbl"></span></td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblStudentName" Text='<%#DataBinder.Eval(Container.DataItem, "StudentName")%>'
                                                runat="server"></asp:Label>
                                            <asp:Label ID="lblCenter" Text='<%#DataBinder.Eval(Container.DataItem, "Center")%>'
                                                runat="server" Visible="false"></asp:Label>
                                            <%-- <asp:Label ID="lblBoard" Text='<%#DataBinder.Eval(Container.DataItem, "Board")%>'
                                        runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblStudRank" Text='<%#DataBinder.Eval(Container.DataItem, "StudRank")%>'
                                        runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPercentile" Text='<%#DataBinder.Eval(Container.DataItem, "Percentile")%>'
                                        runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblYourGuide" Text='<%#DataBinder.Eval(Container.DataItem, "YourGuide")%>'
                                        runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCounterNo" Text='<%#DataBinder.Eval(Container.DataItem, "CounterNo")%>'
                                        runat="server" Visible="false"></asp:Label>--%>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblUID" Text='<%#DataBinder.Eval(Container.DataItem, "UID")%>' runat="server"></asp:Label>
                                        </td>
                                    </ItemTemplate>
                                </asp:DataList>
                                <div class="row-fluid" style="text-align: center;">
                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnGenerateQRCode"
                                        runat="server" Text="Generate QRCode" Width="165px" Font-Bold="True" OnClick="BtnGenerateQRCode_Click" />
                                    <%-- <asp:Button Visible="false" class="btn btn-app btn-primary btn-mini radius-4" ID="btnPrintICard"
                                runat="server" Text="Print I-Card" Width="125px" Font-Bold="True" onclick="btnPrintICard_Click" 
                                 />--%>
                                    <%-- <asp:Button Visible="false" class="btn btn-app btn-success btn-mini radius-4" ID="btnIndivisualICard"
                                runat="server" Text="Indivisual ICard" Width="165px" Font-Bold="True" 
                                onclick="btnIndivisualICard_Click" />--%>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BtnGenerateQRCode" />
                                <%--<asp:PostBackTrigger ControlID="btnPrintICard" />
                        <asp:PostBackTrigger ControlID="btnIndivisualICard" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="DivNew_Upload" visible="true" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Upload Student Data
                            <asp:Label ID="lblimport" runat="server" Visible="false"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <div id="search" runat="server">
                                    <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none; width: 100%;" class="table-hover"
                                                    runat="server" id="Table4">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 15%;">
                                                            <asp:Label ID="Label10" runat="server" ForeColor="Red">Select File</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 85%;">
                                                            <asp:FileUpload ID="uploadfile" runat="server" />
                                                            <asp:Label ID="lblfilepath" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnUpload"
                                                    Text="Upload" ToolTip="Upload" ValidationGroup="UcValidateSearch" OnClick="btnUpload_Click" />
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="New_UploadGrid" runat="server" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="prefix" id="New_UploadGrid1" runat="server" visible="false" style="overflow-x: scroll !important;
                    height: 400PX;">
                    <asp:DataList ID="datalist_NewUploads1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible="True" Style="overflow: scroll">
                        <HeaderTemplate>
                            <b>SPID</b> </th>
                            <th style="text-align: center; width: 15%;">
                                Con ID
                            </th>
                            <th style="text-align: center; width: 15%;">
                                CenterCode
                            </th>
                            <th style="text-align: center; width: 15%;">
                                Center Name
                            </th>
                            <th style="text-align: center; width: 15%;">
                                Acad Year
                            </th>
                            <th style="text-align: center; width: 15%;">
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblspid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SPID")%>' />
                            </td>
                            <td style="text-align: Center; width: 03px;">
                                <asp:Label ID="lblconID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Con_Id")%>' />
                            </td>
                            <td style="text-align: Center; width: 03px;">
                                <asp:Label ID="lblcentercode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Code")%>' />
                            </td>
                            <td style="text-align: Center; width: 03px;">
                                <asp:Label ID="lblcentername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Name")%>' />
                            </td>
                            <td style="text-align: Center; width: 03px;">
                                <asp:Label ID="LblAcadyear" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acadyear")%>' />
                            </td>
                            <td style="text-align: Center; width: 20px;">
                                <asp:Label ID="lblSTATUS" runat="server" Text="" />
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div runat="server" class="widget-main alert-block alert-info" id="Divbtnimport"
                    style="text-align: center;">
                    <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnImport"
                        Text="Save" ToolTip="Import" ValidationGroup="UcValidateSearch" OnClick="btnImport_Click" />
                    <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnClose"
                        Text="Close" ToolTip="Close" ValidationGroup="UcValidateSearch" OnClick="btnClose_Click" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

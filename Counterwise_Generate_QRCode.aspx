<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Counterwise_Generate_QRCode.aspx.cs" Inherits="Counterwise_Generate_QRCode" %>

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
                <%--<asp:UpdatePanel ID="upnlSearchInfo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <table runat="server" id="tblSearchDetail" cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:Label runat="server" ID="Label2">Table No.</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:DropDownList runat="server" ID="ddlTableNo" Width="215px" ToolTip="Table No"
                                                    data-placeholder="Select Table No" CssClass="chzn-select">
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
                                                <asp:Label runat="server" ID="Label1">Center</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:DropDownList runat="server" ID="ddlCenter" Width="215px" ToolTip="Center"
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
                                                <asp:Label runat="server" ID="Label3">Session</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:DropDownList runat="server" ID="ddlSession" Width="215px" ToolTip="Session"
                                                    data-placeholder="Select Session" CssClass="chzn-select">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-style: none; text-align: center; width: 100%;" colspan="4">
                                                <asp:LinkButton ID="lnkSearchInfo" ToolTip="Search" runat="server" Height="20px"
                                                    class="btn btn-purple icon-search" CommandName="SearchInfo" OnClick="lnkSearchInfo_Click">                                                               
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkSearchInfo" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
        <div id="divResult" runat="server" visible="false">
            <div class="widget-header widget-header-small header-color-dark">
                <h5 class="modal-title">
                    Total No of Student :
                    <asp:Label runat="server" ID="lblStudentCount"></asp:Label>
                </h5>
                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnPrint"
                                runat="server" Text="Print" Width="50px" Font-Bold="True" 
                    onclick="BtnPrint_Click" />
                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnBack"
                                runat="server" Text="Back" Width="50px" Font-Bold="True" 
                    onclick="btnBack_Click" />                
            </div>
            <div class="row-fluid">
                <asp:DataList ID="dlSeminarStudent" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover">
                    <HeaderTemplate>
                        <b style="text-align: center">
                            <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="true" OnCheckedChanged="chkAllStudent_CheckedChanged" />
                            <span class="lbl"></span></b></th>
                        <th width="10%" style="text-align: center">
                            Table No
                        </th>
                        <th width="45%" style="text-align: center">
                            Student Name
                        </th>
                        <th width="10%" style="text-align: center">
                            MobileNo
                        </th>
                        <th width="20%" style="text-align: center">
                            UID
                        </th>
                        <th width="10%" style="text-align: center">
                            Attendance
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server" />
                        <span class="lbl"></span></td>
                        <td style="text-align: center">
                            <asp:Label ID="lblCounterNo" Text='<%#DataBinder.Eval(Container.DataItem, "CounterNo")%>'
                                runat="server"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblStudentName" Text='<%#DataBinder.Eval(Container.DataItem, "StudentName")%>'
                                runat="server"></asp:Label>
                            <asp:Label ID="lblCenter" Text='<%#DataBinder.Eval(Container.DataItem, "Center")%>'
                                runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblBoard" Text='<%#DataBinder.Eval(Container.DataItem, "Board")%>'
                                runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblStudRank" Text='<%#DataBinder.Eval(Container.DataItem, "StudRank")%>'
                                runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblPercentile" Text='<%#DataBinder.Eval(Container.DataItem, "Percentile")%>'
                                runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblYourGuide" Text='<%#DataBinder.Eval(Container.DataItem, "YourGuide")%>'
                                runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMobileNo" Text='<%#DataBinder.Eval(Container.DataItem, "MobileNo")%>' runat="server"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblUID" Text='<%#DataBinder.Eval(Container.DataItem, "UID")%>' runat="server"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblAttendance" Text='<%#DataBinder.Eval(Container.DataItem, "Attendance")%>' runat="server"></asp:Label>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>

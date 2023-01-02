<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Generate_QRCode.aspx.cs" Inherits="Generate_QRCode" %>

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
                    Total No of Student : <asp:Label runat="server" ID="lblStudentCount"></asp:Label>
                </h5>
            </div>
            <div class="row-fluid">
                <asp:UpdatePanel ID="upnlSearchInfo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="dlSeminarStudent" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover">
                            <HeaderTemplate>
                                    <b style="text-align: center">
                                    <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="true" OnCheckedChanged="chkAllStudent_CheckedChanged" />
                                                            <span class="lbl"></span>
                                    </b>
                                </th>
                                <th width="70%" style="text-align: center">
                                    Student Name
                                </th>
                                <th width="20%" style="text-align: center">
                                    UID
                                </th>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <asp:CheckBox ID="chkStudent" runat="server" />
                                                            <span class="lbl"></span>
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
                                    <asp:Label ID="lblCounterNo" Text='<%#DataBinder.Eval(Container.DataItem, "CounterNo")%>'
                                        runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblUID" Text='<%#DataBinder.Eval(Container.DataItem, "UID")%>'
                                        runat="server"></asp:Label>
                                </td>                                
                            </ItemTemplate>
                        </asp:DataList>
                        <div class="row-fluid" style="text-align: center;">
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnGenerateQRCode"
                                runat="server" Text="Generate QRCode" Width="165px" Font-Bold="True" 
                                onclick="BtnGenerateQRCode_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnPrintICard"
                                runat="server" Text="Print I-Card" Width="125px" Font-Bold="True" onclick="btnPrintICard_Click" 
                                 />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnIndivisualICard"
                                runat="server" Text="Indivisual ICard" Width="165px" Font-Bold="True" 
                                onclick="btnIndivisualICard_Click" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnGenerateQRCode" />
                        <asp:PostBackTrigger ControlID="btnPrintICard" />
                        <asp:PostBackTrigger ControlID="btnIndivisualICard" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

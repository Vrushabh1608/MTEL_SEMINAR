<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Seminar_Report.aspx.cs" Inherits="Seminar_Report" %>

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
    <div class="row-fluid" style="text-align: left;">
        <button id="btnclosesession1" runat="server" class="btn btn-app btn-purple radius-4"
            onserverclick="btnclosesession1_Click">
            Close Session 1</button>
        <button id="btnclosesession2" runat="server" class="btn btn-app btn-warning radius-4"
            onserverclick="btnclosesession2_Click">
            Close Session 2</button>
        <a href="Seminar_Report.aspx" class="btn btn-app btn-primary radius-4"><i class="icon-refresh">
        </i>Reload </a></a>
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
                    <asp:Label runat="server" ID="lblDivHead">Seminar Detail</asp:Label>
                </h5>
                <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt left"
                    Height="25px" OnClick="HLExport_Click" Visible="false" />
            </div>
            <div class="row-fluid">
                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                    <tr>
                        <td style="border-style: none; text-align: left; width: 100%;">
                            <asp:GridView ID="grdDisplaySeminarDetail" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Attendance.aspx.cs" Inherits="Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        $('#preview1').on('load', function () {
            var css;
            var ratio = $(this).width() / $(this).height();
            var pratio = $(this).parent().width() / $(this).parent().height();
            if (ratio < pratio) {
                css = { width: 'auto', height: '100%' };
            } else {
                css = { width: '100%', height: 'auto' };
            }
            $(this).css(css);
        });

        function previewImage(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#preview1').attr('src', e.target.result)
                };
                reader.readAsDataURL(input.files[0]);

            }
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
                                                <asp:Label runat="server" ID="Label1">Choose QR code image to read/scan:</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <div>
                                                    <input type='file' accept="image/*" capture="camera" onchange="previewImage(this);"
                                                        runat="server" id="fileupload" />
                                                </div>
                                                <div style="height: 100px; width: 200px">
                                                    <img id="preview1" src="" alt="your image here" width="100px" height="100px" />
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnReadQRCode"
                                                            runat="server" Text="Ok" 
                                                        onclick="btnReadQRCode_Click"  />
                                                </div>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                <asp:Image ID="pictureBox1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:Label runat="server" ID="Label2">UID</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <div class="input-append">
                                                    <asp:TextBox runat="server" ID="txtSearchUID" ToolTip="UID" Width="150px" type="number" />
                                                    <asp:LinkButton ID="lnkSearchInfo" ToolTip="Search" runat="server" Height="20px"
                                                        class="btn btn-purple icon-search" CommandName="SearchInfo" OnClick="lnkSearchInfo_Click">                                                               
                                                    </asp:LinkButton>
                                                    <input type="number" id="code" />
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
                                <td style="border-style: none; text-align: left; width: 50%;">
                                    <%--<asp:TextBox runat="server" ID="txtUID" Enabled="false" ToolTip="UId" type="number" Width="265px"/>--%>
                                    <asp:Label runat="server" ID="txtUID"></asp:Label>
                                </td>
                                <%-- <td style="border-style: none; text-align: left; width: 20%;">
                                </td>   --%>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label4">Student Name</asp:Label>
                                    <%-- <asp:Label runat="server" CssClass="red" ID="Label13">*</asp:Label>--%>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;">
                                    <%-- <asp:TextBox runat="server" ID="txtFName" CssClass="uppercase" ToolTip="First Name"  ValidationGroup="UcValidate" type="text" Width="265px"/>                                    --%>
                                    <asp:Label runat="server" ID="lblStudentName"></asp:Label>
                                </td>
                                <%-- <td style="border-style: none; text-align: left; width: 20%;">                                    
                                    
                                </td> --%>
                            </tr>
                            <tr>
                                <td style="border-style: none; text-align: left; width: 20%;">
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">
                                    <asp:Label runat="server" ID="Label7">Parent Name</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;">
                                    <asp:Label runat="server" ID="lblParentName">Parent Name</asp:Label>
                                    <asp:Label runat="server" ID="lblContactNumber" Visible="false"></asp:Label>
                                </td>
                                <%--<td style="border-style: none; text-align: left; width: 20%;">
                                </td> --%>
                            </tr>
                        </table>
                        <div class="row-fluid" style="text-align: center;">
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAttendance"
                                runat="server" Text="Mark Attendance" Visible="false" Width="120px" OnClick="BtnSaveAttendance_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" runat="server"
                                Text="Close" OnClick="BtnClose_Click" Visible="false" />
                        </div>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkSearchInfo" />
                        <asp:AsyncPostBackTrigger ControlID="btnReadQRCode" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true" CodeFile="Counselor_Feedback.aspx.cs" Inherits="Counselor_Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                       
                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                 
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                 UID
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 70%;">
                                                <asp:Label runat="server" ID="lblUID"></asp:Label>
                                            </td>                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                 
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                 Student Name
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 70%;">
                                                <asp:Label runat="server" ID="lblStudentName"></asp:Label>
                                            </td>                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                 
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                 Student Contact No
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 70%;">
                                                <asp:Label runat="server" ID="lblStudentContactNo"></asp:Label>
                                            </td>                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                 
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                 Parent Name
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 70%;">
                                                <asp:Label runat="server" ID="lblParentName"></asp:Label>
                                            </td>                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                 
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                 Parent Contact No
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 70%;">
                                                <asp:Label runat="server" ID="lblParentContactNo"></asp:Label>
                                            </td>                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">                                        
                                        <tr>
                                            <td style="border-style: none; text-align: right; width: 100%;">
                                                <asp:DataList ID="dlFeedBackQue" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover">
                                                    <HeaderTemplate>
                                                        <b>Points</b></th>
                                                        <th width="15%" style="text-align : center">
                                                            Excellent
                                                        </th>
                                                        <th width="15%" style="text-align : center">
                                                            Very Good
                                                        </th>
                                                        <th width="15%" style="text-align : center">
                                                            Good
                                                        </th>
                                                        <th width="15%" style="text-align : center">
                                                            Satisfactory
                                                        </th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPoint" Text='<%#DataBinder.Eval(Container.DataItem, "Question")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblPointID" Text='<%#DataBinder.Eval(Container.DataItem, "Question_No")%>' runat="server" Visible="false"></asp:Label>
                                                        <td style="text-align : center">
                                                            <asp:CheckBox ID="chkExcellent" runat="server" AutoPostBack="true" OnCheckedChanged="chkExcellent_CheckedChanged" />
                                                            <span class="lbl"></span>
                                                            
                                                        </td>
                                                        <td style="text-align : center">
                                                            <asp:CheckBox ID="chkVGood" runat="server" AutoPostBack="true" OnCheckedChanged="chkVGood_CheckedChanged" />
                                                            <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align : center">
                                                            <asp:CheckBox ID="chkGood" runat="server" AutoPostBack="true" OnCheckedChanged="chkGood_CheckedChanged" />
                                                            <span class="lbl"></span>
                                                            
                                                        </td>
                                                        <td style="text-align : center">
                                                            <asp:CheckBox ID="chkSatisfactory" runat="server" AutoPostBack="true" OnCheckedChanged="chkSatisfactory_CheckedChanged" />
                                                            <span class="lbl"></span>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: right; width: 100%;">
                                                <asp:DataList ID="dlFeedBackType2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover">
                                                    <HeaderTemplate>
                                                        <b>Question</b></th>
                                                        <th width="65%" style="text-align : center">
                                                            Answer
                                                        </th>                                                       
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuestion" Text='<%#DataBinder.Eval(Container.DataItem, "Question")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblQuestiNo" Text='<%#DataBinder.Eval(Container.DataItem, "Question_No")%>' runat="server" Visible="false"></asp:Label>
                                                        <td style="text-align : center">
                                                             <label>
                                                                <input runat="server" id="chkQNo1Ans" name="switch-field-1" type="text" style="width:60%"
                                                                checked="false" visible="true"/>
                                                                <span class="lbl"></span>
                                                            </label>   
                                                        </td>                                                        
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div class="row-fluid" style="text-align: center;">
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                                Text="Save" Visible="true" onclick="BtnSave_Click" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>


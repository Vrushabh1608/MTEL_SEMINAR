<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true" CodeFile="SMS_Communication.aspx.cs" Inherits="SMS_Communication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row-fluid" style="text-align: Right;">
         <a href="Default.aspx" class="btn btn-app btn-success btn-mini radius-4" >HOME</a>
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
                       

                        <div class="row-fluid" style="text-align: center;">                   
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAttendance" runat="server"
                                Text="Send SMS" Visible="true" Width="120px" OnClick="BtnSaveAttendance_Click"/>
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" 
                                runat="server" Text="Close"  Visible="false" />                   
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        
                    </Triggers>
                </asp:UpdatePanel>
                
            </div>
        </div>         
    </div>
</asp:Content>



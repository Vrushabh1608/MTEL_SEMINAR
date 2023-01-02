<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true" CodeFile="ThanksMessage.aspx.cs" Inherits="ThanksMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row-fluid" id="divSaveMessage" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-block alert-success" id="Div1"  runat="server">
                        <p>
                            <strong><i class="icon-ok"></i></strong>
                            <asp:Label ID="lblMessage" runat="server" Text="Thank You for the Enquiry....!"></asp:Label>
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnOk" runat="server"
                                    Text="OK"  onclick="btnOk_Click" />
                        </p>
                    </div>                                    
                </ContentTemplate>
            </asp:UpdatePanel>
    </div>

</asp:Content>


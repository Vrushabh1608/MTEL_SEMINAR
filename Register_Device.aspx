<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true" CodeFile="Register_Device.aspx.cs" Inherits="Register_Device" %>

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
                        <table runat="server" id="tblSearchDetail" cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 20%;">
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <asp:Label runat="server" ID="Label2">UID</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                <div class="input-append">
                                                    <asp:TextBox runat="server" ID="txtSearchUID" ToolTip="UID" Width="150px" 
                                                        type="number" />
                                                    <asp:LinkButton ID="lnkSearchInfo" ToolTip="Search" runat="server" Height="20px"
                                                        class="btn btn-purple icon-search" CommandName="SearchInfo" 
                                                         onclick="lnkSearchInfo_Click">                                                               

                                                    </asp:LinkButton>
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
                             <tr id="trViewInfoMessage" runat="server" visible="false">  
                                <td style="border-style: none; text-align: left; width: 100%;" colspan="3">
                                    <div class="alert alert-block alert-success" style="text-align: center;">
                                        <%--<button type="button" class="close" data-dismiss="alert">
                                            <i class="icon-remove"></i>
                                        </button>--%>
                                        <p>
                                            <strong><i class="icon-ok"></i></strong>
                                            <asp:Label ID="lblTabMessage" runat="server" Text="Tab Already Issued" 
                                                Font-Bold="True" Font-Size="Large"></asp:Label>
                                        </p>
                                    </div>
                                </td>         
                            </tr>   
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
                                </td>
                                <%--<td style="border-style: none; text-align: left; width: 20%;">
                                </td> --%>        
                            </tr>                            
                            
                            <tr>                  
                                <td style="border-style: none; text-align: left; width: 20%;">    
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">                                        
                                    <asp:Label runat="server" ID="Label5">Manufacturer</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;"> 
                                    <asp:TextBox runat="server" ID="txtManufacturer" CssClass="uppercase" Enabled="false" ToolTip="Device Name"  type="text" Width="265px" Text="Samsung"></asp:TextBox>
                                </td>
                                <%--<td style="border-style: none; text-align: left; width: 20%;">
                                </td> --%>        
                            </tr>  
                            <tr>                  
                                <td style="border-style: none; text-align: left; width: 20%;">    
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">                                        
                                    <asp:Label runat="server" ID="Label8">Model No</asp:Label>
                                    <asp:Label runat="server" ID="Label10" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;"> 
                                    <asp:DropDownList runat="server" ID="ddlModelNo" Width="280px" ToolTip="Model Number"
                                        data-placeholder="Select Model No" AutoPostBack="true" CssClass="chzn-select">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="SM-T531">SM-T531</asp:ListItem>
                                        <asp:ListItem Value="SM-T561">SM-T561</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <%--<td style="border-style: none; text-align: left; width: 20%;">
                                </td> --%>        
                            </tr>  
                            <tr>                  
                                <td style="border-style: none; text-align: left; width: 20%;">    
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">                                        
                                    <asp:Label runat="server" ID="Label9">SD Card Size</asp:Label>
                                    <asp:Label runat="server" ID="Label11" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;"> 
                                    <asp:DropDownList runat="server" ID="ddlSDCardSize" Width="280px" ToolTip="SD Card Size"
                                        data-placeholder="Select SD Card Size" AutoPostBack="true" CssClass="chzn-select">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="32">32</asp:ListItem>
                                        <asp:ListItem Value="64">64</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <%--<td style="border-style: none; text-align: left; width: 20%;">
                                </td> --%>        
                            </tr>  

                            <tr>                  
                                <td style="border-style: none; text-align: left; width: 20%;">    
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">                                        
                                    <asp:Label runat="server" ID="Label1">IMEI Number</asp:Label>
                                    <asp:Label runat="server" ID="Label6" CssClass="red">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;"> 
                                    <asp:TextBox runat="server" ID="txtSerialNumber" CssClass="uppercase" 
                                        ToolTip="Serial Number"  type="text" Width="265px"></asp:TextBox>
                                </td>
                                <%--<td style="border-style: none; text-align: left; width: 20%;">
                                </td> --%>        
                            </tr>   
                           
                        </table>
                        
                        <div class="row-fluid" style="text-align: center;">                   
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnRegister" runat="server"
                                Text="Register" Visible="false" Width="75px" onclick="BtnRegister_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" 
                                runat="server" Text="Close" OnClick="BtnClose_Click"  Visible="false" />                   
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkSearchInfo" />
                         <asp:PostBackTrigger ControlID="BtnRegister" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </div>
        </div>         
    </div>
</asp:Content>


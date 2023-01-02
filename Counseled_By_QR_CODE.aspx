<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Counseled_By_QR_CODE.aspx.cs" Inherits="Counseled_By_QR_CODE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style type="text/css">
        body
        {
            width: 100%;
            text-align: center;
        }
        img
        {
            border: 0;
        }
        #main
        {
            margin: 15px auto;
            background: white;
            overflow: auto;
            width: 100%;
        }
        #header
        {
            background: white;
            margin-bottom: 15px;
        }
        #mainbody
        {
            background: white;
            width: 100%;
            display: none;
        }
        #footer
        {
            background: white;
        }
        #v
        {
            width: 300px;
            height: 240px;
        }
        #qr-canvas
        {
            display: none;
        }
        #qrfile
        {
            width: 300px;
            height: 240px;
        }
        #mp1
        {
            text-align: center;
            font-size: 35px;
        }
        #imghelp
        {
            position: relative;
            left: 0px;
            top: -160px;
            z-index: 100;
            font: 18px arial,sans-serif;
            background: #f0f0f0;
            margin-left: 35px;
            margin-right: 35px;
            padding-top: 10px;
            padding-bottom: 10px;
            border-radius: 20px;
        }
        .selector
        {
            margin: 0;
            padding: 0;
            cursor: pointer;
            margin-bottom: -5px;
        }
        #outdiv
        {
            width: 300px;
            height: 240px;
            border: solid;
            border-width: 3px 3px 3px 3px;
        }
        #result
        {
            border: solid;
            border-width: 1px 1px 1px 1px;
            padding: 20px;
            width: 70%;
        }
        
        ul
        {
            margin-bottom: 0;
            margin-right: 40px;
        }
        li
        {
            display: inline;
            padding-right: 0.5em;
            padding-left: 0.5em;
            font-weight: bold;
            border-right: 1px solid #333333;
        }
        li a
        {
            text-decoration: none;
            color: black;
        }
        
        #footer a
        {
            color: black;
        }
        .tsel
        {
            padding: 0;
        }
    </style>
    <script type="text/javascript" src="QR_JS_IMAGE/llqrcode.js"></script>
    <script type="text/javascript" src="../apis.google.com/js/plusone.js"></script>
    <script type="text/javascript" src="QR_JS_IMAGE/webqr.js"></script>
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
                <asp:UpdatePanel ID="upnlSearchInfo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table runat="server" id="tblSearchDetail" cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <div id="main">
                                        <div id="mainbody">
                                            <table class="tsel" border="0" width="100%">
                                                <tr>
                                                    <td valign="top" class="span12" style="text-align: left">
                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2" >
                                                                    <table>
                                                                        <tr>
                                                                            <td style="border-style: none; text-align: left; width: 50%;">
                                                                                <img class="selector" id="webcamimg" src="QR_JS_IMAGE/vid.png" onclick="setwebcam()"
                                                                                    align="left" />
                                                                            </td>
                                                                            <td style="border-style: none; text-align: right; width: 50%;">
                                                                                <div class="input-append">
                                                                                    <img class="selector" id="qrimg" src="QR_JS_IMAGE/cam.png" onclick="setimg()" align="right" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: center; width: 60%;" colspan="2">
                                                                    <div id="outdiv">
                                                                    </div>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2">
                                                                    <img src="QR_JS_IMAGE/down.png" />
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2">
                                                                    <div id="result">
                                                                    </div>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;" colspan="2">
                                                                    <asp:TextBox runat="server" ID="txtSearchUID" ToolTip="UID" Width="150px" ClientIDMode="Static"
                                                                        Text="" OnTextChanged="txtSearchUID_TextChanged" AutoPostBack="true" />
                                                                    <asp:LinkButton ID="lnkSearchInfo" ToolTip="Search" runat="server" Height="20px"
                                                                        class="btn btn-purple icon-search" CommandName="SearchInfo" OnClick="lnkSearchInfo_Click"
                                                                        Visible="false">                                                               
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 20%;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <canvas id="qr-canvas" width="800" height="600">
                                    </canvas>
                                </td>
                            </tr>
                        </table>
                        <script type="text/javascript">load();</script>
                        <table runat="server" id="tblSearchInfo" visible="false" cellpadding="3" class="table table-striped table-bordered table-condensed">                            
                            <tr>                  
                                <td style="border-style: none; text-align: left; width: 20%;">    
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">                                        
                                    <asp:Label runat="server" ID="Label3">UID</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;">                                                  
                                     <asp:Label runat="server" ID="txtUID"></asp:Label>
                                </td> 
                            </tr> 
                            <tr>                  
                                <td style="border-style: none; text-align: left; width: 20%;">    
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">                                        
                                    <asp:Label runat="server" ID="Label4">Student Name</asp:Label>                                   
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;">                                             
                                   <asp:Label runat="server" ID="lblStudentName"></asp:Label>
                                </td> 
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
                            </tr>   
                            <tr>                  
                                <td style="border-style: none; text-align: left; width: 20%;">    
                                </td>
                                <td style="border-style: none; text-align: left; width: 30%;">                                        
                                    <asp:Label runat="server" ID="Label1">Counselled By</asp:Label>
                                    <asp:Label runat="server" CssClass="red" ID="Label5">*</asp:Label>
                                </td>
                                <td style="border-style: none; text-align: left; width: 50%;"> 
                                    <asp:DropDownList runat="server" ID="ddlCouncilBy" Width="280px" ToolTip="Council By"
                                        data-placeholder="Select Council" CssClass="chzn-select">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>    
                            </tr>                                
                        </table>
                        <div class="row-fluid" style="text-align: center;">
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveCouncil" runat="server"
                                Text="Save" Visible="false" OnClick="BtnSaveCouncil_Click"/>
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" 
                                runat="server" Text="Close" OnClick="BtnClose_Click"  Visible="false" />    
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkSearchInfo" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

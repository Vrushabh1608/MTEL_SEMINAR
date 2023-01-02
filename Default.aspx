<%@ Page Title="" Language="C#" MasterPageFile="~/Seminar.Master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
   <%-- <div class="navbar navbar-inverse">
        <div class="navbar-inner">
            <div class="container-fluid">
                <a class="brand" href="#"><small><i class="icon-leaf"></i>MT Educare Ltd.</small>
                </a>
               
                <!--/.ace-nav-->
            </div>
            <!--/.container-fluid-->
        </div>
        <!--/.navbar-inner-->
    </div>--%>
    <!--/.navbar--><%--
    <div class="container-fluid" id="main-container">     
        <div id="main-content" >--%>
            
            <!--#breadcrumbs-->
           <%-- <div id="page-content" class="clearfix">--%>
                <!--<div class="page-header position-relative">
                    <h1>
                        Form Wizard <small><i class="icon-double-angle-right"></i>and Validation</small></h1>
                </div>-->
                <!--/page-header-->
                    <!-- PAGE CONTENT BEGINS HERE -->
                    <div class="row-fluid">
                       <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>     
                                            <td style="border-style: none; text-align: Left; width: 50%;">                                        
                                                <%--<asp:TextBox runat="server" ID="txtTabId" ToolTip="Tab ID" type="text"
                                                                Width="205px" ValidationGroup="UcValidate"/>
                                                <asp:LinkButton ID="lnkSave" ToolTip="Save" class="btn-small btn-primary icon-info-sign"
                                                        runat="server"
                                                    CommandName="SaveInfo" onclick="lnkSave_Click" />--%>
                                            </td>                                       
                                            <td style="border-style: none; text-align: Right; width: 50%;">                                        
                                                <ul class="breadcrumb">
                                                    
							                        <%--<li><a href="configuration.aspx">Configuration</a><span class="divider"></span></li>--%>							                        
						                        </ul><!--.breadcrumb-->

                                            </td>
                                        </tr>
                                    </table>
                                </td>                               
                            </tr>
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>                                            
                                            <td style="border-style: none; text-align: Center; width: 100%;">                                        
                                                <a href="#">
		                                           <img style="width: 50%; height:50%;" src="Images/MT_Logo.jpg">		                                          
		                                        </a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>                               
                            </tr>
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>                                            
                                            <td style="border-style: none; text-align: Center; width: 100%;">  
                                                <div class="widget-main alert-block alert-success  alert- " style="text-align: Center;">                                      
                                                   
                                                    
                                                        <a href="Attendance_QR_Code.aspx" class="btn btn-app btn-success btn-mini radius-4" style ="width:150px"> Attendance       </a>
                                                        <a href="Counseled_By_QR_CODE.aspx" class="btn btn-app btn-success btn-mini radius-4" style ="width:150px">Counselled By</a>
                                                        <a href="Customer_Verification_QR_CODE.aspx" class="btn btn-app btn-success btn-mini radius-4" style ="width:150px">Customer Veri.</a>
                                                        <a href="Admission_QR_CODE.aspx"" class="btn btn-app btn-success btn-mini radius-4" style ="width:150px"> Admission        </a>
                                                        
                                                        <a href="Register_Device.aspx" class="btn btn-app btn-success btn-mini radius-4" style ="width:150px"> Register Device</a>

                                                        <a href="Attendance_Staff_QR_Code.aspx" class="btn btn-app btn-success btn-mini radius-4" style ="width:150px">Staff Attendance</a>
                                                        
                                                        <a href="Seminar_Report.aspx" class="btn btn-app btn-success btn-mini radius-4" style ="width:150px; display:none"> Report</a>
                                                        

                                                   
                                                </div>                                                             
                                            </td>
                                        </tr>
                                    </table>
                                </td>                               
                            </tr>
                        </table>
                    </div>
                    <!-- PAGE CONTENT ENDS HERE -->
                </div>
                <!--/row-->
            <%--</div>--%>
    <!--/#page-content-->
    <!--<div id="ace-settings-container">
                <div class="btn btn-app btn-mini btn-warning" id="ace-settings-btn">
                    <i class="icon-cog"></i>
                </div>
                <div id="ace-settings-box">
                    <div>
                        <div class="pull-left">
                            <select id="skin-colorpicker" class="hidden">
                                <option data-class="default" value="#438EB9">#438EB9</option>
                                <option data-class="skin-1" value="#222A2D">#222A2D</option>
                                <option data-class="skin-2" value="#C6487E">#C6487E</option>
                                <option data-class="skin-3" value="#D0D0D0">#D0D0D0</option>
                            </select>
                        </div>
                        <span>&nbsp; Choose Skin</span>
                    </div>
                    <div>
                        <input type="checkbox" class="ace-checkbox-2" id="ace-settings-header" /><label class="lbl"
                            for="ace-settings-header">
                            Fixed Header</label></div>
                    <div>
                        <input type="checkbox" class="ace-checkbox-2" id="ace-settings-sidebar" /><label
                            class="lbl" for="ace-settings-sidebar">
                            Fixed Sidebar</label></div>
                </div>
            </div>-->
    <!--/#ace-settings-container-->
    </div>
    <!-- #main-content -->
    </div>
    <!--/.fluid-container#main-container-->
   </asp:Content>

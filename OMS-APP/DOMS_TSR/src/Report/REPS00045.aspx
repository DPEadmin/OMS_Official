<%@ Page Language="C#"   AutoEventWireup="true" CodeBehind="REPS00045.aspx.cs" Inherits="KMISRWebApplication.Webcontent.Report.REPS00045" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>
<html>



<head>
   <title></title>
    	
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
                 <rsweb:ReportViewer ID="REP00045" runat="server" Width="100%" Height="700px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"  >
                        <LocalReport ReportPath="ReportFile/Report/Report1.rdlc">
                        </LocalReport>
                 </rsweb:ReportViewer>


             </ContentTemplate>
         </asp:UpdatePanel>
    </form>
</body>
</html>

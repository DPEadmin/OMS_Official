<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceViewer.aspx.cs" Inherits="DOMS_TSR.src.FullfillOrderlist.InvoiceViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidEmpCode" runat="server" />
          <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"/>
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="700px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana">
                <LocalReport ReportPath="src/Report/Report2.rdlc">
                        </LocalReport>

            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>


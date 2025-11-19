<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="StockOrderDetail.aspx.cs" Inherits="DOMS_TSR.src.FullfillOrderlist.StockOrderDetail" %>

<%@ Register Src="~/src/UserControl/UserControlStockOrderDetail.ascx" TagPrefix="uc1" TagName="OrderDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/Chart.js-master/css js/Chart.css">
    <link rel="icon" href="favicon.ico">
    <script src="../../Scripts/Chart.js-master/css js/Chart.js"></script>
    <script src="../../Scripts/utils.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <uc1:OrderDetail runat="server" id="OrderDetail" />

            <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />

        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>


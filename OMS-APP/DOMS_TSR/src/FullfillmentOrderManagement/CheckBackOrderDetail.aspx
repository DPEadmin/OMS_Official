<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="CheckBackOrderDetail.aspx.cs" Inherits="DOMS_TSR.src.FullfillmentOrderManagement.CheckBackOrderDetail" %>

<%@ Register Src="~/src/UserControl/OrderDetail.ascx" TagPrefix="uc1" TagName="OrderDetail" %>


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
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>


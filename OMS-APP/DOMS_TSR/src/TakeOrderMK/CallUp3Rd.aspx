<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/src/MasterPage/Web.master"  CodeBehind="CallUp3Rd.aspx.cs" Inherits="DOMS_TSR.src.TakeOrderMK.CallUp3Rd" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

          <div>
                <asp:LinkButton ID="LinkButton1" runat="server"   PostBackUrl="~/src/TakeOrderRetail/TakeOrder.aspx?CustomerCode=C2018091700001&RefCode=Tele0001&BrandNo=01&Customerphone=0838023829&UniqueId=dadc2ac1-8dd7-4508-8ff9-0e6adb3c1b3f&ChannelCode=Lin&CallinDate=15/07/2019&CallinTime=10:05" >Call up Orrapan</asp:LinkButton>                
        </div>
        <div>
                <asp:LinkButton ID="LinkButton2" runat="server"   PostBackUrl="~/src/TakeOrderRetail/TakeOrder.aspx?CustomerCode=C2018091700002&RefCode=Tele0001&BrandNo=01&Customerphone=0986547895&UniqueId=Unq002&ChannelCode=Lin&CallinDate=15/07/2019&CallinTime=10:05" >Call up SuKanya</asp:LinkButton>
        </div>
   
</asp:Content>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     
    </form>
</body>
</html>--%>


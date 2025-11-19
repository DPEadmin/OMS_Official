<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="DOMS_TSR.src.TestCode.WebForm3" %>
<%@ Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <CKEditor:CKEditorControl ID="CKEditor1" runat="server">
</CKEditor:CKEditorControl>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="DOMS_TSR.src.OrderManagement.OrderDetail" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidEmpCode" runat="server" />

    <div class="page-body">
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header border-0">
                        <div class="sub-title">ค้นหาข้อมูลการสั่งซื้อ</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

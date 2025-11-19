<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="BranchMapProduct.aspx.cs" Inherits="DOMS_TSR.src.BranchManagement.BranchMapProduct" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hidEmpCode" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">

                        <div class="card">

                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูลสินค้า</div>
                            </div>

                            <div class="card-body">

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-1"></div>

                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ส่วนประกอบ</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchRecipe" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-1"></div>

                                    <label class="col-sm-2 col-form-label">สถานะการขาย</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchActive" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

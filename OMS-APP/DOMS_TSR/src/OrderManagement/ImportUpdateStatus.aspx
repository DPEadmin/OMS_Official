<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ImportUpdateStatus.aspx.cs" Inherits="DOMS_TSR.src.OrderManagement.ImportUpdateStatus" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="page-body">
                <div class="row">
                    <div class="col-12">

                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Import Update Status</div>
                            </div>
                            <div class="card-block">
                                <div class="m-b-10">
                                    <!--Start modal Add Customer-->
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />  
                                            <asp:LinkButton ID="btnPreview" class="button-action button-add" runat="server">Preview</asp:LinkButton>
                                            <asp:LinkButton ID="btnUpload" class="button-action button-add" runat="server"><i class="fa fa-plus m-r-5"></i>Upload</asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvImportUpdateStatus" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowCommand="gvCallInfo_RowCommand">
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

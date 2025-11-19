<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="UploadChangStatusOrder.aspx.cs" Inherits="DOMS_TSR.src.OrderManagement.UploadChangStatusOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
             <asp:HiddenField ID="hidEmpCode" runat="server" />
             <asp:HiddenField ID="hidMerCode" runat="server"/>
    <div class="card">
        <div class="card-header border-0">
            <div class="sub-title">
              Import sales order status</div>
        </div>
        <div class="card-body">
            <div id="searchSection_All" runat="server">
                <div class="form-group row">
                    <label class="col-sm-3 col-form-label">
                  Order list import file </label>
                    <div class="col-sm-4">
                       <asp:FileUpload ID="fiUpload" runat="server" class="form-control"></asp:FileUpload><br />
                    </div>
                 
                </div>
              
                <div class="text-center m-t-20 col-sm-12">
                    <asp:Button ID="btnSearch_OrderCancelled" OnClick="btnPreview_Click" runat="server" class="button-pri button-accept m-r-10" Text="Import" />
                    <asp:Button ID="btnClearSearch_OrderCancelled" runat="server" class="button-pri button-cancel m-r-10"  Text="Clear" />
                </div>
            </div>
        </div>

        <div class="page-body" id="divGrid" runat="server" visible="false">
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Basic Form Inputs card start -->
                                    <div class="card">
                                        <div class="card-block">
                                         
                                            <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
            
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Ordercode</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrdercode" Text='<%# DataBinder.Eval(Container.DataItem, "Ordercode")%>' runat="server" />
                                                            <asp:HiddenField runat="server" ID="HidOrdercode" Value='<%# DataBinder.Eval(Container.DataItem, "Ordercode")%>' />
                                                            <asp:HiddenField runat="server" ID="HidPostNumber" Value='<%# DataBinder.Eval(Container.DataItem, "PostNumber")%>' />
                                                            <asp:HiddenField runat="server" ID="HidOrderStatus" Value='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' />
                                                         <asp:HiddenField runat="server" ID="HidOrderState" Value='<%# DataBinder.Eval(Container.DataItem, "OrderState")%>' />
                                                            <asp:HiddenField runat="server" ID="HidMerchantName" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Postnumber</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPostNumber" Text='<%# DataBinder.Eval(Container.DataItem, "PostNumber")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Orderstatus</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Orderstate</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderState" Text='<%# DataBinder.Eval(Container.DataItem, "OrderState")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>

                                                            <div align="left">Merchantname</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                            <br />
                                            <br />
                                        

                                        </div>
                                    </div>
                                </div>
                                    <div class="text-center m-t-20 col-sm-12">
                    <asp:Button ID="btnSaveStatusOrder" runat="server" class="button-pri button-accept m-r-10" Text="Submit" OnClick="btnSaveStatusOrder_Click" />
                    <asp:Button ID="btnCancelSaveStatusOrder" runat="server" class="button-pri button-cancel m-r-10" OnClick="btnCancelSaveStatusOrder_Click" Text="Cancle" />
                </div>
                                <!-- Basic Form Inputs card end -->
                            </div>
                        </div>

    </div>
</asp:Content>

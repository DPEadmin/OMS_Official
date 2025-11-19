<%@ Page Language="C#"  MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ImportProduct.aspx.cs" Inherits="DOMS_TSR.src.InventoryManagement.ImportProduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


  
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>

    <script>
        $(document).ready(function () {
            $("#btnPreviewImport").click(function () {
                $("#modal-ImportMedia").modal();
            });
        });
    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

       <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                        <asp:HiddenField ID="hidInventorySelected" runat="server" />
                                        <asp:HiddenField ID="hidFlagInsertProduct" runat="server" />
                                        <asp:HiddenField ID="hidMerCode" runat="server" />

    <asp:HiddenField ID="hd" runat="server" />
    <div class="page-body">
        <div class="row">
            <div class="col-sm-12">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Import Product</div>
                            </div>
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="card">
                    <div class="card-body">
                     <%--   <div class="form-group row">
                           <label class="col-sm-1 col-form-label">คลังสินค้า</label>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlInventory" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInventory_SelectedIndexChanged" ></asp:DropDownList>
                            </div>                                                                                
                        </div>--%>

                        <div class="form-group row">
                            <div class="col-4">
                                <asp:FileUpload ID="fiUpload" runat="server" class="form-control"></asp:FileUpload>
                            </div>
                            <div class="col-4">
                                <asp:Button type="button" ID="btnUpload" OnClick="btnUpload_Click" Text="Upload" class="button-pri button-accept m-r-10" runat="server" />
                            </div>
                            <div class="col-4">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="false">
                                    <ContentTemplate> 
                                        <asp:LinkButton ID="btnShowImportFile" runat="server" class="button-pri button-accept m-r-10">Show</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            

                           
                        </div>
                    </div>
                </div>


                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-12" runat="server" visible="false" id="DivSubmitUpload">
                                <asp:GridView ID="gvProductImport" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvInventoryDetailImport_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ProductCode</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ProductName</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ProductBrandName</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ProductCategoryName</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Price</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Unit</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" Text='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' runat="server" />
                                                 <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' />
                                             <%--   <asp:HiddenField runat="server" ID="hidInventoryDetailID" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryDetailID")%>' />--%>
                                                <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                       <%--         <asp:HiddenField runat="server" ID="hidQTY" Value='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' />--%>
                                                <%--<asp:HiddenField runat="server" ID="hidReserved" Value='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' />--%>
                                                <%--<asp:HiddenField runat="server" ID="hidCurrents" Value='<%# DataBinder.Eval(Container.DataItem, "Current")%>' />--%>
                                                <%--<asp:HiddenField runat="server" ID="hidBalance" Value='<%# DataBinder.Eval(Container.DataItem, "Balance")%>' />--%>
                                                <asp:HiddenField runat="server" ID="hidProductCategoryCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidProductBrandCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductBrandCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidUnitCode" Value='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' />
                                                <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                <asp:HiddenField runat="server" ID="hidSKU" Value='<%# DataBinder.Eval(Container.DataItem, "SKU")%>' />
                                                <asp:HiddenField runat="server" ID="hidDescription" Value='<%# DataBinder.Eval(Container.DataItem, "Description")%>' />
                                                <asp:HiddenField runat="server" ID="hidUpsellScript" Value='<%# DataBinder.Eval(Container.DataItem, "UpsellScript")%>' />
                                                <%--<asp:HiddenField runat="server" ID="hidReOrder" Value='<%# DataBinder.Eval(Container.DataItem, "ReOrder")%>' />--%>
                                                <asp:HiddenField runat="server" ID="hidProductID" Value='<%# DataBinder.Eval(Container.DataItem, "ProductID")%>' />
                                                <asp:HiddenField runat="server" ID="hidProductImportDup" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCodeImportDup")%>' />
                                                <asp:HiddenField runat="server" ID="hidMerName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCodeImportDup")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">SKU</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSku" Text='<%# DataBinder.Eval(Container.DataItem, "SKU")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Description</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">UpsellScript</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpsellScript" Text='<%# DataBinder.Eval(Container.DataItem, "UpsellScript")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Merchant Name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        
                                        
 

                              
                                    </Columns>
                                </asp:GridView>
                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button type="button" ID="btnSubmitImport" OnClick="btnSubmitImport_Clicked" Text="submit" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button type="button" ID="btnCancel" Text="Cancel" class="button-pri button-accept m-r-10" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    


    
</asp:Content>

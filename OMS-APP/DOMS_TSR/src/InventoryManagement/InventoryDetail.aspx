<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="InventoryDetail.aspx.cs" Inherits="DOMS_TSR.src.InventoryManagement.InventoryDetail" %>

<%@ Register Src="~/src/UserControl/Drawpolygon.ascx" TagPrefix="uc1" TagName="Drawpolygon" %>





<asp:Content ID="Head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .hideText  {
    width:20rem;
    overflow:hidden;
    text-overflow:ellipsis;
    white-space:nowrap;
 }
    </style>

    <script type="text/javascript">
        function validatenumerics(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57)) {
                alert(" กรุณาระบุตัวเลข ");
                return false;
            }
            else return true;
        }
    
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <uc1:Drawpolygon runat="server" id="Drawpolygon" />

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hd" runat="server" />
            <asp:HiddenField ID="hidMerCode" runat="server" />
          

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">

                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">Inventory details</div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">Inventory code</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblInventoryCode" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">Inventory name</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblInventoryName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">address</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">sub-district/sub-district</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblSubDistrict" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">district/district</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">province</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblProvince" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">Post code</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">telephone number</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblContactTel" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">Fax.</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblFax" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">transmission line</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Repeater id="myRepeater" runat="server">
                                                    <ItemTemplate>
                                                <asp:Label ID="lblroutingInventory" Text='<%# DataBinder.Eval(Container.DataItem, "Routing_name")%>' runat="server"></asp:Label> <br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">Lattitude,Longtitude</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                          <asp:Label ID="LbLatLong" runat="server"></asp:Label>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">Inventory Area</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                 <asp:HiddenField ID="hidInvenIDPopupMap" runat="server" />
                                                <asp:Button ID="btnAreaConfig" runat="server" Text="Configue" class="button-pri button-accept m-r-10" OnClick="btnPopMap_Click" />
                                              
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">Search for inventory</div>
                            </div>
                            <div class="card-body">

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Product code</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    

                                    <label class="col-sm-2 col-form-label">Product name</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                             
                                    <label class="col-sm-2 col-form-label">Product type</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchProductCategory" class="form-control" runat="server"></asp:DropDownList>
                                    </div>

                               

                                    <label class="col-sm-2 col-form-label">Brand</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchProductBrand" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="m-b-10">
                                    <asp:LinkButton ID="btnChooseProduct" class="button-action button-add" data-backdrop="false" runat="server" OnClick="btnChooseProduct_Click"><i class="fa fa-plus m-r-5"></i>Add Product</asp:LinkButton>
                                    <asp:LinkButton ID="btnChoosePO" class="button-action button-add" runat="server" OnClick="btnChoosePO_Click"><i class="fa fa-plus m-r-5"></i>Choose PO</asp:LinkButton>
                                    <asp:LinkButton ID="btnExport" OnClick="btnExport_Click" class="button-action button-add" data-backdrop="false" runat="server" ><i class="fa fa-plus m-r-5"></i>Export Excel</asp:LinkButton>
                                </div>
<div class="table-responsive">
                                <asp:GridView ID="gvInventoryDetail" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvInventoryDetail_RowCommand" OnRowDataBound="gvInventoryDetail_RowDataBound">                                  
                                    <Columns>
                                        <%--<asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                <asp:CheckBox ID="chkInventoryDetailAll" OnCheckedChanged="chkInventoryDetailAll_Change" AutoPostBack="true" runat="server"  />
                                            </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkInventoryDetail" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Update Date</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <!--<asp:Label ID="lblUpdateDate1" Text='<%# DataBinder.Eval(Container.DataItem, "UpdateDate")%>' runat="server" />-->
                                                <asp:Label ID="lblUpdateDate" Text='<%# ((null == Eval("UpdateDate"))||("" == Eval("UpdateDate"))) ? string.Empty : DateTime.Parse(Eval("UpdateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Product Code</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode"),DataBinder.Eval(Container.DataItem, "InventoryDetailID")) %>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Product Name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Product Price</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">PO No.</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPONo" Text='<%# DataBinder.Eval(Container.DataItem, "Pocode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Product Category</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Brand</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">On Hand</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOnhand" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                                <asp:TextBox ID="txtOnhand" Width="70px" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" onkeypress="return validatenumerics(event);" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Accumulate Reserved</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReserved" Text='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Pick Pack</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPickpack" Text='<%# DataBinder.Eval(Container.DataItem, "Pickpack")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Available Balance</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrent" Text='<%# DataBinder.Eval(Container.DataItem, "Current")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Balance</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalance" Text='<%# DataBinder.Eval(Container.DataItem, "Balance")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Safety Stock</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSafetyStock" Text='<%# DataBinder.Eval(Container.DataItem, "SafetyStock")%>' runat="server" />
                                                <!--<asp:TextBox ID="txtSafetyStock" Width="70px" Text='<%# DataBinder.Eval(Container.DataItem, "SafetyStock")%>' runat="server" onkeypress="return validatenumerics(event);" Enabled="false" />-->
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail" Visible = "false">
                                            <HeaderTemplate>
                                                <div align="Center">Re Order</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ReOrder")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnCancel" runat="Server" CommandName="CancelInventoryDetail" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="Server" CommandName="SaveInventoryDetail" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="fa fa-edit f-14"></span></asp:LinkButton>              
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="EditInventoryDetail" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                           
                                                <asp:HiddenField runat="server" ID="hidPOCode" Value='<%# DataBinder.Eval(Container.DataItem, "POCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidSupplierCode"   Value='<%# DataBinder.Eval(Container.DataItem, "SupplierCode")%>'/>                         
                                                <asp:HiddenField runat="server" ID="hidInventoryDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryDetailId")%>' />
                                                <asp:HiddenField runat="server" ID="hidInventoryCode" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidQTY" Value='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' />
                                                <asp:HiddenField runat="server" ID="hidReserved" Value='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' />
                                                <asp:HiddenField runat="server" ID="hidBalance" Value='<%# DataBinder.Eval(Container.DataItem, "Balance")%>' />
                                                <asp:HiddenField runat="server" ID="hidPickpack" Value='<%# DataBinder.Eval(Container.DataItem, "PickPack")%>' />
                                                <asp:HiddenField runat="server" ID="hidReOrder" Value='<%# DataBinder.Eval(Container.DataItem, "ReOrder")%>' />
                                                <br />
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <EmptyDataTemplate>
                                        <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <div class="m-t-10">
                                <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
                                                    <td style=" width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First" Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous" Text="<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>
                                                        of
                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnNext" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnLast" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last" Text=">>" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        </div>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-product">
        <div class="modal-dialog modal-lg" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">Choose Product</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>

                <div class="modal-body">
                 
                          

                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Product code</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtSearchProductCode_ProductModal" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            

                                            <label class="col-sm-2 col-form-label">Product name</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtSearchProductName_ProductModal" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                            <label class="col-sm-2 col-form-label">Product type</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlSearchCategory_ProductModal" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                            </div>

                                            

                                            <label class="col-sm-2 col-form-label">Brand</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtBrand" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="text-center m-t-20 col-sm-12">
                                            <asp:Button ID="btnSearch_ProductModal" Text="Search" class="button-pri button-accept m-r-10" OnClick="btnSearch_ProductModal_Click" runat="server" />
                                            <asp:Button ID="btnClearSearch_ProductModal" Text="Clear" class="button-pri button-cancel" OnClick="btnClearSearch_ProductModal_Click" runat="server" />
                                        </div>

                                        <hr />
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Purchase Order (PO)</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPurchaseOrder" AutoPostBack="true" OnTextChanged="txtPOChanged" class="form-control" runat="server"></asp:TextBox>
                                                <asp:ListBox ID="ListBoxPurchaseOrder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxPurchaseOrder_SelectedIndexChanged" CssClass="autocomplete-listbox" Visible="false"></asp:ListBox>
                                            </div>
                                            <label class="col-sm-2 col-form-label">Supplier</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtSupplier" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:HiddenField ID="hidSupplierCode" runat="server"/>
                                            </div>
                                            <label class="col-sm-2 col-form-label">*Lot No</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtLotNumber" class="form-control" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red">*Lot no. is required</asp:Label>
                                            </div>
                                        </div>
                                        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             OnRowCommand="gvProduct_RowCommand">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Product code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProducCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Product name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' style="text-overflow: ellipsis;" runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Product type</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Brand</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">number of goods imported</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountIns" Style="text-align:right" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" onkeypress="return validatenumerics(event);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <!--<asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddProduct" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-plus-circle f-14"></span></asp:LinkButton>-->

                                                        <asp:HiddenField ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidProductCodeIns" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidProductNameIns" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidSupplierCodeIns" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidSupplierNameIns" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidMerchantCodeIns" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidMerchantNameIns" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidProductCategoryCodeIns" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidProductCategoryNameIns" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server"/>

                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                


                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <div class="text-center m-t-20 col-sm-12">
                                                    <asp:Button ID="btnImportProduct" Text="Import" class="button-pri button-accept m-r-10" OnClick="btnImportProduct_Click" runat="server" />
                                                    <asp:Button ID="btnReset" Text="Reset" class="button-pri button-cancel" OnClick="btnReset_Click" runat="server" />
                                                </div>
                                    <div class="m-t-10">
                        <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                            <tr height="30" bgcolor="#ffffff">
                                <td width="100%" align="right" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                        <tr>
                                            <td style="font-size: 8.5pt;">                                               
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductFirst" CssClass="Button pagina_btn" ToolTip="gvProductFirst" CommandName="gvProductFirst" OnCommand="GetPagegvProductIndex"
                                                    Text="<<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductPre" CssClass="Button pagina_btn" ToolTip="gvProductPrevious" CommandName="gvProductPrevious" OnCommand="GetPagegvProductIndex"
                                                    Text="<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlgvProductPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgvProductPage_SelectedIndexChanged"
                                                                                      >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblgvProductTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductNext" CssClass="Button pagina_btn" ToolTip="gvProductNext" runat="server" CommandName="gvProductNext" Text=">" OnCommand="GetPagegvProductIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductLast" CssClass="Button pagina_btn" ToolTip="gvProductLast" runat="server" CommandName="gvProductLast"
                                                    Text=">>" OnCommand="GetPagegvProductIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>


                                    </ContentTemplate>
                                </asp:UpdatePanel>

                    
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="modal-po">
        <div class="modal-dialog modal-lg" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
                                <div class="modal-title sub-title " style="font-size: 16px;">Choose PO</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card-block">

                                <asp:UpdatePanel ID="UpModalPO" runat="server">
                                    <ContentTemplate>

                                        <asp:HiddenField ID="hidPOCode_Ins" runat="server"></asp:HiddenField>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Purchase Order Code (PO)</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtSearchPOCode_POModal" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>

                                            <label class="col-sm-2 col-form-label">Manufacturer name (Supplier)</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlSearchSupplierName_POModal" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Merchant name</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlSearchMerchantName_POModal" runat="server" class="form-control"></asp:DropDownList>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>

                                            <label class="col-sm-2 col-form-label">Date order</label>
                                            <div class="col-sm-3">
                                                <div class="input-group mb-0">

                                                    <asp:TextBox ID="txtSearchCreateDateFrom" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchCreateDateFrom" runat="server" TargetControlID="txtSearchCreateDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    <asp:TextBox ID="txtSearchCreateDateTo" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchCreateDateTo" runat="server" TargetControlID="txtSearchCreateDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">delivery date</label>
                                            <div class="col-sm-3">
                                                <div class="input-group mb-0">
                                                    <asp:TextBox ID="txtSearchRequestDateFrom" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchRequestDateFrom" runat="server" TargetControlID="txtSearchRequestDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    <asp:TextBox ID="txtSearchRequestDateTo" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchRequestDateTo" runat="server" TargetControlID="txtSearchRequestDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="text-center m-t-20 col-sm-12">
                                            <asp:Button ID="btnSearch_POModal" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" OnClick="btnSearch_POModal_Click" />
                                            <asp:Button ID="btnClearSearch_POModal" Text="ล้าง" class="button-pri button-cancel" runat="server" OnClick="btnClearSearch_POModal_Click" />
                                        </div>

                                        <hr />

                                        <asp:GridView ID="gvPO" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvPO_RowCommand" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Purchase Order Code (PO Number)</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOCode" Text='<%# DataBinder.Eval(Container.DataItem, "POCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Name of manufacturer (Supplier)</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Merchant name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">order date</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreateDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Plan delivery date</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestDate" Text='<%# DataBinder.Eval(Container.DataItem, "RequestDate")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAddPO" runat="Server" CommandName="AddPO" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="fa fa-plus"></span></asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidPOCode" Value='<%# DataBinder.Eval(Container.DataItem, "POCode")%>' />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-show-product" style="z-index: 1051;">
        <div class="modal-dialog modal-lg" style="max-width: 700px;">
            <div class="modal-content">

                <asp:UpdatePanel ID="UpModalShowPOItem" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="modal-header modal-header2  p-l-0">
                                    <div class="col-sm-12">
                                        <asp:HiddenField ID="test1" runat="server"></asp:HiddenField>
                                        <div class="modal-title sub-title " style="font-size: 16px;">รหัสใบสั่งซื้อ (PO) : <asp:Label ID="lblPOCodeTest" runat="server"></asp:Label></div>
                                    </div>
                                    <span>
                                        <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="card-block">

                                        <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Product code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCode_gvPOItem" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Product name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName_gvPOItem" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>

                                        <div class="text-center m-t-20 col-sm-12">
                                            <asp:LinkButton ID="btnAddInventoryDetail" class="button-action button-add" OnClick="btnAddInventoryDetail_Click" data-backdrop="false" runat="server">Add product PO</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" class="button-action button-delete" OnClick="btnCancel_Click" runat="server">Cancal</asp:LinkButton>
                                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            </div>
                    </div>

    
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-show-Map" style="z-index: 1051;">
        <div class="modal-dialog modal-lg" style="max-width: 700px;">
            <div class="modal-content">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="modal-header modal-header2  p-l-0">
                                    <div class="col-sm-12">
                                        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                        <div class="modal-title sub-title " style="font-size: 16px;">Inventory Name : <asp:Label ID="LbNameInventory" runat="server"></asp:Label></div>
                                    </div>
                                    <span>
                                        <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="card-block">
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Lat</label>
                                            <div class="col-sm-4">
                                                <asp:Label ID="PopmapLbLat"  runat="server"></asp:Label>
                                            </div>

                                            

                                            <label class="col-sm-2 col-form-label">Long</label>
                                            <div class="col-sm-4">
                                                <asp:Label ID="PopmapLbLong"  runat="server"></asp:Label>
                                            </div>
                                           
                                          
                                       
                                        </div>
                                        <div>
                                            MAP Wutdy

                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtareMap" runat="server" class="form-control"></asp:TextBox>

                                        </div>
                                        

                                        
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            </div>
                    </div>
</asp:Content>

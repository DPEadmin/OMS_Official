<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="InventoryDetail.aspx.cs" Inherits="DOMS_TSR.src.InventoryManagement.InventoryDetail" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .hideText  {
    width:20rem;
    overflow:hidden;
    text-overflow:ellipsis;
    white-space:nowrap;
 }
    </style>

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

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">

                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูลคลังสินค้า</div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">รหัสคลังสินค้า</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblInventoryCode" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">ชื่อคลังสินค้า</label>
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
                                                <label class="font-weight-bold">ที่อยู่</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">แขวง/ตำบล</label>
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
                                                <label class="font-weight-bold">เขต/อำเภอ</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">จังหวัด</label>
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
                                                <label class="font-weight-bold">รหัสไปรษณีย์</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">เบอร์โทรศัพท์</label>
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
                                                <label class="font-weight-bold">สายส่ง</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                -
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูลสินค้าในคลัง</div>
                            </div>
                            <div class="card-body">

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ชื่อผู้ผลิต</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchSupplierName" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">ชื่อร้านค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="m-b-10">
                                    <asp:LinkButton ID="btnChooseProduct" class="button-action button-add" data-backdrop="false" runat="server" OnClick="btnChooseProduct_Click"><i class="fa fa-plus m-r-5"></i>Choose Product</asp:LinkButton>
                                    <asp:LinkButton ID="btnChoosePO" class="button-action button-add" runat="server" OnClick="btnChoosePO_Click"><i class="fa fa-plus m-r-5"></i>Choose PO</asp:LinkButton>
                                </div>

                                <asp:GridView ID="gvInventoryDetail" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvInventoryDetail_RowCommand">                                   
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
                                                <div align="left">วันที่ปรับปรุงล่าสุด</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpdateDate" Text='<%# DataBinder.Eval(Container.DataItem, "UpdateDate")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">รหัสสินค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode"),DataBinder.Eval(Container.DataItem, "InventoryDetailID")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">ชื่อสินค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">ประเภทสินค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">ชื่อผู้ผลิต</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">ร้านค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">On Hand</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOnhand" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Reserved</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReserved" Text='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Balance</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalance" Text='<%# DataBinder.Eval(Container.DataItem, "Balance")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">จุดสั่งซื้อ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ReOrder")%>' runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                            <ItemTemplate>
                                                <!--<asp:LinkButton ID="btnEdit" runat="Server" CommandName="EditInventoryDetail" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="Server" CommandName="SaveInventoryDetail" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="fas fa-save"> </span></asp:LinkButton>-->

                                                <asp:HiddenField runat="server" ID="hidInventoryDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryDetailId")%>' />
                                                <asp:HiddenField runat="server" ID="hidInventoryCode" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidQTY" Value='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' />
                                                <asp:HiddenField runat="server" ID="hidReserved" Value='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' />
                                                <asp:HiddenField runat="server" ID="hidBalance" Value='<%# DataBinder.Eval(Container.DataItem, "Balance")%>' />
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

                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
                                                    <td style=" width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button" ToolTip="First" CommandName="First" Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous" Text="<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>
                                                        of
                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last" Text=">>" OnCommand="GetPageIndex"></asp:Button>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-product">
        <div class="modal-dialog modal-lg" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card-block">

                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtSearchProductCode_ProductModal" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>

                                            <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtSearchProductName_ProductModal" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">ชื่อผู้ผลิต</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlSearchSupplierName_ProductModal" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>

                                            <label class="col-sm-2 col-form-label">ชื่อร้านค้า</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlSearchMerchantName_ProductModal" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">ประเภทสินค้า</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlSearchCategory_ProductModal" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="text-center m-t-20 col-sm-12">
                                            <asp:Button ID="btnSearch_ProductModal" Text="ค้นหา" class="button-pri button-accept m-r-10" OnClick="btnSearch_ProductModal_Click" runat="server" />
                                            <asp:Button ID="btnClearSearch_ProductModal" Text="ล้าง" class="button-pri button-cancel" OnClick="btnClearSearch_ProductModal_Click" runat="server" />
                                        </div>

                                        <hr />

                                        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             OnRowCommand="gvProduct_RowCommand">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">รหัสสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProducCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' style="text-overflow: ellipsis;" runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อผู้ผลิต</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อร้านค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">จำนวนสินค้าที่นำเข้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountIns" Style="text-align:right" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddProduct" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

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
                        <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                            <tr height="30" bgcolor="#ffffff">
                                <td width="100%" align="right" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                        <tr>
                                            <td style="font-size: 8.5pt;">                                               
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductFirst" CssClass="Button" ToolTip="gvProductFirst" CommandName="gvProductFirst" OnCommand="GetPagegvProductIndex"
                                                    Text="<<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductPre" CssClass="Button" ToolTip="gvProductPrevious" CommandName="gvProductPrevious" OnCommand="GetPagegvProductIndex"
                                                    Text="<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlgvProductPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                      >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblgvProductTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductNext" CssClass="Button" ToolTip="gvProductNext" runat="server" CommandName="gvProductNext" Text=">" OnCommand="GetPagegvProductIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvProductLast" CssClass="Button" ToolTip="gvProductLast" runat="server" CommandName="gvProductLast"
                                                    Text=">>" OnCommand="GetPagegvProductIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>


                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
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
                                            <label class="col-sm-2 col-form-label">รหัสใบสั่งซื้อ (PO)</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtSearchPOCode_POModal" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>

                                            <label class="col-sm-2 col-form-label">ชื่อผู้ผลิต</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlSearchSupplierName_POModal" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">ชื่อร้านค้า</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlSearchMerchantName_POModal" runat="server" class="form-control"></asp:DropDownList>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>

                                            <label class="col-sm-2 col-form-label">วันที่สั่งสินค้า</label>
                                            <div class="col-sm-3">
                                                <div class="input-group mb-0">

                                                    <asp:TextBox ID="txtSearchCreateDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchCreateDateFrom" runat="server" TargetControlID="txtSearchCreateDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    <asp:TextBox ID="txtSearchCreateDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchCreateDateTo" runat="server" TargetControlID="txtSearchCreateDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">วันที่ส่งสินค้า</label>
                                            <div class="col-sm-3">
                                                <div class="input-group mb-0">
                                                    <asp:TextBox ID="txtSearchRequestDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchRequestDateFrom" runat="server" TargetControlID="txtSearchRequestDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    <asp:TextBox ID="txtSearchRequestDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
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
                                                        <div align="left">รหัสใบสั่งซื้อ (PO)</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOCode" Text='<%# DataBinder.Eval(Container.DataItem, "POCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อผู้ผลิต</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อร้านค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreateDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่ส่งสินค้า</div>
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
                                                        <div align="Center">รหัสสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCode_gvPOItem" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อสินค้า</div>
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
                                            <asp:LinkButton ID="btnAddInventoryDetail" class="button-action button-add" OnClick="btnAddInventoryDetail_Click" data-backdrop="false" runat="server">เพิ่มสินค้าใน PO</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" class="button-action button-delete" OnClick="btnCancel_Click" runat="server">ยกเลิก</asp:LinkButton>
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


</asp:Content>

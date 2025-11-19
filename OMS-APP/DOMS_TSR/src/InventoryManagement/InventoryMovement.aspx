<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="InventoryMovement.aspx.cs" Inherits="DOMS_TSR.src.InventoryManagement.InventoryMovement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                                <div class="sub-title">รายละเอียดสินค้าหมุนเวียนในคลังสินค้า</div>
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
                                                <label class="font-weight-bold">รหัสสินค้า</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblProductCode" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">ชื่อสินค้า</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblProductName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">ประเภทสินค้า</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblProductCategoryName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm">
                                        <div class="row">
                                            <div class="col-6 col-sm-6">
                                                <label class="font-weight-bold">แบรนด์</label>
                                            </div>
                                            <div class="col-6 col-sm-6">
                                                <asp:Label ID="lblProductBrand" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>  
                        
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูล Movement ของสินค้าในคลัง</div>
                            </div>
                            <div class="card-body">

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">MovementID</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchInventoryManualLotCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">รหัสใบสั่งซื้อ (PO)</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchPOCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสใบรับสินค้าสินค้า (GR)</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchGRCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">ชื่อผู้ผลิต (Supplier)</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchSupplierName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย (SO)</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchOrderNo" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Lot No.</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchInventoryMovementCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ผู้ดำเนินการ</label>
                                    <div class="col-sm-3">
                                        <div class="input-group mb-0">
                                            <asp:TextBox ID="txtSearchEmpFNameTH" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtSearchEmpLNameTH" class="form-control" placeholder="สกุล" runat="server"></asp:TextBox>
                                         </div>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">วันที่</label>
                                    <div class="col-sm-3">
                                        <div class="input-group mb-0">
                                            <asp:TextBox ID="txtSearchCreateDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchCreateDateFrom" runat="server" TargetControlID="txtSearchCreateDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchCreateDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchCreateDateTo" runat="server" TargetControlID="txtSearchCreateDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                         </div>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="ค้นหา" class="button-pri button-accept m-r-10" OnClick="btnSearch_Clicked" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">                               
                                <asp:GridView ID="gvInventoryMovement" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
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
                                                <div align="left">วันที่</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <!--<asp:Label ID="lblCreateDate1" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />-->
                                                <asp:Label ID="lblCreateDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MovementID</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventoryManualLotCode" Text='<%# DataBinder.Eval(Container.DataItem, "InventoryManualLotCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="16%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Lot No</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventoryMovementCode" Text='<%# DataBinder.Eval(Container.DataItem, "InventoryMovementCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">รหัสใบสั่งซื้อ<br />(PO Number)</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOCode" Text='<%# DataBinder.Eval(Container.DataItem, "POCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">รหัสรับสินค้า<br /> (GR)</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGRCode" Text='<%# DataBinder.Eval(Container.DataItem, "GRCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ราคา<br /> (Supplier)</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblprice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ชื่อผู้ผลิต<br /> (Supplier)</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="12%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">รหัสใบสั่งขาย<br /> (SO)</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrderNo" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNo")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                       

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">ผู้ดำเนินการ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">หมายเหตุ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button" ToolTip="First" CommandName="First" Text="<<" runat="server" OnCommand="GetPageIndex" ></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous" Text="<" runat="server" OnCommand="GetPageIndex" ></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" ></asp:DropDownList>
                                                        of
                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex" ></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last" Text=">>" OnCommand="GetPageIndex" ></asp:Button>
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

</asp:Content>
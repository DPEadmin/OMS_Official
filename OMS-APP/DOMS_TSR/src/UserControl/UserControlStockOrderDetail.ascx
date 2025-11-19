<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlStockOrderDetail.ascx.cs" Inherits="DOMS_TSR.src.UserControl.UserControlStockOrderDetail" %>
     <asp:HiddenField ID="hidEmpCode" runat="server" />
     <asp:HiddenField ID="hidInventorySelected" runat="server" />

<div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ข้อมูล Order</div>
                            </div>

                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblOrderCode" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ:</label>

                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCreateDate" runat="server" Text="-"></asp:Label>
                                </div>
                                <div class="form-group row">
                                <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblOrderStatus" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">วันที่จัดส่ง:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblDeliveryDate" runat="server" Text="-"></asp:Label>
                                </div>
      
                                <div class="form-group row">
                                  
                                    <!--<label class="col-sm-2 col-form-label">หมายเหตุ:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblRemark" runat="server" Text="-"></asp:Label> -->
                                </div>

                        

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ลำดับการสั่งซื้อ:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblBranchOrderID" runat="server" Text="-"></asp:Label>
                                </div>

                                <asp:Panel ID="Panel_All" runat="server" Style="overflow-x: scroll;">
                                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvProduct_RowDataBound">

                                        <Columns>
                                             <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">รหัสสินค้า</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">ชื่อสินค้า</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="right">จำนวนสินค้าของ Order</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <EmptyDataTemplate>
                                            <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">เปลียนคลังสินค้า</div>
                            </div>

                            <div class="card-body">
                                  <div class="form-group row">
                                    <label class="col-sm-12 col-form-label">หมายเหตุ</label>
                                    <div class="col-sm-12">
                                      <asp:DropDownList ID="ddlInventory" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlInventory_SelectedChanged" class="form-control"></asp:DropDownList>
                                    </div>
                                      <div>
                                           <asp:GridView ID="gvProductInventory" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0" class="transport" border="0" OnRowDataBound="gvProductInventory_RowDatabound"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">รหัสสินค้า</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">ชื่อสินค้า</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">จำนวน</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Onhand</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Reserved</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReserved" Text='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Current</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrent" Text='<%# DataBinder.Eval(Container.DataItem, "Current")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Balance</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" Text='<%# DataBinder.Eval(Container.DataItem, "Balance")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty1" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>
                                      </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-12 col-form-label">หมายเหตุ</label>
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="txtDetailbackOrder" class="form-control" runat="server" Height="100px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            
                                <div class="text-center m-t-20 col-sm-12">
                                      <asp:Button ID="Button3" Text="บันทึกเปลียนคลัง" OnClick="btnsubmit_Click"
                                          class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="Button1" Text="ตี Back Oreder" OnClick="btnsubmit_Click"
                                          class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="Button2" Text="ล้าง" OnClick="btnClear_Click"
                                        class="button-pri button-cancel"
                                        runat="server" />

                                </div>
                        </div>

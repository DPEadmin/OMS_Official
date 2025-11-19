<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlOrderChangeStatus.ascx.cs" Inherits="DOMS_TSR.src.UserControl.UserControlOrderChangeStatus" %>
     <asp:HiddenField ID="hidEmpCode" runat="server" />
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
                                                    <div align="center">สินค้า</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="right">จำนวน</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="left">reserve</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblreserve" Text='จำนวน reserve' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                 <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="left">Curent</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurent" Text='จำนวน Curent' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="left">Balance</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurent" Text='จำนวน Balance' runat="server" />
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
                                <div class="sub-title">Note การตี Back Order</div>
                            </div>

                            <div class="card-body">


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

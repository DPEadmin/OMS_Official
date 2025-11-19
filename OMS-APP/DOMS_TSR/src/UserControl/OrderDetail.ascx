<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.ascx.cs" Inherits="DOMS_TSR.src.UserControl.OrderDetail" %>
<asp:HiddenField ID="hidEmpCode" runat="server" />
<asp:HiddenField ID="HidInventory" runat="server" />
<div class="page-body">
    <style>
        .centerline {
            text-decoration:line-through;
        }
    </style>
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header border-0">
                    <div class="sub-title">ข้อมูลลูกค้า</div>
                </div>

                <div class="card-body">
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCustomerName" runat="server" Text="-"></asp:Label>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-2 col-form-label">รหัสลูกค้า:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCustomerCode" runat="server" Text="-"></asp:Label>
                    </div>

                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ :</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCustomerTel1" runat="server" Text="-"></asp:Label>
                      
                    </div>
                </div>

            </div>

            <div class="card">
                <div class="card-header border-0">
                    <div class="sub-title">รายละเอียดใบสั่งขาย</div>
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
                             <label class="col-sm-2 col-form-label">วันที่นัดส่งสินค้า:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblDeliveryDate" runat="server" Text="-"></asp:Label>
                          <div class="col-sm-1"></div>
                            <label class="col-sm-2 col-form-label">แบรนด์:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lbBrand" runat="server" Text="-"></asp:Label>
                      </div>

                    <div class="form-group row">
                           <label class="col-sm-2 col-form-label">ขั้นตอน:</label>
                         <asp:Label CssClass="col-sm-3 col-form-label" ID="lblOrderStatus" runat="server" Text="-"></asp:Label>
                       
                        <div class="col-sm-1"></div>
                        <label class="col-sm-2 col-form-label">สถานะใบการสั่งขาย:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblOrderStatename" runat="server" Text="-"></asp:Label>
                        <div class="col-sm-1"></div>
                   
                    </div>

              



                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblChannel" runat="server" Text="-"></asp:Label>
                            <div class="col-sm-1"></div>
                        <label class="col-sm-2 col-form-label">การชำระเงิน</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lbPay" runat="server" Text="-"></asp:Label>
                    </div>
                          <div class="form-group row">

                      <label class="col-sm-2 col-form-label">คลังสินค้า:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lbInventory" runat="server" Text="-"></asp:Label>
                        <div class="col-sm-1"></div>
                          <label class="col-sm-2 col-form-label">เลขพัสดุ:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblOrderTrackinNo" runat="server" Text="-"></asp:Label>
                    </div>


                    <asp:Panel ID="Panel_All" runat="server" Style="overflow-x: scroll;">
                        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvProduct_RowDataBound">

                            <Columns>
                                
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">รหัสสินค้า</div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                          <asp:Label ID="lbFlagProSetHeader" Text='<%# DataBinder.Eval(Container.DataItem, "FlagProSetHeader")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">รายการสินค้า</div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="right">จำนวน</div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                      
                                        <asp:Label ID="lblNetPrice" Text='<%# DataBinder.Eval(Container.DataItem, "NetPrice","{0:N2}")%>' runat="server"  Visible="false"/>
                                        <asp:HiddenField runat="server" ID="hidTransportPrice" Value='<%# DataBinder.Eval(Container.DataItem, "TransportPrice")%>'  />
                                      <asp:Label ID="lbplush" Text="x" runat="server" Visible="false" />
                                        
                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                             <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left">หน่วย</div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" Text='<%# DataBinder.Eval(Container.DataItem, "unitname")%>' runat="server" />
                                    </ItemTemplate>
                                             </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left"></div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                         <asp:Label ID="lblProductPrice" Text='<%# DataBinder.Eval(Container.DataItem, "ProductPrice","{0:N2}")%>' CssClass="centerline"  runat="server"  />
                                        <asp:HiddenField ID="hidDiscount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left">ราคาต่อหน่วย</div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                         <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>'  runat="server"  /> ฿
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="right">ราคารวม</div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalPrice" Text='<%# DataBinder.Eval(Container.DataItem, "TotalPrice","{0:N2}")%>' runat="server" />
                                         <asp:Label ID="lbbath" Text="฿" runat="server" />
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

            <div class="card-deck" style="margin-bottom: 10px">
                <div class="card">
              <%--      <div class="card-header border-0">
                        <div class="sub-title">Note ในการสั่งครั้งนี้</div>
                    </div>--%>
                    <div class="row">
                        <div class="card-body">
                             <label style="font-weight: bold">Note ในการสั่งครั้งนี้:</label>
                            <asp:Label ID="lblOrderNote" runat="server" Text="-"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="card">
             <%--       <div class="card-header border-0">
                        <div class="sub-title">การชำระเงิน</div>
                    </div>--%>

                    <div class="card-body">

               
                        <div class="row">
                            <div class="col-sm-6">
                                <label style="font-weight: bold">
                                    ราคาสินค้ารวม
                                    <asp:Label ID="lblCountsumTotalPrice" runat="server" Text="0" Visible="false"></asp:Label>
                                    :</label>
                            </div>

                            <div class="col-sm-6 text-right">
                                <asp:Label ID="lblsumTotalPrice" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6">
                                <label style="font-weight: bold">ส่วนลดจากโปรท้ายบิล:</label>
                            </div>

                            <div class="col-sm-6 text-right">
                                <label>0.00฿</label>
                            </div>
                        </div>

                        <div class="row" style="display:none">
                            <div class="col-sm-6">
                                <label style="font-weight: bold">Gift Voucher / e-Voucher:</label>
                            </div>

                            <div class="col-sm-6 text-right">
                                <label>0.00฿</label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6">
                                <label style="font-weight: bold">Vat </label>
                                  <asp:Label ID ="lblvat" runat="server" Font-Bold="true" ></asp:Label>
                             
                            </div>

                            <div class="col-sm-6 text-right">
                                <asp:Label ID="lblsumVat" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6">
                                <label style="font-weight: bold">ค่าจัดส่ง:</label>
                            </div>

                            <div class="col-sm-6 text-right">
                                <asp:Label ID="lblTransportPrice" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6">
                                <label style="font-weight: bold">ราคาที่ต้องชำระ:</label>
                            </div>

                            <div class="col-sm-6 text-right">
                                <asp:Label ID="lblsumAllPrice" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>


            </div>

            <div class="card-deck" style="margin-bottom: 10px">
                <div class="card">
                    <div class="card-header border-0">
                        <div class="sub-title">ที่อยู่จัดส่ง</div>
                    </div>

                    <div class="card-body">
                        <asp:Label ID="lblDeliveryAddress" runat="server" Text="-"></asp:Label>
                    </div>
                </div>

                <div class="card">
                    <div class="card-header border-0">
                        <div class="sub-title">ที่อยู่ใบกำกับภาษี</div>
                    </div>

                    <div class="card-body">
                        <asp:Label ID="lblReceiptAddress" runat="server" Text="-"></asp:Label>
                    </div>
                </div>
            </div>



                  <div class="card" id="divProductinventory" runat="server" >
                <div class="card-header border-0">
                    <div class="sub-title">สินค้าคงคลัง</div>
                </div>

                <div>
                    <asp:GridView ID="gvProductInventory" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                        TabIndex="0" Width="100%" CellSpacing="0" class="transport" border="0" OnRowDataBound="gvProductInventory_RowDatabound"
                        ShowHeaderWhenEmpty="true">

                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <center>
                                            <div align="center">รหัสสินค้า</div>
                                        </center>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <center>
                                            <div >รายการสินค้า</div>
                                        </center>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <center>
                                            <div align="center">จำนวน</div>
                                        </center>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <center>
                                            <div align="center">Onhand</div>
                                        </center>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblQTY" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <center>
                                            <div align="center">Reserved</div>
                                        </center>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReserved" Text='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <center>
                                            <div align="center">Current</div>
                                        </center>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrent" Text='<%# DataBinder.Eval(Container.DataItem, "Current")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
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



            <div class="card">
                <div class="card-header border-0">
                    <div class="sub-title">ประวัติการดำเนินงาน</div>
                </div>

                <div class="card-body">
                    <asp:GridView ID="gvOrderActivity" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvOrderActivity_RowDataBound">

                        <Columns>

                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">วันที่ดำเนินการ</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblCreateDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">ขั้นตอน</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">สถานะใบสั่งขาย</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStateName" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">ดำเนินการโดย</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblEmpName" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">หมายเหตุ</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblNote" Text='<%# DataBinder.Eval(Container.DataItem, "NOTE")%>' runat="server" />
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
            </div>

                <div class="card" id ="divgvtransport" runat="server">
                <div class="card-header border-0">
                    <div class="sub-title">ติดตามสถานะขนส่ง</div>
                </div>
                    <asp:HiddenField ID="HidtrackingtransportNo" runat="server" />
                      <asp:HiddenField ID="HidToken" runat="server" />
                <div class="card-body">
                    <asp:GridView ID="GVStatusTransport" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                        <Columns>

                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">วันที่ดำเนินการ</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblCreateDate" Text='<%# DataBinder.Eval(Container.DataItem, "status_date")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">status Code</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "status")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">ขั้นตอน</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "status_description")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                 

                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">ดำเนินการ</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblEmpName" Text='<%# DataBinder.Eval(Container.DataItem, "receiver_name")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                <HeaderTemplate>
                                    <div align="center">หมายเหตุ</div>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblNote" Text='<%# DataBinder.Eval(Container.DataItem, "status_description")%>' runat="server" />
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
            </div>
      

        </div>
    </div>

</div>



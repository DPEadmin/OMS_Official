<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="OrderPaymentDetail.aspx.cs" Inherits="DOMS_TSR.src.OrderPaymentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">

    <style>
        .btn {
            text-align: left;
        }
        

        .text-bold {
            font-weight: bold;
        }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hidBranchcode" runat="server" />
    <asp:HiddenField ID="hidEmpCode" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-body">
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
                                    <label class="col-sm-2 col-form-label">เบอร์ติดต่อ 1:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCustomerTel1" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">เบอร์ติดต่อ 2:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCustomerTel2" runat="server" Text="-"></asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ข้อมูลบัตรเครดิต</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">จำนวนงวด:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblInstallment" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">จ่ายงวดละ:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblInstallmentPrice" runat="server" Text="-"></asp:Label>
                                </div>
                                 <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">จ่ายงวดแรก:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblFirstInstallment" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">ธนาคารผู้ออกบัตร:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCardIssuename" runat="server" Text="-"></asp:Label>
                                </div>
                                    <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ชนิตบัตร:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCardType" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">เลขที่บัตร:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCardNo" runat="server" Text="-"></asp:Label>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">CVC:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCVCNo" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">ชื่อผู้ถือบัตร:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCardHolderName" runat="server" Text="-"></asp:Label>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">เดือน/ปีที่หมดอายุ:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCardExpYear" runat="server" Text="-"></asp:Label>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">เลขบัตรประชาชน:</label>
                                    <asp:Label CssClass="col-sm-3 col-form-label" ID="lblCitizenId" runat="server" Text="-"></asp:Label>
                                </div>
                            </div>
                        </div>

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
                                     <div class="col-sm-1"></div>
                          <label class="col-sm-2 col-form-label">เลขพัสดุ:</label>
                        <asp:Label CssClass="col-sm-3 col-form-label" ID="lblOrderTrackinNo" runat="server" Text="-"></asp:Label>
                                </div>

                                <asp:Panel ID="Panel_All" runat="server" Style="overflow-x: scroll;">
                                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvProduct_RowDataBound">

                                        <Columns>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">สินค้า</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="right">จำนวน</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:N2}")%>' Style="text-decoration: line-through" runat="server" />
                                                    <asp:Label ID="lblNetPrice" Text='<%# DataBinder.Eval(Container.DataItem, "NetPrice","{0:N2}")%>' runat="server" />
                                                    <asp:HiddenField runat="server" ID="hidTransportPrice" Value='<%# DataBinder.Eval(Container.DataItem, "TransportPrice")%>' />
                                                    x
                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="right">ราคารวม</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalPrice" Text='<%# DataBinder.Eval(Container.DataItem, "TotalPrice","{0:N2}")%>' runat="server" />฿
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
                                <div class="card-header border-0">
                                    <div class="sub-title">การชำระเงิน</div>
                                </div>

                                <div class="card-body">

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label style="font-weight: bold">วิธีการชำระเงิน:</label>
                                        </div>

                                        <div class="col-sm-6 text-right">
                                            <asp:Label ID="lblPayDetail" runat="server">ชำระด้วยเงินสด</asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label style="font-weight: bold">
                                                ราคารวม(<asp:Label ID="lblCountsumTotalPrice" runat="server" Text="0"></asp:Label>
                                                รายการ):</label>
                                        </div>

                                        <div class="col-sm-6 text-right">
                                            <asp:Label ID="lblsumTotalPrice" runat="server" Text="-"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label style="font-weight: bold">ส่วนลด:</label>
                                        </div>

                                        <div class="col-sm-6 text-right">
                                            <label>0.00฿</label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label style="font-weight: bold">Gift Voucher / e-Voucher:</label>
                                        </div>

                                        <div class="col-sm-6 text-right">
                                            <label>0.00฿</label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label style="font-weight: bold">Vat</label>
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

                            <div class="card">
                                <div class="card-header border-0">
                                    <div class="sub-title">Note ในการสั่งครั้งนี้</div>
                                </div>

                                <div class="card-body">
                                    <asp:Label ID="lblOrderNote" runat="server" Text="-"></asp:Label>
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

                        



                  

                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ประวัติ</div>
                            </div>

                            <div class="card-body">
                                <asp:GridView ID="gvOrderActivity" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvOrderActivity_RowDataBound">

                                        <Columns>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">วันที่</div>
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
                                                    <div align="center">ผู้ดำเนินการ</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName")%>' runat="server" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

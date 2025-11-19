<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/src/MasterPage/WebReatil.Master" CodeBehind="OrderHistoryTakeOrder.aspx.cs" Inherits="DOMS_TSR.src.OrderManagement.OrderHistoryTakeOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

</script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="page-body">
        <div class="col-sm-12">
            <!-- Basic Form Inputs card start -->
            <div class="card">
                <div class="card-header">
                    <div class="sub-title">ค้นหาข้อมูลประวัติการสั่งซื้อ (Order History)</div>
                </div>
                <div class="card-block">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>


                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtOrderCode_Search" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-1 col-form-label"></label>
                                <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
                                <div class="col-sm-3">


                                    <asp:DropDownList ID="ddlOrderStatusCode_Search" class="form-control" runat="server"></asp:DropDownList>
                                    <input type="hidden" id="hidIdList" runat="server" />
                                    <input type="hidden" id="hidFlagInsert" runat="server" />
                                    <asp:HiddenField ID="hidFlagDel" runat="server" />
                                    <input type="hidden" id="hidaction" runat="server" />
                                    <asp:HiddenField ID="hidMsgDel" runat="server" />
                                    <asp:HiddenField ID="hidOrderCodeHistory" runat="server" />
                                    <asp:HiddenField ID="hidEmpCode" runat="server" />
                                </div>

                                <!--           <label class="col-sm-2 col-form-label">ชนิดใบสั่งขาย</label>
              <div class="col-sm-3">
                  <asp:DropDownList ID="ddlOrderTypeCode_Search" class="form-control" runat="server"></asp:DropDownList>
                  </div>
              <label class="col-sm-1 col-form-label"></label>-->


                                <label class="col-sm-2 col-form-label">วันที่สร้างใบสั่งขาย</label>
                                <div class="col-sm-3">
                                    <div class="input-group">

                                        <%--                <asp:TextBox  ID="txtOrderDateFrom_Search" class="form-control input-sm" runat="server" type="date"></asp:TextBox>
             
                <span class="input-group-addon"style="background-color: deepskyblue"> ถึง</span>
               
                 <asp:TextBox  ID="txtOrderDateTo_Search" class="form-control input-sm  " runat="server" type="date"></asp:TextBox>--%>

                                        <asp:TextBox ID="txtOrderDateFrom_Search" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="carSearchEndDateFrom" runat="server" TargetControlID="txtOrderDateFrom_Search" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                          <asp:TextBox ID="txtOrderDateTo_Search" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="carSearchEndDateTo" runat="server" TargetControlID="txtOrderDateTo_Search" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                        
                                    </div>
                                </div>
                                <label class="col-sm-1 col-form-label"></label>
                                <label class="col-sm-2 col-form-label">วันนัดส่งสินค้า</label>
                                <div class="col-sm-3">
                                    <div class="input-group">

                                        <%--  <asp:TextBox  ID="txtDeliveryDateFrom_Search" class="form-control input-sm" runat="server" type="date"></asp:TextBox>
       
          <span class="input-group-addon" style="background-color: deepskyblue"> ถึง</span>
         
           <asp:TextBox  ID="txtDeliveryDateTo_Search" class="form-control input-sm  " runat="server" type="date"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtDeliveryDateFrom_Search" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDeliveryDateFrom_Search" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                          <asp:TextBox ID="txtDeliveryDateTo_Search" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDeliveryDateTo_Search" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                                <label class="col-sm-1 col-form-label"></label>

                                <label class="col-sm-2 col-form-label">วันที่รับสินค้า</label>
                                <div class="col-sm-3">
                                    <div class="input-group">

                                        <%--                <asp:TextBox  ID="txtDateReceivedFrom_Search" class="form-control input-sm" runat="server" type="date"></asp:TextBox>
             
                <span class="input-group-addon"style="background-color: deepskyblue"> ถึง</span>
               
                 <asp:TextBox  ID="txtDateReceivedTo_Search" class="form-control input-sm  " runat="server" type="date"></asp:TextBox>--%>

                                        <asp:TextBox ID="txtDateReceivedFrom_Search" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateReceivedFrom_Search" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                          <asp:TextBox ID="txtDateReceivedTo_Search" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateReceivedTo_Search" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="text-center m-t-20 col-sm-12">
                                <asp:Button ID="btnSearch" Text="Search" CssClass="button-pri button-accept m-r-10" OnClick="btnSearch_Click" runat="server" />
                                <asp:Button ID="btnClearSearch" Text="Clear" CssClass="button-pri button-cancel " OnClick="btnClearSearch_Click" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="card">
                <div class="card-block">
                    <div class="m-b-10">
                        <!--Start modal Add Product-->
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="dt-responsive table-responsive">
                                <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " Style="white-space: nowrap" OnRowCommand="gvOrderHistory_OnRowCommand"
                                    TabIndex="0" Width="100%" CellSpacing="0"
                                    ShowHeaderWhenEmpty="true">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">รหัสใบสั่งขาย</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnOrderCode" runat="Server" CommandName="ShowOrderDetail" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                    class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">สถานะใบสั่งขาย</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrderStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">ยอดชำระใบสั่งขาย</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalPrice" Text='<%# DataBinder.Eval(Container.DataItem, "TotalPrice", "{0:n}")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">วันที่สร้างใบสั่งขาย</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreateDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">วันนัดส่งสินค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate", "{0:n}")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">วันที่รับสินค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReceiveDate" Text='<%# DataBinder.Eval(Container.DataItem, "ReceivedDate", "{0:n}")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hidOrderCode" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
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
                                <%-- PAGING CAMPAIGN --%>
                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
                                                    <td style="font-size: 8.5pt;">
                                                        <%--Rows per page 
                                                            <asp:DropDownList ID="ddlRows" runat="server" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlRows_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True">10</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>40</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                    </td>
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button" ToolTip="First" CommandName="First"
                                                            Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                            Text="<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                        of
                                                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                            Text=">>" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            </div>
          </div>
          <!-- Basic Form Inputs card end -->
                            </div>
      </div>
      <!-- Basic Form Inputs card end -->
                            </div>
  </div>
</div>
          
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <div class="card-block">
                        <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                            aria-hidden="true" id="modal-orderhistorydetail">
                            <div class="modal-dialog modal-lg" style="max-width: 1300px;">
                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-content">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="modal-header modal-header2 ">
                                                        <div class="col-sm-11">
                                                            <div id="exampleModalLongTitle" runat="server" class="sub-title">
                                                                รายละเอียดใบสั่งขาย (Order Detail)
                                                            </div>
                                                            <div id="exampleModalLongTitle1" runat="server">
                                                                เลขที่ใบสั่งขาย 
                                      <asp:Label ID="lblHeadOrderCode" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="card-block">

                                                            <div class="dt-responsive table-responsive">
                                                                <asp:GridView ID="gvOrderHistoryDetail" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                                    TabIndex="0" Width="100%" CellSpacing="0"
                                                                    ShowHeaderWhenEmpty="true">

                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">รหัสสินค้า</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">ชื่อสินค้า</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">ราคาสินค้า</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:n}")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">ส่วนลด(บาท)</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDiscountAmount" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount", "{0:n}")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">ส่วนลด(%)</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDiscountPercent" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">ราคาสุทธิ</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNetPrice" Text='<%# DataBinder.Eval(Container.DataItem, "NetPrice", "{0:n}")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">จำนวน</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">หน่วย</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                            <HeaderTemplate>
                                                                                <div align="center">ราคารวม(บาท)</div>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalPrice" Text='<%# DataBinder.Eval(Container.DataItem, "TotalPrice", "{0:n}")%>' runat="server" />
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
                                                            <div class="row" style="margin-top: 10px;">
                                                                <div class="col-md-6"></div>
                                                                <div class="col-md-6">

                                                                    <div>

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
                                                                                <%--<label style="font-weight: bold">
                                                ราคารวม(<asp:Label ID="lblCountsumTotalPrice" runat="server" Text="0"></asp:Label>
                                                รายการ):</label>--%>
                                                                                <label style="font-weight: bold">ราคารวม (ก่อน VAT) :</label>
                                                                            </div>

                                                                            <div class="col-sm-6 text-right">
                                                                                <asp:Label ID="lblsumTotalPrice" runat="server" Text="-"></asp:Label>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-6">
                                                                                <label style="font-weight: bold">ส่วนลดท้ายบิล :</label>
                                                                            </div>

                                                                            <div class="col-sm-6 text-right">
                                                                                <label>0.00฿</label>
                                                                            </div>
                                                                        </div>

                                                                        <%--<div class="row">
                                        <div class="col-sm-6">
                                            <label style="font-weight: bold">Gift Voucher / e-Voucher:</label>
                                        </div>

                                        <div class="col-sm-6 text-right">
                                            <label>0.00฿</label>
                                        </div>
                                    </div>--%>

                                                                        <div class="row">
                                                                            <div class="col-sm-6">
                                                                                <label style="font-weight: bold">Vat 7%:</label>
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
                                                        </div>
                                                        <!-- Basic Form Inputs card end -->
                                                    </div>
                                                </div>
                                                <!-- Basic Form Inputs card end -->
                                            </div>
                                        </div>
                                        </div>

                    
                </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <label class="col-sm-2 col-form-label"></label>

                                <div class="text-center m-t-20 center">
                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

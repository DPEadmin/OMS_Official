<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ReaportMediaDailySale.aspx.cs" Inherits="DOMS_TSR.src.Report.ReaportMediaDailySale" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="/code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
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

    <asp:HiddenField ID="hidBranchcode" runat="server" />
    <asp:HiddenField ID="hiddisplayname" runat="server" />
    <asp:HiddenField ID="hidordermsg" runat="server" />

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidTabNo" runat="server" />
            
                    <div class="page-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="card">
                                    <div class="card-header border-0">
                                        <div class="sub-title">รายงานมีเดีย Daily Sale </div>
                                    </div>

                                    <div class="card-body">

                                        <div id="searchSection_NoAnswerOrder" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchOrderCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">

                                                        <asp:TextBox ID="txtSearchOrderDateFrom_NoAnswerOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchOrderDateUntil_NoAnswerOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCustomerCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchFName_NoAnswerOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchLName_NoAnswerOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                     <%--       <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchContact_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchOrderType_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                            </div>--%>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchDeliverDate_NoAnswerOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchDeliverDate_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchDeliverDateTo_NoAnswerOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchDeliverDateTo_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                     <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchChannel_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnSearch_NoAnswerOrder" Text="ค้นหา" OnClick="btnSearch_Click_NoAnswerOrder" class="button-pri button-accept m-r-10" runat="server" />
                                                <asp:Button ID="btnClearSearch_NoAnswerOrder" Text="ล้าง" OnClick="btnClearSearch_Click_NoAnswerOrder" class="button-pri button-cancel" runat="server" />
                                            </div>

                                        </div>

                                    </div>

                                </div>
                                <div class="card ">
                                        <div class="col-5 m-t-10 m-b-10" >
                               
                            </div>
                               
                                    <div id="GvData" runat="server" visible="false" class="card-block p-t-5"  style="width: 100%;">

                                        <div id="Section_NoAnswerOrder" runat="server">

                                            <input type="hidden" id="hidIdList_NoAnswerOrder" runat="server" />

                                            <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_NoAnswerOrder" OnClick="btnAcceptOrder_NoAnswerOrder_Click" runat="server" Text="Export Excel" />

                                            <asp:Button CssClass="button-pri button-delete" ID="btnCancelOrder_NoAnswerOrder" runat="server" Text="ยกเลิกใบสั่งขาย"  OnClick="btnCancelOrder_NoAnswerOrder_Click" Visible="false" />

                                            <asp:Panel ID="Panel_NoAnswerOrder" runat="server" Style="overflow-x: scroll;">
                                                <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                                    <Columns>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่สั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ORDER_DATE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสใบสั่งขาย</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ORDER_NO")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่ออกอากาศ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_DATE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">เบอร์ติดต่อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ช่องทางการขาย</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CHANNEL")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อโปรแกรม</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PROGRAM_NAME")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสแคมเปญ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_CODE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อแคมเปญ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_NAME")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสโปรโมชั่น</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PROMOTION_CODE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อโปรโมชั่น</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PROMOTION_NAME")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสสินค้า</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PRODUCT_CODE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อสินค้า</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PRODUCT_NAME")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">จำนวน</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ราคา</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PRICE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วิธีการชำระ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PAYMENT_TERM")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสพนักงาน</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "SALE_CODE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อพนักงาน</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "SALE_NAME")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสลูกค้า</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CUSTOMER_CODE")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อลูกค้า</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CUSTOMER_NAME")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">เวลาที่เริ่ม</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "TIME_START")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">เวลาที่สิ้นสุด</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "TIME_END")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ระยะเวลา</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                        
                                                               <asp:Label ID="lblSaleCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "DURATION")%>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                             
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                       <center>
                                                           <asp:Label ID="lblDataEmpty_NoAnswerOrder" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                                       </center>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                                <tr height="30" bgcolor="#ffffff">
                                                    <td width="100%" align="right" valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnFirst_NoAnswerOrder" CssClass="Button" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre_NoAnswerOrder" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                        Text="<" runat="server" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_NoAnswerOrder" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_NoAnswerOrder">
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPages_NoAnswerOrder" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnNext_NoAnswerOrder" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLast_NoAnswerOrder" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
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
                
            </div>

           
        </ContentTemplate>
        
    </asp:UpdatePanel>
 
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         
    </asp:UpdatePanel>
</asp:Content>
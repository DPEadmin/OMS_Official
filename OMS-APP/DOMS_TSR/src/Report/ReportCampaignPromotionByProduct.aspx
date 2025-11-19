<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ReportCampaignPromotionByProduct.aspx.cs" Inherits="DOMS_TSR.src.Report.ReportCampaignPromotionByProduct" %>

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
                                        <div class="sub-title">รายงานการขายตาม Campaign&Promotionสินค้า </div>
                                    </div>

                                    <div class="card-body">

                                        <div id="searchSection_NoAnswerOrder" runat="server">

                                <%--            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label"></label>
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
                                            </div>--%>

                                         <%--   <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCustomerCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ชื่อลูกค้า</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchFName_NoAnswerOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchLName_NoAnswerOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสพนักงานขาย</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchSaleCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ชื่อพนักงานขาย</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchSaleFName_NoAnswerOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchSaleLName_NoAnswerOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>--%>
                                                <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสแคมเปญ</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCampaignCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblSearchCampaignCode_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ชื่อแคมเปญ</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchCampaignName_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                        <asp:Label ID="lblSearchCampaignName_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
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

                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchChannel_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">แบรนด์</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchCamCate_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>--%>
                                            <div class="form-group row">
                                                     <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">

                                                         <asp:TextBox ID="txtSearchOrderDateFrom_NoAnswerOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchOrderDateUntil_NoAnswerOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    </div>
                                                        <asp:Label ID="lblSearchOrderDate_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
                                                </div>
                                         <%--      <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchDeliverDate_NoAnswerOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchDeliverDate_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchDeliverDateTo_NoAnswerOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchDeliverDateTo_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>--%>

                                                <div class="col-sm-1"></div>

                                             <%--   <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchOrderstatus_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>--%>
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
                               
                                    <div id="GvData" runat="server" visible="false"  class="card-block p-t-5"  style="width: 100%;">

                                        <div id="Section_NoAnswerOrder" runat="server">

                                            <input type="hidden" id="hidIdList_NoAnswerOrder" runat="server" />

                                            <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_NoAnswerOrder" OnClick="btnAcceptOrder_NoAnswerOrder_Click" runat="server" Text="Export Excel" />

                                            <asp:Button CssClass="button-pri button-delete" ID="btnCancelOrder_NoAnswerOrder" runat="server" Text="ยกเลิกใบสั่งขาย"  OnClick="btnCancelOrder_NoAnswerOrder_Click" Visible="false" />

                                            <asp:Panel ID="Panel_NoAnswerOrder" runat="server" Style="overflow-x: scroll;">
                                                <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                                    <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right"  HeaderText="0" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="Center">Platform</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlatform" Text='Lazada' runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblfootPlatform" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>
                                                  <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Order Date</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderDate_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ORDER_DATE")%>' runat="server" />
                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Campaign Code</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCampaignCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_CODE")%>' runat="server" />
                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Campaign Name</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCampaignName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_NAME")%>' runat="server" />
                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Promotion Code</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPromotionCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PROMOTION_CODE")%>' runat="server" />
                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Promotion Name</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPromotionName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PROMOTION_NAME")%>' runat="server" />
                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                      <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Product Name</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PRODUCT_NAME")%>' runat="server" />
                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Amount</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT")%>' runat="server" />
                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Total Price</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotalPrice_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "PRICE")%>' runat="server" />
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
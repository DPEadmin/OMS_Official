<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ReportSaleOrder.aspx.cs" Inherits="DOMS_TSR.src.Report.ReportSaleOrder" %>

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
            <asp:HiddenField ID="hidMerCode" runat="server" />
            
                    <div class="page-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="card">
                                    <div class="card-header border-0">
                                        <div class="sub-title">Sales report by product</div>
                                    </div>

                                    <div class="card-body">

                                        <div id="searchSection_NoAnswerOrder" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchOrderCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">

                                                        <asp:TextBox ID="txtSearchOrderDateFrom_NoAnswerOrder" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchOrderDateUntil_NoAnswerOrder" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">Customer Code</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCustomerCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">Customer Name</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchFName_NoAnswerOrder" class="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchLName_NoAnswerOrder" class="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
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
                                                <label class="col-sm-2 col-form-label">Delivery Date</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchDeliverDate_NoAnswerOrder" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchDeliverDate_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchDeliverDateTo_NoAnswerOrder" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchDeliverDateTo_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">Sales Order Status</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchOrderstatus_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">Sale Channel</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchChannel_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">Brand</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchCamCate_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">Employee Code</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSaleCode" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">Employee Name</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="TxtFsalename" class="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtLsalename" class="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnSearch_NoAnswerOrder" Text="Search" OnClick="btnSearch_Click_NoAnswerOrder" class="button-pri button-accept m-r-10" runat="server" />
                                                <asp:Button ID="btnClearSearch_NoAnswerOrder" Text="Clear" OnClick="btnClearSearch_Click_NoAnswerOrder" class="button-pri button-cancel" runat="server" />
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
                                                <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="GvproductAmount_RowDataBound">
                                     <Columns>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"  HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

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
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="Center">MerchantName</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />

                                                </ItemTemplate>   
                                                <FooterTemplate>

                                                    <asp:Label ID="lblfootMerchantCode" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="Center">Product Code</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFProductCode" runat="server" Text="GrandTotal"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="LEFT" ItemStyle-HorizontalAlign="LEFT" ItemStyle-Width="40%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div>Product Name</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="LblProductname" Text='<%# DataBinder.Eval(Container.DataItem, "Productname")%>' runat="server" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"  HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="Center">Quanlity</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuanlity" Text='<%# string.Format("{0:0}",DataBinder.Eval(Container.DataItem, "Quanlity"))%>' runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFQuanlity" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="Center">Amount Item</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                 <%--   <asp:Label ID="lblAmount" Text='<%# string.Format("{0:0.00}",DataBinder.Eval(Container.DataItem, "Amount"))%>' runat="server" />--%>
                                                         <asp:Label ID="lblAmount" Text='<%# string.Format("{0:0.00}",DataBinder.Eval(Container.DataItem, "Amount"))%>' runat="server" />
                                                    <asp:HiddenField ID="HidAmount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                                       <asp:Label ID="LblAmonut1"  runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFAmount" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>
                                     


                                        </Columns>
                                             <EmptyDataTemplate>
                                        <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
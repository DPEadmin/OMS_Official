<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="ProgressOrderPLOL.aspx.cs" Inherits="DOMS_TSR.src.FullfillOrderlist.ProgressOrderPLOL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hd" runat="server" />
            <div class="page-body">

                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูล</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">วันที่</label>
                                    <div class="col-sm-3">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchStartDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateFrom" runat="server" TargetControlID="txtSearchStartDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>


                                        </div>


                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>


                                </div>



                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click"
                                        class="button-pri button-cancel"
                                        runat="server" />

                                </div>

                            </div>
                        </div>

                        <div class="page-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Basic Form Inputs card start -->
                                    <div class="card">
                                        <div class="card-block">



                                            <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gvOrder_RowCommand"
                                                TabIndex="0" Width="100%" CellSpacing="0"
                                                ShowHeaderWhenEmpty="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">วันออกรอบ</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblShipmentDate" Text='<%# ((null == Eval("shipmentdate"))||("" == Eval("shipmentdate"))) ? string.Empty : DateTime.Parse(Eval("shipmentdate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <Columns>
                                                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail" Visible="false">
                                                        <HeaderTemplate>
                                                            <div align="center">ออก OrdeList แล้ว</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSentedAmount" Text='<%# Eval("Sented").ToString()%>' runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail" Visible="false">
                                                        <HeaderTemplate>
                                                            <div align="center">ยังไม่ออก OrdeList</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNosentAmount" Text='<%# Eval("NotSent").ToString()%>' runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">จำนวน</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderTotalAmount" Text='<%# Eval("OrderTotalAmount").ToString()%>' runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>
                                                            <div align="center">Orderlist</div>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:HiddenField runat="server" ID="hidshipmentdate" Value='<%# DataBinder.Eval(Container.DataItem, "shipmentdate")%>' />
                                                            <asp:HiddenField runat="server" ID="hidconfirmno" Value='<%# DataBinder.Eval(Container.DataItem, "OrderTotalAmount")%>' />


                                                            <%--<asp:Button ID="EditCustmer" Text="Edit" runat="Server" CommandName="ShowCustomer" CssClass="Button" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>

                                                            <asp:ImageButton ID="ImgOL" ImageUrl="~/Image/product.png" runat="server" CommandName="ShowOL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                                            <br />
                                                            <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>
                                                            <div align="center">OrderdetailList(PDF)</div>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <%--                                    <asp:Button ID="EditCustmer" Text="Edit" runat="Server" CommandName="ShowCustomer" CssClass="Button" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                                            <asp:ImageButton ID="ImgPL" ImageUrl="~/Image/ordering.png" runat="server" CommandName="ShowPL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>
                                                            <div align="center">OrderdetailList(Excel)</div>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgExcel" ImageUrl="~/Image/ordering.png" runat="server" CommandName="ShowExcel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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





</asp:Content>

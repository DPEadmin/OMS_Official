<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="FFOrderTracking.aspx.cs" Inherits="DOMS_TSR.src.FullfillmentOrderManagement.FFOrderTracking" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
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


    <script type="text/javascript">
        function ApproveConfirmGV() {

            var gridApprove = document.getElementById("<%= gvOrder_NoAnswerOrder.ClientID %>");
             console.log("asdasdsadasd")
             var cell;
             var sum = 0;
            if (gridApprove.rows.length > 0) {
                 //alert("length=" + grid.rows.length);
                 //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < gridApprove.rows.length; i++) {
                     //get the reference of first column
                    cell = gridApprove.rows[i].cells[0];
                     // alert("cell=" + cell);
                     //alert("cell childNodes.length=" + cell.childNodes.length);
                     //loop according to the number of childNodes in the cell
                     for (j = 0; j < cell.childNodes.length; j++) {
                         //alert("type=" + cell.childNodes[j].type);
                         //alert("checked=" + cell.childNodes[j].checked);
                         //if childNode type is CheckBox
                         if (cell.childNodes[j].type == "checkbox") {
                             if (cell.childNodes[j].checked == true) {
                                 //assign the status of the Select All checkbox to the cell checkbox within the grid
                                 //cell.childNodes[j].checked = document.getElementById(id).checked;
                                 sum++;
                                 //alert("checked=" + cell.childNodes[j].checked);
                             }
                         }
                     }
                 }
             }


             if (sum == 0) {

                 alert("กรุณาเลือกรายการอนุมัติ");

                 return false;

             } else {

                 var MsgDelete = "คุณแน่ใจที่จะอนุมัติข้อมูลนี้ ?";

                 if (confirm(MsgDelete)) {
                     //alert("c");
                     document.getElementById("<%=hidFlagDel.ClientID%>").value = "TRUE";
                     alert("อนุมัติใบสั่งขายสำเร็จ");
                    return true;

                } else {

                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "FALSE";

                     return false;
                 }
             }
         }
        function DeleteConfirmGV() {

            var grid = document.getElementById("<%= gvOrder_NoAnswerOrder.ClientID %>");
            console.log("asdasdsadasd")
            var cell;
            var sum = 0;
            if (grid.rows.length > 0) {
                //alert("length=" + grid.rows.length);
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    // alert("cell=" + cell);
                    //alert("cell childNodes.length=" + cell.childNodes.length);
                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //alert("type=" + cell.childNodes[j].type);
                        //alert("checked=" + cell.childNodes[j].checked);
                        //if childNode type is CheckBox
                        if (cell.childNodes[j].type == "checkbox") {
                            if (cell.childNodes[j].checked == true) {
                                //assign the status of the Select All checkbox to the cell checkbox within the grid
                                //cell.childNodes[j].checked = document.getElementById(id).checked;
                                sum++;
                                //alert("checked=" + cell.childNodes[j].checked);
                            }
                        }
                    }
                }
            }


            if (sum == 0) {

                alert("กรุณาเลือกรายการที่จะยกเลิกใบสั่งขาย");

                return false;

            } else {

                var MsgDelete = "คุณแน่ใจที่จะยกเลิกใบสั่งขายนี้ ?";

                if (confirm(MsgDelete)) {
                    //alert("c");
                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "TRUE";

                    return true;

                } else {

                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "FALSE";

                    return false;
                }
            }
        }

    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hidBranchcode" runat="server" />
    <asp:HiddenField ID="hiddisplayname" runat="server" />
    <asp:HiddenField ID="hidordermsg" runat="server" />
      <asp:HiddenField ID="hidFlagDel" runat="server" />
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
                                        <div class="sub-title">Sales Order Search</div>
                                    </div>

                                    <div class="card-body">

                                        <div id="searchSection_NoAnswerOrder" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">sales order code</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchOrderCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblSearchOrderCode_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
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
                                                    <asp:Label ID="lblSearchOrderDate_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">Customer Code</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCustomerCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblSearchCustomerCode_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">Customer Name</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchFName_NoAnswerOrder" class="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchLName_NoAnswerOrder" class="form-control" placeholder="Lastname" runat="server"></asp:TextBox>
                                                    </div>
                                                        <asp:Label ID="txtSearchName_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">contact number</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchContact_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblSearchContact_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
                                                </div>

                                                <div class="col-sm-1"></div>

                                             <%--   <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchOrderType_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>--%>
                                                    <label class="col-sm-2 col-form-label">Brand</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchCamCate_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">delivery date</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchDeliverDate_NoAnswerOrder" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchDeliverDate_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchDeliverDateTo_NoAnswerOrder" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchDeliverDateTo_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    </div>
                                                        <asp:Label ID="lblSearchDeliverDate_NoAnswerOrder" runat="server" CssClass="validatecolor"></asp:Label>
                                                </div>

                                                <div class="col-sm-1"></div>

                                             <%--   <label class="col-sm-2 col-form-label">สาขา</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchBranch_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>--%>
                                            </div>

                                     <%--       <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchChannel_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1"></div>

                                            
                                            </div>--%>

                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnSearch_NoAnswerOrder" Text="Search" OnClick="btnSearch_Click_NoAnswerOrder" class="button-pri button-accept m-r-10" runat="server" />
                                                <asp:Button ID="btnClearSearch_NoAnswerOrder" Text="Clear" OnClick="btnClearSearch_Click_NoAnswerOrder" class="button-pri button-cancel" runat="server" />
                                            </div>

                                        </div>

                                    </div>

                                </div>
                                <div class="card ">
                                        <div class="col-5 m-t-10 m-b-10" >
                                <div class="card-group">

                                    <div class="card" id="cardex1" runat="server">
                                        <asp:LinkButton CssClass="btn-3bar-disable1" ID="showSection_NoAnswerOrder" OnClick="showSection_NoAnswerOrder_Click" runat="server">
                                            <div id="listcard1" runat="server">
                                                <div class="row">
                                                    <div class="col-3 text-left p-b-15">
                                                        <i class="fi fi-3x flaticon-warning text-c-blue"></i>
                                                    </div>
                                                    <div class="col-9 text-right">
                                                        <h3 class="text-c-blue">
                                                            <asp:Label ID="countSection_NoAnswerOrder" runat="server"></asp:Label></h3>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 text-left">
                                                        <p class=" m-0">Order wait approve</p>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                
                                    <div class="card-block p-t-5">

                                        <div id="Section_NoAnswerOrder" runat="server">

                                            <input type="hidden" id="hidIdList_NoAnswerOrder" runat="server" />

                                            <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_NoAnswerOrder" OnClick="btnAcceptOrder_NoAnswerOrder_Click" runat="server" Text="Approve sale order" Visible="false" OnClientClick="return ApproveConfirmGV();" />

                                            <asp:Button CssClass="button-pri button-delete" ID="btnCancelOrder_NoAnswerOrder"  runat="server" Text="Reject sale order"  OnClick="btnCancelOrder_NoAnswerOrder_Click" Visible="false" OnClientClick="return DeleteConfirmGV();" />
                                                 <asp:Button CssClass="button-pri button-delete" ID="Button1"  runat="server" Text="Back Order"  OnClick="btnBackOrder_NoAnswerOrder_Click" Visible="false" OnClientClick="return DeleteConfirmGV();" />
                                            <asp:Panel ID="Panel_NoAnswerOrder" runat="server" Style="overflow-x: scroll;">
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

                                <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" AutoGenerateColumns="False" CellSpacing="0" CssClass="table-p-stand" ShowHeaderWhenEmpty="true" TabIndex="0" Width="100%">
                                    <Columns>
                       <%--                 <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkAll_NoAnswerOrder" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_Change_NoAnswerOrder" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_NoAnswerOrder" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                    รหัสใบสั่งขาย</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "OrderCode"),DataBinder.Eval(Container.DataItem, "InventoryCode")) %>
                                                <asp:Label ID="lblOrderCode_NoAnswerOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' Visible="false" />
                                                <asp:HiddenField ID="hidOrderId_NoAnswerOrder" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />
                                                <asp:HiddenField ID="hidOrderCode_NoAnswerOrder" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                <asp:HiddenField ID="hidSaleOrderTypeName_NoAnswerOrder" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                <asp:HiddenField ID="hidCustomerName_NoAnswerOrder" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                <asp:HiddenField ID="hidCustomerContact_NoAnswerOrder" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                <asp:HiddenField ID="hidCreateDate_NoAnswerOrder" runat="server" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                    Sales Order Date</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                <asp:Label ID="lblOrderDate_NoAnswerOrder" runat="server" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                  Customer Name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName_NoAnswerOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                    contact number</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerContact_NoAnswerOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                    Inventory</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventory_NoAnswerOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InventoryName")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                    delivery date</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliverDate_NoAnswerOrder" runat="server" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                    Brand</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBrandName_NoAnswerOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" ItemStyle-Wrap="false" Visible="false">
                                            <HeaderTemplate>
                                                <div align="Center">
                                                    Back Order</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBackorder_NoAnswerOrder" runat="server" Text="0000" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail btn-action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-Wrap="false" Visible="false">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRequestReject_NoAnswerOrder" runat="Server" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"><span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                <br />
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <center>
                                            <asp:Label ID="lblDataEmpty_NoAnswerOrder" runat="server" class="fontBlack" Text="Data not Found"></asp:Label>
                                        </center>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                        </div>
                    </div>
                    </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
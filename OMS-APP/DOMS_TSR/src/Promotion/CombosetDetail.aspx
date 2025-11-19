<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="CombosetDetail.aspx.cs" Inherits="DOMS_TSR.src.Promotion.CombosetDetail" %>

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

        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvPromotion.ClientID %>");

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

            //  alert("sum=" + sum);

            if (sum == 0) {

                alert("กรุณาเลือกรายการที่จะลบ");

                return false;

            } else {

                //var MsgDelete = document.getElementById("<%=hidMsgDel.ClientID%>").value;
                var MsgDelete = "คุณแน่ใจที่จะลบข้อมูลนี้ ?";

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

        function DeleteConfirmSub() {

            var gridsub = document.getElementById("<%= GridMainSubexchang.ClientID %>");

            var cell;
            var sum = 0;
            if (gridsub.rows.length > 0) {
                //alert("length=" + grid.rows.length);
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < gridsub.rows.length; i++) {
                    //get the reference of first column
                    cell = gridsub.rows[i].cells[0];
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

            //  alert("sum=" + sum);

            if (sum == 0) {

                alert("กรุณาเลือกรายการที่จะลบ");

                return false;

            } else {


                var MsgDelete = "คุณแน่ใจที่จะลบข้อมูลนี้ ?";

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

        function AddPromotionDetailConfirm() {

            var grid = document.getElementById("<%= gvProduct.ClientID %>");

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

            //  alert("sum=" + sum);

            if (sum == 0) {

                alert("กรุณาเลือกรายการที่จะเพิ่ม");

                return false;

            } else {

                //var MsgDelete = document.getElementById("<%=hidMsgDel.ClientID%>").value;
                var MsgDelete = "คุณแน่ใจที่จะเพิ่มข้อมูลนี้ ?";

                if (confirm(MsgDelete)) {
                    //alert("c");
                    //document.getElementById("<%=hidFlagDel.ClientID%>").value = "TRUE";

                    return true;

                } else {

                    // document.getElementById("<%=hidFlagDel.ClientID%>").value = "FALSE";

                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="hidFlagInsert" runat="server" />
    <asp:HiddenField ID="hidFlagDel" runat="server" />
    <input type="hidden" id="hidaction" runat="server" />
    <asp:HiddenField ID="hidMsgDel" runat="server" />
    <asp:HiddenField ID="hidEmpCode" runat="server" />
      <asp:HiddenField ID="hidMainExchange" runat="server" />

    <!-- Page body start -->
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="page-body">
                <div class="row" >
                    <div class="col-sm-12">

                        <div class="card">
                            <div class="card-header">

                                <div class="sub-title ">
                                    Comboset Detail
                                </div>



                            </div>
                            <div class="card-block">
                                <div class="col-sm-12">
                                    <div class="row">
                                            <div class="col-lg-12 col-xl-6 col-sm-12">
                                                <table class="table-detail m-0" style="width:100%">
                                                <tbody>
                                                    <tr>
                                                        <th scope="row">รหัสคอมโบ</th>
                                                        <td>
                                                            <asp:Label runat="server" ID="txtCombosetCode"></asp:Label>
                                                        </td>
                                                        <asp:Label Visible="true" runat="server"
                                                            ID="txtPromotionDetailId"></asp:Label>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">ชื่อโปรโมชั่น</th>
                                                        <td>
                                                            <asp:Label runat="server" ID="txtPromotionName"></asp:Label>
                                                        </td>
                                                        <asp:Label Visible="false" runat="server" ID="txtPromotionCode">
                                                        </asp:Label>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">ชื่อคอมโบ</th>
                                                        <td>
                                                            <asp:Label runat="server" ID="txtCombosetName"></asp:Label>
                                                            <asp:Label Visible="false" runat="server"
                                                                ID="txtProductBrandCode"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <th scope="row">ราคา</th>
                                                        <td>
                                                            <asp:Label runat="server" ID="txtCombosetPrice"></asp:Label>

                                                        </td>

                                                    </tr>



                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="col-lg-12 col-xl-6 col-sm-12  " id="big_banner port_big_img " >
                                             
                                                <img class="img thumb-post" src="1" runat="server" id="ProductImg"  style="height: 100%; width: 100%; object-fit: contain" alt="">
                                           </div> 
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card" style="display:none;">
                            <div class="card-header">
                                <div class="sub-title">Search Product Exchange Management</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Product Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server">
                                        </asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="Hidden1" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <input type="hidden" id="Hidden2" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">Product Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server">
                                        </asp:TextBox>

                                    </div>
                                    <label class="col-sm-2 col-form-label">Merchant Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchMerchantCode" class="form-control" runat="server">
                                        </asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">Merchant Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchMerchantName" class="form-control" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"
                                        class="button-active button-submit m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click"
                                        class="button-active button-cancle" runat="server" />

                                </div>

                            </div>
                        </div>

                        <div class="page-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Basic Form Inputs card start -->
                                    <div class="card">
                                        <div class="card-block">
                                            <div>
                                                <div class="card-header">
                                                    <div class="sub-title">Main Exchange</div>
                                                </div>
                                            </div>
                                            <div class="m-b-10">
                                                <!--Start modal Add Promotion-->
                                                <asp:LinkButton ID="btnAddPromotion"
                                                   class="button-action button-add" data-backdrop="false"
                                                    OnClick="btnAddPromotion_Click" runat="server"><i
                                                        class="fa fa-plus"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click"
                                                    OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete " runat="server"><i
                                                        class="fa fa-minus"></i>ลบ</asp:LinkButton>
                                            </div>

                                            <div class="dt-responsive table-responsive">
                                                <asp:GridView ID="gvPromotion" runat="server" OnRowCommand="gvPromotion_RowCommand"
                                                     CssClass="table-p-stand " style="white-space:nowrap"
                                                    AutoGenerateColumns="False" TabIndex="0" Width="100%" CellSpacing="0"
                                                    ShowHeaderWhenEmpty="true">

                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                                                    <asp:CheckBox ID="chkMainProductAll"
                                                                        OnCheckedChanged="chkMainProductAll_Change"
                                                                        AutoPostBack="true" runat="server" />
                                                                </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkMainProduct" runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Product Code</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                       <%--         
                                                                <asp:Label ID="lblGvProductCode"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>'
                                                                    runat="server" />--%>

                                                                <%--   <%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>--%>
                                                                
                                                          <asp:LinkButton ID="btnAdd" runat="Server" CommandName="addSub" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>'
                                                          
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> </asp:LinkButton>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Product Name</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                              
                                                                <asp:Label ID="lblProductName"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            Visible="false" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead"
                                                            ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Price</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="lblPrice"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Quantity</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                              
                                                                <asp:Label ID="lblDefaultAmount"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'
                                                                    runat="server" />
                                                                <asp:HiddenField runat="server"
                                                                    ID="HidSubMainId"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "SubMainId")%>' />
                                                                <asp:HiddenField runat="server"
                                                                    ID="hidPromotionDetailId"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "SubMainId")%>' />
                                                                <asp:HiddenField runat="server" ID="hidProductCode"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                                <asp:HiddenField runat="server" ID="hidProductName"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidPrice"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                                <asp:HiddenField runat="server" ID="hidDefaultAmount"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                                <br />
                                                                <asp:Label ID="lblmsg" runat="server"
                                                                    CssClass="font12Red"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                          <asp:LinkButton ID="btnAdd" runat="Server" CommandName=""
                                                            class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                        <span class="icofont icofont-ui-add f-16"></span>
                                                        </asp:LinkButton>







                                                        </ItemTemplate>

                                                        </asp:TemplateField>--%>
                                                    </Columns>

                                                    <EmptyDataTemplate>
                                                        <center>
                                                            <asp:Label ID="lblDataEmpty" class="fontBlack"
                                                                runat="server" Text="Data not Found"></asp:Label>
                                                        </center>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                                <br />
                                                <br />
                                                <%-- PAGING CAMPAIGN --%>
                                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                                    <tr height="30" bgcolor="#ffffff">
                                                        <td width="100%" align="right" valign="top">
                                                            <table border="0" cellpadding="0" cellspacing="0"
                                                                style="vertical-align: middle;">
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
                                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button"
                                                                            ToolTip="First" CommandName="First"
                                                                            Text="<<" runat="server"
                                                                            OnCommand="GetPageIndex"></asp:Button>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td>
                                                                        <asp:Button ID="lnkbtnPre" CssClass="Button"
                                                                            ToolTip="Previous" CommandName="Previous"
                                                                            Text="<" runat="server"
                                                                            OnCommand="GetPageIndex"></asp:Button>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td style="font-size: 8.5pt;">Page
                                                                        <asp:DropDownList ID="ddlPage"
                                                                            CssClass="textbox" runat="server"
                                                                            AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        of
                                                                        <asp:Label ID="lblTotalPages"
                                                                            CssClass="fontBlack" runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td>
                                                                        <asp:Button ID="lnkbtnNext" CssClass="Button"
                                                                            ToolTip="Next" runat="server"
                                                                            CommandName="Next" Text=">"
                                                                            OnCommand="GetPageIndex"></asp:Button>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td>
                                                                        <asp:Button ID="lnkbtnLast" CssClass="Button"
                                                                            ToolTip="Last" runat="server"
                                                                            CommandName="Last" Text=">>"
                                                                            OnCommand="GetPageIndex"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="text-center m-t-20 col-sm-12 ">
                                                    <asp:Button runat="server" Text="ย้อนกลับ" OnClick="btnBack_Click"
                                                        class="button-active button-submit m-r-10 " Visible="false" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Basic Form Inputs card end -->
                            </div>
                        </div>
                        <!-- Basic Form Inputs card end -->
                        <div id="GVSubExchang" runat="server" visible="false">
                            <div class="page-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Basic Form Inputs card start -->
                                    <div class="card">
                                        <div class="card-block">
                                            <div>
                                                <div class="card-header">
                                                    <div class="sub-title">SuB Exchanng</div>
                                                    <asp:Label ID="LbMainId" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="m-b-10">
                                                <!--Start modal Add Promotion-->
                                                <asp:LinkButton ID="LinkButton1" class="button-action button-add"
                                                    OnClick="btnAddSubPromotion_Click" runat="server"><i
                                                        class="fa fa-plus"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" OnClick="btnDeleteSub_Click"    OnClientClick="return DeleteConfirmSub();"
                                                     class="button-action button-delete " runat="server"><i
                                                        class="fa fa-minus"></i>ลบ</asp:LinkButton>
                                            </div>

                                            <div class="dt-responsive table-responsive">
                                                <asp:GridView ID="GridMainSubexchang" runat="server"
                                                    AutoGenerateColumns="False" CssClass="table-p-stand " style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0"
                                                    ShowHeaderWhenEmpty="true">

                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                                                    <asp:CheckBox ID="chkSubProductAll"
                                                                        OnCheckedChanged="chkSubProductAll_Change"
                                                                        AutoPostBack="true" runat="server" />
                                                                </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkSubProduct" runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Product Code</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="lblProductCode"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>'
                                                                    runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Product Name</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                             
                                                                <asp:Label ID="lblProductName"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            Visible="false" ItemStyle-HorizontalAlign="right"
                                                            ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead"
                                                            ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Price</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="lblPrice"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Quantity</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="lblDefaultAmount"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'
                                                                    runat="server" />
                                                                <asp:HiddenField runat="server"
                                                                    ID="hidSubExchngPromotionDetailId"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "SubMainId")%>' />
                                                                <asp:HiddenField runat="server"
                                                                    ID="hidSubExchngProductCode"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                                <asp:HiddenField runat="server"
                                                                    ID="hidSubExchngProductName"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidSubExchngPrice"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                                <asp:HiddenField runat="server"
                                                                    ID="hidSubExchngDefaultAmount"
                                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                                <br />
                                                                <asp:Label ID="lblmsg" runat="server"
                                                                    CssClass="font12Red"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>



                                                    </Columns>

                                                    <EmptyDataTemplate>
                                                        <center>
                                                            <asp:Label ID="lblDataEmpty" class="fontBlack"
                                                                runat="server" Text="Data not Found"></asp:Label>
                                                        </center>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                                <br />
                                                <br />
                                                <%-- PAGING CAMPAIGN --%>
                                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                                    <tr height="30" bgcolor="#ffffff">
                                                        <td width="100%" align="right" valign="top">
                                                            <table border="0" cellpadding="0" cellspacing="0"
                                                                style="vertical-align: middle;">
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
                                                                        <asp:Button ID="btnSubGvPdFirst"
                                                                            CssClass="Button" ToolTip="First"
                                                                            CommandName="First" Text="<<" runat="server"
                                                                            OnCommand="GetPageIndex_GvSubPd">
                                                                        </asp:Button>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td>
                                                                        <asp:Button ID="btnSubGvPdPre" CssClass="Button"
                                                                            ToolTip="Previous" CommandName="Previous"
                                                                            Text="<" runat="server"
                                                                            OnCommand="GetPageIndex_GvSubPd">
                                                                        </asp:Button>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td style="font-size: 8.5pt;">Page
                                                                        <asp:DropDownList ID="ddlSubGvPdPage"
                                                                            CssClass="textbox" runat="server"
                                                                            AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlSubGvPdPage_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        of
                                                                        <asp:Label ID="lblTotalPages_GvSubPd"
                                                                            CssClass="fontBlack" runat="server">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td>
                                                                        <asp:Button ID="btnSubGvPdNext"
                                                                            CssClass="Button" ToolTip="Next"
                                                                            runat="server" CommandName="Next" Text=">"
                                                                            OnCommand="GetPageIndex_GvSubPd">
                                                                        </asp:Button>
                                                                    </td>
                                                                    <td style="width: 6px"></td>
                                                                    <td>
                                                                        <asp:Button ID="btnSubGvPdLast"
                                                                            CssClass="Button" ToolTip="Last"
                                                                            runat="server" CommandName="Last" Text=">>"
                                                                            OnCommand="GetPageIndex_GvSubPd">
                                                                        </asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="text-center m-t-20 col-sm-12 ">
                                                    <asp:Button runat="server" Text="ย้อนกลับ" OnClick="btnBack_Click"
                                                        class="button-active button-submit m-r-10 " />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Basic Form Inputs card end -->
                            </div>
                        </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <div class="modal fade bd-example-modal-lg" id="modal-Product" tabindex="-1" role="dialog"
        aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width: 1200px">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title sub-title " style="font-size: 18px;">Product </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">

                    <asp:UpdatePanel ID="UpModal" runat="server">
                        <ContentTemplate>

                            <div class="dt-responsive table-responsive">
                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False"  CssClass="table-p-stand " style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0"
                                    OnRowCommand="gvProduct_RowCommand" ShowHeaderWhenEmpty="true">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkProductAll"
                                                        OnCheckedChanged="chkProductAll_Change" AutoPostBack="true"
                                                        runat="server" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="chkProduct" runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="center">Product Code</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               
                                                <asp:Label ID="lblProductCode"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>'
                                                    runat="server" />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="center">Product Name</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblProductName"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                                    runat="server" />

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail" Visible="false">

                                            <HeaderTemplate>

                                                <div align="center">Price</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                
                                                <asp:TextBox ID="txtPrice_Ins" runat="server"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>'
                                                    TextMode="Number" class="form-control" />
                                                <asp:HiddenField runat="server" ID="hidProductPrice"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="center">Quantity</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                           
                                                <asp:TextBox ID="txtQty_Ins" runat="server" text="1" TextMode="Number" style="text-align:right" 
                                                    Value='<%#DataBinder.Eval(Container.DataItem, "Amount")%>'
                                                    class="form-control" />
                                                <%--<asp:HiddenField runat="server" ID="hidQty_Ins" Value='<%#txtQty_Ins.Text%>'
                                                />--%>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                            <HeaderTemplate>
                                            </HeaderTemplate>

                                            <ItemTemplate>

                                                <asp:LinkButton ID="btnEdit" runat="Server"
                                                    CommandName="AddPromotionDetail"
                                                    class="button-active button-submit m-r-10  "
                                                    Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span
                                                        class="icofont icofont-ui-add f-16"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidProductId"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                                <asp:HiddenField runat="server" ID="hidProductCode"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidProductName"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />






                                                <br />
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                    <EmptyDataTemplate>
                                        <center>
                                            <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server"
                                                Text="Data not Found"></asp:Label>
                                        </center>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <br />
                                <br />
                                <%-- PAGING CAMPAIGN --%>
                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0"
                                                style="vertical-align: middle;">
                                                <tr>
                                                    <td style="font-size: 8.5pt;">

                                                    </td>
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="btnMainPdSelFirst" CssClass="Button"
                                                            ToolTip="First" CommandName="First" Text="<<" runat="server"
                                                            OnCommand="GetPageIndex_MainPdModal"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnMainPdSelPre" CssClass="Button"
                                                            ToolTip="Previous" CommandName="Previous" Text="<"
                                                            runat="server" OnCommand="GetPageIndex_MainPdModal">
                                                        </asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                        <asp:DropDownList ID="ddlMainPdModalPage" CssClass="textbox"
                                                            runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlMainPdModalPage_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        of
                                                        <asp:Label ID="lblTotalPages_MainPdModal" CssClass="fontBlack"
                                                            runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnMainPdSelNext" CssClass="Button"
                                                            ToolTip="Next" runat="server" CommandName="Next" Text=">"
                                                            OnCommand="GetPageIndex_MainPdModal"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnMainPdSelLast" CssClass="Button"
                                                            ToolTip="Last" runat="server" CommandName="Last" Text=">>"
                                                            OnCommand="GetPageIndex_MainPdModal"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="btnSubmit" Text="สร้าง"
                            class="button-active button-submit m-r-10 " OnClick="btnSubmit_Click" Visible="false"
                            runat="server" />
                        <%--<asp:Button type="button" ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click" class="button-active button-cancle m-r-10" runat="server" />--%>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade bd-example-modal-lg" id="modal-SubProduct" tabindex="-1" role="dialog"
        aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width: 1200px">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title sub-title " style="font-size: 18px;">Exchang Product </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="dt-responsive table-responsive">
                                <asp:GridView ID="GridSubProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0"
                                    OnRowCommand="gvSubProduct_RowCommand" ShowHeaderWhenEmpty="true">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkProductAll"
                                                        OnCheckedChanged="chkProductAll_Change" AutoPostBack="true"
                                                        runat="server" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="chkProduct" runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="center">Product Code</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="center">Product Name</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblProductName"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                                    runat="server" />

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="false"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="center">Price</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                              
                                                <asp:TextBox ID="txtPrice_Ins" runat="server"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>'
                                                    TextMode="Number" class="form-control" />
                                                <asp:HiddenField runat="server" ID="hidProductPrifce"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="center">Quantity</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                               
                                                <asp:TextBox ID="txtQty_Ins" runat="server" text="1" style="text-align:right" TextMode="Number"
                                                    Value='<%#DataBinder.Eval(Container.DataItem, "Amount")%>'
                                                    class="form-control" />
                                                <%--<asp:HiddenField runat="server" ID="hidQty_Ins" Value='<%#txtQty_Ins.Text%>'
                                                />--%>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px"
                                            HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                            <HeaderTemplate>
                                            </HeaderTemplate>

                                            <ItemTemplate>

                                                <asp:LinkButton ID="btnEdit" runat="Server"
                                                    CommandName="AddPromotionDetail"
                                                    class="button-active button-submit m-r-10  "
                                                    Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span
                                                        class="icofont icofont-ui-add f-16"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidSubProductId"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                                <asp:HiddenField runat="server" ID="hidSubProductCode"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidSubProductName"
                                                    Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />






                                                <br />
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                    <EmptyDataTemplate>
                                        <center>
                                            <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server"
                                                Text="Data not Found"></asp:Label>
                                        </center>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <br />
                                <br />
                                <%-- PAGING CAMPAIGN --%>
                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0"
                                                style="vertical-align: middle;">
                                                <tr>
                                                    <td style="font-size: 8.5pt;">

                                                    </td>
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSubPdSelFirst" CssClass="Button"
                                                            ToolTip="First" CommandName="First" Text="<<" runat="server"
                                                            OnCommand="GetPageIndex_SubPdModal"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSubPdSelPre" CssClass="Button"
                                                            ToolTip="Previous" CommandName="Previous" Text="<"
                                                            runat="server" OnCommand="GetPageIndex_SubPdModal">
                                                        </asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                        <asp:DropDownList ID="ddlSubPdModalPage" CssClass="textbox"
                                                            runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlSubPdSelPage_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        of
                                                        <asp:Label ID="lblTotalPages_SubPdModal" CssClass="fontBlack"
                                                            runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSubPdSelNext" CssClass="Button"
                                                            ToolTip="Next" runat="server" CommandName="Next" Text=">"
                                                            OnCommand="GetPageIndex_SubPdModal"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSubPdSelLast" CssClass="Button"
                                                            ToolTip="Last" runat="server" CommandName="Last" Text=">>"
                                                            OnCommand="GetPageIndex_SubPdModal"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="Button9" Text="สร้าง" class="button-active button-submit m-r-10 "
                            OnClick="btnSubmit_Click" Visible="false" runat="server" />
                        <%--<asp:Button type="button" ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click" class="button-active button-cancle m-r-10" runat="server" />--%>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
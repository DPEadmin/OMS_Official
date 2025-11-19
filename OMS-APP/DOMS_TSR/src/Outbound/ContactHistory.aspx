<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ContactHistory.aspx.cs" Inherits="DOMS_TSR.src.Outbound.ContactHistory" %>

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

                alert("กรุณาเลือกรายการที่จะไม่อนุมัติใบสั่งขายนี้");

                return false;

            } else {

                var MsgDelete = "คุณแน่ใจที่จะไม่อนุมัติใบสั่งขายนี้ ?";

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
        function BackOrderConfirmGV() {

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

                alert("กรุณาเลือกรายการที่จะตี Back Order");

                return false;

            } else {

                var MsgDelete = "คุณแน่ใจที่จะตี Back Orderใบสั่งขายนี้ ?";

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
                                <div class="sub-title">search for information Contact History</div>
                            </div>

                            <div class="card-block">

                                <div id="searchSection_NoAnswerOrder" runat="server">
                                    <div class="form-group row">
                                        <%--<label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>--%>
                                        <label class="col-sm-2 col-form-label">Name</label>
                                        <div class="col-sm-4">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_NoAnswerOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_NoAnswerOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchName" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%--<label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>--%>
                                        <label class="col-sm-2 col-form-label">contact number</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSearchContact_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtSearchTelephone" class="form-control" runat="server"></asp:TextBox>
                                        </div>





                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Import Date</label>
                                        <div class="col-sm-4">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchCreateDateFrom" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchCreateDateFrom" runat="server" TargetControlID="txtSearchCreateDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                <asp:TextBox ID="txtSearchCreateDateTo" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchCreateDateTo" runat="server" TargetControlID="txtSearchCreateDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>


                                        </div>
                                        <label class="col-sm-2 col-form-label">Status</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchLeadStatus" runat="server" class="form-control"></asp:DropDownList>
                                        </div>




                                    </div>



                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_NoAnswerOrder" Text="Search" OnClick="btnSearch_Click_NoAnswerOrder" class="button-pri button-accept m-r-10" runat="server" />
                                        <asp:Button ID="btnClearSearch_NoAnswerOrder" Text="clear" OnClick="btnClearSearch_Click_NoAnswerOrder" class="button-pri button-cancel" runat="server" />
                                    </div>

                                </div>

                            </div>

                        </div>
                        <div class="card ">
                            <div class="col-2 m-t-10 m-b-10">
                                <div class="card-group">

                                    <div class="card" id="cardex1" runat="server">
                                        <asp:LinkButton CssClass="btn-3bar-disable1" ID="showSection_NoAnswerOrder" OnClick="showSection_NoAnswerOrder_Click" runat="server">
                                            <div id="listcard1" runat="server">
                                                <div class="row">
                                                    <div class="col-3 text-left p-b-15">
                                                        <i class="fi-3x far fa-file-alt text-c-blue"></i>
                                                    </div>
                                                    <div class="col-9 text-right">
                                                        <h3 class="text-c-blue">
                                                            <asp:Label ID="countSection_NoAnswerOrder" runat="server"></asp:Label></h3>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 text-left">
                                                        <p class=" m-0">Order Lead </p>
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

                                    <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_NoAnswerOrder" OnClick="btnAcceptOrder_NoAnswerOrder_Click" runat="server" Text="sale order Approve" OnClientClick="return ApproveConfirmGV();" Visible="false" />

                                    <asp:Button CssClass="button-pri button-delete" ID="btnCancelOrder_NoAnswerOrder" runat="server" Text="sale order reject" OnClick="btnCancelOrder_NoAnswerOrder_Click" OnClientClick="return DeleteConfirmGV();" Visible="false" />
                                    <asp:Button CssClass="button-pri button-delete" ID="Button1" runat="server" Text="Back Order" OnClick="btnBackOrder_NoAnswerOrder_Click" OnClientClick="return BackOrderConfirmGV();" Visible="false" />
                                    <asp:Panel ID="Panel_NoAnswerOrder" runat="server" Style="overflow-x: scroll;">


                                        <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" OnRowDataBound="gvOrder_RowDatabound" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            OnRowCommand="gvOrder_NoAnswerOrder_RowCommand" OnRowCreated="gvOrder_NoAnswerOrder_RowCreated">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                                            <asp:CheckBox ID="chkAll_NoAnswerOrder" OnCheckedChanged="chkAll_Change_NoAnswerOrder" AutoPostBack="true" runat="server" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_NoAnswerOrder" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ClickToCall"
                                                            class="button-activity   "
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont fa-phone icofont-phone  "></span></asp:LinkButton>
                                                        <asp:HiddenField runat="server" ID="hidLeadID" Value='<%# DataBinder.Eval(Container.DataItem, "LeadID")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCAMPAIGN_ID" Value='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_ID")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMEDIA_PHONE" Value='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' />

                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Import Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblCreateDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Customer Id</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomercode" Text='<%# DataBinder.Eval(Container.DataItem, "Customercode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFULL_NAME_TH" Text='<%# DataBinder.Eval(Container.DataItem, "FULL_NAME_TH")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Status</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "LeadStatusname")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                      <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="left">contact matter</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCusReason" Text='<%# DataBinder.Eval(Container.DataItem, "CusReason")%>' runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Telephone No.1</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_1")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Telephone No.2</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_2")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Telephone No.3</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_3")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Telephone No.4</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_4")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Telephone No.5</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_5")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Telephone No.6</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_6")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Tel Home 1</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "PHONE_1")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Tel Home 2</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "PHONE_2")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Tel Home 3</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "PHONE_3")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Employee Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPREVIOUS_SALE_NAME" Text='<%# DataBinder.Eval(Container.DataItem, "PREVIOUS_SALE_NAME")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">previously purchased item</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPREVIOUS_PRODUCT" Text='<%# DataBinder.Eval(Container.DataItem, "PREVIOUS_PRODUCT")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPREVIOUS_ORDER_BRAND" Text='<%# DataBinder.Eval(Container.DataItem, "PREVIOUS_ORDER_BRAND")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                       
                                              
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Promotion Name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                                        <asp:HiddenField runat="server" ID="hidCustomerCode" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidEmail" Value='<%# DataBinder.Eval(Container.DataItem, "Email")%>' />
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
                                    <div class="m-t-10">
                                        <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                            <tr height="30" bgcolor="#ffffff">
                                                <td width="100%" align="right" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="lnkbtnFirst_NoAnswerOrder" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                                    Text="<<" runat="server" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                            </td>
                                                            <td style="width: 6px"></td>
                                                            <td>
                                                                <asp:Button ID="lnkbtnPre_NoAnswerOrder" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
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
                                                                <asp:Button ID="lnkbtnNext_NoAnswerOrder" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                            </td>
                                                            <td style="width: 6px"></td>
                                                            <td>
                                                                <asp:Button ID="lnkbtnLast_NoAnswerOrder" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
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

</asp:Content>

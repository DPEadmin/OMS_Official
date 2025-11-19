<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ContactHistory_1.aspx.cs" Inherits="DOMS_TSR.src.Outbound.ContactHistory_1" %>

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
    <asp:HiddenField ID="hidLeadCodeforUpdate" runat="server" />
    <asp:HiddenField ID="hidLeadIdforUpdate" runat="server" />
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
                                <div class="sub-title">search for information </div>
                            </div>

                            <div class="card-block">

                                <div id="searchSection_NoAnswerOrder" runat="server">
                                    <div class="form-group row">
                                        <%--<label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>--%>
                                        <label class="col-sm-2 col-form-label">Name</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_NoAnswerOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_NoAnswerOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchName" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%--<label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>--%>
                                        <label class="col-sm-2 col-form-label">contact number</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtSearchTelephone" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">

                                        <label class="col-sm-2 col-form-label">Import Date</label>

                                        <div class="col-sm-3">
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



                                        <div class="text-center m-t-20 col-sm-12">
                                            <asp:Button ID="btnSearch_NoAnswerOrder" Text="Search" OnClick="btnSearch_Click_NoAnswerOrder" class="button-pri button-accept m-r-10" runat="server" />
                                            <asp:Button ID="btnClearSearch_NoAnswerOrder" Text="Clear" OnClick="btnClearSearch_Click_NoAnswerOrder" class="button-pri button-cancel" runat="server" />
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

                                    <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_NoAnswerOrder" OnClick="btnAcceptOrder_NoAnswerOrder_Click" runat="server" Text="Approve sale order" OnClientClick="return ApproveConfirmGV();" Visible="false" />

                                    <asp:Button CssClass="button-pri button-delete" ID="btnCancelOrder_NoAnswerOrder" runat="server" Text="ไม่อนุมิใบสั่งขาย" OnClick="btnCancelOrder_NoAnswerOrder_Click" OnClientClick="return DeleteConfirmGV();" Visible="false" />
                                    <asp:Button CssClass="button-pri button-delete" ID="Button1" runat="server" Text="Back Order" OnClick="btnBackOrder_NoAnswerOrder_Click" OnClientClick="return BackOrderConfirmGV();" Visible="false" />
                                    <asp:Panel ID="Panel_NoAnswerOrder" runat="server" Style="overflow-x: scroll;">


                                        <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" OnRowDataBound="gvOrder_RowDatabound" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            OnRowCommand="gvOrder_NoAnswerOrder_RowCommand" >

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
                                                   <%--     <asp:HiddenField runat="server" ID="hidCAMPAIGN_ID" Value='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_ID")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMEDIA_PHONE" Value='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' />--%>

                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton runat="Server" CommandName="SaveContactHistory" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "LeadCode")%>'></asp:LinkButton>
                                                        <asp:HiddenField runat="server" ID="hidLeadCode" Value='<%# DataBinder.Eval(Container.DataItem, "LeadCode")%>' />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                    
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">data import date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblCreateDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

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
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Contact 1</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "mobile_1")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Contact 2</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "mobile_2")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Contact 3</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "mobile_3")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Contact 4</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "mobile_4")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Contact 5</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "mobile_5")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Contact 6</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "mobile_6")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Tel home 1</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "phone_1")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Tel home 2</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "phone_2")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                        <div align="Center">Tel home 3</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="Server" CommandName="ClickToCall" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "phone_3")%>'></asp:LinkButton>
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
                                                        <div align="Center">previously purchased items</div>
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
                                                        <div align="left">Status</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "LeadStatusName")%>' runat="server" />
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

    <div class="modal fade" id="modal-Promotion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 60%">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2    ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">Contact History</div>

                            </div>
                            <span>
                                <button type="button" class="close  " style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="modal-body ">

                    <asp:UpdatePanel ID="UpModal" runat="server">
                        <ContentTemplate>
                            <div class="form-group row">

                                <label class="col-sm-2 col-form-label">Lead Number</label>
                                <div class="col-sm-4 form-controls  ">

                                    <asp:Label ID="lblLeadCode" runat="server" class="form-control"></asp:Label>
                                    <asp:HiddenField ID="hidleadIDfroloadPopup" runat="server" />
                                    <asp:HiddenField ID="hidLeadTelephonePopUp" runat="server" />
                                </div>
                                <label class="col-sm-2 col-form-label"></label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:LinkButton ID="LinkgotoTakeOrder" CssClass="btn_takeorder" OnClick="LinkgotoTakeOrder_Clicked" runat="server"> <i class="fas fa-shopping-cart m-r-10"></i>Take Order</asp:LinkButton>
                                </div>

                                <label class="col-sm-2 col-form-label">data import date</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtUpdateDate" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 col-form-label">Status</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:DropDownList ID="ddlStatus" class="form-control" name="select" runat="server">
                                        <asp:ListItem Value="-99">---- Please select ----</asp:ListItem>
                                        <asp:ListItem Value="Open">Open</asp:ListItem>
                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label class="col-sm-2 col-form-label">callback date</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtRecontactbackDate" class="form-control" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carRecontactbackDate" runat="server" TargetControlID="txtRecontactbackDate" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                </div>
                                <label class="col-sm-2 col-form-label">Contact time</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:DropDownList ID="ddlRecontactbactPeriodTime" class="form-control" name="select" runat="server">
                                        <asp:ListItem Value="-99">---- Please select ----</asp:ListItem>
                                        <asp:ListItem Value="01">เช้า</asp:ListItem>
                                        <asp:ListItem Value="02">กลางวัน</asp:ListItem>
                                        <asp:ListItem Value="03">เย็น</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label class="col-sm-2 col-form-label">contact matter</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtCusreason" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 col-form-label">Contact details(Other)</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtCusReasonOther" class="form-control" runat="server"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 col-form-label">Name</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 col-form-label">Contact</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtTelephone" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 col-form-label">Insurance type</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtInsuranceType" class="form-control" runat="server"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 col-form-label">car year</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtCarYear" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 col-form-label">car model</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtCarType" class="form-control" runat="server"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 col-form-label">Car</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtCarModel" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 col-form-label">Sub version</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtCarsubmodel" class="form-control" runat="server"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 col-form-label">out of warranty</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtInsurancedate" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 col-form-label">contact status</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:DropDownList ID="ddlContact_Status" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlContact_Status_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                </div>

                                <label class="col-sm-2 col-form-label">reason</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:DropDownList ID="ddlOrderSituation" runat="server" CssClass="form-control" class="form-control"></asp:DropDownList>
                                </div>

                                <label class="col-sm-2 col-form-label">type of contact</label>
                                <div class="col-sm-4 form-controls  ">
                                    <asp:TextBox ID="txtTransactionTypeCode" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 col-form-label"></label>
                                <div class="col-sm-4 form-controls  ">
                                </div>

                            </div>

                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Comment</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCallCommendHistory" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">
                                    SO Number
                                </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtSONumber" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="btnSubmit" Text="Submit" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Clicked" runat="server" />
                        <asp:Button type="button" ID="btnCancel" Text="Cancle" class="button-pri button-cancel m-r-10" runat="server" />
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>



<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="TestPrint.aspx.cs" Inherits="DOMS_TSR.src.TestCode.TestPrint" %>

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

        function CallPrint(var strid) {
            var prtContent = document.getElementById(strid);
        var WinPrint = window.open('', '', 'letf=10,top=10,width="450",height="250",toolbar=1,scrollbars=1,status=0');

        //WinPrint.document.write("<html><head><LINK rel=\"stylesheet\" type\"text/css\" href=\"css/print.css\" media=\"print\"><LINK rel=\"stylesheet\" type\"text/css\" href=\"css/print.css\" media=\"screen\"></head><body>");

        WinPrint.document.write(prtContent.innerHTML);
        WinPrint.document.write("</body></html>");
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();

        return false;
        }
        function ClickHereToPrint() {
            try {
                var oIframe = document.getElementById('ifrmPrint');
                var oContent = document.getElementById('divToPrint').innerHTML;
                var oDoc = (oIframe.contentWindow || oIframe.contentDocument);
                if (oDoc.document) oDoc = oDoc.document;
                oDoc.write("&lt;head><title>title&lt;/title>");
                oDoc.write("<body onload='this.focus(); this.print();'>");
                oDoc.write(oContent + "</body>");
                oDoc.close();
            }
            catch (e) {
                self.print();
            }
        }
        function printMe() {
            Var oWord;
            oWord = new ActivexObject("Word.Application");
            oWord.Visible = false; //hide from user
            oWord.Documents.Open(doc);
            oWord.Documents.Item(1).PrintOut(false);
            oWord.Quit();
        }
    </script>
</asp:Content>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" Visible="false">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="hidFlagInsert" runat="server" />
    <asp:HiddenField ID="hidFlagDel" runat="server" />
    <input type="hidden" id="hidaction" runat="server" />
    <asp:HiddenField ID="hidMsgDel" runat="server" />
    <asp:HiddenField ID="hidEmpCode" runat="server" />
    <asp:HiddenField ID="hidDiscountPercent" runat="server" />
    <asp:HiddenField ID="hidDiscountAmount" runat="server" />
    <%--<asp:HiddenField ID="hidPromotionDetailName" runat="server" />--%>
    <asp:HiddenField ID="hidProductBrandCode" runat="server" />
    <style>
        .aspNetDisabled {
            width: 100%;
        }
    </style>
    <style media="print">
        .noPrint {
            display: none;
        }

        .yesPrint {
            display: block !important;
        }
    </style>
    <!-- Page body start -->

    <div class="page-body">
        <div class="row">
            <div class="col-sm-6" style="margin: 0 auto;" id="receiptOrder" runat="server">
                <div class="row yesPrint">
                     <div class="sub-title " style="text-align:center;">
                            <asp:Label ID="lblOrderID" runat="server" Text="S000000000024"></asp:Label>
                        </div>
                    <table class="table-info">
                        <tbody>
                            <tr><td>1</td></tr>
                            <tr><td>2</td></tr>
                            <tr><td>3</td></tr>

                        </tbody>
                    </table>
                  
                </div>
            </div>
        </div>
        <div class="row">
            <div class="text-center m-t-20 col-sm-12">

                            
                           <asp:Button ID="btnSavePDF" runat="server" OnClick="btnSavePDF_Click"  
                               class="button-pri button-accept m-r-10" Text="บันทึก" />

                            </div>
        </div>
    </div>

    <div class="page-body noPrint" style="display:none;">
        <div class="row">
            <div class="col-sm-12">

                <div class="card">
                    <div class="card-header">

                        <div class="sub-title ">
                            รายละเอียดโปรโมชั่น
                        </div>



                    </div>
                    <div class="card-block">
                        <div class="col-sm-12">

                            <div class="row yesPrint">


                                <div class="col-lg-12 col-xl-6 col-sm-12">
                                    <table class="table-detail m-0" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <th scope="row">รหัสโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionCode"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">ชื่อโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionName"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">ประเภทโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionTypeName">
                                                    </asp:Label>
                                                    <asp:Label Visible="false" ID="hidFlagComboset"
                                                        runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label Visible="false" ID="hidPromotionTypeCode"
                                                        runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">สถานะ</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionStatusName">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">วันเริ่มโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtStartDate">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">วันสิ้นสุดโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtEndDate">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="display: none;">
                                                <th scope="row">ฟรีค่าขนส่ง</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtFreeShippingFlag">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">จับกลุ่ม</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtLockCheckbox">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">แก้ไขจำนวนสินค้า</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtLockAmountFlag">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">ส่วนลด</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtDiscount">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">รายละเอียด
                                                </th>
                                                <td style="border-top: 0px;">
                                                    <textarea rows="4" runat="server" id="txtDescription"
                                                        cols="100" class="form-control" disabled
                                                        style="resize: none; overflow: hidden;"></textarea>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="col-lg-12 col-xl-6 col-sm-12  " id="big_banner port_big_img ">

                                    <img class="img thumb-post" src="1" runat="server" id="ProductImg" style="height: 100%; width: 100%; object-fit: contain" alt="">
                                </div>


                            </div>
                            <div class="text-center m-t-20 col-sm-12">

                                <asp:button id="btnPrint" text="พิมพ์"
                                    OnClick="btnPrint_Click"
                                    class="button-pri button-accept m-r-10"
                                    runat="server" xmlns:asp="#unknown" />
                                <asp:button id="Button1" text="บันทึก"
                                    OnClientClick="printMe();"
                                    class="button-pri button-accept m-r-10"
                                    runat="server" xmlns:asp="#unknown" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="mainPanel" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาโปรโมชั่น</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="Hidden1" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <input type="hidden" id="Hidden2" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchChannelCode" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <%--    <label class="col-sm-1 col-form-label"></label>
                            <label class="col-sm-2 col-form-label">Merchant Name</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
                            </div>--%>
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

                                            <div class="m-b-10">
                                                <!--Start modal Add Promotion-->
                                                <asp:LinkButton ID="btnAddPromotion" class="button-action button-add m-r-10"
                                                    OnClick="btnAddPromotion_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnAddCombo" class="button-action button-add m-r-10"
                                                    OnClick="btnAddCombo_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>


                                            <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                OnRowCreated="gvPromotion_RowCreated"
                                                TabIndex="0" Width="100%" CellSpacing="0"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                            <asp:CheckBox ID="chkPromotionDetailAll" OnCheckedChanged="chkPromotionDetailAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkPromotion" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Comboset Code</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblComboCode" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetCode") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Comboset Name</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCombosetName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Product Code</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProdCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Product Name</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Unit</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Price</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Quantity</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDefaultAmount" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' runat="server" />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDetailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDeailInfoId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDefaultAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                                            <br />
                                                            <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Channel</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                          <asp:LinkButton ID="btnAdd" runat="Server" CommandName=""
                                                            class="button-activity"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-16"></span></asp:LinkButton>







                                                    </ItemTemplate>

                                                </asp:TemplateField>--%>
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
                                            <div class="text-center m-t-20 col-sm-12 ">
                                                <asp:Button runat="server" Text="ย้อนกลับ" OnClick="btnBack_Click" class="button-pri button-accept " />

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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <div class="modal fade bd-example-modal-lg" id="modal-Product" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width: 1200px">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpModal" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="modal-header modal-header2  p-l-0 ">
                                    <div class="col-sm-12">
                                        <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เลือกสินค้า</div>

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
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchModalProductCode" class="form-control" runat="server"></asp:TextBox>


                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchModalProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchModalChannel" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <%--    <label class="col-sm-1 col-form-label"></label>
                            <label class="col-sm-2 col-form-label">Merchant Name</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
                            </div>--%>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnModalSearch" Text="ค้นหา" OnClick="btnModalSearch_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnModalClear" Text="ล้าง" OnClick="btnModalClear_Click"
                                        class="button-pri button-cancel"
                                        runat="server" />

                                </div>

                            </div>

                            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                OnRowDataBound="gvProduct_RowDataBound" OnRowCreated="gvProduct_RowCreated"
                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProduct_RowCommand"
                                ShowHeaderWhenEmpty="true">

                                <Columns>
                                    <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                            <asp:CheckBox ID="chkProductAll" OnCheckedChanged="chkProductAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkProduct" runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>



                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="left">Product Code</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductCodeChoose" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="left">Product Name</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="left">Unit</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProduUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="left">Price</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="Label2" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="left">
                                                <asp:Label runat="server" ID="lblHeaderPrice" Text="Price" />
                                            </div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrice_Ins" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' TextMode="Number" class="form-control" Enabled='<%# hidPromotionTypeCode.Text == "02" || hidPromotionTypeCode.Text == "11" %>' Style="text-align: right" />
                                            <asp:HiddenField runat="server" ID="hidProductPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="left">Quantity</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty_Ins" runat="server" TextMode="Number" Value='<%#DataBinder.Eval(Container.DataItem, "Qty")%>' class="form-control" Style="text-align: right" />
                                            <%--<asp:HiddenField runat="server" ID="hidQty_Ins" Value='<%#txtQty_Ins.Text%>' />--%>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="left">Channel</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                        <HeaderTemplate>
                                        </HeaderTemplate>

                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddPromotionDetail"
                                                class="button-activity "
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-14"></span></asp:LinkButton>

                                            <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                            <asp:HiddenField runat="server" ID="HiddenField4" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                            <asp:HiddenField runat="server" ID="HiddenField5" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                            <asp:HiddenField runat="server" ID="hidPromotionDetailId" />






                                            <br />
                                            <asp:Label ID="Label3" runat="server" CssClass="font12Red"></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                                <EmptyDataTemplate>
                                    <center>
                                    <asp:Label ID="Label4" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                    <asp:Button ID="btnPdFirst" CssClass="Button" ToolTip="First" CommandName="First"
                                                        Text="<<" runat="server" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdPre" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                        Text="<" runat="server" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPdPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlProductPage_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                    of
                                                                                    <asp:Label ID="lblTotalPdPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                        Text=">>" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>




                            <div class="text-center m-t-20 col-sm-12">
                                <%--<asp:Button type="button" ID="btnSubmit" Text="สร้าง" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Click" runat="server" />--%>
                                <%--<asp:Button type="button" ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click" class="button-pri button-cancel" runat="server" />--%>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-Comboset" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 650px">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">คอมโบ</div>

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

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">รหัสคอมโบ</label>
                                <div class="col-sm-6">

                                    <asp:TextBox ID="txtCombosetCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblCombosetCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:HiddenField ID="hidPromotionCode_Ins" runat="server"></asp:HiddenField>
                                    <asp:HiddenField runat="server" ID="hidFlagInsertCombo" />

                                </div>
                                <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">ชื่อคอมโบ</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCombosetName_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="hidCombosetName_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <%-- <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">ระดับโปรโมชั่น</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPromotion_Ins" runat="server" class="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblPromotionLevel_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>--%>


                                <label class="col-sm-4 col-form-label">ราคา</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCombosetPrice_Ins" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblCombosetPrice_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>


                            </div>
                            <style>
                                .aspNetDisabled {
                                    font-size: 14px;
                                    padding: 10px 15px;
                                    height: 100%;
                                    width: 100%;
                                }
                            </style>



                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="btnSubmitCombo" Text="สร้าง" class="button-pri button-accept m-r-10 " OnClick="btnSubmitCombo_Click" runat="server" />
                        <asp:Button type="button" ID="btnCancelCombo" Text="ล้าง" OnClick="btnCancelCombo_Click" class="button-pri button-cancel " runat="server" />
                    </div>
                </div>

            </div>
        </div>
    </div>


</asp:Content>


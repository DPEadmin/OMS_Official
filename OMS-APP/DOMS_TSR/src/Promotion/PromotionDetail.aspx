<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="PromotionDetail.aspx.cs" Inherits="DOMS_TSR.src.Promotion.PromotionDetail" %>

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

        function check(e, value) {
            //Check Charater
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (value.indexOf(".") != -1) if (unicode == 46) return false;
            if (unicode != 8) if ((unicode < 48 || unicode > 57) && unicode != 46) return false;
        }
        function checkLength() {
            var fieldLength = document.getElementsByClassName('.txtF').value.length;
            //Suppose u want 4 number of character
            if (fieldLength <= 4) {
                return true;
            }
            else {
                var str = document.getElementsByClassName('.txtF').value;
                str = str.substring(0, str.length - 1);
                document.getElementById('txtF').value = str;
            }
        }

        var submit = 0;
        function CheckDouble() {
            if (++submit > 1) {
                alert('ระบบกำลังทำงาน กรุณารอสักครู่');
                return false;
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
    <asp:HiddenField ID="hidPromotionId" runat="server" />
    <asp:HiddenField ID="hidMerchantCode" runat="server" />
    <asp:HiddenField ID="hidDiscountPercent" runat="server" />
    <asp:HiddenField ID="hidDiscountAmount" runat="server" />
    <asp:HiddenField ID="hidProductDiscountPercent" runat="server" />
    <asp:HiddenField ID="hidProductDiscountAmount" runat="server" />
    <asp:HiddenField ID="hidPromotionType" runat="server" />
    <asp:HiddenField ID="hidApplyScope" runat="server" />
    <asp:HiddenField ID="hidCriteriaType" runat="server" />
    <asp:HiddenField ID="hidDiscountType" runat="server" />
    <%--<asp:HiddenField ID="hidPromotionDetailName" runat="server" />--%>
    <asp:HiddenField ID="hidProductBrandCode" runat="server" />
    <asp:HiddenField ID="hidQTYInsert" runat="server" />
    <asp:HiddenField ID="hidQuotaOnhandInsert" runat="server" />
    <asp:HiddenField ID="hidProductTagCodeCheck" runat="server" />
    <style>
        .aspNetDisabled {
            width: 100%;
        }
    </style>
    <!-- Page body start -->
    <div class="page-body">
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

                            <div class="row">


                                <div class="col-lg-12 col-xl-6 col-sm-12">
                                    <table class="table-detail m-0" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <th scope="row" style="width: 40%;">รหัสโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionCode"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row" style="width: 40%;">ชื่อโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionName"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row" style="width: 40%;">ประเภทโปรโมชั่น</th>
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
                                                <th scope="row" style="width: 40%;">สถานะ</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionStatusName">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row" style="width: 40%;">วันเริ่มโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtStartDate">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row" style="width: 40%;">วันสิ้นสุดโปรโมชั่น</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtEndDate">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="display: none;">
                                                <th scope="row" style="width: 40%;">ฟรีค่าขนส่ง</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtFreeShippingFlag">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row" style="width: 40%;">จับกลุ่ม</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtLockCheckbox">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row" style="width: 40%;">แก้ไขจำนวนสินค้า</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtLockAmountFlag">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row" style="width: 40%;">ส่วนลด</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtDiscount">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr runat ="server" id="trPromotionDiscountType" visible = "false">
                                                <th scope="row" style="width: 40%;">ประเภทโปรโมชัน</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPromotionDiscountType">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr runat ="server" id="trCriteriaType" visible = "false">
                                                <th scope="row" style="width: 40%;">เงื่อนไขโปรโมชัน</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtCriteriaType">
                                                    </asp:Label>

                                                </td>
                                            </tr>

                                            <tr runat ="server" id="trApplyScope" visible = "false">
                                                <th scope="row" style="width: 40%;">โปรโมชันนี้ใช้ได้กับ</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtApplyScope">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr runat ="server" id="trOrderNumbers" visible = "false">
                                                <th scope="row" style="width: 40%;">จำนวนคำสั่งซื้อที่โปรโมชั่นต้องการ</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtOrderNumbers">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                              <tr runat ="server" id="trLazadaPromotionId" visible = "false">
                                                <th scope="row" style="width: 40%;">รหัสโปรโมชัน Lazada</th>
                                                <td>
                                                    <asp:Label runat="server" ID="txtLazadaPromotionId">
                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr runat ="server" id="trTier" visible = "false">
                                                <th scope="row" style="width: 40%;">รายละเอียดส่วนลด</th>
                                                <td>
                                                    <asp:TextBox runat="server" class ="form-control" rows="3"  ID="txtTier" style="width : 120%" TextMode="MultiLine" ReadOnly></asp:TextBox>

                                                </td>
                                            </tr>
                                            

                                            <tr>
                                                <th scope="row" style="width: 40%;">รายละเอียด
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
                                <div class="sub-title">ค้นหาสินค้า</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="Hidden1" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <input type="hidden" id="Hidden2" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />

                                    </div>
                                  
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                <%--    <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchChannelCode" runat="server" class="form-control"></asp:DropDownList>
                                    </div>--%>
                                    <%--    <label class="col-sm-1 col-form-label"></label>
                            <label class="col-sm-2 col-form-label">Merchant Name</label>
                            <div class="col-sm-4">
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
                                                <asp:LinkButton ID="btnAddLazada" OnClick="btnAddFlexicomboLazada_Click" Visible="false" class="button-action button-add m-r-10"
                                                    runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่มไปยัง Lazada</asp:LinkButton>
                                                <asp:LinkButton ID="btndeActivateLazada" visible="false" OnClick="btndeActivateFlexicomboLazada_Click"  class="button-action button-delete"
                                                    runat="server"><i class="fa fa-minus m-r-5"></i>ปิดการใช้งานบน Lazada</asp:LinkButton>
                                                <asp:LinkButton ID="btnActivateLazada" visible="false" OnClick="btnActivateFlexicomboLazada_Click"  class="button-action button-add m-r-10"
                                                    runat="server"><i class="fa fa-plus m-r-5"></i>เปิดการใช้งานบน Lazada</asp:LinkButton>
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

                                                            <div align="Center">Comboset Code</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                       <%--     <%# GetLinktoCombo(DataBinder.Eval(Container.DataItem, "CombosetCode")) %>--%>
                                                            <asp:Label ID="lblCombosetCode" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  Visible="false"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">Comboset Name</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCombosetName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailName")%>' runat="server" />
                                                            <asp:Label ID="LBLid" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoID")%>' runat="server" Visible="false" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">รหัสสินค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>--%>
                                                            <asp:HyperLink ID="hyperlink" NavigateUrl='<%# String.Format("../Product/ProductDetail.aspx?ProductCode=" + DataBinder.Eval(Container.DataItem, "ProductCode") + "&PromotionDetailInfoID=" + DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")) %>' Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server"> </asp:hyperlink>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">แบรนด์</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductBrand" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">ชื่อสินค้า</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">หน่วย</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">ราคา</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:#,0.00}")%>' runat="server" />
                                                             <asp:HiddenField runat="server" ID="hidPromotionDetailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")%>' />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">DiscountAmount</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:Label ID="lblDiscountAmount" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount","{0:#,0.00}")%>' runat="server" />                                                            
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">DiscountPercent</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:Label ID="lblDiscountPercent" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent","{0:N2}")+ "%"%>' runat="server" />                                                            
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">ราคาสินค้าที่ลดแล้ว</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:Label ID="lblProductPrice" Text='<%# DataBinder.Eval(Container.DataItem, "ProductPrice","{0:#,0.00}")%>' runat="server" />                                                            
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">จำนวน</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDefaultAmount" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' runat="server" />
                                                           
                                                            <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDefaultAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                                            <br />
                                                            <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
         
                                             <%--       <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">ช่องทางการสั่งซื้อ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>--%>

                                                    <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                          <asp:LinkButton ID="btnAdd" runat="Server" CommandName=""
                                                            class="button-activity"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-16"></span></asp:LinkButton>







                                                    </ItemTemplate>

                                                </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail" Visible="false">

                                                        <HeaderTemplate>

                                                            <div align="Center">PromotionDetailInfoID </div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionDetailInfoID" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoID")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                               <HeaderTemplate>

                                                                   <asp:LinkButton ID="btnAddSetItemPromotion" class="button-action button-add m-r-10"
                                                    OnClick="btnAddSetItemPromotion_Click" runat="server">เซต</asp:LinkButton>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                         <%--   <asp:CheckBox ID="chkItemType" runat="server" />--%>
                                                             <asp:CheckBox ID="chkItemType" runat="server"  Checked='<%# Eval("ItemType").ToString().Equals("1") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                     <HeaderTemplate>

                                                            <div align="Center">Point</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                             <asp:Label ID="lblPoint" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PointNum")%>' />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                     <HeaderTemplate>

                                                            <div align="Center">QuotaOnHand</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                             <asp:Label ID="lblQuotaOnHand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "QuotaOnHand")%>' />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                     <HeaderTemplate>

                                                            <div align="Center">QuotaBalance</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                             <asp:Label ID="lblQuotaBalance" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "QuotaBalance")%>' />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                          
                                            <%-- PAGING CAMPAIGN --%>
                                            <div class="m-t-10">
                                            <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
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
                                                                    <asp:Button ID="lnkbtnFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
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
                                                                    <asp:Button ID="lnkbtnNext" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLast" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetPageIndex"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>                                            
                                            <div class="text-center m-t-20 col-sm-12 ">
                                                <asp:Button ID="btnCreatePromotionWF" runat="server" Text="สร้าง" OnClick="btnCreatePromotionWF_Click" OnClientClick="return CheckDouble();" class="button-pri button-accept " />
                                                <asp:Button runat="server" Text="ย้อนกลับ" OnClick="btnBack_Click" class="button-pri button-cancel" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <!-- Basic Form Inputs card end -->
                            </div>
                        </div>
                        <!-- Basic Form Inputs card end -->

                        <div class="page-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Basic Form Inputs card start -->
                                    <div class="card">
                                        <div class="card-block">

                                            <div class="m-b-10">
                                                <!--Start modal Add Promotion-->
                                                <asp:LinkButton ID="btnAddTemplate" class="button-action button-add m-r-10"
                                                  OnClick="btnAddTemplate_Click"   runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม Template</asp:LinkButton>
                                              
                                                <asp:LinkButton ID="btnDelPromotionTemplate" OnClick="btnDeleteTemplate_Click"
                                                    class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                                <asp:LinkButton ID="btnAddPromotionTemplatetoLine" OnClick="btnAddtoLine_Click"  class="button-action button-add m-r-10"
                                                    runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่มไปยัง Line</asp:LinkButton>
                                                
                                                <asp:LinkButton ID="btnAddPromotionTemplatetoFacebook"  OnClick="btnAddtoFacebook_Click"  class="button-action button-add m-r-10"
                                                    runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่มไปยัง facebook</asp:LinkButton>
                                            </div>


                                            <asp:GridView ID="gvPromotionTemplate" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                               
                                                TabIndex="0" Width="100%" CellSpacing="0"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                                               <asp:CheckBox ID="chkPromotionTemplateAll" OnCheckedChanged="chkPromotionTemplateAll_Change" AutoPostBack="true" runat="server"  />
                                                            </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkPromotionTemplate" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">TemplateCode</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <div class="hideText">
                                                                <asp:Label ID="lblTemplateCode" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateCode")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "TemplateCode")%>' runat="server" />
                                                                 <asp:HiddenField runat="server" ID="hidPromotionTemplateId" Value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />
                                                                <asp:HiddenField runat="server" ID="hidTemplateImageURL" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateImageURL")%>' />
                                                                <asp:HiddenField runat="server" ID="hidTemplateBody" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateBody")%>' />
                                                            </div>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">TemplateName</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTemplateName" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">Line Status</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblFlagLine" Text='<%# DataBinder.Eval(Container.DataItem, "FlagLine")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">Facebook Status</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblFlagFacebook" Text='<%# DataBinder.Eval(Container.DataItem, "FlagFacebook")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                          
                                            <%-- PAGING CAMPAIGN --%>
                                            <div class="m-t-10">
                                            <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
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
                                                                    <asp:Button ID="lnkbtnFirstProTemp" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" OnCommand="GetPageIndexProTemp"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPreProTemp" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
                                                                        Text="<" runat="server" OnCommand="GetPageIndexProTemp"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPageProTemp" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPageProTemp_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPagesProTemp" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnNextProTemp" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndexProTemp"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLastProTemp" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetPageIndexProTemp"></asp:Button>
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
                                <!-- Basic Form Inputs card end -->
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <div class="modal fade bd-example-modal-lg" id="modal-Product" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width: 80%">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpModal" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="modal-header modal-header2   ">
                                    <div class="col-sm-12 p-0">
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
                          
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchModalProductCode" class="form-control" runat="server"></asp:TextBox>


                                    </div>
                                   
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchModalProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                              <%--      <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchModalChannel" runat="server" class="form-control"></asp:DropDownList>
                                    </div>--%>
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

                           

                            <asp:HiddenField ID="hidPromotionSet" runat="server" />
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



                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">รหัสสินค้า</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>--%>
                                            <asp:HyperLink ID="hyperlink" NavigateUrl='<%# String.Format("../Product/ProductDetail.aspx?ProductCode=" + DataBinder.Eval(Container.DataItem, "ProductCode")) %>' Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server"> </asp:hyperlink>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">แบรนด์</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductBrand" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">สินค้า</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">หน่วย</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProduUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="right">ราคา</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>

                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:#,0.00}")%>' runat="server" Style="text-align: right" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">
                                                <asp:Label runat="server" ID="lblHeaderPrice" Text="Price" />
                                            </div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrice_Ins" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' class="form-control" Enabled='<%# hidPromotionTypeCode.Text == "02" || hidPromotionTypeCode.Text == "11" %>' Style="text-align: right" />

                                            <asp:HiddenField runat="server" ID="hidProductPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">จำนวน</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty_Ins" runat="server" TextMode="Number" Value='<%#DataBinder.Eval(Container.DataItem, "Qty")%>' class="form-control txtF" Style="text-align: right" onKeyPress="return check(event,value)" onInput="checkLength()" min="1"/>
                                            <%--<asp:HiddenField runat="server" ID="hidQty_Ins" Value='<%#txtQty_Ins.Text%>' />--%>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">Point</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                             <asp:TextBox ID="txtPoint_Ins" runat="server" TextMode="Number"  class="form-control"  Style="text-align: right" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">QuotaOnHand</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                             <asp:TextBox ID="txtQuotaOnHand_Ins" runat="server" TextMode="Number"  class="form-control"  Style="text-align: right" onKeyPress="return check(event,value)" min="1" />
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
                                            <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                            <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />

                                            <asp:HiddenField runat="server" ID="hidPromotionDetailId" />






                                            <br />
                                            <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                                <EmptyDataTemplate>
                                    <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                </EmptyDataTemplate>
                            </asp:GridView>

                            
                            <%-- PAGING CAMPAIGN --%>
                            <div class="m-t-10"> 
                            <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
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
                                                    <asp:Button ID="btnPdFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                        Text="<<" runat="server" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
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
                                                    <asp:Button ID="btnPdNext" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdLast" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                        Text=">>" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </div>




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
        <div class="modal fade bd-example-modal-lg" id="modal-Template" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width: 80%">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="modal-header modal-header2   ">
                                    <div class="col-sm-12 p-0">
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
                          
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัส Template</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTemplateCode_Search" class="form-control" runat="server"></asp:TextBox>


                                    </div>
                                   
                                    <label class="col-sm-2 col-form-label">ชื่อ Template</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTemplateName_Search" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                            
                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearchTemplate" Text="ค้นหา" 
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearTemplate" Text="ล้าง" 
                                        class="button-pri button-cancel"
                                        runat="server" />

                                </div>

                           

                            <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvTemplate_RowCommand"
                                ShowHeaderWhenEmpty="true">

                                <Columns>
                                 
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">รหัส Template</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>--%>
                                            <asp:Label ID="lblTemplateCode" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateCode")%>' runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">ชื่อ Template</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductBrand" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>
                            
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                        <HeaderTemplate>
                                        </HeaderTemplate>

                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddPromotionTemplate"
                                                class="button-activity "
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-14"></span></asp:LinkButton>

                                            <asp:HiddenField runat="server" ID="hidTemplateId" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateId")%>' />
                                            <asp:HiddenField runat="server" ID="hidTemplateCode" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateCode")%>' />
                                            <asp:HiddenField runat="server" ID="hidTemplateName" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateName")%>' />


                                            <br />
                                            <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                                <EmptyDataTemplate>
                                    <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                </EmptyDataTemplate>
                            </asp:GridView>

                            
                            <%-- PAGING CAMPAIGN --%>
                            <div class="m-t-10"> 
                            <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
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
                                                    <asp:Button ID="lnkbtnFirstTemp" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                        Text="<<" runat="server" OnCommand="GetTempPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="lnkbtnPreTemp" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
                                                        Text="<" runat="server" OnCommand="GetTempPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPageTemp" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPageTemp_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                    of
                                                                                    <asp:Label ID="lblTotalPagesTemp" CssClass="fontBlack" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="lnkbtnNextTemp" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetTempPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="lnkbtnLastTemp" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                        Text=">>" OnCommand="GetTempPageIndex"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </div>




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
        <div class="modal-dialog" role="document" style="max-width: 850px">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitleCombo" class="modal-title sub-title " style="font-size: 16px;">คอมโบ</div>

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
                                <asp:GridView ID="GvProCombo" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                    OnRowDataBound="GvProCombo_RowDataBound" OnRowCreated="GvProCombo_RowCreated"
                                    TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="GvProCombo_RowCommand"
                                    ShowHeaderWhenEmpty="true">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">Combo Code</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                         <%--       <%# GetLink(DataBinder.Eval(Container.DataItem, "CombosetCode")) %>--%>
                                            <asp:Label ID="lblCombosetCode" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' runat="server" />

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">Combo Name</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblCombosetName" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetName")%>' runat="server" />

                                            </ItemTemplate>

                                        </asp:TemplateField>

                          

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">Price</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetPrice")%>' runat="server" Style="text-align: right" />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                       

                      

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                            <HeaderTemplate>
                                            </HeaderTemplate>

                                            <ItemTemplate>

                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddPromotionComboDetail"
                                                    class="button-activity "
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-14"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidCombosetId" Value='<%# DataBinder.Eval(Container.DataItem, "CombosetId")%>' />
                                                <asp:HiddenField runat="server" ID="hidCombosetCode" Value='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidCombosetName" Value='<%# DataBinder.Eval(Container.DataItem, "CombosetName")%>' />

                                                <br />
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                    <EmptyDataTemplate>
                                        <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                               
                                <%-- PAGING CAMPAIGN --%>
                                <div class="m-t-10">
                                <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
                                                    <td style="font-size: 8.5pt;"></td>
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="btnnextProcom" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                            Text="<<" runat="server" OnCommand="GetProComboPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnpreviousProcom" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
                                                            Text="<" runat="server" OnCommand="GetProComboPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlProcom" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlProcomPage_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                        of
                                                                                    <asp:Label ID="Label1" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="Button3" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetProComboPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="Button4" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                            Text=">>" OnCommand="GetProComboPageIndex"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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

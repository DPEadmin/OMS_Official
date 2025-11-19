<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="PromotionPointManagement.aspx.cs" Inherits="DOMS_TSR.src.Point.PromotionPointManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .validation {
            color: red;
        }
    </style>
    <script type="text/javascript">


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
                                <div class="sub-title">ค้นหาข้อมูลโปรโมชั่น</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                            <%--        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchProductBrand" runat="server" class="form-control"></asp:DropDownList>
                                    </div>--%>
                               
                                    <label class="col-sm-2 col-form-label">รหัสโปรโมชั่น</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchPromotionCode" class="form-control" runat="server"></asp:TextBox>
                                                 <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                        <asp:HiddenField ID="hidMerchantCode" runat="server" />
                                        <asp:HiddenField ID="hidBu" runat="server" />
                                        <asp:HiddenField ID="hidWfStatus" runat="server" />
                                        <asp:HiddenField ID="hidFinishFlag" runat="server" />
                                        <asp:HiddenField ID="hidLevels" runat="server" />
                                        <asp:HiddenField ID="hidFlagSavedraft" runat="server" />
                                        <asp:HiddenField ID="hidFlagApprove" runat="server" />
                                    </div>

                            
                                    <label class="col-sm-2 col-form-label">ชื่อโปรโมชั่น</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchPromotionName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
 
                                    <label class="col-sm-2 col-form-label">วันเริ่มโปรโมชั่น</label>
                                    <div class="col-sm-4">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchStartDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateFrom" runat="server" TargetControlID="txtSearchStartDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchStartDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateTo" runat="server" TargetControlID="txtSearchStartDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>


                                    </div>
                                 
                                    <label class="col-sm-2 col-form-label">วันสิ้นสุดโปรโมชั่น</label>
                                    <div class="col-sm-4">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchEndDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchEndDateFrom" runat="server" TargetControlID="txtSearchEndDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchEndDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchEndDateTo" runat="server" TargetControlID="txtSearchEndDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>

                                    </div>
                                     <label class="col-sm-2 col-form-label">หมวดหมู่</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlPropointSearch" runat="server" class="form-control"></asp:DropDownList>

                                    </div>
                                     <label class="col-sm-2 col-form-label">ประเภท</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlPointTypeSearch" runat="server" class="form-control"></asp:DropDownList>

                                    </div>
                                    <label class="col-sm-2 col-form-label">ร้านค้า</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlCompanySearch" runat="server" class="form-control"></asp:DropDownList>

                                    </div>
                                    <label class="col-sm-2 col-form-label">ระดับ Point</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlPointRangeSearch" runat="server" class="form-control"></asp:DropDownList>

                                    </div>

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
                                                <asp:LinkButton ID="btnAddPromotion" class="button-action button-add m-r-5"
                                                    OnClick="btnAddPromotion_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagPromotion" runat="server" />
                                            <div class="table-responsive">
                                            <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvPromotion_RowCommand"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                                            <asp:CheckBox ID="chkPromotionAll" OnCheckedChanged="chkPromotionAll_Change" AutoPostBack="true" runat="server"  />
                                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkPromotion" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

           
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">รหัสโปรโมชั่น</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# GetLink(DataBinder.Eval(Container.DataItem, "PromotionCode")) %>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">รายละเอียดการขาย</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# GetBuyerDetail(DataBinder.Eval(Container.DataItem, "PromotionCode"),DataBinder.Eval(Container.DataItem, "PromotionName")) %>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อโปรโมชั่น</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">หมวดหมู่</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionPropointName" Text='<%# DataBinder.Eval(Container.DataItem, "PropointName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ประเภท</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionPointTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "PointTypeName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ระดับPoint</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionPointRangeName" Text='<%# DataBinder.Eval(Container.DataItem, "PointRangeName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วันเริ่มโปรโมชั่น</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วันสิ้นสุดโปรโมชั่น</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEndDate" Text='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ร้านค้า</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                           
                                                            <asp:Label ID="lblCompanyNameEN" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyNameEN")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">สิทธ์ถูกใช้</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblPromotionStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionStatusName")%>' runat="server" />--%>
                                                            <asp:Label ID="lblPromotionUsed" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionUsed")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">สิทธ์คงเหลือ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblPromotionStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionStatusName")%>' runat="server" />--%>
                                                            <asp:Label ID="lblPromotionRemain" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionRemain")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowPromotion"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>







                                                            <asp:HiddenField runat="server" ID="hidPropoint" Value='<%# DataBinder.Eval(Container.DataItem, "Propoint")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPointType" Value='<%# DataBinder.Eval(Container.DataItem, "PointType")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCompanyCode" Value='<%# DataBinder.Eval(Container.DataItem, "CompanyCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFlagPatent" Value='<%# DataBinder.Eval(Container.DataItem, "FlagPatent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPatentAmount" Value='<%# DataBinder.Eval(Container.DataItem, "PatentAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountCode" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPointRangeCode" Value='<%# DataBinder.Eval(Container.DataItem, "PointRangeCode")%>' />

                                                            <asp:HiddenField runat="server" ID="hidPromotionId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDesc" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDesc")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionLevel" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionLevel")%>' />

                                                            <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShippingCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidStatus" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionStatusCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionType" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeCode")%>' />

                                                            <asp:HiddenField runat="server" ID="hidDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountAmount")%>' />

                                                            <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMinimumQty" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>' />
                                                            <asp:HiddenField runat="server" ID="hidLockCheckbox" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                                            <asp:HiddenField runat="server" ID="hidLockAmountFlag" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidGroupPrice" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />

                                                            <asp:HiddenField runat="server" ID="hidMinimumTotPrice" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumTotPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidRedeemFlag" Value='<%# DataBinder.Eval(Container.DataItem, "RedeemFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidComplementaryFlag" Value='<%# DataBinder.Eval(Container.DataItem, "ComplementaryFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidComplementaryChangeAble" Value='<%# DataBinder.Eval(Container.DataItem, "ComplementaryChangeAble")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPicturePromotionUrl" Value='<%# DataBinder.Eval(Container.DataItem, "PicturePromotionUrl")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCombosetName" Value='<%# DataBinder.Eval(Container.DataItem, "CombosetName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCombosetFlag" Value='<%# DataBinder.Eval(Container.DataItem, "CombosetFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidStartDate" Value='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy")%>' />
                                                            <asp:HiddenField runat="server" ID="hidEndDate" Value='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductBrandCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductBrandCode")%>' />
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
                                            </div>
                                            <br />
                                            <br />
                                            <%-- PAGING CAMPAIGN --%>
                                            <div class="m-t-10"></div>
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

    <div class="modal fade" id="modal-Promotion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 40%">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2    ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มโปรโมชั่น</div>

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
                            <%--    <label class="col-sm-4 col-form-label">แบรนด์<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlProductBrand_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblProductBrand_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>--%>

                                
                                <label class="col-sm-4 col-form-label">รหัสโปรโมชั่น<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">

                                    <asp:TextBox ID="txtPromotionCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblPromotionCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:HiddenField ID="hidPromotionCode_Ins" runat="server"></asp:HiddenField>
                                    <asp:HiddenField runat="server" ID="hidPromotionImgId" />

                                </div>
                                
                                <label class="col-sm-4 col-form-label">ชื่อโปรโมชั่น<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPromotionName_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="LbPromotionName_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="hidPromotionName_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                <label class="col-sm-4 col-form-label">หมวดหมู่<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlPropoint" runat="server" class="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblPropoint" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                <label class="col-sm-4 col-form-label">ประเภท<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlPointType" runat="server" class="form-control" AutoPostBack="True"  OnSelectedIndexChanged="ddlPointType_Ins_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblPointType" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                
                                    <label class="col-sm-4 col-form-label" id="DiscountCode_lbl" runat="server">โค้ดส่วนลด<span style="color: red; background-position: right top;">*</span></label>
                                    <div class="col-sm-8" id="DiscountCode_div" runat="server">
                                        <asp:TextBox ID="txtDiscountCode" runat="server" class="form-control"></asp:TextBox>
                                        <asp:Label ID="lblDiscountCode" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                              
                                <label class="col-sm-4 col-form-label">ร้านค้า<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCompany" runat="server" class="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblCompany" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                <label class="col-sm-4 col-form-label">จำนวนการเรียกใช้สิทธิ์<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlPatent" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPatent_Ins_SelectedIndexChanged">
                                        <asp:ListItem value="-99" Text="กรุณาเลือก" />
                                        <asp:ListItem value="01" Text="จำกัด" />
                                        <asp:ListItem value="02" Text="ไม่จำกัด" />
                                    </asp:DropDownList>
                                    <asp:Label ID="lblpatent" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                <label class="col-sm-4 col-form-label"></label>
                                    <div class="col-sm-6" >
                                         <asp:TextBox ID="txtpatentnum" runat="server" type="number" class="form-control"></asp:TextBox>
                                        <asp:Label ID="lblpatentnum" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                                <label class="col-sm-2 col-form-label">สิทธิ์</label>
                                <label class="col-sm-4 col-form-label">ระดับPoint<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlPointRange" runat="server" class="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblPointRange" runat="server" CssClass="validation"></asp:Label>
                                </div>
                             

                                
                                <label class="col-sm-4 col-form-label">วันเริ่มโปรโมชั่น<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtStartDate_Ins" class="form-control" placeholder="" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carStartDate_Ins" runat="server" TargetControlID="txtStartDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lblStartDate_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                
                                <label class="col-sm-4 col-form-label">วันสิ้นสุดโปรโมชั่น<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEndDate_Ins" class="form-control" placeholder="" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carEndDate_Ins" runat="server" TargetControlID="txtEndDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lblEndDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="lblStartEnd_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                            </div>


                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">สถานะ<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlPromotionStatus_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblPromotionStatus_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                
                                <label class="col-sm-4 col-form-label">รายละเอียดโปรโมชั่น</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPromotionDesc_Ins" runat="server" class="form-control"
                                        TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    <%--<asp:Label ID="Label3" runat="server" CssClass="validation"></asp:Label>--%>
                                </div>
                                <div class="col-sm-12">
                                    <div class="sub-title m-b-10 m-t-10  "></div>
                                </div>
                            </div>

                            <input type="hidden" id="hidCombosetFlag_Ins" runat="server" />
                            <input type="hidden" id="hidComplementaryFlag_Ins" runat="server" />
                            <input type="hidden" id="hidRedeemFlag_Ins" runat="server" />
                            <input type="hidden" id="hidPicturePromotionUrl_Ins" runat="server" />
                            <input type="hidden" id="hidComplementaryChangeAble_Ins" runat="server" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="form-group row">
                        <label class="col-sm-4 col-form-label">รูปภาพ</label>
                        <div class="col-sm-8">
                            <input type="file" name="files[]" id="filer_input1" multiple="multiple">
                        </div>
                    </div>

                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="btnSubmit" Text="สร้าง" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Click" runat="server" />
                        <asp:Button type="button" ID="btnSavedraft" Text="ฉบับร่าง" class="button-pri button-accept m-r-10 " OnClick="btnSavedraft_Click" runat="server" />
                        <asp:Button type="button" ID="btnCancel" Text="ยกเลิก" OnClick="btnCancel_Click" class="button-pri button-cancel m-r-10" runat="server" />
                    </div>
                </div>

            </div>
        </div>
    </div>



    <script>
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

</asp:Content>

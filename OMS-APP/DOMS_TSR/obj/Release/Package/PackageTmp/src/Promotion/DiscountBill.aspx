<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="DiscountBill.aspx.cs" Inherits="DOMS_TSR.src.Promotion.DiscountBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvDiscountBill.ClientID %>");

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
                                    <label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchProductBrand" runat="server" class="form-control"></asp:DropDownList>
                                        <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">รหัสโปรท้ายบิล</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchDiscountBillCode" class="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ชื่อโปรท้ายบิล</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchDiscountBillName" class="form-control" runat="server"></asp:TextBox>

                                    </div>


                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">วันเริ่มโปรโมชั่นท้ายบิล</label>
                                    <div class="col-sm-3">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchStartDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateFrom" runat="server" TargetControlID="txtSearchStartDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchStartDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateTo" runat="server" TargetControlID="txtSearchStartDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>


                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">วันสิ้นสุดโปรโมชั่น</label>
                                    <div class="col-sm-3">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchEndDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchEndDateFrom" runat="server" TargetControlID="txtSearchEndDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchEndDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchEndDateTo" runat="server" TargetControlID="txtSearchEndDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>

                                    </div>

                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ประเภทโปรโมชั่น</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchDiscountBillType" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">สถานะ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlDiscountBill_Status" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:Label ID="lblDiscountBill_Status" runat="server" CssClass="validation"></asp:Label>
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
                                                <!--Start modal Add DiscountBill-->
                                                <asp:LinkButton ID="btnAddDiscountBill" class="button-action button-add m-r-5"
                                                    OnClick="btnAddDiscountBill_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagDiscountBill" runat="server" />
                                            <asp:GridView ID="gvDiscountBill" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvDiscountBill_RowCommand"  OnRowDataBound ="gvDiscountBill_OnRowDataBound"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                            <asp:CheckBox ID="chkDiscountBillAll" OnCheckedChanged="chkDiscountBillAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkDiscountBill" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อแบรนด์</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">รหัสโปรท้ายบิล</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                

                                                            <asp:LinkButton ID="lnkDownload" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountBillCode")%>' PostBackUrl='DiscountBillDetail.aspx?DiscountBillCode=<%# Eval("DiscountBillCode") %>'  runat="server" ></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อโปรท้ายบิล</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscountBillName" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountBillName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ประเภทโปรท้ายบิล</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscountBillTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountBillTypeName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วันเริ่มโปรโมชั่น</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วันสิ้นสุดโปรโมชั่น</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEndDate" Text='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">สถานะ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscountActive" Text='<%# DataBinder.Eval(Container.DataItem, "DISCOUNTBILLSTATUS")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowDiscountBill"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                                <asp:HiddenField runat="server" ID="HidFlagAddProduct" Value='<%# DataBinder.Eval(Container.DataItem, "FlagAddProduct")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountBillId" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountBillId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountBillCode" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountBillCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountBillName" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountBillName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShipping")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountBillType" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountBillTypeCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMinimumTotPrice" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumTotPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidStartDate" Value='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy")%>' />
                                                            <asp:HiddenField runat="server" ID="hidEndDate" Value='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductBrandCode" Value='<%# DataBinder.Eval(Container.DataItem, "BrandCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidActive" Value='<%# DataBinder.Eval(Container.DataItem, "Active")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDISCOUNTBILLSTATUS" Value='<%# DataBinder.Eval(Container.DataItem, "DISCOUNTBILLSTATUS")%>' />
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

    <div class="modal fade" id="modal-DiscountBill" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 650px">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
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
                                <label class="col-sm-4 col-form-label">แบรนด์</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlProductBrand_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblProductBrand_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">รหัสโปรโมชั่น</label>
                                <div class="col-sm-6">

                                    <asp:TextBox ID="txtDiscountBillCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblDiscountBillCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:HiddenField ID="hidDiscountBillCode_Ins" runat="server"></asp:HiddenField>
                                    <asp:HiddenField runat="server" ID="hidDiscountBillImgId" />

                                </div>
                                <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">ชื่อโปรโมชั่น</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtDiscountBillName_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="LbDiscountBillName_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="hidDiscountBillName_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>


                                <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">วันเริ่มโปรโมชั่น</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtStartDate_Ins" class="form-control" placeholder="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carStartDate_Ins" runat="server" TargetControlID="txtStartDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lblStartDate_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">วันสิ้นสุดโปรโมชั่น</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtEndDate_Ins" class="form-control" placeholder="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carEndDate_Ins" runat="server" TargetControlID="txtEndDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lblEndDate_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <label class="col-sm-4 col-form-label">สถานะ</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlDiscountBill_Status_Ins" name="select" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiscountBillTypeIns_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblDiscountBill_Status_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <label class="col-sm-4 col-form-label">ประเภทโปรท้ายบิล</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlDiscountBillType_Ins" name="select" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiscountBillTypeIns_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="Label1" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                  </div>

                                <div class="form-group row" id="FreeShippingSection" runat="server" visible="false">

                                    <label class="col-sm-4 col-form-label">ฟรีค่าขนส่ง</label>
                              

                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlFreeShipFlag_Ins" name="select" class="form-control" runat="server">
                                            <asp:ListItem Value="-99">---- กรุณาเลือก ----</asp:ListItem>
                                            <asp:ListItem Value="Y">ใช่</asp:ListItem>
                                            <asp:ListItem Value="N">ไม่</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="LbddlFreeShipFlag_Ins" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group row" id="DiscountBillDiscountSection" runat="server"  visible="false">
                                    
                                    <label class="col-sm-4 col-form-label">ส่วนลด (บาท)</label>
                                   
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDiscountAmount_Ins" runat="server" TextMode="Number" class="form-control" onkeyup="countAmount(this)" onkeypress="return validatenumerics(event)"></asp:TextBox>
                                        <asp:Label ID="lblDiscountAmount_Ins" runat="server" CssClass="validation"></asp:Label>
                                    </div>

                                   
                                    <label class="col-sm-4 col-form-label">ส่วนลด (%)</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDiscountPercent_Ins" runat="server" TextMode="Number" class="form-control" onkeyup="countPercent(this)" onkeypress="return validatenumerics(event)"></asp:TextBox>
                                        <asp:Label ID="lblDiscountPercent_Ins" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                                </div>


                                <div class="form-group row" id="MinimumTotPriceSection" runat="server"  visible="false">
                                   
                                    <label class="col-sm-2 col-form-label">ยอดซื้อขั้นต่ำ</label>
                                    <label class="col-sm-2 col-form-label"></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtMinimumTotPrice_Ins" runat="server" TextMode="Number" class="form-control" onkeypress="return validatenumerics(event)"></asp:TextBox>
                                        <asp:Label ID="lblMinimumTotPrice_Ins" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button type="button" ID="btnSubmit" Text="สร้าง" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Click" runat="server" />
                                    <asp:Button type="button" ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click" class="button-pri button-cancel m-r-10" runat="server" />
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>
        </div>
    </div>

    <script>
        function countAmount(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtDiscountPercent_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtDiscountPercent_Ins.ClientID %>').disabled = false;
            }
        };

        function countPercent(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtDiscountAmount_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtDiscountAmount_Ins.ClientID %>').disabled = false;
            }
        };

        function validatenumerics(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57)) {
                alert(" กรุณาระบุตัวเลข ");
                return false;
            }
            else return true;
        }

    </script>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="PromotionWorkList.aspx.cs" Inherits="DOMS_TSR.src.WorkList.PromotionWorkList" %>
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
                alert("length=" + grid.rows.length);
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
                        <%--<div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูลโปรโมชั่น</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">                                                           
                                    <label class="col-sm-2 col-form-label">รหัสโปรโมชั่น</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchPromotionCode" class="form-control" runat="server"></asp:TextBox>
                                                 
                                    </div>

                            
                                    <label class="col-sm-2 col-form-label">ชื่อโปรโมชั่น</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchPromotionName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    
                                    <label class="col-sm-2 col-form-label">ระดับโปรโมชั่น</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchPromotionLevel" runat="server" class="form-control"></asp:DropDownList>

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

                               
                                    <label class="col-sm-2 col-form-label">ประเภทโปรโมชั่น</label>
                                    <div class="col-sm-4">

                                        <asp:DropDownList ID="ddlSearchPromotionType" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>

                                    </div>


                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearch" Text="ค้นหา" 
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" 
                                        class="button-pri button-cancel"
                                        runat="server" />

                                </div>

                            </div>
                        </div>--%>
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
                        <div class="page-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Basic Form Inputs card start -->
                                    <div class="card">

                                        <div class="card-header">
                                            <div class="sub-title">Promotion worklist</div>
                                        </div>

                                        <div class="card-block">

                                            <div class="m-b-10">
                                                <!--Start modal Add Promotion-->
                                                <%--<asp:LinkButton ID="btnAddPromotion" class="button-action button-add m-r-5"
                                                    OnClick="btnAddPromotion_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>--%>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagPromotion" runat="server" />
                                            <div class="table-responsive">
                                            <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvPromotion_RowCommand"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <%--<asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                                            <asp:CheckBox ID="chkPromotionAll" AutoPostBack="true" runat="server"  />
                                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkPromotion" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Brand</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion code</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "PromotionCode")) %>--%>
                                                             <asp:Label ID="lblPromotionCode" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion name</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion type</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">promotion level</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionLevelName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionLevelName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion start date</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion end date</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEndDate" Text='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Status</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWfStatus" Text='<%# DataBinder.Eval(Container.DataItem, "wfStatus")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowPromotion"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span><i class="fas fa-file-alt f-16 m-r-4"></i></span></asp:LinkButton>









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
                                                                    <asp:Button ID="lnkbtnFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First" OnCommand="GetPageIndex"
                                                                        Text="<<" runat="server" ></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous" OnCommand="GetPageIndex"
                                                                        Text="<" runat="server" ></asp:Button>
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

                                    <div class="card">

                                        <div class="card-header">
                                            <div class="sub-title">Promotion worklist Work History</div>
                                        </div>

                                        <div class="card-block">

                                            <div class="m-b-10">
                                                <!--Start modal Add Promotion-->
                                                <%--<asp:LinkButton ID="btnAddPromotion" class="button-action button-add m-r-5"
                                                    OnClick="btnAddPromotion_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>--%>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagPromotion1" runat="server" />
                                            <div class="table-responsive">
                                            <asp:GridView ID="gvPromotionCompleted" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvPromotionCompleted_RowCommand"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <%--<asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                                            <asp:CheckBox ID="chkPromotionAll" AutoPostBack="true" runat="server"  />
                                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkPromotion" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Brand</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion code</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "PromotionCode")) %>--%>
                                                             <asp:Label ID="lblPromotionCode" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion name</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion type</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">promotion level</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionLevelName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionLevelName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion start date</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Promotion end date</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEndDate" Text='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Status</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWfStatus" Text='<%# DataBinder.Eval(Container.DataItem, "wfStatus")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowPromotion"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span><i class="fas fa-file-alt f-16 m-r-4"></i></span></asp:LinkButton>









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
                                                                    <asp:Button ID="lnkbtncompFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First" OnCommand="GetcomPageIndex"
                                                                        Text="<<" runat="server" ></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtcomnPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous" OnCommand="GetcomPageIndex"
                                                                        Text="<" runat="server" ></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlcomPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                      OnSelectedIndexChanged="ddlcomPage_SelectedIndexChanged" >
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPagescom" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtncomNext" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetcomPageIndex"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtncomLast" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetcomPageIndex"></asp:Button>
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

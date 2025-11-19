<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="Promotion.aspx.cs" Inherits="DOMS_TSR.src.Promotion.Promotion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .validation {
            color: red;
        }

        .checkboxlist {
            display: inline;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="http://harvesthq.github.io/chosen/chosen.css">
    <script type="text/javascript" src="http://harvesthq.github.io/chosen/chosen.jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#modal-Promotion').on('shown.bs.modal', function () {                
                $('.chosen-select', this).chosen();
            });
        });

        function ChosenChangeSelectChanged() {
                $(".chosen-select").chosen();
        }

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

                var MsgDelete = "คุณแน่ใจที่จะลบข้อมูลนี้ ?";

                if (confirm(MsgDelete)) {

                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "TRUE";

                    return true;

                } else {

                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "FALSE";

                    return false;
                }
            }
        }
        function ApproveConfirm() {

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

                  alert("กรุณาเลือกรายการที่จะอนุมัติ");

                  return false;

              } else {

                  var MsgDelete = "คุณแน่ใจที่จะอนุมัติ?";

                  if (confirm(MsgDelete)) {

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
                                <div class="sub-title">Search Promotion information</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                            <%--        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchProductBrand" runat="server" class="form-control"></asp:DropDownList>
                                    </div>--%>
                               
                                    <label class="col-sm-2 col-form-label">Promotions code</label>
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
                                        <asp:HiddenField ID="hidTiercharge" runat="server" />
                                        <asp:HiddenField ID="hidFinishFlag" runat="server" />
                                        <asp:HiddenField ID="hidLevels" runat="server" />
                                        <asp:HiddenField ID="hidFlagSavedraft" runat="server" />
                                        <asp:HiddenField ID="hidFlagApprove" runat="server" />
                                    </div>

                            
                                    <label class="col-sm-2 col-form-label">Promotion name</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchPromotionName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    
                                    <label class="col-sm-2 col-form-label">Promotion level</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchPromotionLevel" runat="server" class="form-control"></asp:DropDownList>

                                    </div>
 
                                    <label class="col-sm-2 col-form-label">Promotion start date</label>
                                    <div class="col-sm-4">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchStartDateFrom" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateFrom" runat="server" TargetControlID="txtSearchStartDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchStartDateTo" class="form-control" placeholder="to" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateTo" runat="server" TargetControlID="txtSearchStartDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>


                                    </div>
                                 
                                    <label class="col-sm-2 col-form-label">Promotion end date</label>
                                    <div class="col-sm-4">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchEndDateFrom" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchEndDateFrom" runat="server" TargetControlID="txtSearchEndDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchEndDateTo" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchEndDateTo" runat="server" TargetControlID="txtSearchEndDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>

                                    </div>

                               
                                    <label class="col-sm-2 col-form-label">Promotion type</label>
                                    <div class="col-sm-4">

                                        <asp:DropDownList ID="ddlSearchPromotionType" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>

                                    </div>

                                    <label class="col-sm-2 col-form-label">PromotionTag</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchPromotionTag" runat="server" class="form-control" AutoPostBack="True" Visible="true"></asp:DropDownList>
                                    </div>

                            
                                    <label class="col-sm-2 col-form-label">ProductTag</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchProductTag" runat="server" class="form-control" AutoPostBack="True" Visible="true"></asp:DropDownList>
                                    </div>


                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click"
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
                                                        <asp:LinkButton ID="btnApprovePromotion" class="button-action button-add m-r-5"
                                                    OnClick="btnApprovePromotion_Click" OnClientClick="return ApproveConfirm();" runat="server"><i class="fa fa-plus m-r-5"></i>Approve</asp:LinkButton>
                                                <asp:LinkButton ID="btnAddPromotion" class="button-action button-add m-r-5"
                                                    OnClick="btnAddPromotion_Click" runat="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagPromotion" runat="server" />
                                            <div class="table-responsive">
                                            <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowDataBound="gvPromotion_RowDataBound" OnRowCommand="gvPromotion_RowCommand"
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

                                                            <div align="left">Promotions code</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# GetLink(DataBinder.Eval(Container.DataItem, "PromotionCode")) %>
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

                                                            <div align="left">Promotion Code</div>

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
                                                            <%--<asp:Label ID="lblPromotionStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionStatusName")%>' runat="server" />--%>
                                                            <asp:Label ID="lblWfStatus" Text='<%# DataBinder.Eval(Container.DataItem, "WfStatus")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">LazadaStatus</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblPromotionStatusName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionStatusName")%>' runat="server" />--%>
                                                            <asp:Label ID="lblLazStatus" Text='<%# DataBinder.Eval(Container.DataItem, "LazadaPromotionStatusName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PromotionTag</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionTagName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionTagName")%>' runat="server" />

                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ProductTag</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductTagName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductTagName")%>' runat="server" />

                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowPromotion"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>









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
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountPercentTier2" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountPercentTier2")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountAmountTier2" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountAmountTier2")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMinimumQty" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMinimumQtyTier2" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQtyTier2")%>' />
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

                                                            <asp:HiddenField runat="server" ID="hidApplyScope" Value='<%# DataBinder.Eval(Container.DataItem, "ApplyScope")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCriteriaType" Value='<%# DataBinder.Eval(Container.DataItem, "CriteriaType")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountType" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountType")%>' />
                                                            <asp:HiddenField runat="server" ID="hidOrderNumbers" Value='<%# DataBinder.Eval(Container.DataItem, "OrderNumbers")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCriteriaValueTier1" Value='<%# DataBinder.Eval(Container.DataItem, "CriteriaValueTier1")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCriteriaValueTier2" Value='<%# DataBinder.Eval(Container.DataItem, "CriteriaValueTier2")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCriteriaValueTier3" Value='<%# DataBinder.Eval(Container.DataItem, "CriteriaValueTier3")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountValueTier1" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountValueTier1")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountValueTier2" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountValueTier2")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountValueTier3" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountValueTier3")%>' />
                                                            <asp:HiddenField runat="server" ID="hidLazadaPromotionStatus" Value='<%# DataBinder.Eval(Container.DataItem, "LazadaPromotionStatus")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionTagCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTagCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionTagName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTagName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductTagCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductTagCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductTagName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductTagName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionQuotaFlag" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionQuotaFlag")%>' />
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
        <div class="modal-dialog" role="document" style="max-width: 80%">

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

                                
                                <label class="col-sm-2 col-form-label">Promotion Code<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">

                                    <asp:TextBox ID="txtPromotionCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblPromotionCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:HiddenField ID="hidPromotionCode_Ins" runat="server"></asp:HiddenField>
                                    <asp:HiddenField runat="server" ID="hidPromotionImgId" />

                                </div>
                                
                                <label class="col-sm-2 col-form-label">Promotion Name<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPromotionName_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="LbPromotionName_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="hidPromotionName_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                
                                <label class="col-sm-2 col-form-label">Promotion level<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPromotionLevel_Ins" runat="server" class="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblPromotionLevel_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>

                                
                                <label class="col-sm-2 col-form-label">Promotion period<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <div class="input-group mb-0">
                                    <asp:TextBox ID="txtStartDate_Ins" class="form-control" placeholder="วันเริ่มโปรโมชัน" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carStartDate_Ins" runat="server" TargetControlID="txtStartDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lblStartDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:TextBox ID="txtEndDate_Ins" class="form-control" placeholder="วันสิ้นสุดโปรโมชัน" runat="server" AutoCompleteType="Disabled"   onkeydown="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carEndDate_Ins" runat="server" TargetControlID="txtEndDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lblEndDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="lblStartEnd_Ins" runat="server" CssClass="validation"></asp:Label>

                                    </div>

                                </div>
                                <%--  
                                <label class="col-sm-2 col-form-label">เป็นคอมโบเซ็ท</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlCombosetFlag_Ins" name="select" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCombosetFlag_Ins_SelectedIndexChanged">
                                        <asp:ListItem Value="-99">---- กรุณาเลือก ----</asp:ListItem>
                                        <asp:ListItem Value="Y">เป็น</asp:ListItem>
                                        <asp:ListItem Value="N">ไม่เป็น</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                            </div>


                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">สถานะ<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPromotionStatus_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblPromotionStatus_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <!--อย่าลบ และ อย่าย้าย <style> -->
                                <style>
                                    .chosen-container {
                                        width:100% !important;
                                    }
                                </style>
                                <!--อย่าลบ และ อย่าย้าย <style> -->
                                <label class="col-sm-2 col-form-label">MultiPromotionTag<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <asp:ListBox ID="ddlMultiSelectPromoTag_Ins" data-placeholder="กรุณาเลือก PromotionTag..." Visible="true" class="chosen-select form-control" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlMultiSelectPromoTag_Ins_SelectChanged" runat="server"></asp:ListBox>
                                    <asp:Label ID="lbllMultiSelectPromoTag_Ins" runat="server" CssClass="validation"></asp:Label>
                                    
                                </div>                                            


                            </div>


                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label"><span style="color: red; background-position: right top;"></span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPromotionTag_Ins" class="form-control" Visible="false" AutoPostBack="True" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblPromotionTag_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <label class="col-sm-2 col-form-label"><span style="color: red; background-position: right top;"></span></label>
                                <div class="col-sm-4">
                                    
                                    
                                </div>                                                                        
                                    
                             </div>     

                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>

                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">ProductTag<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                   <asp:CheckBoxList ID="chklistPromotionMapProductTag_Ins" Width="100%" CssClass="checkboxlist" runat="server"></asp:CheckBoxList>
                                    <asp:Label ID="lblPromotionMapProductTag_Ins" runat="server" CssClass="validation"></asp:Label> 

                                </div>

                                <label class="col-sm-2 col-form-label">Promotion type<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">                                                                        
                                    <asp:DropDownList ID="ddlPromotionType_Ins" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPromoType_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblPromotionType_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>    

                            </div>       
                            
                            <div id="insertfalshsalesection" runat="server">
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">FlashSale StartDateTime<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <div class="input-group mb-0">
                                    <asp:TextBox ID="txtFlashSaleStartDate_Ins" class="form-control" placeholder="วันเริ่มแฟลชเซลส์" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="FlashSaleStartDate_Ins" runat="server" TargetControlID="txtFlashSaleStartDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>                                    
                                    <asp:TextBox ID="txtFlashSaleStartTime_Ins" class="form-control" TextMode="Time" placeholder="เวลาเริ่มแฟลชเซลส์" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblFlashSaleStartDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="lblFlashSaleStartTime_Ins" runat="server" CssClass="validation"></asp:Label>

                                    </div>

                                </div>

                                <label class="col-sm-2 col-form-label">FlashSale EndDateTime<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <div class="input-group mb-0">
                                    <asp:TextBox ID="txtFlashSaleEndDate_Ins" class="form-control" placeholder="วันสิ้นสุดแฟลชเซลส์" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="FlashSaleEndDate_Ins" runat="server" TargetControlID="txtFlashSaleEndDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>                                    
                                    <asp:TextBox ID="txtFlashSaleEndTime_Ins" class="form-control" TextMode="Time" placeholder="เวลาสิ้นสุดแฟลชเซลส์" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblFlashSaleEndDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="lblFlashSaleEndTime_Ins" runat="server" CssClass="validation"></asp:Label>

                                    </div>

                                </div>

                            </div>
                            </div>

                            <div class="col-sm-12">
                                    <div class="sub-title m-b-10 m-t-10  "></div>
                            </div>

                            <div class="form-group row" id="GroupingSection" runat="server">

                                <label class="col-sm-2 col-form-label">Group</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlLockCheckbox_Ins" name="select" class="form-control " runat="server">
                                        <asp:ListItem Value="-99">---- Please select ----</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbddlLockCheckbox_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>

                                
                                <label class="col-sm-2 col-form-label">แก้ไขจำนวนสินค้า</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlLockAmountFlag_Ins" name="select" class="form-control" runat="server">
                                        <asp:ListItem Value="-99">---- กรุณาเลือก ----</asp:ListItem>
                                        <asp:ListItem Value="N">แก้ไขได้</asp:ListItem>
                                        <asp:ListItem Value="Y">แก้ไขไม่ได้</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="LbddlLockAmountFlag_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row" id="FreeShippingSection" runat="server" visible="false">

                                <label class="col-sm-2 col-form-label">ฟรีค่าขนส่ง</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlFreeShipFlag_Ins" name="select" class="form-control" runat="server">
                                        <asp:ListItem Value="-99">---- กรุณาเลือก ----</asp:ListItem>
                                        <asp:ListItem Value="Y">ใช่</asp:ListItem>
                                        <asp:ListItem Value="N">ไม่</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="LbddlFreeShipFlag_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row" id="ComplementaryStandardSection" runat="server" visible="false">

                                <label class="col-sm-2 col-form-label">การเปลี่ยนของแถม</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddl_ComplementaryStandardChangeAble_Ins" name="select" class="form-control" runat="server">
                                        <asp:ListItem Value="-99">---- กรุณาเลือก ----</asp:ListItem>
                                        <asp:ListItem Value="N">เปลี่ยนของแถมไม่ได้</asp:ListItem>
                                        <asp:ListItem Value="Y">เปลี่ยนของแถมได้</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label1" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row" id="PromotionDiscountSection" runat="server">

                                <label class="col-sm-2 col-form-label">ส่วนลด (บาท)</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDiscountAmount_Ins" runat="server" class="form-control" onkeyup="countPromoDisAmount(this)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:Label ID="lblDiscountAmount_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                
                                <label class="col-sm-2 col-form-label">ส่วนลด (%)</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDiscountPercent_Ins" runat="server" class="form-control" onkeyup="countPromoDisPercent(this)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:Label ID="lblDiscountPercent_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row" id="ProductDiscountSection" runat="server">

                                <label class="col-sm-2 col-form-label">ส่วนลด (บาท)</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtProductDiscountAmount_Ins" runat="server" class="form-control" onkeyup="countProductDisAmount(this)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:Label ID="lblProductDiscountAmount_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                
                                <label class="col-sm-2 col-form-label">ส่วนลด (%)</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtProductDiscountPercent_Ins" runat="server" class="form-control" onkeyup="countProductDisPercent(this)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:Label ID="lblProductDiscountPercent_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>
                            
                            <div class="form-group row" id="ProductDiscountSectionTier2" runat="server">

                                <label class="col-sm-2 col-form-label">ส่วนลด (บาท) Tier2</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtProductDiscountAmountTier2_Ins" runat="server" class="form-control" onkeyup="countProductDisAmountTier2(this)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:Label ID="lblProductDiscountAmountTier2_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                
                                <label class="col-sm-2 col-form-label">ส่วนลด (%) Tier2</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtProductDiscountPercentTier2_Ins" runat="server" class="form-control" onkeyup="countProductDisPercentTier2(this)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:Label ID="lblProductDiscountPercentTier2_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row" id="GroupPriceSection" runat="server">
                                <label class="col-sm-2 col-form-label">ราคา</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtGroupPrice_Ins" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:Label ID="lblGroupPrice_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row" id="MOQSection" runat="server">

                                <label class="col-sm-2 col-form-label">กำหนดจำนวนสินค้าขั้นต่ำ</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlMOQFlag_Ins" name="select" class="form-control" runat="server">
                                        <asp:ListItem runat="server" Value="-99">---- กรุณาเลือก ----</asp:ListItem>
                                        <asp:ListItem runat="server" Value="Y">ใช่</asp:ListItem>
                                        <asp:ListItem runat="server" Value="N">ไม่</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblMOQFlag_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                
                                <asp:Label ID="LbLowQty" runat="server" Text="จำนวนสินค้าขั้นต่ำ (หน่วย)" CssClass="col-sm-2 col-form-label "></asp:Label>
                                <%--<label class="col-sm-4 col-form-label">จำนวนสินค้าขั้นต่ำ (หน่วย)</label>--%>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMinimumQty_Ins" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblMinimumQty_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row" id="MOQSectionTier2" runat="server">
                                   <asp:Label ID="LbLowQtyTier2" runat="server" Text="จำนวนสินค้าขั้นต่ำ Tier2 (หน่วย)" CssClass="col-sm-2 col-form-label "></asp:Label>
                                <%--<label class="col-sm-4 col-form-label">จำนวนสินค้าขั้นต่ำ (หน่วย)</label>--%>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMinimumQtyTier2_Ins" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblMinimumQtyTier2_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row" id="MinimumTotPriceSection" runat="server">

                                <label class="col-sm-2 col-form-label">ยอดซื้อขั้นต่ำ</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMinimumTotPrice_Ins" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblMinimumTotPrice_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>



                            <div class="form-group row" visible="false" id="CombosetSection" runat="server">
                                
                                <label class="col-sm-2 col-form-label">ชื่อคอมโบเซ็ท</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtCombosetName_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblCombosetName_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>

                            </div>
                            <div class="form-group row" visible="false" id="FlexiComboSection" runat="server">
                                  <label class="col-sm-2 col-form-label">โปรโมชั่นนี้สามารถใช้ได้กับ</label>
                                <div class="col-sm-4">
                                    <asp:RadioButtonList ID="rbtApplyScope" runat="server" RepeatLayout="Flow">
                                            <asp:ListItem Value="ENTIRE_STORE">ทั้งร้าน</asp:ListItem>
                                            <asp:ListItem Value="SPECIFIC_PRODUCTS">เฉพาะบางสินค้า(โปรดเพิ่มสินค้าหลังสร้าง)</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblApplyScope_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                               
                                <label class="col-sm-2 col-form-label">เงื่อนไขโปรโมชัน<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <asp:RadioButtonList ID="rbtCriteriaType" runat="server" AutoPostBack="True" OnSelectedIndexChanged ="rbtCriteriaType_SelectedIndexChanged" RepeatLayout="Flow" >
                                            <asp:ListItem Value="AMOUNT">ตามมูลค่าการสั่งซื้อ</asp:ListItem>
                                            <asp:ListItem Value="QUANTITY">ตามจำนวนรายการ</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblCriteriaType_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                                 <label class="col-sm-2 col-form-label">ประเภทส่วนลด<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                   <asp:DropDownList ID="ddlDisCountType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDisCountType_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblDisCountType" runat="server" CssClass="validation"></asp:Label>
                                </div>
                               <label class="col-sm-2 col-form-label">จำนวนคำสั่งซื้อโปรโมชันที่ต้องการ<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtOrderNumbers_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblOrderNumbers_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>  
                                </div>
                            <div style ="background-color :#eee;">
                            <div class="form-group row" visible="false" id="Tier1Section" runat="server">
                                <label class="col-sm-2 col-form-label">
                                   <asp:LinkButton ID="btnAddTier" OnClick="btnAddTier_Click" class="button-action button-add m-r-5"
                                       runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม Tier</asp:LinkButton>
                                </label>
                                <div class="col-sm-4">
                                    <div class="input-group mb-0">
                                         <label runat = "server" id ="lblCriteriaValueHeader">หากจำนวนรายการถึง</label>
                                         <label style ="margin-left : 50px;" id ="lblDiscountValueHeader" runat ="server">ส่วนลดจะเป็น</label>
                                    </div>
                                </div>
                               
                                 <label class="col-sm-2 col-form-label"></label>
                                <div class="col-sm-4"></div>

                                <label class="col-sm-2 col-form-label" style="text-align: Right;">Tier1</label>
                                <div class="col-sm-4">
                                      <div class="input-group mb-1">
                                         <asp:TextBox ID="txtCriteriaValueTier1_Ins" runat="server" class="form-control"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                        
                                         <asp:TextBox ID="txtDiscountValueTier1_Ins" runat="server" class="form-control"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                        
                                      </div>
                                </div>
                             </div>
                             <div class="form-group row"  visible="false" id="Tier2Section" runat="server">
                            
                                 <label class="col-sm-2 col-form-label" style="text-align: Right;">Tier2</label>
                                <div class="col-sm-4">
                                     <div class="input-group mb-0">
                                         <asp:TextBox ID="txtCriteriaValueTier2_Ins" runat="server" class="form-control"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                        
                                         <asp:TextBox ID="txtDiscountValueTier2_Ins" runat="server" class="form-control"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                         
                                      </div>
                                </div>
                                 <asp:LinkButton ID="btnCloseTier2" OnClick="btnTier2_Close" class="close"
                                       runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
                              
                                    
                                 </div>
                            <div class="form-group row" visible="false" id="Tier3Section" runat="server">
                                        
                                      <label class="col-sm-2 col-form-label" style="text-align: Right;">Tier3</label>
                                <div class="col-sm-4">
                                     <div class="input-group mb-0">
                                         <asp:TextBox ID="txtCriteriaValueTier3_Ins" runat="server" class="form-control"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                         
                                         <asp:TextBox ID="txtDiscountValueTier3_Ins" runat="server" class="form-control"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                         
                                      </div>
                                </div>
                                    <asp:LinkButton ID="btnCloseTier3" OnClick="btnTier3_Close" class="close"
                                       runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
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
                        <label class="col-sm-2 col-form-label">รูปภาพ</label>
                        <div class="col-sm-4">
                            <input type="file" name="files[]" id="filer_input1" multiple="multiple">
                        </div>
                       
                    </div>
                    <div class="form-group row">
                         <label class="col-sm-2 col-form-label">รายละเอียดโปรโมชั่น</label>
                         <div class="col-sm-10">
                            <asp:TextBox ID="txtPromotionDesc_Ins" runat="server" class="form-control"
                                TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    <%--<asp:Label ID="Label3" runat="server" CssClass="validation"></asp:Label>--%>
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
        function countPromoDisAmount(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtDiscountPercent_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtDiscountPercent_Ins.ClientID %>').disabled = false;
            }
        };

        function countPromoDisPercent(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtDiscountAmount_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtDiscountAmount_Ins.ClientID %>').disabled = false;
            }
        };

        function countProductDisAmount(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtProductDiscountPercent_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtProductDiscountPercent_Ins.ClientID %>').disabled = false;
            }
        };
        function countProductDisAmountTier2(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtProductDiscountPercentTier2_Ins.ClientID %>').value = 0;
                document.getElementById('<%= txtProductDiscountPercentTier2_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtProductDiscountPercentTier2_Ins.ClientID %>').disabled = false;
            }
        };

        function countProductDisPercent(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtProductDiscountAmount_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtProductDiscountAmount_Ins.ClientID %>').disabled = false;
            }
        };
        function countProductDisPercentTier2(val) {
            var len = val.value.length;
            if (len > 0) {
                document.getElementById('<%= txtProductDiscountAmountTier2_Ins.ClientID %>').value = 0;
                document.getElementById('<%= txtProductDiscountAmountTier2_Ins.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= txtProductDiscountAmountTier2_Ins.ClientID %>').disabled = false;
            }
        };
    </script>

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

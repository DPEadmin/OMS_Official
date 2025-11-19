<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="MediaPlan.aspx.cs" Inherits="DOMS_TSR.src.MediaPlanManagement.MediaPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvlMediaPlan.ClientID %>");

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

    <script>
        $(document).ready(function () {
            $("#btnPreviewImport").click(function () {
                $("#modal-ImportMedia").modal();
            });
        });
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>



    <asp:HiddenField ID="hd" runat="server" />
    <div class="page-body">
        <div class="row">
            <div class="col-sm-12">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูล Media Plan</div>
                            </div>
                            <div class="card-body">

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Media Plan Date </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtdateMediaPlan" class="form-control" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="car_SearchtimeMediaPlan" runat="server" TargetControlID="txtdateMediaPlan" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">Media Plan Time</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txttimeMediaPlan" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ชื่อรายการ</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtProgramName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchMediaPlanChannel" class="form-control" runat="server"></asp:DropDownList>
                                        <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Duration</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtDuration" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">Media Phone</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtMediaPhone" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">แคมเปญ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlCamp" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <%--<label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อโปรโมชั่น</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlpromotion" class="form-control" runat="server"></asp:DropDownList>
                                    </div>--%>
                                </div>



                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                </div>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-4">
                                <asp:FileUpload ID="fiUpload" runat="server" class="form-control"></asp:FileUpload>
                            </div>
                            <div class="col-4">
                                <asp:Button type="button" ID="btnUpload" Text="Upload" class="button-pri button-accept m-r-10" OnClick="btnUpload_Click" runat="server" />
                            </div>
                            <div class="col-4">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnShowImportFile" OnClick="btnShowImportFile_Click" runat="server" class="button-pri button-accept m-r-10">Show</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-body">

                                <div class="m-b-10">
                                    <!--Start modal Add MediaPlan-->
                                    <asp:LinkButton ID="btnAddMediaPlan" class="button-action button-add m-r-5"
                                        OnClick="btnAddMediaPlan_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                        class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>

                                </div>

                                <asp:HiddenField ID="hidMOQFlagMediaPlan" runat="server" />
                                <asp:GridView ID="gvlMediaPlan" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                    TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvlMediaPlan_RowCommand" OnRowDataBound="gvlMediaPlan_OnRowDataBound"
                                    ShowHeaderWhenEmpty="true">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkMediaPlanAll" OnCheckedChanged="chkMediaPlanAll_Change" AutoPostBack="true" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkMediaPlan" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MediaPlanDate</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMediaPlanDate" Text='<%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MediaPlanDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MediaPlanTime</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMediaPlanTime" Text='<%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MediaPlanTime").ToString()).ToString("HH:ss น.") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ProgramName</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProgramName" Text='<%# DataBinder.Eval(Container.DataItem, "ProgramName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Channel</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChannel" Text='<%# DataBinder.Eval(Container.DataItem, "Channel")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Duration</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDuration" Text='<%# DataBinder.Eval(Container.DataItem, "Duration")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MediaPhone</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMediaPhone" Text='<%# DataBinder.Eval(Container.DataItem, "MediaPhone")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">CampaignCode</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCampaignCode" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowMediaPlan" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="HidMediaPlanId" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPlanId")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanDate" Value='<%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MediaPlanDate").ToString()).ToString("dd/MM/yyyy") %>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanTime" Value='<%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MediaPlanTime").ToString()).ToString("HH:ss") %>' />
                                                <asp:HiddenField runat="server" ID="HidProgramName" Value='<%# DataBinder.Eval(Container.DataItem, "ProgramName")%>' />
                                                <asp:HiddenField runat="server" ID="HidDuration" Value='<%# DataBinder.Eval(Container.DataItem, "Duration")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPhone" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPhone")%>' />
                                                <%--<asp:HiddenField runat="server" ID="HidChannelName" Value='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' />
                                                <asp:HiddenField runat="server" ID="hidChannelCode" Value='<%# DataBinder.Eval(Container.DataItem, "ChannelCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidStatusActive" Value='<%# DataBinder.Eval(Container.DataItem, "StatusActive")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanCode" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPlanCode")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanName" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPlanName")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanProgramName" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPlanProgramName")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanDate" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPlanDate")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanTime" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPlanTime")%>' />
                                                <asp:HiddenField runat="server" ID="HidMediaPlanDuration" Value='<%# DataBinder.Eval(Container.DataItem, "MediaPlanDuration")%>' />
                                                <asp:HiddenField runat="server" ID="HidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                                <asp:HiddenField runat="server" ID="HidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                <asp:HiddenField runat="server" ID="HidSaleChannelCode" Value='<%# DataBinder.Eval(Container.DataItem, "SaleChannelCode")%>' />--%>

                                                <br />
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modal-MediaPlan" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 650px">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <asp:Label ID="lblModalHeaderMediaPlan" class="modal-title" style="font-size: 16px;" runat="server">เพิ่ม/แก้ไข Media Plan</asp:Label>
                                <div class="sub-title"></div>
                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
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
                                <label class="col-sm-4 col-form-label">Media Plan Date</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMediaPlanDate_Ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="car_txtMediaPlanDate_Ins" runat="server" TargetControlID="txtMediaPlanDate_Ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lblMediaPlanDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">Media Plan Time</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMediaPlanTime_Ins" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblMediaPlanTime_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>
                            
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">Program Name</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtProgramName_Ins" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblProgramName_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">Channel</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlChannel_Ins" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblChannel_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">Duration</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtDuration_Ins" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblDuration_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>
                            
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">Media Phone</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMediaPhone_Ins" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblMediaPhone_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">รหัสแคมเปญ</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCamp_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblddlCamp_Ins" runat="server" CssClass="validation"></asp:Label>
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


    <div class="modal fade" id="modal-ImportMedia" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 1000px">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitleImport" class="modal-title sub-title " style="font-size: 16px;">Import Media Excel</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="modal-body ">
                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="btnSubmitImport" Text="submit" class="button-pri button-accept m-r-10" runat="server" OnClick="btnSubmitImport_Click" />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="gvMediaPlanImport" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">LINE_NO</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLINE_NO" Text='<%# DataBinder.Eval(Container.DataItem, "LINE_NO")%>' runat="server" />

                                            <asp:HiddenField runat="server" ID="HidDATE" Value='<%# DataBinder.Eval(Container.DataItem, "DATE")%>' />
                                            <asp:HiddenField runat="server" ID="HidTIME" Value='<%# DataBinder.Eval(Container.DataItem, "TIME")%>' />
                                            <asp:HiddenField runat="server" ID="HidPROGRAM_NAME" Value='<%# DataBinder.Eval(Container.DataItem, "PROGRAM_NAME")%>' />
                                            <asp:HiddenField runat="server" ID="HidCHANNEL" Value='<%# DataBinder.Eval(Container.DataItem, "CHANNEL")%>' />
                                            <asp:HiddenField runat="server" ID="HidDURATION" Value='<%# DataBinder.Eval(Container.DataItem, "DURATION")%>' />
                                            <asp:HiddenField runat="server" ID="HidMEDIA_PHONE" Value='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' />
                                            <asp:HiddenField runat="server" ID="HidCAMPAIGN_NAME" Value='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_NAME")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">DATE</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDATE" Text='<%# DataBinder.Eval(Container.DataItem, "DATE")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">TIME</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTIME" Text='<%# DataBinder.Eval(Container.DataItem, "TIME")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">PROGRAM_NAME</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROGRAM_NAME" Text='<%# DataBinder.Eval(Container.DataItem, "PROGRAM_NAME")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">CHANNEL</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCHANNEL" Text='<%# DataBinder.Eval(Container.DataItem, "CHANNEL")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">DURATION</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDURATION" Text='<%# DataBinder.Eval(Container.DataItem, "DURATION")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">MEDIA_PHONE</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMEDIA_PHONE" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                            <div align="left">CAMPAIGN_NAME</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCAMPAIGN_NAME" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_NAME")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

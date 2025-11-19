<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="SaleChannel.aspx.cs" Inherits="DOMS_TSR.src.SaleChannel.SaleChannel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvSaleChannel.ClientID %>");

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
                                <div class="sub-title">ค้นหาข้อมูล Sale Channel</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchSaleChannelChannel" class="form-control" runat="server"></asp:DropDownList>
                                        <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">สถานะ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchSaleChannelActive" class="form-control" runat="server"></asp:DropDownList>

                                    </div>

                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสช่องทางขาย</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSaleChannelCode" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อช่องทางขาย</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtname" class="form-control" runat="server"></asp:TextBox>

                                    </div>


                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">วันเริ่ม</label>
                                    <div class="col-sm-3">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchStartDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateFrom" runat="server" TargetControlID="txtSearchStartDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchStartDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateTo" runat="server" TargetControlID="txtSearchStartDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>


                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">เบอร์</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPhone" class="form-control" runat="server"></asp:TextBox>

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
                                                <!--Start modal Add SaleChannel-->
                                                <asp:LinkButton ID="btnAddSaleChannel" class="button-action button-add m-r-5"
                                                    OnClick="btnAddSaleChannel_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagSaleChannel" runat="server" />
                                            <asp:GridView ID="gvSaleChannel" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvSaleChannel_RowCommand" OnRowDataBound="gvSaleChannel_OnRowDataBound"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                            <asp:CheckBox ID="chkSaleChannelAll" OnCheckedChanged="chkSaleChannelAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkSaleChannel" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Channel</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSaleChannelTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">SaleChannelCode</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSaleChannelCode" Text='<%# DataBinder.Eval(Container.DataItem, "SaleChannelCode")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">SaleChannelName</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSaleChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleChannelName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Tel</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSaleChannelTel" Text='<%# DataBinder.Eval(Container.DataItem, "SaleChannelPhone")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วันเริ่ม</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartTime"))||("" == Eval("StartTime"))) ? string.Empty : DateTime.Parse(Eval("StartTime").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วันสิ้น</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEndDate" Text='<%# ((null == Eval("EndTime"))||("" == Eval("EndTime"))) ? string.Empty : DateTime.Parse(Eval("EndTime").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowSaleChannel"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

                                                            <asp:HiddenField runat="server" ID="HidSaleChannelId" Value='<%# DataBinder.Eval(Container.DataItem, "SaleChannelId")%>' />
                                                            <asp:HiddenField runat="server" ID="HidChannelName" Value='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidChannelCode" Value='<%# DataBinder.Eval(Container.DataItem, "ChannelCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidStatusActive" Value='<%# DataBinder.Eval(Container.DataItem, "StatusActive")%>' />
                                                               <asp:HiddenField runat="server" ID="HidSaleChannelCode" Value='<%# DataBinder.Eval(Container.DataItem, "SaleChannelCode")%>' />
                                                            <asp:HiddenField runat="server" ID="HidSaleChannelName" Value='<%# DataBinder.Eval(Container.DataItem, "SaleChannelName")%>' />
                                                            <asp:HiddenField runat="server" ID="HidSaleChannelPhone" Value='<%# DataBinder.Eval(Container.DataItem, "SaleChannelPhone")%>' />
                                                            <asp:HiddenField runat="server" ID="hidStartTime" Value='<%# ((null == Eval("StartTime"))||("" == Eval("StartTime"))) ? string.Empty : DateTime.Parse(Eval("StartTime").ToString()).ToString("dd/MM/yyyy")%>' />
                                                            <asp:HiddenField runat="server" ID="hidEndTime" Value='<%# ((null == Eval("EndTime"))||("" == Eval("EndTime"))) ? string.Empty : DateTime.Parse(Eval("EndTime").ToString()).ToString("dd/MM/yyyy")%>' />

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

    <div class="modal fade" id="modal-SaleChannel" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 650px">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่ม Sale channel</div>

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
                                <label class="col-sm-4 col-form-label">Channel</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlChannel_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblChannel_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                <label class="col-sm-4 col-form-label">รหัสช่องทางขาย</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtsalechannelcode_ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblsalechannelcode_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <label class="col-sm-4 col-form-label">ชื่อช่องทางขาย</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtsalechannelname_ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblsalechannelname_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>

                                <label class="col-sm-4 col-form-label">ชื่เบอร์</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtsalechannelPhone_ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblsalechannelphone_Ins" runat="server" CssClass="validation"></asp:Label>

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
                                    <asp:DropDownList ID="ddlStatusActive_Ins" name="select" class="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbStatusActive_Ins" runat="server" CssClass="validation"></asp:Label>

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




</asp:Content>

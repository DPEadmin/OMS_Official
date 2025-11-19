<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="Planner.aspx.cs" Inherits="DOMS_TSR.src.Planner.Planner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvPlanner.ClientID %>");

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
                                <div class="sub-title">ค้นหาข้อมูล Planner</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchPlannerChannel" class="form-control" runat="server"></asp:DropDownList>
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
                                        <asp:DropDownList ID="ddlSearchPlannerActive" class="form-control" runat="server"></asp:DropDownList>

                                    </div>

                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">แคมเปญ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlCamp" class="form-control" runat="server"></asp:DropDownList>

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อโปรโมชั่น</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlpromotion" class="form-control" runat="server"></asp:DropDownList>

                                    </div>


                                </div>
                                         <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัส Planner</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtplannercode" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อ Planner</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtplannername" class="form-control" runat="server"></asp:TextBox>

                                    </div>


                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ช่องทางขาย</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlChannelCode" class="form-control" runat="server"></asp:DropDownList>

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อรายการ</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtprogramname" class="form-control" runat="server"></asp:TextBox>

                                    </div>


                                </div>


                       
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Date Planner</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtdateplanner" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">เวลา Planner</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txttimeplanner" class="form-control" runat="server"></asp:TextBox>

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
                                                <!--Start modal Add Planner-->
                                                <asp:LinkButton ID="btnAddPlanner" class="button-action button-add m-r-5"
                                                    OnClick="btnAddPlanner_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagPlanner" runat="server" />
                                            <asp:GridView ID="gvPlanner" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvPlanner_RowCommand" OnRowDataBound="gvPlanner_OnRowDataBound"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                            <asp:CheckBox ID="chkPlannerAll" OnCheckedChanged="chkPlannerAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkPlanner" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Channel</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlannerTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PlannerCode</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlannerCode" Text='<%# DataBinder.Eval(Container.DataItem, "PlannerCode")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PlannerName</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlannerName" Text='<%# DataBinder.Eval(Container.DataItem, "PlannerName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อรายการ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlannerProgramName" Text='<%# DataBinder.Eval(Container.DataItem, "PlannerProgramName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วัน และ เวลา</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlannerDate" Text='<%# DataBinder.Eval(Container.DataItem, "PlannerDate")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ระยะเวลา</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlannerDuration" Text='<%# DataBinder.Eval(Container.DataItem, "PlannerDuration")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">แคมเปญ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampaignCode" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อโปรโมชั่น</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionCode" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowPlanner"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

                                                            <asp:HiddenField runat="server" ID="HidPlannerId" Value='<%# DataBinder.Eval(Container.DataItem, "PlannerId")%>' />
                                                            <asp:HiddenField runat="server" ID="HidChannelName" Value='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidChannelCode" Value='<%# DataBinder.Eval(Container.DataItem, "ChannelCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidStatusActive" Value='<%# DataBinder.Eval(Container.DataItem, "StatusActive")%>' />
                                                            <asp:HiddenField runat="server" ID="HidPlannerCode" Value='<%# DataBinder.Eval(Container.DataItem, "PlannerCode")%>' />
                                                            <asp:HiddenField runat="server" ID="HidPlannerName" Value='<%# DataBinder.Eval(Container.DataItem, "PlannerName")%>' />
                                                            <asp:HiddenField runat="server" ID="HidPlannerProgramName" Value='<%# DataBinder.Eval(Container.DataItem, "PlannerProgramName")%>' />
                                                                   <asp:HiddenField runat="server" ID="HidPlannerDate" Value='<%# DataBinder.Eval(Container.DataItem, "PlannerDate")%>' />
                                                            <asp:HiddenField runat="server" ID="HidPlannerTime" Value='<%# DataBinder.Eval(Container.DataItem, "PlannerTime")%>' />
                                                            <asp:HiddenField runat="server" ID="HidPlannerDuration" Value='<%# DataBinder.Eval(Container.DataItem, "PlannerDuration")%>' />
                                                            <asp:HiddenField runat="server" ID="HidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                                            <asp:HiddenField runat="server" ID="HidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                              <asp:HiddenField runat="server" ID="HidSaleChannelCode" Value='<%# DataBinder.Eval(Container.DataItem, "SaleChannelCode")%>' />

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

    <div class="modal fade" id="modal-Planner" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                                 <label class="col-sm-4 col-form-label">รหัส Planner</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtPlannerCode_ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lbPlannerCode_ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                 <label class="col-sm-4 col-form-label">ชื่อ Planner</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtPlannername_ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblPlannername_ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                <label class="col-sm-4 col-form-label">Channel</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlChannel_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblChannel_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                    <label class="col-sm-4 col-form-label">Sale Channel</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSaleChannel_Ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="Label1" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                 <label class="col-sm-4 col-form-label">รหัสแคมเปญ</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlcamp_ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblddlcamp_ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                   <label class="col-sm-4 col-form-label">โปรโมชั่น</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlpromotion_ins" name="select" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblddlpromotion_ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                <label class="col-sm-4 col-form-label">ชื่อรายการ</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtProgramname_ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblProgramname_ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                    <label class="col-sm-4 col-form-label">ระยะเวลา</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtDuration_Ins" name="select" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblDuration_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                    <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">วัน</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtdateplanner_ins" class="form-control" placeholder="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateplanner_ins" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:Label ID="lbldateplanner_ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                                <label class="col-sm-4 col-form-label">เวลา</label>
                                <div class="col-sm-6" >
                                       <div class="input-group mb-0">
                                     <asp:DropDownList ID="ddlHourplanner_ins" runat="server" class="form-control">
                                    </asp:DropDownList> : 
                                    <asp:DropDownList ID="ddltimeplanner_ins" runat="server" class="form-control">
                                    </asp:DropDownList>
                                  
                                    <asp:Label ID="lbltimeplanner_ins" runat="server" CssClass="validation"></asp:Label>
                                           </div>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="ProductManagement.aspx.cs" Inherits="DOMS_TSR.src.BranchManagement.ProductManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
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

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function ActiveConfirm() {

            var grid = document.getElementById("<%= gvBranchMapProduct.ClientID %>");

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

                alert("กรุณาเลือกรายการที่จะเปิดการขาย");

                return false;

            } else {

                //var MsgDelete = document.getElementById("<%=hidMsgDel.ClientID%>").value;
                var MsgDelete = "คุณแน่ใจที่จะเปิดการขายสินค้า ?";

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
         function InActiveConfirm() {

            var grid = document.getElementById("<%= gvBranchMapProduct.ClientID %>");

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

                alert("กรุณาเลือกรายการที่จะปิดการขาย");

                return false;

            } else {

                //var MsgDelete = document.getElementById("<%=hidMsgDel.ClientID%>").value;
                var MsgDelete = "คุณแน่ใจที่จะปิดการขายสินค้า ?";

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
    <asp:HiddenField ID="hidMsgDel" runat="server" />

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
                                <div class="sub-title">ค้นหาข้อมูลการสั่งซื้อ</div>
                            </div>

                            <div class="card-body">

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-1"></div>

                                    <label class="col-sm-2 col-form-label">สถานะการขาย</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchActive" runat="server" class="form-control">
                                            <asp:ListItem Text="---- กรุณาเลือก ----" Value="-99"></asp:ListItem>
                                            <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="form-group row">

                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า หรือส่วนประกอบ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList Visible="false" ID="ddlSearchRecipe" runat="server" class="form-control"></asp:DropDownList>
                                        <div class="input-group mb-0">
                                            <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-1"></div>


                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">

                                <input type="hidden" id="hidIdList" runat="server" />

                                <div class="m-b-10">
                                                <!--Start modal Add Promotion-->
                                                <asp:LinkButton ID="btnSetActive" class="button-action button-add m-r-10" OnClientClick="return ActiveConfirm();"
                                                    OnClick="btnSetActive_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เปิด</asp:LinkButton>
                                                
                                                <asp:LinkButton ID="btnSetInActive" OnClick="btnSetInActive_Click" OnClientClick="return InActiveConfirm();"
                                                    class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ปิด</asp:LinkButton>
                                            </div>

                                <asp:Panel ID="Panel" runat="server" Style="overflow-x: scroll;">
                                    <asp:GridView ID="gvBranchMapProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                        TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowCommand="gvBranchMapProduct_RowCommand"
                                        OnRowDataBound="gvBranchMapProduct_RowDataBound">

                                        <Columns>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                            <asp:CheckBox ID="chkProductAll" OnCheckedChanged="chkProductAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkProduct" runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="Center">รหัสสินค้า</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' CommandName="ShowProduct" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="Center">ชื่อสินค้า</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="Center">สถานะการขาย</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblActiveStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Active")%>' runat="server" />--%>
                                                    <div class="form-group">
                                                        <label class="custom-switch mt-2">
                                                            <asp:Button runat="server" ID="hidChkbox" CommandName="SetActiveStatus" Style="display: none" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <input type="checkbox" id="hidSwitchChkBox" name="custom-switch-checkbox" class="custom-switch-input" value='<%# DataBinder.Eval(Container.DataItem, "BranchMapProductId")%>' runat="server" onclick="chkActive_CheckedChanged" />
                                                            <%--<asp:CheckBox  class="custom-switch-input" OnCheckedChanged="chkActive_CheckedChanged" ID="chkActive" runat="server" />--%>
                                                            <%--<asp:LinkButton runat="server"  class="custom-switch-input"  CommandName="SetActive"/>--%>
                                                            <span class="custom-switch-indicator"></span>
                                                        </label>
                                                    </div>

                                                    <asp:HiddenField runat="server" ID="hidBranchMapProductId" Value='<%# DataBinder.Eval(Container.DataItem, "BranchMapProductId")%>' />
                                                    <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                                    <asp:HiddenField runat="server" ID="hidActive" Value='<%# DataBinder.Eval(Container.DataItem, "Active")%>' />
                                                    <asp:HiddenField runat="server" ID="hidActiveCancelProduct" Value='<%# DataBinder.Eval(Container.DataItem, "ActiveCancelProduct")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="Center">เวลาปิดการขาย</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdateTime" Text='<%# ((null == Eval("UpdateDate"))||("" == Eval("UpdateDate"))) ? string.Empty : DateTime.Parse(Eval("UpdateDate").ToString()).ToString("HH:mm:ss") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <%--<div align="Center"></div>--%>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblActiveCancelStatus" Style="color:red;" Text='<%# DataBinder.Eval(Container.DataItem, "ActiveCancelProduct")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <EmptyDataTemplate>
                                            <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>

                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
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


                    <div class="card col-sm-12">
                    </div>

                </div>
            </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="modal-Product" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 650px">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">จัดการสินค้า</div>

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
                                <label class="col-sm-4 col-form-label">รหัสสินค้า</label>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblProductCode_Ins" runat="server"></asp:Label>

                                </div>

                                <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">ชื่อสินค้า</label>
                                <div class="col-sm-6">

                                    <asp:Label ID="lblProductName_Ins" runat="server"></asp:Label>

                                </div>
                                <label class="col-sm-2 col-form-label"></label>
                                <label class="col-sm-4 col-form-label">เปิด-ปิดการขายถาวร</label>
                                <div class="col-sm-6">
                                        <label class="custom-switch mt-2">
                                    <div class="form-group">
                                            <asp:Button ID="btnActive"  OnClick="hidBtnSetActiveClose_Click" Style="display: none" runat="server"/>
                                            <input type="checkbox" id="hidSwitchChkBox_Ins" name="custom-switch-checkbox" class="custom-switch-input" runat="server" onclick="hidBtnSetActiveClose_Click" />
                                            <%--<asp:CheckBox  class="custom-switch-input" OnCheckedChanged="chkActive_CheckedChanged" ID="chkActive" runat="server" />--%>
                                            <%--<asp:LinkButton runat="server"  class="custom-switch-input"  CommandName="SetActive"/>--%>
                                            <%--<asp:LinkButton runat="server" ID="hidBtnSetActiveClose" Style="display: none" OnClick="hidBtnSetActiveClose_Click" />--%>
                                            <span class="custom-switch-indicator"></span>
                                        </label>
                                    </div>

                                </div>
                                                               
                                <input type="hidden" id="hidActiveCancelFlag_Ins" runat="server" />
                                <input type="hidden" id="hidBranchMapProductId_Ins" runat="server" />
                             
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--  <div class="form-group row">
                                  <label class="col-sm-4 col-form-label">รูปภาพ</label>
                            <div class="col-sm-8">
                                <input type="file" name="files[]" id="filer_input1" multiple="multiple">
                             </div>
                        </div>

                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="btnSubmit" Text="สร้าง" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Click" runat="server" />
                        <asp:Button type="button" ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click" class="button-pri button-cancel m-r-10" runat="server" />
                    </div>--%>
                </div>

            </div>
        </div>
    </div>


</asp:Content>

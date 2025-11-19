<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="Driver.aspx.cs" Inherits="DOMS_TSR.src.Driver.Driver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvDriver.ClientID %>");

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

            }else {

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <input type="hidden" id="hidIdList" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">Search Driver</div>
                            </div>

                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัส</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchDriverCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ชื่อ</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchDriverFName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">นามสกุล</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchDriverLName" class="form-control" runat="server"></asp:TextBox>
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

                        <div class="card">
                            <div class="card-body">
                                <div class="m-b-10">
                                    <asp:LinkButton ID="btnAddDriver" class="button-action button-add m-r-5"
                                        OnClick="btnAddDriver_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                       class="button-action button-delete"  runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                </div>

                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvDriver" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                        TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvDriver_RowCommand"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <center>
                                            <asp:CheckBox ID="chkDriverAll" OnCheckedChanged="chkDriverAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <asp:CheckBox ID="chkDriver" runat="server" />

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">Driver No.</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# GetLink(DataBinder.Eval(Container.DataItem, "Driver_No")) %>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">Driver Name</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDriverName" Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>' runat="server" />
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                <HeaderTemplate>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowProduct"
                                                        class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                    <asp:HiddenField runat="server" ID="hidDriverId" Value='<%# DataBinder.Eval(Container.DataItem, "DriverId")%>' />
                                                    <asp:HiddenField runat="server" ID="hidDriverNo" Value='<%# DataBinder.Eval(Container.DataItem, "Driver_No")%>' />
                                                    <asp:HiddenField runat="server" ID="hidTitle" Value='<%# DataBinder.Eval(Container.DataItem, "TitleCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidFName" Value='<%# DataBinder.Eval(Container.DataItem, "FName")%>' />
                                                    <asp:HiddenField runat="server" ID="hidLName" Value='<%# DataBinder.Eval(Container.DataItem, "LName")%>' />
                                                    <asp:HiddenField runat="server" ID="hidGender" Value='<%# DataBinder.Eval(Container.DataItem, "Gender")%>' />
                                                    <asp:HiddenField runat="server" ID="hidMobile" Value='<%# DataBinder.Eval(Container.DataItem, "Mobile")%>' />
                                                    
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
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="modal-driver">
        <div class="modal-dialog modal-lg" style="max-width: 650px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2 ">
                            <div class="col-sm-11">
                                <div id="exampleModalLongTitle">
                                    Add Driver
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card-block">
                                <asp:UpdatePanel ID="upModal" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">รหัสพนักงาน</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtDriverNo_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblDriverNo_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidDriverNo_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                       
                                            <label class="col-sm-4 col-form-label">คำนำหน้า</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlTitleName_Ins" runat="server" class="form-control"></asp:DropDownList> 
                                                <asp:Label ID="lblTitleName_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">ชื่อ</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtFName_Ins" runat="server" class="form-control"></asp:TextBox>

                                                <asp:Label ID="lblFName_Ins" runat="server" CssClass="validation"></asp:Label>


                                            </div>

                                         
                                            <label class="col-sm-4 col-form-label">นามสกุล</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtLName_Ins" runat="server" class="form-control"></asp:TextBox>

                                                <asp:Label ID="lblLName_Ins" runat="server" CssClass="validation"></asp:Label>

                                            </div>
                                 
                                            <label class="col-sm-4 col-form-label">เบอร์โทรศัพท์</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtTel_Ins" runat="server" class="form-control"></asp:TextBox>


                                                <asp:Label ID="lblTel_Ins" runat="server" CssClass="validation"></asp:Label>

                                            </div>

                                        </div>



                                        <div class="text-center m-t-20 center">

                                            <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                                  class="button-pri button-accept m-r-10"
                                                runat="server" />
                                            <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                               class="button-pri button-cancel"
                                                runat="server" />

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

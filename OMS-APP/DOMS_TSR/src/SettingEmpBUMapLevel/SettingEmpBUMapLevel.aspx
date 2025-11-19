<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="SettingEmpBUMapLevel.aspx.cs" Inherits="DOMS_TSR.src.SettingEmpBUMapLevel.SettingEmpBUMapLevel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
         $('#modal-empmapbu').on('shown.bs.modal', function () {
             $('.chosen-select', this).chosen();
              $('.chosen-select1', this).chosen();
        });
    });
    </script>

    <script type="text/javascript">
        function DeleteConfirm() {
            var grid = document.getElementById("<%= gvEmpMapBu.ClientID %>");
            var cell;
            var sum = 0;
            if (grid.rows.length > 0) {
                for (i = 1; i < grid.rows.length; i++) {
                    cell = grid.rows[i].cells[0];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == "checkbox") {
                            if (cell.childNodes[j].checked == true) {
                                sum++;
                            }
                        }
                    }
                }
            }

            if (sum == 0) {
                alert("กรุณาเลือกรายการที่จะลบ");
                return false;
            }

            else
            {
                var MsgDelete = "คุณแน่ใจที่จะลบข้อมูลนี้ ?";
                if (confirm(MsgDelete)) {
                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "TRUE";
                    return true;
                }

                else
                {
                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "FALSE";
                    return false;
                }
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
            {
                return false;
            }
            return true;
        }

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
   
        //function check_char(w) {
        //    if (!w.match(/^[ก-๙a-zA-Z0-9/ ]*$/)) {
        //        alert('ไม่สามารถใช้ตัวอักษรพิเศษได้');
        //        document.getElementById("txtInventoryCode_Ins").value = '';
        //    }
        //}
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidMerCode" runat="server"/>
            <asp:HiddenField ID="hidEmpMapRoleIdforUpdate" runat="server"/>

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูลคลังสินค้า</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสพนักงาน</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchEmpCodeCode" class="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSearchEmpCodeCode" runat="server" CssClass="validatecolor"></asp:Label>
                                    </div>
                                
                                    <label class="col-sm-2 col-form-label">Role</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchRole" class="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSearchRole" runat="server" CssClass="validatecolor"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Level</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchLevels" class="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSearchLevels" runat="server" style="color:red" CssClass="validatecolor"></asp:Label>
                                    </div>
                                
                                    <label class="col-sm-2 col-form-label"></label>
                                    <div class="col-sm-4">
                                        
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="m-b-10">
                                    <asp:LinkButton ID="btnAddEmpBUMapLevel" class="button-action button-add" data-backdrop="false" OnClick="btnAddEmpBUMapLevel_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click"  OnClientClick="return DeleteConfirm()" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                </div>

                                <asp:GridView ID="gvEmpMapBu" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvEmpMapBu_RowCommand" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                <asp:CheckBox ID="chkEmpMapBuAll" OnCheckedChanged="chkEmpMapBuAll_Change" AutoPostBack="true" runat="server"  />
                                            </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmpMapBu" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">รหัสพนักงาน</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ชื่อสกุลพนักงาน</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">BU</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBu" Text='<%# DataBinder.Eval(Container.DataItem, "Bu")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Role</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRole" Text='<%# DataBinder.Eval(Container.DataItem, "Role")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">Level</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLevels" Text='<%# DataBinder.Eval(Container.DataItem, "Levels")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowInventory" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidEmpMapBuId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpMapBuId")%>' />
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
<div class="m-t-10">
                                <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First" Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous" Text="<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>
                                                        of
                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnNext" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnLast" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last" Text=">>" OnCommand="GetPageIndex"></asp:Button>
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

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-empmapbu">
        <div class="modal-dialog modal-lg" style="max-width: 600px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2   ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">สร้างข้อมูลคลังสินค้า</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>

                <div class="modal-body">
                   

                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">รหัสพนักงาน<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlEmpCode_Ins" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpCode_Ins_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblEmpCode_Ins" runat="server" style="color:red" CssClass="validatecolor"></asp:Label>
                                                <asp:HiddenField ID="hidEmpCode_Ins" runat="server"></asp:HiddenField>
                                                <asp:HiddenField runat="server" ID="hidEmpCodeImgId" />
                                            </div>

                                            <label class="col-sm-4 col-form-label">Bu<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBu_Ins" runat="server" ReadOnly ="true" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblBu_Ins" runat="server" style="color:red" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">Role<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlRole_Ins" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                                                <asp:Label ID="lblRole_Ins" runat="server" style="color:red" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">Level<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtLevels_Ins" runat="server" class="form-control" onkeypress="return validatenumerics(event);"></asp:TextBox>
                                                <asp:Label ID="lblLevels_Ins" runat="server" style="color:red" CssClass="validatecolor"></asp:Label>
                                            </div>                                          
                                        </div>

                                        <div class="text-center m-t-20 center">
                                    <asp:Button ID="btnSubmit" Text="บันทึก" OnClick="btnSubmit_Click" class="button-pri button-accept m-r-10" runat="server"/>
                                    <asp:Button ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click" class="button-pri button-cancel" runat="server" />
                                </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                
                
                </div>
            </div>
        </div>
    </div>
</asp:Content>

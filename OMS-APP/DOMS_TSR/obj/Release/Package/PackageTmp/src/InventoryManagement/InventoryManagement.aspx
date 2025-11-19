<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="InventoryManagement.aspx.cs" Inherits="DOMS_TSR.src.InventoryManagement.InventoryManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
         $('#modal-inventory').on('shown.bs.modal', function () {
             $('.chosen-select', this).chosen();
              $('.chosen-select1', this).chosen();
        });
    });
    </script>

    <script type="text/javascript">
        function DeleteConfirm() {
            var grid = document.getElementById("<%= gvInventory.ClientID %>");
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

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูลคลังสินค้า</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสคลังสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchInventoryCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">ชื่อคลังสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchInventoryName" class="form-control" runat="server"></asp:TextBox>
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
                                    <asp:LinkButton ID="btnAddInventory" class="button-action button-add" data-backdrop="false" OnClick="btnAddInventory_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click"  OnClientClick="return DeleteConfirm()" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                </div>

                                <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvInventory_RowCommand" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                <asp:CheckBox ID="chkInventoryAll" OnCheckedChanged="chkInventoryAll_Change" AutoPostBack="true" runat="server"  />
                                            </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkInventory" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">รหัสคลังสินค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "InventoryCode")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">ชื่อคลังสินค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventoryName" Text='<%# DataBinder.Eval(Container.DataItem, "InventoryName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                            <ItemTemplate>
                                                <%# GetLink2(DataBinder.Eval(Container.DataItem, "InventoryCode")) %>
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowInventory" class="button-activity" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidInventoryId" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryId")%>' />
                                                <asp:HiddenField runat="server" ID="hidInventoryCode" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidInventoryName" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryName")%>' />
                                                <asp:HiddenField runat="server" ID="hidAddress" Value='<%# DataBinder.Eval(Container.DataItem, "Address")%>' />
                                                <asp:HiddenField runat="server" ID="hidProvince" Value='<%# DataBinder.Eval(Container.DataItem, "Province")%>' />
                                                <asp:HiddenField runat="server" ID="hidDistrict" Value='<%# DataBinder.Eval(Container.DataItem, "District")%>' />
                                                <asp:HiddenField runat="server" ID="hidSubDistrict" Value='<%# DataBinder.Eval(Container.DataItem, "SubDistrict")%>' />
                                                <asp:HiddenField runat="server" ID="hidPostCode" Value='<%# DataBinder.Eval(Container.DataItem, "PostCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidContactTel" Value='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' />
                                                <asp:HiddenField runat="server" ID="hidFax" Value='<%# DataBinder.Eval(Container.DataItem, "Fax")%>' />
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

                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button" ToolTip="First" CommandName="First" Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous" Text="<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>
                                                        of
                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last" Text=">>" OnCommand="GetPageIndex"></asp:Button>
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

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-inventory">
        <div class="modal-dialog modal-lg" style="max-width: 600px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card-block">

                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">รหัสคลังสินค้า</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtInventoryCode_Ins" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:Label ID="lblInventoryCode_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                                <asp:HiddenField ID="hidInventoryCode_Ins" runat="server"></asp:HiddenField>
                                                <asp:HiddenField runat="server" ID="hidInventoryImgId" />
                                            </div>

                                            <label class="col-sm-4 col-form-label">ชื่อคลังสินค้า</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtInventoryName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblInventoryName_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">ที่อยู่</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAddress_Ins" runat="server" class="form-control" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                                <asp:Label ID="lblAddress_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">จังหวัด</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProvince_Ins" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblProvince_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">เขต</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlDistrict_Ins" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">แขวง</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSubDistrict_Ins" runat="server" class="form-control"></asp:DropDownList>
                                            </div>

                                            <label class="col-sm-4 col-form-label">รหัสไปรษณีย์</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPostCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPostCode_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">เบอร์โทรศัพท์</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtContactTel_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblContactTel_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">Fax.</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFax_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblFax_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="text-center m-t-20 center">
                                    <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="button-pri button-accept m-r-10" runat="server"/>
                                    <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" class="button-pri button-cancel" runat="server" />
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

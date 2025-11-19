<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="VatManagement.aspx.cs" Inherits="DOMS_TSR.src.VatManagement" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .validation {
            color: red
        }
    </style>

    <script type="text/javascript">
        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvVat.ClientID %>");

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
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <input type="hidden" id="hidIdList" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidVatId" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <div class="page-body">
                <div class="row">
                    <div class="col-12">

                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Vat Management</div>
                            </div>

                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Vat Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtVatCode" CssClass="form-control" runat="server" onkeypress="return validatetext(event);"></asp:TextBox>
                                        <asp:Label ID="lblVatCode" runat="server" CssClass="validation"></asp:Label>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Vat Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtVatName" CssClass="form-control" runat="server" onkeypress="return validatetext(event);"></asp:TextBox>
                                        <asp:Label ID="lblVatName" runat="server" CssClass="validation"></asp:Label>
                                    </div>

                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Vat Value</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtVatValue" CssClass="form-control" runat="server" onkeypress="return validatetext(event);"></asp:TextBox>
                                        <asp:Label ID="lblVatValue" runat="server" CssClass="validation"></asp:Label>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Status</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlActive" runat="server" class="form-control">
                                            <asp:ListItem Value="-99"> Please Select------------------------------- </asp:ListItem>
                                            <asp:ListItem Value="Y"> Active </asp:ListItem>
                                            <asp:ListItem Value="N"> Inactive </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblActive" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button OnClick="btnSearch_Click" ID="btnSearch" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>

                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <div class="m-b-10">
                                            <!--Start modal Add Vat-->
                                            <asp:LinkButton ID="btnAddVat" class="button-action button-add" data-backdrop="false" OnClick="btnAddVat_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmGV();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                        </div>

                                        <asp:GridView ID="gvVat" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                            <Columns>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                                            <asp:CheckBox ID="chkVatAll" OnCheckedChanged="chkVatAll_Change" AutoPostBack="true" runat="server"/>
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkVat" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสภาษีมูลค่าเพิ่ม</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVatCode_gv" Text='<%# DataBinder.Eval(Container.DataItem, "VatCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อภาษีมูลค่าเพิ่ม</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVatName_gv" Text='<%# DataBinder.Eval(Container.DataItem, "VatName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">อัตราภาษีมูลค่าเพิ่ม</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVatValue_gv" Text='<%# DataBinder.Eval(Container.DataItem, "VatValue")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">สถานะ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblStatus_gv" Text='<%# DataBinder.Eval(Container.DataItem, "FlagActive")%>' runat="server" />--%>
                                                        <asp:DropDownList SelectedValue='<%# DataBinder.Eval(Container.DataItem, "FlagActive")%>' ID="ddlActive" runat="server" class="form-control">
                                                            <asp:ListItem Value="-99"> Please Select------------------------------- </asp:ListItem>
                                                            <asp:ListItem Value="Y"> Active </asp:ListItem>
                                                            <asp:ListItem Value="N"> Inactive </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton
                                                            ID="btnEdit"
                                                            runat="Server"
                                                            CommandName="ShowCustomer"
                                                            class="button-activity button-action m-r-10"
                                                            Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> 
                                                            <span class="icofont icofont-ui-edit f-16"></span>
                                                        </asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidVatId" Value='<%# DataBinder.Eval(Container.DataItem, "VatId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidVatCode" Value='<%# DataBinder.Eval(Container.DataItem, "VatCode")%>' />

                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                            <tr height="30" bgcolor="#ffffff">
                                                <td width="100%" align="right" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                        <tr>
                                                            <td style="font-size: 8.5pt;"></td>
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
                                                                <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last" Text=">>"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-vat">
        <div class="modal-dialog modal-lg" style="max-width: 600px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2 p-l-0">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มภาษีมูลค่าเพิ่ม</div>
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
                                            <label class="col-sm-4 col-form-label">รหัสภาษีมูลค่าเพิ่ม</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtVatCode_Ins" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:Label ID="lblVatCode_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                                <asp:HiddenField ID="hidVatCode_Ins" runat="server"></asp:HiddenField>
                                                <asp:HiddenField runat="server" ID="hidVatImgId" />
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">ชื่อภาาษีมูลค่าเพิ่ม</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtVatName_Ins" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:Label ID="lblVatName_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">อัตราภาษามูลค่าเพิ่ม (%)</label>
                                            <div class="col-sm-8">

                                                <asp:TextBox ID="txtVatValue_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblVatValue_Ins" runat="server" CssClass="validatecolor"></asp:Label>

                                            </div>
                                        </div>
                                      
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">สถานะ</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlActive_Ins" runat="server" class="form-control">
                                                    <asp:ListItem Value="-99"> Please Select------------------------------- </asp:ListItem>
                                                    <asp:ListItem Value="Y"> Active </asp:ListItem>
                                                    <asp:ListItem Value="N"> Inactive </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="text-center m-t-20 center">

                                    <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                        class="button-pri button-cancel"
                                        runat="server" />

                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="InventoryGoodsInTransit.aspx.cs" Inherits="DOMS_TSR.src.InventoryManagement.InventoryGoodsInTransit" %>

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
            var grid = document.getElementById("<%= gvInventoryDetailStart.ClientID %>");
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
                alert("กรุณาเลือกรายการ");
                return false;
            }

            else
            {
                var MsgDelete = "คุณแน่ใจที่จะเพิ่มข้อมูลนี้ ?";
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
            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">warehouse information</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">warehouse</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlinvStart" runat="server" CssClass="form-control"></asp:DropDownList>
                                       
                                    </div>

                                 

                                    <label class="col-sm-2 col-form-label">to the warehouse</label>
                                    <div class="col-sm-4">
                                         <asp:DropDownList ID="ddlinvTo" runat="server" CssClass="form-control"></asp:DropDownList>
                                       
                                    </div>
                                

                                </div>

                                <div class="text-center m-t-20 col-sm-12" >
                                    <asp:Button ID="btnSearch" Text="List" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click" Visible="false" class="button-pri button-cancel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                
                                <div class="m-b-10" style="display:none" >
                                    <asp:LinkButton ID="btnAddInventory" class="button-action button-add" data-backdrop="false" OnClick="btnAddInventory_Click" runat="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click"  OnClientClick="return DeleteConfirm()" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                                </div>
                                 <div class="form-group row">
                                     <div class="col-sm-6">
                                           <asp:GridView ID="gvInventoryDetailStart" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvInventoryDetailStart_RowDataBound">
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
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Product code</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Product name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">On Hand</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOnhand" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hidInventoryDetailID" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryDetailID")%>' />
                                                <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidQTY" Value='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' />                                                                                  
                                                <asp:HiddenField runat="server" ID="hidProductImportDup" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCodeImportDup")%>' />
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">QTY Move</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXTQTY_MOVE" runat="server" TextMode="Number" Text="0"></asp:TextBox>
                                               
                                                </ItemTemplate>
                                        </asp:TemplateField>

                                 
                                        
                                   
                                        
                                    </Columns>
                                </asp:GridView>
                                     </div>
                                          <div class="col-sm-6">
                                              <asp:GridView ID="gvInventoryDetailTo" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" >
                                    <Columns>
                                
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Product code</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Product name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">On Hand</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOnhand" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                              
                                                </ItemTemplate>
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">On Hand</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" runat="server" />
                                                <asp:Label ID="lblOnhand" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                              
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                          </div>

                                     <div class="col-sm-12">
                                           <asp:Button type="button" ID="btnSubmitImport"  Text="Move QTY" OnClick="btnSubmitImport_Click"   OnClientClick="return DeleteConfirm()" class="button-pri button-accept m-r-10" runat="server" />
                                     </div>
                                 </div>
                               
                           
<div class="m-t-10">
                                <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff" style="display:none">
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

                                                 <div class="text-center m-t-20 col-sm-12">

                                </div>
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
        aria-hidden="true" id="modal-inventory">
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
                                            <label class="col-sm-4 col-form-label">Product code<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtInventoryCode_Ins" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:Label ID="lblInventoryCode_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                                <asp:HiddenField ID="hidInventoryCode_Ins" runat="server"></asp:HiddenField>
                                                <asp:HiddenField runat="server" ID="hidInventoryImgId" />
                                            </div>

                                            <label class="col-sm-4 col-form-label">Product name<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtInventoryName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblInventoryName_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">Address<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAddress_Ins" runat="server" class="form-control" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                                <asp:Label ID="lblAddress_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">Province<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProvince_Ins" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblProvince_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">district/district<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlDistrict_Ins" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblDistrict_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">Sub-district/Subdistrict<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSubDistrict_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblSubDistrict_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">Post code<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPostCode_Ins" runat="server" class="form-control" onkeypress="return validatenumerics(event);"></asp:TextBox>
                                                <asp:Label ID="lblPostCode_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">telephone number<span style="color: red; background-position: right top;">*</span></label>
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
                                 
                                </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                
                
                </div>

                                  

            </div>
                                                              

        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="Vehicle.aspx.cs" Inherits="DOMS_TSR.src.Vehicle.Vehicle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvVehicle.ClientID %>");

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

            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">

                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูลรถขนส่ง </div>
                            </div>

                            <div class="card-body">

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ทะเบียนรถ</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchLicensePlate" class="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSearchLicensePlate" runat="server" CssClass="validatecolor"></asp:Label>
                                    </div>

                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">ยี่ห้อ</label>
                                    <div class="col-sm-3">
                                             <asp:TextBox ID="TxtSearchVehicleBrand" class="form-control" runat="server"></asp:TextBox>
                                             <asp:Label ID="lblSearchVehicleBrand" runat="server" CssClass="validatecolor"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchVehicleBrand" runat="server" class="form-control" Visible="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ประเภทรถ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchVehicleType" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                     <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">สถานะ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchlActive" class="form-control" runat="server"></asp:DropDownList>

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
                                    <asp:LinkButton ID="btnAddVehicle" class="button-action button-add m-r-5"
                                        OnClick="btnAddVehicle_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                       class="button-action button-delete"  runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                </div>

                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvVehicle" runat="server" AutoGenerateColumns="False"  CssClass="table-p-stand"
                                        TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvVehicle_RowCommand"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <center>
                                            <asp:CheckBox ID="chkVehicleAll" OnCheckedChanged="chkVehicleAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVehicle" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">ทะเบียนรถ</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                     <asp:Label ID="lblVechicle_No" Text='<%# DataBinder.Eval(Container.DataItem, "Vechicle_No")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                               <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div>ยี่ห้อ</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleBrand" Text='<%# DataBinder.Eval(Container.DataItem, "Name_Band")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px"  ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div>รุ่น</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                     <asp:Label ID="lblVechicle_Model" Text='<%# DataBinder.Eval(Container.DataItem, "Vechicle_Model")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
         


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"  >
                                                <HeaderTemplate>
                                                    <div >ประเภทรถ</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleType" Text='<%# DataBinder.Eval(Container.DataItem, "Name_TypeCar")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                  <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"  >
                                                <HeaderTemplate>
                                                    <div >สถานะ</div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                       <asp:Label ID="lblactive" Text= '<%# (((string)DataBinder.Eval(Container.DataItem,"Active")) == "Y") ? ("Active") : ("Inactive" )%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                <HeaderTemplate>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowVehicle"
                                                        class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="button-pri button-activity m-r-10 icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                    <asp:HiddenField runat="server" ID="hidVehicleId" Value='<%# DataBinder.Eval(Container.DataItem, "VechicleId")%>' />
                                                    <asp:HiddenField runat="server" ID="hidVehicle_No" Value='<%# DataBinder.Eval(Container.DataItem, "Vechicle_No")%>' />
                                                    <asp:HiddenField runat="server" ID="hidVehicle_Band" Value='<%# DataBinder.Eval(Container.DataItem, "Vechicle_Band")%>' />
                                                    <asp:HiddenField runat="server" ID="hidVehicle_Type" Value='<%# DataBinder.Eval(Container.DataItem, "Vechicle_Lookup")%>' />
                                                    <asp:HiddenField runat="server" ID="HidVechicle_Model" Value='<%# DataBinder.Eval(Container.DataItem, "Vechicle_Model")%>' />
                                                    <asp:HiddenField runat="server" ID="HidActive" Value='<%# DataBinder.Eval(Container.DataItem, "Active")%>' />
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

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="modal-vehicle">
        <div class="modal-dialog modal-lg" style="max-width: 650px;">
            <div class="modal-content">
              <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มรถขนส่ง</div>

                            </div>
                            <span>
                                <button type="button" class="close  " style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
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
                                <asp:UpdatePanel ID="upModal" runat="server">
                                    <ContentTemplate>

                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">ทะเบียนรถ</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtLicensePlate_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblLicensePlate_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidLicensePlate_Ins" runat="server"></asp:HiddenField>
                                            </div>
                                                
                                            <label class="col-sm-4 col-form-label">ยี่ห้อ</label>
                                            <div class="col-sm-6">
                                                 <asp:TextBox ID="txtVehicleBrand_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <%--<asp:DropDownList ID="ddlVehicleBrand_Ins" runat="server" class="form-control"></asp:DropDownList>--%> 
                                                <asp:Label ID="lblVehicleBrand_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                               <label class="col-sm-4 col-form-label">รุ่น</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtModel_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="LbModel_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="HidModel_Ins" runat="server"></asp:HiddenField>
                                            </div>
                                        
                                            <label class="col-sm-4 col-form-label">ประเภทรถ</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlVehicleType_Ins" runat="server" class="form-control"></asp:DropDownList> 
                                                <asp:Label ID="lblVehicleType_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                                 <label class="col-sm-4 col-form-label">สถานะ</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlActive_Ins" runat="server" class="form-control"></asp:DropDownList> 
                                                <asp:Label ID="lbActive_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="text-center m-t-20 center">

                                            <asp:Button ID="btnSubmit" Text="บันทึก" OnClick="btnSubmit_Click"
                                               class="button-pri button-accept m-r-10 "
                                                runat="server" />
                                            <asp:Button ID="btnCancel" Text="ยกเลิก" OnClick="btnCancel_Click"
                                         class="button-pri button-cancel m-r-10"
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

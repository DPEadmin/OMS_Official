<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="DOMS_TSR.src.UserManagement.UserDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .hideText {
            width: 20rem;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .validation {
            color: red;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="http://harvesthq.github.io/chosen/chosen.css">
    <script type="text/javascript" src="http://harvesthq.github.io/chosen/chosen.jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#modal-product').on('shown.bs.modal', function () {
                $('.chosen-select', this).chosen();
                $('.chosen-select1', this).chosen();
            });
        });
    </script>
    <script type="text/javascript">
        function DeleteConfirm() {
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

        function DeleteConfirmGV() {

            var grid = document.getElementById("<%= gvEmpRole.ClientID %>");

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

    <div class="page-body">
        <div class="col-sm-12">
            <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidSupplierIdIns" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <!-- Basic Form Inputs card start -->
            <div class="card">
                <div class="card-header">
                    <div class="sub-title">รายละเอียดผู้ใช้งาน <span style="float: right;"><a class="  button-pri button-accept m-r-10" style=" color: #fff;" href="User.aspx">กลับ</a> </span>   
                    </div>

                </div>
                <div class="card-block">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                         


                                        <div class="form-group row">

                                            <label class="col-sm-2 col-form-label">รหัสพนักงาน</label>
                                            <div class="col-sm-4 pcontrol">
                                                <asp:HiddenField ID="hidEmpIdIns" runat="server"></asp:HiddenField>

                                                <asp:Label ID="lblEmpCodeIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-2 col-form-label">BU</label>
                                            <div class="col-sm-4 pcontrol">
                                                <asp:Label ID="lblBUIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>
                                        
                                            <label class="col-sm-2 col-form-label">ชื่อ</label>
                                            <div class="col-sm-4 pcontrol">
                                                <asp:Label ID="lblEmpFNameTHIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-2 col-form-label">นามสกุล</label>
                                            <div class="col-sm-4 pcontrol">
                                                <asp:Label ID="lblEmpLNameTHIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>
                                       
                                            <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์</label>
                                            <div class="col-sm-4 pcontrol">
                                                <asp:Label ID="lblMobileIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-2 col-form-label">อีเมล์</label>
                                            <div class="col-sm-4 pcontrol">
                                                <asp:Label ID="lblEmailIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>
                                     
                                            <label class="col-sm-2 col-form-label">Extension ID</label>
                                            <div class="col-sm-4 pcontrol">
                                                <asp:Label ID="lblExtensionid" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>

                                        </div>



                             


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <div class="card-group">
                            <div class="card" id="cardex1" runat="server">
                                <asp:LinkButton CssClass="btn-8bar-disable" ID="showSection_UserLogin" title="Hello World!" OnClick="showSection_UserLogin_Click" runat="server">
                                    <div id="listcard1" runat="server">
                                        <div class="row">
                                            <div class="col-3 text-left p-b-15">
                                                <i class=" ti-key text-c-blue f-30"></i>
                                            </div>
                                            <div class="col-9 text-right">
                                                <h3 class="text-c-blue">
                                                    <asp:Label ID="countSection_UserLogin" runat="server"></asp:Label></h3>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 text-left">
                                                <p class=" m-0">User Login</p>
                                            </div>
                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>

                            <div class="card" id="cardex2" runat="server">
                                <asp:LinkButton CssClass="btn-8bar-disable2" ID="showSection_Role" OnClick="showSection_Role_Click" runat="server">
                                    <div id="listcard2" runat="server">
                                        <div class="row">
                                            <div class="col-3 text-left p-b-15">
                                                <i class=" ti-user text-c-blue f-30"></i>
                                            </div>
                                            <div class="col-9 text-right">
                                                <h3 class="text-c-blue">
                                                    <asp:Label ID="countSection_Role" runat="server"></asp:Label></h3>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 text-left">
                                                <p class=" m-0">Role</p>
                                            </div>

                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                            <div class="card" id="Div1" runat="server">
                                <asp:LinkButton CssClass="btn-8bar-disable3" ID="showSection_Merchant" OnClick="showSection_Merchant_Click" runat="server">
                                    <div id="Div2" runat="server">
                                        <div class="row">
                                            <div class="col-3 text-left p-b-15">
                                                <i class=" fas fa-store-alt  text-c-blue f-30"></i>  
                                            </div>
                                            <div class="col-9 text-right">
                                                <h3 class="text-c-blue">
                                                    <asp:Label ID="countSection_Merchant" runat="server"></asp:Label></h3>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 text-left">
                                                <p class=" m-0">Merchant</p>
                                            </div>

                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-sm-12">


                            <div id="Section_UserLogin" runat="server">
                                <div class="card">
                                    <div class="card-header border-0">
                                        <div class="sub-title">User Login</div>
                                    </div>
                                    <div class="card-body">

                                        <div class="form-group row">

                                            <label class="col-sm-2 col-form-label">Username</label>
                                            <div class="col-sm-4">
                                                <asp:HiddenField ID="hidUserLoginId" runat="server"></asp:HiddenField>

                                                <asp:TextBox ID="txtusernameIns" class="form-control" runat="server"></asp:TextBox>

                                                <asp:Label ID="lblusernameIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-2 col-form-label">Password</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPasswordIns" class="form-control" runat="server"></asp:TextBox>

                                                <asp:Label ID="lblPasswordIns" runat="server" ForeColor="#6c757d"  CssClass=" validation"></asp:Label>
                                            </div>

                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnEditUser" Text="บันทึก" CssClass="button-pri button-accept m-r-10" OnClick="btnEditUser_Click" runat="server" />
                                                <asp:Button ID="btnClearEditUser" Text="ล้าง" CssClass="button-pri button-cancel" OnClick="btnClearEditUser_Click" runat="server" />
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>


                            <div id="Section_Role" runat="server">

                                <div class="card">
                                    <div class="card-header border-0">
                                        <div class="sub-title">Role</div>
                                    </div>
                                    <div class="card-block">
                                       
                                            <div class="form-group row">
                                                <div class="col-sm-2 col-form-label">Role  </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlRole" class="form-control" runat="server"></asp:DropDownList>
                                                    <asp:Label ID="lblRole" runat="server" CssClass="validation"></asp:Label>

                                                </div>
                                        
                                            </div>
 
                                                <div class="m-b-20 m-t-20">
                                                    <asp:LinkButton ID="btnSubmitRole" class="button-action button-add m-r-5" data-backdrop="false" OnClick="btnSubmitRole_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmGV();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                                </div>
 
                                             
                                        <asp:GridView ID="gvEmpRole" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand " Style="white-space: nowrap" TabIndex="0" Width="100%"
                                            CellSpacing="0" ShowHeaderWhenEmpty="true">
                                            <Columns>


                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                            <asp:CheckBox ID="chkEmpRoleAll" OnCheckedChanged="chkEmpRoleAll_Changed" AutoPostBack="true" runat="server"  />
                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmpRole" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Role Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRoleCode" Text=' <%# DataBinder.Eval(Container.DataItem, "RoleCode")%>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Role Name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRoleName" Text=' <%# DataBinder.Eval(Container.DataItem, "RoleName")%>' runat="server" />
                                                        <asp:HiddenField runat="server" ID="hidEmpRoleId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpRoleId")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>


                            <div id="Section_Merchant" runat="server">

                                <div class="card">
                                    <div class="card-header border-0">
                                        <div class="sub-title">ร้านค้า</div>
                                    </div>
                                    <div class="card-block">
                                    
                                            <div class="form-group row">
                                                <div class="col-sm-2 col-form-label">ร้านค้า  </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlMerchant" class="form-control" runat="server"></asp:DropDownList>
                                                    <asp:Label ID="lblMerchant" runat="server" CssClass="validation"></asp:Label>

                                                </div> 
                                            </div> 
                                            <div class="m-b-20 m-t-20"> 
                                                <asp:LinkButton ID="btnSubmitMerchant" class="button-action button-add m-r-5" data-backdrop="false" OnClick="btnSubmitMerchant_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                               <asp:LinkButton ID="btnDeleteMer" OnClick="btnDeleteMer_Click" OnClientClick="return DeleteConfirmGV();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
 
                                           </div>
                                        <asp:GridView ID="gvMerchant" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand " Style="white-space: nowrap" TabIndex="0" Width="100%"
                                            CellSpacing="0" ShowHeaderWhenEmpty="true">
                                            <Columns>


                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                            <asp:CheckBox ID="chkMerchantAll" OnCheckedChanged="chkMerchantAll_Changed" AutoPostBack="true" runat="server"  />
                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMerchant" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสร้านค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMerCode" Text=' <%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อร้านค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMerName" Text=' <%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                        <asp:HiddenField runat="server" ID="hidMermapId" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantId")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

</asp:Content>






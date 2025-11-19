<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="CallInfo_Olds.aspx.cs" Inherits="DOMS_TSR.src.CallInfo.CallInfo_Olds" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .validation {
            color:red
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
             <asp:HiddenField ID="HidRefUsername" runat="server" />
             <asp:HiddenField ID="HidCompanyCode" runat="server" />
            <asp:HiddenField ID="hidFlagSave" runat="server" />
            <asp:HiddenField ID="hidMsgSave" runat="server" />
            <asp:HiddenField ID="hidCallInfoID" runat="server" />
            <asp:HiddenField ID="hidMerCode" runat="server" />
            <asp:HiddenField ID="hidMerName" runat="server" />
            <div class="page-body">
                <div class="row">
                    <div class="col-12">

                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Manual Take Call</div>
                            </div>

                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ชื่อ</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtFName" CssClass="form-control" runat="server" onkeypress="return validatetext(event);"></asp:TextBox>
                                        <asp:Label ID="lbltxtFName" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                                    
                                    <label class="col-sm-2 col-form-label">นามสกุล</label>
                                    <asp:Label ID="lblCustomerLName" runat="server" CssClass="validation"></asp:Label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtLName" CssClass="form-control" runat="server" onkeypress="return validatetext(event);"></asp:TextBox>
                                        <asp:Label ID="lbltxtLName" runat="server" CssClass="validation"></asp:Label>
                                    </div>

                                    <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTel" CssClass="form-control" runat="server" MaxLength="10" onkeypress="return validatenumerics(event);"></asp:TextBox>
                                        <asp:Label ID="lbltxtTel" runat="server" CssClass="validation"></asp:Label>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSave" Text="บันทึก" class="button-pri button-accept m-r-10" OnClick="btnSave_Click" runat="server" />
                                    <asp:Button ID="btnSearch" Text="ค้นหา" class="button-pri button-accept m-r-10" OnClick="btnSearch_Click" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div id="cardGridviewCallInfo" class="card" runat="server" style="display:none">
                            <div class="card-block">

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvCallInfo" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvCallInfo_RowDataBound" OnRowCommand="gvCallInfo_RowCommand">
                                            <Columns>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">วันที่ติดต่อ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreateDate" Text='<%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "CreateDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">เวลาที่ติดต่อ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreateDateTime" Text='<%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "CreateDate").ToString()).ToString("HH:mm") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">เบอร์ที่ติดต่อ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCallInNumber" Text='<%# DataBinder.Eval(Container.DataItem, "CallInNumber")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" CommandName="ShowCustomer" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:HiddenField runat="server" ID="hidCustomerCode" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidOrderCode" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerFName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerFName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerLName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerLName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidTitle" Value='<%# DataBinder.Eval(Container.DataItem, "Title")%>' />
                                                        <asp:HiddenField runat="server" ID="hidGender" Value='<%# DataBinder.Eval(Container.DataItem, "Gender")%>' />
                                                        <asp:HiddenField runat="server" ID="hidBirthDate" Value='<%# DataBinder.Eval(Container.DataItem, "BirthDate")%>' />
                                                        <asp:HiddenField runat="server" ID="hidIdentification" Value='<%# DataBinder.Eval(Container.DataItem, "Identification")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMaritalStatusCode" Value='<%# DataBinder.Eval(Container.DataItem, "MaritalStatusCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidOccupation" Value='<%# DataBinder.Eval(Container.DataItem, "Occupation")%>' />
                                                        <asp:HiddenField runat="server" ID="hidHomePhone" Value='<%# DataBinder.Eval(Container.DataItem, "HomePhone")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMail" Value='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' />
                                                        <asp:HiddenField runat="server" ID="hidIncome" Value='<%# DataBinder.Eval(Container.DataItem, "Income")%>' />
                                                        <asp:HiddenField runat="server" ID="hidContactTel" Value='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' />
                                                        <asp:HiddenField runat="server" ID="hidAge" Value='<%# DataBinder.Eval(Container.DataItem, "Age")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMobile" Value='<%# DataBinder.Eval(Container.DataItem, "Mobile")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCallInNumber" Value='<%# DataBinder.Eval(Container.DataItem, "CallInNumber")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ประวัติการสั่งซื้อ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# GetLink(DataBinder.Eval(Container.DataItem, "OrderCode")) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อ - นามสกุล</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFName")%>' runat="server" />
                                                        <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerLName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">สถานะการติดต่อ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTSTATUS" Text='<%# DataBinder.Eval(Container.DataItem, "CONTACTSTATUS")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>







    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-customer">
        <div class="modal-dialog modal-lg" style="max-width: 1300px;">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2    ">
                            <div class="col-sm-12  p-0"">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มลูกค้า</div>

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
                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>

                                        <div class="form-group row">
                                            <label id ="lblCusCode_Ins" class="col-sm-2 col-form-label" runat="server">รหัสลูกค้า</label>
                                            <div id="divTxtCus_Ins" class="col-sm-3"  runat="server">
                                                <asp:TextBox ID="txtCustomerCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblCustomerCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidCustomer_Ins" runat="server"></asp:HiddenField>
                                            </div>

                                            <label id="lblCol1" class="col-sm-1 col-form-label"  runat="server"></label>
                                            <label class="col-sm-2 col-form-label">คำนำหน้า</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlTitle_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblTitle_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">ชื่อ</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtFirstName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblFirstName_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">นามสกุล</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtLastName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblLastName_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">เพศ</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlGender_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblGender_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">วันเกิด</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtBirthDate_Ins" runat="server" CssClass="form-control" autocomplete="off" BackColor="White" />
                                                <ajaxToolkit:CalendarExtender ID="car_txtBirthDate_Ins" runat="server" TargetControlID="txtBirthDate_Ins" PopupButtonID="Image2" OnClientDateSelectionChanged="SelectDate" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                <script type="text/javascript">

                                                    function SelectDate(e) {
                                                        var PresentDay = new Date();
                                                        var dateOfBirth = e.get_selectedDate();
                                                        var months = (PresentDay.getMonth() - dateOfBirth.getMonth() + (12 * (PresentDay.getFullYear() - dateOfBirth.getFullYear())));
                                                        //alert("You Are Of " + Math.round(months / 12) + " Years");
                                                        document.getElementById('<%=txtAge_Ins.ClientID%>').value = Math.round(months / 12);
                                                    }
                                                </script>
                                                <asp:Label ID="lblBirthDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">อายุ</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAge_Ins" runat="server" CssClass="form-control" Enabled="false" />
                                                <asp:Label ID="lblAge_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">บัตรประจำตัวประชาชน</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtIdentificationNo_Ins" runat="server" class="form-control" MaxLength="13" onkeypress="return validatenumerics(event);"></asp:TextBox>
                                                <asp:Label ID="lblIdentificationNo_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">สถานะ</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlMaritalStatus_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblMaritalStatus_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">อาชีพ</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlOccupation_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblOccupation_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">รายได้</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtIncome_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblIncome_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtContactTel_Ins" runat="server" class="form-control" MaxLength="10" onkeypress="return validatenumerics(event);"></asp:TextBox>
                                                <asp:Label ID="lblContactTel_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์(บ้าน)</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtHomePhone_Ins" runat="server" class="form-control" MaxLength="10"  onkeypress="return validatenumerics(event);"></asp:TextBox>
                                                <asp:Label ID="lblHomePhone_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">อีเมล์</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtEmail_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblEmail_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="text-center m-t-20 center">
                                    <asp:Button ID="btnSubmit" Text="Submit" class="button-pri button-accept m-r-10" runat="server" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" Text="Cancel" class="button-pri button-cancel" runat="server" />
                                </div> 
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
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

    <script type="text/javascript">
        function validatetext(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if ((keycode < 1 || keycode > 7) && (keycode < 9 || keycode > 64) && (keycode < 91 || keycode > 96) && (keycode < 123 || keycode > 127)) {
                return true;
            }
            else alert(" กรุณาระบุตัวอักษร "); return false;
        }
    </script>

</asp:Content>

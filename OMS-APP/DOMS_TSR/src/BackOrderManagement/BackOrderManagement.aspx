<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="BackOrderManagement.aspx.cs" Inherits="DOMS_TSR.src.BackOrderManagement.BackOrderManagement" %>

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

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hidBranchcode" runat="server" />
    <asp:HiddenField ID="hiddisplayname" runat="server" />
    <asp:HiddenField ID="hidordermsg" runat="server" />

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

                                        <div id="searchSection_NoAnswerOrder" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchOrderCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">

                                                        <asp:TextBox ID="txtSearchOrderDateFrom_NoAnswerOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchOrderDateUntil_NoAnswerOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCustomerCode_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchFName_NoAnswerOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchLName_NoAnswerOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchContact_NoAnswerOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchOrderType_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchDeliverDate_NoAnswerOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchDeliverDate_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchDeliverDateTo_NoAnswerOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchDeliverDateTo_NoAnswerOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">สาขา</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchBranch_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchChannel_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">แบรนด์</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchCamCate_NoAnswerOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnSearch_NoAnswerOrder" Text="ค้นหา" OnClick="btnSearch_Click_NoAnswerOrder" class="button-pri button-accept m-r-10" runat="server" />
                                                <asp:Button ID="btnClearSearch_NoAnswerOrder" Text="ล้าง" OnClick="btnClearSearch_Click_NoAnswerOrder" class="button-pri button-cancel" runat="server" />
                                            </div>

                                        </div>

                                        <div id="searchSection_RequestForEditOrder" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchOrderCode_RequestForEditOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">

                                                        <asp:TextBox ID="txtSearchOrderDateFrom_RequestForEditOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_RequestForEditOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_RequestForEditOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchOrderDateUntil_RequestForEditOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_RequestForEditOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_RequestForEditOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCustomerCode_RequestForEditOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchFName_RequestForEditOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchLName_RequestForEditOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchContact_RequestForEditOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchOrderType_RequestForEditOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchDeliverDate_RequestForEditOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSearchDeliverDate_RequestForEditOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchDeliverDateTo_RequestForEditOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSearchDeliverDateTo_RequestForEditOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">สาขา</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchBranch_RequestForEditOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchChannel_RequestForEditOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">แบรนด์</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchCamCate_RequestForEditOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnSearch_RequestForEditOrder" Text="ค้นหา" OnClick="btnSearch_Click_RequestForEditOrder" class="button-pri button-accept m-r-10" runat="server" />
                                                <asp:Button ID="btnClearSearch_RequestForEditOrder" Text="ล้าง" OnClick="btnClearSearch_Click_RequestForEditOrder" class="button-pri button-cancel" runat="server" />
                                            </div>

                                        </div>

                                        <div id="searchSection_RequestForRejectOrder" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchOrderCode_RequestForRejectOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">

                                                        <asp:TextBox ID="txtSearchOrderDateFrom_RequestForRejectOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_RequestForRejectOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_RequestForRejectOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchOrderDateUntil_RequestForRejectOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_RequestForRejectOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_RequestForRejectOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchCustomerCode_RequestForRejectOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchFName_RequestForRejectOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchLName_RequestForRejectOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSearchContact_RequestForRejectOrder" class="form-control" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchOrderType_RequestForRejectOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtSearchDeliverDate_RequestForRejectOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtSearchDeliverDate_RequestForRejectOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchDeliverDateTo_RequestForRejectOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtSearchDeliverDateTo_RequestForRejectOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">สาขา</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchBranch_RequestForRejectOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchChannel_RequestForRejectOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">แบรนด์</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchCamCate_RequestForRejectOrder" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnSearch_RequestForRejectOrder" Text="ค้นหา" OnClick="btnSearch_Click_RequestForRejectOrder" class="button-pri button-accept m-r-10" runat="server" />
                                                <asp:Button ID="btnClearSearch_RequestForRejectOrder" Text="ล้าง" OnClick="btnClearSearch_Click_RequestForRejectOrder" class="button-pri button-cancel" runat="server" />
                                            </div>

                                        </div>

                                    </div>

                                </div>
                                <div class="card ">
                                        <div class="col-5 m-t-10 m-b-10" >
                                <div class="card-group">

                                    <div class="card" id="cardex1" runat="server">
                                        <asp:LinkButton CssClass="btn-3bar-disable1" ID="showSection_NoAnswerOrder" OnClick="showSection_NoAnswerOrder_Click" runat="server">
                                            <div id="listcard1" runat="server">
                                                <div class="row">
                                                    <div class="col-3 text-left p-b-15">
                                                        <i class="fi fi-3x flaticon-error text-c-blue"></i>
                                                    </div>
                                                    <div class="col-9 text-right">
                                                        <h3 class="text-c-blue">
                                                            <asp:Label ID="countSection_NoAnswerOrder" runat="server"></asp:Label></h3>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 text-left">
                                                        <p class=" m-0">สาขาไม่ตอบรับ Order</p>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="card" id="cardex2" runat="server">
                                        <asp:LinkButton CssClass="btn-3bar-disable2" ID="showSection_RequestForEditOrder" OnClick="showSection_RequestForEditOrder_Click" runat="server">
                                            <div id="listcard2" runat="server">
                                                <div class="row">
                                                    <div class="col-3 text-left p-b-15">
                                                        <i class="fi fi-3x flaticon-edit text-c-blue"></i>
                                                    </div>
                                                    <div class="col-9 text-right">
                                                        <h3 class="text-c-blue">
                                                            <asp:Label ID="countSection_RequestForEditOrder" runat="server"></asp:Label></h3>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 text-left">
                                                        <p class=" m-0">ขอแก้ไข Order</p>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="card" id="cardex3" runat="server">
                                        <asp:LinkButton CssClass="btn-3bar-disable3" ID="showSection_RequestForRejectOrder" OnClick="showSection_RequestForRejectOrder_Click" runat="server">
                                            <div id="listcard3" runat="server">
                                                <div class="row">
                                                    <div class="col-3 text-left p-b-15">
                                                        <i class="fi fi-3x flaticon-close text-c-blue"></i>
                                                    </div>
                                                    <div class="col-9 text-right">
                                                        <h3 class="text-c-blue">
                                                            <asp:Label ID="countSection_RequestForRejectOrder" runat="server"></asp:Label></h3>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 text-left">
                                                        <p class=" m-0">ขอยกเลิก Order</p>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                                
                                    <div class="card-block p-t-5">

                                        <div id="Section_NoAnswerOrder" runat="server">

                                            <input type="hidden" id="hidIdList_NoAnswerOrder" runat="server" />

                                            <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_NoAnswerOrder" runat="server" Text="Merge" />

                                            <asp:Button CssClass="button-pri button-delete" ID="btnCancelOrder_NoAnswerOrder" runat="server" Text="Cancel" OnClick="btnCancelOrder_NoAnswerOrder_Click" />

                                            <asp:Panel ID="Panel_NoAnswerOrder" runat="server" Style="overflow-x: scroll;">
                                                <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                                    <Columns>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                            <asp:CheckBox ID="chkAll_NoAnswerOrder" OnCheckedChanged="chkAll_Change_NoAnswerOrder" AutoPostBack="true" runat="server"  />
                                        </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_NoAnswerOrder" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสใบสั่งขาย</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสลูกค้า</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerCode_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อ - สกุล</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">เบอร์ติดต่อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerContact_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่สั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                                <asp:Label ID="lblOrderDate_NoAnswerOrder" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ประเภทการสั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSaleOrderTypeName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่จัดส่ง</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeliverDate_NoAnswerOrder" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">หมายเหตุ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderRejectRemark_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">สาขา</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBranchName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChannelName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">แบรนด์</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrandName_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                            <HeaderTemplate>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="btnRequestReject_NoAnswerOrder" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                                <asp:HiddenField runat="server" ID="hidOrderId_NoAnswerOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />

                                                                <asp:HiddenField runat="server" ID="hidOrderCode_NoAnswerOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                                <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_NoAnswerOrder" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidCustomerName_NoAnswerOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidCustomerContact_NoAnswerOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                                <asp:HiddenField runat="server" ID="hidCreateDate_NoAnswerOrder" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />


                                                                <br />
                                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                    </Columns>

                                                    <EmptyDataTemplate>
                                                        <center>
                                    <asp:Label ID="lblDataEmpty_NoAnswerOrder" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                                    <asp:Button ID="lnkbtnFirst_NoAnswerOrder" CssClass="Button" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre_NoAnswerOrder" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                        Text="<" runat="server" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_NoAnswerOrder" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_NoAnswerOrder">
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPages_NoAnswerOrder" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnNext_NoAnswerOrder" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLast_NoAnswerOrder" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetPageIndex_NoAnswerOrder"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>





                                        </div>

                                        <div id="Section_RequestForEditOrder" runat="server">

                                            <input type="hidden" id="hidIdList_RequestForEditOrder" runat="server" />

                                            <asp:Button CssClass="button-pri button-print m-b-10" ID="btnMergeOrder_RequestForEditOrder" runat="server" Text="Merge" />

                                            <%--<asp:Button CssClass=" button-pri button-delete m-b-10" ID="btnCancelOrder_RequestForEditOrder" runat="server" Text="Cancel" OnClick="btnCancelOrder_RequestForEditOrder_Click" />--%>

                                            <asp:Panel ID="Panel_RequestForEditOrder" runat="server" Style="overflow-x: scroll;">
                                                <asp:GridView ID="gvOrder_RequestForEditOrder" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                                    <Columns>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                            <asp:CheckBox ID="chkAll_RequestForEditOrder" OnCheckedChanged="chkAll_Change_RequestForEditOrder" AutoPostBack="true" runat="server"  />
                                        </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_RequestForEditOrder" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสใบสั่งขาย</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderCode_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสลูกค้า</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerCode_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อ - สกุล</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">เบอร์ติดต่อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerContact_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่สั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                                <asp:Label ID="lblOrderDate_RequestForEditOrder" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ประเภทการสั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSaleOrderTypeName_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่จัดส่ง</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeliverDate_RequestForEditOrder" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">หมายเหตุ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderRejectRemark_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">สาขา</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBranchName_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChannelName_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">แบรนด์</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrandName_RequestForEditOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                            <HeaderTemplate>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="btnRequestReject_RequestForEditOrder" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                                <asp:HiddenField runat="server" ID="hidOrderId_RequestForEditOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />

                                                                <asp:HiddenField runat="server" ID="hidOrderCode_RequestForEditOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                                <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_RequestForEditOrder" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidCustomerName_RequestForEditOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidCustomerContact_RequestForEditOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                                <asp:HiddenField runat="server" ID="hidCreateDate_RequestForEditOrder" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />


                                                                <br />
                                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                    </Columns>

                                                    <EmptyDataTemplate>
                                                        <center>
                                    <asp:Label ID="lblDataEmpty_RequestForEditOrder" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                                    <asp:Button ID="lnkbtnFirst_RequestForEditOrder" CssClass="Button" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" OnCommand="GetPageIndex_RequestForEditOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre_RequestForEditOrder" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                        Text="<" runat="server" OnCommand="GetPageIndex_RequestForEditOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_RequestForEditOrder" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_RequestForEditOrder">
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPages_RequestForEditOrder" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnNext_RequestForEditOrder" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_RequestForEditOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLast_RequestForEditOrder" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetPageIndex_RequestForEditOrder"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>





                                        </div>

                                        <div id="Section_RequestForRejectOrder" runat="server">

                                            <input type="hidden" id="hidIdList_RequestForRejectOrder" runat="server" />

                                            <asp:Button CssClass="button-pri button-delete m-b-10" ID="btnCancelOrder_RequestForRejectOrder" runat="server" Text="Cancel" OnClick="btnCancelOrder_RequestForRejectOrder_Click" />
                                            <%--<asp:Button CssClass="button-pri button-delete m-b-10" ID="btnCancelOrder_RequestForRejectOrder" runat="server" Text="Cancel" OnClick="btnCancelOrder_RequestForRejectOrder_Click" />--%>

                                            <asp:Panel ID="Panel_RequestForRejectOrder" runat="server" Style="overflow-x: scroll;">
                                                <asp:GridView ID="gvOrder_RequestForRejectOrder" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                                    <Columns>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                            <asp:CheckBox ID="chkAll_RequestForRejectOrder" OnCheckedChanged="chkAll_Change_RequestForRejectOrder" AutoPostBack="true" runat="server"  />
                                        </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_RequestForRejectOrder" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสใบสั่งขาย</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderCode_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">รหัสลูกค้า</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerCode_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ชื่อ - สกุล</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">เบอร์ติดต่อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerContact_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่สั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                                <asp:Label ID="lblOrderDate_RequestForRejectOrder" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ประเภทการสั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSaleOrderTypeName_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">วันที่จัดส่ง</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeliverDate_RequestForRejectOrder" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">หมายเหตุ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderRejectRemark_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">สาขา</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBranchName_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChannelName_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">แบรนด์</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrandName_RequestForRejectOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnRequestReject_RequestForRejectOrder" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidOrderId_RequestForRejectOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />

                                                        <asp:HiddenField runat="server" ID="hidOrderCode_RequestForRejectOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_RequestForRejectOrder" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerName_RequestForRejectOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerContact_RequestForRejectOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCreateDate_RequestForRejectOrder" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />


                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>--%>
                                                    </Columns>

                                                    <EmptyDataTemplate>
                                                        <center>
                                    <asp:Label ID="lblDataEmpty_RequestForRejectOrder" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                                    <asp:Button ID="lnkbtnFirst_RequestForRejectOrder" CssClass="Button" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" OnCommand="GetPageIndex_RequestForRejectOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre_RequestForRejectOrder" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                        Text="<" runat="server" OnCommand="GetPageIndex_RequestForRejectOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_RequestForRejectOrder" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_RequestForRejectOrder">
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPages_RequestForRejectOrder" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnNext_RequestForRejectOrder" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_RequestForRejectOrder"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLast_RequestForRejectOrder" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetPageIndex_RequestForRejectOrder"></asp:Button>
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

</asp:Content>

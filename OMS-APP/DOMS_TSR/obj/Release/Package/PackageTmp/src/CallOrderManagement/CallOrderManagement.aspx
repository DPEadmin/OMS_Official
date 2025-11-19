<%@ Page Title="" Language="C#" MasterPageFile="~/src/TakeOrderMK/MK.Master" AutoEventWireup="true" CodeBehind="CallOrderManagement.aspx.cs" Inherits="DOMS_TSR.src.CallOrderManagement.CallOrderManagement" %>

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

    <script>
        function EvalSound(soundobj) {
            var thissound = document.getElementById(soundobj);


            var trackLength = 4000 // 4 seconds for instance
            var playthroughs = 3 //play through the file 3 times

            var player = setInterval(function () {
                if (playthroughs > 0) {
                    thissound.play();
                    playthroughs--;
                }
                else clearInterval(player)
            }, trackLength);

        }
    </script>

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
            <div class="main-content">
              <div class="page-wrapper">
            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">ค้นหาข้อมูลการสั่งซื้อ</div>
                            </div>

                            <div class="card-body">
                                <div id="searchSection_All" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom" runat="server" TargetControlID="txtSearchOrderDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil" runat="server" TargetControlID="txtSearchOrderDateUntil" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_All" runat="server" TargetControlID="txtSearchDeliverDate" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_All" runat="server" TargetControlID="txtSearchDeliverDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    
                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                        <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                    </div>
                                </div>

                                <div id="searchSection_NewOrder" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_NewOrder" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_NewOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_NewOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_NewOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_NewOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_NewOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_NewOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_NewOrder" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_NewOrder" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_NewOrder" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_NewOrder" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType_NewOrder" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate_NewOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchDeliverDate_NewOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo_NewOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchDeliverDateTo_NewOrder" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus_NewOrder" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch_NewOrder" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel_NewOrder" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                   
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_NewOrder" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_NewOrder" Text="ค้นหา" OnClick="btnSearch_Click_NewOrder"
                                            class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_NewOrder" Text="ล้าง" OnClick="btnClearSearch_Click_NewOrder"
                                            class="button-pri button-cancel"
                                            runat="server" />
                                    </div>

                                </div>

                                <div id="searchSection_Cooking" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Cooking" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Cooking" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Cooking" runat="server" TargetControlID="txtSearchOrderDateFrom_Cooking" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Cooking" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Cooking" runat="server" TargetControlID="txtSearchOrderDateUntil_Cooking" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Cooking" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Cooking" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Cooking" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Cooking" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType_Cooking" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate_Cooking" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSearchDeliverDate_Cooking" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo_Cooking" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSearchDeliverDateTo_Cooking" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus_Cooking" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch_Cooking" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel_Cooking" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Cooking" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Cooking" Text="ค้นหา" OnClick="btnSearch_Click_Cooking"
                                        class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_Cooking" Text="ล้าง" OnClick="btnClearSearch_Click_Cooking"
                                            class="button-pri button-cancel"
                                            runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_Cooked" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Cooked" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Cooked" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Cooked" runat="server" TargetControlID="txtSearchOrderDateFrom_Cooked" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Cooked" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Cooked" runat="server" TargetControlID="txtSearchOrderDateUntil_Cooked" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Cooked" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Cooked" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Cooked" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Cooked" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType_Cooked" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate_Cooked" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtSearchDeliverDate_Cooked" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo_Cooked" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtSearchDeliverDateTo_Cooked" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus_Cooked" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch_Cooked" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel_Cooked" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Cooked" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Cooked" Text="ค้นหา" OnClick="btnSearch_Click_Cooked"
                                        class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_Cooked" Text="ล้าง" OnClick="btnClearSearch_Click_Cooked"
                                        class="button-pri button-cancel "
                                            runat="server" />
                                    </div>

                                </div>
                                <div id="searchSection_Delivering" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Delivering" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Delivering" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Delivering" runat="server" TargetControlID="txtSearchOrderDateFrom_Delivering" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Delivering" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Delivering" runat="server" TargetControlID="txtSearchOrderDateUntil_Delivering" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Delivering" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Delivering" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Delivering" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Delivering" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType_Delivering" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate_Delivering" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtSearchDeliverDate_Delivering" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo_Delivering" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtSearchDeliverDateTo_Delivering" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus_Delivering" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch_Delivering" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel_Delivering" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Delivering" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Delivering" Text="ค้นหา" OnClick="btnSearch_Click_Delivering"
                                        class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_Delivering" Text="ล้าง" OnClick="btnClearSearch_Click_Delivering"
                                        class="button-pri button-cancel "
                                            runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_Delivered" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Delivered" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Delivered" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Delivered" runat="server" TargetControlID="txtSearchOrderDateFrom_Delivered" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Delivered" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Delivered" runat="server" TargetControlID="txtSearchOrderDateUntil_Delivered" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Delivered" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Delivered" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Delivered" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Delivered" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType_Delivered" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate_Delivered" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtSearchDeliverDate_Delivered" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo_Delivered" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtSearchDeliverDateTo_Delivered" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus_Delivered" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch_Delivered" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel_Delivered" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Delivered" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Delivered" Text="ค้นหา" OnClick="btnSearch_Click_Delivered"
                                        class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_Delivered" Text="ล้าง" OnClick="btnClearSearch_Click_Delivered"
                                        class="button-pri button-cancel "
                                            runat="server" />
                                    </div>

                                </div>
                                <div id="searchSection_OrderCancelled" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_OrderCancelled" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_OrderCancelled" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_OrderCancelled" runat="server" TargetControlID="txtSearchOrderDateFrom_OrderCancelled" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_OrderCancelled" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_OrderCancelled" runat="server" TargetControlID="txtSearchOrderDateUntil_OrderCancelled" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_OrderCancelled" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_OrderCancelled" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_OrderCancelled" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_OrderCancelled" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType_OrderCancelled" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate_OrderCancelled" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txtSearchDeliverDate_OrderCancelled" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo_OrderCancelled" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txtSearchDeliverDateTo_OrderCancelled" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus_OrderCancelled" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch_OrderCancelled" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel_OrderCancelled" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_OrderCancelled" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_OrderCancelled" Text="ค้นหา" OnClick="btnSearch_Click_OrderCancelled"
                                        class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_OrderCancelled" Text="ล้าง" OnClick="btnClearSearch_Click_OrderCancelled"
                                            class="button-pri button-cancel"
                                            runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_OrderChanged" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_OrderChanged" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_OrderChanged" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_OrderChanged" runat="server" TargetControlID="txtSearchOrderDateFrom_OrderChanged" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_OrderChanged" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_OrderChanged" runat="server" TargetControlID="txtSearchOrderDateUntil_OrderChanged" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_OrderChanged" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_OrderChanged" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_OrderChanged" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_OrderChanged" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ประเภทการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderType_OrderChanged" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">วันที่จัดส่ง</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchDeliverDate_OrderChanged" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txtSearchDeliverDate_OrderChanged" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchDeliverDateTo_OrderChanged" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txtSearchDeliverDateTo_OrderChanged" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderStatus_OrderChanged" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">สาขา</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchBranch_OrderChanged" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchChannel_OrderChanged" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_OrderChanged" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_OrderChanged" Text="ค้นหา" OnClick="btnSearch_Click_OrderChanged"
                                        class="button-pri button-accept m-r-10" runat="server" />
                                        <asp:Button ID="btnClearSearch_OrderChanged" Text="ล้าง" OnClick="btnClearSearch_Click_OrderChanged"
                                            class="button-pri button-cancel" runat="server" />
                                    </div>
                                </div>
                            </div>


                        </div>







                        <div class="card">
                            <div class="card-group">
                                <div class="card" id="cardex1" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable" ID="showSection_All" title="Hello World!" OnClick="showSection_All_Click" runat="server">
                                        <div id="listcard1" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class=" ti-layout-list-thumb-alt text-c-blue f-30"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_All" runat="server"></asp:Label></h3>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">ทั้งหมด</p>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex2" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable2" ID="showSection_NewOrder" OnClick="showSection_NewOrder_Click" runat="server">
                                        <div id="listcard2" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fi fi-3x flaticon-new text-c-blue  "></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_NewOrder" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">การสั่งซื้อใหม่</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>


                                <div class="card" id="cardex4" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable4" ID="showSection_Cooking" OnClick="showSection_Cooking_Click" runat="server">
                                        <div id="listcard4" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class=" fi fi-3x flaticon-cook text-c-blue "></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Cooking" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">กำลังปรุงหรือจัด...</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex5" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable5" ID="showSection_Cooked" OnClick="showSection_Cooked_Click" runat="server">
                                        <div id="listcard5" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="ion-checkmark-circled text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Cooked" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">เตรียมสินค้าเรียบ...</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex6" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable6" ID="showSection_Delivering" OnClick="showSection_Delivering_Click" runat="server">

                                        <div id="listcard6" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fi flaticon-truck text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Delivering" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">กำลังจัดส่งสินค้า</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex7" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable7" ID="showSection_Delivered" OnClick="showSection_Delivered_Click" runat="server">

                                        <div id="listcard7" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fi flaticon-truck-1 text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Delivered" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">จัดส่งสินค้าเรียบร้อย</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex8" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable8" ID="showSection_OrderCancelled" OnClick="showSection_OrderCancelled_Click" runat="server">

                                        <div id="listcard8" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="ion-close-circled text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_OrderCancelled" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">ยกเลิกการสั่งซื้อ</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                                     
                                <div class="card" id="cardex9" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable9" ID="showSection_OrderChanged" OnClick="showSection_OrderChanged_Click" runat="server">



                                        <div id="listcard9" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fi ion-shuffle text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_OrderChanged" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">เปลี่ยนแปลงข้อมูล...</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>


                            </div>
                            <div class="card-body">

                                <div id="Section_All" runat="server">

                                    <%--<asp:Button CssClass="btn btn-sm btn-success" ID="btnCreateTakeOrder_All" runat="server" Text="Create" PostBackUrl="~/src/TakeOrderMK/TakeOrder.aspx?CustomerCode=C2018091700001&RefCode=0001&BrandNo=01&Customerphone=0838023829&UniqueId=Unq001"/>--%>
                                    <%--<asp:LinkButton ID="LinkButton1" runat="server"    >Call up Orrapan</asp:LinkButton>--%>

                                    <asp:Panel ID="Panel_All" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_All" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                        <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "CustomerCode")) %>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สถานะการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
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

                                <div id="Section_NewOrder" runat="server">

                                    <input type="hidden" id="hidIdList_NewOrder" runat="server" />

                                    <asp:Button CssClass="button-pri button-print m-b-10" ID="btnMergeOrder_NewOrder" runat="server" Text="Merge" />

                                    <asp:Button CssClass="button-pri button-delete m-b-10" ID="btnCancelOrder_NewOrder" runat="server" Text="Cancel" OnClick="btnCancelOrder_NewOrder_Click" />

                                    <asp:Panel ID="Panel_NewOrder" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_NewOrder" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                            <asp:CheckBox ID="chkAll_NewOrder" OnCheckedChanged="chkAll_Change_NewOrder" AutoPostBack="true" runat="server"  />
                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_NewOrder" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_NewOrder" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate_NewOrder" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_NewOrder" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnRequestReject_NewOrder" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidOrderId_NewOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />

                                                        <asp:HiddenField runat="server" ID="hidOrderCode_NewOrder" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_NewOrder" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerName_NewOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerContact_NewOrder" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCreateDate_NewOrder" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />


                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_NewOrder" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_NewOrder" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_NewOrder"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_NewOrder" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_NewOrder"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_NewOrder" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_NewOrder">
                                                                                    </asp:DropDownList>
                                                            of
                                                                                    <asp:Label ID="lblTotalPages_NewOrder" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_NewOrder" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_NewOrder"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_NewOrder" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_NewOrder"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>





                                </div>

                                <div id="Section_Cooking" runat="server">
                                    <%--<asp:Button CssClass="btn btn-sm btn-primary" ID="btnSubmitToRider_Cooking" runat="server" Text="Merge"/>
                                    <asp:Button CssClass="btn btn-sm btn-danger" ID="btnPrint_Cooking" runat="server" Text="Cancel" OnClick="btnSubmitToRider_Cooking_Click"/>--%>
                                    <asp:Panel ID="Panel_Cooking" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Cooking" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowCommand="gvOrder_Cooking_RowCommand">

                                            <Columns>

                                                <%--<asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                            <asp:CheckBox ID="chkAll_Cooking" OnCheckedChanged="chkAll_Change_Cooking" AutoPostBack="true" runat="server"  />
                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Cooking" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Cooking" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate_Cooking" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Cooking" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnRequestReject_Cooking" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Request for reject"  CommandName="ShowRequestReject_Cooking"></asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidOrderId_Cooking" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidOrderCode_Cooking" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_Cooking" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerName_Cooking" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerContact_Cooking" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCreateDate_Cooking" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />


                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                               

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_Cooking" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_Cooking" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_Cooking"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Cooking" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_Cooking"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_Cooking" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Cooking">
                                                                                    </asp:DropDownList>
                                                            of
                                                                                    <asp:Label ID="lblTotalPages_Cooking" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Cooking" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_Cooking"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Cooking" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Cooking"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div id="Section_Cooked" runat="server">
                                    <%--<asp:Button CssClass="btn btn-sm btn-primary" ID="btnMergeOrder_Cooked" runat="server" Text="Merge"/>
                                    <asp:Button CssClass="btn btn-sm btn-danger" ID="btnOrderCancel_Cooked" runat="server" Text="Cancel" OnClick="btnOrderCancel_Cooked_Click"/>--%>
                                    
                                    <asp:Panel ID="Panel_Cooked" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Cooked" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowCommand="gvOrder_Cooked_RowCommand">
                                            
                                            <Columns>

                                                <%--<asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                            <asp:CheckBox ID="chkAll_Cooked" OnCheckedChanged="chkAll_Change_Cooked" AutoPostBack="true" runat="server"  />
                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Cooked" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Cooked" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate_Cooked" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Cooked" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnRequestReject_Cooked" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Request for reject"  CommandName="ShowRequestReject_Cooked"></asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidOrderId_Cooked" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidOrderCode_Cooked" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_Cooked" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerName_Cooked" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerContact_Cooked" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCreateDate_Cooked" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />


                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_Cooked" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_Cooked" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_Cooked"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Cooked" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_Cooked"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_Cooked" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Cooked">
                                                                                    </asp:DropDownList>
                                                            of
                                                                                    <asp:Label ID="lblTotalPages_Cooked" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Cooked" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_Cooked"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Cooked" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Cooked"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div id="Section_Delivering" runat="server">
                                    <%--<asp:Button CssClass="btn btn-sm btn-danger" ID="btnOrderCancel_Delivering" runat="server" Text="Cancel" OnClick="btnOrderCancel_Delivering_Click"/>--%>

                                    <asp:Panel ID="Panel_Delivering" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Delivering" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowCommand="gvOrder_Delivering_RowCommand">

                                            <Columns>

                                                <%--<asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                            <asp:CheckBox ID="chkAll_Delivering" OnCheckedChanged="chkAll_Change_Delivering" AutoPostBack="true" runat="server"  />
                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Delivering" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Delivering" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate_Delivering" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Delivering" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnRequestReject_Delivering" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Request for reject"  CommandName="ShowRequestReject_Delivering"></asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidOrderId_Delivering" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidOrderCode_Delivering" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_Delivering" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerName_Delivering" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerContact_Delivering" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCreateDate_Delivering" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />


                                                        <br />
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_Delivering" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_Delivering" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_Delivering"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Delivering" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_Delivering"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_Delivering" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Delivering">
                                                                                    </asp:DropDownList>
                                                            of
                                                                                    <asp:Label ID="lblTotalPages_Delivering" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Delivering" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_Delivering"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Delivering" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Delivering"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="Section_Delivered" runat="server">
                                    <asp:Panel ID="Panel_Delivered" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Delivered" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                            TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                           <Columns>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Delivered" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate_Delivered" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Delivered" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_Delivered" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_Delivered" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_Delivered"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Delivered" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_Delivered"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_Delivered" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Delivered">
                                                                                    </asp:DropDownList>
                                                            of
                                                                                    <asp:Label ID="lblTotalPages_Delivered" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Delivered" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_Delivered"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Delivered" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Delivered"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div id="Section_OrderCancelled" runat="server">
                                    <asp:Panel ID="Panel_OrderCancelled" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_OrderCancelled" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                            TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_OrderCancelled" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate_OrderCancelled" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_OrderCancelled" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_OrderCancelled" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_OrderCancelled"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_OrderCancelled" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_OrderCancelled"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_OrderCancelled" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_OrderCancelled">
                                                                                    </asp:DropDownList>
                                                            of
                                                                                    <asp:Label ID="lblTotalPages_OrderCancelled" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_OrderCancelled" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_OrderCancelled"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_OrderCancelled" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_OrderCancelled"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div id="Section_OrderChanged" runat="server">
                                    <asp:Button CssClass="button-pri button-print m-b-10" ID="btnMergeOrder_OrderChanged" runat="server" Text="Merge"/>
                                    <asp:Button CssClass="button-pri button-delete m-b-10" ID="btnOrderCancel_OrderChanged" runat="server" Text="Cancel" OnClick="btnOrderCancel_OrderChanged_Click"/>

                                    <asp:Panel ID="Panel_OrderChanged" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_OrderChanged" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                            TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <center>
                                            <asp:CheckBox ID="chkAll_OrderChanged" OnCheckedChanged="chkAll_Change_OrderChanged" AutoPostBack="true" runat="server"  />
                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_OrderChanged" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_OrderChanged" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ประเภทการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaleOrderTypeName_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">วันที่จัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliverDate_OrderChanged" Text='<%# ((null == Eval("DeliveryDate"))||("" == Eval("DeliveryDate"))) ? string.Empty : DateTime.Parse(Eval("DeliveryDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "OrderRejectRemark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">สาขา</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ช่องทางการสั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelName_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_OrderChanged" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnRequestReject_OrderChanged" runat="Server" class="button-activity" Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                        <asp:HiddenField runat="server" ID="hidOrderId_OrderChanged" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidOrderCode_OrderChanged" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSaleOrderTypeName_OrderChanged" Value='<%# DataBinder.Eval(Container.DataItem, "SaleOrderTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerName_OrderChanged" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCustomerContact_OrderChanged" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCreateDate_OrderChanged" Value='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/%MM/%yyyy HH:mm:ss น.") %>' />

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_OrderChanged" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_OrderChanged" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_OrderChanged"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_OrderChanged" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_OrderChanged"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_OrderChanged" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_OrderChanged">
                                                                                    </asp:DropDownList>
                                                            of
                                                                                    <asp:Label ID="lblTotalPages_OrderChanged" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_OrderChanged" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_OrderChanged"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_OrderChanged" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_OrderChanged"></asp:Button>
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
                </div>
            <%--<asp:Button ID="testsound" OnClick="testsound_Click" Text="testsound" runat="server" />--%>
            <audio id="audio1" src="call.wav" controls preload="auto" autobuffer hidden="true">
                <asp:Button ID="btnsubmit" OnClick="btnsubmit_Click" runat="server" Style="margin-left: -900px;" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalRequestForReject_Cooking" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body ">
                            <div class="form-group row">
                                <asp:Label ID="lblOrderCode_Cooking" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblSaleOrderTypeName_Cooking" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="lblCustomerName_Cooking" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblCustomerContact_Cooking" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label CssClass="col-sm-6 col-form-label text-right" runat="server" Text="วันที่สั่งออเดอร์"></asp:Label>
                                <asp:Label ID="lblOrderDate_Cooking" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="lblsumAmount_Cooking" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblsumTotalPrice_Cooking" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <br />

                            <div class="form-group row">
                                <span class="col-sm-1"></span>
                                <asp:Label CssClass="col-sm-11 col-form-label text-bold" Text="เหตุผลปฏิเสธการสั่งซื้อ" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-2"></span>
                                <asp:DropDownList ID="ddlOrderRejectStatus_Cooking" runat="server" class="form-control col-sm-8"></asp:DropDownList>
                                <span class="col-sm-2"></span>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-1"></span>
                                <asp:Label CssClass="col-sm-11 col-form-label text-bold" Text="หมายเหตุปฏิเสธการสั่งซื้อ" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-2"></span>
                                <asp:TextBox ID="areaOrderRejectStatus_Cooking" TextMode="multiline" Rows="5" class="form-control col-sm-8" runat="server"></asp:TextBox>
                                <span class="col-sm-2"></span>
                            </div>

                            <div class="text-center m-t-20 col-sm-12">
                                <asp:Button ID="btnSubmitRejectForRequest_Cooking" Text="Submit" class="button-pri button-accept m-r-10" runat="server" OnClick="btnSubmitRejectForRequest_Cooking_Click" />
                                <asp:Button ID="btnCancelRejectForRequest_Cooking" Text="Cancel" class="button-pri button-cancel" runat="server" OnClick="btnCancelRejectForRequest_Cooking_Click" />
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
   
    <div id="modalRequestForReject_Cooked" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body ">
                            <div class="form-group row">
                                <asp:Label ID="lblOrderCode_Cooked" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblSaleOrderTypeName_Cooked" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="lblCustomerName_Cooked" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblCustomerContact_Cooked" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label CssClass="col-sm-6 col-form-label text-right" runat="server" Text="วันที่สั่งออเดอร์"></asp:Label>
                                <asp:Label ID="lblOrderDate_Cooked" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="lblsumAmount_Cooked" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblsumTotalPrice_Cooked" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <br />

                            <div class="form-group row">
                                <span class="col-sm-1"></span>
                                <asp:Label CssClass="col-sm-11 col-form-label text-bold" Text="เหตุผลปฏิเสธการสั่งซื้อ" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-2"></span>
                                <asp:DropDownList ID="ddlOrderRejectStatus_Cooked" runat="server" class="form-control col-sm-8"></asp:DropDownList>
                                <span class="col-sm-2"></span>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-1"></span>
                                <asp:Label CssClass="col-sm-11 col-form-label text-bold" Text="หมายเหตุปฏิเสธการสั่งซื้อ" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-2"></span>
                                <asp:TextBox ID="areaOrderRejectStatus_Cooked" TextMode="multiline" Rows="5" class="form-control col-sm-8" runat="server"></asp:TextBox>
                                <span class="col-sm-2"></span>
                            </div>

                            <div class="text-center m-t-20 col-sm-12">
                                <asp:Button ID="btnSubmitRejectForRequest_Cooked" Text="Submit" class="button-active button-submit m-r-10" runat="server" OnClick="btnSubmitRejectForRequest_Cooked_Click" />
                                <asp:Button ID="btnCancelRejectForRequest_Cooked" Text="Cancel" class="button-active button-cancle" runat="server" OnClick="btnCancelRejectForRequest_Cooked_Click" />
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <div id="modalRequestForReject_Delivering" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body ">
                            <div class="form-group row">
                                <asp:Label ID="lblOrderCode_Delivering" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblSaleOrderTypeName_Delivering" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="lblCustomerName_Delivering" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblCustomerContact_Delivering" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label CssClass="col-sm-6 col-form-label text-right" runat="server" Text="วันที่สั่งออเดอร์"></asp:Label>
                                <asp:Label ID="lblOrderDate_Delivering" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="lblsumAmount_Delivering" CssClass="col-sm-6 col-form-label text-right" runat="server"></asp:Label>
                                <asp:Label ID="lblsumTotalPrice_Delivering" CssClass="col-sm-6 col-form-label" runat="server"></asp:Label>
                            </div>

                            <br />

                            <div class="form-group row">
                                <span class="col-sm-1"></span>
                                <asp:Label CssClass="col-sm-11 col-form-label text-bold" Text="เหตุผลปฏิเสธการสั่งซื้อ" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-2"></span>
                                <asp:DropDownList ID="ddlOrderRejectStatus_Delivering" runat="server" class="form-control col-sm-8"></asp:DropDownList>
                                <span class="col-sm-2"></span>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-1"></span>
                                <asp:Label CssClass="col-sm-11 col-form-label text-bold" Text="หมายเหตุปฏิเสธการสั่งซื้อ" runat="server"></asp:Label>
                            </div>

                            <div class="form-group row">
                                <span class="col-sm-2"></span>
                                <asp:TextBox ID="areaOrderRejectStatus_Delivering" TextMode="multiline" Rows="5" class="form-control col-sm-8" runat="server"></asp:TextBox>
                                <span class="col-sm-2"></span>
                            </div>

                            <div class="text-center m-t-20 col-sm-12">
                                <asp:Button ID="btnSubmitRejectForRequest_Delivering" Text="Submit" class="button-pri button-accept m-r-10" runat="server" OnClick="btnSubmitRejectForRequest_Delivering_Click" />
                                <asp:Button ID="btnCancelRejectForRequest_Delivering" Text="Cancel" class="button-pri button-cancel" runat="server" OnClick="btnCancelRejectForRequest_Delivering_Click" />
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {

            var audio = document.getElementsByTagName("audio")[0];

            // Declare a proxy to reference the hub.
            var chat = $.connection.myChatHub;

            // Create a function that the hub can call to broadcast messages.
            chat.client.broadcastMessage = function (name, to, message) {
                // Html encode display name and message.
                //  $('#displayname').val("bvc");
                if (to == $('#<%= hiddisplayname.ClientID %>').val()) {

                    var encodedName = $('<div />').text(name).html();
                    var encodedMsg = $('<div />').text(message).html();
                    // Add the message to the page.
                    $('#discussion').append('<li><strong>' + encodedName
                        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
                    $('#<%= hidordermsg.ClientID %>').val(encodedMsg);
                    // alert("ordermsg=" + $('#<%= hidordermsg.ClientID %>').val());
                    document.getElementById('<%= btnsubmit.ClientID %>').click();
                }
            };

            // Create a function that the hub can call to broadcast messages.
            chat.client.broadcasttest = function (name) {
                alert("New Messege from " + name);

            };

            // Get the user name and store it to prepend to messages.
            //$('#displayname').val(prompt('Enter your name:', ''));
            // $('#yourname').html('Your name = ' + $('#displayname').val());
            // $('#displayname').val('b');
            $('#yourname').html('EmpCode =' + $('#<%= hiddisplayname.ClientID %>').val() + 'BranchCode =' + $('#<%= hidBranchcode.ClientID %>').val());

            // Set initial focus to message input box.
            $('#message').focus();
            // Start the connection.
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {


                    //  alert($('#displayname').val());
                    // alert($('#to').val());
                    // alert($('#message').val());
                    // Call the Send method on the hub.
                    chat.server.myChatSend($('#<%= hiddisplayname.ClientID %>').val(), $('#to').val(), $('#message').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                });
            });



        });

    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="FFChangeGroupOrderStatus_GP.aspx.cs" Inherits="DOMS_TSR.src.FullfillmentOrderManagement.FFChangeGroupOrderStatus_GP" %>

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
    <script type="text/javascript">
        function ApproveConfirmGVDistri() {

            var gridApprove = document.getElementById("<%= gvOrder_Distribute.ClientID %>");
                console.log("asdasdsadasd")
                var cell;
                var sum = 0;
                if (gridApprove.rows.length > 0) {
                    //alert("length=" + grid.rows.length);
                    //loop starts from 1. rows[0] points to the header.
                    for (i = 1; i < gridApprove.rows.length; i++) {
                        //get the reference of first column
                        cell = gridApprove.rows[i].cells[0];
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


                if (sum == 0) {

                    alert("กรุณาเลือกรายการอนุมัติ");

                    return false;

                } else {

                    var MsgDelete = "คุณแน่ใจที่จะอนุมัติข้อมูลนี้ ?";

                    if (confirm(MsgDelete)) {
                      
                    
                    alert("อนุมัติใบสั่งขายสำเร็จ");
                    return true;

                } else {

                   

                    return false;
                }
            }
        }
    </script>
    <asp:HiddenField ID="hidBranchcode" runat="server" />
    <asp:HiddenField ID="hiddisplayname" runat="server" />
    <asp:HiddenField ID="hidordermsg" runat="server" />

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>

            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidTabNo" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">อัพเดทสถานะการสั่งซื้อ</div>
                            </div>

                            <div class="card-body">
                                <div id="searchSection_Distribute" runat="server">
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

                                        <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState" runat="server" class="form-control"></asp:DropDownList>
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
                                        <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click"  class="button-pri button-cancel" runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_Setup" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Setup" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Setup" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Setup" runat="server" TargetControlID="txtSearchOrderDateFrom_Setup" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Setup" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Setup" runat="server" TargetControlID="txtSearchOrderDateUntil_Setup" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Setup" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Setup" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Setup" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Setup" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_Setup" runat="server" class="form-control"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Setup" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Setup" Text="ค้นหา" OnClick="btnSearch_Click_Setup" class="button-pri button-accept m-r-10" runat="server" />
                                        <asp:Button ID="btnClearSearch_Setup" Text="ล้าง" OnClick="btnClearSearch_Click_Setup" class="button-pri button-cancel " runat="server" />
                                    </div>

                                </div>
                                <div id="searchSection_ReadyDeli" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_ReadyDeli" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_ReadyDeli" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_ReadyDeli" runat="server" TargetControlID="txtSearchOrderDateFrom_ReadyDeli" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_ReadyDeli" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_ReadyDeli" runat="server" TargetControlID="txtSearchOrderDateUntil_ReadyDeli" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_ReadyDeli" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_ReadyDeli" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_ReadyDeli" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_ReadyDeli" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_ReadyDeli" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_ReadyDeli" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_ReadyDeli" Text="ค้นหา" OnClick="btnSearch_Click_ReadyDeli"
                                        class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_ReadyDeli" Text="ล้าง" OnClick="btnClearSearch_Click_ReadyDeli"
                                        class="button-pri button-cancel "
                                            runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_FinishDeli" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_FinishDeli" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_FinishDeli" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_FinishDeli" runat="server" TargetControlID="txtSearchOrderDateFrom_FinishDeli" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_FinishDeli" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_FinishDeli" runat="server" TargetControlID="txtSearchOrderDateUntil_FinishDeli" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_FinishDeli" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">ชื่อ - สกุล</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_FinishDeli" class="form-control" placeholder="ชื่อ" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_FinishDeli" class="form-control" placeholder="นามสกุล" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_FinishDeli" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_FinishDeli" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_FinishDeli" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_FinishDeli" Text="ค้นหา" OnClick="btnSearch_Click_FinishDeli"
                                        class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_FinishDeli" Text="ล้าง" OnClick="btnClearSearch_Click_FinishDeli"
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

                                        <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_OrderCancelled" runat="server" class="form-control"></asp:DropDownList>
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
                                        class="button-pri button-cancel "
                                            runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="card">
                            <div class="card-group">

                                <div class="card" id="cardex1" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable  h-120"  ID="showSection_Distribute" title="Hello World!" OnClick="showSection_Distribute_Click" runat="server">
                                        <div id="listcard1" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                  <img src="../../Image/icon/cart.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Distribute" runat="server"></asp:Label></h3>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">บันทึกสั่งซื้อ</p>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex2" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable2 h-120 " ID="showSection_Setup" OnClick="showSection_Setup_Click" runat="server">
                                        <div id="listcard2" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                  <img src="../../Image/icon/invoice.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Setup" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">พิมพ์ใบ Invoice</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>


                                <div class="card" id="cardex4" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable4 h-120 " ID="showSection_ReadyDeli" OnClick="showSection_ReadyDeli_Click" runat="server">
                                        <div id="listcard4" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                <img src="../../Image/icon/box.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_ReadyDeli" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">เช็คและจัดเตรียมสินค้า</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                 <div class="card" id="Div5" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable  h-120"  ID="LinkButton3" title="Hello World!" OnClick="showSection_Distribute_Click" runat="server">
                                        <div id="Div6" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                  <img src="../../Image/icon/cancel.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="Label3" runat="server" Text="1"></asp:Label></h3>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">ไม่มีสินค้าในคลัง</p>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                                    <div class="card" id="Div7" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable  h-120"  ID="LinkButton4" title="Hello World!" OnClick="showSection_Distribute_Click" runat="server">
                                        <div id="Div8" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                  <img src="../../Image/icon/fail.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="Label4" runat="server" Text="3"></asp:Label></h3>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">ไม่ผ่าน QC</p>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                                <div class="card" id="cardex5" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable5 h-120 " ID="showSection_FinishDeli" OnClick="showSection_FinishDeli_Click" runat="server">
                                        <div id="listcard5" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                     <img src="../../Image/icon/search.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_FinishDeli" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">ตรวจสอบสินค้า</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                           

                                <div class="card" id="cardex8" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable8 h-120 " ID="showSection_OrderCancelled" OnClick="showSection_OrderCancelled_Click" runat="server">

                                        <div id="listcard8" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    
                                                       <img src="../../Image/icon/shipped.svg" width="20px" />
                                                 
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_OrderCancelled" runat="server"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">รอรถรับของ</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                                     <div class="card" id="Div1" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable5 h-120 " ID="LinkButton1" OnClick="showSection_FinishDeli_Click" runat="server">
                                        <div id="Div2" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <img src="../../Image/icon/shipped.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="Label1" runat="server" Text="1"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">รถรับของแล้ว</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                                <div class="card" id="Div3" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable2 h-120 " ID="LinkButton2" OnClick="showSection_Setup_Click" runat="server">
                                        <div id="Div4" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                       <img src="../../Image/icon/cancel.svg" width="20px" />
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="Label2" runat="server" Text="2"></asp:Label></h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">สินค้าถูกยกเลิก</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="card-body">

                                <div id="Section_Distribute" runat="server">
                                    <div>
                                        <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_Distribute" OnClick="btnAcceptOrder_Distribute_Click"   runat="server" Text="อัพเดท"  OnClientClick="return ApproveConfirmGVDistri();" />
                                    </div>
                                    <asp:Panel ID="Panel_Distribute" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Distribute" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                            <Columns>
                                                 <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkAll_Order" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_Change_Distribute" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_ByOrder" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                        <asp:HiddenField ID="hidOrderCode_GvvDistribute" runat="server"  Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                        <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "CustomerCode")) %>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">สถานะจัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">แบรนด์</div>
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
                                                        <td style="font-size: 8.5pt;">
                                                            Page
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
                                                            <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div id="Section_Setup" runat="server">
                                    <div>
                                        <asp:Button CssClass="button-pri button-print  m-b-10" ID="Button1" OnClick="btnAcceptOrder_Setup_Click" runat="server" Text="อัพเดท" OnClientClick="return ApproveConfirmGVDistri();" />
      

                                    </div>
                                    <asp:Panel ID="Panel_Setup" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Setup" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                            <Columns>
                                                 <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkAll_Setup" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_Change_Setup" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Setup" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Setup" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                          <asp:HiddenField ID="hidOrderCode_Setup" runat="server"  Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Setup" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Setup" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Setup" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Setup" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">สถานะจัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Setup" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Setup" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_Setup" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_Setup" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_Setup"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Setup" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_Setup"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_Setup" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Setup"></asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_Setup" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Setup" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_Setup"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Setup" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Setup"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>





                                </div>

                                <div id="Section_ReadyDeli" runat="server">
                                     <div>
                                        <asp:Button CssClass="button-pri button-print  m-b-10" ID="Button2" OnClick="btnAcceptOrder_ReadyDeli_Click" runat="server" Text="อัพเดท" OnClientClick="return ApproveConfirmGVDistri();" />
                                                                    <asp:Button CssClass="button-pri button-cancel   m-b-10" ID="Button3"  runat="server" Text="ตีกลับ" OnClientClick="return ApproveConfirmGVDistri();" />
                                         </div>
                                    <asp:Panel ID="Panel_ReadyDeli" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_ReadyDeli" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                            <Columns>
                                                 <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkAll_ReadyDeli" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_Change_ReadyDeli" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_ReadyDeli" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>  

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_ReadyDeli" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                        <asp:HiddenField ID="hidOrderCode_ReadyDeli" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_ReadyDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_ReadyDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_ReadyDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderDate_ReadyDeli" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                     <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">สถานะจัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_ReadyDeli" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_ReadyDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_ReadyDeli" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_ReadyDeli" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_ReadyDeli"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_ReadyDeli" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_ReadyDeli"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_ReadyDeli" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_ReadyDeli"></asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_ReadyDeli" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_ReadyDeli" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_ReadyDeli"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_ReadyDeli" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_ReadyDeli"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div id="Section_FinishDeli" runat="server">
                                <asp:Button CssClass="button-pri button-print  m-b-10" ID="Button4" OnClick="btnAcceptOrder_FinishDeli_Click" runat="server" Text="อัพเดท" OnClientClick="return ApproveConfirmGVDistri();" />
                                                                    <asp:Button CssClass="button-pri button-cancel   m-b-10" ID="Button5"  runat="server" Text="ตีกลับ" OnClientClick="return ApproveConfirmGVDistri();" />
                                    <asp:Panel ID="Panel_FinishDeli" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_FinishDeli" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                            
                                            <Columns>
                                                           <asp:TemplateField HeaderStyle-CssClass="TDHead" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-CssClass="TDDetail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkAll_FinishDeli" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_Change_FinishDeli" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_FinishDeli" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_FinishDeli" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                       <asp:HiddenField ID="hidOrderCode_FinishDeli" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' />

                                                        </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_FinishDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_FinishDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_FinishDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_FinishDeli" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                     <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">สถานะจัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_FinishDeli" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">แบรนด์</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_FinishDeli" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty_FinishDeli" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                                            <asp:Button ID="lnkbtnFirst_FinishDeli" CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex_FinishDeli"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_FinishDeli" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                Text="<" runat="server" OnCommand="GetPageIndex_FinishDeli"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_FinishDeli" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_FinishDeli"></asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_FinishDeli" CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_FinishDeli" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_FinishDeli"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_FinishDeli" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_FinishDeli"></asp:Button>
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


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Left">รหัสใบสั่งขาย</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสลูกค้า</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อ - สกุล</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">เบอร์ติดต่อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">วันที่สั่งซื้อ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>' runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_OrderCancelled" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                     <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">สถานะจัดส่ง</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">หมายเหตุ</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_OrderCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">แบรนด์</div>
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
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_OrderCancelled" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_OrderCancelled"></asp:DropDownList>
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

                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <audio id="audio1" src="call.wav" controls preload="auto" autobuffer hidden="true">
                <asp:Button ID="btnsubmit" OnClick="btnsubmit_Click" runat="server" Style="margin-left: -900px;" />
        </contenttemplate>
    </asp:UpdatePanel>

</asp:Content>

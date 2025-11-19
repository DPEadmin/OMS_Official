<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="DOMS_TSR.src.OrderManagement.OrderManagement" %>

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
                } else clearInterval(player)
            }, trackLength);

        }
    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:HiddenField ID="hidMerchantCode" runat="server" />
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
                                <div class="sub-title">Sales Order Search</div>
                            </div>

                            <div class="card-block">
                                <div id="searchSection_All" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSearchOrderCode" class="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>



                                        <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                        <div class="col-sm-4">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom" class="form-control"
                                                    placeholder="Start" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom" runat="server"
                                                    TargetControlID="txtSearchOrderDateFrom" PopupButtonID="Image1"
                                                    Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil" class="form-control"
                                                    placeholder="To" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil"
                                                    runat="server" TargetControlID="txtSearchOrderDateUntil"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>

                                        <label class="col-sm-2 col-form-label">Customer Code</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSearchCustomerCode" class="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>



                                        <label class="col-sm-2 col-form-label">Customer Name</label>
                                        <div class="col-sm-4">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName" class="form-control" placeholder="First Name"
                                                    runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName" class="form-control"
                                                    placeholder="Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <label class="col-sm-2 col-form-label">Contact Number</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSearchContact" class="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>



                                        <label class="col-sm-2 col-form-label">Delivery Status</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlSearchOrderState" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>


                                        <label class="col-sm-2 col-form-label">Brand</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlSearchCamCate" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"
                                            class="button-pri button-accept m-r-10" runat="server" />
                                        <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click"
                                            class="button-pri button-cancel" runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_NewOrder" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_NewOrder" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_NewOrder" class="form-control"
                                                    placeholder="Start" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_NewOrder"
                                                    runat="server" TargetControlID="txtSearchOrderDateFrom_NewOrder"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_NewOrder" class="form-control"
                                                    placeholder="To" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_NewOrder"
                                                    runat="server" TargetControlID="txtSearchOrderDateUntil_NewOrder"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Customer Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_NewOrder" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Customer Name</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_NewOrder" class="form-control"
                                                    placeholder="First Name" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_NewOrder" class="form-control"
                                                    placeholder="Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Contact Number</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_NewOrder" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Delivery Status</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_NewOrder" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Brand</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_NewOrder" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_NewOrder" Text="Search"
                                            OnClick="btnSearch_Click_NewOrder" class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_NewOrder" Text="Clear"
                                            OnClick="btnClearSearch_Click_NewOrder" class="button-pri button-cancel "
                                            runat="server" />
                                    </div>

                                </div>
                                <div id="searchSection_Cooking" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Cooking" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Cooking" class="form-control"
                                                    placeholder="Start" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Cooking"
                                                    runat="server" TargetControlID="txtSearchOrderDateFrom_Cooking"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Cooking" class="form-control"
                                                    placeholder="To" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Cooking"
                                                    runat="server" TargetControlID="txtSearchOrderDateUntil_Cooking"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Customer Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Cooking" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Customer Name</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Cooking" class="form-control"
                                                    placeholder="First Name" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Cooking" class="form-control"
                                                    placeholder="Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Contact Number</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Cooking" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Delivery Status</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_Cooking" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Brand</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Cooking" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Cooking" Text="Search"
                                            OnClick="btnSearch_Click_Cooking" class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_Cooking" Text="Clear"
                                            OnClick="btnClearSearch_Click_Cooking" class="button-pri button-cancel "
                                            runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_Cooked" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Cooked" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Cooked" class="form-control"
                                                    placeholder="Start" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Cooked"
                                                    runat="server" TargetControlID="txtSearchOrderDateFrom_Cooked"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Cooked" class="form-control"
                                                    placeholder="To" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Cooked"
                                                    runat="server" TargetControlID="txtSearchOrderDateUntil_Cooked"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Customer Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Cooked" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Customer Name</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Cooked" class="form-control"
                                                    placeholder="First Name" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Cooked" class="form-control"
                                                    placeholder="Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Contact Number</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Cooked" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Delivery Status</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_Cooked" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Brand</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Cooked" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Cooked" Text="Search" OnClick="btnSearch_Click_Cooked"
                                            class="button-pri button-accept m-r-10" runat="server" />
                                        <asp:Button ID="btnClearSearch_Cooked" Text="Clear"
                                            OnClick="btnClearSearch_Click_Cooked" class="button-pri button-cancel "
                                            runat="server" />
                                    </div>

                                </div>
                                <div id="searchSection_Delivering" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Delivering" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Delivering" class="form-control"
                                                    placeholder="Start" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Delivering"
                                                    runat="server" TargetControlID="txtSearchOrderDateFrom_Delivering"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Delivering"
                                                    class="form-control" placeholder="To" runat="server"
                                                    AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Delivering"
                                                    runat="server" TargetControlID="txtSearchOrderDateUntil_Delivering"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Customer Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Delivering" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Customer Name</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Delivering" class="form-control"
                                                    placeholder="First Name" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Delivering" class="form-control"
                                                    placeholder="Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Contact Number</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Delivering" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Delivery Status</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_Delivering" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Brand</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Delivering" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Delivering" Text="Search"
                                            OnClick="btnSearch_Click_Delivering" class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_Delivering" Text="Clear"
                                            OnClick="btnClearSearch_Click_Delivering" class="button-pri button-cancel "
                                            runat="server" />
                                    </div>
                                </div>
                                <div id="searchSection_Delivered" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_Delivered" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_Delivered" class="form-control"
                                                    placeholder="Start" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_Delivered"
                                                    runat="server" TargetControlID="txtSearchOrderDateFrom_Delivered"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_Delivered" class="form-control"
                                                    placeholder="To" runat="server" AutoCompleteType="Disabled">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_Delivered"
                                                    runat="server" TargetControlID="txtSearchOrderDateUntil_Delivered"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Customer Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_Delivered" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Customer Name</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_Delivered" class="form-control"
                                                    placeholder="First Name" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_Delivered" class="form-control"
                                                    placeholder="Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Contact Number</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_Delivered" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Delivery Status</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_Delivered" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Brand</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_Delivered" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_Delivered" Text="Search"
                                            OnClick="btnSearch_Click_Delivered" class="button-pri button-accept m-r-10"
                                            runat="server" />
                                        <asp:Button ID="btnClearSearch_Delivered" Text="Clear"
                                            OnClick="btnClearSearch_Click_Delivered" class="button-pri button-cancel "
                                            runat="server" />
                                    </div>

                                </div>
                                <div id="searchSection_OrderCancelled" runat="server">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Sales Order Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchOrderCode_OrderCancelled" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Sales Order Date</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">

                                                <asp:TextBox ID="txtSearchOrderDateFrom_OrderCancelled"
                                                    class="form-control" placeholder="Start" runat="server"
                                                    AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_OrderCancelled"
                                                    runat="server"
                                                    TargetControlID="txtSearchOrderDateFrom_OrderCancelled"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                                <asp:TextBox ID="txtSearchOrderDateUntil_OrderCancelled"
                                                    class="form-control" placeholder="To" runat="server"
                                                    AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender
                                                    ID="carSearchOrderDateUntil_OrderCancelled" runat="server"
                                                    TargetControlID="txtSearchOrderDateUntil_OrderCancelled"
                                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Customer Code</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchCustomerCode_OrderCancelled" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Customer Name</label>
                                        <div class="col-sm-3">
                                            <div class="input-group mb-0">
                                                <asp:TextBox ID="txtSearchFName_OrderCancelled" class="form-control"
                                                    placeholder="First Name" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchLName_OrderCancelled" class="form-control"
                                                    placeholder="Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Contact Number</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearchContact_OrderCancelled" class="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-1"></div>

                                        <label class="col-sm-2 col-form-label">Delivery Status</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchOrderState_OrderCancelled" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Brand</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlSearchCamCate_OrderCancelled" runat="server"
                                                class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="text-center m-t-20 col-sm-12">
                                        <asp:Button ID="btnSearch_OrderCancelled" Text="Search"
                                            OnClick="btnSearch_Click_OrderCancelled"
                                            class="button-pri button-accept m-r-10" runat="server" />
                                        <asp:Button ID="btnClearSearch_OrderCancelled" Text="Clear"
                                            OnClick="btnClearSearch_Click_OrderCancelled"
                                            class="button-pri button-cancel " runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="card">
                            <div class="card-group">
                                <div class="card" id="cardex1" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable" ID="showSection_All"
                                        title="Hello World!" OnClick="showSection_All_Click" runat="server">
                                        <div id="listcard1" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class=" ti-layout-list-thumb-alt text-c-blue f-30"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_All" runat="server"></asp:Label>
                                                    </h3>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">All</p>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex2" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable2" ID="showSection_NewOrder"
                                        OnClick="showSection_NewOrder_Click" runat="server">
                                        <div id="listcard2" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fi fi-3x flaticon-new text-c-blue  "></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_NewOrder" runat="server">
                                                        </asp:Label>
                                                    </h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">Created Sales Order</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>


                                <div class="card" id="cardex4" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable4" ID="showSection_Cooking"
                                        OnClick="showSection_Cooking_Click" runat="server">
                                        <div id="listcard4" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fa fi-3x fa-box-open text-c-blue "></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Cooking" runat="server"></asp:Label>
                                                    </h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">Preparing Order</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex5" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable5" ID="showSection_Cooked"
                                        OnClick="showSection_Cooked_Click" runat="server">
                                        <div id="listcard5" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="ion-checkmark-circled text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Cooked" runat="server"></asp:Label>
                                                    </h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">Waiting For Delivery</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex6" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable6" ID="showSection_Delivering"
                                        OnClick="showSection_Delivering_Click" runat="server">

                                        <div id="listcard6" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fi flaticon-truck text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Delivering" runat="server">
                                                        </asp:Label>
                                                    </h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">Delivery In Progress</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex7" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable7" ID="showSection_Delivered"
                                        OnClick="showSection_Delivered_Click" runat="server">

                                        <div id="listcard7" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="fi flaticon-truck-1 text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_Delivered" runat="server">
                                                        </asp:Label>
                                                    </h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">Delivery Completed</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                                <div class="card" id="cardex8" runat="server">
                                    <asp:LinkButton CssClass="btn-8bar-disable8" ID="showSection_OrderCancelled"
                                        OnClick="showSection_OrderCancelled_Click" runat="server">

                                        <div id="listcard8" runat="server">
                                            <div class="row">
                                                <div class="col-3 text-left p-b-15">
                                                    <i class="ion-close-circled text-c-blue fi-3x"></i>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <h3 class="text-c-blue">
                                                        <asp:Label ID="countSection_OrderCancelled" runat="server">
                                                        </asp:Label>
                                                    </h3>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-left">
                                                    <p class=" m-0">Cancel Sales Order</p>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>

                            </div>
                            <div class="card-block" style="padding: .9rem;">

                                <div id="Section_All" runat="server">

                                    <asp:Panel ID="Panel_All" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_All" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>'
                                                            runat="server" />
                                                        <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "CustomerCode")) %>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Contact Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>'
                                                        runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate"
                                                            Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>'
                                                            runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Stage</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatusName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Status</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Notes</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                       <asp:TemplateField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Merchant</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMerchant_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server"
                                                        Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
<div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0"
                                                    style="vertical-align: middle;">
                                                    <tr>

                                                        <td>
                                                            <asp:Button ID="lnkbtnFirst" CssClass="Button pagina_btn"
                                                                ToolTip="First" CommandName="First" Text="<<"
                                                                runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre" CssClass="Button pagina_btn"
                                                                ToolTip="Previous" CommandName="Previous" Text="<"
                                                                runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage" CssClass="textbox"
                                                                runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages" CssClass="fontBlack"
                                                                runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext" CssClass="Button pagina_btn" ToolTip="Next"
                                                                runat="server" CommandName="Next" Text=">"
                                                                OnCommand="GetPageIndex"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast" CssClass="Button pagina_btn" ToolTip="Last"
                                                                runat="server" CommandName="Last" Text=">>"
                                                                OnCommand="GetPageIndex"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                </div>

                                <div id="Section_NewOrder" runat="server">

                                    <asp:Panel ID="Panel_NewOrder" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_NewOrder" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_NewOrder"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_NewOrder"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_NewOrder"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Contact Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_NewOrder"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>'
                                                        runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_NewOrder"
                                                            Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>'
                                                            runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Stage</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatusName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Status</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Notes</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_NewOrder"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_NewOrder"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                          <asp:TemplateField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Merchant </div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMerchant_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty_NewOrder" class="fontBlack"
                                                        runat="server" Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
<div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0"
                                                    style="vertical-align: middle;">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="lnkbtnFirst_NewOrder" CssClass="Button pagina_btn"
                                                                ToolTip="First" CommandName="First" Text="<<"
                                                                runat="server" OnCommand="GetPageIndex_NewOrder">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_NewOrder" CssClass="Button pagina_btn"
                                                                ToolTip="Previous" CommandName="Previous" Text="<"
                                                                runat="server" OnCommand="GetPageIndex_NewOrder">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_NewOrder" CssClass="textbox"
                                                                runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_NewOrder">
                                                            </asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_NewOrder" CssClass="fontBlack"
                                                                runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_NewOrder" CssClass="Button pagina_btn"
                                                                ToolTip="Next" runat="server" CommandName="Next"
                                                                Text=">" OnCommand="GetPageIndex_NewOrder"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_NewOrder" CssClass="Button pagina_btn"
                                                                ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_NewOrder">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
</div>




                                </div>

                                <div id="Section_Cooking" runat="server">
                                    <asp:Panel ID="Panel_Cooking" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Cooking" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Cooking"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Cooking"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Cooking"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Contact Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Cooking"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderDate_Cooking"
                                                            Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>'
                                                            runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Stage</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatusName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Status</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Notes</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Cooking"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Cooking"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                          <asp:TemplateField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Merchant </div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMerchant_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Tracking Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderTracking_Cooking"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderTrackingNo")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty_Cooking" class="fontBlack"
                                                        runat="server" Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0"
                                                    style="vertical-align: middle;">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="lnkbtnFirst_Cooking" CssClass="Button pagina_btn"
                                                                ToolTip="First" CommandName="First" Text="<<"
                                                                runat="server" OnCommand="GetPageIndex_Cooking">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Cooking" CssClass="Button pagina_btn"
                                                                ToolTip="Previous" CommandName="Previous" Text="<"
                                                                runat="server" OnCommand="GetPageIndex_Cooking">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_Cooking" CssClass="textbox"
                                                                runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Cooking">
                                                            </asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_Cooking" CssClass="fontBlack"
                                                                runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Cooking" CssClass="Button pagina_btn"
                                                                ToolTip="Next" runat="server" CommandName="Next"
                                                                Text=">" OnCommand="GetPageIndex_Cooking"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Cooking" CssClass="Button pagina_btn"
                                                                ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Cooking"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
</div>
                                </div>

                                <div id="Section_Cooked" runat="server">

                                    <asp:Panel ID="Panel_Cooked" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Cooked" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Cooked"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead"
                                                    ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Cooked"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Cooked"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Contact Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Cooked"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>'
                                                        runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Cooked"
                                                            Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>'
                                                            runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ขั้</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatusName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Status</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Notes</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Cooked"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Cooked"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Tracking Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderTracking_Cooked"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderTrackingNo")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                          <asp:TemplateField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Merchant </div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMerchant_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                            </Columns>


                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty_Cooked" class="fontBlack" runat="server"
                                                        Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0"
                                                    style="vertical-align: middle;">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="lnkbtnFirst_Cooked" CssClass="Button pagina_btn"
                                                                ToolTip="First" CommandName="First" Text="<<"
                                                                runat="server" OnCommand="GetPageIndex_Cooked">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Cooked" CssClass="Button pagina_btn"
                                                                ToolTip="Previous" CommandName="Previous" Text="<"
                                                                runat="server" OnCommand="GetPageIndex_Cooked">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_Cooked" CssClass="textbox"
                                                                runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Cooked">
                                                            </asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_Cooked" CssClass="fontBlack"
                                                                runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Cooked" CssClass="Button pagina_btn"
                                                                ToolTip="Next" runat="server" CommandName="Next"
                                                                Text=">" OnCommand="GetPageIndex_Cooked"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Cooked" CssClass="Button pagina_btn"
                                                                ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Cooked"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
</div>
                                </div>

                                <div id="Section_Delivering" runat="server">

                                    <asp:Panel ID="Panel_Delivering" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Delivering" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Delivering"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Delivering"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Delivering"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Contact Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Delivering"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>'
                                                        runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Delivering"
                                                            Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>'
                                                            runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Stage</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatusName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Status</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Notes</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Delivering"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Delivering"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Tracking Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderTracking_Delivering"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderTrackingNo")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                          <asp:TemplateField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Merchant </div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMerchant_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty_Delivering" class="fontBlack"
                                                        runat="server" Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0"
                                                    style="vertical-align: middle;">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="lnkbtnFirst_Delivering" CssClass="Button pagina_btn"
                                                                ToolTip="First" CommandName="First" Text="<<"
                                                                runat="server" OnCommand="GetPageIndex_Delivering">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Delivering" CssClass="Button pagina_btn"
                                                                ToolTip="Previous" CommandName="Previous" Text="<"
                                                                runat="server" OnCommand="GetPageIndex_Delivering">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_Delivering" CssClass="textbox"
                                                                runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Delivering">
                                                            </asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_Delivering"
                                                                CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Delivering" CssClass="Button pagina_btn"
                                                                ToolTip="Next" runat="server" CommandName="Next"
                                                                Text=">" OnCommand="GetPageIndex_Delivering">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Delivering" CssClass="Button pagina_btn"
                                                                ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Delivering">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                </div>

                                <div id="Section_Delivered" runat="server">
                                    <asp:Panel ID="Panel_Delivered" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_Delivered" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Left">Sales Order Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_Delivered"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_Delivered"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_Delivered"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Contact Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_Delivered"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>'
                                                        runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_Delivered"
                                                            Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>'
                                                            runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Stage</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatusName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Status</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Notes</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_Delivered"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_Delivered"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Tracking Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderTracking_Delivered"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderTrackingNo")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                          <asp:TemplateField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Merchant </div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMerchant_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty_Delivered" class="fontBlack"
                                                        runat="server" Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0"
                                                    style="vertical-align: middle;">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="lnkbtnFirst_Delivered" CssClass="Button pagina_btn"
                                                                ToolTip="First" CommandName="First" Text="<<"
                                                                runat="server" OnCommand="GetPageIndex_Delivered">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_Delivered" CssClass="Button pagina_btn"
                                                                ToolTip="Previous" CommandName="Previous" Text="<"
                                                                runat="server" OnCommand="GetPageIndex_Delivered">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_Delivered" CssClass="textbox"
                                                                runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Delivered">
                                                            </asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_Delivered" CssClass="fontBlack"
                                                                runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_Delivered" CssClass="Button pagina_btn"
                                                                ToolTip="Next" runat="server" CommandName="Next"
                                                                Text=">" OnCommand="GetPageIndex_Delivered">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_Delivered" CssClass="Button pagina_btn"
                                                                ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_Delivered">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
</div>
                                </div>

                                <div id="Section_OrderCancelled" runat="server">
                                    <asp:Panel ID="Panel_OrderCancelled" runat="server" Style="overflow-x: scroll;">
                                        <asp:GridView ID="gvOrder_OrderCancelled" runat="server"
                                            AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0"
                                            Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                            <Columns>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead"
                                                    ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Left">Sales Order Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderCode_OrderCancelled"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Code</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerCode_OrderCancelled"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Customer Name</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName_OrderCancelled"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Contact Number</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerContact_OrderCancelled"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CustomerContact")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Date</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>'
                                                        runat="server" />--%>
                                                        <asp:Label ID="lblOrderDate_OrderCancelled"
                                                            Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy HH:mm:ss") %>'
                                                            runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Stage</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatusName"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatusName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Status</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderStateName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Sales Order Notes</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderRejectRemark_OrderCancelled"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderNote")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px"
                                                    HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">Brand</div>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrandName_OrderCancelled"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                          <asp:TemplateField ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Merchant </div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMerchant_NoAnswerOrder" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblDataEmpty_OrderCancelled" class="fontBlack"
                                                        runat="server" Text="Data not Found"></asp:Label>
                                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0"
                                                    style="vertical-align: middle;">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="lnkbtnFirst_OrderCancelled"
                                                                CssClass="Button" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server"
                                                                OnCommand="GetPageIndex_OrderCancelled"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre_OrderCancelled" CssClass="Button pagina_btn"
                                                                ToolTip="Previous" CommandName="Previous" Text="<"
                                                                runat="server" OnCommand="GetPageIndex_OrderCancelled">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td style="font-size: 8.5pt;">
                                                            Page
                                                            <asp:DropDownList ID="ddlPage_OrderCancelled"
                                                                CssClass="textbox" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_OrderCancelled">
                                                            </asp:DropDownList>
                                                            of
                                                            <asp:Label ID="lblTotalPages_OrderCancelled"
                                                                CssClass="fontBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnNext_OrderCancelled" CssClass="Button pagina_btn"
                                                                ToolTip="Next" runat="server" CommandName="Next"
                                                                Text=">" OnCommand="GetPageIndex_OrderCancelled">
                                                            </asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast_OrderCancelled" CssClass="Button pagina_btn"
                                                                ToolTip="Last" runat="server" CommandName="Last"
                                                                Text=">>" OnCommand="GetPageIndex_OrderCancelled">
                                                            </asp:Button>
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
            <audio id="audio1" src="call.wav" controls preload="auto" autobuffer hidden="true">
                <asp:Button ID="btnsubmit" OnClick="btnsubmit_Click" runat="server" Style="margin-left: -900px;" />
        </contenttemplate>
    </asp:UpdatePanel>

</asp:Content>
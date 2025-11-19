<%@ Page Title="" Language="C#"  MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="REP00045.aspx.cs" Inherits="DOMS_TSR.src.Report.REP00045" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="/code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
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
                                        <div class="sub-title">ค้นหาข้อมูลมีเดีย</div>
                                    </div>

                                    <div class="card-body">

                                        <div id="searchSection_NoAnswerOrder" runat="server">

                                            <div class="form-group row">
     
                                                <label class="col-sm-2 col-form-label">ช่องทางการสั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlSearchMedia" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                           

                                                <div class="col-sm-1"></div>

                                                <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                                <div class="col-sm-3">
                                                    <div class="input-group mb-0">

                                                        <asp:TextBox ID="txtSearchOrderDateFrom_NoAnswerOrder" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateFrom_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateFrom_NoAnswerOrder" PopupButtonID="Image1" Format="yyyy-MM-dd"></ajaxToolkit:CalendarExtender>

                                                        <asp:TextBox ID="txtSearchOrderDateUntil_NoAnswerOrder" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="carSearchOrderDateUntil_NoAnswerOrder" runat="server" TargetControlID="txtSearchOrderDateUntil_NoAnswerOrder" PopupButtonID="Image1" Format="yyyy-MM-dd"></ajaxToolkit:CalendarExtender>

                                                    </div>
                                                </div>
                                            </div>

                               
                                            <div class="text-center m-t-20 col-sm-12">
                                                <asp:Button ID="btnSearch_NoAnswerOrder" Text="ค้นหา" OnClick="btnSearch_Click_NoAnswerOrder" class="button-pri button-accept m-r-10" runat="server" />
                                                <asp:Button ID="btnClearSearch_NoAnswerOrder" Text="ล้าง" OnClick="btnClearSearch_Click_NoAnswerOrder" class="button-pri button-cancel" runat="server" />
                                            </div>

                                        </div>

                                    </div>

                                </div>
                            
                                 
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
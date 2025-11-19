<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="OrderIncompleteBackOrderDetail.aspx.cs" Inherits="DOMS_TSR.src.OrderManagement.OrderIncompleteBackOrderDetail" %>

<%@ Register Src="~/src/UserControl/OrderDetail.ascx" TagPrefix="uc1" TagName="OrderDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/Chart.js-master/css js/Chart.css">
    <link rel="icon" href="favicon.ico">
    <script src="../../Scripts/Chart.js-master/css js/Chart.js"></script>
    <script src="../../Scripts/utils.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
              <asp:HiddenField ID="hidEmpCode" runat="server" />

            <uc1:OrderDetail runat="server" id="OrderDetail" />
            <div class="card">
        <div class="card-header border-0">
            <div class="sub-title">อัพเดทสถานะใบสั่งขาย</div>
        </div>

        <div class="card-body">


            <div class="form-group row">
                <label class="col-sm-3 col-form-label">ขั้นตอน</label>
                <div class="col-sm-9">
                       <asp:DropDownList ID="ddlSearchOrderstatus" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <label class="col-sm-3 col-form-label">สถานะใบสั่งขาย</label>
                <div class="col-sm-9">
                      <asp:DropDownList ID="ddlSearchOrderstate" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <label class="col-sm-3 col-form-label">หมายเหตุ</label>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtDetailbackOrder" class="form-control" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="text-center m-t-20 col-sm-12">
            <asp:Button ID="Button3" Text="Submit" OnClick="btnsubmit_Click"
                class="button-pri button-accept m-r-10"
                runat="server" />
            <asp:Button ID="Button1" Text="ตี Back Oreder" OnClick="btnsubmit_Click" Visible="false"
                class="button-pri button-accept m-r-10"
                runat="server" />
            <asp:Button ID="Button2" Text="Cancel" OnClick="btnClear_Click"
                class="button-pri button-cancel"
                runat="server" />

        </div>
    </div>
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>


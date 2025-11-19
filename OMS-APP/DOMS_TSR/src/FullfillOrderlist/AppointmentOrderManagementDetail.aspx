<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="AppointmentOrderManagementDetail.aspx.cs" Inherits="DOMS_TSR.src.FullfillOrderlist.AppointmentOrderManagementDetail" %>

<%@ Register Src="~/src/UserControl/OrderDetail.ascx" TagPrefix="uc1" TagName="OrderDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/Chart.js-master/css js/Chart.css">
    <link rel="icon" href="favicon.ico">
    <script src="../../Scripts/Chart.js-master/css js/Chart.js"></script>
    <script src="../../Scripts/utils.js"></script>
    <script type="text/javascript">

        
        function ApproveConfirm() {

            var gridApprove = document.getElementById("<%= Button3.ClientID %>");
              console.log("asdasdsadasd")
         


                  var MsgDelete = "คุณแน่ใจที่จะเปลียนสถานะใบสั่งขายนี้ ?";

                  if (confirm(MsgDelete)) {
                    
                      alert("เปลียนสถานะใบสั่งขายสำเร็จ");
                     return true;
                  }
                  else
                  {
                      return false;
                  }
             
          }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
              <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidMerchantMapcode" runat="server" />
            <asp:HiddenField ID="hidMerchantMapName" runat="server" />
            <uc1:OrderDetail runat="server" id="OrderDetail" />
            <div class="card">
        <div class="card-header border-0">
            <div class="sub-title">Update sales order status
                    <%-- <span><asp:Button ID="btnEditOrder" Text="แก้ไขใบสั่งขาย" OnClick="btnEditOrder_Click"
                        class="button-action button-accept m-r-10 m-r-10 p-t-0 p-b-0"
                        runat="server" /></span>--%>
            </div>
            
        
        </div>

        <div class="card-body">


            <div class="form-group row">
                <label class="col-sm-3 col-form-label">Sales Order Stage</label>
                <div class="col-sm-9">
                       <asp:DropDownList ID="ddlOrderstatus" runat="server" class="form-control"></asp:DropDownList>
                     <asp:Label ID="Lbstatus" runat="server" CssClass="validatecolor"></asp:Label>
                </div>
                <label class="col-sm-3 col-form-label">sales order status</label>
            
                <div class="col-sm-9">
                      <asp:DropDownList ID="ddlOrderstate" runat="server" class="form-control"></asp:DropDownList>
                         <asp:Label ID="Lbstate" runat="server" CssClass="validatecolor"></asp:Label>
                </div>
                <label class="col-sm-3 col-form-label">Remark</label>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtDetailbackOrder" class="form-control" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>

            <div class="text-center m-t-20 col-sm-12">
            <asp:Button ID="Button3" Text="Submit" OnClick="btnsubmit_Click"
                class="button-pri button-accept m-r-10" OnClientClick="return ApproveConfirm();"
                runat="server" />
          
            <asp:Button ID="Button1" Text="ตี Back Order" OnClick="btnsubmit_Click" Visible="false"
                class="button-pri button-accept m-r-10"
                runat="server" />
            <asp:Button ID="Button2" Text="Clear" OnClick="btnClear_Click"
                class="button-pri button-cancel"
                runat="server" />

        </div>
        </div>

        
    </div>
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>


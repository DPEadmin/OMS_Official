

<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/WebReatil.Master" AutoEventWireup="true" CodeBehind="TakeOrder.aspx.cs" Inherits="DOMS_TSR.src.TakeOrderRetail.TakeOrder" %>
<%@ Register Src="~/src/UserControl/SelectBranch.ascx" TagName="SelectBranch" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>

    .aspNetDisabled
    {
    color: white !important;
    }
    .modal {
        overflow-y:auto;
    }

    .card .card-header{
      padding: 0px 20px 0px 10px;
    }
    .sub-title{
      padding-bottom: 0px;
    
    }
    .card-block {
      padding: 0.60rem;
      padding-top: 0rem;
     
    }
    /* .card .card-header .card-header-right{
      top:7px;
    } */
    .card {
      margin-bottom: 10px
    }
    .col-pad-l{
      padding-left: 5px;
      
    }
    .col-pad-r{
      padding-right: 5px;
      
    }
    .form-control{
      padding: 0;
    }
    .col-form-label{
      font-size:12px
    }
    .checkbox-fade .cr, .checkbox-zoom .cr{
      height: 15px;
      width: 15px;
    } 
    
    #btn-md{
      line-height:23px
    }
    /* .table td,.table th{
     
      font-size: 14px;
      padding: 1px;
    } */
    .col-form-label{
      margin-bottom: 0px;
      padding-top: 2px;
      padding-bottom: 2px;
    }
    
    .tsr{
    background-color: #00ACEC;
    border-color: #00ACEC;
    }
    .tsr :hover{
    background-color: rgb(12, 92, 121);
    border-color:  rgb(12, 92, 121);
    
    }
    .tsr :hover{
    background-color: rgb(12, 92, 121);
    border-color:  rgb(12, 92, 121);
    
    }
    .tsr :focus{
    background-color: rgb(12, 92, 121);
    border-color:  rgb(12, 92, 121);
    
    }
    .btn-success:hover{
    background-color: rgb(12, 92, 121);
    border-color:  rgb(12, 92, 121);
    
    }
    .btn-success:focus{
    background-color: rgb(12, 92, 121);
    border-color:  rgb(12, 92, 121);
    
    }
    .checkbox-fade.fade-in-primary .cr .cr-icon, .checkbox-fade.zoom-primary .cr .cr-icon, .checkbox-zoom.fade-in-primary .cr .cr-icon, .checkbox-zoom.zoom-primary .cr .cr-icon{
    color: #00ACEC;
    }
    .checkbox-fade .cr .cr-icon, .checkbox-zoom .cr .cr-icon{
    color: #00ACEC;
    }
    .checkbox-fade.fade-in-primary .cr, .checkbox-fade.zoom-primary .cr, .checkbox-zoom.fade-in-primary .cr, .checkbox-zoom.zoom-primary .cr{
    border: 2px solid #00ACEC;
    }
    .table>tbody>tr>td {
    padding: 8px
    }
    .ssre>tbody>tr>td {
    padding: 8px 0px 5px 0px
    }
    .card .card-header .card-header-right{
    
    padding-top: 7px;
    
    }
    .table td, .table th{
    padding: 5px;
    }
    .table > thead > tr > th {
    background-color: #00ACEC;
    
    }
    
    
    
    </style>
<script>
    function showDate() {
            $find("Date").show();
    }

    function checkRadioBtn(id) {
        var gv = document.getElementById('<%=gvTransport.ClientID %>');

        for (var i = 1; i < gv.rows.length; i++) {
            var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

            // Check if the id not same
            if (radioBtn[0].id != id.id) {
                radioBtn[0].checked = false;
            }
        }
    }
    function checkRadioBtn1(id) {
        var gv = document.getElementById('<%=gvOrderPayment.ClientID %>');

        for (var i = 1; i < gv.rows.length; i++) {
            var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

            // Check if the id not same
            if (radioBtn[0].id != id.id) {
                radioBtn[0].checked = false;
            }
        }
    }
    function checkRadioBtn2(id) {
        var gv = document.getElementById('<%=gvChannel.ClientID %>');

        for (var i = 1; i < gv.rows.length; i++) {
            var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

            // Check if the id not same
            if (radioBtn[0].id != id.id) {
                radioBtn[0].checked = false;
            }
        }
    }

 
    function OpenConfirmDialog()
    {
        if (confirm('ต้องการปิดหน้าจอหรือไม่'))
        {
            document.getElementById("<%= isConfirm.ClientID %>").value = "Y";
            CloseAllTab();
        }
        else
        {
           document.getElementById("<%= isConfirm.ClientID %>").value = "N";
        }
    }

    function CloseAllTab()
    {
         document.getElementById("<%= CloseAllTab.ClientID %>").click();
    }

</script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

         <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <uc1:SelectBranch ID="SelectBranch" OnBtnClick="UsrCtrl_SelectBranch_Click" runat="server" /> 

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

                 <input type="hidden" id="isConfirm" runat="server" />
                      <input type="hidden" id="hidIdList" runat="server" />
        <input type="hidden" id="hidIdList1" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
             <asp:HiddenField ID="hidEmpCode" runat="server" />
    
                          <asp:HiddenField ID="hidtab" runat="server" />
                          <asp:HiddenField ID="hidcampaigncategorycode" runat="server" />
                          <asp:HiddenField ID="hidcampaigncategoryname" runat="server" />
                          <asp:HiddenField ID="hidtab1transportprice" runat="server" />
                          <asp:HiddenField ID="hidtab2transportprice" runat="server" />
                          <asp:HiddenField ID="hidtab3transportprice" runat="server" />
                           <asp:HiddenField ID="hidtab1orderstatus" runat="server" />
                          <asp:HiddenField ID="hidtab2orderstatus" runat="server" />
                          <asp:HiddenField ID="hidtab3orderstatus" runat="server" />
                          <asp:HiddenField ID="hidtab1ordercode" runat="server" />
                          <asp:HiddenField ID="hidtab2ordercode" runat="server" />
                          <asp:HiddenField ID="hidtab3ordercode" runat="server" />
                             <asp:HiddenField ID="hidtab1countcomboset" runat="server" />
                          <asp:HiddenField ID="hidtab2countcomboset" runat="server" />
                          <asp:HiddenField ID="hidtab3countcomboset" runat="server" />
                             <asp:HiddenField ID="hidtab1customerpay" runat="server" />
                          <asp:HiddenField ID="hidtab2customerpay" runat="server" />
                          <asp:HiddenField ID="hidtab3customerpay" runat="server" />
                            <asp:HiddenField ID="hidtab1ReturnCashAMount" runat="server" />
                          <asp:HiddenField ID="hidtab2ReturnCashAMount" runat="server" />
                          <asp:HiddenField ID="hidtab3ReturnCashAMount" runat="server" />
                          <asp:HiddenField ID="hidtab1OrderNote" runat="server" />
                          <asp:HiddenField ID="hidtab2OrderNote" runat="server" />
                          <asp:HiddenField ID="hidtab3OrderNote" runat="server" />
                            <asp:HiddenField ID="hidcampaigncodetorecipe" runat="server" />
                            <asp:HiddenField ID="hidflagshowproductpromotionrecipe" runat="server" />
                            <asp:HiddenField ID="hidpromotiontorecipe" runat="server" />
                            <asp:HiddenField ID="hidcampaigntorecipe" runat="server" />
                            <asp:HiddenField ID="hidpagedisplay" runat="server" />
                            <asp:HiddenField ID="hidCampaignCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidPromotionCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidFlagComboset_Selected" runat="server" />
                            <asp:HiddenField ID="hidPromotiondetailId_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab1PaymentType_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab2PaymentType_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab3PaymentType_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab1TransportTypeCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab2TransportTypeCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab3TransportTypeCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab1ChannelCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab2ChannelCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab3ChannelCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidtab1CampaignCategory" runat="server" />
                            <asp:HiddenField ID="hidtab2CampaignCategory" runat="server" />
                            <asp:HiddenField ID="hidtab3CampaignCategory" runat="server" />
                            <asp:HiddenField ID="hidtab1CampaignCategoryname" runat="server" />
                            <asp:HiddenField ID="hidtab2CampaignCategoryname" runat="server" />
                            <asp:HiddenField ID="hidtab3CampaignCategoryname" runat="server" />
                            <asp:HiddenField ID="hidtab1VoucherCode" runat="server" />
                            <asp:HiddenField ID="hidtab2VoucherCode" runat="server" />
                            <asp:HiddenField ID="hidtab3VoucherCode" runat="server" />
                            <asp:HiddenField ID="hidtab1VoucherPrice" runat="server" />
                            <asp:HiddenField ID="hidtab2VoucherPrice" runat="server" />
                            <asp:HiddenField ID="hidtab3VoucherPrice" runat="server" />
                            <asp:HiddenField ID="hidtab1BillDiscountPrice" runat="server" />
                            <asp:HiddenField ID="hidtab2BillDiscountPrice" runat="server" />
                            <asp:HiddenField ID="hidtab3BillDiscountPrice" runat="server" />
                            <asp:HiddenField ID="hidtab1PointDiscountPrice" runat="server" />
                            <asp:HiddenField ID="hidtab2PointDiscountPrice" runat="server" />
                            <asp:HiddenField ID="hidtab3PointDiscountPrice" runat="server" />
                            <asp:HiddenField ID="hidTotalPriceBefore" runat="server" />
                            <asp:HiddenField ID="hidtab1countproset" runat="server" />
                            <asp:HiddenField ID="hidtab2countproset" runat="server" />
                            <asp:HiddenField ID="hidtab3countproset" runat="server" />
                            <asp:HiddenField ID="hidCombosetCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidCampaignMediaCode_Selected" runat="server" />
                            <asp:HiddenField ID="hidPromotionCodeMedia_Selected" runat="server" />
                            <asp:HiddenField ID="hidFlagCombosetMedia_Selected" runat="server" />

          <div class="page-body">
            <div class="row">
                <div class="m-b-5 col-md-12">
                  
                      <asp:Button ID="btntab1" runat="server" OnClick="btntab1_Click" Text="Order 1" CssClass="myButton" />
                            <asp:Button ID="btntab2" runat="server" OnClick="btntab2_Click" Text="Order 2" CssClass="myButton" />
                            <asp:Button ID="btntab3" runat="server" OnClick="btntab3_Click" Text="Order 3" CssClass="myButton" />  
                  </div> 
              <div class=" col-md-9 col-pad-r">
               
                <div class="card">
                  <div class="card-header">
                    <div class="sub-title  " style="border: none;">ตะกร้าสินค้า 
                      <span> 
                           <asp:LinkButton id="btnAddProduct" class="button-pri button-add"
                       OnClick="btnAddProduct_Click" runat ="server"><i class="fa fa-plus m-r-5"></i>เพิ่มสินค้า</asp:LinkButton>
                     <!--     <button type="button" class="btn-add button-active btn-small"
                          data-toggle="modal" data-target="#addpro"><i class="fa fa-plus"></i>เลือกสินค้า</button>-->
                       <!--ยังไม่ได้ต่อหลังบ้าน-->
                        
                        <asp:LinkButton runat="server" ID="btnCleargvOrder" class="button-pri button-delete" OnClick="btnCleargvOrder_Click"><i class="fa fa-plus m-r-5" runat="server"></i>ล้างตะกร้า</asp:LinkButton>
                      </span>
                    </div> 
                      <div class="sub-title" style="border: none">
                          <asp:Label ID="lblorderstatus" runat="server"></asp:Label>
                          <asp:Label ID="lblordercode" runat="server"></asp:Label>
                      </div>
                  </div>
                  <div class="card-block">
                   
                       <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" cssclass="table-p-stand"  OnRowDataBound="gvOrder_RowDataBound"
                            TabIndex="0" style="width:100%; border:none;" CellSpacing="0" OnRowCommand="gvOrder_RowCommand" 
                            ShowHeaderWhenEmpty="true"> 
                            <Columns>
                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="" ItemStyle-Width="" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate >

                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                                  
                                          <asp:CheckBox ID="chkOrder"  runat="server" />
                                      
                                    </ItemTemplate>

                                </asp:TemplateField>--%>

                                  <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="" ItemStyle-Width="">

                                    <HeaderTemplate >

                                        <div align="center">รหัส</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                               
                                          <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                   </font>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="" ItemStyle-Width="">

                                    <HeaderTemplate >

                                        <div align="center">ชื่อ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                  <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="" ItemStyle-Width="" >

                                    <HeaderTemplate>

                                        <div align="right">ราคา</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>    <%#GetTextPrice(DataBinder.Eval(Container.DataItem, "ParentProductCode"),(DataBinder.Eval(Container.DataItem, "ParentProductCode").ToString() == "-PromotionNewPrice") ? DataBinder.Eval(Container.DataItem, "ProductPrice").ToString() + "," + DataBinder.Eval(Container.DataItem, "Price").ToString() : DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"),DataBinder.Eval(Container.DataItem, "ColorCode"))%>
                                   
                                     
                                                                                 
                                    </ItemTemplate>

                                </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="" ItemStyle-Width="" >

                                    <HeaderTemplate>

                                        <div align="right">หน่วย</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>   
                                       <asp:Label ID="lblUnitNameTh" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                                                 
                                    </ItemTemplate>

                                </asp:TemplateField>
                                  
                                 
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="" ItemStyle-Width="" >

                                    <HeaderTemplate>

                                        <div align="right">จำนวน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>     
                                        <asp:TextBox style="width:40px;text-align:right" min="0" max="99999" ID="txtAmount_gvOrder" AutoPostBack="True" OnTextChanged="txtAmount_gvOrder_TextChanged" Text='<%# Eval("Amount") %>' 
                                          runat="server" TextMode="Number" ></asp:TextBox>
                                       
                                                                        
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="" ItemStyle-Width="" >

                                    <HeaderTemplate>

                                        <div align="right">จำนวน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>     
                                        <asp:TextBox style="width:40px;text-align:right" ID="txtAmount_gvOrderView" AutoPostBack="true" OnTextChanged="txtAmount_gvOrderView1_TextChange" Text='<%# Eval("Amount") %>' 
                                          runat="server" TextMode="Number" ></asp:TextBox>
                                       
                                                                        
                                    </ItemTemplate>

                                </asp:TemplateField>
                                
                               <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="" ItemStyle-Width="" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="right">รวม</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <font color="<%# DataBinder.Eval(Container.DataItem, "ColorCode")%>">
                                                 <asp:Label ID="lblSumPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SumPrice", "{0:n}")%>'></asp:Label>
                                      </font>
                                    <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# Eval("PromotionDetailId") %>' />    
                                    <asp:HiddenField ID="hidPromotionDeailInfoId" runat="server" Value='<%# Eval("PromotionDetailId") %>' />  
                                    <asp:HiddenField ID="hidSumprice" runat="server" value='<%#GetPrice(DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"))%>' />
                                    <asp:HiddenField ID="hidRunning" runat="server" value='<%# Eval("runningNo") %>' />
                                    <asp:HiddenField ID="hidAmount" runat="server" value='<%# Eval("Amount") %>' />
                                    <asp:HiddenField ID="hidDefaultAmount" runat="server" value='<%# Eval("DefaultAmount") %>' />
                                    <asp:HiddenField ID="hidParentProductCode" runat="server" value='<%# Eval("ParentProductCode") %>' />
                                    <asp:HiddenField ID="hidParentPromotionCode" runat="server" value='<%# Eval("ParentPromotionCode") %>' />
                                    <asp:HiddenField ID="hidFlagCombo" runat="server" value='<%# Eval("FlagCombo") %>' />
                                    <asp:HiddenField ID="hidUnit" runat="server" value='<%# Eval("Unit") %>' />
                                    <asp:HiddenField ID="hidUnitName" runat="server" value='<%# Eval("UnitName") %>' />
                                    <asp:HiddenField ID="hidCampaignCategoryCode" runat="server" value='<%# Eval("CampaignCategory") %>' />
                                    <asp:HiddenField ID="hidCampaignCategoryName" runat="server" value='<%# Eval("CampaignCategoryName") %>' />
                                    <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                    <asp:HiddenField ID="hidMerchantName" runat="server" value='<%# Eval("MerchantName") %>' />
                                    <asp:HiddenField ID="hidFreeShippingCode" runat="server" value='<%# Eval("FreeShipping") %>' />
                                    <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                    <asp:HiddenField ID="hidPromotionTypeCode" runat="server" value='<%# Eval("PromotionTypeCode") %>' />
                                    <asp:HiddenField ID="hidProductCode" runat="server" value='<%# Eval("ProductCode") %>' />
                                    <asp:HiddenField ID="hidProductName" runat="server" value='<%# Eval("ProductName") %>' />
                                    <asp:HiddenField ID="hidDiscountAmount" runat="server" value='<%# Eval("DiscountAmount") %>' />
                                    <asp:HiddenField ID="hidDiscountPercent" runat="server" value='<%# Eval("DiscountPercent") %>' />
                                    <asp:HiddenField ID="hidPrice" runat="server" value='<%# Eval("Price") %>' />
                                    <asp:HiddenField ID="hidGroupPrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />
                                    <asp:HiddenField ID="hidProductPrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductPrice")%>' />
                                    <asp:HiddenField ID="hidComboName" runat="server" value='<%# Eval("ComboName") %>' />
                                    <asp:HiddenField ID="hidComboCode" runat="server" value='<%# Eval("ComboCode") %>' />
                                    <asp:HiddenField ID="hidLockCheckbox" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                    <asp:HiddenField ID="hidLockAmountFlag" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                    <asp:HiddenField ID="hidProhMinQtyHeaderFlag" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProhMinQtyHeaderFlag")%>' />
                                    <asp:HiddenField ID="hidMOQNewHeaderFlag" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MOQNewHeaderFlag")%>' />
                                    <asp:HiddenField ID="hidFlagProSetHeader" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "FlagProSetHeader")%>' />
                                    <asp:HiddenField ID="hidTradeFlag" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TradeFlag")%>' />
                                    <asp:HiddenField ID="hidMOQFlag" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                    <asp:HiddenField ID="hidMinimumQty" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>' />
                                    <asp:HiddenField ID="hidProductDiscountPercent" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountPercent")%>' />
                                    <asp:HiddenField ID="hidProductDiscountAmount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountAmount")%>' />
                                    <asp:HiddenField ID="hidPromotionDiscountPercent" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountPercent")%>' />
                                    <asp:HiddenField ID="hidPromotionDiscountAmount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountAmount")%>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
             
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center"></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                                 
                                            <asp:LinkButton ID="btnClose" AutoPostBack="True" OnClick="btnClose_Click" runat="server"><i class="ti-close"></i></asp:LinkButton>
                  
                                                      
                                    </ItemTemplate>

                                </asp:TemplateField>
             
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="ไม่พบสินค้าในตะกร้า"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>
                                   
                      <!--
                      <table class="table-p table-bordered table-striped ">
                      <thead>
                        <tr>
                          <th style="text-align:center">รหัสสินค้า</th>
                          <th style="text-align:center"> ชื่อสินค้า</th>
                          <th style="text-align:center">ราคา</th>
                          <th style="width: 1%;">หน่วย</th>
                          <th style="text-align:center">ส่วนลด</th>
                          <th style="text-align:center">ราคาสุทธิ</th>
                          <th style="text-align:center">จำนวน </th>
                          <th style="text-align:center">รวม</th>

                          <th></th>
                        </tr>
                      </thead>
                      <tbody>

                        <tr>

                          <td style="text-align: center;">PD17340000836</td>
                          <td>เครื่องกรองน้ำเซฟ รุ่น Alkaline Plus</td>
                          <td style="text-align: right;">28,900.00</td>
                          <td style="text-align: right;">เครื่อง</td>
                          <td style="text-align: right;">0.00</td>
                          <td style="text-align: right;">28,900.00</td>

                          <td style="text-align: right;">1</td>

                          <td style="text-align: right;">28,900.00</td>
                          <td>

                            <a href=""><i class="ti-trash"></i></a>
                          </td>
                        </tr>
                        <tr>

                          <td style="text-align: center;">PD17340002683</td>
                          <td>เครื่องกรองน้ำ รุ่น UV Plus</td>
                          <td style="text-align: right;">14,500.00</td>
                          <td style="text-align: right;">เครื่อง</td>
                          <td style="text-align: right;">0.00</td>
                          <td style="text-align: right;">14,500.00</td>

                          <td style="text-align: right;">1</td>

                          <td style="text-align: right;">14,500.00</td>
                          <td>

                            <a href=""><i class="ti-trash"></i></a>
                          </td>
                        </tr>
                        <tr>

                          <td style="text-align: center;">PD17340000104</td>
                          <td>เครื่องทำน้ำอุ่น Q-Series WH 4.5</td>
                          <td style="text-align: right;">19,300.00</td>
                          <td style="text-align: right;">เครื่อง</td>
                          <td style="text-align: right;">1,200.00</td>
                          <td style="text-align: right;">18,100.00</td>

                          <td style="text-align: right;">1</td>

                          <td style="text-align: right;">18,100.00</td>
                          <td>

                            <a href=""><i class="ti-trash"></i></a>
                          </td>
                        </tr>
                        <tr>

                          <td style="text-align: center;">PD17340005042</td>
                          <td>เครื่องกรองน้ำเซฟ รุ่น Super Alkaline</td>
                          <td style="text-align: right;">47,200.00</td>
                          <td style="text-align: right;">เครื่อง</td>
                          <td style="text-align: right;">3,000.00</td>
                          <td style="text-align: right;">44,200.00</td>

                          <td style="text-align: right;">1</td>

                          <td style="text-align: right;">44,200.00</td>
                          <td>

                            <a href=""><i class="ti-trash"></i></a>
                          </td>
                        </tr>














                      </tbody>
                    </table>

                      -->
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-4 col-pad-r" style="display:inline-grid;" >
                    <div class="card"  >
                      <div class="card-header">
                        <div class="sub-title ">วิธีการจัดส่ง</div>
                      </div>
                      <div class="card-block">

                          <asp:GridView ID="gvTransport" runat="server" AutoGenerateColumns="False" CssClass=""
                            TabIndex="0" Width="100%" CellSpacing="0" class="transport" border="0" OnRowDataBound="gvTransport_RowDataBound"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radTransportType01" runat="server" GroupName="radTransportType" OnClick="checkRadioBtn(this);" OnCheckedChanged="radTransportType01_CheckChanged" AutoPostBack="true" Value='<%# Eval("LogisticCode") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left">ชนิดการขนส่ง</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidLogisticCode" runat="server" value='<%# Eval("LogisticCode") %>' />
                                        <asp:Label ID="lblLogisticName" Text='<%# DataBinder.Eval(Container.DataItem, "LogisticName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left">ระยะเวลาขนส่ง</div>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                        <asp:Label ID="lblLogicticEstimateTime" Text='<%# DataBinder.Eval(Container.DataItem, "WorkingDay")%>' runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;วัน
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="140px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="right">ราคาขนส่ง</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidFee" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Fee", "{0:n}")%>' />
                                        <asp:HiddenField ID="hidDefaultFee" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Fee", "{0:n}")%>' />
                                        <asp:Label ID="lblFee" Text='<%# DataBinder.Eval(Container.DataItem, "Fee", "{0:n}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty1" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>
                          <!--<table class="ssre " style="width:100%"> 
                              <tbody> 
                                <tr>
                                  <th style="text-align:left ; "> <input type="radio" id="huey" name="drone" value="huey" checked> <span class="p-r-5">Register</span> </th>
                                  <td style="text-align:right ; "> 100</td>
                                  <td style="text-align:right ; "> บาท</td>
                                </tr>
                                <tr>
                                    <th style="text-align:left ; "> <input type="radio" id="huey" name="drone" value="huey" > <span class="p-r-5">EMS</span></th>
                                    <td style="text-align:right ; "> 150</td>
                                    <td style="text-align:right ; "> บาท</td>
                                  </tr>
                                  <tr>
                                      <th style="text-align:left ; "> <input type="radio" id="huey" name="drone" value="huey"> <span class="p-r-5">Kerry</span></th>
                                      <td style="text-align:right ; "> 90</td>
                                      <td style="text-align:right ; "> บาท</td>
                                    </tr>
                                    <tr>
                                        <th  style="text-align:left;">  <input type="radio" id="huey" name="drone" value="huey"> <span class="p-r-5">Other</span></th>
                                        
                                      </tr> 
                              </tbody>
                            </table>--> 
                      </div>
                    </div>
                  </div>
                  <div class="col-md-8 col-pad-l ">
                    <!-- <div class="card">
                      <div class="card-header">

                        <div class="sub-title ">ส่วนลดพิเศษ
                        </div>
                      </div>
                      <div class="card-block">
                        <div class="form-group row">
                          <label class="col-sm-4 col-form-label">คะแนนสะสม</label>
                          <div class="col-sm-4">
                            <input type="text" class="form-control">
                          </div>
                          <div class="col-sm-4">
                            <input type="text" class="form-control">
                          </div>
                          <label class="col-sm-4 col-form-label">บาท</label>
                          <div class="col-sm-4">
                            <input type="text" class="form-control">
                          </div>
                          <div class="col-sm-4">
                            <input type="text" class="form-control">
                          </div>
                          <label class="col-sm-4 col-form-label">คูปอง</label>
                          <div class="col-sm-8">
                            <input type="text" class="form-control">
                          </div>

                        </div>
                      </div>
                    </div> -->
                    <div class="card">
                     
                      <div class="card-header">
                        <div class="sub-title ">สรุปรายการ</div>
                      </div>
                      <div class="card-block">
                        <table class="order-list " style="width:100%">
                            <thead>
                                <tr>
                                 
                                  <th style="widows: 50%;"></th>
                                  <th style="width:25%; text-align:right;"></th>
                                  <th style="width:25%; text-align:right;"></th>
                                  
                                </tr>
                              </thead> 
                      <tbody>
                        <tr>
                          <td style=" " >ราคารวม (ก่อน VAT)</td>
                          <td style="text-align: right;"><asp:Label ID="lblTotalPricebefore" runat="server"></asp:Label></td>
                          <td style="text-align: right;"> บาท  </td>
                         
                          </tr>
                       <tr>
                              <td style=" " >ส่วนลดจากโปรท้ายบิล </td>
                              <td style="text-align: right;"><asp:Label ID="lblBillDiscountPrice" runat="server"></asp:Label></td>
                              <td style="text-align: right;"> บาท  </td>
                              </tr>
                        <%--      <tr>
                                  <td style=" " >ส่วนลดจากคะแนนสะสม</td>
                                  <td style="text-align: right;"><asp:Label ID="lblPointDiscountPrice" runat="server"></asp:Label></td>
                                  <td style="text-align: right;"> บาท  </td>
                                  </tr>
                                  <tr>
                                      <td style=" " >ส่วนลดจากคูปอง</td>
                                      <td style="text-align: right;"><asp:Label ID="lblVoucherDiscountPrice" runat="server"></asp:Label></td>
                                      <td style="text-align: right;"> บาท  </td>
                                      </tr>--%>
                                      <tr>
                                          <td style=" " >VAT (    <asp:TextBox ID="txtVat" style="text-align: center;width:20px;height:20px;display:inline-block" OnTextChanged="txtVat_TextChanged" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox> %)</td>
                                          <td style="text-align: right;"><asp:Label ID="lblVatPrice" runat="server"></asp:Label></td>
                                          <td style="text-align: right;"> บาท  </td>
                                          </tr>
                                          <tr>
                                              <td style=" " >ค่าจัดส่ง</td>
                                              <td style="text-align: right;"><asp:Label ID="lblTransportPrice" runat="server"></asp:Label></td>
                                              <td style="text-align: right;"> บาท  </td>
                                              </tr>
                                              <tr>
                                                  <td style=" " >รวมทั้งสิ้น</td>
                                                  <td style="text-align: right;"><asp:Label ID="lblTotalPriceAfter" runat="server"></asp:Label></td>
                                                  <td style="text-align: right;"> บาท  </td>
                                                  </tr>
                          
                        </tbody>
                      </table>
                                 
                        
                    </div>
                        
                    </div>
                      <div class="card">
                           <div class="card-header">
                        <div class="sub-title ">วันนัดส่งสินค้า</div>
                      </div>
                          <div class="card-block">
                          <asp:TextBox class="form-control" style="border: 2px solid #eb954a;background-color: #ffaf69;" runat="server" onclick="showDate();" ID="txtDate" placeholder="วว/ดด/ปปปป"/>
    <asp:ImageButton style="display:none;" runat="Server" ID="imgCal1" AlternateText="Click to show calendar" />
         <ajaxToolkit:CalendarExtender ID="car_modal_RequestDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDate" PopupButtonID="img_modal_RequestDate" Enabled="True" TodaysDateFormat="dd/MM/yyyy" />
                          </div>
                      </div> 
                  
                  </div>

                  <div class="col-md-6 col-pad-r" style="display:none;">
                      <div class="card">
                          <div class="card-header">
    
                            <div class="sub-title ">ส่วนลดจากคะแนนสะสม
                            </div>
                          </div>
                          <div class="card-block">
                            <div class="form-group row">
                              <label class="col-sm-4 col-form-label">คะแนนสะสม</label>
                              <div class="col-sm-4">
                                <input type="text" class="form-control" value="25" style="text-align: right;padding-right:5px;" readonly>
                              </div>
                              <div class="col-sm-4">
                               <p>คะแนน</p>
                              </div>
                              <label class="col-sm-4 col-form-label">ใช้แลกเป็นเงิน</label>
                              <div class="col-sm-4">
                                <input type="text" class="form-control" value="0"style="text-align: right ;padding-right:5px;" readonly>
                              </div>
                              <div class="col-sm-4">
                                  <p>บาท</p>
                              </div>
                              <style>
                                  .switch {
                                    position: relative;
                                    display: inline-block;
                                    width: 60px;
                                    height: 34px;
                                  }
                                  
                                  .switch input { 
                                    opacity: 0;
                                    width: 0;
                                    height: 0;
                                  }
                                  
                                  .slider {
                                    position: absolute;
                                    cursor: pointer;
                                    top: 5px;
                                    left: 0;
                                    right: 5px;
                                    bottom: 0;
                                    background-color: #ccc;
                                    -webkit-transition: .4s;
                                    transition: .4s;
                                  }
                                  
                                  .slider:before {
                                    position: absolute;
                                    content: "";
                                    height: 20px;
                                    width: 20px;
                                    left: 4px;
                                    bottom: 4px;
                                    background-color: white;
                                    -webkit-transition: .4s;
                                    transition: .4s;
                                  }
                                  
                                  input:checked + .slider {
                                    background-color: #2196F3;
                                  }
                                  
                                  input:focus + .slider {
                                    box-shadow: 0 0 1px #2196F3;
                                  }
                                  
                                  input:checked + .slider:before {
                                    -webkit-transform: translateX(26px);
                                    -ms-transform: translateX(26px);
                                    transform: translateX(26px);
                                  }
                                  
                                  /* Rounded sliders */
                                  .slider.round {
                                    border-radius: 34px;
                                  }
                                  
                                  .slider.round:before {
                                    border-radius: 50%;
                                  }
                                  </style>
                              <label class="col-sm-4 col-form-label">ต้องการใช้เป็นส่วนลด</label>
                              <div class="col-sm-6">
                                  
                                  <label class="switch">
                                      <asp:CheckBox ID="chkDiscount" runat="server" OnCheckedChanged="chkDiscount_Changed" AutoPostBack="true"/>
                                      <span class="slider round"></span>
                                    </label>
                              </div>
    
                            </div>
                          </div>
                        </div>
                  </div>
                  <div class="col-md-6 col-pad-l" style="display:inline-grid;display:none;">
                      <div class="card">
                          <div class="card-header"> 
                            <div class="sub-title ">ส่วนลดจากคูปอง
                            </div>
                          </div>
                          <div class="card-block">
                            <div class="form-group row">
                              <label class="col-sm-4 col-form-label">Code</label>
                              <div class="col-sm-4">
                                <asp:TextBox ID="txtVoucherCode" style="text-align: right" OnTextChanged="txtVoucherCode_TextChanged" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                              </div> 
                              <div>                                  
                                  <i id="VoucherTrue" class="fa fa-2x ion-checkmark-circled" style="color:LimeGreen" runat="server"></i>
                                  <i id="VoucherFault" class="fa fa-2x ion-close-circled" style="color:red" runat="server"></i>
                                  <i id="VoucherDefault" class="fa fa-2x ion-checkmark-circled" style="color:transparent" runat="server"></i>

                              </div> 
                              <label class="col-sm-4 col-form-label">ใช้แลกเป็นเงิน</label>
                              <div class="col-sm-4">
                                <asp:TextBox ID="txtVoucherPrice" runat="server" class="form-control" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                              </div> 
                              <div><p>บาท</p></div>
                            </div>
                          </div>
                        </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6 col-pad-r "  >
                    <div class="card">
                            <div class="card-header" style="padding:5px 5px">
                                <div class="sub-title p-b-5 " >ที่อยู่จัดส่ง   <asp:LinkButton id="btnMapAddress" OnClick="btnMapAddress_Click" cssclass="btn-mk-pri btn-map"  runat="server" style=" text-decoration: none !important;"><i  class=" fa  ti-location-pin " style="font-size:1.2em;color:red" ></i> map</asp:LinkButton> 
                                        
                                  </div>
                                </div>
                      <div class="card-block">
                            <ul style="list-style: none;">
                                <li> <span>ที่อยู่  </span> <span class="p-l-50 m-r-5">:</span><asp:Label ID="lblCustomerAddress" runat="server"></asp:Label></li>
                                <li><span>แขวง/ตำบล <span class="p-l-8 m-r-5">:</span></span><asp:Label ID="lblSubDistrict" runat="server"></asp:Label></li>
                                 <li><span>เขต/อำเภอ <span class="p-l-13 m-r-5">:</span></span><asp:Label ID="lblDistrict" runat="server"></asp:Label></li>
                                 <li><span>จังหวัด <span class="p-l-38 m-r-5">:</span></span><asp:Label ID="lblProvince" runat="server"></asp:Label></li>
                                 <li><span>รหัสไปรษณีย์ <span class="m-r-5">:</span></span><asp:Label ID="lblZipcode" runat="server"></asp:Label></li>
                            </ul>
                        
                      </div>
                    </div>
                  </div>

                  <div class="col-md-6 col-pad-l"  style="display:inline-grid">
                  
                      <div class="card">
                          <div class="card-header"  style="padding:5px 5px">
    
                            <div class="sub-title ">การชำระเงิน
    
                            </div>
                          </div>
                          <div class="card-block">

                          <asp:GridView ID="gvOrderPayment" runat="server" AutoGenerateColumns="False" CssClass=""
                            TabIndex="0" Width="100%" CellSpacing="0" class="transport" border="0" OnRowDataBound="gvOrderPayment_RowDataBound"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radPaymentType" runat="server" GroupName="radTransportType" OnClick="checkRadioBtn1(this);" OnCheckedChanged="radPaymentType_CheckChanged" AutoPostBack="true" Value='<%# Eval("LookupCode") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left"></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymenymentTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "LookupValue")%>' runat="server" />
                                        <asp:HiddenField ID="hidPaymentTypeCode" runat="server" value='<%# Eval("LookupCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty1" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>

                            <%--<ul style="list-style-type:none;" >
                              <li>
                                <div class="checkbox-fade fade-in-primary d-">
                                  <label>
                                    <input type="checkbox" value="">
                               
                                    <span class="text-inverse">ชำระเงินสดปลายทาง </span>
                                  </label>
                                </div>
                              </li>
                              <li>
                                <div class="checkbox-fade fade-in-primary d-">
                                  <label>
                                    <input type="checkbox" value="">
                                    
                                    <span class="text-inverse">เครดิต &nbsp; VISA / Master Card / JCB </span>
                                  </label>
                                </div>
                              </li>
                              <li>
                                <div class="checkbox-fade fade-in-primary d-">
                                  <label>
                                    <input type="checkbox" value="">
                                
                                    <span class="text-inverse">iBanking </span>
                                  </label>
                                </div>
                              </li>
                              <li>
                                <div class="checkbox-fade fade-in-primary d-">
                                  <label>
                                    <input type="checkbox" value="">
                                    
                                    <span class="text-inverse">ชำระผ่าน ATM </span>
                                  </label>
                                </div>
                              </li>
                            </ul>--%>
                          </div>
                        </div>
                  </div>
                 
                </div>
                <div class="row ">
                  
                    <div class="col-md-12 text-center m-t-10 " style="padding-left:1px"> 
                      <asp:linkbutton ID="BtnSubmitOrder" class="button-submitorder" onclick="Submitorder" runat="server">สั่งสินค้า</asp:linkbutton> 
                        <asp:Button ID="CloseAllTab" style="display:none;" class="button-submitorder" onclick="CloseAllTab_Click" runat="server"></asp:Button> 
                    </div>
                  </div>
              </div>
              <div class=" col-md-3 col-pad-l" >
                <div class="card">
                  <div class="card-header">

                    <div class="sub-title ">ข้อมูลลูกค้า
                      <div class="card-header-right">
                       
                        <%--<asp:LinkButton id="btnEdituser" class="button-pri btn-edit"
                        OnClick="btnEdituser_Click" runat ="server"><i class="fa fa-edit m-r-5"></i>แก้ไข</asp:LinkButton>--%>
                      </div>
                    </div>
                  </div>
                  <div class="card-block">
                    <table class="ssre " style="width:100%">

                      <tbody>
                        <tr>
                          <th style="font-size:16px ;" colspan="2"><asp:Label ID="lblCustomerName" runat="server"></asp:Label></th>
                        <tr>
                          <th style="text-align:left ; ">รหัสลูกค้า <span style="">:</span></th>
                          <td style="text-align:right ; "><asp:Label ID="lblCustomerCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                          <th style="text-align:left ; ">เบอร์ติดต่อ <span style="">:</span></th>
                          <td style="text-align:right ;"><asp:Label ID="lblCustomerPhone01" runat="server"></asp:Label></td>
                        </tr>
                        <%--<tr>
                          <th style="text-align:left ; ">เบอร์ติดต่อ <span style="">:</span></th>
                          <td style="text-align:right ; "><asp:Label ID="lblCustomerPhone02" runat="server"></asp:Label></td>
                        </tr>--%>
                      </tbody>
                    </table>
                    <div class="text-center m-t-10 m-b-10">
                     

                        <%--<asp:LinkButton ID="ContactHistory" class="button-pri btn-submit m-r-15"
                            OnClick="btnHistorycontact_Click" runat="server"><i class="fa fa-edit m-r-5"></i>ประวัติการติดต่อ</asp:LinkButton>--%>

                        <asp:LinkButton id="OrderHistory" class="button-pri btn-submit"
                        OnClick="OrderHistory_Click" runat ="server"><i class="fa fa-edit m-r-5"></i>ประวัติการสั่งซื้อ</asp:LinkButton>
                    </div>

                  </div>
                </div>
                <div class="card">
                    <div class="card-header">

                        <div class="sub-title ">Note ลูกค้า
    
                        </div>
                      </div>
                  <div class="card-block">
                      <div class="form-group row">
                          <div class="col-sm-12">
                              <asp:TextBox id="txtNoteProfile" TextMode="multiline" Rows="5" Width="100%"  runat="server" ></asp:TextBox>
                          </div>
                      </div>
                  </div>
                </div>
                <div class="card">
                    <div class="card-header">

                        <div class="sub-title ">Note การสั่งซื้อครั้งนี้
    
                        </div>
                      </div>
                      <div class="card-block">
                      <div class="form-group row"> 
                      <div class="col-sm-12">
                          <asp:TextBox Rows="5" TextMode="MultiLine"  ID="txtNoteOrder" Width="100%" class="form-control" runat="server" ></asp:TextBox>
                      </div>
                    </div>
                 
                  </div>
                </div>
                <div class="card">
                    <div class="card-header">
  
                      <div class="sub-title ">ช่องทางการสั่งซื้อ
                      </div>
                    </div>
                    <div class="card-block">

                        <asp:GridView ID="gvChannel" runat="server" AutoGenerateColumns="False" CssClass=""
                            TabIndex="0" Width="100%" CellSpacing="0" class="transport" border="0" OnRowDataBound="gvChannel_OnRowDataBound"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radChannelType" runat="server" GroupName="ChannelType" OnClick="checkRadioBtn2(this);" OnCheckedChanged="radChannelType_CheckChanged" AutoPostBack="true" Value='<%# Eval("ChannelCode") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left"></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                        <asp:HiddenField ID="hidChannelCode" runat="server" value='<%# Eval("ChannelCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty7" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>

                      <!--<ul style="list-style-type:none;">
                        <li>
                          <div  >
                            <label>
                              <input type="radio" value=""> 
                              <span class="text-inverse">Telephone</span>
                            </label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                        </li>
                        <li>
                          <div  >
                            <label>
                              <input type="radio" value="">
                              <span class="text-inverse">Line  </span>
                            </label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                        </li>
                        <li>
                          <div  >
                            <label>
                              <input type="radio" value=""> 
                              <span class="text-inverse">Advertise (02 999 9999) </span>
                            </label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                        </li>
                     
  
  
                      </ul>-->
  
                    </div>
                  </div>
                <div class="card">
                  <div class="card-header">

                    <div class="sub-title ">สินค้ายอดนิยม

                    </div>
                  </div>
                  <div class="card-block">
                      <div class="card-block">
                                           <ul style="list-style-type: none;  padding: 0px ;">
                                              
                                           <li class="bb">                 
                                           <span class="m-r-20"><i runat="server" id="ico01" class="fa fa-plus button-activity"></i></span>                                                                 
                                           <asp:LinkButton ID="LinkBtnProductTop01" runat="server" OnClick="AddtocartProductTop01">
                                           <asp:Label ID="lblProductNameTop1" runat="server" style="color:#6c757d; text-decoration: none;"></asp:Label>
                                           </asp:LinkButton></li>                                      
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidUnitProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidUnitNameProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop01" runat="server" />
                                           <li class="bb">  
                                           <span class="m-r-20"><i runat="server" id="ico02"   class="fa fa-plus button-activity"></i></span>    
                                           <asp:LinkButton ID="LinkBtnProductTop02" runat="server" OnClick="AddtocartProductTop02">                                            
                                           <asp:Label ID="lblProductNameTop2" runat="server" style="color:#6c757d; text-decoration: none;"></asp:Label></asp:LinkButton></li>
                                           <li class="bb"></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidUnitProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidUnitNameProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop02" runat="server" />
                                           <li class="bb">  
                                           <span class="m-r-20"><i runat="server" id="ico03"   class="fa fa-plus button-activity"></i></span>    
                                           <asp:LinkButton ID="LinkBtnProductTop03" runat="server" OnClick="AddtocartProductTop03">                                            
                                           <asp:Label ID="lblProductNameTop3" runat="server" style="color:#6c757d; text-decoration: none;"></asp:Label></asp:LinkButton></li>
                                           <li class="bb"></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidUnitProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidUnitNameProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop03" runat="server" /> 
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop03" runat="server" />
                                           <li class="bb">
                                           <span class="m-r-20"><i  runat="server" id="ico04"  class="fa fa-plus button-activity"></i></span>    
                                           <asp:LinkButton ID="LinkBtnProductTop04" runat="server" OnClick="AddtocartProductTop04">                                            
                                           <asp:Label ID="lblProductNameTop4" runat="server" style="color:#6c757d; text-decoration: none;"></asp:Label></asp:LinkButton></li>
                                           <li class="bb"></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidUnitProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidUnitNameProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop04" runat="server" />   
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop04" runat="server" />
                                           <li class="bb">
                                           <span class="m-r-20"><i  runat="server" id="ico05"  class="fa fa-plus button-activity"></i></span>    
                                           <asp:LinkButton ID="LinkBtnProductTop05" runat="server" OnClick="AddtocartProductTop05">                                            
                                           <asp:Label ID="lblProductNameTop5" runat="server" style="color:#6c757d; text-decoration: none;"></asp:Label></asp:LinkButton></li>  
                                           <li class="bb"></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidUnitProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidUnitNameProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidPromotionTypeSelected" runat="server" />
                                           <asp:HiddenField ID="hidProLockCheckbox" runat="server" />
                                           <asp:HiddenField ID="hidProLockAmountFlag" runat="server" />
                                           <asp:HiddenField ID="hidProFreeShippingCode" runat="server" />
                                           <asp:HiddenField ID="hidPromotionTypeSelectedMedia" runat="server" />
                                           <asp:HiddenField ID="hidProLockAmountFlagMedia" runat="server" />
                                           <asp:HiddenField ID="hidProLockCheckboxMedia" runat="server" />
                                           <asp:HiddenField ID="hidProFreeShippingCodeMedia" runat="server" />

                                         </ul>
                                         <%--<asp:DataList ID="dtlTop5Product" runat="server"
                                       style="width: 100%;float: right;">
                                      <HeaderTemplate></HeaderTemplate>
                                      <ItemTemplate>                                    
                                        <asp:Label runat="server" ID="lblProductNameTop1" Text='<%# Eval("ProductName1") %>' />
                                        <asp:HiddenField ID="hidProductNameTop1" runat="server" Value='<%# Eval("ProductName1") %>' />
                                        <asp:HiddenField ID="hidProductCodeTop1" runat="server" Value='<%# Eval("ProductCode1") %>' />                                        
                                          </ItemTemplate>

                                             <ItemTemplate>                                    
                                        <asp:Label runat="server" ID="lblProductNameTop2" Text='<%# Eval("ProductName2") %>' />
                                        <asp:HiddenField ID="hidProductNameTop2" runat="server" Value='<%# Eval("ProductName2") %>' />
                                        <asp:HiddenField ID="hidProductCodeTop2" runat="server" Value='<%# Eval("ProductCode2") %>' />                                        
                                          </ItemTemplate>

                                             <ItemTemplate>                                    
                                        <asp:Label runat="server" ID="lblProductNameTop3" Text='<%# Eval("ProductName3") %>' />
                                        <asp:HiddenField ID="hidProductNameTop3" runat="server" Value='<%# Eval("ProductName3") %>' />
                                        <asp:HiddenField ID="hidProductCodeTop3" runat="server" Value='<%# Eval("ProductCode3") %>' />                                        
                                          </ItemTemplate>

                                             <ItemTemplate>                                    
                                        <asp:Label runat="server" ID="lblProductNameTop4" Text='<%# Eval("ProductName4") %>' />
                                        <asp:HiddenField ID="hidProductNameTop4" runat="server" Value='<%# Eval("ProductName4") %>' />
                                        <asp:HiddenField ID="hidProductCodeTop4" runat="server" Value='<%# Eval("ProductCode4") %>' />                                        
                                          </ItemTemplate>

                                             <ItemTemplate>                                    
                                        <asp:Label runat="server" ID="lblProductNameTop5" Text='<%# Eval("ProductName5") %>' />
                                        <asp:HiddenField ID="hidProductNameTop5" runat="server" Value='<%# Eval("ProductName5") %>' />
                                        <asp:HiddenField ID="hidProductCodeTop5" runat="server" Value='<%# Eval("ProductCode5") %>' />                                        
                                          </ItemTemplate>

                                    </asp:DataList>--%>
                                       </div>
                    <!--<ul style="list-style-type:none;">
                      <li>

                        <label>

                          <span><i class="fa fa-plus"></i></span>
                          <span class="text-inverse m-l-10">เครื่องกรองน้ำเซฟ Alkaline Plus </span>
                        </label>

                      </li>
                      <li>

                        <label>

                          <span><i class="fa fa-plus"></i></span>
                          <span class="text-inverse m-l-10"> เครื่องกรองน้ำ UV Plus </span>
                        </label>

                      </li>
                      <li>

                        <label>

                          <span><i class="fa fa-plus"></i></span>
                          <span class="text-inverse m-l-10"> เครื่องทำน้ำอุ่น Q-Series WH 4.5</span>
                        </label>

                      </li>
                      <li>

                        <label>

                          <span><i class="fa fa-plus"></i></span>
                          <span class="text-inverse m-l-10"> เครื่องกรองน้ำเซฟ Super Alkaline</span>
                        </label>

                      </li>
                      <li>

                        <label>

                          <span><i class="fa fa-plus"></i></span>
                          <span class="text-inverse m-l-10"> เครื่องทำน้ำอุ่น P-Series WH 3.8</span>
                        </label>

                      </li>


                    </ul>-->

                  </div>
                </div>
              
               
              </div>
            </div> 
          </div>

              </ContentTemplate>
     </asp:UpdatePanel>

     <div class="modal fade" id="Historycontact" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width:50%">
          <div class="modal-box-s">
            <div class="modal-content">
              <div class="modal-body">
                ประวัติการติดต่อ (Customer History)
              </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal fade" id="orderhistory" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document" style="max-width:70%">
              <div class="modal-box-s">
                <div class="modal-content">
                  <div class="modal-body">
                     
                    <div class="modal-header">
                        <div class="sub-title" >ค้นหาประวัติใบสั่งขายลูกค้า (Order History)</div>
                        </div>
                        <div class="card-block">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
               <ContentTemplate>

                 
            <div class="form-group row">
              <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtOrderHistoryCode_Search" class="form-control" runat="server"></asp:TextBox>
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">สถานะใบสั่งขาย</label>
              <div class="col-sm-4">
                      <asp:DropDownList ID="ddlOrderStatus_Search" runat="server" class="form-control"></asp:DropDownList>    
              </div>

                <label class="col-sm-2 col-form-label">สถานะ</label>
              <div class="col-sm-3">
                  <asp:DropDownList ID="ddlOrderType_Search" runat="server" class="form-control"></asp:DropDownList>
                  </div> 
                  <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">วันที่สร้างใบสั่งขาย</label>
                <div class="col-sm-3">
                <div class="input-group">
              <div class="input-sm ">
                  <asp:TextBox  ID="txtOrderDateFrom_Search" class="form-control" runat="server"></asp:TextBox>
                  </div> 
                  <span class="input-group-addon"> ถึง</span>
                    <div class="input-sm  ">
                   <asp:TextBox  ID="txtOrderDateTo_Search" class="form-control" runat="server"></asp:TextBox>
              </div>
            </div>
          </div>
            </div>           
              <div class="text-center m-t-20 col-sm-12">
                  <asp:Button ID="btnSearch" Text="Search" CssClass="button-active button-submit m-r-10" runat="server"/>
                  <asp:Button ID="btnClearSearch" Text="Clear" CssClass="button-active button-cancle" runat="server" />   
              </div>        
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
                         

                      <div class="card">
         <div class="card-block">            
             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                  <div class="dt-responsive table-responsive">
                      <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap" 
                            TabIndex="0" Width="100%" CellSpacing="0" 
                            ShowHeaderWhenEmpty="true">

                            <Columns>                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">รหัสใบสั่งขาย</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ShowOrderHistoryDetail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">สถานะใบสั่งขาย</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">สถานะ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ActiveFlagName")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate  >
                                          <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowSupplier"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                        
                                        <asp:HiddenField runat="server" ID="hidSupplierId" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierId")%>' />
                                        <asp:HiddenField runat="server" ID="hidSupplierCode" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidSupplierName" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' />
                                        <asp:HiddenField runat="server" ID="hidTaxIdNo" Value='<%# DataBinder.Eval(Container.DataItem, "TaxIdNo")%>' />
                                        <asp:HiddenField runat="server" ID="hidAddress" Value='<%# DataBinder.Eval(Container.DataItem, "Address")%>' />
                                        <asp:HiddenField runat="server" ID="hidProvinceCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProvinceCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidDistrictCode" Value='<%# DataBinder.Eval(Container.DataItem, "DistrictCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidSubDistrictCode" Value='<%# DataBinder.Eval(Container.DataItem, "SubDistrictCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidZipNo" Value='<%# DataBinder.Eval(Container.DataItem, "ZipNo")%>' />
                                        <asp:HiddenField runat="server" ID="hidFaxNumber" Value='<%# DataBinder.Eval(Container.DataItem, "FaxNumber")%>' />
                                        <asp:HiddenField runat="server" ID="hidMail" Value='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' />
                                        <asp:HiddenField runat="server" ID="hidActiveFlag" Value='<%# DataBinder.Eval(Container.DataItem, "ActiveFlag")%>' />
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
                        <%-- PAGING CAMPAIGN --%>
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
                                                    Text="<<" runat="server"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                    Text="<" runat="server"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                      >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                    Text=">>" ></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                  </div>
            </div>
          </div>
          <!-- Basic Form Inputs card end -->
        </div>
      </div>
      <!-- Basic Form Inputs card end -->
    </div>
  </div>
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
    



     <div class="modal fade" id="Edituser" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document" style="max-width:30%">
        <div class="modal-box-s">
          <div class="modal-content">
            <div class="modal-body">
            <div class="form-group row">
                <label class="col-sm-4 col-form-label">รหัสลูกค้า</label>
                <div class="col-sm-8">
                  <p><asp:Label ID="lblCustomerCode_Edit" runat="server"></asp:Label></p>
                </div>
                <label class="col-sm-4 col-form-label">ชื่อ-สกุล</label>
                <div class="col-sm-8">
                  <asp:TextBox ID="txtCustomerFName_Edit" runat="server" class="form-control"></asp:TextBox> &nbsp;
                  <asp:TextBox ID="txtCustomerLName_Edit" runat="server" class="form-control"></asp:TextBox>
                </div>
                <label class="col-sm-4 col-form-label">เพิ่มเบอร์ติดต่อ</label>
                <div class="col-sm-8">
                  <asp:TextBox ID="txtCustomerPhone01_Edit" runat="server" class="form-control"></asp:TextBox>
                </div>
                <label class="col-sm-4 col-form-label">เพิ่มเบอร์ติดต่อ</label>
                <div class="col-sm-8">
                  <asp:TextBox ID="txtCustomerPhone02_Edit" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="m-t-10 text-center">
                <asp:linkbutton id="SubmitEdit" runat="Server" OnClick="btnSubmit_EditCustomer" class="button-pri button-accept">บันทึก</asp:linkbutton>
                <asp:linkbutton id="CancelEdit" runat="Server" class=" button-pri button-cancel" data-dismiss="modal">ยกเลิก</asp:linkbutton>
                
            </div>
          </div>
          </div>
        </div>
      </div>
     </div>


    <div class="modal fade" id="ProductScript" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="z-index:999999;">
      <div class="modal-dialog" role="document" style="max-width:35%; top:25%">
        <div class="modal-box-s">
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                      <div class="modal-content">
                        <div class="modal-body">
                        <div class="form-group row">
                            <label class="col-sm-4 col-form-label">อัพเซล สคริปต์</label>
                            <div class="col-sm-8">
                               <textarea rows="4" runat="server" id="txtUpsellScript"
                                cols="100"
                                class="form-control"
                                disabled
                                style="resize:none; overflow: hidden; " >
                               </textarea>
                            </div>
                      </div>
                         </div>
                      </div>
                 </ContentTemplate>
             </asp:UpdatePanel>
        </div>
      </div>
     </div>

        
        <div class="modal fade" id="modal-addpro" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog" role="document" style="max-width:70%">
                  
                <div class="modal-box">   
                       <asp:UpdatePanel ID="UpModal" runat="server">
                        <ContentTemplate>
                            <div  style="pointer-events: auto;" >   
                                <div>
                                    <asp:Button ID="btntab12" runat="server" OnClick="campaign_Click" Text="แคมเปญ" CssClass="myButton"  />
                                     <asp:Button ID="btntabmediaplan" OnClick="media_Click" runat="server" Text="Mediaplan" CssClass="myButton" Visible="false"  />
                                
                                 <span>
                                <button type="button" class="close  " style="padding-left: -5px;" data-dismiss="modal" aria-label="Close">
                                          <span aria-hidden="true">×</span>
                                  </button>
                                    </span>
                                </div>
                               
                            </div>
                            <div id="campaign" runat="server">
                         
                            <div class="modal-content m-b-10">
                                <div class="modal-body">


                                    <div class="col-sm-4" style="float: right;">
                                        <div class="input-group" style="float: right;">
                                             <asp:TextBox id="txtSearchCampaign" runat="server" class="form-control m-r-10" placeholder="ค้นหาแคมเปญ"></asp:TextBox>
                                            <asp:LinkButton class="button-search " id="btnSearchCampaign" OnClick="btnSearchCampaign_Click" runat="server"><i class="ti-search m-r-5"></i>ค้นหา</asp:LinkButton>
                                        </div>
                                    </div>
                                    <style>
                                        .fixed_header tbody {
                                            display: block;
                                            overflow: auto;
                                            height: 200px;
                                        }
                                    </style>
                                    <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">แคมเปญ</h5>

                                    <div class="table-wrapper-scroll-y my-custom-scrollbar " style="width: 100%; padding: 10px">
                                        <asp:GridView ID="gvCampaign" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " border="0" Style="width: 100%"
                                            CellSpacing="0" OnRowCommand="gvCampaign_RowCommand"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสแคมเปญ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelectCampaignCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' runat="Server" CommandName="SelectCampaign" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อแคมเปญ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCampaignName" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' runat="server" />

                                                        <asp:HiddenField runat="server" ID="hidActive" Value="N" />
                                                        <asp:HiddenField runat="server" ID="hidCampaignId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCampaignName" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                                        <asp:HiddenField runat="server" ID="hidNotifyDate" Value='<%# ((null == Eval("NotifyDate"))||("" == Eval("NotifyDate"))) ? string.Empty : DateTime.Parse(Eval("NotifyDate").ToString()).ToString("dd/MM/yyyy") %>' />
                                                        <asp:HiddenField runat="server" ID="hidExpireDate" Value='<%# ((null == Eval("ExpireDate"))||("" == Eval("ExpireDate"))) ? string.Empty : DateTime.Parse(Eval("ExpireDate").ToString()).ToString("dd/MM/yyyy") %>' />

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


                            <div class="modal-content m-b-10">
                                <div class="modal-body">

                                    <div class="col-sm-4" style="float: right;">
                                        <div class="input-group" style="float: right;">
                                            <asp:TextBox id="txtSearchPromotion" runat="server" class="form-control m-r-10" placeholder="ค้นหาโปรโมชั่น"></asp:TextBox>
                                            <asp:LinkButton class="button-search " id="btnSearchPromotion" OnClick="btnSearchPromotion_Click" runat="server"><i class="ti-search m-r-5"></i>ค้นหา</asp:LinkButton>
                                        </div>

                                    </div>
                                    <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">โปรโมชั่น</h5>
                                    <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False"
                                        Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvPromotion_RowCommand"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">รหัสโปรโมชัน</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelectPromotionCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="Server" CommandName="SelectPromotion" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">ชื่อโปรโมชัน</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                                    <asp:HiddenField runat="server" ID="hidActive" Value="N" />
                                                    <asp:HiddenField runat="server" ID="hidPromotionId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionId")%>' />
                                                    <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidPromotionName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' />
                                                    <asp:HiddenField runat="server" ID="hidPromotionTypeCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidLockAmountFlag" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                                    <asp:HiddenField runat="server" ID="hidLockCheckbox" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                                    <asp:HiddenField runat="server" ID="hidFreeShippingCode" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShippingCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                    <asp:HiddenField runat="server" ID="hidGroupPrice" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <EmptyDataTemplate>
                                            <center>
                                    <asp:Label ID="lblDataEmpty1" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                </div>
                            </div>
                            <!--ComboPart-->
                            <div id="combosetPart" runat="server">
                                <div id="comboset" runat="server" class="modal-content m-b-10">

                                    <div class="modal-body">
                                        <div class="card-block">
                                            <div class="col-sm-4" style="float: right;">
                                                <div class="input-group" style="float: right;">
                                                    <input type="text" class="form-control m-r-10" placeholder="ค้นหาคอมโบเซ็ต">
                                                    <button class="button-search " id="basic-addon11"><i class="ti-search m-r-5"></i>ค้นหา</button>
                                                </div>
                                            </div>
                                            <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">คอมโบเซ็ต</h5>
                                            <asp:GridView ID="gvComboset" runat="server" AutoGenerateColumns="False"
                                                Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvComboset_RowCommand"
                                                ShowHeaderWhenEmpty="false">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">รหัสคอมโบ</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelectComboSetCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' runat="Server" CommandName="SelectComboSet" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ชื่อคอมโบ</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionDetailName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ราคาชุดคอมโบ(บาท)</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />

                                                            <asp:HiddenField runat="server" ID="hidCombosetCode" Value='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDetailName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShippingCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDeailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty3" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            <!--
                                <table class="table-p-stand" style="width:100%"><thead>
                              <tr>
                                <th>รหัส</th>
                                <th>ชื่อ</th>
                               
                              </tr>
                             </thead>
                              <tbody>
                                <tr>
                              <td>COMB001</td>
                              <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                           
                             </tr>
                             <tr>
                                <td>COMB001</td>
                                <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                             
                               </tr>
                               <tr>
                                  <td>COMB001</td>
                                  <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                               
                                 </tr>
                                 <tr>
                                    <td>COMB001</td>
                                    <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                                 
                                   </tr>
                           
                             </tbody>
                            </table>-->
                                        </div>
                                    </div>
                                </div>

                                <div id="subpromotiondetail" runat="server" class="row">
                                    <div class="col-md-6">
                                        <div class="modal-content m-b-10">
                                            <div class="modal-body">
                                                <div class="card-block">
                                                    <div class="col-sm-8" style="float: right;">

                                                        <div class="input-group" style="float: right;">
                                                            <input type="text" class="form-control m-r-10" placeholder="ค้นหาคอมโบเซ็ต">
                                                            <button class="button-search" id="basic-addon12"><i class="ti-search m-r-5"></i>ค้นหา</button>
                                                        </div>

                                                    </div>

                                                    <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">สินค้าของชุดคอมโบ</h5>



                                                    <asp:GridView ID="gvSubMainPromotionDetail" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSubPromotionDetail_RowDataBound"
                                                        Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvSubMainPromotionDetail_RowCommand"
                                                        ShowHeaderWhenEmpty="false">

                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">รหัสสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMainProductCodexx" Text='<%# DataBinder.Eval(Container.DataItem, "MainProductCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">ชื่อสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMainProductNamexx" Text='<%# DataBinder.Eval(Container.DataItem, "MainProductName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">จำนวน</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">เลือกสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>


                                                                    <asp:LinkButton ID="btnSelectExchangeProduct" class="font12Blue" Text="Exchange" runat="Server" CommandName="SelectExchangeProduct" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                    <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode ")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidMainProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "MainProductCode")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidMainProductName" Value='<%# DataBinder.Eval(Container.DataItem, "MainProductName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFlagSubPromotionDetailMain" Value='<%# DataBinder.Eval(Container.DataItem, "FlagSubPromotionDetailMain")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidSubMainPromotionDetailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "SubMainPromotionDetailInfoId")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "UNIT")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUnitName" Value='<%# DataBinder.Eval(Container.DataItem, "UNITName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShipping")%>' />

                                                                    <asp:HiddenField runat="server" ID="hidPromotionDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailId")%>' />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>

                                                        <EmptyDataTemplate>
                                                            <center>
                                    <asp:Label ID="lblDataEmpty3" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div class="m-t-10 text-center">

                                                    <asp:LinkButton ID="btnAddCombo" runat="Server" OnClick="btnAddCombo_Click" class="button-pri button-accept">ยืนยัน</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelCombo" runat="Server" class=" button-pri button-cancel" OnClick="btnCancelCombo_Click">ยกเลิก</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="modal-content m-b-10">
                                            <div class="modal-body">
                                                <div class="card-block">
                                                    <div class="col-sm-8" style="float: right;">

                                                        <div class="input-group" style="float: right;">
                                                            <input type="text" class="form-control m-r-10" placeholder="ค้นหาคอมโบเซ็ต">
                                                            <button class="button-search " id="basic-addon6"><i class="ti-search m-r-5"></i>ค้นหา</button>
                                                        </div>

                                                    </div>
                                                    <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">เปลี่ยนได้ของชุดคอมโบ</h5>


                                                    <asp:GridView ID="gvSubExchangePromotiondetailInfo" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvSubExchangePromotiondetailInfo_RowCommand"
                                                        ShowHeaderWhenEmpty="false">

                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <center>
                                        </center>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnSelectSubProduct" runat="Server" CommandName="SelectSubProduct" class="button-pri button-activity  button-activity"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">เลือก</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">รหัสสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExchangeProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">ชื่อสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExchangeProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">จำนวน</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                                                    <asp:HiddenField runat="server" ID="hidExchangeProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductCode")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidExchangeProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidExchangeAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidSubMainExchangeID" Value='<%# DataBinder.Eval(Container.DataItem, "SubMainExchangeID")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "UNIT")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUNITName" Value='<%# DataBinder.Eval(Container.DataItem, "UNITName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShipping")%>' />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>

                                                        <%--  <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty5" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>--%>
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>
                            <div id="PromotiondetailbyProduct" runat="server">
                                <div class="modal-content m-b-10">
                                    <div class="modal-body">
                                        <div class="card-block">
                                            <div class="col-sm-4" style="float: right;">

                                                <div class="input-group" style="float: right;">
                                                 <asp:TextBox id="txtSearchProduct" runat="server" class="form-control m-r-10" placeholder="ค้นหาสินค้า"></asp:TextBox>
                                                 <asp:LinkButton class="button-search " id="btnSearchProduct" OnClick="btnSearchProduct_Click" runat="server"><i class="ti-search m-r-5"></i>ค้นหา</asp:LinkButton>
                                                </div>

                                            </div>

                                            <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none">สินค้า</h5>
                                            <asp:Button ID="btnSelectSet" runat="server" />
                                            <asp:HiddenField ID="hidProductCodeView" runat="server" />
                                            <asp:HiddenField ID="hidProductNameView" runat="server" />
                                            <asp:HiddenField ID="hidPriceView" runat="server" />
                                            <asp:HiddenField ID="hidAmountView" runat="server" />
                                            <asp:HiddenField ID="hidUnitNameView" runat="server" />
                                            <asp:HiddenField ID="hidMerchantNameView" runat="server" />
                                            <asp:HiddenField ID="hidSumPriceView" runat="server" />
                                            <asp:HiddenField ID="hidPromotionSetAmount" runat="server" />
                                            <asp:HiddenField ID="hidFlagNotDupe" runat="server" />
                                            <asp:HiddenField ID="hidPriceProSet" runat="server" />
                                            <div class="table-wrapper-scroll-y my-custom-scrollbar " style="width: 100%; padding: 10px">
                                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProduct_RowDataBound"
                                                Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvProduct_OnRowCommand"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>                                            
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelectProduct" runat="Server" CommandName="SelectProduct"
                                                                class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">รหัสสินค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="LockCheckbox" Text='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' runat="server" />--%>
                                                             <asp:LinkButton ID="lblProductCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="Server" CommandName="ShowProductScript" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <%--<asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ชื่อสินค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ราคา</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPriceBeforeDisCount" style="text-decoration:line-through" Text='<%# DataBinder.Eval(Container.DataItem, "PriceB4ofPrdDisc", "{0:n}")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:n}")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">จำนวน</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox Style="width: 40px; text-align: right;" ID="txtAmount" AutoPostBack="True" min="0" max="99999" Text='<%# Eval("Amount") %>' OnTextChanged="txtAmountgvProduct_TextChanged" runat="server" TextMode="Number"></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">หน่วย</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

<%--                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ร้านค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">รวม</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSumPrice" Text='<%# DataBinder.Eval(Container.DataItem, "SumPrice", "{0:n}")%>' runat="server" />

                                                            <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductPrice" Value='<%# DataBinder.Eval(Container.DataItem, "ProductPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidSumPrice" Value='<%# DataBinder.Eval(Container.DataItem, "SumPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDefaultAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromtionName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDeailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' />
                                                            <asp:HiddenField runat="server" ID="hidUnitName" Value='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMerchantCode" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
                                                            <asp:HiddenField runat="server" ID="HidMerchantName" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionTypeCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionTypeName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidLockAmountFlag" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidLockCheckbox" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFreeShippingCode" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShippingCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMinimumQty" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>' />
                                                            <asp:HiddenField runat="server" ID="hidGroupPrice" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFlagProSetHeader" Value='<%# DataBinder.Eval(Container.DataItem, "FlagProSetHeader")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFlagCombo" Value='<%# DataBinder.Eval(Container.DataItem, "FlagCombo")%>' />
                                                            <asp:HiddenField runat="server" ID="hidParentProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ParentProductCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountAmount")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty6" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!--Start Card of gvProductView-->
                            <div class="modal-content m-b-10">
                                <div class="modal-body">
                                    <div class="card-block">
                                        <div class="col-sm-4" style="float: right;">

                                            <div class="input-group" style="float: right;">
                                            </div>

                                        </div>

                                        <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none">ตะกร้าสินค้า</h5>

                                        <asp:HiddenField ID="hidPromotionTypeCodeMOQ" runat="server" />
                                        <asp:HiddenField ID="hidAmountChange" runat="server" />
                                        <asp:HiddenField ID="hidCampaignCodeMOQ" runat="server" />
                                        <asp:HiddenField ID="hidPromotionCodeMOQ" runat="server" />
                                        <asp:HiddenField ID="hidPromotionNameMOQ" runat="server" />
                                        <asp:HiddenField ID="hidMOQAmount" runat="server" />
                                        <asp:HiddenField ID="hidPromotionMOQPrice" runat="server" />
                                        <asp:HiddenField ID="hidUnitMOQ" runat="server" />
                                        <asp:HiddenField ID="hidUnitNameMOQ" runat="server" />
                                        <asp:HiddenField ID="hidFreeShppingMOQ" runat="server" />
                                        <asp:HiddenField ID="hidLockCheckboxMOQ" runat="server" />
                                        <asp:HiddenField ID="hidMerchantCodeMOQ" runat="server" />
                                        <asp:HiddenField ID="hidMerchantNameMOQ" runat="server" />
                                        <asp:HiddenField ID="hidFlagProSetHeaderMOQ" runat="server" />
                                        <asp:HiddenField ID="hidParentPromotionCodeMOQ" runat="server" />
                                        <asp:HiddenField ID="hidParentProductCodeMOQ" runat="server" />
                                        <asp:HiddenField ID="hidMOQFlagMOQ" runat="server" />
                                        <asp:HiddenField ID="hidMinimumQtyMOQ" runat="server" />
                                        <asp:HiddenField ID="hidRunningNoMOQ" runat="server" />


                                        <asp:GridView ID="gvProductView" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowDataBound="gvProductView_RowDataBound"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ราคา</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# GetTextPrice(DataBinder.Eval(Container.DataItem, "ParentProductCode"), (DataBinder.Eval(Container.DataItem, "ParentProductCode").ToString() == "-PromotionNewPrice") ? DataBinder.Eval(Container.DataItem, "ProductPrice").ToString() + "," + DataBinder.Eval(Container.DataItem, "Price").ToString() : DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"),DataBinder.Eval(Container.DataItem, "ColorCode"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">จำนวน</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox Style="width: 40px; text-align: right;" ID="txtAmount" AutoPostBack="True" min="0" max="99999" Text='<%# Eval("Amount") %>' OnTextChanged="txtAmountView_TextChanged" runat="server" TextMode="Number"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">หน่วย</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <%--    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ร้านค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รวม</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSumPrice" Text='<%# DataBinder.Eval(Container.DataItem, "SumPrice", "{0:n}")%>' runat="server" />

                                                        <asp:HiddenField ID="hidCampaignCategoryCode" runat="server" Value='<%# Eval("CampaignCategory") %>' />
                                                        <asp:HiddenField ID="hidCampaignCode" runat="server" Value='<%# Eval("CampaignCode") %>' />
                                                        <asp:HiddenField ID="hidPromotionCode" runat="server" Value='<%# Eval("PromotionCode") %>' />
                                                        <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                        <asp:HiddenField ID="hidProductName" runat="server" Value='<%# Eval("ProductName") %>' />
                                                        <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProductPrice" Value='<%# DataBinder.Eval(Container.DataItem, "ProductPrice")%>' />
                                                        <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSumPrice" Value='<%# DataBinder.Eval(Container.DataItem, "SumPrice")%>' />
                                                        <asp:HiddenField runat="server" ID="hidLockCheckbox" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                                        <asp:HiddenField runat="server" ID="hidLockAmountFlag" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidDefaultAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionDeailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                                        <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' />
                                                        <asp:HiddenField runat="server" ID="hidUnitName" Value='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' />
                                                        <asp:HiddenField ID="hidComboName" runat="server" Value='<%# Eval("ComboName") %>' />
                                                        <asp:HiddenField ID="hidComboCode" runat="server" Value='<%# Eval("ComboCode") %>' />
                                                        <asp:HiddenField runat="server" ID="hidRunning" Value='<%# DataBinder.Eval(Container.DataItem, "runningNo")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFlagProSetHeader" Value='<%# DataBinder.Eval(Container.DataItem, "FlagProSetHeader")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProhMinQtyHeaderFlag" Value='<%# DataBinder.Eval(Container.DataItem, "ProhMinQtyHeaderFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMOQNewHeaderFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQNewHeaderFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMerchantCode" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
                                                        <asp:HiddenField runat="server" ID="HidMerchantName" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFreeShippingCode" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShipping")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFlagCombo" Value='<%# DataBinder.Eval(Container.DataItem, "FlagCombo")%>' />
                                                        <asp:HiddenField runat="server" ID="hidParentPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "ParentPromotionCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidParentProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ParentProductCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionTypeCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionTypeName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMinimumQty" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>' />
                                                        <asp:HiddenField runat="server" ID="hidGroupPrice" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProductDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountPercent")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProductDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountAmount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountPercent")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountAmount")%>' />






                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                    <HeaderTemplate>

                                                        <div align="center"></div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnClose" AutoPostBack="True" OnClick="btnCloseView_Click" runat="server"><i class="ti-close"></i></asp:LinkButton>


                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty8" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                                <!--End Card of gvProductView-->
                                <div class="col-sm-12 text-center m-t-10 " style="pointer-events: auto;">
                        <!--<button type="button" class="button-active button-submit m-r-10 ">Add &nbsp; To &nbsp; Cart   </button>-->
                         <asp:Button ID="BtnAddtoCart" CssClass="button-pri button-accept m-r-10"  OnClick="BtnAddtoCart_Click" Text="AddtoCart" runat="server"/>
                         <asp:Button ID="btnCloseAddtoCart" runat="server" CssClass="button-pri button-cancel" Text="Close" OnClick="btnCloseAddtoCart_Click" />
                      </div>
                         </div>
                            <div id="mediaplan" runat="server"> 
                            <div class="modal-content m-b-10">
                                 <div class="modal-body">
                                     
                        <asp:HiddenField ID="hidCampaignMedia" runat="server" />
                        <asp:HiddenField ID="hidPromotionMedia" runat="server" />
                        <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none ;text-transform:unset" >Media Time</h5>
                                 <table class="table-p-stand" style="width:100%">
                                     <thead>
                                         <tr>
                                             <th style="text-align:center ;">Call in Time</th>
                                             <th style="text-align:center ;">Campaign Media Start Date</th>
                                             <th style="text-align:center ;">Campaign Media Expire Date</th>
                                             <th style="text-align:center ;">Media Plan Time</th>                                             
                                            
                                         </tr>
                                     </thead>
                                     <tbody>
                                         <tr style="background-color: #f4f4f4;"  >
                                               <td style="text-align:center ;"><asp:Label ID="lblCallinTime" runat="server"></asp:Label></td>
                                             <td style="text-align:center ;"><asp:Label ID="lblCampaignMediaStartDate" runat="server"></asp:Label></td>
                                             <td style="text-align:center ;"><asp:Label ID="lblCampaignMediaExpireDate" runat="server"></asp:Label></td>
                                              <td style="text-align:center ;"><asp:Label ID="lblMediaPlanTime" runat="server"></asp:Label></td>                                             
                                         </tr>
                                         
                                     </tbody>
                                 </table>
                             </div>
                                 </div>
                            <div class="modal-content m-b-10">
                                 <div class="modal-body">
                                     
                            <div class="col-sm-4"  style=" float: right;">
                                <div class="input-group" style=" float: right;">
                                      <asp:TextBox id="txtSearchCampaign1" runat="server" class="form-control m-r-10" placeholder="ค้นหาแคมเปญ"></asp:TextBox>
                                            <asp:LinkButton class="button-search " id="btnSearchCampaign1" OnClick="btnSearchCampaignMediaPlan_Click" runat="server"><i class="ti-search m-r-5"></i>ค้นหา</asp:LinkButton>
                                </div> 
                              </div>
                        <style>
                            .fixed_header tbody{
  display:block;
  overflow:auto;
  height:200px;
  
}

                        </style>
                        <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none" >แคมเปญ</h5>

                                 <div class="table-wrapper-scroll-y my-custom-scrollbar " style="width: 100%; padding: 10px">
                                        <asp:GridView ID="gvCampaignMedia" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " border="0" Style="width: 100%"
                                            CellSpacing="0" OnRowCommand="gvCampaignMedia_RowCommand"
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสแคมเปญ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelectCampaignMediaCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' runat="Server" CommandName="SelectCampaignMedia" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อแคมเปญ</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCampaignMediaName" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' runat="server" />

                                                        <asp:HiddenField runat="server" ID="hidActive" Value="N" />
                                                        <asp:HiddenField runat="server" ID="hidCampaignId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidCampaignName" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                                        <asp:HiddenField runat="server" ID="hidNotifyDate" Value='<%# ((null == Eval("NotifyDate"))||("" == Eval("NotifyDate"))) ? string.Empty : DateTime.Parse(Eval("NotifyDate").ToString()).ToString("dd/MM/yyyy") %>' />
                                                        <asp:HiddenField runat="server" ID="hidExpireDate" Value='<%# ((null == Eval("ExpireDate"))||("" == Eval("ExpireDate"))) ? string.Empty : DateTime.Parse(Eval("ExpireDate").ToString()).ToString("dd/MM/yyyy") %>' />

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

                            <!--<div class="modal-content m-b-10">
                                 <div class="modal-body">
                                     
                        <div class="col-sm-4"  style=" float: right;">
                              <div class="input-group" style=" float: right;">
                                    <asp:TextBox id="txtSearchProduct1" runat="server" class="form-control m-r-10" placeholder="ค้นหาสินค้า"></asp:TextBox>
                                    <asp:LinkButton class="button-search " id="btnSearchProduct1" OnClick="btnSearchProduct_Click" runat="server"><i class="ti-search m-r-5"></i>ค้นหา</asp:LinkButton>
                              </div>
                           
                            </div>
                          <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none" >โปรโมชั่น</h5> 
                                 <table class="table-p-stand" style="width:100%">
                                     <thead>
                                         <tr>
                                             <th>รหัสโปรโมชัน</th>
                                             <th>ชื่อโปรโมชัน</th>
                                           
                                         </tr>
                                     </thead>
                                     <tbody>
                                      
                                         <tr>
                                             <td> ProAS0007</td>
                                             <td>ราคาพิเศษประจำเดือนกรกฏาคม</td>
                                           
                                         </tr>
                                         <tr  style="background-color: Cyan;">
                                             <td> ProAS0005</td>
                                             <td>ลด 30% ทุกรายการ</td>
                                           
                                         </tr>

                                     </tbody>
                                 </table>
                             </div>
                                 </div>-->

                            <div class="modal-content m-b-10">
                                <div class="modal-body">

                                    <div class="col-sm-4" style="float: right;">
                                        <div class="input-group" style="float: right;">
                                            <asp:TextBox id="txtSearchPromotionMedia" runat="server" class="form-control m-r-10" placeholder="ค้นหาโปรโมชั่นมีเดีย"></asp:TextBox>
                                            <asp:LinkButton class="button-search " id="linkbuttonSearchPromotionMedia" OnClick="linkbtnSearchPromotionMedia_Click" runat="server"><i class="ti-search m-r-5"></i>ค้นหา</asp:LinkButton>
                                        </div>

                                    </div>
                                    <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">โปรโมชั่น</h5>
                                    <asp:GridView ID="gvPromotionMedia" runat="server" AutoGenerateColumns="False"
                                        Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvPromotionMedia_RowCommand"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">รหัสโปรโมชัน</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelectPromotionCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="Server" CommandName="SelectPromotionMedia" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div align="center">ชื่อโปรโมชัน</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                                    <asp:HiddenField runat="server" ID="hidActive" Value="N" />
                                                    <asp:HiddenField runat="server" ID="hidPromotionId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionId")%>' />
                                                    <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidPromotionName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' />
                                                    <asp:HiddenField runat="server" ID="hidPromotionTypeCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidLockAmountFlag" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                                    <asp:HiddenField runat="server" ID="hidLockCheckbox" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                                    <asp:HiddenField runat="server" ID="hidFreeShippingCode" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShippingCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                    <asp:HiddenField runat="server" ID="hidGroupPrice" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <EmptyDataTemplate>
                                            <center>
                                    <asp:Label ID="lblDataEmpty1" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                </div>
                            </div>
                            <!--ComboPartMedia-->
                            <div id="combosetPartMedia" runat="server">
                                <div id="combosetMedia" runat="server" class="modal-content m-b-10">

                                    <div class="modal-body">
                                        <div class="card-block">
                                            <div class="col-sm-4" style="float: right;">
                                                <div class="input-group" style="float: right;">
                                                    <input type="text" class="form-control m-r-10" placeholder="ค้นหาคอมโบเซ็ต">
                                                    <button class="button-search " id="basic-addon13"><i class="ti-search m-r-5"></i>ค้นหา</button>
                                                </div>
                                            </div>
                                            <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">คอมโบเซ็ต</h5>
                                            <asp:GridView ID="gvCombosetMedia" runat="server" AutoGenerateColumns="False"
                                                Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvCombosetMedia_RowCommand"
                                                ShowHeaderWhenEmpty="false">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">รหัสคอมโบ</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelectComboSetCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' runat="Server" CommandName="SelectComboSetMedia" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ชื่อคอมโบ</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionDetailName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ราคาชุดคอมโบ(บาท)</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />

                                                            <asp:HiddenField runat="server" ID="hidCombosetCode" Value='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDetailName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShippingCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDeailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty3" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            <!--
                                <table class="table-p-stand" style="width:100%"><thead>
                              <tr>
                                <th>รหัส</th>
                                <th>ชื่อ</th>
                               
                              </tr>
                             </thead>
                              <tbody>
                                <tr>
                              <td>COMB001</td>
                              <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                           
                             </tr>
                             <tr>
                                <td>COMB001</td>
                                <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                             
                               </tr>
                               <tr>
                                  <td>COMB001</td>
                                  <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                               
                                 </tr>
                                 <tr>
                                    <td>COMB001</td>
                                    <td>เครื่องใช้ไฟฟ้าลด 70%</td>
                                 
                                   </tr>
                           
                             </tbody>
                            </table>-->
                                        </div>
                                    </div>
                                </div>

                                <div id="subpromotiondetailMedia" runat="server" class="row">
                                    <div class="col-md-6">
                                        <div class="modal-content m-b-10">
                                            <div class="modal-body">
                                                <div class="card-block">
                                                    <div class="col-sm-8" style="float: right;">

                                                        <div class="input-group" style="float: right;">
                                                            <input type="text" class="form-control m-r-10" placeholder="ค้นหาคอมโบเซ็ต">
                                                            <button class="button-search" id="basic-addon14"><i class="ti-search m-r-5"></i>ค้นหา</button>
                                                        </div>

                                                    </div>

                                                    <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">สินค้าของชุดคอมโบ</h5>



                                                    <asp:GridView ID="gvSubMainPromotionDetailMedia" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSubPromotionDetailMedia_RowDataBound"
                                                        Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvSubMainPromotionDetailMedia_RowCommand"
                                                        ShowHeaderWhenEmpty="false">

                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">รหัสสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMainProductCodexx" Text='<%# DataBinder.Eval(Container.DataItem, "MainProductCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">ชื่อสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMainProductNamexx" Text='<%# DataBinder.Eval(Container.DataItem, "MainProductName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">จำนวน</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">เลือกสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>


                                                                    <asp:LinkButton ID="btnSelectExchangeProduct" class="font12Blue" Text="Exchange" runat="Server" CommandName="SelectExchangeProductMedia" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                    <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode ")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidMainProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "MainProductCode")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidMainProductName" Value='<%# DataBinder.Eval(Container.DataItem, "MainProductName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFlagSubPromotionDetailMain" Value='<%# DataBinder.Eval(Container.DataItem, "FlagSubPromotionDetailMain")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidSubMainPromotionDetailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "SubMainPromotionDetailInfoId")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "UNIT")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUnitName" Value='<%# DataBinder.Eval(Container.DataItem, "UNITName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShipping")%>' />

                                                                    <asp:HiddenField runat="server" ID="hidPromotionDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailId")%>' />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>

                                                        <EmptyDataTemplate>
                                                            <center>
                                    <asp:Label ID="lblDataEmpty3" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div class="m-t-10 text-center">

                                                    <asp:LinkButton ID="btnAddComboMedia" runat="Server" OnClick="btnAddComboMedia_Click" class="button-pri button-accept">ยืนยัน</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelComboMedia" runat="Server" class=" button-pri button-cancel" OnClick="btnCancelComboMedia_Click">ยกเลิก</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="modal-content m-b-10">
                                            <div class="modal-body">
                                                <div class="card-block">
                                                    <div class="col-sm-8" style="float: right;">

                                                        <div class="input-group" style="float: right;">
                                                            <input type="text" class="form-control m-r-10" placeholder="ค้นหาคอมโบเซ็ต">
                                                            <button class="button-search " id="basic-addon7"><i class="ti-search m-r-5"></i>ค้นหา</button>
                                                        </div>

                                                    </div>
                                                    <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none">เปลี่ยนได้ของชุดคอมโบ</h5>


                                                    <asp:GridView ID="gvSubExchangePromotiondetailInfoMedia" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvSubExchangePromotiondetailInfoMedia_RowCommand"
                                                        ShowHeaderWhenEmpty="false">

                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <center>
                                        </center>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnSelectSubProduct" runat="Server" CommandName="SelectSubProductMedia" class="button-pri button-activity  button-activity"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">เลือก</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">รหัสสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExchangeProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">ชื่อสินค้า</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExchangeProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                                <HeaderTemplate>
                                                                    <div align="center">จำนวน</div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                                                    <asp:HiddenField runat="server" ID="hidExchangeProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductCode")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidExchangeProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidExchangeAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidSubMainExchangeID" Value='<%# DataBinder.Eval(Container.DataItem, "SubMainExchangeID")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "UNIT")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidUNITName" Value='<%# DataBinder.Eval(Container.DataItem, "UNITName")%>' />
                                                                    <asp:HiddenField runat="server" ID="hidFreeShipping" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShipping")%>' />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>

                                                        <%--  <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty5" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>--%>
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>

                            <div id="PromotiondetailbyProductMedia" runat="server">
                                <div class="modal-content m-b-10">
                                    <div class="modal-body">
                                        <div class="card-block">
                                            <div class="col-sm-4" style="float: right;">

                                                <div class="input-group" style="float: right;">
                                                 <asp:TextBox id="txtSearchProductMedia" runat="server" class="form-control m-r-10" placeholder="ค้นหาสินค้า"></asp:TextBox>
                                                 <asp:LinkButton class="button-search " id="btnSearchProductMedia" OnClick="btnSearchProductMedia_Click" runat="server"><i class="ti-search m-r-5"></i>ค้นหา</asp:LinkButton>
                                                </div>

                                            </div>

                                            <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none">สินค้า</h5>
                                            <asp:Button ID="btnSelectSetMedia" runat="server" />
                                            <asp:HiddenField ID="hidProductCodeViewMedia" runat="server" />
                                            <asp:HiddenField ID="hidProductNameViewMedia" runat="server" />
                                            <asp:HiddenField ID="hidPriceViewMedia" runat="server" />
                                            <asp:HiddenField ID="hidAmountViewMedia" runat="server" />
                                            <asp:HiddenField ID="hidUnitNameViewMedia" runat="server" />
                                            <asp:HiddenField ID="hidMerchantNameViewMedia" runat="server" />
                                            <asp:HiddenField ID="hidSumPriceViewMedia" runat="server" />
                                            <asp:HiddenField ID="hidPromotionSetAmountMedia" runat="server" />
                                            <asp:HiddenField ID="hidFlagNotDupeMedia" runat="server" />
                                            <asp:HiddenField ID="hidPriceProSetMedia" runat="server" />
                                            <asp:GridView ID="gvProductMedia" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProductMedia_RowDataBound"
                                                Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowCommand="gvProductMedia_OnRowCommand"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>                                            
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelectProduct" runat="Server" CommandName="SelectProductMedia"
                                                                class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">รหัสสินค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="LockCheckbox" Text='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' runat="server" />--%>
                                                             <asp:LinkButton ID="lblProductCode" class="font12Blue" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="Server" CommandName="ShowProductScriptMedia" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <%--<asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ชื่อสินค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ราคา</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPriceBeforeDisCount" style="text-decoration:line-through" Text='<%# DataBinder.Eval(Container.DataItem, "PriceB4ofPrdDisc", "{0:n}")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:n}")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">จำนวน</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox Style="width: 40px; text-align: right;" ID="txtAmount" AutoPostBack="True" min="0" max="99999" Text='<%# Eval("Amount") %>' OnTextChanged="txtAmountgvProductMedia_TextChanged" runat="server" TextMode="Number"></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">หน่วย</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                <%--    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">ร้านค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <div align="center">รวม</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSumPrice" Text='<%# DataBinder.Eval(Container.DataItem, "SumPrice", "{0:n}")%>' runat="server" />

                                                            <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductPrice" Value='<%# DataBinder.Eval(Container.DataItem, "ProductPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidSumPrice" Value='<%# DataBinder.Eval(Container.DataItem, "SumPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDefaultAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromtionName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDeailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' />
                                                            <asp:HiddenField runat="server" ID="hidUnitName" Value='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMerchantCode" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
                                                            <asp:HiddenField runat="server" ID="HidMerchantName" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionTypeCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionTypeName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidLockAmountFlag" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidLockCheckbox" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFreeShippingCode" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShippingCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                            <asp:HiddenField runat="server" ID="hidMinimumQty" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>' />
                                                            <asp:HiddenField runat="server" ID="hidGroupPrice" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFlagProSetHeader" Value='<%# DataBinder.Eval(Container.DataItem, "FlagProSetHeader")%>' />
                                                            <asp:HiddenField runat="server" ID="hidFlagCombo" Value='<%# DataBinder.Eval(Container.DataItem, "FlagCombo")%>' />
                                                            <asp:HiddenField runat="server" ID="hidParentProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ParentProductCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountAmount")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountPercent")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPromotionDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountAmount")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty6" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--StartProductViewMedia-->
                            <div class="modal-content m-b-10">
                                <div class="modal-body">
                                    <div class="card-block">
                                        <div class="col-sm-4" style="float: right;">

                                            <div class="input-group" style="float: right;">
                                            </div>

                                        </div>

                                        <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none">ตะกร้าสินค้า</h5>

                                        <asp:GridView ID="gvProductViewMedia" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CellSpacing="0" CssClass="table-p-stand" OnRowDataBound="gvProductViewMedia_RowDataBound" 
                                            ShowHeaderWhenEmpty="true">

                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รหัสสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ชื่อสินค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ราคา</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# GetTextPrice(DataBinder.Eval(Container.DataItem, "ParentProductCode"), (DataBinder.Eval(Container.DataItem, "ParentProductCode").ToString() == "-PromotionNewPrice") ? DataBinder.Eval(Container.DataItem, "ProductPrice").ToString() + "," + DataBinder.Eval(Container.DataItem, "Price").ToString() : DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"),DataBinder.Eval(Container.DataItem, "ColorCode"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">จำนวน</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox Style="width: 40px; text-align: right;" ID="txtAmount" AutoPostBack="True" min="0" max="99999" Text='<%# Eval("Amount") %>' OnTextChanged="txtAmountViewMedia_TextChanged" runat="server" TextMode="Number"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">หน่วย</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">ร้านค้า</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="center">รวม</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSumPrice" Text='<%# DataBinder.Eval(Container.DataItem, "SumPrice", "{0:n}")%>' runat="server" />

                                                        <asp:HiddenField ID="hidCampaignCategoryCode" runat="server" Value='<%# Eval("CampaignCategory") %>' />
                                                        <asp:HiddenField ID="hidCampaignCode" runat="server" Value='<%# Eval("CampaignCode") %>' />
                                                        <asp:HiddenField ID="hidPromotionCode" runat="server" Value='<%# Eval("PromotionCode") %>' />
                                                        <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                        <asp:HiddenField ID="hidProductName" runat="server" Value='<%# Eval("ProductName") %>' />
                                                        <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProductPrice" Value='<%# DataBinder.Eval(Container.DataItem, "ProductPrice")%>' />
                                                        <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidSumPrice" Value='<%# DataBinder.Eval(Container.DataItem, "SumPrice")%>' />
                                                        <asp:HiddenField runat="server" ID="hidLockCheckbox" Value='<%# DataBinder.Eval(Container.DataItem, "LockCheckbox")%>' />
                                                        <asp:HiddenField runat="server" ID="hidLockAmountFlag" Value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidDefaultAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionDeailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailId")%>' />
                                                        <asp:HiddenField runat="server" ID="hidDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                                        <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' />
                                                        <asp:HiddenField runat="server" ID="hidUnitName" Value='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' />
                                                        <asp:HiddenField ID="hidComboName" runat="server" Value='<%# Eval("ComboName") %>' />
                                                        <asp:HiddenField ID="hidComboCode" runat="server" Value='<%# Eval("ComboCode") %>' />
                                                        <asp:HiddenField runat="server" ID="hidRunning" Value='<%# DataBinder.Eval(Container.DataItem, "runningNo")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFlagProSetHeader" Value='<%# DataBinder.Eval(Container.DataItem, "FlagProSetHeader")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProhMinQtyHeaderFlag" Value='<%# DataBinder.Eval(Container.DataItem, "ProhMinQtyHeaderFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMOQNewHeaderFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQNewHeaderFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMerchantCode" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
                                                        <asp:HiddenField runat="server" ID="HidMerchantName" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFreeShippingCode" Value='<%# DataBinder.Eval(Container.DataItem, "FreeShipping")%>' />
                                                        <asp:HiddenField runat="server" ID="hidFlagCombo" Value='<%# DataBinder.Eval(Container.DataItem, "FlagCombo")%>' />
                                                        <asp:HiddenField runat="server" ID="hidParentPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "ParentPromotionCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidParentProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ParentProductCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionTypeCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeCode")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionTypeName" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionTypeName")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMOQFlag" Value='<%# DataBinder.Eval(Container.DataItem, "MOQFlag")%>' />
                                                        <asp:HiddenField runat="server" ID="hidMinimumQty" Value='<%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>' />
                                                        <asp:HiddenField runat="server" ID="hidGroupPrice" Value='<%# DataBinder.Eval(Container.DataItem, "GroupPrice")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProductDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountPercent")%>' />
                                                        <asp:HiddenField runat="server" ID="hidProductDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "ProductDiscountAmount")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionDiscountPercent" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountPercent")%>' />
                                                        <asp:HiddenField runat="server" ID="hidPromotionDiscountAmount" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDiscountAmount")%>' />






                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                    <HeaderTemplate>

                                                        <div align="center"></div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnClose" AutoPostBack="True" OnClick="btnCloseViewMedia_Click" runat="server"><i class="ti-close"></i></asp:LinkButton>


                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                            <EmptyDataTemplate>
                                                <center>
                                    <asp:Label ID="lblDataEmpty8" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <!--<div class="modal-content m-b-10">
                                 <div class="modal-body">
                                         <div class="col-sm-4"  style=" float: right;"> 
                                    
                              <div class="input-group" style=" float: right;"> 
                                  <input type="text" class="form-control m-r-10" placeholder="ค้นหาสินค้า">
                                  <button class="button-search" id="basic-addon17"><i class="ti-search m-r-5"></i>ค้นหา</button>
                              </div>
                           
                            </div>
                            
                        <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none" >สินค้า</h5>
                                 <table class="table-p-stand" style="width:100%">
                                     <thead>
                                         <tr>
                                             <th>รหัสสินค้า</th>
                                             <th>ชื่อสินค้า</th>
                                             <th>ราคา</th>
                                             <th>จำนวน</th>
                                             <th>หน่วย</th>
                                             <th>รวม</th>
                                         </tr>
                                     </thead>
                                     <tbody>
                                         <tr  style="background-color: Cyan;">
                                             <td>P0000460</td>
                                             <td>Genie Classic or Jegging</td>
                                             <td ><span style="color:red; margin-right:10px; text-decoration: line-through;">1,990</span>1,393</td>
                                             <td><input type="number"value="1" style="width:40px;text-align:right;" /></td>
                                             <td >เครื่อง</td>
                                             <td>1,393</td>
                                         </tr>
                                         <tr>
                                              <td>P0000457</td>
                                             <td> เครื่องเล่นเพลง มนต์เพลงลูกทุ่ง รุ่น S-8</td>
                                             <td><span style="color:red; margin-right:10px; text-decoration: line-through;">9,900</span>6,930</td>
                                             <td><input type="number"value="1" style="width:40px;text-align:right;"/></td>
                                             <td>เครื่อง</td>
                                             <td>6,930</td>
                                         </tr>
                                           <tr>
                                              <td>P0000459</td>
                                             <td>BIG VISIONแว่นตาขยายไร้มือจับ+CLIP ON LED</td>
                                             <td><span style="color:red; margin-right:10px; text-decoration: line-through;">1,950</span>1,365</td>
                                             <td><input type="number"value="1"style="width:40px;text-align:right;" /></td>
                                             <td>อัน</td>
                                             <td>1,365</td>
                                         </tr>
                                     </tbody>
                                 </table>
                             </div>
                                 </div>-->



                            <!--<div class="modal-content m-b-10">
                                 <div class="modal-body">
                                     
                            
                        <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none" >สินค้า</h5>
                                 <table class="table-p-stand" style="width:100%">
                                     <thead>
                                         <tr>
                                             <th>รหัสสินค้า</th>
                                             <th>ชื่อสินค้า</th>
                                             <th>ราคา</th>
                                             <th>จำนวน</th>
                                             <th>หน่วย</th>
                                             <th>รวม</th>
                                         </tr>
                                     </thead>
                                     <tbody>
                                         <tr>
                                             <td>P0000460</td>
                                             <td>Genie Classic or Jegging</td>
                                             <td>1,393</td>
                                             <td>1</td>
                                             <td>เครื่อง</td>
                                             <td>1,393</td>
                                         </tr>
                                     
                                     </tbody>
                                 </table>
                             </div>
                                 </div>-->
                                  <div class="col-sm-12 text-center m-t-10 " style="pointer-events: auto;">
                        <!--<button type="button" class="button-active button-submit m-r-10 ">Add &nbsp; To &nbsp; Cart   </button>-->
                         <asp:Button ID="BtnAddtoCartMedia" CssClass="button-pri button-accept m-r-10" OnClick="BtnAddtoCartMedia_Click" Text="AddtoCart" runat="server"/>
                         <asp:Button ID="btnCloseAddtoCartMedia" runat="server" CssClass="button-pri button-cancel" Text="Close" OnClick="btnCloseAddtoCartMedia_Click" />
                      </div>
                            </div>
                        </ContentTemplate>
                       </asp:UpdatePanel>
              </div>
            
            
            
            
            
            
            
            
            
            
            
            </div> 
            </div>


           
                    
   
  
 
  
<!-- General JS Scripts -->
<script src="../assets/js/jquery-3.3.1.min.js"></script> 
<script src="../assets/js/popper.min.js"></script>
<script src="../assets/js/bootstrap.min.js" ></script>
<script src="../assets/js/jquery.nicescroll.min.js"></script>
<script src="../assets/js/moment.min.js"></script>
<script src="../assets/js/stisla.js"></script>

<!-- upload file -->
<script src="../assets/js/jquery.filer/js/jquery.filer.min.js"></script>
<script src="../assets/js/filer/custom-filer.js"></script>
<script src="../assets/js/filer/jquery.fileuploads.init.js"></script>

<!-- upload file -->


<script src="../assets/js/scripts.js"></script>
<script src="../assets/js/custom.js"></script>
<script type="text/javascript">
  function showHideDiv(ele) {
    var srcElement = document.getElementById(ele);
    if (srcElement != null) {
      if (srcElement.style.display == "block") {
        srcElement.style.display = 'none';
      }
      else {
        srcElement.style.display = 'block';
      }
      return false;
    }
  }
</script>

      
</asp:Content>
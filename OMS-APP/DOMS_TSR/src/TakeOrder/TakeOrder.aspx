<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="TakeOrder.aspx.cs" Inherits="DOMS_TSR.src.TakeOrderTSR.TakeOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    
<!-- General JS Scripts -->
<script src="assets/js/jquery.filer/js/jquery.filer.min.js"></script>
<script src="assets/js/filer/custom-filer.js"></script>
<script src="assets/js/filer/jquery.fileuploads.init.js"></script>

<!-- upload file -->


<script src="assets/js/scripts.js"></script>
<script src="assets/js/custom.js"></script>
<script src="assets/js/jquery-3.3.1.min.js"></script> 
<script src="assets/js/popper.min.js"></script>
<script src="assets/js/bootstrap.min.js" ></script>
<script src="assets/js/jquery.nicescroll.min.js"></script>
<script src="assets/js/moment.min.js"></script>
<script src="assets/js/stisla.js"></script>
<script type="text/javascript">
    function checkRadioBtn(id) {
        var gvPopupCustomerAddress = document.getElementById('<%=gvPopupCustomerAddress.ClientID %>');
        for (var i = 1; i < gvPopupCustomerAddress.rows.length; i++) {
            var radioBtn = gvPopupCustomerAddress.rows[i].cells[0].getElementsByTagName("input");
            // Check if the id not same
            if (radioBtn[0].id != id.id) {
                radioBtn[0].checked = false;
            }
        }
    }
</script>
<script type="text/javascript">
    function checkRadioBtn1(id) {
        var gvCustomerAddressReceipt = document.getElementById('<%=gvCustomerAddressReceipt.ClientID %>');
        for (var i = 1; i < gvCustomerAddressReceipt.rows.length; i++) {
            var radioBtn = gvCustomerAddressReceipt.rows[i].cells[0].getElementsByTagName("input");
            // Check if the id not same
            if (radioBtn[0].id != id.id) {
                radioBtn[0].checked = false;
            }
        }
    }
</script>

<!-- upload file -->

    <link rel="stylesheet" href="/assets/js/bootstrap.min.css">
<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
  
<link rel="stylesheet" type="text/css" href="assets/icon/icofont/css/flaticon.css">

<link rel="stylesheet" type="text/css" href="assets/icon/font-awesome/css/font-awesome.css">
<link rel="stylesheet" type="text/css" href="assets/icon/font-awesome/css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="assets/icon/ico-fonts/css/icofont.css">
<link rel="stylesheet" type="text/css" href="assets/icon/icofont/css/icofont.css">
<link rel="stylesheet" type="text/css" href="assets/icon/ion-icon/css/ionicons.min.css">
<link rel="stylesheet" type="text/css" href="assets/icon/material-design/css/material-design-iconic-font.min.css">
<link rel="stylesheet" type="text/css" href="assets/icon/themify-icons/themify-icons.css">
<link rel="stylesheet" type="text/css" href="assets/js/jquery.filer/css/jquery.filer.css">
<link rel="stylesheet" type="text/css" href="assets/js/jquery.filer/css/themes/jquery.filer-dragdropbox-theme.css">

<link rel="stylesheet" href="assets/css/style.css">
<!-- <link rel="stylesheet" href="/assets/css/ssre.css"> -->




<link rel="stylesheet" href="assets/css/components.css">
<link rel="stylesheet" type="text/css" href="assets/icon/icofont/css/flaticon.css">
<style>

.card .card-header{
  padding: 0px 20px 0px 10px;
}
.sub-title{
  padding-bottom: 0px;

}
.card-block {
  padding: 0.60rem;
  padding-top: 0rem;
  padding-bottom: 0px;
}
.card .card-header .card-header-right{
  top:7px;
}
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

padding-top: 3px;

}
.table td, .table th{
padding: 5px;
}
.table > thead > tr > th {
background-color: #00ACEC;

}






</style>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

         <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

 
     
      <!-- Main Content -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
          <div class="page-wrapper">
              <div class="page-body">
                  <div class="row">
                      <div class=" col-md-9 col-pad-r">
                          <div class="card">
                              <div class="card-header">
                                  <div class="sub-title " style=" border-bottom: none; ">ตะกร้าสินค้า</div>
                               <div class="card-header-right"> 
                             
                                 <input type="hidden" id="hidIdList" runat="server" />
                                <input type="hidden" id="hidEmpCode" runat="server" />
                                <input type="hidden" id="hidCampCode" runat="server" />
                                <input type="hidden" id="hidFlagInsert" runat="server" />
                                <asp:HiddenField ID="hidFlagDel" runat="server" />
                                <input type="hidden" id="hidaction" runat="server" />
                                <asp:HiddenField ID="hidMsgDel" runat="server" />
                                <asp:HiddenField ID="hidAddressType" runat="server" />
                                <asp:HiddenField ID="hidCustomerCodeAddressType01" runat="server" />
                                <asp:HiddenField ID="hidCustomerCodeAddressType02" runat="server" />
                                <asp:HiddenField ID="hidCustomerAddressDeliveryIdUpd" runat="server" />
                                <asp:HiddenField ID="hidCustomerAddressReceiptIdUpd" runat="server" />
                                <asp:HiddenField ID="hidTabSelecteventCusAddressDelivery" runat="server" />
                                         <asp:LinkButton id="btnAddProduct" class="btn-add button-active btn-small"
                       OnClick="btnAddProduct_Click" runat ="server"><i class="fa fa-plus"></i>เพิ่มสินค้า</asp:LinkButton>
             
                               </div>
                                        </div>
                              <div class="card-block">

                                  <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap" OnRowDataBound="gvOrder_RowDataBound"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvOrder_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkOrderAll" OnCheckedChanged="chkOrderAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkOrder" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รหัสสินค้า</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      &nbsp;&nbsp;<asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ชื่อสินค้า</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ราคา</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                         <%#GetTextPrice(DataBinder.Eval(Container.DataItem, "ParentProductCode"),DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"))%>
                                   
                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">จำนวน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                           &nbsp;&nbsp;<asp:TextBox ID="txtAmount"  AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"  TextMode="Number" Style="text-align:right" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">หน่วย</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>                                        
                                        <asp:HiddenField runat="server" ID="hidPromotionDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailId")%>' />
                                        

                                        &nbsp;&nbsp;<asp:Label ID="lblUnit" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รวม</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;    <asp:Label ID="lbltotal" runat="server" />
                                      
                                         <asp:HiddenField ID="hidSumprice" runat="server" value='<%#GetPrice(DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"))%>' />
                                                            <asp:HiddenField ID="hidRunning" runat="server" value='<%# Eval("runningNo") %>' />
                                                            <asp:HiddenField ID="hidAmount" runat="server" value='<%# Eval("Amount") %>' />
                                                            <asp:HiddenField ID="hidParentProductCode" runat="server" value='<%# Eval("ParentProductCode") %>' />
                                                                  <asp:HiddenField ID="hidFlagCombo" runat="server" value='<%# Eval("FlagCombo") %>' />
                                                        
                                                            <asp:HiddenField ID="hidCampaignCategoryCode" runat="server" value='<%# Eval("CampaignCategory") %>' />
                                                                                    <asp:HiddenField ID="hidCampaignCategoryName" runat="server" value='<%# Eval("CampaignCategoryName") %>' />
                                                                               <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                                <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                              <asp:HiddenField ID="hidProductCode" runat="server" value='<%# Eval("ProductCode") %>' />
                                                                               <asp:HiddenField ID="hidProductName" runat="server" value='<%# Eval("ProductName") %>' />
                                                                                <asp:HiddenField ID="hidDiscountAmount" runat="server" value='<%# Eval("DiscountAmount") %>' />
                                                                               <asp:HiddenField ID="hidDiscountPercent" runat="server" value='<%# Eval("DiscountPercent") %>' />
                                                                               <asp:HiddenField ID="hidPrice" runat="server" value='<%# Eval("Price") %>' />
                                                                      <asp:HiddenField ID="hidComboName" runat="server" value='<%# Eval("ComboName") %>' />
                                                                      <asp:HiddenField ID="hidComboCode" runat="server" value='<%# Eval("ComboCode") %>' />
                                                                    
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
                          <div class="row">
                              <div class="col-md-4 col-pad-r" style="display:flex;">
                                  <div class="card">
                                      <div class="card-header">
                                          <div class="sub-title " >ส่วนลด</div>
                                      </div>
                                      <div class="card-block">
                                          <div class="form-group row ">
                                                  <div class="  col-sm-4">
                                                          <label class=" col-form-label " >
                                                              <input type="checkbox" value="">
                                                              
                                                              <span class="text-inverse"> บาท </span>
                                                          </label>
                                                      </div>

                                            
                                              <div class="col-sm-7">
                                                  <input type="text" class="form-control">
                                              </div>
                                              <div class=" col-sm-4">
                                                      <label class=" col-form-label  " >
                                                          <input type="checkbox" value="">
                                                      
                                                          <span class="text-inverse"> % </span>
                                                      </label>
                                                  </div>

                                        
                                          <div class="col-sm-7">
                                              <input type="text" class="form-control">
                                          </div>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                              <div class="col-md-8 col-pad-l ">
                                  <div class="card">
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
                                  </div>
                              </div>
                          </div>
                          <div class="row">
                              <div class="col-md-6 col-pad-r " style="display:grid">
                                  <div class="card">
                                      <div class="card-header">

                                          <div class="sub-title "  >ที่อยู่จัดส่ง  </div>
                                              <div class="card-header-right" style="top:2px"> 
                                                    <asp:LinkButton id="btneditaddress1" class="btn-edit button-active btn-small"
                                               OnClick="btneditaddress1_Click" runat ="server"><i class="fa fa-edit"></i>แก้ไข</asp:LinkButton>
                                                  

                                              </div>

                                          

                                      </div>
                                      <div class="card-block">
                                          <ul>
                                              <li>
                                                  <asp:Label ID="lblAddress01" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblSubDistrictName01" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblDistrictName01" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblProvinceName01" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblZipCode01" runat="server"></asp:Label>&nbsp;
                                                  <asp:HiddenField ID="hidSubDistrictCodeShow01" runat="server"/>
                                                  <asp:HiddenField ID="hidDistrictCodeShow01" runat="server"/>
                                                  <asp:HiddenField ID="hidProvinceCodeShow01" runat="server"/>
                                                  <asp:HiddenField ID="hidCustomerAddressIdShow01" runat="server"/>
                                              </li>
                                          </ul>
                                      </div>
                                  </div>
                              </div>
                              <div class="col-md-6 col-pad-l">
                                  <div class="card">
                                      <div class="card-header">

                                          <div class="sub-title " >ที่อยู่ออกใบกำกับภาษี </div>
                                          <div class="card-header-right" style="top:2px">  
                                              <asp:LinkButton id="btneditaddress2" class="btn-edit button-active btn-small"
                                               OnClick="btneditaddress2_Click" runat ="server"><i class="fa fa-edit"></i>แก้ไข</asp:LinkButton>
                                          </div>
                                         
                                      </div>
                                      <div class="card-block">
                                          <ul>

                                              <li>
                                                 
                                                      <label>
                                                          <asp:CheckBox ID="chkCustomerAddressSameDelivery" runat="server" OnCheckedChanged="chkCustomerAddressSameDelivery_Changed" AutoPostBack="true" />                                                          
                                                          <span class="text-inverse">ที่อยู่เดียวกันกับที่อยู่จัดส่ง </span>
                                                      </label>
                                                 
                                              </li>
                                              <ul>
                                              <li>
                                                  <asp:Label ID="lblAddress02" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblSubDistrictName02" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblDistrictName02" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblProvinceName02" runat="server"></asp:Label>&nbsp;
                                                  <asp:Label ID="lblZipCode02" runat="server"></asp:Label>&nbsp;
                                                  <asp:HiddenField ID="hidSubDistrictCodeShow02" runat="server"/>
                                                  <asp:HiddenField ID="hidDistrictCodeShow02" runat="server"/>
                                                  <asp:HiddenField ID="hidProvinceCodeShow02" runat="server"/>
                                                  <asp:HiddenField ID="hidCustomerAddressIdShow02" runat="server"/>
                                              </li>
                                          </ul>


                                          </ul>
                                      </div>
                                  </div>
                              </div>
                          </div>
                          <div class="row">
                              <div class="col-md-6  col-pad-r " style="display: inline-grid;">
                                  <div class="card">
                                      <div class="card-header">

                                          <div class="sub-title ">การจัดส่ง
                                          </div>
                                      </div>
                                      <div class="card-block">
                                          <ul>
                                              <li>
                                                  <div class="checkbox-fade fade-in-primary d-">
                                                      <label>
                                                          <input type="checkbox" value="">
                                                          <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                          <span class="text-inverse">Kerry &nbsp; (150บาท) </span>
                                                      </label>
                                                  </div>
                                              </li>
                                              <li>
                                                  <div class="checkbox-fade fade-in-primary d-">
                                                      <label>
                                                          <input type="checkbox" value="">
                                                          <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                          <span class="text-inverse">จัดส่งโดยบริษัท &nbsp; (200บาท) </span>
                                                      </label>
                                                  </div>
                                              </li>


                                          </ul>

                                      </div>

                                  </div>
                              </div>
                              <div class="col-md-6   col-pad-l">
                                  <div class="card">
                                      <div class="card-header">

                                          <div class="sub-title ">การชำระเงิน

                                          </div>
                                      </div>
                                      <div class="card-block">
                                          <ul>
                                              <li>
                                                  <div class="checkbox-fade fade-in-primary d-">
                                                      <label>
                                                          <input type="checkbox" value="">
                                                          <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                          <span class="text-inverse">ชำระเงินสดปลายทาง </span>
                                                      </label>
                                                  </div>
                                              </li>
                                              <li>
                                                  <div class="checkbox-fade fade-in-primary d-">
                                                      <label>
                                                          <input type="checkbox" value="">
                                                          <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                          <span class="text-inverse">เครดิต &nbsp; VISA / Master Card / JCB </span>
                                                      </label>
                                                  </div>
                                              </li>
                                              <li>
                                                  <div class="checkbox-fade fade-in-primary d-">
                                                      <label>
                                                          <input type="checkbox" value="">
                                                          <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                          <span class="text-inverse">iBanking </span>
                                                      </label>
                                                  </div>
                                              </li>
                                              <li>
                                                  <div class="checkbox-fade fade-in-primary d-">
                                                      <label>
                                                          <input type="checkbox" value="">
                                                          <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                          <span class="text-inverse">ชำระผ่าน ATM </span>
                                                      </label>
                                                  </div>
                                              </li>


                                          </ul>

                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class=" col-md-3 col-pad-l">
                          <div class="card">
                              <div class="card-header">

                                  <div class="sub-title ">ข้อมูลลูกค้า
                                          <div class="card-header-right">
                                            
                                              <asp:LinkButton ID="btneditcustomer" runat="server" CssClass="btn-mk-pri btn-edit" OnClick="btneditcustomer_Click" ><i class="fa fa-edit"></i>แก้ไข</asp:LinkButton>
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
                                              <td style="text-align:right ; "> <asp:Label ID="lblCustomerCode" runat="server"></asp:Label></td>
                                          </tr>
                                          <tr>
                                              <th style="text-align:left ; ">เบอร์ติดต่อ <span style="">:</span></th>
                                              <td style="text-align:right ;"> <asp:Label ID="lblCustomerPhone1" runat="server"></asp:Label></td>
                                          </tr>
                                 
                                      </tbody>
                                  </table>
                                  <div class="text-center m-t-0 m-b-10">
                                      <button type="button" class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 tsr">ประวัติการติดต่อ</button>
                                      <button type="button" class="btn  btn-round  btn-sm btn-warning waves-effect waves-light tsr">ประวัติการสั่งซื้อ</button>
                                  </div>
                         
                              </div>
                          </div>
                          <div class="card">
                              <div class="card-header">

                                  <div class="sub-title ">สินค้ายอดนิยม

                                  </div>
                              </div>
                              <div class="card-block">
                                  <ul>
                                      <li>
                                          
                                              <label>
                                               
                                                      <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10">1. <asp:Label ID="ProductNameTop1" runat="server"></asp:Label></span>
                                              </label>
                                        
                                      </li>
                                      <li>
                                          
                                              <label>
                                                 
                                                  <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10">2. <asp:Label ID="ProductNameTop2" runat="server"></asp:Label></span>
                                              </label>
                                          
                                      </li>
                                      <li>
                                         
                                              <label>
                                                  
                                                      <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10">3. <asp:Label ID="ProductNameTop3" runat="server"></asp:Label></span>
                                              </label>
                                          
                                      </li>
                                      <li>
                                         
                                              <label>
                                              
                                                      <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10">4. <asp:Label ID="ProductNameTop4" runat="server"></asp:Label></span>
                                              </label>
                                         
                                      </li>
                                      <li>
                                         
                                              <label>
                                                
                                                      <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10">5. <asp:Label ID="ProductNameTop5" runat="server"></asp:Label></span>
                                              </label>
                                          
                                      </li>


                                  </ul>

                              </div>
                          </div>
                          <div class="card">
                              <div class="card-header">

                                  <div class="sub-title " >ช่องทางการติดต่อ
                                  </div>
                              </div>
                              <div class="card-block">
                                  <ul>
                                      <li>
                                          <div class="checkbox-fade fade-in-primary d-">
                                              <label>
                                                  <input type="checkbox" value="">
                                                  <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                  <span class="text-inverse">เว็บไซต์สั่งซื้อแบบ Online </span>
                                              </label>
                                          </div>
                                      </li>
                                      <li>
                                          <div class="checkbox-fade fade-in-primary d-">
                                              <label>
                                                  <input type="checkbox" value="">
                                                  <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                  <span class="text-inverse">Mobile Application </span>
                                              </label>
                                          </div>
                                      </li>
                                      <li>
                                          <div class="checkbox-fade fade-in-primary d-">
                                              <label>
                                                  <input type="checkbox" value="">
                                                  <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                  <span class="text-inverse">LINE Official Account </span>
                                              </label>
                                          </div>
                                      </li>
                                      <li>
                                          <div class="checkbox-fade fade-in-primary d-">
                                              <label>
                                                  <input type="checkbox" value="">
                                                  <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                  <span class="text-inverse">LINE Business Connect </span>
                                              </label>
                                          </div>
                                      </li>


                                  </ul>

                              </div>
                          </div>
                          <div class="row ">
                          <div class="col-md-6" style="padding-right:1px">
                              <button type="button" class="btn btn-primary btn-md btn-block waves-effect text-center" style="background-color:#52DEA6; border-radius: 5px"> บันทึกการโทร </button>
                           
                          </div>
                          <div class="col-md-6 " style="padding-left:1px">                                                                  
                                  <asp:Button ID="btnSubmitOrder" runat="server" OnClick="btnSubmitOrder_Click" CssClass="btn btn-primary btn-md btn-block waves-effect text-center" Style="background-color:#07C9C9; border-radius: 5px" Text="สรุปจำนวนเงิน" />
                              </div>
                          </div>
                      </div>
                      <div id="styleSelector">

                      </div>

                      





                  </div>
              </div>
          </div>
 
             </ContentTemplate>
</asp:UpdatePanel>     


      <div class="modal fade" id="modal-product" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog" role="document" style="max-width:1000px">
                <div class="modal-content">
                  <div class="modal-header">
                   
                    
                    <h5 class="modal-title sub-title " style="font-size: 18px;">เพิ่มสินค้า  
                        
                       
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button> 
                  </div>
                  <div class="modal-body">
                    <div class="card-block">
                   <asp:UpdatePanel ID="upModal" runat="server">
                    <ContentTemplate>
                      <div class="col-sm-3"  style=" float: right;"> 
                          <div class="input-group" style=" float: right;">
                              <input type="text" class="form-control m-r-10" placeholder="ค้นหาแคมเปญ">
                              <button class="input-group-addon btn-import button-active " id="basic-addon5"><i class="ti-search"></i>ค้นหา</button>
                          </div>
                       
                        </div>
                        <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none" >แคมเปญ</h5>
                      
                        <div class="form-group m-b-30 "  >
                              <asp:HiddenField ID="hidCampaigncode" runat="server" />
                                                            
                         <asp:ListBox ID="lbCampaign" onclick="lbCampaign_SelectedIndexChanged"  CssClass="form-control selectric" style="height:150px;font-size:18px;" AutoPostBack="true" OnSelectedIndexChanged="lbCampaign_SelectedIndexChanged" runat="server" Width="100%" Height="120">
                        </asp:ListBox>
                      </div>
                      <div class="col-sm-3"  style=" float: right;"> 
                          <div class="input-group" style=" float: right;">
                              <input type="text" class="form-control m-r-10" placeholder="ค้นหาโปรโมชั่น">
                              <button class="input-group-addon btn-import button-active " id="basic-addon5"><i class="ti-search"></i>ค้นหา</button>
                          </div>
                       
                        </div>
                        <h5 class="modal-title sub-title" style="font-size: 18px; border-bottom: none" >โปรโมชั่น</h5>
                      <div class="form-group m-b-30">
                       <asp:HiddenField ID="hidPromotioncode" runat="server" />
                               
                          <asp:ListBox ID="lbPromotion" OnSelectedIndexChanged="lbPromotion_SelectedIndexChanged"  CssClass="form-control selectric" style="height:150px;font-size:18px;" AutoPostBack="true" runat="server" Width="100%" Height="120">
                        </asp:ListBox>
                      </div>
                      <div class="col-sm-4"  style=" float: right;">
                          <div class="input-group" style=" float: right;">
                          <input type="text" class="form-control m-r-10" placeholder="ค้นหารหัสสินค้า">
                          <input type="text" class="form-control m-r-10" placeholder="ค้นหาชื่อสินค้า">
                          <button class="input-group-addon btn-import button-active " id="basic-addon5"><i class="ti-search"></i>ค้นหา</button>

                       </div>
                      </div>
                      <div>
                      <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none" >สินค้า</h5>
                    
                               <asp:GridView ID="gvProductPopup" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap" OnRowDataBound="gvProductPopup_RowDataBound"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProductPopup_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkProductPopupAll" OnCheckedChanged="chkProductPopupAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkProductPopup" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รหัสสินค้า</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      &nbsp;&nbsp;<asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ชื่อสินค้า</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ราคา</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                         <%#GetTextPriceProductPopup(DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"))%>
                                   
                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">จำนวน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:TextBox ID="txtAmount"  AutoPostBack="True" OnTextChanged="txtAmountProductPopup_TextChanged"  TextMode="Number" Style="text-align:right" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                        <asp:HiddenField ID="hidAmount"  Value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">หน่วย</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblUnit" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รวม</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lbltotal" Text='' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >  
                                        <asp:HiddenField ID="hidSumprice" runat="server" value='<%#GetPrice(DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"))%>' />
                                          <asp:HiddenField ID="hidRunning" runat="server"  />
                                          <asp:HiddenField ID="hidPrice" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                              
                                           <asp:HiddenField ID="hidDiscountAmount" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                           <asp:HiddenField ID="hidDiscountPercent" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                           <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                             <asp:HiddenField ID="hidLockAmountFlag" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                              <asp:HiddenField ID="hidDefaultAmount" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                             <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailId")%>' />
                                              <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                              <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidUnitName" Value='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' />
                                                <asp:HiddenField runat="server" ID="hidUnitCode" Value='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' />
                                                                                                                                                                                                                                                                                                  
                       
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
                       
                           <div class="text-center m-t-10 m-b-10">

                              <asp:Button runat="server" CssClass="button-active button-submit m-r-10" Text="Add  To  Cart" ID="btnProductPopupSubmit" OnClick="btnProductPopupSubmit_Click">   </asp:Button>
                             <asp:Button runat="server" CssClass="button-active button-submit m-r-10" Text="Close" ID="btnProductPopupClose" OnClick="btnProductPopupClose_Click">   </asp:Button>
                              </div>
                        
                    </ContentTemplate>
                </asp:UpdatePanel> 
                    </div>
                  </div>

                </div>
              </div>

            </div>
            
      <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-customer">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
                  <div class="modal-content">
                         <div class="modal-header">
                   
                    
                            <h5 class="modal-title sub-title " style="font-size: 18px;">Edit Customer
                        
                       
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                              <span aria-hidden="true">&times;</span>
                            </button> 
                          </div>
                        <div class="modal-body">
                          <div class="row">
                            <div class="col-sm-12">
                              <div class="card-block">

                                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                  <ContentTemplate>
               
                                <div class="form-group row">
      
                                  <label class="col-sm-2 col-form-label">First Name</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtCustomerFName_Edit" runat="server" class="form-control"></asp:TextBox> 
                                      <asp:Label ID="lblCustomerFName_Edit" runat="server" CssClass="validation"></asp:Label>
                                       <asp:HiddenField ID="hidCustomerFName_Edit" runat="server" ></asp:HiddenField>
                               
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">Last Name</label>
                                  <div class="col-sm-3">

                                      <asp:TextBox ID="txtCustomerLName_Edit" runat="server" class="form-control"></asp:TextBox> 
                                        <asp:Label ID="lblCustomerLName_Ins" runat="server" CssClass="validation"></asp:Label>
                                
                                  </div>
                                </div>
                                   <%--<div class="form-group row">
                                  <label class="col-sm-2 col-form-label">Contact Tel</label>
                                  <div class="col-sm-3">
                                       <asp:TextBox ID="txtContactTel_Edit" runat="server" class="form-control"></asp:TextBox> 
                                        <asp:Label ID="lblContactTel_Edit" runat="server" CssClass="validation"></asp:Label>
                                  </div>

                                  </div>--%>  
                                  </ContentTemplate>
                                  </asp:UpdatePanel>

                        <div class="text-center m-t-20 center">
                          
                                      <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_EditCustomer"
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />
                                     <asp:Button ID="btnCancel" Text="Cancel" 
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />

                                </div>

                                  

                              </div>
                            </div>
                          </div>
                        </div>
        </div>
             </div>
        </div>    
          
    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-address">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                      <div class="modal-content">
                         <div class="modal-header">
                   
                    
                            <h5 class="modal-title sub-title " style="font-size: 18px;">Select and address  
                        
                       
                            <button type="button" id="modaladdressdeliveryclose" class="close" data-dismiss="modal" aria-label="Close">
                              <span aria-hidden="true">&times;</span>
                            </button> 
                          </div>
                        <div class="modal-body">
                          <div class="row">
                            <div class="col-sm-12">
                              <div class="card-block">

                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
               
                                   <div>
                      <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none" >ที่อยู่จัดส่ง</h5>
                    
                               <asp:GridView ID="gvPopupCustomerAddress" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap" OnRowDataBound="gvPopupCustomerAddress_RowDataBound"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvPopupCustomerAddress_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <%--<asp:CheckBox ID="chkPopupCustomerAddressAll" OnCheckedChanged="chkPopupCustomerAddressAll_Change" AutoPostBack="true" runat="server"  ></asp:CheckBox>--%>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:RadioButton ID="radCustomerAddress" GroupName="CustomerAddress" onclick="checkRadioBtn(this);" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      &nbsp;&nbsp;<asp:Label ID="lblAddress" Text='<%# DataBinder.Eval(Container.DataItem, "Address")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Subdistrict</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblSubdistrict" Text='<%# DataBinder.Eval(Container.DataItem, "SubDistrictName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">District</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                           &nbsp;&nbsp;<asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Province</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                         &nbsp;&nbsp;<asp:Label ID="lblProvince" Text='<%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Postal Code</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblZipCode" Text='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                               
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >  
                              
                                        <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowCustomerAddressDelivery"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidCustomerAddressId" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerAddressId")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerCode" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidAddress" Value='<%# DataBinder.Eval(Container.DataItem, "Address")%>' />
                                                <asp:HiddenField runat="server" ID="hidSubdistrict" Value='<%# DataBinder.Eval(Container.DataItem, "Subdistrict")%>' />                                                                                                                                                                                                                                                  
                                                <asp:HiddenField runat="server" ID="hidDistrict" Value='<%# DataBinder.Eval(Container.DataItem, "District")%>' />  
                                                <asp:HiddenField runat="server" ID="hidProvince" Value='<%# DataBinder.Eval(Container.DataItem, "Province")%>' /> 
                                                <asp:HiddenField runat="server" ID="hidZipCode" Value='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>' />
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

                               
                                  <div class="card-header-left" style="top:2px">
                                  <asp:LinkButton id="LinkButtonCreateCustomerAddressDelivery" class="btn-edit button-active btn-small"
                                               OnClick="LinkButtonCreateCustomerAddressDelivery_Click" runat ="server"><i class="fa fa-edit"></i>เพิ่ม</asp:LinkButton>
                                      </div>
                            
                            <div id="InsertSectionDelivery" runat="server">
            <div class="card-block m-t-20" style="border: 1px solid">
                <div class="form-group row" >
                 <div class="card-header" style="border:none;"><h5 class="f-14 sub-title" ><asp:Label ID="lblAddressText" runat="server" ></asp:Label></h5></div>
                  <label class="col-sm-2 col-form-label">Address</label>
                  <div class="col-sm-9">
                     <asp:TextBox id="txtAddress_Ins" class="form-control" runat="server"></asp:TextBox>
                     <asp:Label ID="lblAddress" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-2 col-form-label">Province</label>
                  <div class="col-sm-3">
                     <asp:DropDownList ID="ddlProvince" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                     <asp:Label ID="lblProvince_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-1 col-form-label"></label>
                  <label class="col-sm-2 col-form-label">District</label>
                  <div class="col-sm-3">
                      <asp:DropDownList ID="ddlDistrict" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                      <asp:Label ID="lblDistrict_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-2 col-form-label">Subdistrict</label>
                  <div class="col-sm-3">
                    <asp:DropDownList ID="ddlSubDistrict" runat="server" cssclass="form-control"></asp:DropDownList>
                    <asp:Label ID="lblSubDistrict_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-1 col-form-label"></label>
                  <label class="col-sm-2 col-form-label">Post Code</label>
                  <div class="col-sm-3">
                    <asp:TextBox id="txtPostcode_Ins" class="form-control" runat="server"></asp:TextBox>
                      <asp:Label ID="lblPostCode_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <%--<label class="col-sm-5 col-form-label"></label>
                  <div class="text-center m-t-20">
                     <asp:Button ID="Button1" Text="Submit" OnClick="btnSubmit_Click"
                     class="btn-mk-pri btn-submit m-r-10"
                    runat="server" />
                    <asp:Button ID="Button2" Text="Cancel" 
                    class="btn-mk-pri btn-cancel"
                    runat="server" />
                  </div>        --%>            
                </div>
                </div>
            </div>
<div class="text-center m-t-20 center">
                          
                                      <asp:Button ID="btnSubmit_EditCustomerAddress" Text="Submit" OnClick="btnSubmit_EditCustomerAddress_Click"
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />
                                     <asp:Button ID="btnCancel_EditCustomerAddress" Text="Cancel" OnClick="btnCancel_Click"
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />

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

        <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-address2">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                      <div class="modal-content">
                         <div class="modal-header">
                   
                    
                            <h5 class="modal-title sub-title " style="font-size: 18px;">Select and address  
                        
                       
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                              <span aria-hidden="true">&times;</span>
                            </button> 
                          </div>
                        <div class="modal-body">
                          <div class="row">
                            <div class="col-sm-12">
                              <div class="card-block">

                                  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
               
                                   <div>
                      <h5 class="modal-title sub-title m-b-5" style="font-size: 18px; border-bottom: none" >ที่อยู่ใบเสร็จรับสินค้า</h5>
                    
                               <asp:GridView ID="gvCustomerAddressReceipt" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap" OnRowCommand="gvCustomerAddressReceipt_RowCommand"
                            TabIndex="0" Width="100%" CellSpacing="0" 
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <%--<asp:CheckBox ID="chkPopupCustomerAddressAll" OnCheckedChanged="chkPopupCustomerAddressAll_Change" AutoPostBack="true" runat="server"  ></asp:CheckBox>--%>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        
                                        <asp:RadioButton ID="radCustomerAddressReceipt" GroupName="CustomerAddressReceipt" onclick="checkRadioBtn1(this);" runat="server" />


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      &nbsp;&nbsp;<asp:Label ID="lblAddressReceipt" Text='<%# DataBinder.Eval(Container.DataItem, "Address")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Subdistrict</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblSubdistrictReceipt" Text='<%# DataBinder.Eval(Container.DataItem, "SubDistrictName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">District</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                           &nbsp;&nbsp;<asp:Label ID="lblDistrictReceipt" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Province</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                         &nbsp;&nbsp;<asp:Label ID="lblProvinceReceipt" Text='<%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Postal Code</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblZipCodeReceipt" Text='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                               
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >  
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowCustomerAddressReceipt"
                                                class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidCustomerAddressReceiptId" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerAddressId")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerReceiptCode" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidAddressReceipt" Value='<%# DataBinder.Eval(Container.DataItem, "Address")%>' />
                                                <asp:HiddenField runat="server" ID="hidSubdistrictReceipt" Value='<%# DataBinder.Eval(Container.DataItem, "Subdistrict")%>' />                                                                                                                                                                                                                                                  
                                                <asp:HiddenField runat="server" ID="hidDistrictReceipt" Value='<%# DataBinder.Eval(Container.DataItem, "District")%>' />  
                                                <asp:HiddenField runat="server" ID="hidProvinceReceipt" Value='<%# DataBinder.Eval(Container.DataItem, "Province")%>' /> 
                                                <asp:HiddenField runat="server" ID="hidZipCodeReceipt" Value='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>' />
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

                            

                                <div class="card-header-left" style="top:2px">
                                  <asp:LinkButton id="LinkBtnCreateCustomerAddressReceipt" class="btn-edit button-active btn-small"
                                               OnClick="LinkBtnCreateCustomerAddressReceipt_Click" runat ="server"><i class="fa fa-edit"></i>เพิ่ม</asp:LinkButton>
                                      </div> 
                                  
                                  <div id="InsertCustomerAddressReceiptSection" runat="server">
            <div class="card-block m-t-20" style="border: 1px solid">
                <div class="form-group row" >
                 <div class="card-header" style="border:none;"><h5 class="f-14 sub-title" ><asp:Label ID="Label1" runat="server" ></asp:Label></h5></div>
                  <label class="col-sm-2 col-form-label">Address</label>
                  <div class="col-sm-9">
                     <asp:TextBox id="txtCusAddressReceiptIns" class="form-control" runat="server"></asp:TextBox>
                     <asp:Label ID="lblCusAddressReceiptIns" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-2 col-form-label">Province</label>
                  <div class="col-sm-3">
                     <asp:DropDownList ID="ddlCusProvinceReceiptIns" OnSelectedIndexChanged="ddlCusProvinceReceiptIns_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                     <asp:Label ID="lblCusProvinceReceiptIns" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-1 col-form-label"></label>
                  <label class="col-sm-2 col-form-label">District</label>
                  <div class="col-sm-3">
                      <asp:DropDownList ID="ddlCusDistrictReceiptIns" OnSelectedIndexChanged="ddlCusDistrictReceiptIns_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                      <asp:Label ID="lblCusDistrictReceiptIns" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-2 col-form-label">Subdistrict</label>
                  <div class="col-sm-3">
                    <asp:DropDownList ID="ddlCusSubdistrictReceiptIns" runat="server" cssclass="form-control"></asp:DropDownList>
                    <asp:Label ID="lblCusSubdistrictReceiptIns" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-1 col-form-label"></label>
                  <label class="col-sm-2 col-form-label">Post Code</label>
                  <div class="col-sm-3">
                    <asp:TextBox id="txtCusPostCodeReceiptIns" class="form-control" runat="server"></asp:TextBox>
                      <asp:Label ID="lblCusPostCodeReceiptIns" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <%--<label class="col-sm-5 col-form-label"></label>
                  <div class="text-center m-t-20">
                     <asp:Button ID="btnSubmitCusAddressReceipt" Text="Submit" OnClick="btnSubmitCusAddressReceipt_Click"
                     class="btn-mk-pri btn-submit m-r-10"
                    runat="server" />
                    <asp:Button ID="btnCancelCusAddressReceipt" Text="Cancel" 
                    class="btn-mk-pri btn-cancel"
                    runat="server" />
                  </div>--%>
                </div>
                </div>
            </div>

                            <div class="text-center m-t-20 center">
                          
                                      <asp:Button ID="btnSubmit_EditCustomerAddressReceipt" Text="Submit" OnClick="btnSubmit_EditCustomerAddressReceipt_Click"
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />
                                     <asp:Button ID="btnCancel_EditCustomerAddressReceipt_Click" Text="Cancel" OnClick="btnCancelCusAddressReceipt_Click"
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />

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
      
</asp:Content>

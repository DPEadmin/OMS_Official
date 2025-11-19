<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="TakeOrder.aspx.cs" Inherits="DOMS_TSR.src.TakeOrderTSR.TakeOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    
<!-- General JS Scripts -->
<script src="../../assets/js/jquery.filer/js/jquery.filer.min.js"></script>
<script src="../../assets/js/filer/custom-filer.js"></script>
<script src="../../assets/js/filer/jquery.fileuploads.init.js"></script>

<!-- upload file -->


<script src="../../assets/js/scripts.js"></script>
<script src="../../assets/js/custom.js"></script>
<script src="../../assets/js/jquery-3.3.1.min.js"></script> 
<script src="../../assets/js/popper.min.js"></script>
<script src="../../assets/js/bootstrap.min.js" ></script>
<script src="../../assets/js/jquery.nicescroll.min.js"></script>
<script src="../../assets/js/moment.min.js"></script>
<script src="../../assets/js/stisla.js"></script>

<!-- upload file -->

    <link rel="stylesheet" href="../../assets/js/bootstrap.min.css">
<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
  
<link rel="stylesheet" type="text/css" href="../../assets/icon/icofont/css/flaticon.css">

<link rel="stylesheet" type="text/css" href="../../assets/icon/font-awesome/css/font-awesome.css">
<link rel="stylesheet" type="text/css" href="../../assets/icon/font-awesome/css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="../../assets/icon/ico-fonts/css/icofont.css">
<link rel="stylesheet" type="text/css" href="../../assets/icon/icofont/css/icofont.css">
<link rel="stylesheet" type="text/css" href="../../assets/icon/ion-icon/css/ionicons.min.css">
<link rel="stylesheet" type="text/css" href="../../assets/icon/material-design/css/material-design-iconic-font.min.css">
<link rel="stylesheet" type="text/css" href="../../assets/icon/themify-icons/themify-icons.css">
<link rel="stylesheet" type="text/css" href="../../assets/js/jquery.filer/css/jquery.filer.css">
<link rel="stylesheet" type="text/css" href="../../assets/js/jquery.filer/css/themes/jquery.filer-dragdropbox-theme.css">

<link rel="stylesheet" href="../../assets/css/style.css">
<!-- <link rel="stylesheet" href="/assets/css/ssre.css"> -->




<link rel="stylesheet" href="../../assets/css/components.css">
<link rel="stylesheet" type="text/css" href="../../assets/icon/icofont/css/flaticon.css">
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
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkOrderAll" OnCheckedChanged="chkOrderAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkOrder" runat="server" />

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
                                        &nbsp;&nbsp;<asp:Label ID="lblPrice" Text='<%# String.Format("{0:#,###0.00}",DataBinder.Eval(Container.DataItem, "Price"))%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">จำนวน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:TextBox ID="txtAmount" Width="100" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">หน่วย</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblUnit" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รวม</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblSumPrice" Text='' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >
                                           <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                        
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
                                              <div class="card-header-right" style="top:2px">   <button type="button" class="btn-edit button-active btn-small"><i class="fa fa-edit"></i>แก้ไข</button></div>

                                          

                                      </div>
                                      <div class="card-block">
                                          <ul>
                                              <li>
                                                  1200 ถ.บางนา-ตราด เขตบางนา แขวง บางนา กรุงเทพมหานคร 10260
                                              </li>
                                          </ul>
                                      </div>
                                  </div>
                              </div>
                              <div class="col-md-6 col-pad-l">
                                  <div class="card">
                                      <div class="card-header">

                                          <div class="sub-title " >ที่อยู่ออกใบกำกับภาษี </div>
                                          <div class="card-header-right" style="top:2px">  <button type="button" class="btn-edit button-active btn-small"><i class="fa fa-edit"></i>แก้ไข</button></div>
                                         
                                      </div>
                                      <div class="card-block">
                                          <ul>

                                              <li>
                                                 
                                                      <label>
                                                          <input type="checkbox" value="">
                                                          
                                                          <span class="text-inverse">ที่อยู่เดียวกันกับที่อยู่จัดส่ง </span>
                                                      </label>
                                                 
                                              </li>
                                              <li>
                                                  1200 ถ.บางนา-ตราด เขตบางนา แขวง บางนา กรุงเทพมหานคร 10260
                                              </li>


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
                                            <button type="button" class="btn-edit button-active btn-small"><i class="fa fa-edit"></i>แก้ไข</button>
                                              </div>
                                  </div>
                              </div>
                              <div class="card-block">
                                  <table class="ssre " style="width:100%">
                                      
                                      <tbody>
                                          <tr>
                                             <th style="font-size:16px ;" colspan="2">สุกัญญา โกแสนตอ</th>
                                          <tr>
                                              <th style="text-align:left ; ">รหัสลูกค้า <span style="">:</span></th>
                                              <td style="text-align:right ; "> CM20193001</td>
                                          </tr>
                                          <tr>
                                              <th style="text-align:left ; ">เบอร์ติดต่อ <span style="">:</span></th>
                                              <td style="text-align:right ;"> 09 2649 4115</td>
                                          </tr>
                                          <tr>
                                              <th style="text-align:left ; ">เบอร์ติดต่อ <span style="">:</span></th>
                                              <td style="text-align:right ; "> 09 2649 4115</td>
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
                                                  <span class="text-inverse m-l-10">เครื่องกรองน้ำเซฟ  Alkaline Plus </span>
                                              </label>
                                        
                                      </li>
                                      <li>
                                          
                                              <label>
                                                 
                                                  <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10"> เครื่องกรองน้ำ UV Plus </span>
                                              </label>
                                          
                                      </li>
                                      <li>
                                         
                                              <label>
                                                  
                                                      <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10"> เครื่องทำน้ำอุ่น Q-Series WH 4.5</span>
                                              </label>
                                          
                                      </li>
                                      <li>
                                         
                                              <label>
                                              
                                                      <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10"> เครื่องกรองน้ำเซฟ  Super Alkaline</span>
                                              </label>
                                         
                                      </li>
                                      <li>
                                         
                                              <label>
                                                
                                                      <span ><i class="fa fa-plus"></i></span>
                                                  <span class="text-inverse m-l-10"> เครื่องทำน้ำอุ่น P-Series WH 3.8</span>
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
                                 
                                  <button type="button" class="btn btn-primary btn-md btn-block waves-effect text-center " style="background-color:#07C9C9; border-radius: 5px">สรุปจำนวนเงิน</button>
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
                   
                    
                     
                    <h5 class="modal-title sub-title " style="font-size: 18px;">เพิ่มสินค้า   <button type="button" class="btn-add button-active  btn-small m-b-5" data-toggle="modal" data-target="#www" >
                        <i class="fa fa-2x fa-shopping-cart"></i>ตะกร้าสินค้า</button></h5>
                        <div style="width: 80px; height: 20px; background-color: red;" 
        onmouseover="document.getElementById('div1').style.display = 'block';">
   <div id="div1" style="display: none;">Text</div>
</div>
                 

                 
                     
             
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
                                        &nbsp;&nbsp;<asp:Label ID="lblPrice" Text='<%# String.Format("{0:#,###0.00}",DataBinder.Eval(Container.DataItem, "Price"))%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">จำนวน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:TextBox ID="txtAmount" Width="100" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">หน่วย</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblUnit" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รวม</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblSumPrice" Text='' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >
                                           <asp:HiddenField ID="hidDiscountAmount" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount")%>' />
                                           <asp:HiddenField ID="hidDiscountPercent" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>' />
                                           <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                             <asp:HiddenField ID="hidLockAmountFlag" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "LockAmountFlag")%>' />
                                            <asp:HiddenField ID="hidAmount" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' />
                                              <asp:HiddenField ID="hidDefaultAmount" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "DefaultAmount")%>' />
                                             <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailId")%>' />
                                              <asp:HiddenField runat="server" ID="hidCampaignCode1" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                              <asp:HiddenField runat="server" ID="hidPromotionCode1" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                                                                                                                                                                                                                                                                                    
                       
                                    </ItemTemplate>

                                </asp:TemplateField>

                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>


                                  <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                            <tr height="30" bgcolor="#ffffff">
                                <td width="100%" align="right" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                        <tr>
                                            <td style="font-size: 8.5pt;">
                                         
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnFirstProductPopup" CssClass="Button" ToolTip="First" CommandName="First"
                                                    Text="<<" runat="server" OnCommand="GetPageIndexProductPopup"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnPreProductPopup" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                    Text="<" runat="server" OnCommand="GetPageIndexProductPopup"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPageProductPopup" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPageProductPopup_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblTotalPagesProductPopup" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnNextProductPopup" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndexProductPopup"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnLastProductPopup" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                    Text=">>" OnCommand="GetPageIndexProductPopup"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                         </table>

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
<<<<<<< .mine
            </div>
            <div class="modal fade" id="www" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="false">
=======
            </div> 
            <div class="modal fade" id="www" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="false">
>>>>>>> .r113
                <div class="modal-dialog" role="document" style="max-width:1000px">
                  <div class="modal-content">
                    <div class="modal-header">
                      <h5 class="modal-title sub-title " style="font-size: 18px;">ตะกร้าสินค้า </h5>
                      <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                    <div class="modal-body">
              <div class="card-block">
               
                     <asp:UpdatePanel ID="upModal2" runat="server">
                    <ContentTemplate>
                      <table class="table-p table-bordered table-striped " style="width:100%" >
                         <thead>
                           <tr>
                             <th style="text-align:center">รหัสสินค้า</th>
                             <th style="text-align:center"> ชื่อสินค้า</th>
                             <th style="text-align:center">ราคา</th>
                            
                           
                             <th style="text-align:center">จำนวน </th>
                             <th style="width: 1%;">หน่วย</th>
                             <th style="text-align:center">ร้านค้า </th>
                             <th style="text-align:center">รวม</th>
   
                             <th></th>
                           </tr>
                         </thead>
                         <tbody>
   
                             <tr>
     
                               <td style="text-align: center;">PD17340000836</td>
                               <td>เครื่องกรองน้ำเซฟ รุ่น Alkaline Plus</td>
                               <td  style="text-align: right;"><p style="margin: 0px; "><span style="text-decoration: line-through; text-decoration-color: red; color:red ">28,900.00</span><span class="m-l-5">20000.00 </span> </p></td>
                           <style>
   input[type=number]::-webkit-inner-spin-button {
   opacity: 1

}
                           </style>
                               
     
                               <td style="text-align: right;     width: 1%;"><input type="number"value="1"min="1"max="99999"></td>
                               <td style="text-align: right;">เครื่อง</td>
                               <td style="text-align: right;">เครื่อง</td>
     
                               <td style="text-align: right;">20000.00</td>
                               <td>
     
                                 <a href=""><i class="ti-trash"></i></a>
                               </td>
                             </tr>
                             <tr>
   
                                 <td style="text-align: center;">PD17340002683</td>
                                 <td>เครื่องกรองน้ำ รุ่น UV Plus</td>
                                 <td style="text-align: right;">14,500.00</td>
                                
       
                                 <td style="text-align: right;     width: 1%;"><input type="number"value="1"min="1"max="99999"></td>
                                 <td style="text-align: right;">เครื่อง</td>
                                 <td style="text-align: right;">สุรเชต เครื่องจักร</td>
       
                                 <td style="text-align: right;">14,500.00</td>
                                 <td>
       
                                   <a href=""><i class="ti-trash"></i></a>
                                 </td>
                        
                         
                             </tbody>
                       
                       </table>   
                       </ContentTemplate>
                </asp:UpdatePanel> 
                
              </div>
                    </div>
                  </div>
                </div>
              </div>
    
</asp:Content>

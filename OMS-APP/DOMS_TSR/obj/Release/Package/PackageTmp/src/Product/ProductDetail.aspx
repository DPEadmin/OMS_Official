
<%@ Page Language="C#"  MasterPageFile="~/src/MasterPage/Web.master"  AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="DOMS_TSR.src.Product.ProductDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
        <input type="hidden" id="hidFlagInsert" runat="server" />
        <asp:HiddenField ID="hidFlagDel" runat="server" />
        <input type="hidden" id="hidaction" runat="server" />
        <asp:HiddenField ID="hidMsgDel" runat="server" />
        <asp:HiddenField ID="hidEmpCode" runat="server" />

  <!-- Page body start -->
          <div class="page-body">
            <div class="row">
              <div class="col-sm-12">
                <div class="card">
                  <div class="card-header">
                    <div class="sub-title ">รายละเอียดสินค้า
                    </div>
                 </div>
                  <div class="card-block">
                      
                          <div class="col-sm-12">
                              <div class="row">
                                                  <div class="col-lg-12 col-xl-6 col-sm-12">
                                                      <table class="table-detail m-0" style="width:100%">
                                                          <tbody>
                                                                <tr>
                                                                        <th scope="row">ชื่อแบรนด์</th>
                                                                        <td><asp:Label runat="server" ID="txtProductBrand"></asp:Label></td>
                                                                    </tr>
                                                              <tr>
                                                                  <th scope="row">รหัสสินค้า</th>
                                                                  <td><asp:Label runat="server" ID="txtProductCode"></asp:Label></td>
                                                              </tr>

                                                               <tr>
                                                                  <th scope="row">รหัสอ้างอิง</th>
                                                                  <td><asp:Label runat="server" ID="txtProductSku"></asp:Label></td>
                                                              </tr>
                                                           
                                                              <tr>
                                                                  <th scope="row">ชื่อสินค้า</th>
                                                                  <td><asp:Label runat="server" ID="txtProductName"></asp:Label></td>
                                                              </tr>
                                                              <!-- <tr>
                                                                  <th scope="row">ชื่อร้านค้า</th>
                                                                  <td><asp:Label runat="server" ID="txtMerchantName"></asp:Label></td>
                                                              </tr> -->
                                                              <tr>
                                                                  <th scope="row">ราคาสินค้า(บาท)</th>
                                                                  <td><asp:Label runat="server" ID="txtPrice"></asp:Label></td>
                
                                                              </tr>
                                                              <tr>
                                                                    <th scope="row"
                                                                    ">หน่วย</th>
                                                                    <td "><asp:Label runat="server" ID="txtUnit"></asp:Label></td>
                                                                </tr>
                                                          <tr>
                                                              <th scope="row">ส่วนประกอบ
                                                              </th>
                                                              <td><asp:Label runat="server" ID="txtProductRecipes"></asp:Label></td>
                                                                <!-- <td><asp:Label runat="server" ID="txtProductCategory"></asp:Label></td> -->
                                                          </tr>
                                                          
                                                        <tr>
                                                                <th scope="row">รายละเอียด
                                                                    </th>
                                                                
                                                                <td style="border-top: 0px; ">
                                                                    <textarea rows="4" runat="server" id="txtDescription"
                                                                        cols="100"
                                                                        class="form-control"
                                                                        disabled
                                                                        style="resize:none; overflow: hidden; " ></textarea>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <th scope="row">อัพเซล สคริปต์
                                                                    </th>
                                                                
                                                                <td style="border-top: 0px; ">
                                                                    <textarea rows="4" runat="server" id="txtUpsellScript"
                                                                        cols="100"
                                                                        class="form-control"
                                                                        disabled
                                                                        style="resize:none; overflow: hidden; " ></textarea>
                                                                </td>
                                                            </tr>
                                                           
                                                          </tbody>
                                                      </table>
                                                  </div>
                                                   <div class="col-lg-12 col-xl-6 col-sm-12  " id="big_banner port_big_img " >
                                             
                                                         <img class="img thumb-post" src="1" runat="server" id="ProductImg"  style="height: 100%; width: 100%; object-fit: contain" alt="">
                                                    </div> 
                                                          <!--   <table class="table-detail m-0" style="width:100%">
                                                          <tbody>
                                                             
                                                               <tr>
                                                                  <th scope="row">ขนาดของสินค้า(ซม)</th>
                                                                  <td><asp:Label runat="server" ID="txtProductWidth"></asp:Label> x 
                                                                      <asp:Label runat="server" ID="txtProductLength"></asp:Label> x
                                                                      <asp:Label runat="server" ID="txtProductHeight"></asp:Label>
                                                                  </td>
                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">ขนาดบรรจุภัณฑ์(ซม)</th>
                                                                  <td><asp:Label runat="server" ID="txtPackageWidth"></asp:Label> x 
                                                                      <asp:Label runat="server" ID="txtPackageLength"></asp:Label> x
                                                                      <asp:Label runat="server" ID="txtPackageHeight"></asp:Label>
                                                                  </td>
                                                              </tr>
                                                               <tr>
                                                                  <th scope="row">น้ำหนัก
                                                                      (กก.)</th>
                                                                  <td><asp:Label runat="server" ID="txtProductWeight"></asp:Label></td>
                                                              </tr> 
                                                              <tr>
                                                                <th scope="row">ประเภทการขนส่ง  
                                                                    </th>
                                                                <td><asp:Label runat="server" ID="txtLogisticType"></asp:Label></td>
                                                                </tr> 
                                                          </tbody>
                                                      </table>-->
                                                      
                                                 
                               </div>
                             </div>
                                        </div>
                                      </div>
                                    </div>
                                  </div>
                                </div>

      

    

</asp:Content>

<%@ Page Title="" Language="C#"  MasterPageFile="MK.master"   AutoEventWireup="true" CodeBehind="TakeOrder.aspx.cs" Inherits="DOMS_TSR.src.TakeOrderMK.TakeOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/src/UserControl/SelectBranch.ascx" TagName="SelectBranch" TagPrefix="uc1" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
     <script type="text/javascript">
         function resetname(radioId, btnId) {

             var radio = document.getElementById(radioId);
             var btn = document.getElementById(btnId);

             radio.name = 'radBranch';

             var hidtab = document.getElementById("<%= hidtab.ClientID %>").value 


             if (hidtab == "1") {
                 document.getElementById("<%= hidSelectedBranchCode1.ClientID %>").value = radio.value;
             } else if (hidtab == "2") {
                 document.getElementById("<%= hidSelectedBranchCode2.ClientID %>").value = radio.value;
             } else if (hidtab == "3") {
                 document.getElementById("<%= hidSelectedBranchCode3.ClientID %>").value = radio.value;
             }

             btn.click();
            
         }

         function clicked(radioId) {          
             var radio = document.getElementById(radioId);

             radio.name = 'radBranch';

             document.getElementById(radioId).checked = true;
         }

         function fillText(pname) {
             alert("pname=" + pname);
             $("#textBox").val = "Your Text";
         }

         function viewProductdesc(pname) {
             //alert("pname=" + pname);
             document.getElementById("<%= lblProductDescriptionfromPromotiondetail.ClientID %>").innerHTML  = pname;
             
             //document.getElementById(ClientId).value = RowID;
         }

    </script>

      
            <style>
                .badgez {
                  position: absolute;
                  right: 0;
                  top: 0;
                  z-index: 1;
                  vertical-align: middle;

font-weight: 600;
letter-spacing: .3px;
border-radius: 30px;
font-size: 10px;
text-align:center;
color:white;
                }
                .dffg{
                  float: right;
margin-left: .3125rem;
                }
                .badge--promotion{
                  background-color: red;
                }
                .badge-detail{
                  display: flex;
-webkit-box-orient: vertical;
-webkit-box-direction: reverse;
-webkit-flex-direction: column-reverse;
-moz-box-orient: vertical;
-moz-box-direction: reverse;
-ms-flex-direction: column-reverse;
flex-direction: column-reverse;
padding: .1rem;
                }
                .button-additem{
                  float: right;
border: none;
border-radius: 5px;
font-size: 0.85rem;
color: white;
background: #990305;
background: -moz-linear-gradient(top, #990305 0%, #ec1c24 100%);
background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#990305), color-stop(100%,#ec1c24));
background: -webkit-linear-gradient(top, #990305 0%,#ec1c24 100%);
background: -o-linear-gradient(top, #990305 0%,#ec1c24 100%);
background: -ms-linear-gradient(top, #990305 0%,#ec1c24 100%);
background: linear-gradient(to bottom, #990305 0%,#ec1c24 100%);
                }
              </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

          <uc1:SelectBranch ID="SelectBranch" OnBtnClick="UsrCtrl_SelectBranch_Click" runat="server" /> 

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        
                      <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
             <asp:HiddenField ID="hidEmpCode" runat="server" />
        <asp:HiddenField ID="hidpromotioncodeselect" runat="server" />
        <asp:HiddenField ID="hidcampaigncodeselect" runat="server" />
               <asp:HiddenField ID="hidcampaigncategorycode" runat="server" />
            <asp:HiddenField ID="hidcampaigncategoryname" runat="server" />
            <asp:HiddenField ID="hidLandmarkLat1" runat="server" />
            <asp:HiddenField ID="hidLandmarkLng1" runat="server" />
            <asp:HiddenField ID="hidLandmarkLat2" runat="server" />
            <asp:HiddenField ID="hidLandmarkLng2" runat="server" />
            <asp:HiddenField ID="hidLandmarkLat3" runat="server" />
            <asp:HiddenField ID="hidLandmarkLng3" runat="server" />


            <style>
                .card-block {
                  padding: 0.25rem;
                }
                body{
                 overflow-y:hidden; 
                 
                     overflow-x:hidden;
                     height: 100vh;
                }
              
                  .main {
                    font-family: Arial;
                    width: 88%;
                    display: block;
                    margin: 0 auto;
              
                  }
              
                  h3 {
                    background: #fff;
                    color: #3498db;
                    font-size: 36px;
                    line-height: 50px;
                    margin: 10px;
                    padding: 2%;
                    position: relative;
                    text-align: center;
                  }
              
                  .action {
                    display: block;
                    margin: 100px auto;
                    width: 100%;
                    text-align: center;
                  }
              
                  .action a {
                    display: inline-block;
                    padding: 5px 10px;
                    background: #f30;
                    color: #fff;
                    text-decoration: none;
                  }
              
                  .action a:hover {
                    background: #000;
                  }
                  table {
                                    border-collapse: collapse;
                                    table-layout: fixed;
                                  }
              
                                  table td {
                                    word-wrap: break-word;
                                  }
              
                                  input[type=number]::-webkit-inner-spin-button {
                                    opacity: 1
                                  }
                                }
                                ::-webkit-scrollbar {
                width: 0px;
              }
              
              /* Track */
              ::-webkit-scrollbar-track {
                box-shadow: inset 0 0 5px rgb(255, 255, 255); 
                border-radius: 10px;
              }
               
              /* Handle */
              ::-webkit-scrollbar-thumb {
                background: gray; 
                border-radius: 10px;
              }
              
              /* Handle on hover */
              ::-webkit-scrollbar-thumb:hover {
                background: #b30000; 
              } 
                </style> 
            <div class="main-content">
              <div class="page-wrapper" style="    padding: .2rem;">
                <div class="page-body">
                  <div class="row">
                    <div class="col-sm-4  p-r-1 " >
                      <div class="card" style="height:100vh ;">
                   
                        <div class="card-block p-t-0 p-b-0 ">
                          <div class="card-block p-t-0 p-b-0">
                        <div class="w3-bar w3-black p-t-10">
                          <asp:Button ID="btntab1" runat="server" OnClick="btntab1_Click" Text="Order 1" CssClass="w3-bar-item w3-button" />
                          <asp:Button ID="btntab2" runat="server" OnClick="btntab2_Click" Text="Order 2" CssClass="w3-bar-item w3-button" />
                          <asp:Button ID="btntab3" runat="server" OnClick="btntab3_Click" Text="Order 3" CssClass="w3-bar-item w3-button" />
                        
                          <asp:HiddenField ID="hidtab" runat="server" />
                          <asp:HiddenField ID="hidtab1CampaignCategory" runat="server" />
                          <asp:HiddenField ID="hidtab2CampaignCategory" runat="server" />
                          <asp:HiddenField ID="hidtab3CampaignCategory" runat="server" />
                          <asp:HiddenField ID="hidtab1CampaignCategoryname" runat="server" />
                          <asp:HiddenField ID="hidtab2CampaignCategoryname" runat="server" />
                          <asp:HiddenField ID="hidtab3CampaignCategoryname" runat="server" />
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
                          
                        </div>
                      </div>
                      </div>
                      <div class="card-block p-t-0">
                        <div class="card-block city p-t-0 p-b-0" id="London" > 
                          <div class=" p-l-5 p-r-5 p-t-5 p-b-5" style="border: 1px solid #ddd;   border-width: thin; height:45vh; overflow-y: auto;">     
                            <p class="m-b-0 f-14">Brand&nbsp;:&nbsp; <span style="float: right">สถานะ &nbsp;: &nbsp;
                               <asp:Label ID="lblorderstatus"  runat="server"></asp:Label>
                                <asp:Label ID="lblordercode" ForeColor="Green" runat="server"></asp:Label></span>
                                <asp:Label ID="lblCampaignCategory" runat="server"></asp:Label></p>
                                
                              <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" cssclass="table-mk-list"  OnRowDataBound="gvOrder_RowDataBound"
                            TabIndex="0" style="width:100%; border:none;" CellSpacing="0" OnRowCommand="gvOrder_RowCommand" 
                            ShowHeaderWhenEmpty="true">

                              

                            <Columns>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate >

                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                                  
                                          <asp:CheckBox ID="chkOrder"  runat="server" />
                                      
                                    </ItemTemplate>

                                </asp:TemplateField>


                                 <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="35%" ItemStyle-Width="35%">

                                    <HeaderTemplate >

                                        <div align="center"></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                     <style>abbr[title], acronym[title] {
  text-decoration: none;
}</style>
                                       <font color="<%# DataBinder.Eval(Container.DataItem, "ColorCode")%>">
                                              <abbr  title="<%# DataBinder.Eval(Container.DataItem, "ProductName")%>">
                                                  <%# GetSubString(DataBinder.Eval(Container.DataItem, "ProductName")) %></abbr>
                                  </font>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="35%" ItemStyle-Width="35%" >

                                    <HeaderTemplate>

                                        <div align="right">จำนวน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>    <%#GetTextPrice(DataBinder.Eval(Container.DataItem, "ParentProductCode"),DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"),DataBinder.Eval(Container.DataItem, "ColorCode"))%>
                                   
                                        <asp:TextBox  style="width:40px" ID="txtAmount" AutoPostBack="True"  Text='<%# Eval("Amount") %>' OnTextChanged="txtAmount_TextChanged" runat="server" TextMode="Number"  ></asp:TextBox>
                                       
                                                                                 
                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                               <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="20%" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="right">ราคารวม</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <font color="<%# DataBinder.Eval(Container.DataItem, "ColorCode")%>">
                                                 <asp:Label ID="lbltotal" runat="server" />
                                      </font>
                                                             <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# Eval("PromotionDetailId") %>' />
                                                            
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
             
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center"></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                                 
                                            <asp:LinkButton ID="btnClose"  AutoPostBack="True"  OnClick="btnClose_Click"  runat="server"><i class="ti-close"></i></asp:LinkButton>
                  
                                                      
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
                                <!-- <div  class="p-t-40 p-b-10" style="float:right; font-size: 10px">
                                   
                                   <asp:TextBox ID="txtvoucher" runat="server" placeholder="Gift Voucher / e--Voucher"></asp:TextBox>
                                    <asp:Button ID="btnVoucher" Text="VALIDATE" runat="server" OnClick="btnVoucher_Click" CssClass="btn-add button-active btn-small"/>
                               
                                 
                                </div> -->
                                <div class="card-block" style="height:25vh">
                                    <table class="order-list " style="width:100%">
                                    <thead>
                                      <tr>
                                       
                                        <th style="widows: 50%;"></th>
                                        <th style="width:25%; text-align:right;" >จำนวน</th>
                                        <th style="width:25%; text-align:right;">ราคา(บาท)</th>
                                        
                                      </tr>
                                    </thead>
                                    <tr>
                                    <td style=" " >สินค้า</td>
                                    <td style="text-align: right;    "><asp:Label ID="lblTotalAmount" runat="server" Text="" ></asp:Label></td>
                                    <td style="text-align: right; "><asp:Label ID="lblTotalPrice" runat="server" Text="" ></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style=" " >ส่วนลด</td>
                                        <td style="text-align: right;    "><asp:Label ID="lblTotalDiscount_Amount" runat="server" Text="" ></asp:Label></td>
                                        <td style="text-align: right; "><asp:Label ID="lblTotalDiscount_Price" runat="server" Text="" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="overflow-wrap: unset" >Gift Voucher / e-Voucher</td>
                                            <td style="text-align: right;    "><asp:Label ID="lblTotalVoucher_Amount" runat="server" Text="" ></asp:Label></td>
                                            <td style="text-align: right; "><asp:Label ID="lblTotalVoucher_Price" runat="server" Text="" ></asp:Label></td>
                                            </tr>
                                                <tr>
                                                    <td style=" " >รวม (ก่อน VAT)</td>
                                                    <td style="text-align: right;    "></td>
                                                    <td style="text-align: right; "><asp:Label ID="lblBeforeTotal_Vat" runat="server" Text="" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style=" " >VAT 7%</td>
                                                        <td style="text-align: right;    "><asp:Label ID="lblVatAmount" runat="server" Text="" ></asp:Label></td>
                                                        <td style="text-align: right; "><asp:Label ID="lbl_VatPrice" runat="server" Text="" ></asp:Label></td>
                                                        </tr>
                                        <tr>
                                                <td style="" >ค่าจัดส่ง</td>
                                                <td style="text-align: right;    "><asp:Label ID="Label1" runat="server" Text="" ></asp:Label></td>
                                                <td style="text-align: right; "><asp:TextBox ID="txtTransportPrice" OnTextChanged="txtTransportPrice_TextChanged" AutoPostBack="true" runat="server" Text ="40" style="text-align:right ;width:40px;" ></asp:TextBox></td>
                                            
                                                </tr>
                                                        <tr>
                                                            <td style=" " >ราคาที่ต้องชำระ</td>
                                                            <td style="text-align: right;    "></td>
                                                            <td style="text-align: right; "><asp:Label ID="lblAfterTotal_Vat" runat="server" Text="" ></asp:Label></td>
                                                            </tr>
                                    </tbody>
                                    </table>
                                    </div>
                                    <div class="card-block" style="height:5vh">
                                        <div id="covercheckout" runat="server" class="text-center m-t-50  col-12" >
                                                     <button type="button" runat="server" id="btncheckout" onclick="displayPayment();" class="btn-mk-pri btn-checkout  m-r-5 btnClick bind">Checkout</button>
                                                    <asp:Button ID="btnNewOrder" Text="New Order" runat="server" OnClick="btnNewOrder_Click" CssClass="btn-mk-pri btn-neworder m-r-5"/>
                                                    <%--  <asp:Button ID="btnCalculateTotal" Text="Calculate" runat="server" OnClick="btnCalculateTotal_Click" CssClass="btn-add button-active btn-small"/>--%>
                                                    <%--<asp:Button ID="btncheckout" Text="Checkout" runat="server" CssClass="button-active button-submit m-r-10 btnClick bind"/>--%> 
                                                    <!-- <button type="button" class="btn-mk-pri btn-reset m-r-5">Reset</button> -->
                                                    <asp:Button ID="btnCloseTab" Text="Close Tab" runat="server" OnClick="btnCloseTab_Click" CssClass="btn-mk-pri btn-cancel m-r-5"/>
                                                    <asp:Button ID="btnClear" Text="Clear" runat="server" OnClick="btnClear_Click" CssClass="btn-mk-pri btn-clear"/>
                                  
                                                </div>
                                              </div>
                                
                              </div>
         
                            </div>
                      </div>
                  </div>
                  <div class="col-sm-5 p-r-1 p-l-1" >
                      <div id="1" class="card " style="height:100vh;">
                        <div  style="height:35vh; padding:0rem 1rem;" > 
                          <div class="scrollbar m-t-5 " id="style-3" style="height:10vh; overflow-y: hidden; overflow-x:visible">
                                  <asp:DataList ID="dtlCampaignCategory" OnItemCommand="dtlCampaignCategory_ItemCommand" RepeatDirection="Horizontal"
                                         RepeatColumns="10" runat="server"  >
                                                                    <HeaderTemplate>
                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                       <div>
                                                                               <asp:HiddenField ID="hidCampaignCategoryCode" runat="server" value='<%# Eval("CampaignCategoryCode") %>' />
                                                                               <asp:HiddenField ID="hidCampaignCategoryName" runat="server" value='<%# Eval("CampaignCategoryName") %>' />
                                                                              <asp:ImageButton runat="server" ID="btnShow" Width="60" Height="60" Cssclass="m-r-15" ImageUrl='<%# Eval("PictureCampaignUrl") %>' CommandName="ShowCampaign"      />
                                                                        </div>    
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        
                                                                    </FooterTemplate>
                                  </asp:DataList>
                          </div>
                          <div class="sub-title"></div>
                          <div class="scrollbar m-t-5  " id="style-3" style="height:10vh; overflow-y: hidden; overflow-x:visible">
                                <asp:HiddenField ID="hidflagcombo" runat="server" />
                                   <asp:DataList ID="dtlCampaign" OnItemCommand="dtlCampaign_ItemCommand" RepeatDirection="Horizontal"
                                         RepeatColumns="50" runat="server" CssClass="slider slider-nav" RepeatLayout = "Table">
                                                                    <HeaderTemplate>
                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                          <asp:HiddenField ID="hidFlagShowProductPromotion" runat="server" value='<%# Eval("FlagShowProductPromotion") %>' />
                                                                           <asp:HiddenField ID="hidFlagComboSet" runat="server" value='<%# Eval("FlagComboSet") %>' />
                                                                           
                                                                               <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                              <asp:ImageButton runat="server" ID="btnShow"  Width="60" Height="60" ImageUrl='<%# GetImage(DataBinder.Eval(Container.DataItem, "PictureCampaignUrl")) %>' CommandName="ShowPromotion"  />
                                                                      
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                    
                                                                    </FooterTemplate>
                                  </asp:DataList>
                          </div>
                          <div class="sub-title"></div>
                          <div class="scrollbar m-t-5 " id="style-3" style="height:10vh; overflow-y: hidden; overflow-x:visible">
                                        <asp:DataList ID="dtlPromotion" OnItemCommand="dtlPromotion_ItemCommand"  RepeatDirection="Horizontal"
                                         RepeatColumns="7" runat="server" >
                                                                    <HeaderTemplate>
                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                            <asp:HiddenField ID="hidLockCheckbox" runat="server" value='<%# Eval("LockCheckbox") %>' />
                                                                               <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                              <asp:ImageButton runat="server" ID="btnShow" CommandName="ShowProduct"  ImageUrl='<%# GetImage(DataBinder.Eval(Container.DataItem, "PicturePromotionUrl")) %>' />
                                                                      
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                    
                                                                    </FooterTemplate>
                                        </asp:DataList>
                              <asp:DataList ID="dtlPromotionType" OnItemCommand="dtlPromotionType_ItemCommand" RepeatDirection="Horizontal"
                                         RepeatColumns="7" runat="server" >
                                                                    <HeaderTemplate>
                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                            <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                        <asp:HiddenField ID="hidPromotionTypeCode" runat="server" value='<%# Eval("PromotionTypeCode") %>' />
                                                                             <asp:ImageButton runat="server" ID="btnShow" CommandName="ShowPromotionType"  ImageUrl='<%# GetImage(DataBinder.Eval(Container.DataItem, "PicturePromotionTypeUrl")) %>' />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                    
                                                                    </FooterTemplate>
                                        </asp:DataList>
                          </div>
                          <div class="sub-title m-b-10"></div>
                          </div>
                          <style>
                              .scrollbar {
                               
                                height: auto;
                                width: 100%;
                                
                                overflow-y: hidden;
                                overflow-x: scroll;
                               
                              }
                              .force-overflow {
                                min-height: 450px;
                              } #style-3::-webkit-scrollbar-track {
                                /* -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3); */
                                background-color:#990305;
                              }

                              #style-3::-webkit-scrollbar {
                                width: 0px;
                                height: 3px;
                                background-color: #F5F5F5;
                               
                              }

                              #style-3::-webkit-scrollbar-thumb {
                                background-color: #96323365;
                                opacity: 0.5;
  filter: alpha(opacity=50);
                              }
                            </style>
                            <div class="card-block">
                            <div class="col-md-6 m-r-5 input-group" style="float: right; font-size: 12px;">
                                  <asp:TextBox ID="txtSearchRecipe"  runat="server"  class="form-control" Placeholder="รหัส, ชื่อ, ส่วนประกอบ"></asp:TextBox>
                                  <div class="input-group-append">     
                               <asp:LinkButton ID="BtnSearchRecipe1" runat="server" CssClass="btn-mk-third btn-search-list" OnClick="BtnSearchRecipe_Click" ><i class=" ion-ios-search-strong" style="font-size:16px"></i></asp:LinkButton>
                              </div>
                              </div> 
                            </div>

                              <div class="card-block" style=" overflow-y: scroll; height: 50vh; padding-top: 0rem; padding-bottom: 0rem; "> 
                             
                                     <div class="container">
                                       <div id="gvPromotionComboSection" runat="server">
                                          <asp:DataList ID="dtlPromotionCombo" OnItemCommand="dtlPromotionDetail_ItemCommand" OnItemDataBound="dtlPromotionDetail_ItemDataBound" RepeatDirection="Horizontal"
                                           RepeatColumns="3" runat="server" style="width: 100% ; "  >  
                                      
                                                                      <HeaderTemplate>   
                                                                      </HeaderTemplate>
                                                                      <ItemTemplate  >
                                                                         
                                                                              <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# Eval("PromotionDetailId") %>' />
                                                                                <asp:HiddenField ID="hidPromotionDetailName" runat="server" value='<%# Eval("PromotionDetailName") %>' />
                                                                              <asp:HiddenField ID="hidProductDesc" runat="server" value='<%# Eval("ProductDesc") %>' />
                                                                                
                                                                                 <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                                  <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                                <asp:HiddenField ID="hidProductCode" runat="server" value='<%# Eval("ProductCode") %>' />
                                                                                 <asp:HiddenField ID="hidProductName" runat="server" value='<%# Eval("ProductName") %>' />
                                                                                  <asp:HiddenField ID="hidDiscountAmount" runat="server" value='<%# Eval("DiscountAmount") %>' />
                                                                                 <asp:HiddenField ID="hidDiscountPercent" runat="server" value='<%# Eval("DiscountPercent") %>' />
                                                                                 <asp:HiddenField ID="hidPrice" runat="server" value='<%# Eval("Price") %>' />
                                                                               <asp:Button  runat="server" class="btn-menu" CommandName="addtocart" style="width: 100% ; text-align:center;"  ID="btnShow" 
                                                                                    Text='<%# GetProductName(DataBinder.Eval(Container.DataItem, "ProductName"),DataBinder.Eval(Container.DataItem, "PromotionDetailName")) %>' />      
                                                                      </ItemTemplate>
                                                                      <FooterTemplate>
                                                                      </FooterTemplate>
                                                                  </asp:DataList> 
                                             </div>

                                         <div id="gvPromotionTypeSection" runat="server">
                                          <asp:DataList ID="dtlPromotionTypebyCampaign" OnItemCommand="dtlPromotionTypebyCampaign_ItemCommand" OnItemDataBound="dtlPromotionTypebyCampaign_ItemDataBound" RepeatDirection="Horizontal"
                                           RepeatColumns="3" runat="server" style="width: 100% ; "  >  
                                      
                                                                      <HeaderTemplate>   
                                                                      </HeaderTemplate>
                                                                      <ItemTemplate  >
                                                                                                                                                         
                                                                                 <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                                  <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                               <asp:Button  runat="server" class="btn-menu" CommandName="addtocart" style="width: 100% ; text-align:center;"  ID="btnShow" 
                                                                                    Text='<%# GetPromotionName(DataBinder.Eval(Container.DataItem, "PromotionTypeCode"), DataBinder.Eval(Container.DataItem, "PromotionTypeName")) %>' />      
                                                                      </ItemTemplate>
                                                                      <FooterTemplate>
                                                                      </FooterTemplate>
                                                                  </asp:DataList> 
                                             </div>

                                         <div id="gvPromotionSection" runat="server">
                                          <asp:DataList ID="dtlPromotionbyPromotionType" OnItemCommand="dtlPromotionbyPromotionType_ItemCommand" OnItemDataBound="dtlPromotionbyPromotionType_ItemDataBound" RepeatDirection="Horizontal"
                                           RepeatColumns="3" runat="server" style="width: 100% ; "  >  
                                      
                                                                      <HeaderTemplate>   
                                                                      </HeaderTemplate>
                                                                      <ItemTemplate>  

                 <div class="card">
                    <div class="card-item" >
                      <div>
                        <div>
                            <asp:ImageButton style="width:100% ;height:90px" runat="server" ID="btnPromotionShow" CommandName="ShowPromotionbyType" ImageUrl='<%# GetImage(DataBinder.Eval(Container.DataItem, "PicturePromotionUrl")) %>' />
                         <%-- <img src='<%# GetImage(DataBinder.Eval(Container.DataItem, "PicturePromotionUrl")) %>'
                            alt="Card image" style="width:100% ;height:90px">--%> 
                        </div>
                        <div class="badgez">
                          <div class="dffg badge--promotion ">
                            <div class="badge-detail">
                              <span> 45.2‬%</span>
                              <span>ส่วนลด</span>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="h-100 d-flex flex-column justify-content-end">
                        <h6><asp:Label runat="server" id="lblProdductName" Text='<%# Eval("CampaignCode") %>'></asp:Label>Aston Idea X</h6>
                        <div><a href=""><img style="width:25px;height:25px" src="../assets/img/aston/promotion/free.svg"
                            alt=""></a></div>
                        
                        <p style="color:red;"> <span style="text-decoration: line-through; color: #707070;">
                            ฿3,590.00</span> ฿1,790.00 </p>
                      </div>
                    </div>
                  </div></>
                </div>
                                                                                 <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                                  <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                                <asp:HiddenField ID="hidPromotionTypeCode" runat="server" value='<%# Eval("PromotionTypeCode") %>' />
                                                                              <%-- <asp:ImageButton runat="server" ID="btnPromotionShow" CommandName="ShowPromotionbyType" ImageUrl='<%# GetImage(DataBinder.Eval(Container.DataItem, "PicturePromotionUrl")) %>' />      --%>
                                                                      </ItemTemplate>
                                                                      <FooterTemplate>
                                                                      </FooterTemplate>
                                                                  </asp:DataList> 
                                             </div>
                                           <asp:GridView ID="gvPromotionDetail" runat="server" AutoGenerateColumns="False" cssclass="table-munu-list"  OnRowDataBound="gvPromotionDetail_RowDataBound"
                            TabIndex="0" style="width:100%; border:none;" CellSpacing="0" OnRowCommand="gvPromotionDetail_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="8%" ItemStyle-Width="8%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                <HeaderTemplate>

                                    <div align="center"></div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                             
                                         <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# Eval("PromotionDetailId") %>' />
                                                                            <asp:HiddenField ID="hidPromotionDetailName" runat="server" value='<%# Eval("PromotionDetailName") %>' />
                                                                          <asp:HiddenField ID="hidProductDesc" runat="server" value='<%# Eval("ProductDesc") %>' />
                                                                            
                                                                             <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                              <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                            <asp:HiddenField ID="hidProductCode" runat="server" value='<%# Eval("ProductCode") %>' />
                                                                             <asp:HiddenField ID="hidProductName" runat="server" value='<%# Eval("ProductName") %>' />
                                                                              <asp:HiddenField ID="hidDiscountAmount" runat="server" value='<%# Eval("DiscountAmount") %>' />
                                                                             <asp:HiddenField ID="hidDiscountPercent" runat="server" value='<%# Eval("DiscountPercent") %>' />
                                                                             <asp:HiddenField ID="hidPrice" runat="server" value='<%# Eval("Price") %>' />
                                                                   
                                                                                 <asp:LinkButton runat="server" ID="btnShow"   CommandName="addtocart" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ><i class="fa fa-cart-arrow-down f-20"></i></asp:LinkButton>  
                                                                                
                                                 
                                </ItemTemplate>

                            </asp:TemplateField>

                                 <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left" HeaderStyle-width="15%" ItemStyle-width="15%" >

                                    <HeaderTemplate >

                                        <div align="left">รหัส</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:Label  runat="server"  ID="lblProductName" 
                                       Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode") %>' />      
                                                                      
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"  >

                                    <HeaderTemplate>

                                        <div align="center">ชื่อ</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>    
                                        
                                        <asp:Label  runat="server"   style="width: 100% ; text-align:center;"  ID="Label2" 
                                            Text='<%# GetProductName(DataBinder.Eval(Container.DataItem, "ProductName"),DataBinder.Eval(Container.DataItem, "PromotionDetailName")) %>' />      
                                                                      
                                                                                 
                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                               <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"   >

                                    <HeaderTemplate>

                                        <div align="right">ราคา</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>    
                                              <%#GetPricedtlPromotiondetail(DataBinder.Eval(Container.DataItem, "Price"),DataBinder.Eval(Container.DataItem, "DiscountAmount"),DataBinder.Eval(Container.DataItem, "DiscountPercent"))%>
                                   
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="18%" ItemStyle-Width="18%"  >

                                    <HeaderTemplate>

                                      <div align="center">สารก่อภูมิแพ้</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>    
                                      <%#(DataBinder.Eval(Container.DataItem, "AllergyRemark"))%>
                                     
                                   
                                   
                                    

                                         
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
                                                             
                          
                          
                          <!--<div style="padding:1.25rem;"> <div class="sub-title headertext " >รายละเอียดสินค้า</div>
                            
                                     
                                                                <div class="card-block">
                                                            
                                                                    <asp:Label ID="lblProductDescriptionfromPromotiondetail" runat="server"></asp:Label>
                                                                </div>
                                                          
                                                   </div>
                          -->

                      </div>
                      <div id="2"  class="card p-t-10" style="display:none; height:100vh;">
                         
              
            <div class="card-header">
                <div class="sub-title headertext "> การชำระเงิน</div>
            </div>
            <div class="card-block" style="padding:1.25rem;">
                <div class="form-group row">
                    <label class="col-sm-12 col-form-label"> <asp:RadioButton cssclass="m-r-15 "  ID="rad1" runat="server" GroupName="PaymentType" Enabled="true"  /> บัตรเครดิต/บัตรเดบิต</label> 
                    <label class="col-sm-6 col-form-label"> <asp:RadioButton cssclass="m-r-15 "  ID="rad2" runat="server" GroupName="PaymentType" Enabled="true"  /> Gift Voucher / e-Voucher</label> 
                    <span class="col-form-label">Code:</span> <div class="col-sm-4"><input type="text" class="form-control"></div>
                    <label class="col-sm-12 col-form-label"> <asp:RadioButton cssclass="m-r-15 "  ID="rad3" runat="server" GroupName="PaymentType" Enabled="true"  /> เงินสด   [ชำระพอดี]</label>  
                    <label class="col-sm-6 col-form-label"> 
                      <asp:RadioButton cssclass="m-r-15 "  ID="rad" runat="server" GroupName="PaymentType" Enabled="true" Checked="true" />ราคาที่ต้องชำระ</label>
                    <div class="col-sm-4 m-l-37">
                        <asp:TextBox  cssclass="form-control" ID="txtTotalPrice" style="text-align:right" runat="server" Enabled="false" ></asp:TextBox>
                    </div>
                         <span><div class="col-sm-4"> </div></span>

                    <label class="col-sm-6 col-form-label p-l-42 p-r-0 p-l-0">ลูกค้าจ่ายเงิน</label>
                    <div class="col-sm-4 m-l-37 "> 
                        <asp:TextBox cssclass="form-control" ID="txtcustomerpay"  AutoPostBack="True" style="text-align:right" OnTextChanged="txtcustomerpay_TextChanged" runat="server" ></asp:TextBox>
                    </div>
                  <div class="col-sm-1 p-r-0 p-l-0"> บาท</div>
                    <label class="col-sm-6 col-form-label p-l-42">   ทอน</label>
                    <div class="col-sm-4 m-l-37">
                        <asp:TextBox ID="txtReturnCashAMount" cssclass="form-control" style="text-align:right"  runat="server" ReadOnly="true" Enabled="false" ></asp:TextBox>
                        <asp:Label ID="lblReturnCashAMount" runat="server" ForeColor="red"></asp:Label>
                    </div>
                    <div class="col-sm-1 p-r-0 p-l-0"> บาท</div>
                </div>
            </div>
            <div class="card-header">
                <div class="sub-title headertext "> เวลาส่งสินค้า</div>
            </div>
                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                              <ContentTemplate>
                                    <div class="card-block f-12" style="padding:1.25rem;">
                <div class="form-group row">
                    
                    <label class="col-sm-12 col-form-label"><asp:RadioButton ID="radordernow" runat="server" GroupName="OrderType" AutoPostBack="true" OnCheckedChanged="ordernow_Changed" Checked="true" CssClass="m-r-15"/>   Order Now</label>
                    
                
      
                   <label class="col-sm-6 col-form-label"> <asp:RadioButton ID="radpreorder" runat="server" GroupName="OrderType" AutoPostBack="true" OnCheckedChanged="radpreorder_Changed" CssClass="m-r-15"/>  Pre Order</label>
                    <div class="col-sm-4 m-l-37">
                        <input type="date" id="txtdPreOrder" runat="server" class="form-control">
                        

                    </div>
                  
                    <label class="col-sm-6 col-form-label"> </label>
                    <div class="col-sm-4 m-l-37">
                    <!-- <div class="input-group clockpicker" >
                        <input type="text" id="txttimePreOrder" class="form-control" runat="server">
                        
                        <span class="input-group-addon">
                            <span class="fa fa-clock-o"></span>
                        </span>
                        
                    </div> -->
                    <div class="input-group">
                      <input type="number" id="txtPreOrderHr" class="form-control"  placeholder="hh" runat="server" />
                      <span class="input-group-addon" style="background-color:transparent;color:gray">:</span>
                      <input type="number" id="txtPreOrderMin" class="form-control"  placeholder="mm" runat="server"/>
                    </div>
                  </div>
                    
                  
                
               




            
                </div>
            </div>
                              </ContentTemplate>
                          </asp:UpdatePanel>
          
                          
            <div class="card-header">
                <div class="sub-title "> </div>
            </div>
            <div class="text-center m-t-5  col-12" >
                <%--<button type="button" class="btn-mk-pri btn-checkout  m-r-5 btnClick" runat="server" onclick="SubmitOrder">Submit</button>--%>
                <asp:Button ID="SubmitOrder" runat="server" class="btn-mk-pri btn-checkout  m-r-5" Text="Submit" OnClick="SubmitOrder_Click" />
              <button type="button" onclick="displaySelect();" class="btn-mk-pri btn-back btnClick unbind ">Back</button>
               
            </div>
      

          </div>
                        </div>  
                     <div class="col-sm-3 p-l-1 p-r-1 " >
              <div class="card" style="height:100vh">
            <div class="card-header" style="padding:5px 5px"> 
                <div class="sub-title p-b-5 headertext" >ข้อมูลลูกค้า
                        <div class="card-header-right" >
                            <asp:LinkButton ID="btneditcustomer" runat="server" CssClass="btn-mk-pri btn-edit" OnClick="btneditcustomer_Click" ><i class="fa fa-edit"></i>แก้ไข</asp:LinkButton>
                            </div>
                </div>
            </div>
            <div class="card-block">
                <table class="customer-tb" style="width:100%">
                    
                    <tbody>
                        <tr>
                           <th  colspan="1"><asp:Label ID="lblCustomerName" runat="server"></asp:Label></th>
                           <th style="text-align:right ; font-size:13px ; " > <asp:Label ID="lblCustomerCode" runat="server"></asp:Label></th>
                        <tr>
                            <td style="text-align:left ; ">เบอร์ติดต่อ&nbsp1 <span >:</span></td>
                            <td style="text-align:right ; "> <asp:Label ID="lblCustomerPhone1" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
               
       
            </div>
          <div class="card-block">
            
            <div class="row m-0" >
                <div class="col-sm-12 p-0">
                    <div class="sub-title p-b-0 headertext " style="text-transform: none; font-size:14px;" >Note Profile :  </div>
                <!-- <p class="m-0 p-b-1">Note Profile:</p> -->
                <textarea id="txtNoteProfile" style="width:100%" runat="server" ></textarea>
              </div>
              <div class="col-sm-5 p-0 ">
                  <div class="sub-title p-b-0 headertext " style="text-transform: none; font-size:14px;" >Note เดิม : </div>
                  <!-- <p  class="m-0 m-b-2">โน๊ตเดิม:</p> -->
              <textarea id="txtOrderNote" style="width:100%" runat="server" readonly="readonly" ></textarea>
              </div>
              <div class="col-sm-2 p-0"  >
            
          <div class="text-center  "style="vertical-align: middle;
          line-height: 20px;     margin-top: 30px;">
              <asp:Button ID="txtOrderNoteCopy" CssClass="btn-mk-four btn-copy" runat="server" Text="Copy" OnClick="txtOrderNoteCopy_Click" />

             
              <!--<asp:Button  ID="txtOrderNoteCopy1" CssClass="btn-mk-four btn-copy" runat="server" Text="Copy >" />-->
        
             
          </div>
                </div>
              
              <div class="col-sm-5 p-0">
                  <div class="sub-title p-b-0 headertext "  style="text-transform: none; font-size: 14px;">Note ในการสั่งครั้งนี้ </div>
                  <!-- <p  class="m-0 m-b-2">โน๊ตในการสั่งครั้งนี้:</p> -->
                <textarea id="txtOrderNoteLast" style="width:100%" runat="server" ></textarea>
                </div> 
            </div>
          </div>
        
      <div style=" overflow-y: scroll; height: 50vh ">
        <div class="card-header" style="padding:5px 5px">

            <div class="sub-title p-b-5 headertext" >ที่อยู่จัดส่ง   <asp:LinkButton id="btnMapAddress" OnClick="btnMapAddress_Click" cssclass="btn-mk-pri btn-map"  runat="server" style=" text-decoration: none !important;"><i  class=" fa  ti-location-pin " style="font-size:1.2em;color:red" ></i> map</asp:LinkButton>  
                    
              </div>
            </div>
                   <div class="card-block">
                      <ul class="m-0" style="list-style-type: none; padding: 0px ">
                          
                          <li>
                             <asp:Label ID="lblCustomerAddress" runat="server"></asp:Label>
                          </li>
                        </ul>
                   </div>
                   <div class="card-header" style="padding:5px 5px">
                    <div class="sub-title p-b-5 headertext" >สาขา</div>
                      </div>
                             <div class="card-block ">
                                  <asp:DataList ID="dtlNearestBranch" runat="server"  OnItemCommand="dtlNearestBranch_ItemCommand"
                                      OnItemDataBound="dtlNearestBranch_ItemDataBound" style="width: 100%;float: right;">
                                      <HeaderTemplate></HeaderTemplate>
                                      <ItemTemplate>
                                    <span>
                                        <asp:Button runat="server" ID="hidRadioBtn" CommandName="radBranch" style="display:none"  />
                                        <asp:HiddenField ID="hidBranchCode" runat="server" Value='<%# Eval("BranchCode") %>' />
                                        <asp:RadioButton runat="server" id="radBranch" Value='<%# Eval("BranchCode") %>' />
                                        <asp:Label runat="server" ID="lblBranchName" Text='<%# Eval("BranchName") + " (" +  Math.Round( Convert.ToDouble(Eval("Distance")), 2).ToString() + "km)" %>'></asp:Label></span>
                                        <asp:HiddenField ID="hidBranchName" runat="server" Value='<%# Eval("BranchName") %>' />
                                        &nbsp;&nbsp;<div style="float:right;"> 
                                          <span id="RiderOK" runat="server"><svg id="bike" class="m-r-5" style="width: 20px; height: 20px;" viewBox="0 -4 472.06723 472"><title>bike</title><path d="m112.066406 8.039062h16v232h-16zm0 0" fill="#dedede"></path><path d="m80.066406 416.039062c-.050781 6.761719 4.148438 12.824219 10.496094 15.148438 6.34375 2.324219 13.46875.410156 17.796875-4.78125 4.324219-5.191406 4.921875-12.542969 1.492187-18.367188h33.496094c3.304688 18.863282-4.917968 37.882813-20.917968 48.398438-16.003907 10.515625-36.722657 10.515625-52.722657 0-16.003906-10.515625-24.222656-29.535156-20.917969-48.398438h33.496094c-1.4375 2.425782-2.199218 5.1875-2.21875 8zm0 0" fill="#734730"></path><path d="m436.066406 384.039062h15.703125c14.316407 15.933594 16.34375 39.421876 4.972657 57.570313-11.375 18.148437-33.394532 26.566406-53.972657 20.632813-20.582031-5.933594-34.738281-24.785157-34.703125-46.203126.023438-2.8125.289063-5.617187.800782-8.382812 11.289062-.847656 22.105468-4.875 31.199218-11.617188 10.386719-7.789062 23.019532-12 36-12zm0 0" fill="#734730"></path><path d="m432.066406 416.039062c0 8.839844-7.164062 16-16 16-8.835937 0-16-7.160156-16-16 0-8.835937 7.164063-16 16-16 8.835938 0 16 7.164063 16 16zm0 0" fill="#dedede"></path><path d="m352.066406 192.039062h24l16-32h-40c-8.835937 0-16 7.164063-16 16 0 8.839844 7.164063 16 16 16zm0 0" fill="#f05d46"></path><path d="m96.066406 432.039062c-5.726562.035157-11.03125-3.011718-13.886718-7.980468-2.851563-4.96875-2.8125-11.085938.105468-16.019532h27.566406c2.917969 4.933594 2.957032 11.050782.101563 16.019532-2.851563 4.96875-8.15625 8.015625-13.886719 7.980468zm0 0" fill="#dedede"></path><path d="m8.066406 388.96875c0-52.929688 56-76.929688 56-76.929688v-32h176c0 24-16 24-16 24h-4c-19.882812 0-36 16.121094-36 36 0 19.882813 16.117188 36 36 36h82.710938c5.746094.003907 11.050781-3.074218 13.898437-8.0625l27.390625-47.9375-8-88c-1.328125-19.613281 12.570313-36.988281 32-40l12.554688 99.304688c2.09375 12.566406 11.441406 22.695312 23.796875 25.785156l11.648437 2.910156v24h-16c-26.507812 0-48 21.492188-48 48v16h-344zm0 0" fill="#f05d46"></path><path d="m400.066406 344.039062h32c22.09375 0 40 17.910157 40 40h-36c-12.980468 0-25.613281 4.210938-36 12-9.09375 6.742188-19.910156 10.769532-31.199218 11.617188-1.601563.128906-3.199219.382812-4.800782.382812h-12v-16c0-26.507812 21.492188-48 48-48zm0 0" fill="#f05d46"></path><path d="m360.066406 408.039062h-16v-16c.035156-30.910156 25.085938-55.964843 56-56h16v16h-16c-22.078125.027344-39.972656 17.921876-40 40zm0 0" fill="#d64d37"></path><path d="m240.066406 280.039062h-88v-16c0-8.835937 7.164063-16 16-16h56c8.835938 0 16 7.164063 16 16zm0 0" fill="#734730"></path><path d="m336.066406 408.039062c-4.703125 18.808594-21.597656 32-40.984375 32h-86.03125c-19.382812 0-36.28125-13.191406-40.984375-32zm0 0" fill="#dedede"></path><path d="m304.066406 168.039062v16h40c4.417969 0 8-3.578124 8-8 0-4.417968-3.582031-8-8-8zm0 0" fill="#8c563b"></path><path d="m120.066406 288.039062h-56c-.742187.003907-1.484375-.101562-2.199218-.308593l-49.382813-14.089844c-8.246094-2.320313-13.507813-10.371094-12.320313-18.855469 1.1875-8.480468 8.460938-14.777344 17.023438-14.746094h102.878906c4.417969 0 8 3.582032 8 8v32c0 4.421876-3.582031 8-8 8zm-54.878906-16h46.878906v-16h-94.878906c-.5625-.003906-1.039062.410157-1.117188.96875-.078124.554688.269532 1.085938.8125 1.234376zm0 0" fill="#dedede"></path><path d="m24.066406 144.039062h88c8.835938 0 16 7.164063 16 16v80h-120v-80c0-8.835937 7.164063-16 16-16zm0 0" fill="#f05d46"></path><path d="m8.066406 168.039062h120v16h-120zm0 0" fill="#d64d37"></path><path d="m152.066406 216.039062c0 17.675782 14.328125 32 32 32h88l-32 96h32l34.914063-90.765624c3.1875-8.300782 2.152343-17.632813-2.777344-25.03125-5.082031-7.625-13.636719-12.203126-22.800781-12.203126zm0 0" fill="#8c563b"></path><path d="m304.066406 376.039062h-72l8-32h32c17.675782 0 32 14.328126 32 32zm0 0" fill="#dedede"></path><path d="m152.066406 96.039062h64v120h-64zm0 0" fill="#d64d37"></path><path d="m216.066406 32.039062h-64v64h64v-32h16l-16-24zm0 0" fill="#fec9a3"></path><path d="m208.066406 152.039062-12.328125-41.300781c-2.597656-10.398437-13.136719-16.71875-23.53125-14.117187-10.394531 2.601562-16.714843 13.136718-14.117187 23.53125l13.433594 45.710937c2.671874 10.683594 12.269531 18.175781 23.28125 18.175781h93.261718v-32zm0 0" fill="#f05d46"></path><path d="m304.066406 152.039062h-16v32h16c8.835938 0 16-7.160156 16-16 0-8.835937-7.164062-16-16-16zm0 0" fill="#fec9a3"></path><path d="m128.066406 8.039062h-17.503906c-4.269531 0-8.484375.996094-12.304688 2.90625-6.523437 3.261719-14.078124 3.800782-21 1.496094l-13.191406-4.402344v48l13.191406 4.402344c6.921876 2.304688 14.476563 1.765625 21-1.496094 3.820313-1.910156 8.035157-2.90625 12.304688-2.90625h17.503906zm0 0" fill="#f05d46"></path><path d="m64.066406 312.039062h-48v44.167969c11.054688-19.328125 27.820313-34.757812 48-44.167969zm0 0" fill="#c44639"></path><g fill="#d64d37"><path d="m144.066406 304.039062h-88c-4.417968 0-8 3.582032-8 8 0 4.421876 3.582032 8 8 8h88c4.417969 0 8-3.578124 8-8 0-4.417968-3.582031-8-8-8zm0 0"></path><path d="m104.066406 336.039062h48v16h-48zm0 0"></path><path d="m104.066406 368.039062h48v16h-48zm0 0"></path></g><path d="m214.9375 24.039062c-3.996094-15.601562-18.988281-25.78125-34.964844-23.75-15.976562 2.03125-27.941406 15.644532-27.90625 31.75v8h96v-16zm0 0" fill="#f05d46"></path></svg></span>
                                          <span id="RiderNotOK" runat="server"><svg id="bike-1" class="m-r-5" viewBox="0 0 480 480.16691" width="20px" height="20px"><title>bike-1</title><path d="m432.082031 344.039062h-8v-16c0-3.667968-2.496093-6.867187-6.054687-7.757812l-11.648438-2.914062c-9.175781-2.257813-16.15625-9.71875-17.808594-19.023438l-11.429687-90.457031c2.617187-.320313 4.902344-1.921875 6.101563-4.269531l16-32c1.242187-2.480469 1.109374-5.425782-.351563-7.785157-1.457031-2.359375-4.035156-3.792969-6.808594-3.792969h-40c-10.132812.042969-19.148437 6.445313-22.527343 16h-1.472657c0-13.253906-10.742187-24-24-24h-80v-72h8c2.953125 0 5.664063-1.621093 7.054688-4.226562 1.394531-2.601562 1.242187-5.757812-.398438-8.210938l-13.054687-19.5625h22.398437v-16h-24.796875c-4.089844-20.136718-22.773437-33.902343-43.222656-31.835937-20.445312 2.066406-36 19.285156-35.980469 39.835937v184c.011719 10.359376 4.058594 20.304688 11.28125 27.730469-7 4.359375-11.265625 12.019531-11.28125 20.269531v8h-16v-272h-17.503906c-5.511719.007813-10.949219 1.292969-15.886719 3.753907-4.621094 2.328125-9.984375 2.710937-14.886718 1.054687l-13.191407-4.398437c-2.441406-.8125-5.125-.402344-7.207031 1.101562-2.085938 1.503907-3.324219 3.917969-3.324219 6.488281v48c0 3.445313 2.207031 6.503907 5.472657 7.59375l13.191406 4.398438c3.632812 1.214844 7.4375 1.835938 11.265625 1.839844 5.5-.007813 10.917969-1.296875 15.832031-3.765625 2.714844-1.355469 5.703125-2.0625 8.734375-2.066407h1.503906v81.472657c-2.558593-.945313-5.265625-1.445313-8-1.472657h-80c-13.253906 0-23.9999998 10.746094-23.9999998 24v80c.0273438 1.277344.3632808 2.527344.9843748 3.640626-.628906 1.757812-.960937 3.613281-.9843748 5.480468.0195308 7.640625 5.0781248 14.355469 12.4179688 16.480469l43.582031 12.429687v17.96875h-40c-4.417969 0-8 3.582032-8 8v42.121094c-5.289062 10.839844-8.0273435 22.746094-7.9999998 34.808594v19.070312c0 4.417969 3.5820308 8 7.9999998 8h32c0 30.929688 25.074219 56 56 56 30.929688 0 56-25.070312 56-56h10.3125c7.460938 19.28125 26 31.992188 46.671875 32h86.035156c20.671876-.007812 39.210938-12.71875 46.671876-32h18.308593c-.050781 23.023438 13.996094 43.734376 35.40625 52.203126 21.410157 8.46875 45.820313 2.96875 61.53125-13.863282 15.710938-16.828125 19.519531-41.5625 9.601563-62.339844h5.460937c4.417969 0 8-3.582031 8-8-.023437-26.496093-21.5-47.972656-48-48zm30.992188 40h-26.992188c-14.707031.035157-29.011719 4.800782-40.796875 13.601563-7.890625 5.839844-17.277344 9.320313-27.066406 10.039063l-1.808594.175781c-.769531.105469-1.546875.167969-2.328125.183593h-4v-8c.027344-22.078124 17.921875-39.972656 40-40h32c14.589844.023438 27.324219 9.882813 30.992188 24zm-38.992188 40c0 4.417969-3.582031 8-8 8s-8-3.582031-8-8c0-4.417968 3.582031-8 8-8s8 3.582032 8 8zm-44.941406-248-8 16h-19.058594c-1.417969-.035156-2.800781-.460937-4-1.230468 2.46875-1.359375 4-3.953125 4-6.769532 0-2.816406-1.53125-5.40625-4-6.765624 1.199219-.769532 2.582031-1.195313 4-1.234376zm-49.601563 16c2.078126 5.816407 6.320313 10.609376 11.839844 13.378907-9.140625 9.425781-13.957031 22.226562-13.296875 35.34375l7.777344 85.496093-25.960937 45.421876c-4.117188-12.550782-14.171876-22.25-26.863282-25.910157l31.390625-81.601562c4.199219-10.882813 2.765625-23.132813-3.832031-32.753907-6.597656-9.617187-17.511719-15.371093-29.175781-15.375h-57.335938v-16h80c6.789063-.019531 13.246094-2.929687 17.761719-8zm-109.457031 184c-15.460937 0-28-12.535156-28-28 0-15.460937 12.539063-28 28-28h4c8.304688 0 24-6.6875 24-32v-16c-.023437-2.730468-.523437-5.4375-1.46875-8h14.398438l-28.519531 85.472657c0 .121093 0 .25-.074219.367187-.070313.121094-.078125.136719-.09375.21875l-6.488281 25.941406zm-12-160h-48v-52.800781l3.691407 12.5625c3.558593 14.246094 16.359374 24.242188 31.046874 24.238281h13.261719zm8 16h65.335938c6.402343-.003906 12.394531 3.148438 16.019531 8.425782 3.621094 5.277344 4.402344 12.003906 2.09375 17.976562l-32.945312 85.597656h-15.40625l28.496093-85.472656c.8125-2.4375.402344-5.121094-1.101562-7.207031-1.503907-2.085937-3.917969-3.320313-6.492188-3.320313h-88c-10.167969-.011718-19.226562-6.417968-22.628906-16zm30.25 128h25.75c10.167969.011719 19.230469 6.417969 22.632813 16h-52.382813zm65.75-184c0 4.417969-3.582031 8-8 8h-8v-16h8c4.417969 0 8 3.582032 8 8zm-32 8h-85.261719c-7.453124-.027343-13.910156-5.171874-15.609374-12.429687l-13.335938-45.402344c-.851562-3.40625-.085938-7.011719 2.070312-9.78125 2.160157-2.765625 5.476563-4.382812 8.988282-4.386719 5.351562.035157 9.976562 3.757813 11.148437 8.984376l12.328125 41.304687c1.015625 3.390625 4.132813 5.714844 7.671875 5.710937h72zm-72-52-4.558593-15.277343c-.421876-1.621094-.988282-3.203125-1.695313-4.722657h6.253906zm1.34375-79.589843 7.707031 11.589843h-1.050781c-4.417969 0-8 3.582032-8 8v24h-48v-48h48c.003907 1.582032.472657 3.125 1.34375 4.441407zm-25.34375-36.410157c10.136719.042969 19.152344 6.445313 22.53125 16h-45.058593c3.378906-9.554687 12.394531-15.957031 22.527343-16zm-24 256c0-4.417968 3.582031-8 8-8h56c4.417969 0 8 3.582032 8 8v8h-72zm-144-24v-48h96v48zm94.496094-192c-5.511719.007813-10.949219 1.292969-15.886719 3.753907-4.625 2.320312-9.984375 2.703125-14.886718 1.054687l-7.722657-2.574218v-31.128907l2.664063.886719c8.929687 2.996094 18.683594 2.300781 27.097656-1.925781 2.714844-1.355469 5.703125-2.0625 8.734375-2.066407h1.503906v32zm-86.496094 112h80c4.417969 0 8 3.582032 8 8v8h-96v-8c0-4.417968 3.582031-8 8-8zm-8 97.121094c0-.617187.503907-1.121094 1.121094-1.121094h94.878906v16h-46.878906l-48.320313-13.800781c-.472656-.140625-.800781-.582031-.800781-1.078125zm8 62.878906h12.066407c-4.28125 3.410157-8.3125 7.121094-12.066407 11.105469zm72 136c-22.078125-.023437-39.972656-17.917968-40-40h16c0 13.257813 10.746094 24 24 24 13.257813 0 24-10.742187 24-24h16c-.023437 22.082032-17.917969 39.976563-40 40zm8-40c0 4.417969-3.582031 8-8 8s-8-3.582031-8-8zm191.019531 16h-86.035156c-11.769531.007813-22.714844-6.035156-28.984375-16h144c-6.265625 9.964844-17.210937 16.007813-28.980469 16zm48.980469-40v8h-328v-11.070312c0-42.730469 41.929688-65.105469 49.792969-68.929688h86.207031v-16h-80v-16h159.113281c-1.894531 7.199219-6.222656 7.9375-7.113281 8h-4c-24.300781 0-44 19.699219-44 44 0 24.300782 19.699219 44 44 44h84c.863281-.027343 1.714844-.203124 2.523438-.511718 7.136719-1.085938 13.398437-5.34375 17.03125-11.582032l27.390625-47.90625c.832031-1.425781 1.203125-3.074218 1.054687-4.71875l-8-87.28125c-1.183593-12.472656 5.800781-24.289062 17.296875-29.269531l11.367188 89.894531c2.617187 15.703126 14.296875 28.355469 29.738281 32.222657l5.597656 1.402343v9.75h-8c-30.910156.035157-55.964843 25.085938-56 56zm112 24c0 22.09375-17.90625 40-40 40-22.089843 0-40-17.90625-40-40 0-.414062 0-.800781 0-1.253906.585938-.105468 1.136719-.289062 1.722657-.402344 1.054687-.214843 2.085937-.480468 3.128906-.734374 1.875-.46875 3.726562-1.003907 5.550781-1.601563 1.023437-.351563 2.039063-.695313 3.039063-1.09375 1.222656-.480469 2.398437-1.007813 3.601562-1.601563-.664062 2.167969-1.015625 4.421876-1.042969 6.6875-.042969 12.1875 9.054688 22.476563 21.15625 23.921876 12.101563 1.445312 23.367188-6.414063 26.191407-18.269532 2.824218-11.855468-3.683594-23.945312-15.136719-28.113281 3.859375-.964844 7.8125-1.480469 11.789062-1.539063h11.9375c5.226563 6.910157 8.058594 15.335938 8.0625 24zm0 0"></path><path d="m104.082031 344.039062h48v16h-48zm0 0"></path><path d="m104.082031 376.039062h48v16h-48zm0 0"></path></svg></span>
                                          <span id="Gift" runat="server"><svg id="gift" class="m-r-5" viewBox="0 0 57 57" width="20px" height="20px"><title>gift</title><rect x="1" y="13.002" style="fill:#CB465F;" width="55" height="12"></rect><rect x="6" y="25.002" style="fill:#EF4D4D;" width="46" height="30"></rect><path style="fill:#EBBA16;" d="M56,12.002H41.741C42.556,10.837,43,9.453,43,7.995c0-1.875-0.726-3.633-2.043-4.95 c-2.729-2.729-7.17-2.729-9.899,0l-2.829,2.829l-2.828-2.829c-2.729-2.729-7.17-2.729-9.899,0c-1.317,1.317-2.043,3.075-2.043,4.95 c0,1.458,0.444,2.842,1.259,4.007H1c-0.552,0-1,0.447-1,1s0.448,1,1,1h27v24H6c-0.552,0-1,0.447-1,1s0.448,1,1,1h22v15 c0,0.553,0.448,1,1,1s1-0.447,1-1v-15h22c0.552,0,1-0.447,1-1s-0.448-1-1-1H30v-24h26c0.552,0,1-0.447,1-1S56.552,12.002,56,12.002z M32.472,4.459c1.95-1.949,5.122-1.949,7.071,0C40.482,5.399,41,6.654,41,7.995c0,1.34-0.518,2.596-1.457,3.535l-0.472,0.472H24.929 l4.006-4.006l0.001-0.001l0.001-0.001L32.472,4.459z M16.916,11.53c-0.939-0.939-1.457-2.195-1.457-3.535 c0-1.341,0.518-2.596,1.457-3.536c1.95-1.949,5.122-1.949,7.071,0l2.828,2.829l-3.536,3.536c-0.331,0.331-0.622,0.735-0.898,1.179 h-4.994L16.916,11.53z"></path></svg></span>
                                          <span id="NoGift" runat="server"><svg id="gift-1" opacity="0" class="m-r-5" viewBox="0 0 57 57" width="20px" height="20px"><title>gift-1</title><path d="M57,12.002H41.741C42.556,10.837,43,9.453,43,7.995c0-1.875-0.726-3.633-2.043-4.95c-2.729-2.729-7.17-2.729-9.899,0 l-2.829,2.829l-2.828-2.829c-2.729-2.729-7.17-2.729-9.899,0c-1.317,1.317-2.043,3.075-2.043,4.95c0,1.458,0.444,2.842,1.259,4.007 H0v14h5v30h48v-30h4V12.002z M32.472,4.459c1.95-1.949,5.122-1.949,7.071,0C40.482,5.399,41,6.654,41,7.995 c0,1.34-0.518,2.596-1.457,3.535l-0.472,0.472H24.929l4.714-4.714l0,0L32.472,4.459z M16.916,11.53 c-0.939-0.939-1.457-2.195-1.457-3.535c0-1.341,0.518-2.596,1.457-3.536c1.95-1.949,5.122-1.949,7.071,0l2.828,2.829l-3.535,3.535 c-0.207,0.207-0.397,0.441-0.581,0.689c-0.054,0.073-0.107,0.152-0.159,0.229c-0.06,0.088-0.123,0.167-0.18,0.26h-4.972 L16.916,11.53z M2,24.002v-10h14.559h4.733h2.255H28v10H5H2z M28,26.002v12H7v-12H28z M7,40.002h21v14H7V40.002z M30,54.002v-14h21 v14H30z M51,38.002H30v-12h21V38.002z M55,24.002h-2H30v-10h9.899H55V24.002z"></path></svg></span>
                                         
                                          <span id="OfflineCircle" runat="server"><svg id="black-circle" class="m-r-5" viewBox="0 0 29.107 29.107" width="10px" height="10px"><title>black-circle</title><path d="M14.554,0C6.561,0,0,6.562,0,14.552c0,7.996,6.561,14.555,14.554,14.555c7.996,0,14.553-6.559,14.553-14.555 C29.106,6.562,22.55,0,14.554,0z" fill="#575656"></path></svg></span>
                                          <span id="OnlineCircle" runat="server"><svg id="circle" class="m-r-5" viewBox="0 0 512 512" width="10px" height="10px"><title>circle</title><path d="M256,0C114.837,0,0,114.837,0,256s114.837,256,256,256s256-114.837,256-256S397.163,0,256,0z" fill="#4ACD1F"></path></svg></span>
                                        </div>
                                        <asp:HiddenField ID="hidOnlineStatus" runat="server" Value='<%# Eval("OnlineStatus") %>' />
                                          </ItemTemplate>
                                    </asp:DataList>
                                 <asp:HiddenField ID="hidSelectedBranchCode1" runat="server" />
                                 <asp:HiddenField ID="hidSelectedBranchCode2" runat="server" />
                                 <asp:HiddenField ID="hidSelectedBranchCode3" runat="server" />
                                 <asp:HiddenField ID="hidSelectedBranchName1" runat="server" />
                                 <asp:HiddenField ID="hidSelectedBranchName2" runat="server" />
                                 <asp:HiddenField ID="hidSelectedBranchName3" runat="server" />
                            <%--    <ul style="list-style-type: none; padding: 0px ">
                                    <li style="width: 100%;float: right;">
                                    <span><input type="radio" name="num1" id="">  The Street รัชดาภิเษก  </span>
                                      
                                        <div style="float:right;"> 
                                          <span><svg id="bike" class="m-r-5" style="width: 20px; height: 20px;" viewBox="0 -4 472.06723 472"><title>bike</title><path d="m112.066406 8.039062h16v232h-16zm0 0" fill="#dedede"></path><path d="m80.066406 416.039062c-.050781 6.761719 4.148438 12.824219 10.496094 15.148438 6.34375 2.324219 13.46875.410156 17.796875-4.78125 4.324219-5.191406 4.921875-12.542969 1.492187-18.367188h33.496094c3.304688 18.863282-4.917968 37.882813-20.917968 48.398438-16.003907 10.515625-36.722657 10.515625-52.722657 0-16.003906-10.515625-24.222656-29.535156-20.917969-48.398438h33.496094c-1.4375 2.425782-2.199218 5.1875-2.21875 8zm0 0" fill="#734730"></path><path d="m436.066406 384.039062h15.703125c14.316407 15.933594 16.34375 39.421876 4.972657 57.570313-11.375 18.148437-33.394532 26.566406-53.972657 20.632813-20.582031-5.933594-34.738281-24.785157-34.703125-46.203126.023438-2.8125.289063-5.617187.800782-8.382812 11.289062-.847656 22.105468-4.875 31.199218-11.617188 10.386719-7.789062 23.019532-12 36-12zm0 0" fill="#734730"></path><path d="m432.066406 416.039062c0 8.839844-7.164062 16-16 16-8.835937 0-16-7.160156-16-16 0-8.835937 7.164063-16 16-16 8.835938 0 16 7.164063 16 16zm0 0" fill="#dedede"></path><path d="m352.066406 192.039062h24l16-32h-40c-8.835937 0-16 7.164063-16 16 0 8.839844 7.164063 16 16 16zm0 0" fill="#f05d46"></path><path d="m96.066406 432.039062c-5.726562.035157-11.03125-3.011718-13.886718-7.980468-2.851563-4.96875-2.8125-11.085938.105468-16.019532h27.566406c2.917969 4.933594 2.957032 11.050782.101563 16.019532-2.851563 4.96875-8.15625 8.015625-13.886719 7.980468zm0 0" fill="#dedede"></path><path d="m8.066406 388.96875c0-52.929688 56-76.929688 56-76.929688v-32h176c0 24-16 24-16 24h-4c-19.882812 0-36 16.121094-36 36 0 19.882813 16.117188 36 36 36h82.710938c5.746094.003907 11.050781-3.074218 13.898437-8.0625l27.390625-47.9375-8-88c-1.328125-19.613281 12.570313-36.988281 32-40l12.554688 99.304688c2.09375 12.566406 11.441406 22.695312 23.796875 25.785156l11.648437 2.910156v24h-16c-26.507812 0-48 21.492188-48 48v16h-344zm0 0" fill="#f05d46"></path><path d="m400.066406 344.039062h32c22.09375 0 40 17.910157 40 40h-36c-12.980468 0-25.613281 4.210938-36 12-9.09375 6.742188-19.910156 10.769532-31.199218 11.617188-1.601563.128906-3.199219.382812-4.800782.382812h-12v-16c0-26.507812 21.492188-48 48-48zm0 0" fill="#f05d46"></path><path d="m360.066406 408.039062h-16v-16c.035156-30.910156 25.085938-55.964843 56-56h16v16h-16c-22.078125.027344-39.972656 17.921876-40 40zm0 0" fill="#d64d37"></path><path d="m240.066406 280.039062h-88v-16c0-8.835937 7.164063-16 16-16h56c8.835938 0 16 7.164063 16 16zm0 0" fill="#734730"></path><path d="m336.066406 408.039062c-4.703125 18.808594-21.597656 32-40.984375 32h-86.03125c-19.382812 0-36.28125-13.191406-40.984375-32zm0 0" fill="#dedede"></path><path d="m304.066406 168.039062v16h40c4.417969 0 8-3.578124 8-8 0-4.417968-3.582031-8-8-8zm0 0" fill="#8c563b"></path><path d="m120.066406 288.039062h-56c-.742187.003907-1.484375-.101562-2.199218-.308593l-49.382813-14.089844c-8.246094-2.320313-13.507813-10.371094-12.320313-18.855469 1.1875-8.480468 8.460938-14.777344 17.023438-14.746094h102.878906c4.417969 0 8 3.582032 8 8v32c0 4.421876-3.582031 8-8 8zm-54.878906-16h46.878906v-16h-94.878906c-.5625-.003906-1.039062.410157-1.117188.96875-.078124.554688.269532 1.085938.8125 1.234376zm0 0" fill="#dedede"></path><path d="m24.066406 144.039062h88c8.835938 0 16 7.164063 16 16v80h-120v-80c0-8.835937 7.164063-16 16-16zm0 0" fill="#f05d46"></path><path d="m8.066406 168.039062h120v16h-120zm0 0" fill="#d64d37"></path><path d="m152.066406 216.039062c0 17.675782 14.328125 32 32 32h88l-32 96h32l34.914063-90.765624c3.1875-8.300782 2.152343-17.632813-2.777344-25.03125-5.082031-7.625-13.636719-12.203126-22.800781-12.203126zm0 0" fill="#8c563b"></path><path d="m304.066406 376.039062h-72l8-32h32c17.675782 0 32 14.328126 32 32zm0 0" fill="#dedede"></path><path d="m152.066406 96.039062h64v120h-64zm0 0" fill="#d64d37"></path><path d="m216.066406 32.039062h-64v64h64v-32h16l-16-24zm0 0" fill="#fec9a3"></path><path d="m208.066406 152.039062-12.328125-41.300781c-2.597656-10.398437-13.136719-16.71875-23.53125-14.117187-10.394531 2.601562-16.714843 13.136718-14.117187 23.53125l13.433594 45.710937c2.671874 10.683594 12.269531 18.175781 23.28125 18.175781h93.261718v-32zm0 0" fill="#f05d46"></path><path d="m304.066406 152.039062h-16v32h16c8.835938 0 16-7.160156 16-16 0-8.835937-7.164062-16-16-16zm0 0" fill="#fec9a3"></path><path d="m128.066406 8.039062h-17.503906c-4.269531 0-8.484375.996094-12.304688 2.90625-6.523437 3.261719-14.078124 3.800782-21 1.496094l-13.191406-4.402344v48l13.191406 4.402344c6.921876 2.304688 14.476563 1.765625 21-1.496094 3.820313-1.910156 8.035157-2.90625 12.304688-2.90625h17.503906zm0 0" fill="#f05d46"></path><path d="m64.066406 312.039062h-48v44.167969c11.054688-19.328125 27.820313-34.757812 48-44.167969zm0 0" fill="#c44639"></path><g fill="#d64d37"><path d="m144.066406 304.039062h-88c-4.417968 0-8 3.582032-8 8 0 4.421876 3.582032 8 8 8h88c4.417969 0 8-3.578124 8-8 0-4.417968-3.582031-8-8-8zm0 0"></path><path d="m104.066406 336.039062h48v16h-48zm0 0"></path><path d="m104.066406 368.039062h48v16h-48zm0 0"></path></g><path d="m214.9375 24.039062c-3.996094-15.601562-18.988281-25.78125-34.964844-23.75-15.976562 2.03125-27.941406 15.644532-27.90625 31.75v8h96v-16zm0 0" fill="#f05d46"></path></svg></span>
                                          <span><svg id="gift" class="m-r-5" viewBox="0 0 57 57" width="20px" height="20px"><title>gift</title><rect x="1" y="13.002" style="fill:#CB465F;" width="55" height="12"></rect><rect x="6" y="25.002" style="fill:#EF4D4D;" width="46" height="30"></rect><path style="fill:#EBBA16;" d="M56,12.002H41.741C42.556,10.837,43,9.453,43,7.995c0-1.875-0.726-3.633-2.043-4.95 c-2.729-2.729-7.17-2.729-9.899,0l-2.829,2.829l-2.828-2.829c-2.729-2.729-7.17-2.729-9.899,0c-1.317,1.317-2.043,3.075-2.043,4.95 c0,1.458,0.444,2.842,1.259,4.007H1c-0.552,0-1,0.447-1,1s0.448,1,1,1h27v24H6c-0.552,0-1,0.447-1,1s0.448,1,1,1h22v15 c0,0.553,0.448,1,1,1s1-0.447,1-1v-15h22c0.552,0,1-0.447,1-1s-0.448-1-1-1H30v-24h26c0.552,0,1-0.447,1-1S56.552,12.002,56,12.002z M32.472,4.459c1.95-1.949,5.122-1.949,7.071,0C40.482,5.399,41,6.654,41,7.995c0,1.34-0.518,2.596-1.457,3.535l-0.472,0.472H24.929 l4.006-4.006l0.001-0.001l0.001-0.001L32.472,4.459z M16.916,11.53c-0.939-0.939-1.457-2.195-1.457-3.535 c0-1.341,0.518-2.596,1.457-3.536c1.95-1.949,5.122-1.949,7.071,0l2.828,2.829l-3.536,3.536c-0.331,0.331-0.622,0.735-0.898,1.179 h-4.994L16.916,11.53z"></path></svg></span>
                                          <span><svg id="circle" class="m-r-5" viewBox="0 0 512 512" width="10px" height="10px"><title>circle</title><path d="M256,0C114.837,0,0,114.837,0,256s114.837,256,256,256s256-114.837,256-256S397.163,0,256,0z" fill="#4ACD1F"></path></svg></span>
                                        </div>
                                    </li>
                                    <li style="    width: 100%;
                                    float: right;">
                                      <span><input type="radio" name="num1" id"">   บิ๊กซี รัชดาภิเษก </span>
                                     
                                        <div style="float:right;"> 
                                            <span class="m-r-5" style="color:red">(ของไม่ครบ)</span>
                                          <span><svg id="bike-1" class="m-r-5" viewBox="0 0 480 480.16691" width="20px" height="20px"><title>bike-1</title><path d="m432.082031 344.039062h-8v-16c0-3.667968-2.496093-6.867187-6.054687-7.757812l-11.648438-2.914062c-9.175781-2.257813-16.15625-9.71875-17.808594-19.023438l-11.429687-90.457031c2.617187-.320313 4.902344-1.921875 6.101563-4.269531l16-32c1.242187-2.480469 1.109374-5.425782-.351563-7.785157-1.457031-2.359375-4.035156-3.792969-6.808594-3.792969h-40c-10.132812.042969-19.148437 6.445313-22.527343 16h-1.472657c0-13.253906-10.742187-24-24-24h-80v-72h8c2.953125 0 5.664063-1.621093 7.054688-4.226562 1.394531-2.601562 1.242187-5.757812-.398438-8.210938l-13.054687-19.5625h22.398437v-16h-24.796875c-4.089844-20.136718-22.773437-33.902343-43.222656-31.835937-20.445312 2.066406-36 19.285156-35.980469 39.835937v184c.011719 10.359376 4.058594 20.304688 11.28125 27.730469-7 4.359375-11.265625 12.019531-11.28125 20.269531v8h-16v-272h-17.503906c-5.511719.007813-10.949219 1.292969-15.886719 3.753907-4.621094 2.328125-9.984375 2.710937-14.886718 1.054687l-13.191407-4.398437c-2.441406-.8125-5.125-.402344-7.207031 1.101562-2.085938 1.503907-3.324219 3.917969-3.324219 6.488281v48c0 3.445313 2.207031 6.503907 5.472657 7.59375l13.191406 4.398438c3.632812 1.214844 7.4375 1.835938 11.265625 1.839844 5.5-.007813 10.917969-1.296875 15.832031-3.765625 2.714844-1.355469 5.703125-2.0625 8.734375-2.066407h1.503906v81.472657c-2.558593-.945313-5.265625-1.445313-8-1.472657h-80c-13.253906 0-23.9999998 10.746094-23.9999998 24v80c.0273438 1.277344.3632808 2.527344.9843748 3.640626-.628906 1.757812-.960937 3.613281-.9843748 5.480468.0195308 7.640625 5.0781248 14.355469 12.4179688 16.480469l43.582031 12.429687v17.96875h-40c-4.417969 0-8 3.582032-8 8v42.121094c-5.289062 10.839844-8.0273435 22.746094-7.9999998 34.808594v19.070312c0 4.417969 3.5820308 8 7.9999998 8h32c0 30.929688 25.074219 56 56 56 30.929688 0 56-25.070312 56-56h10.3125c7.460938 19.28125 26 31.992188 46.671875 32h86.035156c20.671876-.007812 39.210938-12.71875 46.671876-32h18.308593c-.050781 23.023438 13.996094 43.734376 35.40625 52.203126 21.410157 8.46875 45.820313 2.96875 61.53125-13.863282 15.710938-16.828125 19.519531-41.5625 9.601563-62.339844h5.460937c4.417969 0 8-3.582031 8-8-.023437-26.496093-21.5-47.972656-48-48zm30.992188 40h-26.992188c-14.707031.035157-29.011719 4.800782-40.796875 13.601563-7.890625 5.839844-17.277344 9.320313-27.066406 10.039063l-1.808594.175781c-.769531.105469-1.546875.167969-2.328125.183593h-4v-8c.027344-22.078124 17.921875-39.972656 40-40h32c14.589844.023438 27.324219 9.882813 30.992188 24zm-38.992188 40c0 4.417969-3.582031 8-8 8s-8-3.582031-8-8c0-4.417968 3.582031-8 8-8s8 3.582032 8 8zm-44.941406-248-8 16h-19.058594c-1.417969-.035156-2.800781-.460937-4-1.230468 2.46875-1.359375 4-3.953125 4-6.769532 0-2.816406-1.53125-5.40625-4-6.765624 1.199219-.769532 2.582031-1.195313 4-1.234376zm-49.601563 16c2.078126 5.816407 6.320313 10.609376 11.839844 13.378907-9.140625 9.425781-13.957031 22.226562-13.296875 35.34375l7.777344 85.496093-25.960937 45.421876c-4.117188-12.550782-14.171876-22.25-26.863282-25.910157l31.390625-81.601562c4.199219-10.882813 2.765625-23.132813-3.832031-32.753907-6.597656-9.617187-17.511719-15.371093-29.175781-15.375h-57.335938v-16h80c6.789063-.019531 13.246094-2.929687 17.761719-8zm-109.457031 184c-15.460937 0-28-12.535156-28-28 0-15.460937 12.539063-28 28-28h4c8.304688 0 24-6.6875 24-32v-16c-.023437-2.730468-.523437-5.4375-1.46875-8h14.398438l-28.519531 85.472657c0 .121093 0 .25-.074219.367187-.070313.121094-.078125.136719-.09375.21875l-6.488281 25.941406zm-12-160h-48v-52.800781l3.691407 12.5625c3.558593 14.246094 16.359374 24.242188 31.046874 24.238281h13.261719zm8 16h65.335938c6.402343-.003906 12.394531 3.148438 16.019531 8.425782 3.621094 5.277344 4.402344 12.003906 2.09375 17.976562l-32.945312 85.597656h-15.40625l28.496093-85.472656c.8125-2.4375.402344-5.121094-1.101562-7.207031-1.503907-2.085937-3.917969-3.320313-6.492188-3.320313h-88c-10.167969-.011718-19.226562-6.417968-22.628906-16zm30.25 128h25.75c10.167969.011719 19.230469 6.417969 22.632813 16h-52.382813zm65.75-184c0 4.417969-3.582031 8-8 8h-8v-16h8c4.417969 0 8 3.582032 8 8zm-32 8h-85.261719c-7.453124-.027343-13.910156-5.171874-15.609374-12.429687l-13.335938-45.402344c-.851562-3.40625-.085938-7.011719 2.070312-9.78125 2.160157-2.765625 5.476563-4.382812 8.988282-4.386719 5.351562.035157 9.976562 3.757813 11.148437 8.984376l12.328125 41.304687c1.015625 3.390625 4.132813 5.714844 7.671875 5.710937h72zm-72-52-4.558593-15.277343c-.421876-1.621094-.988282-3.203125-1.695313-4.722657h6.253906zm1.34375-79.589843 7.707031 11.589843h-1.050781c-4.417969 0-8 3.582032-8 8v24h-48v-48h48c.003907 1.582032.472657 3.125 1.34375 4.441407zm-25.34375-36.410157c10.136719.042969 19.152344 6.445313 22.53125 16h-45.058593c3.378906-9.554687 12.394531-15.957031 22.527343-16zm-24 256c0-4.417968 3.582031-8 8-8h56c4.417969 0 8 3.582032 8 8v8h-72zm-144-24v-48h96v48zm94.496094-192c-5.511719.007813-10.949219 1.292969-15.886719 3.753907-4.625 2.320312-9.984375 2.703125-14.886718 1.054687l-7.722657-2.574218v-31.128907l2.664063.886719c8.929687 2.996094 18.683594 2.300781 27.097656-1.925781 2.714844-1.355469 5.703125-2.0625 8.734375-2.066407h1.503906v32zm-86.496094 112h80c4.417969 0 8 3.582032 8 8v8h-96v-8c0-4.417968 3.582031-8 8-8zm-8 97.121094c0-.617187.503907-1.121094 1.121094-1.121094h94.878906v16h-46.878906l-48.320313-13.800781c-.472656-.140625-.800781-.582031-.800781-1.078125zm8 62.878906h12.066407c-4.28125 3.410157-8.3125 7.121094-12.066407 11.105469zm72 136c-22.078125-.023437-39.972656-17.917968-40-40h16c0 13.257813 10.746094 24 24 24 13.257813 0 24-10.742187 24-24h16c-.023437 22.082032-17.917969 39.976563-40 40zm8-40c0 4.417969-3.582031 8-8 8s-8-3.582031-8-8zm191.019531 16h-86.035156c-11.769531.007813-22.714844-6.035156-28.984375-16h144c-6.265625 9.964844-17.210937 16.007813-28.980469 16zm48.980469-40v8h-328v-11.070312c0-42.730469 41.929688-65.105469 49.792969-68.929688h86.207031v-16h-80v-16h159.113281c-1.894531 7.199219-6.222656 7.9375-7.113281 8h-4c-24.300781 0-44 19.699219-44 44 0 24.300782 19.699219 44 44 44h84c.863281-.027343 1.714844-.203124 2.523438-.511718 7.136719-1.085938 13.398437-5.34375 17.03125-11.582032l27.390625-47.90625c.832031-1.425781 1.203125-3.074218 1.054687-4.71875l-8-87.28125c-1.183593-12.472656 5.800781-24.289062 17.296875-29.269531l11.367188 89.894531c2.617187 15.703126 14.296875 28.355469 29.738281 32.222657l5.597656 1.402343v9.75h-8c-30.910156.035157-55.964843 25.085938-56 56zm112 24c0 22.09375-17.90625 40-40 40-22.089843 0-40-17.90625-40-40 0-.414062 0-.800781 0-1.253906.585938-.105468 1.136719-.289062 1.722657-.402344 1.054687-.214843 2.085937-.480468 3.128906-.734374 1.875-.46875 3.726562-1.003907 5.550781-1.601563 1.023437-.351563 2.039063-.695313 3.039063-1.09375 1.222656-.480469 2.398437-1.007813 3.601562-1.601563-.664062 2.167969-1.015625 4.421876-1.042969 6.6875-.042969 12.1875 9.054688 22.476563 21.15625 23.921876 12.101563 1.445312 23.367188-6.414063 26.191407-18.269532 2.824218-11.855468-3.683594-23.945312-15.136719-28.113281 3.859375-.964844 7.8125-1.480469 11.789062-1.539063h11.9375c5.226563 6.910157 8.058594 15.335938 8.0625 24zm0 0"></path><path d="m104.082031 344.039062h48v16h-48zm0 0"></path><path d="m104.082031 376.039062h48v16h-48zm0 0"></path></svg></span>
                                          <span><svg id="gift-1" class="m-r-5" viewBox="0 0 57 57" width="20px" height="20px"><title>gift-1</title><path d="M57,12.002H41.741C42.556,10.837,43,9.453,43,7.995c0-1.875-0.726-3.633-2.043-4.95c-2.729-2.729-7.17-2.729-9.899,0 l-2.829,2.829l-2.828-2.829c-2.729-2.729-7.17-2.729-9.899,0c-1.317,1.317-2.043,3.075-2.043,4.95c0,1.458,0.444,2.842,1.259,4.007 H0v14h5v30h48v-30h4V12.002z M32.472,4.459c1.95-1.949,5.122-1.949,7.071,0C40.482,5.399,41,6.654,41,7.995 c0,1.34-0.518,2.596-1.457,3.535l-0.472,0.472H24.929l4.714-4.714l0,0L32.472,4.459z M16.916,11.53 c-0.939-0.939-1.457-2.195-1.457-3.535c0-1.341,0.518-2.596,1.457-3.536c1.95-1.949,5.122-1.949,7.071,0l2.828,2.829l-3.535,3.535 c-0.207,0.207-0.397,0.441-0.581,0.689c-0.054,0.073-0.107,0.152-0.159,0.229c-0.06,0.088-0.123,0.167-0.18,0.26h-4.972 L16.916,11.53z M2,24.002v-10h14.559h4.733h2.255H28v10H5H2z M28,26.002v12H7v-12H28z M7,40.002h21v14H7V40.002z M30,54.002v-14h21 v14H30z M51,38.002H30v-12h21V38.002z M55,24.002h-2H30v-10h9.899H55V24.002z"></path></svg></span>
                                          <span><svg id="black-circle" class="m-r-5" viewBox="0 0 29.107 29.107" width="10px" height="10px"><title>black-circle</title><path d="M14.554,0C6.561,0,0,6.562,0,14.552c0,7.996,6.561,14.555,14.554,14.555c7.996,0,14.553-6.559,14.553-14.555 C29.106,6.562,22.55,0,14.554,0z" fill="#575656"></path></svg></span>
                                        </div>
                                    </li>
                                    <li style="    width: 100%;
                                    float: right;">
                                    <span><input type="radio" name="num1" id="">    เอสพลานาด รัชดาภิเษก </span>
                                    
                                        <div style="float:right;"> 
                                
                                          <span class="m-r-5" style="color:red">(ปิดชั่วคราว)</span>
                                          <span><svg id="bike" class="m-r-5" style="width: 20px; height: 20px;" viewBox="0 -4 472.06723 472"><title>bike</title><path d="m112.066406 8.039062h16v232h-16zm0 0" fill="#dedede"></path><path d="m80.066406 416.039062c-.050781 6.761719 4.148438 12.824219 10.496094 15.148438 6.34375 2.324219 13.46875.410156 17.796875-4.78125 4.324219-5.191406 4.921875-12.542969 1.492187-18.367188h33.496094c3.304688 18.863282-4.917968 37.882813-20.917968 48.398438-16.003907 10.515625-36.722657 10.515625-52.722657 0-16.003906-10.515625-24.222656-29.535156-20.917969-48.398438h33.496094c-1.4375 2.425782-2.199218 5.1875-2.21875 8zm0 0" fill="#734730"></path><path d="m436.066406 384.039062h15.703125c14.316407 15.933594 16.34375 39.421876 4.972657 57.570313-11.375 18.148437-33.394532 26.566406-53.972657 20.632813-20.582031-5.933594-34.738281-24.785157-34.703125-46.203126.023438-2.8125.289063-5.617187.800782-8.382812 11.289062-.847656 22.105468-4.875 31.199218-11.617188 10.386719-7.789062 23.019532-12 36-12zm0 0" fill="#734730"></path><path d="m432.066406 416.039062c0 8.839844-7.164062 16-16 16-8.835937 0-16-7.160156-16-16 0-8.835937 7.164063-16 16-16 8.835938 0 16 7.164063 16 16zm0 0" fill="#dedede"></path><path d="m352.066406 192.039062h24l16-32h-40c-8.835937 0-16 7.164063-16 16 0 8.839844 7.164063 16 16 16zm0 0" fill="#f05d46"></path><path d="m96.066406 432.039062c-5.726562.035157-11.03125-3.011718-13.886718-7.980468-2.851563-4.96875-2.8125-11.085938.105468-16.019532h27.566406c2.917969 4.933594 2.957032 11.050782.101563 16.019532-2.851563 4.96875-8.15625 8.015625-13.886719 7.980468zm0 0" fill="#dedede"></path><path d="m8.066406 388.96875c0-52.929688 56-76.929688 56-76.929688v-32h176c0 24-16 24-16 24h-4c-19.882812 0-36 16.121094-36 36 0 19.882813 16.117188 36 36 36h82.710938c5.746094.003907 11.050781-3.074218 13.898437-8.0625l27.390625-47.9375-8-88c-1.328125-19.613281 12.570313-36.988281 32-40l12.554688 99.304688c2.09375 12.566406 11.441406 22.695312 23.796875 25.785156l11.648437 2.910156v24h-16c-26.507812 0-48 21.492188-48 48v16h-344zm0 0" fill="#f05d46"></path><path d="m400.066406 344.039062h32c22.09375 0 40 17.910157 40 40h-36c-12.980468 0-25.613281 4.210938-36 12-9.09375 6.742188-19.910156 10.769532-31.199218 11.617188-1.601563.128906-3.199219.382812-4.800782.382812h-12v-16c0-26.507812 21.492188-48 48-48zm0 0" fill="#f05d46"></path><path d="m360.066406 408.039062h-16v-16c.035156-30.910156 25.085938-55.964843 56-56h16v16h-16c-22.078125.027344-39.972656 17.921876-40 40zm0 0" fill="#d64d37"></path><path d="m240.066406 280.039062h-88v-16c0-8.835937 7.164063-16 16-16h56c8.835938 0 16 7.164063 16 16zm0 0" fill="#734730"></path><path d="m336.066406 408.039062c-4.703125 18.808594-21.597656 32-40.984375 32h-86.03125c-19.382812 0-36.28125-13.191406-40.984375-32zm0 0" fill="#dedede"></path><path d="m304.066406 168.039062v16h40c4.417969 0 8-3.578124 8-8 0-4.417968-3.582031-8-8-8zm0 0" fill="#8c563b"></path><path d="m120.066406 288.039062h-56c-.742187.003907-1.484375-.101562-2.199218-.308593l-49.382813-14.089844c-8.246094-2.320313-13.507813-10.371094-12.320313-18.855469 1.1875-8.480468 8.460938-14.777344 17.023438-14.746094h102.878906c4.417969 0 8 3.582032 8 8v32c0 4.421876-3.582031 8-8 8zm-54.878906-16h46.878906v-16h-94.878906c-.5625-.003906-1.039062.410157-1.117188.96875-.078124.554688.269532 1.085938.8125 1.234376zm0 0" fill="#dedede"></path><path d="m24.066406 144.039062h88c8.835938 0 16 7.164063 16 16v80h-120v-80c0-8.835937 7.164063-16 16-16zm0 0" fill="#f05d46"></path><path d="m8.066406 168.039062h120v16h-120zm0 0" fill="#d64d37"></path><path d="m152.066406 216.039062c0 17.675782 14.328125 32 32 32h88l-32 96h32l34.914063-90.765624c3.1875-8.300782 2.152343-17.632813-2.777344-25.03125-5.082031-7.625-13.636719-12.203126-22.800781-12.203126zm0 0" fill="#8c563b"></path><path d="m304.066406 376.039062h-72l8-32h32c17.675782 0 32 14.328126 32 32zm0 0" fill="#dedede"></path><path d="m152.066406 96.039062h64v120h-64zm0 0" fill="#d64d37"></path><path d="m216.066406 32.039062h-64v64h64v-32h16l-16-24zm0 0" fill="#fec9a3"></path><path d="m208.066406 152.039062-12.328125-41.300781c-2.597656-10.398437-13.136719-16.71875-23.53125-14.117187-10.394531 2.601562-16.714843 13.136718-14.117187 23.53125l13.433594 45.710937c2.671874 10.683594 12.269531 18.175781 23.28125 18.175781h93.261718v-32zm0 0" fill="#f05d46"></path><path d="m304.066406 152.039062h-16v32h16c8.835938 0 16-7.160156 16-16 0-8.835937-7.164062-16-16-16zm0 0" fill="#fec9a3"></path><path d="m128.066406 8.039062h-17.503906c-4.269531 0-8.484375.996094-12.304688 2.90625-6.523437 3.261719-14.078124 3.800782-21 1.496094l-13.191406-4.402344v48l13.191406 4.402344c6.921876 2.304688 14.476563 1.765625 21-1.496094 3.820313-1.910156 8.035157-2.90625 12.304688-2.90625h17.503906zm0 0" fill="#f05d46"></path><path d="m64.066406 312.039062h-48v44.167969c11.054688-19.328125 27.820313-34.757812 48-44.167969zm0 0" fill="#c44639"></path><g fill="#d64d37"><path d="m144.066406 304.039062h-88c-4.417968 0-8 3.582032-8 8 0 4.421876 3.582032 8 8 8h88c4.417969 0 8-3.578124 8-8 0-4.417968-3.582031-8-8-8zm0 0"></path><path d="m104.066406 336.039062h48v16h-48zm0 0"></path><path d="m104.066406 368.039062h48v16h-48zm0 0"></path></g><path d="m214.9375 24.039062c-3.996094-15.601562-18.988281-25.78125-34.964844-23.75-15.976562 2.03125-27.941406 15.644532-27.90625 31.75v8h96v-16zm0 0" fill="#f05d46"></path></svg></span>
                                          <span><svg id="gift" class="m-r-5" viewBox="0 0 57 57" width="20px" height="20px"><title>gift</title><rect x="1" y="13.002" style="fill:#CB465F;" width="55" height="12"></rect><rect x="6" y="25.002" style="fill:#EF4D4D;" width="46" height="30"></rect><path style="fill:#EBBA16;" d="M56,12.002H41.741C42.556,10.837,43,9.453,43,7.995c0-1.875-0.726-3.633-2.043-4.95 c-2.729-2.729-7.17-2.729-9.899,0l-2.829,2.829l-2.828-2.829c-2.729-2.729-7.17-2.729-9.899,0c-1.317,1.317-2.043,3.075-2.043,4.95 c0,1.458,0.444,2.842,1.259,4.007H1c-0.552,0-1,0.447-1,1s0.448,1,1,1h27v24H6c-0.552,0-1,0.447-1,1s0.448,1,1,1h22v15 c0,0.553,0.448,1,1,1s1-0.447,1-1v-15h22c0.552,0,1-0.447,1-1s-0.448-1-1-1H30v-24h26c0.552,0,1-0.447,1-1S56.552,12.002,56,12.002z M32.472,4.459c1.95-1.949,5.122-1.949,7.071,0C40.482,5.399,41,6.654,41,7.995c0,1.34-0.518,2.596-1.457,3.535l-0.472,0.472H24.929 l4.006-4.006l0.001-0.001l0.001-0.001L32.472,4.459z M16.916,11.53c-0.939-0.939-1.457-2.195-1.457-3.535 c0-1.341,0.518-2.596,1.457-3.536c1.95-1.949,5.122-1.949,7.071,0l2.828,2.829l-3.536,3.536c-0.331,0.331-0.622,0.735-0.898,1.179 h-4.994L16.916,11.53z"></path></svg></span>
                                          <span><svg id="circle" class="m-r-5" viewBox="0 0 512 512" width="10px" height="10px"><title>circle</title><path d="M256,0C114.837,0,0,114.837,0,256s114.837,256,256,256s256-114.837,256-256S397.163,0,256,0z" fill="#4ACD1F"></path></svg></span>
                                        </div>
                                    </li>
                                    
                                    
                                 
                                  </ul>--%>
                             </div>
                             <style>
                               li.bb a:hover{
                                 color: #000;
                                 text-decoration: none;
                               }
                             </style>
                             <div class="card-header" style="padding:5px 5px">

                                <div class="sub-title p-b-5 headertext" >รายการสินค้าชื่นชอบ
                                  </div>
                                </div>
                                       <div class="card-block">
                                           <ul style="list-style-type: none;  padding: 0px ">
                                           <li class="bb">                                                                                   
                                           <asp:LinkButton ID="LinkBtnProductTop01" runat="server" OnClick="AddtocartProductTop01" >
                                           <span class="m-r-20"><i runat="server" id="ico01" class="fa fa-plus button-activity"></i></span>
                                           <asp:Label ID="lblProductNameTop1" runat="server"></asp:Label>
                                           </asp:LinkButton></li>
                                           <li class="bb"> <asp:Label ID="lblProdTop01" runat="server"></asp:Label></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop01" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop01" runat="server" />
                                           <li class="bb">  <asp:LinkButton ID="LinkBtnProductTop02" runat="server" OnClick="AddtocartProductTop02" > 
                                           <span class="m-r-20"><i runat="server" id="ico02"   class="fa fa-plus button-activity"></i></span>
                                           <asp:Label ID="lblProductNameTop2" runat="server"></asp:Label></asp:LinkButton></li>
                                           <li class="bb"> <asp:Label ID="lblProdTop02" runat="server"></asp:Label></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop02" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop02" runat="server" />
                                           <li class="bb">  <asp:LinkButton ID="LinkBtnProductTop03" runat="server" OnClick="AddtocartProductTop03" > 
                                           <span class="m-r-20"><i runat="server" id="ico03"   class="fa fa-plus button-activity"></i></span>
                                           <asp:Label ID="lblProductNameTop3" runat="server"></asp:Label></asp:LinkButton></li>
                                           <li class="bb"> <asp:Label ID="lblProdTop03" runat="server"></asp:Label></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop03" runat="server" /> 
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop03" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop03" runat="server" />
                                           <li class="bb">  <asp:LinkButton ID="LinkBtnProductTop04" runat="server" OnClick="AddtocartProductTop04" > 
                                           <span class="m-r-20"><i  runat="server" id="ico04"  class="fa fa-plus button-activity"></i></span>
                                           <asp:Label ID="lblProductNameTop4" runat="server"></asp:Label></asp:LinkButton></li>
                                           <li class="bb"> <asp:Label ID="lblProdTop04" runat="server"></asp:Label></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop04" runat="server" />   
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop04" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop04" runat="server" />
                                           <li class="bb">  <asp:LinkButton ID="LinkBtnProductTop05" runat="server" OnClick="AddtocartProductTop05" > 
                                           <span class="m-r-20"><i  runat="server" id="ico05"  class="fa fa-plus button-activity"></i></span>
                                           <asp:Label ID="lblProductNameTop5" runat="server"></asp:Label></asp:LinkButton></li>  
                                           <li class="bb"> <asp:Label ID="lblProdTop05" runat="server"></asp:Label></li>
                                           <asp:HiddenField ID="hidPromotionDetailIdProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidProductNameProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidProductCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidDiscountAmountProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidDiscountPercentProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidPromotionCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidPriceProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryCodeProductTop05" runat="server" />
                                           <asp:HiddenField ID="hidCampaignCategoryNameProductTop05" runat="server" />
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
      </div>

      </div>
              </div>
                  </div>
              </div>
          </div>
      
         
              
          </div>
        <script>

                function displayPayment() {
                
                    if ($('#1').css('display') != 'none') {
                        document.getElementById("<%=hidpagedisplay.ClientID%>").value = "initial";
                        var covercheckout = document.getElementById('<%=covercheckout.ClientID%>');
                        covercheckout.style.display = 'none';
                    $('#2').show().siblings('div').hide();
                    }else if($('#2').css('display')!='none'){
                        $('#1').show().siblings('div').hide();
                    }
                    document.getElementById("btncheckout").disabled = true;
                    
            }

            function displaySelect() {
                
                if ($('#2').css('display') != 'none') {
                    document.getElementById("<%=hidpagedisplay.ClientID%>").value = "";
                    var covercheckout = document.getElementById('<%=covercheckout.ClientID%>');
                        covercheckout.style.display = 'block';
                    $('#1').show().siblings('div').hide();
                } else if ($('#1').css('display') != 'none') {
                        $('#2').show().siblings('div').hide();
                }
                document.getElementById("btncheckout").disabled = false;
                document.getElementById("<%=hidpagedisplay.ClientID%>").value = "";
            }
            function displayNonSelected() {
                if (document.getElementById("<%=hidpagedisplay.ClientID%>").value == "") {
                    $('#1').show().siblings('div').hide();
                } else if (document.getElementById("<%=hidpagedisplay.ClientID%>").value == "initial") {
                        var covercheckout = document.getElementById('<%=covercheckout.ClientID%>');
                        covercheckout.style.display = 'none';
                        $('#2').show().siblings('div').hide();
                }
                document.getElementById("btncheckout").disabled = false;
            }
    </script>

               


         

                      </span></span>

               


         

    </ContentTemplate>
 </asp:UpdatePanel>

        <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
            aria-hidden="true" id="modal-productset">
            <div class="modal-dialog modal-lg" style="max-width:1000px;">
               
                <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                    <div class="modal-header modal-header2 ">
                        <div class="col-sm-11">
                        <div id="exampleModalLongTitle">Select Product in Comboset
                        </div>
                        </div>
                        <div class="col-sm-1">
                        <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                    </div>
                    </div>
                </div>
           
                <div class="modal-body">
                      <asp:UpdatePanel ID="UpModal" runat="server">
                        <ContentTemplate>
        <div class="card-block">  
                 <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Comboset Code</label>
                            <div class="col-sm-3">
                                <asp:Label ID="lblCombosetCode" runat="server"></asp:Label>
                              <asp:HiddenField ID="hidCombosetPromotionCode" runat="server" />
                              <asp:HiddenField ID="hidCombosetCampaignCode" runat="server" />
                            </div>
                            <label class="col-sm-1 col-form-label"></label>
                            <label class="col-sm-2 col-form-label">Comboset Name</label>
                            <div class="col-sm-3">

                                <asp:Label ID="lblCombosetName" runat="server" ></asp:Label> 
                                  
                            </div>

                      <label class="col-sm-2 col-form-label">Price</label>
                            <div class="col-sm-3">
                                <asp:Label ID="lblCombosetPrice" runat="server" CssClass="validatecolor"></asp:Label>
                              
                            </div>
                        </div>
        </div>
         <div class="card-block">  


            Main 
           

                            <asp:GridView ID="gvSubMainPromotionDetail" runat="server"
                                AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                            TabIndex="0" Width="100%" CellSpacing="0"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkSubMainPromotionDetailAll" checked="true"  OnCheckedChanged="chkSubMainPromotionDetailAll_Check"   AutoPostBack="true"
                                                runat="server"  ></asp:CheckBox>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkSubMainPromotionDetail"  checked="true"  runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Product Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>                                       
                                        &nbsp;&nbsp;<asp:Label ID="lblMainProductName" Text='<%# DataBinder.Eval(Container.DataItem, "MainProductName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Unit</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblUNITName" Text='<%# DataBinder.Eval(Container.DataItem, "UNITName")%>' runat="server" />
                                       <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# Eval("PromotionDetailId") %>' />
                                                                             
                                                                               <asp:HiddenField ID="hidCampaignCategoryCode" runat="server" value='<%# Eval("CampaignCategoryCode") %>' />
                                                                               <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                                <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                              <asp:HiddenField ID="hidProductCode" runat="server" value='<%# Eval("MainProductCode") %>' />
                                                                               <asp:HiddenField ID="hidProductName" runat="server" value='<%# Eval("MainProductName") %>' />
                                                                               <asp:HiddenField ID="hidPrice" runat="server" value='<%# Eval("Price") %>' />
                                                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>    

             <br /> Exchange               <br />
  
                         <asp:GridView ID="gvSubExchangePromotionDetail" runat="server"
                             AutoGenerateColumns="False"
                             CssClass="table-p
                          table-striped table-bordered nowrap"
                            TabIndex="0" Width="100%" CellSpacing="0"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate> 
                                        <center>
                                            <asp:CheckBox ID="chkSubExchangePromotionDetailAll" OnCheckedChanged="chkSubExchangePromotionDetailAll_Check" AutoPostBack="true" 
                                                runat="server"  ></asp:CheckBox>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkSubExchangePromotionDetail" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Product Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>                                       
                                        &nbsp;&nbsp;<asp:Label ID="lblExchangeProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ExchangeProductName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Unit</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblUNITName" Text='<%# DataBinder.Eval(Container.DataItem, "UNITName")%>' runat="server" />
                                    <asp:HiddenField ID="hidPromotionDetailId" runat="server" value='<%# Eval("PromotionDetailId") %>' />
                                                                             
                                                                               <asp:HiddenField ID="hidCampaignCategoryCode" runat="server" value='<%# Eval("CampaignCategoryCode") %>' />
                                                                               <asp:HiddenField ID="hidCampaignCode" runat="server" value='<%# Eval("CampaignCode") %>' />
                                                                                <asp:HiddenField ID="hidPromotionCode" runat="server" value='<%# Eval("PromotionCode") %>' />
                                                                              <asp:HiddenField ID="hidProductCode" runat="server" value='<%# Eval("ExchangeProductCode") %>' />
                                                                               <asp:HiddenField ID="hidProductName" runat="server" value='<%# Eval("ExchangeProductName") %>' />
                                                                               <asp:HiddenField ID="hidPrice" runat="server" value='<%# Eval("Price") %>' />
                                                                        
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
                      </ContentTemplate>
                     </asp:UpdatePanel>
                </div>
      
            <div align="center">
                        
                             <asp:Button ID="btnSelectComboset" Text="Submit" OnClick="btnSelectComboset_Click"
                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                runat="server" />
                                <asp:Button ID="btnCancelComboset" Text="Cancel" OnClick="btnCancelComboset_Click"
                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                runat="server" />
                   </div>
                          

                 
             </div>
          </div>
            </div>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-customer">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                      <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle1">Edit Customer
                                </div>
                              </div>
                              <div class="col-sm-1">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                              </div>
                            </div>
                          </div>
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
                                        <asp:Label ID="lblCampaignName_Ins" runat="server" CssClass="validation"></asp:Label>
                                
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

    <script type="text/javascript">
        $(function () {
            // Declare a proxy to reference the hub.
            var chat = $.connection.myChatHub;
       
               // Create a function that the hub can call to broadcast messages.
            //chat.client.broadcasttest = function (name) {
            //         alert("New Messege from " + name);

           // };

        });
    </script>

    </asp:Content>


 
     
     

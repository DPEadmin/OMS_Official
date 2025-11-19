<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="MerchantDetail.aspx.cs" Inherits="DOMS_TSR.src.MerchantManagement.MerchantDetail" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .validation {
            color:red
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>

<div class="page-body">
  <div class="col-sm-12">
                      <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />   
            <asp:HiddenField ID="hidSupplierIdIns" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidMerCode" runat="server"/>
      <!-- Basic Form Inputs card start -->
      <div class="card">
          <div class="card-header">
            <div class="sub-title" >Merchant information</div>
            </div>
            <div class="card-block">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
               <style>
                     .form-controls {
    padding: .375rem .75rem;
}
               </style>         

                             
                                <div class="form-group row">
      
                                  <label class="col-sm-2 col-form-label">Merchant  code</label>
                                  <div class="col-sm-4 form-controls  ">
                                        <asp:HiddenField ID="hidMerIdIns" runat="server"></asp:HiddenField>                                   
                                     
                                      <asp:Label ID="lblMerCodeIns" runat="server" ForeColor="#6c757d"  CssClass="validation"></asp:Label>                              
                                  </div>
                                  <%--
                                  <label class="col-sm-2 col-form-label">Company Code</label>
                                  <div class="col-sm-4 form-controls  ">
                                      <asp:Label ID="lblComCodeIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                
                                  </div>
                                  
                                  <label class="col-sm-2 col-form-label">Merchant Type</label>
                                  <div class="col-sm-4 form-controls  ">
                                      <asp:Label ID="lblMerTypeIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                
                                  </div>--%>
                                   
                                  <label class="col-sm-2 col-form-label">Merchant  name</label>
                                  <div class="col-sm-4 form-controls  ">                                                                     
                                        <asp:Label ID="lblMerNameIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>     
                                  </div>
                                    
                                  <label class="col-sm-2 col-form-label">Tax ID</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblTaxIdIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                   
                                   </div>
                                  <%--
                                  <label class="col-sm-2 col-form-label">ที่อยู่</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblAddressIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                   
                                   </div>
                                   
                                  <label class="col-sm-2 col-form-label">จังหวัด</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblProvinceIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                   
                                   </div>
                                  
                                  <label class="col-sm-2 col-form-label">เขต/อำเภอ</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblDistricIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                   
                                   </div>
                                   
                                  <label class="col-sm-2 col-form-label">แขวง/ตำบล</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblSubDistrictIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                   
                                   </div>  
                                     
                                  <label class="col-sm-2 col-form-label">รหัสไปรษณีย์</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblZipCodeIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                   
                                   </div>--%>
                                     
                                  <label class="col-sm-2 col-form-label">Fax</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblFaxIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                   
                                   </div>    
                                  <label class="col-sm-2 col-form-label">telephone number</label>
                                  <div class="col-sm-4 form-controls  ">
                                       <asp:Label ID="lblMobileIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                             
                                  </div>
                                  
                                  <label class="col-sm-2 col-form-label">Email</label>
                                  <div class="col-sm-4 form-controls  ">
                                     <asp:Label ID="lblEmailIns" runat="server" ForeColor="#6c757d" CssClass="validation"></asp:Label>                    
                                  </div>
                            
                            </div>
                            <div class="text-center m-t-20 col-sm-12">
                           <a class=" button-pri button-accept m-r-10 " style="color: #fff;" href="MerchantManagement.aspx"> กลับ</a> 
                          </div>
                          </div>
                   
                     </ContentTemplate>
                </asp:UpdatePanel>
            </div>
      </div>
      </div>
    </div>
</asp:Content>

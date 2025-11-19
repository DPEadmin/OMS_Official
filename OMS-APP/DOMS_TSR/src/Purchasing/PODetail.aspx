<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="PODetail.aspx.cs" Inherits="DOMS_TSR.src.Purchasing.PODetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>
    
<div class="page-body"> 
  <style> .section-headersection-header{ margin-left: -30px;
    margin-right: -30px;
    margin-top: -10px;
    border-radius: 0;
    border-top: 1px solid #f9f9f9;
    padding-left: 35px;
    padding-right: 35px;}
       
    </style>
    <section style="    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.03);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.03);
    background-color: #fff;
    border-radius: 3px;
    border: none;
    position: relative;
    margin-bottom: 20px;
    padding: 20px;
     ">
    <div class="section-headersection-header d-flex  ">
        <h1>Create PO</h1>
        <div class="d-flex ml-auto">
          <div class="breadcrumb-item active"><a href="#">Back</a></div> 
          <div class="breadcrumb-item"></div>
        </div>
      </div>
    </section>
      <div class="card">
          <div class="card-block block-header ml-set"> 
              <div class="form-group row  ">
                <label class="col-sm-2 col-form-label">เลขที่ใบสั่งซื้อ</label>
                <div class="col-sm-4">
                  <asp:TextBox ID="txtOrderCode" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-2 col-form-label">วันที่</label>
                <div class="col-sm-4">
                  <asp:TextBox ID="txtPODate" class="form-control" ReadOnly="true" placeholder="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                </div> 
              </div> 
          </div>
       
<div class="card-block">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
               <ContentTemplate>
        <div class="row">
            <div class="col-md-6 d-flex">
              <div class="card">
                <div class="card-block">
                    <div class="form-group row">
                        
                        <asp:HiddenField ID="hidEmpCode" runat="server" />      
                        <label class="col-sm-3 col-form-label">ชื่อผู้ขาย</label>
                        <div class="col-sm-6"> 
                            <div class="input-group"> 
                                <asp:TextBox  ID="txtSupplierName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                
                              </div>
                        </div>
                        <label class="col-sm-3 col-form-label"></label>
                        <label class="col-sm-3 col-form-label">หมายเลขประจำตัวผู้เสียภาษี</label>
                        <div class="col-sm-6">
                            <asp:TextBox  ID="txtSupplierTaxIdNo" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>                          
                        </div>
                        <label class="col-sm-3 col-form-label"></label>
                        <label class="col-sm-3 col-form-label">ที่อยู่</label>
                        <div class="col-sm-9">
                            <asp:TextBox  TextMode="multiline" ReadOnly="true" Columns="50" Rows="5" ID="txtSupplierAddress" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-3 col-form-label">ผู้ติดต่อ</label>
                        <div class="col-sm-6">
                            <asp:TextBox  ID="txtSupplierContactor" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>                          
                        </div>

                        <label class="col-sm-3 col-form-label"></label>
                        <label class="col-sm-3 col-form-label">เบอร์โทรศัพท์</label>
                        <div class="col-sm-3">
                            <asp:TextBox  ID="txtSupplierPhoneNumber" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-sm-2 col-form-label">แฟกซ์</label>
                        <div class="col-sm-4">
                            <asp:TextBox  ID="txtSupplierFaxNumber" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-sm-3 col-form-label">อีเมล์</label>
                        <div class="col-sm-6">
                            <asp:TextBox  ID="txtSupplierMail" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-sm-3 col-form-label"></label>
                    </div>
                </div>
              </div>
            </div>
            <div class="col-md-6">
                <div class="card ">
                    <div class="card-block"> 
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">วันที่คาดว่าจะส่งสินค้า</label>
                            <div class="col-sm-6"> 
                                <div class="input-group">
                                    <asp:TextBox ID="txtPOExpectDate" class="form-control" ReadOnly="true" placeholder="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                  </div>
                            </div>
                            <label class="col-sm-3 col-form-label"></label>
                            <label class="col-sm-3 col-form-label">วิธีชำระเงิน</label>
                            <div class="col-sm-6">
                                <asp:TextBox  ID="lblPaymentMethod" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <label class="col-sm-3 col-form-label"></label>
                            <label class="col-sm-3 col-form-label">เครดิต(วัน)</label>
                            <div class="col-sm-6">
                                
                                <div class="input-group"> 
                                    <asp:TextBox  ID="txtPOCredit" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    <div class="input-group-prepend">
                                      </div>
                                  </div>
                            </div>
                            <label class="col-sm-3 col-form-label"></label>
                            <label class="col-sm-3 col-form-label">สถานที่จัดส่งสินค้า</label>
                            <div class="col-sm-9">
                                <div class="input-group"> 
                                    
                                <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtInventoryName" runat="server"></asp:TextBox>
                                  </div>

                                <asp:TextBox  TextMode="multiline" Columns="50" Rows="5" ID="txtInventoryAddress" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                            </div>
                             
                            <label class="col-sm-3 col-form-label">เบอร์โทรศัพท์</label>
                            <div class="col-sm-3">
                                <asp:TextBox  ID="txtInventoryContactTel" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 col-form-label">แฟกซ์</label>
                            <div class="col-sm-4">
                                <asp:TextBox  ID="txtInventoryFax" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                            </div>
                             
                            <label class="col-sm-3 col-form-label">หมายเหตุ</label>
                            <div class="col-sm-9">
                                <asp:TextBox  TextMode="multiline" Columns="50" Rows="5" ID="txtPODescription" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                  </div>
                </div>
            </div>
            
        </div>
                        
                     
                   </ContentTemplate>
    </asp:UpdatePanel>
      </div>
    </div>

    <div class="card">
        <div class="card-block">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
               <ContentTemplate>
                        
                       <div class="card">
                            <div class="card-header">
                            </div>
                            <div class="card-block">   
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                                     <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                                    <Columns>
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="10%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">รหัสสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ชื่อสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">จำนวน</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>                                         
                                                                 <asp:Label ID="lbLAmount" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">หน่วย</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ราคาสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblPrice" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ส่วนลด(บาท)</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblDiscountAmount" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount", "{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ส่วนลด(%)</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblDiscountPercent" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent", "{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ราคารวม(บาท)</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblSumPrice" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "TotPrice", "{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                        

                                                    </Columns>

                                                    <EmptyDataTemplate>
                                                        <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>  
                        </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>
                     
                     
               </ContentTemplate>
    </asp:UpdatePanel>       
                       
        </div>
       
        <div class="card-block block-header ml-auto">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
               <ContentTemplate>
            <div>
                <div class="form-group row  ">
                    <label class="col-sm-6 col-form-label">รวม</label>
                    <div class="col-sm-6">
                        <asp:TextBox  ID="totaltext" style="text-align: right" ReadOnly="true" class="form-control" runat="server"></asp:TextBox> 
                        </div>
                   
              
             </div>
            </div>
             <div>
                <div class="form-group row  "> 
                    <label class="col-sm-3 col-form-label">ส่วนลดบิล</label>
                    <label class="col-sm-3 col-form-label p-b-0 p-t-0" >  <span></span></label>
                  
                    <div class="col-sm-6">
                        <asp:TextBox  ID="txtBillDiscount" ReadOnly="true" style="text-align: right" class="form-control" runat="server"></asp:TextBox>
                        </div> 
             </div>
            </div> 
            <div>
                <div class="form-group row  "> 
                    <label class="col-sm-6 col-form-label">vat 7%</label>
                    <div class="col-sm-6 d-flex">
                      
                        <asp:TextBox ID="txtvat" ReadOnly="true" style="text-align: right" class="form-control" runat="server"></asp:TextBox>
                        </div> 
             </div>
            </div> 
            <div>
                <div class="form-group row  "> 
                    <label class="col-sm-6 col-form-label">รวมทั้งหมด</label>
                    <div class="col-sm-6">
                        <asp:TextBox  ID="txtTotalPrice" style="text-align: right" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                        </div> 
             </div>
            </div> 
                </ContentTemplate>
             </asp:UpdatePanel>

          </div> 
          <div class="card-block">
          <div class="text-center col-md-12  ">              
              <asp:Button ID="btnApprove" runat="server" Text="Approve" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnRevise" runat="server" Text="Send back to revise" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnReject" runat="server" Text="Reject" class="button-pri button-accept m-r-5" />
            </div>
          </div>
        </div>
  
      </div>
          

     
</asp:Content>
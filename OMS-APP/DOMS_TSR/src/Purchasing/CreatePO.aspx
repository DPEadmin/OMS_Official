<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="CreatePO.aspx.cs" Inherits="DOMS_TSR.src.Purchasing.CreatePO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    function DeleteConfirm() {
       <%-- var grid = document.getElementById("<%= gvSupplier.ClientID %>");--%>

        var cell;
        var sum = 0;
        if (grid.rows.length > 0) {
            //alert("length=" + grid.rows.length);
            //loop starts from 1. rows[0] points to the header.
            for (i = 1; i < grid.rows.length; i++) {
                //get the reference of first column
                cell = grid.rows[i].cells[0];
                // alert("cell=" + cell);
                //alert("cell childNodes.length=" + cell.childNodes.length);
                //loop according to the number of childNodes in the cell
                for (j = 0; j < cell.childNodes.length; j++) {
                    //alert("type=" + cell.childNodes[j].type);
                    //alert("checked=" + cell.childNodes[j].checked);
                    //if childNode type is CheckBox
                    if (cell.childNodes[j].type == "checkbox") {
                        if (cell.childNodes[j].checked == true) {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            //cell.childNodes[j].checked = document.getElementById(id).checked;
                            sum++;
                            //alert("checked=" + cell.childNodes[j].checked);
                        }
                    }
                }
            }
        }

        //  alert("sum=" + sum);

        if (sum == 0) {

            alert("กรุณาเลือกรายการที่จะลบ");

            return false;

        } else {

                //var MsgDelete = document.getElementById("<%=hidMsgDel.ClientID%>").value;
                var MsgDelete = "คุณแน่ใจที่จะลบข้อมูลนี้ ?";

                if (confirm(MsgDelete)) {
                    //alert("c");
                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "TRUE";

                    return true;

                } else {

                    document.getElementById("<%=hidFlagDel.ClientID%>").value = "FALSE";

                return false;
            }
        }
    }
</script>
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
        <h1><asp:Label ID="lblHeadPOStatus" runat="server"></asp:Label></h1>
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
                  <asp:TextBox ID="txtPOCode" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-2 col-form-label">วันที่</label>
                <div class="col-sm-4">
                  <asp:TextBox ID="txtPODate" class="form-control" placeholder="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPODate" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                  <asp:Label ID="lblPODate" runat="server" CssClass="validatecolor"></asp:Label>
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
                        

                        <label class="col-sm-3 col-form-label">ชื่อผู้ขาย</label>
                        <div class="col-sm-6"> 
                            <div class="input-group"> 
                                <asp:TextBox  ID="txtSupplierName" class="form-control" runat="server"></asp:TextBox>                                
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                    <asp:LinkButton runat="server" ID="SupplierName" OnClick="SelectSupplierName"> <i class="fas fa-search"></i></asp:LinkButton>                                    
                                    </div>                                    
                                  </div>
                                &nbsp;<asp:Label ID="lblSupplierNameIns" runat="server" CssClass="validatecolor"  Style="color:red;"></asp:Label>
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
                                    <asp:TextBox ID="txtPOExpectDate" class="form-control" placeholder="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="carSearchEndDateFrom" runat="server" TargetControlID="txtPOExpectDate" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>   
                                    &nbsp;<asp:Label ID="lblPOExpectDate" runat="server" CssClass="validatecolor"  Style="color:red;"></asp:Label>
                                  </div>                                  
                            </div>
                            <label class="col-sm-3 col-form-label"></label>
                            <label class="col-sm-3 col-form-label">วิธีชำระเงิน</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlPOPaymentType" runat="server" class="form-control"></asp:DropDownList> 
                                <asp:TextBox  ID="txtPOPaymentType" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                &nbsp;<asp:Label ID="lblPOPaymentType" runat="server" CssClass="validatecolor"  Style="color:red;"></asp:Label>
                            </div>
                            <label class="col-sm-3 col-form-label"></label>
                            <label class="col-sm-3 col-form-label">เครดิต(วัน)</label>
                            <div class="col-sm-6">
                                
                                <div class="input-group"> 
                                    <asp:TextBox  ID="txtPOCredit" class="form-control" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblPOCredit" runat="server" CssClass="validatecolor"  Style="color:red;"></asp:Label>
                                    <div class="input-group-prepend">
                                      </div>
                                  </div>
                            </div>
                            <label class="col-sm-3 col-form-label"></label>
                            <label class="col-sm-3 col-form-label">สถานที่จัดส่งสินค้า</label>
                            <div class="col-sm-9">
                                <div class="input-group"> 
                                    
                                <asp:TextBox CssClass="form-control" ID="txtInventoryName" runat="server"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <div class="input-group-text">
                                          <asp:LinkButton runat="server" ID="linkbtnSelectInventory" OnClick="SelectInventoryName"><i class="fas fa-search"></i></asp:LinkButton>
                                        </div>
                                      </div>
                                    &nbsp;<asp:Label ID="lblInventoryName" runat="server" CssClass="validatecolor"  Style="color:red;"></asp:Label>
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
                                <asp:TextBox  TextMode="multiline" Columns="50" Rows="5" ID="txtPODescription" class="form-control" runat="server"></asp:TextBox>
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

            <div class="m-b-10">

                       <asp:Button ID="btnSelectProduct" OnClick="btnSelectProduct_Click" Text="เลือกสินค้า" class="button-pri button-add m-r-5" runat="server" />
                       <asp:Button ID="btnShowProduct" class="button-pri button-add m-r-5" Text="รายการสินค้าของ PO" runat="server" />
                       </div>
                        
                       <div class="card">
                            <div class="card-header">
                            </div>
                            <div class="card-block">   
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                                     <asp:GridView ID="gvProductSelected" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowDataBound="gvProductSelected_RowDataBound" OnRowCommand="gvProductSelected_RowCommand">
                                                    <Columns>
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="10%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">รหัสสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductCodeSelected" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ชื่อสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblProductNameSelected" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">จำนวน</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:TextBox ID="txtAmount" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true" runat="server" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>'></asp:TextBox>
                                                                 <asp:Label ID="lblAmount" runat="server" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>'></asp:Label>
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
                                                                 <asp:TextBox ID="txtPrice" runat="server" style="text-align: right" OnTextChanged="txtPrice_TextChanged" AutoPostBack="true" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:n}")%>'></asp:TextBox>
                                                                 <asp:Label ID="lblPrice" runat="server" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:n}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ส่วนลด(บาท)</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:TextBox ID="txtDiscountAmount" runat="server" style="text-align: right" OnTextChanged="txtDiscountAmount_Textchanged" AutoPostBack="true" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount", "{0:n}")%>'></asp:TextBox>
                                                                 <asp:Label ID="lblDiscountAmount" runat="server" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount", "{0:n}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ส่วนลด(%)</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:TextBox ID="txtDiscountPercent" runat="server" style="text-align: right" OnTextChanged="txtDiscountPercent_TextChanged" AutoPostBack="true" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>'></asp:TextBox>
                                                                 <asp:Label ID="lblDiscountPercent" runat="server" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ราคารวม(บาท)</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblSumPrice" style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "SumPrice", "{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left"></div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:LinkButton ID="buttonDelete" runat="Server" OnClientClick="return DeleteConfirm();" CommandName="DeleteProduct"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="button-activity  " >  <span class="ti-trash f-14"></span></asp:LinkButton>

                                                                 <asp:HiddenField runat="server" ID="hidRunningNo" Value='<%# DataBinder.Eval(Container.DataItem, "RunningNo")%>' />
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
                        <asp:TextBox  ID="txtBillDiscount" OnTextChanged="txtBillDiscount_TextChanged" AutoPostBack="true" style="text-align: right" class="form-control" runat="server"></asp:TextBox>
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
              <asp:Button ID="btnSaveDraft" runat="server" OnClick="btnSaveDraft_Click" Text="SaveDraft" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnSubmitByRequestor" runat="server" OnClick="btnSubmitByRequestor_Click" Text="Save and Send to Approval" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnRevise" runat="server" OnClick="btnRevise_Click" Text="Revise" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" Text="Reject" class="button-pri button-accept m-r-5" />
            </div>
          </div>
        </div>

          <div class="card">
        <div class="card-block" style="display:none"><asp:UpdatePanel ID="UpdatePanel3" runat="server">
               <ContentTemplate>
                

               
            <div class="form-group row">
              <label class="col-sm-2 col-form-label">รหัสผู้ผลิต</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchSupplierCodeModal" class="form-control" runat="server"></asp:TextBox>
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">ชื่อผู้ผลิต</label>
              <div class="col-sm-3">                 
                  <asp:TextBox  ID="txtSearchSupplierNameModal" class="form-control" runat="server"></asp:TextBox>
                      <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <input type="hidden" id="hidApprover1" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidPOIdUpd" runat="server" />
            <asp:HiddenField ID="hidSupplierIdIns" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidwfstatus" runat="server" />
            <asp:HiddenField ID="hidwftaskliststatus" runat="server" />
            <asp:HiddenField ID="hidSupplierCodeIns" runat="server" />
            <asp:HiddenField ID="hidInventoryCodeIns" runat="server" />
              </div>

                <label class="col-sm-2 col-form-label">สถานะ</label>
              <div class="col-sm-3">
                  <asp:DropDownList ID="ddlSearchStatus" class="form-control" runat="server">
                                        <asp:ListItem Enabled="true" Text="Please select status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                  </asp:DropDownList>
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label"></label>
              <div class="col-sm-3">
                  
              </div>
                
            </div>           
              <div class="text-center m-t-20 col-sm-12">
                  <asp:Button ID="btnSearch" Text="Search" CssClass="button-active button-submit m-r-10" OnClick="btnSearch_Click" runat="server"/>
                  <asp:Button ID="btnClearSearch" Text="Clear" CssClass="button-active button-cancle" OnClick="btnClearSearch_Click" runat="server" />   
              </div>        
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
      
          </div>
      </div>
          
      <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-addproduct">
           <div class="modal-dialog modal-lg" style="max-width:70%;">
        
                 <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle3"> เลือกสินค้า
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
                              <div class="card">

                                   <div class="card-header">
                                        <div class="sub-title">ค้นหาข้อมูลสินค้า</div>
                                        </div>
                                        <div class="card-block">  
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                                <div class="form-group row">
                                    
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearchProduct" OnClick="btnSearchProduct_Click" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearchProduct" OnClick="btnClearSearchProduct_Click" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                                        </div>                                        
                             </div>

                              <div class="card">
                            <div class="card-header">
                                <div class="sub-title"></div>
                            </div>
                            <div class="card-block">   
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                                     <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProduct_RowCommand" ShowHeaderWhenEmpty="true">
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

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-width="5%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSelectProduct" runat="Server" CommandName="SelectProduct" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                                <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                                                <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                                <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
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
                        </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>
                            </div>
                          </div>
                        </div>
                       </div>
      </div>
</div>

            <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-addsupplier">
                    <div class="modal-dialog modal-lg" style="max-width:70%;">
        
                      <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle">เลือกผู้ขาย
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
                              <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูลผู้ขาย</div>
                            </div>
                            <div class="card-block">  
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                                <div class="form-group row">
                                    
                                    <label class="col-sm-2 col-form-label">รหัสผู้ขาย</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchSupplierCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อผู้ขาย</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchSupplierName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <!--<label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-3">
                                         <asp:DropDownList ID="ddlProductBrand_Search" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:TextBox ID="txtSearchProductBrandName" class="form-control" runat="server"></asp:TextBox>
                                    </div>-->
                                
                                    
                                             
                                    <%--<label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">Merchant Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
                                    </div>--%>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearchSupplier" OnClick="btnSearchSupplier_Click" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSupplier" OnClick="btnClearSupplier_Click" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>

                              <div class="card">
                            <div class="card-header">
                                <div class="sub-title"></div>
                            </div>
                            <div class="card-block">   
                                <asp:UpdatePanel ID="UpModal01" runat="server">
                        <ContentTemplate>
                                     <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvSupplier_RowCommand" ShowHeaderWhenEmpty="true">
                                                    <Columns>
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="10%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">รหัสผู้ขาย</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplierCode" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ชื่อผู้ขาย</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-width="5%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSelectSupplier" runat="Server" CommandName="SelectSupplier" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                                <asp:HiddenField runat="server" ID="hidSupplierId" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierId")%>' />
                                                                <asp:HiddenField runat="server" ID="hidSupplierCode" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierCode")%>' />
                                                                <asp:HiddenField runat="server" ID="hidSupplierName" Value='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidPhoneNumber" Value='<%# DataBinder.Eval(Container.DataItem, "PhoneNumber")%>' />
                                                                <asp:HiddenField runat="server" ID="hidFaxNumber" Value='<%# DataBinder.Eval(Container.DataItem, "FaxNumber")%>' />
                                                                <asp:HiddenField runat="server" ID="hidMail" Value='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' />
                                                                <asp:HiddenField runat="server" ID="hidAddress" Value='<%# DataBinder.Eval(Container.DataItem, "Address")%>' />
                                                                <asp:HiddenField runat="server" ID="hidSubDistrictName" Value='<%# DataBinder.Eval(Container.DataItem, "SubDistrictName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidDistrictName" Value='<%# DataBinder.Eval(Container.DataItem, "DistrictName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidProvinceName" Value='<%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidZipNo" Value='<%# DataBinder.Eval(Container.DataItem, "ZipNo")%>' />
                                                                <asp:HiddenField runat="server" ID="hidTaxIdNo" Value='<%# DataBinder.Eval(Container.DataItem, "TaxIdNo")%>' />
                                                                <asp:HiddenField runat="server" ID="hidContactor" Value='<%# DataBinder.Eval(Container.DataItem, "Contactor")%>' />                                                                
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
                        </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>
                        
                            </div>
                          </div>
                        </div>
        </div>
      </div>
        </div>

            <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-addinventory">
                    <div class="modal-dialog modal-lg" style="max-width:70%;">
        
                      <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle1">เลือกสถานที่จัดส่ง
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
                              <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูลคลังสินค้า</div>
                            </div>
                            <div class="card-block">  
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                                <div class="form-group row">
                                    
                                    <label class="col-sm-2 col-form-label">รหัสคลังสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchInventoryCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อคลังสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchInventoryName" class="form-control" runat="server"></asp:TextBox>

                                    </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearchInventory" OnClick="btnSearchInventory_Click" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearchInventory" OnClick="btnClearSearchInventory_Click" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>

                              <div class="card">
                            <div class="card-header">
                                <div class="sub-title"></div>
                            </div>
                            <div class="card-block">   
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                                     <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvInventory_RowCommand" ShowHeaderWhenEmpty="true">
                                                    <Columns>
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="10%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">รหัสคลังสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInventoryCode" Text='<%# DataBinder.Eval(Container.DataItem, "InventoryCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="left">ชื่อคลังสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblInventoryName" Text='<%# DataBinder.Eval(Container.DataItem, "InventoryName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-width="5%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSelectInventory" runat="Server" CommandName="SelectInventory" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                                <asp:HiddenField runat="server" ID="hidInventoryId" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryId")%>' />
                                                                <asp:HiddenField runat="server" ID="hidInventoryCode" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryCode")%>' />
                                                                <asp:HiddenField runat="server" ID="hidInventoryName" Value='<%# DataBinder.Eval(Container.DataItem, "InventoryName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidAddress" Value='<%# DataBinder.Eval(Container.DataItem, "Address")%>' />
                                                                <asp:HiddenField runat="server" ID="hidSubDistrictName" Value='<%# DataBinder.Eval(Container.DataItem, "SubDistrictName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidDistrictName" Value='<%# DataBinder.Eval(Container.DataItem, "DistrictName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidProvinceName" Value='<%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>' />
                                                                <asp:HiddenField runat="server" ID="hidPostCode" Value='<%# DataBinder.Eval(Container.DataItem, "PostCode")%>' />
                                                                <asp:HiddenField runat="server" ID="hidContactTel" Value='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' />
                                                                <asp:HiddenField runat="server" ID="hidFax" Value='<%# DataBinder.Eval(Container.DataItem, "Fax")%>' />   
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
                        </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>
                        
                            </div>
                          </div>
                        </div>
        </div>
      </div>
        </div>

          

     
</asp:Content>
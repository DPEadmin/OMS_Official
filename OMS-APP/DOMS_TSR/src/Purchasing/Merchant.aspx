<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="Merchant.aspx.cs" Inherits="DOMS_TSR.src.Purchasing.Merchant" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
</script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>

<div class="page-body">
  <div class="col-sm-12">
      <!-- Basic Form Inputs card start -->
      <div class="card">
          <div class="card-header">
            <div class="sub-title" >ค้นหาข้อมูลร้านค้า (Merchant)</div>
            </div>
            <div class="card-block">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
            <div class="form-group row">
              <label class="col-sm-2 col-form-label">รหัสร้านค้า</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchMerchantCode" class="form-control" runat="server"></asp:TextBox>
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">ชื่อร้านค้า</label>
              <div class="col-sm-3">
                  

                  <asp:TextBox  ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
                      <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />   
            <asp:HiddenField ID="hidSupplierIdIns" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />      
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

      <div class="card">
         <div class="card-block">
             <div class="m-b-10">
                    <!--Start modal Add Product-->
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
               <ContentTemplate>
                   <asp:LinkButton id="btnAddEmployee" class="btn-add button-active btn-small" 
                        runat ="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                 <asp:LinkButton ID="btnDelete"      
                      class="btn-del button-active btn-small"    runat="server" ><i class="fa fa-minus"></i>Delete</asp:LinkButton>                   
                   
                </ContentTemplate>
            </asp:UpdatePanel>
                 <div class="col-4">
                                <asp:FileUpload ID="fileUpload" runat="server" class="form-control"></asp:FileUpload>
                            </div>
                            <div class="col-4">
                                <asp:Button type="button" ID="btnUpload" Text="Upload" class="button-pri button-accept m-r-10" OnClick="btnUpload_Click" runat="server" />
                            </div>
             </div>

             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                  <div class="dt-responsive table-responsive">
                      <asp:GridView ID="gvMerchant" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap" OnRowCommand="gvMerchant_RowCommand" 
                            TabIndex="0" Width="100%" CellSpacing="0" 
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkMerchantAll" OnCheckedChanged="chkMerchantAll_Changed" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMerchant" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">รหัสร้านค้า</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMerchantCode" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">ชื่อร้านค้า</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblMerchantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
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
                                          <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowMerchant"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                        
                                        <asp:HiddenField runat="server" ID="hidMerchantId" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantId")%>' />

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
                                                    Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                    Text="<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                      OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                    Text=">>" OnCommand="GetPageIndex"></asp:Button>
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

             <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-addmerchant">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                      <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle">เพิ่มร้านค้า
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

                                  <asp:UpdatePanel ID="UpModal" runat="server">
                        <ContentTemplate>
               
                                <div class="form-group row">
      
                                  <label class="col-sm-2 col-form-label">รหัสร้านค้า</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtMerchantCodeIns" runat="server" class="form-control"></asp:TextBox>                                   
                                      <asp:Label ID="lblMerchantCodeIns" runat="server" CssClass="validation"></asp:Label>                              
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">ชื่อร้านค้า</label>
                                  <div class="col-sm-3">
                                           <asp:TextBox ID="txtMerchantNameIns" runat="server" class="form-control"></asp:TextBox> 
                                      <asp:Label ID="lblMerchantNameIns" runat="server" CssClass="validation"></asp:Label>                   
                                  </div>
                                </div>
                                <div class="form-group row">
                                  <label class="col-sm-2 col-form-label">เลขประจำตัวผู้เสียภาษี</label>
                                  <div class="col-sm-3">                                                                     
                                        <asp:TextBox ID="txtTaxIdNoIns" runat="server" class="form-control"></asp:TextBox> 
                                        <asp:Label ID="lblTaxIdNoIns" runat="server" CssClass="validation"></asp:Label>     
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">ที่อยู่</label>
                                  <div class="col-sm-3">
                                  <asp:TextBox ID="txtAddressIns" runat="server" class="form-control"></asp:TextBox>
                                  <asp:Label ID="lblAddressIns" runat="server" CssClass="validation"></asp:Label>                                
                                  </div>
                                  </div> 
                                <div class="form-group row">      
                                  <label class="col-sm-2 col-form-label">จังหวัด</label>
                                  <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlProvinceIns" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>                                   
                                      <asp:Label ID="lblProvinceIns" runat="server" CssClass="validation"></asp:Label>                              
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">เขต/อำเภอ</label>
                                  <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlDistrictIns" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                                      <asp:Label ID="lblDistrictIns" runat="server" CssClass="validation"></asp:Label>                   
                                  </div>
                                </div>
                            <div class="form-group row">      
                                  <label class="col-sm-2 col-form-label">แขวง/ตำบล</label>
                                  <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlSubDistrictIns" runat="server" cssclass="form-control"></asp:DropDownList>                                   
                                      <asp:Label ID="lblSubDistrictIns" runat="server" CssClass="validation"></asp:Label>                              
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">รหัสไปรษณีย์</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox id="txtZipCodeIns" class="form-control" runat="server"></asp:TextBox>
                                      <asp:Label ID="lblZipCodeIns" runat="server" CssClass="validation"></asp:Label>                   
                                  </div>
                                </div>
                            <div class="form-group row">      
                                  <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์</label>
                                  <div class="col-sm-3">
                                       <asp:TextBox id="txtContactTelIns" class="form-control" runat="server"></asp:TextBox>
                                      <asp:Label ID="lblContactTelIns" runat="server" CssClass="validation"></asp:Label>                             
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">แฟกซ์</label>
                                  <div class="col-sm-3">
                                     <asp:TextBox id="txtFaxNumIns" class="form-control" runat="server"></asp:TextBox>                                   
                                     <asp:Label ID="lblFaxNumIns" runat="server" CssClass="validation"></asp:Label>                    
                                  </div>
                                </div>
                            <div class="form-group row">      
                                  <label class="col-sm-2 col-form-label">อีเมล์</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox id="txtEmailIns" class="form-control" runat="server"></asp:TextBox>
                                      <asp:Label ID="lblEmailIns" runat="server" CssClass="validation"></asp:Label>                             
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">สถานะ</label>
                                  <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlStatusIns" class="form-control" runat="server">
                                        <asp:ListItem Enabled="true" Text="Please select status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                                      </asp:DropDownList>                                   
                                      <asp:Label ID="lblStatusIns" runat="server" CssClass="validation"></asp:Label>                   
                                  </div>
                                </div>
                                  </ContentTemplate>
                                  </asp:UpdatePanel>
                                  
                                
                                  <label class="col-sm-2 col-form-label"></label>

                        <div class="text-center m-t-20 center">
                          
                                      <asp:Button ID="btnSubmit" Text="Submit"
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
         </div>
      </div>
  </div>
</div>
</asp:Content>





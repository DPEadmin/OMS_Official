
<%@ Page Language="C#"  MasterPageFile="~/src/MasterPage/Web.master"  AutoEventWireup="true" CodeBehind="ProdctMapRecipe.aspx.cs" Inherits="DOMS_TSR.src.Product.ProdctMapRecipe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>

    </style>
    <script type="text/javascript">

        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvProduct.ClientID %>");

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

            }else {

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
   
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hd" runat="server" />
 <div class="page-body">
    
  <div class="row">
    <div class="col-sm-12">
      <!-- Basic Form Inputs card start -->
      <div class="card">
        <div class="card-header">
          <div class="sub-title" >Search Prodct Map Recipe Management</div>
        </div>
        <div class="card-block">            
            <div class="form-group row">
              <label class="col-sm-2 col-form-label">Product Code</label>
              <div class="col-sm-3">
                <asp:TextBox  ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                      <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
             <asp:HiddenField ID="hidEmpCode" runat="server" />

                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">Product Name</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>
            
              </div>
              <label class="col-sm-2 col-form-label">Merchant Code</label>
              <div class="col-sm-3">
                    <asp:TextBox  ID="txtSearchMerchantCode" class="form-control" runat="server"></asp:TextBox>
              </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">Merchant Name</label>
              <div class="col-sm-3">
                     <asp:TextBox  ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
             </div>
            </div>
           
              <div class="text-center m-t-20 col-sm-12">
            
                  <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"
                      class="button-active button-submit m-r-10"
                      runat="server" />
                     <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click"
                      class="button-active button-cancle"
                      runat="server" />
              
              </div>
          
        </div>
      </div>

      <div class="page-body">
        <div class="row">
          <div class="col-sm-12">
            <!-- Basic Form Inputs card start -->
            <div class="card">
              <div class="card-block">
            
                  <div class="m-b-10">
                    <!--Start modal Add Product-->
                   <asp:LinkButton id="btnAddProduct" class="btn-add button-active btn-small" data-backdrop="false"
                       OnClick="btnAddProduct_Click" runat ="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                 <asp:LinkButton ID="btnDelete"  OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"   
                      class="btn-del button-active btn-small"    runat="server" ><i class="fa fa-minus"></i>Delete</asp:LinkButton>
                   </div>
   
                  <div class="dt-responsive table-responsive">
                      <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProduct_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkProductAll" OnCheckedChanged="chkProductAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkProduct" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

            

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Product Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>
                                        
                         
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Product Name</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >

                     <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowProduct"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                         
                    
                                      

                                           
                                         
                            

                                        <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                        <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                         <asp:HiddenField runat="server" ID="hidProductPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                         <asp:HiddenField runat="server" ID="hidTransportationType" Value='<%# DataBinder.Eval(Container.DataItem, "TransportationTypeCode")%>' />
                                         <asp:HiddenField runat="server" ID="hidWeight" Value='<%# DataBinder.Eval(Container.DataItem, "Weight")%>' />
                                         <asp:HiddenField runat="server" ID="hidProductWidth" Value='<%# DataBinder.Eval(Container.DataItem, "ProductWidth")%>' />
                                         <asp:HiddenField runat="server" ID="hidProductLength" Value='<%# DataBinder.Eval(Container.DataItem, "ProductLength")%>' />
                                         <asp:HiddenField runat="server" ID="hidProductHeigth" Value='<%# DataBinder.Eval(Container.DataItem, "ProductHeigth")%>' />
                                         <asp:HiddenField runat="server" ID="hidPackageWidth" Value='<%# DataBinder.Eval(Container.DataItem, "PackageWidth")%>' />
                                         <asp:HiddenField runat="server" ID="hidPackageLength" Value='<%# DataBinder.Eval(Container.DataItem, "PackageLength")%>' />
                                         <asp:HiddenField runat="server" ID="hidPackageHeigth" Value='<%# DataBinder.Eval(Container.DataItem, "PackageHeigth")%>' />
                                         <asp:HiddenField runat="server" ID="hidProductCategory" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryCode")%>' />
                                         <asp:HiddenField runat="server" ID="hidMerchant" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
                                         <asp:HiddenField runat="server" ID="hidUnit" Value='<%# DataBinder.Eval(Container.DataItem, "Unit")%>' />
                                         <asp:HiddenField runat="server" ID="hidDescription" Value='<%# DataBinder.Eval(Container.DataItem, "Description")%>' />
                                     
                                   
                                        
                                     
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
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
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
            aria-hidden="true" id="modal-product">
            <div class="modal-dialog modal-lg" style="max-width:1300px;">
                <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                    <div class="modal-header modal-header2 ">
                        <div class="col-sm-11">
                        <div id="exampleModalLongTitle">Add Product
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
                            <label class="col-sm-2 col-form-label">Product Code</label>
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtProductCode_Ins" runat="server" class="form-control" style="width:100%"></asp:TextBox> 
                                <asp:Label ID="lblProductCode_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                <asp:HiddenField ID="hidProductCode_Ins" runat="server" ></asp:HiddenField>
                                <asp:HiddenField runat="server" ID="hidProductImgId" />
                               
                            </div>               
                        </div>
                
                                
                  
      
           
                        <div class="form-group row">
                                  
                            <label class="col-sm-2 col-form-label">Product Category</label>
                            <div class="col-sm-3">
                            <asp:DropDownList ID="ddlProductCategory_Ins" runat="server" class="form-control"></asp:DropDownList> 
                            <asp:Label ID="lblProductCategory_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                 
                            </div>

                            <label class="col-sm-1 col-form-label"></label>
                            <label class="col-sm-2 col-form-label">Merchant</label>
                            <div class="col-sm-3">
                            <asp:DropDownList ID="ddlMerChant_Ins" runat="server" class="form-control"></asp:DropDownList> 
                            <asp:Label ID="lblMerChant_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                 
                            </div>
                                
                            <label class="col-sm-2 col-form-label">Unit</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlUnit_Ins" runat="server" class="form-control"></asp:DropDownList> 
                            <asp:Label ID="lblUnit_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                 
                            </div>


                            <label class="col-sm-1 col-form-label"></label>
                            <label class="col-sm-2 col-form-label">Description</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtDescription_Ins" runat="server" class="form-control"
                                    TextMode="MultiLine" Rows="5" Columns ="5"></asp:TextBox>
                            <asp:Label ID="lblDescription_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                 
                       
                            </div>
                           
                    </div>


                               </ContentTemplate>
                </asp:UpdatePanel>
                   
              
                        
                        
                        <div class="form-group row">
                                  <label class="col-sm-2 col-form-label"></label>
                            <div class="col-sm-9">
                                <input type="file" name="files[]" id="filer_input1" multiple="multiple">
                             </div>
                        </div>


          
                        <div class="text-center m-t-20 center">
                               
                                <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" 
                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                runat="server" />
                                <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
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

    

</asp:Content>

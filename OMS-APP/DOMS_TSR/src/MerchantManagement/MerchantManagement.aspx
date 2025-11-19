<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="MerchantManagement.aspx.cs" Inherits="DOMS_TSR.src.MerchantManagement.MerchantManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .hideText {
            width: 20rem;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .validation {
            color: red;
        }
    </style>
    <script type="text/javascript">

        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvMer.ClientID %>");

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
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function validatenumerics(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57)) {
                alert(" กรุณาระบุตัวเลข ");
                return false;
            }
            else return true;
        }

    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <input type="hidden" id="hidIdList" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <asp:HiddenField ID="hidMerCode" runat="server" />
            <asp:HiddenField ID="hidPictureMerchantURL_Ins" runat="server" />
            <asp:HiddenField ID="hidFileName" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">SEARCH MERCHANT</div>
                            </div>

                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Merchant code</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchMerCode" class="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSearchMerCode" runat="server" CssClass="validatecolor"></asp:Label>
                                    </div>
                                
                                    <label class="col-sm-2 col-form-label">Merchant name</label>
                                     <div class="col-sm-4">
                                        <asp:TextBox  ID="txtSearchMerName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSearchMerName" runat="server" CssClass="validatecolor"></asp:Label>
                                     </div>  
                                   
                                    <input type="hidden" id="Hidden2" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <input type="hidden" id="hidaction" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />   
                                    <asp:HiddenField ID="hidSupplierIdIns" runat="server" />
                                    <asp:HiddenField ID="HiddenField3" runat="server" />      
                                 
                                    <%--<div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">Merchant Type</label>
                                    <div class="col-sm-4">
                                           <asp:DropDownList ID="ddlSearchMerT" class="form-control" runat="server">
                                           <asp:ListItem Enabled="true" Text="---- กรุณาเลือก ----" Value="-99"></asp:ListItem>
                                            <asp:ListItem Text="Retail" Value="Retail"></asp:ListItem>
                                            <asp:ListItem Text="Food" Value="Food"></asp:ListItem>      
                                           </asp:DropDownList>
                                    </div>
                                    
                                    <label class="col-sm-2 col-form-label">Company Code</label>
                                    <div class="col-sm-4">
                                           <asp:DropDownList ID="ddlSearchComCode" class="form-control" runat="server">
                                       
                  </asp:DropDownList>
                                    </div>
                                </div>--%>

                                    <div class="text-center m-t-20 col-sm-12">

                   <asp:Button ID="btnSearch" Text="Search" CssClass="button-pri button-accept   m-r-10" OnClick="btnSearch_Click" runat="server"/>
                  <asp:Button ID="btnClearSearch" Text="Clear" CssClass="button-pri button-cancel" OnClick="btnClearSearch_Click" runat="server" />   
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="m-b-10">
                                    <asp:LinkButton ID="btnAddMerchant" class="button-action button-add m-r-5"
                                        OnClick="btnAddMerchant_Click" runat="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                                     <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                              
                                </div>

                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvMer" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                        TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvMer_RowCommand"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkMerAll" OnCheckedChanged="chkMerAll_Changed" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMer" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Merchant code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                              <%# GetLink(DataBinder.Eval(Container.DataItem, "MerchantCode")) %>
                                                           
                                    </ItemTemplate>
                                </asp:TemplateField>

                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Merchant name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblMerChantName" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' runat="server" />
                                         </ItemTemplate>
                                </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">ประเภทร้านค้า</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblMerT" Text='<%# DataBinder.Eval(Container.DataItem, "MerchantType")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Company Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblComCode" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyCode")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">telephone</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">fax</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblFax" Text='<%# DataBinder.Eval(Container.DataItem, "FaxNum")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Email</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblMail" Text='<%# DataBinder.Eval(Container.DataItem, "Email")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate  >
                                          <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowMer"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                 
                                        <asp:HiddenField runat="server" ID="hidMerId" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantId")%>' />
                                         <asp:HiddenField runat="server" ID="hidMerCode" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
                                           <asp:HiddenField runat="server" ID="hidMerName" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantName")%>' />
                                          <asp:HiddenField runat="server" ID="hidMerT" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantType")%>' />
                                          <asp:HiddenField runat="server" ID="hidComCode" Value='<%# DataBinder.Eval(Container.DataItem, "CompanyCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidTaxId" Value='<%# DataBinder.Eval(Container.DataItem, "TaxId")%>' />
                                        <asp:HiddenField runat="server" ID="hidAddress" Value='<%# DataBinder.Eval(Container.DataItem, "Address")%>' />
                                        <asp:HiddenField runat="server" ID="hidProvince" Value='<%# DataBinder.Eval(Container.DataItem, "ProvinceCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidDistrict" Value='<%# DataBinder.Eval(Container.DataItem, "DistrictCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidSubDistrict" Value='<%# DataBinder.Eval(Container.DataItem, "SubDistrictCode")%>' />
                                          <asp:HiddenField runat="server" ID="hidPhone" Value='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' />
                                        <asp:HiddenField runat="server" ID="hidZipCode" Value='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>' />
                                          <asp:HiddenField runat="server" ID="hidFax" Value='<%# DataBinder.Eval(Container.DataItem, "FaxNum")%>' />
                                         <asp:HiddenField runat="server" ID="hidEmail" Value='<%# DataBinder.Eval(Container.DataItem, "Email")%>' />
                                         <asp:HiddenField runat="server" ID="hidPictureMerchantURL" Value='<%# DataBinder.Eval(Container.DataItem, "PictureMerchantURL")%>' />
                                      
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

                                     
                                    <%-- PAGING CAMPAIGN --%>
                                    <div class="m-t-10">
                                    <table width="100%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                        <tr height="30" bgcolor="#ffffff">
                                            <td width="100%" align="right" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                    <tr>
                                                        <td style="font-size: 8.5pt;">
                                                            <%--<Rows per page 
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
                                                            <asp:Button ID="lnkbtnFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                                Text="<<" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
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
                                                            <asp:Button ID="lnkbtnNext" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex"></asp:Button>
                                                        </td>
                                                        <td style="width: 6px"></td>
                                                        <td>
                                                            <asp:Button ID="lnkbtnLast" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
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
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
     <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="modal-mer">
        <div class="modal-dialog modal-lg" style="max-width: 50%;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2 ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">
                                      Add Merchant
                                </div>
                            </div>
                            <span>
                                <button type="button" class="close  " style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>

                 <div class="modal-body">
                     
                                <asp:UpdatePanel ID="upModal" runat="server">
                                    <ContentTemplate>
                                         <div class="form-group row">
      
                                  <label class="col-sm-4 col-form-label">Merchant code<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-8">
                                        <asp:HiddenField ID="hidMerIdIns" runat="server"></asp:HiddenField>                                   
                                     
                                      <asp:TextBox ID="txtMerCodeIns" runat="server" class="form-control"></asp:TextBox>                                   
                                      <asp:Label ID="lblMerCodeIns" runat="server" CssClass="validation"></asp:Label>                              
                                  </div>
                                  
                                             <label class="col-sm-4 col-form-label">Merchant name<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-8">                                                                     
                                        <asp:TextBox ID="txtMerNameIns" runat="server" class="form-control"></asp:TextBox> 
                                        <asp:Label ID="lblMerNameIns" runat="server" CssClass="validation"></asp:Label>     
                                  </div>
                                

                                  <%--<label class="col-sm-4 col-form-label">Merchant Type<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-8">
                                             <asp:DropDownList id="ddlMerTIns" class="form-control" runat="server">
                                      <asp:ListItem Enabled="true" Text="---- กรุณาเลือก ----" Value="-99"></asp:ListItem>
                                            <asp:ListItem Text="Retail" Value="Retail"></asp:ListItem>
                                            <asp:ListItem Text="Food" Value="Food"></asp:ListItem>      
                                           </asp:DropDownList>
                                      <asp:Label ID="lblMerTIns" runat="server" CssClass="validation"></asp:Label>                
                                  </div>
                                  
                                  <label class="col-sm-4 col-form-label">Company Code<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-8">
                                             <asp:DropDownList id="ddlComCodeIns" class="form-control" runat="server"></asp:DropDownList>
                                      <asp:Label ID="lblComCodeIns" runat="server" CssClass="validation"></asp:Label>                
                                  </div>--%>
                                                               
                                  <label class="col-12 col-form-label">(ที่อยู่สำหรับออกใบกำกับภาษี)  Invoice Address</label>
                                        <label class="col-sm-4 col-form-label">Tax ID<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-8">                                                                     
                                        <asp:TextBox ID="txtTaxIns" runat="server" class="form-control"></asp:TextBox> 
                                        <asp:Label ID="lblTaxIns" runat="server" CssClass="validation"></asp:Label>     
                                  </div>
                                
                                                                                                                                                             
                                            <label class="col-sm-4 col-form-label">Address<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAddressIns" runat="server" class="form-control" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                                <asp:Label ID="lblAddressIns" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">Province<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProvinceIns" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblProvinceIns" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                            <label class="col-sm-4 col-form-label">District<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlDistrictIns" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblDistrictIns" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">Sub-District (Tumbon)<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSubDistrictIns" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblSubDistrictIns" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>

                                        <label class="col-sm-4 col-form-label">Zip/Postal Code<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPostCodeIns" runat="server" class="form-control" onkeypress="return validatenumerics(event);"></asp:TextBox>
                                                <asp:Label ID="lblPostCodeIns" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                
                                  <label class="col-sm-4 col-form-label">Telephone Number<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-8">
                                       <asp:TextBox id="txtMobileIns" class="form-control" runat="server"></asp:TextBox>
                                      <asp:Label ID="lblMobileIns" runat="server" CssClass="validation"></asp:Label>                             
                                  </div>

                                  
                                  <label class="col-sm-4 col-form-label">Fax</label>
                                  <div class="col-sm-8">
                                     <asp:TextBox id="txtFaxIns" class="form-control" runat="server"></asp:TextBox>                                   
                                     <asp:Label ID="lblFaxIns" runat="server" CssClass="validation"></asp:Label>                    
                                  </div>
                                  
                                  <label class="col-sm-4 col-form-label">Email</label>
                                  <div class="col-sm-8">
                                     <asp:TextBox id="txtEmailIns" class="form-control" runat="server"></asp:TextBox>                                   
                                     <asp:Label ID="lblEmailIns" runat="server" CssClass="validation"></asp:Label>                    
                                  </div>
                                </div>
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                     
                                <div class="form-group row" id="sectionImage" style="display:none;">
                                <label class="col-sm-4 col-form-label">Image</label>
                                <div class="col-sm-8">
                                     <input name="photo" type="file" accept="image/*"
                                            onchange="document.getElementById('<%= MerchantPicIm.ClientID %>').src = window.URL.createObjectURL(this.files[0])">                  
                                </div>

                                <label class="col-sm-4 col-form-label"></label>
                                <div class="col-sm-8">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                     <img src="1" runat="server" id="MerchantPicIm"
                                     style="height: 50%; width: 50%; object-fit: contain" alt="">     
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                </div>

                                        <div class="text-center m-t-20 center">

                                            <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                                  class="button-pri button-accept m-r-10"
                                                runat="server" />
                                            <asp:Button ID="btnCancel" Text="Clear" OnClick="btnCancel_Click"
                                               class="button-pri button-cancel"
                                                runat="server" />

                                        </div>                                    

                                
                
                </div>

            </div>
        </div>
    </div>
   
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="DOMS_TSR.src.Product.Product" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .hideText  {
    width:20rem;
    overflow:hidden;
    text-overflow:ellipsis;
    white-space:nowrap;
 }
    </style>
    <link rel="stylesheet" type="text/css" href="http://harvesthq.github.io/chosen/chosen.css">
    <script type="text/javascript" src="http://harvesthq.github.io/chosen/chosen.jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
         $('#modal-product').on('shown.bs.modal', function () {
             $('.chosen-select', this).chosen();
              $('.chosen-select1', this).chosen();
        });
    });
    </script>
    <script type="text/javascript">
        function DeleteConfirm() {
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

        function DeleteConfirmGV() {

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
    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hd" runat="server" />
            <div class="page-body">

                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูลสินค้า</div>
                            </div>
                            <div class="card-block">
                                
                                
                                <div class="form-group row">
                                    
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-3">
                                         <asp:DropDownList ID="ddlProductBrand_Search" runat="server" class="form-control"></asp:DropDownList>
                                        <!--<asp:TextBox ID="txtSearchProductBrandName" class="form-control" runat="server"></asp:TextBox>-->
                                    </div>
                                
                                    
                                             
                                    <%--<label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">Merchant Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
                                    </div>--%>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                </div>

                            </div>
                        </div>

                       
                                    
                                    <div class="card">
                                        <div class="card-block">

                                            <div class="m-b-10">
                                                <!--Start modal Add Product-->
                                                <asp:LinkButton ID="btnAddProduct" class="button-action button-add" data-backdrop="false" OnClick="btnAddProduct_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmGV();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>

                                         
                                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " style="white-space:nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProduct_RowCommand" ShowHeaderWhenEmpty="true">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-width="5%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                                                    <asp:CheckBox ID="chkProductAll" OnCheckedChanged="chkProductAll_Change" AutoPostBack="true" runat="server"  />
                                                                </center>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkProduct" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="10%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="left">รหัสสินค้า</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                          <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-width="15%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="left">รหัสอ้างอิง</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblProductSku" Text='<%# DataBinder.Eval(Container.DataItem, "Sku")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-width="25%"   HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="Center">ชื่อสินค้า</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <div class="hideText">
                                                               <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                                                    </div>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-width="20%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="right">ราคา (บาท)</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                               <asp:Label ID="lblProductPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:0.00}")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-width="15%"   HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                                <HeaderTemplate>
    
                                                                    <div align="Center">หน่วย</div>
    
                                                                </HeaderTemplate>
    
                                                                <ItemTemplate>
                                                                   <asp:Label ID="lblProductUnit" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
    
                                                                </ItemTemplate>
    
                                                            </asp:TemplateField>
                                                        
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-width="25%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="Center">แบรนด์</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-width="5%"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                            <HeaderTemplate>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowProduct" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>
                                                                <%--<asp:LinkButton ID="buttonDelete" runat="Server" OnClientClick="return DeleteConfirm();" CommandName="DeleteProduct"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="button-activity  " >  <span class="ti-trash f-14"></span></asp:LinkButton>--%>

                                                                <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                                                <asp:HiddenField runat="server" ID="hidProductSku" Value='<%# DataBinder.Eval(Container.DataItem, "Sku")%>' />
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
                                                                <asp:HiddenField runat="server" ID="hidProductBrand" Value='<%# DataBinder.Eval(Container.DataItem, "ProductBrandCode")%>' />
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
                     
                        <!-- Basic Form Inputs card end -->
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-product">
        <div class="modal-dialog modal-lg" style="max-width: 600px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px; ">เพิ่มสินค้า</div>
                                
                            </div>
                           <span><button type="button" class="close  " style="padding-left:0px; padding-right:0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                            </button> </span>  
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
                                            <label class="col-sm-4 col-form-label">รหัสสินค้า</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtProductCode_Ins" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:Label ID="lblProductCode_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                                <asp:HiddenField ID="hidProductCode_Ins" runat="server"></asp:HiddenField>
                                                <asp:HiddenField runat="server" ID="hidProductImgId" />

                                            </div>

                                            <label class="col-sm-4 col-form-label">รหัสอ้างอิง</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtProductSku_Ins" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:Label ID="lblProductSku_Ins" runat="server" CssClass="validatecolor"></asp:Label>

                                            </div>
                                            
                                            <label class="col-sm-4 col-form-label">ชื่อสินค้า</label>
                                            <div class="col-sm-8">

                                                <asp:TextBox ID="txtProductName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblProductName_Ins" runat="server" CssClass="validatecolor"></asp:Label>

                                            </div>
                                            <label class="col-sm-4 col-form-label">ราคา(บาท)</label>
                                            <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPrice_Ins" runat="server" class="form-control" onkeypress="return validatenumerics(event);"></asp:TextBox>
    
                                                    <asp:Label ID="lblPrice_Ins" runat="server" CssClass="validatecolor"></asp:Label>
    
                                                </div>
                                                <label class="col-sm-4 col-form-label">หน่วย</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlUnit_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblUnit_Ins" runat="server" CssClass="validatecolor"></asp:Label>

                                            </div>
                                                <label class="col-sm-4 col-form-label">แบรนด์</label>
                                                <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProductBrand_Ins" runat="server" class="form-control"></asp:DropDownList>
                                            </div>
                                            
                                             <label class="col-sm-4 col-form-label">รายละเอียด</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDescription_Ins" runat="server" class="form-control"
                                                    TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                                <asp:Label ID="lblDescription_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                              <label class="col-sm-4 col-form-label">อัพเซล สคริปต์</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtUpsellScript_Ins" runat="server" class="form-control"
                                                    TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                                <asp:Label ID="lblUpsellScript_Ins" runat="server" CssClass="validatecolor"></asp:Label>
                                            </div>
                                            
                                          
                                          

                                        </div>


                                        <!-- <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Weight</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtWeight_Ins" runat="server" class="form-control"></asp:TextBox>

                                                <asp:Label ID="lblWeight_Ins" runat="server" CssClass="validatecolor"></asp:Label>


                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>
                                        

                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">Logistic Type</label>
                                            <div class="col-sm-3">
                                             
                                                <asp:DropDownList ID="ddlLogisticType_Ins" runat="server" class="form-control"></asp:DropDownList>

                                                <asp:Label ID="lblLogisticType_Ins" runat="server" CssClass="validatecolor"></asp:Label>

                                            </div>

                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">Product Brand</label>
                                            <div class="col-sm-3">
                                               
                                                <%--<asp:DropDownList ID="ddlProductBrand_Ins" runat="server" class="form-control"></asp:DropDownList>--%>

                                                <asp:Label ID="lblProductBrand_Ins" runat="server" CssClass="validatecolor"></asp:Label>

                                            </div>


                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Product Size(cm)</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtProductSizeWidth_Ins" placeholder="ก" runat="server" class="form-control"></asp:TextBox>

                                            </div>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtProductSizeLength_Ins" placeholder="ย" runat="server" class="form-control"></asp:TextBox>

                                            </div>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtProductSizeHeight_Ins" placeholder="ส" runat="server" class="form-control"></asp:TextBox>

                                            </div>

                                        </div>

                                        <div class="col-sm-3 offset-2" style="padding-left: 4px;">
                                            <asp:Label ID="lblProductSize_Ins" runat="server" CssClass="validatecolor"></asp:Label>

                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Package Size(cm)</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtPackageSizeWidth_Ins" placeholder="ก" runat="server" class="form-control"></asp:TextBox>

                                            </div>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtPackageSizeLenght_Ins" placeholder="ย" runat="server" class="form-control"></asp:TextBox>

                                            </div>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtPackageSizeHeight_Ins" placeholder="ส" runat="server" class="form-control"></asp:TextBox>

                                            </div>

                                        </div>
                                        <div class="col-sm-3 offset-2" style="padding-left: 4px;">
                                            <asp:Label ID="lblPackageSize_Ins" runat="server" CssClass="validatecolor"></asp:Label>

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



                                           
                                            
                                       
                                                   
                                            

                                        </div> -->
                                 <div class="form-group row"  runat="server" id="MultiSelectrecipe" visible="false">
                                        <label class="col-sm-4 col-form-label">ส่วนประกอบ</label> 
                                      <div class="col-sm-8">

                                        <asp:ListBox ID="ddlMultiSelect" data-placeholder="กรุณาเลือกส่วนประกอบ..." class="chosen-select" SelectionMode="Multiple" Style="width: 100%;" runat="server">
                                        </asp:ListBox>

                                                </div>
                                  </div>

                                <div class="form-group row" runat="server" id="IDAllergy" visible="false">
                                        <label class="col-sm-4 col-form-label">อาการแพ้</label> 
                                      <div class="col-sm-8">

                                        <asp:ListBox ID="ddlAllergy" data-placeholder="กรุณาเลือกอาการแพ้..." class="chosen-select1" SelectionMode="Multiple" Style="width: 100%;" runat="server">
                                        </asp:ListBox>

                                                </div>
                                  </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     


                                <div class="form-group row">
                                    <label class="col-sm-4 col-form-label">รูปภาพ</label>
                                    <div class="col-sm-8">
                                        <input type="file" name="files[]" id="filer_input1">
                                       <%-- <input type="file" name="files[]" id="filer_input1" multiple="multiple">--%>
                                    </div>
                                </div>



                                <div class="text-center m-t-20 center">

                                    <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                        class="button-pri button-cancel"
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

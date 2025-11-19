
<%@ Page Language="C#"  MasterPageFile="~/src/MasterPage/Web.master"  AutoEventWireup="true" CodeBehind="ProductPointDetail.aspx.cs" Inherits="DOMS_TSR.src.Point.ProductPointDetail" %>
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

        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvProductComplementary.ClientID %>");

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

        function check(e, value) {
            //Check Charater
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (value.indexOf(".") != -1) if (unicode == 46) return false;
            if (unicode != 8) if ((unicode < 48 || unicode > 57) && unicode != 46) return false;
        }

        function checkLength() {
            var fieldLength = document.getElementsByClassName('.txtF').value.length;
            //Suppose u want 4 number of character
            if (fieldLength <= 4) {
                return true;
            }
            else {
                var str = document.getElementsByClassName('.txtF').value;
                str = str.substring(0, str.length - 1);
                document.getElementById('txtF').value = str;
            }
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
                                                                  <th scope="row" style="width: 40%;">รหัสรางวัล</th>
                                                                  <td><asp:Label runat="server" ID="txtProductCode"></asp:Label></td>
                                                              </tr>

                                                               <tr>
                                                                  <th scope="row" style="width: 40%;">รหัสอ้างอิง</th>
                                                                  <td><asp:Label runat="server" ID="txtProductSku"></asp:Label></td>
                                                              </tr>
                                                           
                                                              <tr>
                                                                  <th scope="row" style="width: 40%;">ชื่อสินค้า</th>
                                                                  <td><asp:Label runat="server" ID="txtProductName"></asp:Label></td>
                                                              </tr>

                                                            
                                                              <tr>
                                                                  <th scope="row" style="width: 40%;">อัตราแลกเปลี่ยน Point</th>
                                                                  <td><asp:Label runat="server" ID="txtExchangeRate"></asp:Label></td>
                
                                                              </tr>
                                                              <tr>
                                                                    <th scope="row" style="width: 40%;"
                                                                    ">หน่วย</th>
                                                                    <td "><asp:Label runat="server" ID="txtUnit"></asp:Label></td>
                                                              </tr>
                                                              <tr>
                                                                    <th scope="row" style="width: 40%;"
                                                                    ">หมวดหมู่</th>
                                                                    <td "><asp:Label runat="server" ID="txtCouponType"></asp:Label></td>
                                                              </tr>
                                                          
                                                          
                                                        <tr>
                                                                <th scope="row" style="width: 40%;">รายละเอียด
                                                                    </th>
                                                                
                                                                <td style="border-top: 0px; ">
                                                                    <textarea rows="4" runat="server" id="txtDescription"
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
                                                                  <th scope="row" style="width: 40%;">ขนาดของสินค้า(ซม)</th>
                                                                  <td><asp:Label runat="server" ID="txtProductWidth"></asp:Label> x 
                                                                      <asp:Label runat="server" ID="txtProductLength"></asp:Label> x
                                                                      <asp:Label runat="server" ID="txtProductHeight"></asp:Label>
                                                                  </td>
                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row" style="width: 40%;">ขนาดบรรจุภัณฑ์(ซม)</th>
                                                                  <td><asp:Label runat="server" ID="txtPackageWidth"></asp:Label> x 
                                                                      <asp:Label runat="server" ID="txtPackageLength"></asp:Label> x
                                                                      <asp:Label runat="server" ID="txtPackageHeight"></asp:Label>
                                                                  </td>
                                                              </tr>
                                                               <tr>
                                                                  <th scope="row" style="width: 40%;">น้ำหนัก
                                                                      (กก.)</th>
                                                                  <td><asp:Label runat="server" ID="txtProductWeight"></asp:Label></td>
                                                              </tr> 
                                                              <tr>
                                                                <th scope="row" style="width: 40%;">ประเภทการขนส่ง  
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

            <asp:UpdatePanel ID="mainPanel" runat="server">
            <ContentTemplate>
                <div id="complementarysection" runat="server">
                     <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาสินค้าที่เป็นของแถมกับสินค้า</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchProductCode" class="form-control" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="Hidden1" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <input type="hidden" id="Hidden2" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />

                                    </div>
                                  
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click"
                                        class="button-pri button-cancel"
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
                                                <!--Start modal Add Promotion-->
                                                <asp:LinkButton ID="btnAddProductComplementary" class="button-action button-add m-r-10" 
                                                    OnClick="btnAddProductComplementary_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClientClick="return DeleteConfirm();" OnClick="btnDelete_Click"
                                                    class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>


                                            <asp:GridView ID="gvProductComplementary" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                
                                                TabIndex="0" Width="100%" CellSpacing="0"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                            <asp:CheckBox ID="chkProductComplementaryAll" OnCheckedChanged="chkProductComplementaryAll_Click" AutoPostBack="true" runat="server"  />
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkProductComplementary" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">รหัสสินค้า</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>--%>
                                                            <asp:HyperLink ID="hyperlink" NavigateUrl='<%# String.Format("../Product/ProductDetail.aspx?ProductCode=" + DataBinder.Eval(Container.DataItem, "ProductCode")) %>' Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server"> </asp:hyperlink>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">แบรนด์</div>

                                        </HeaderTemplate>

                                        <ItemTemplate >
                                            <asp:Label ID="lblProductBrand" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">ชื่อสินค้า</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">หน่วย</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">ราคา</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:#,0.00}")%>' runat="server" />
                                                             <asp:HiddenField runat="server" ID="hidPromotionDetailInfoId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")%>' />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">จำนวน</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                                           
                                                            <asp:HiddenField runat="server" ID="hidComplementaryId" Value='<%# DataBinder.Eval(Container.DataItem, "ComplementaryId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />                                                            
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
                                                                                       OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" >
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
                                            <div class="text-center m-t-20 col-sm-12 ">
                                                <asp:Button OnClick="btnBack_Click" runat="server" Text="ย้อนกลับ" class="button-pri button-accept " />

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

                                </div>
            
            <div class="modal fade bd-example-modal-lg" id="modal-Product" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width: 80%">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpModal" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="modal-header modal-header2   ">
                                    <div class="col-sm-12 p-0">
                                        <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เลือกสินค้า</div>

                                    </div>
                                    <span>
                                        <button type="button" class="close  " style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="modal-body ">
                          
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchModalProductCode" class="form-control" runat="server"></asp:TextBox>


                                    </div>
                                   
                                    <label class="col-sm-2 col-form-label">ชื่อสินค้า</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchModalProductName" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                              <%--      <label class="col-sm-2 col-form-label">ช่องทาง</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchModalChannel" runat="server" class="form-control"></asp:DropDownList>
                                    </div>--%>
                                    <%--    <label class="col-sm-1 col-form-label"></label>
                            <label class="col-sm-2 col-form-label">Merchant Name</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSearchMerchantName" class="form-control" runat="server"></asp:TextBox>
                            </div>--%>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnModalSearch" Text="ค้นหา" OnClick="btnModalSearch_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnModalClear" Text="ล้าง" OnClick="btnModalClear_Click"
                                        class="button-pri button-cancel"
                                        runat="server" />

                                </div>

                           
                            <asp:HiddenField ID="hidFlagInsertComplementary" runat="server" />
                            <asp:HiddenField ID="hidPromotionSet" runat="server" />
                            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                OnRowDataBound="gvProduct_RowDataBound" OnRowCreated="gvProduct_RowCreated"
                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProduct_RowCommand"
                                ShowHeaderWhenEmpty="true">

                                <Columns>
                                    <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                            <asp:CheckBox ID="chkProductAll" OnCheckedChanged="chkProductAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkProduct" runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>



                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">รหัสสินค้า</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# GetLink(DataBinder.Eval(Container.DataItem, "ProductCode")) %>--%>
                                            <asp:HyperLink ID="hyperlink" NavigateUrl='<%# String.Format("../Product/ProductDetail.aspx?ProductCode=" + DataBinder.Eval(Container.DataItem, "ProductCode")) %>' Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server"> </asp:hyperlink>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">แบรนด์</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductBrand" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">สินค้า</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">หน่วย</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProduUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="right">ราคา</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>

                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:#,0.00}")%>' runat="server" Style="text-align: right" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">จำนวน</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty_Ins" runat="server" TextMode="Number" Value='<%#DataBinder.Eval(Container.DataItem, "Qty")%>' class="form-control txtF" Style="text-align: right" onKeyPress="return check(event,value)" onInput="checkLength()" min="1"/>
                                            <%--<asp:HiddenField runat="server" ID="hidQty_Ins" Value='<%#txtQty_Ins.Text%>' />--%>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                        <%--            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                            <div align="Center">Channel</div>

                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblProductChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                        <HeaderTemplate>
                                        </HeaderTemplate>

                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddProductComplementary"
                                                class="button-activity "
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-14"></span></asp:LinkButton>

                                            <asp:HiddenField runat="server" ID="hidProductId" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                            <asp:HiddenField runat="server" ID="hidProductCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' />
                                            <asp:HiddenField runat="server" ID="hidProductName" Value='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />

                                            <asp:HiddenField runat="server" ID="hidPromotionDetailId" />






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
                                                    <asp:Button ID="btnPdFirst" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                        Text="<<" runat="server" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdPre" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
                                                        Text="<" runat="server" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPdPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlProductPage_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                    of
                                                                                    <asp:Label ID="lblTotalPdPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdNext" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                                <td style="width: 6px"></td>
                                                <td>
                                                    <asp:Button ID="btnPdLast" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                        Text=">>" OnCommand="GetProductPageIndex"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </div>




                            <div class="text-center m-t-20 col-sm-12">
                                <%--<asp:Button type="button" ID="btnSubmit" Text="สร้าง" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Click" runat="server" />--%>
                                <%--<asp:Button type="button" ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click" class="button-pri button-cancel" runat="server" />--%>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

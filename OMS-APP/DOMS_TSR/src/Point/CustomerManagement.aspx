<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="CustomerManagement.aspx.cs" Inherits="DOMS_TSR.src.Point.CustomerManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .hideText  {
    width:20rem;
    overflow:hidden;
    text-overflow:ellipsis;
    white-space:nowrap;
 }

        .validation {
            color: red;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="http://harvesthq.github.io/chosen/chosen.css">
    <script type="text/javascript" src="http://harvesthq.github.io/chosen/chosen.jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#modal-customer').on('shown.bs.modal', function () {
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

            var grid = document.getElementById("<%= gvCustomer.ClientID %>");

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
                                <div class="sub-title">ข้อมูลสมาชิก</div>
                            </div>
                            <div class="card-block">


                                <div class="form-group row">

                                    <label class="col-sm-2 col-form-label">รหัสสมาชิก</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchCustomerCode" class="form-control" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                        <asp:HiddenField ID="hidMerchantCode" runat="server" />
                                    </div>
                                    
                                    <label class="col-sm-2 col-form-label">ชื่อ-นามสกุล</label>
                                    <div class="col-sm-4">
                                        <div class="input-group mb-0">
                                               <asp:TextBox  ID="txtSearchCustomerFName" class="form-control" placeholder="ชื่อ" runat="server" ></asp:TextBox>
                                               <asp:TextBox ID="txtSearchCustomerLName" class="form-control" placeholder="นามสกุล" runat="server" ></asp:TextBox>
                                        </div>

                                    </div>
                                    <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchCustomerTel" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2 col-form-label">อีเมล์</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchCustomerEmail" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <label class="col-sm-2 col-form-label">ระดับ</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlPointRange_Search" runat="server" class="form-control"></asp:DropDownList>
                                    </div>

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
                                    <asp:LinkButton ID="btnAddCustomer" class="button-action button-add" data-backdrop="false" OnClick="btnAddCustomer_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmGV();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                    
                                </div>

                                <div class="table-responsive">
                                <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvCustomer_RowCommand" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                           <asp:CheckBox ID="chkCustomerAll" OnCheckedChanged="chkCustomerAll_Change" AutoPostBack="true" runat="server"  />
                                                                </center>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCustomer" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="left">รหัสสมาชิก</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <%--<asp:Label ID="lblCustomeCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />--%>
                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "CustomerCode")) %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">ชื่อสมาชิก</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <div class="hideText">
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                                </div>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                        <HeaderTemplate>

                                           <div align="left">วันที่เป็นสมาชิก</div>

                                            </HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="lblRegisterDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                              </ItemTemplate>

                                         </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">เบอร์โทรศัพท์</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerTel" Text='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' runat="server" />

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">Email</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerEmail" Text='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' runat="server" />

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">Point</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerPointNum" Text='<%# DataBinder.Eval(Container.DataItem, "PointNum")%>' runat="server" />

                                            </ItemTemplate>

                                            </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">PointName</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerPointName" Text='<%# DataBinder.Eval(Container.DataItem, "PointName")%>' runat="server" />

                                            </ItemTemplate>

                                            </asp:TemplateField>
                                       
                                        
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                            <HeaderTemplate>
                                                  <div align="Center">แก้ไข</div>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowCustomer" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="button-pri button-activity m-r-10 icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                <%--<asp:LinkButton ID="buttonDelete" runat="Server" OnClientClick="return DeleteConfirm();" CommandName="DeleteCustomer"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="button-activity  " >  <span class="ti-trash f-14"></span></asp:LinkButton>--%>


                                                <asp:HiddenField runat="server" ID="hidCustomerId" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerId")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerFName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerFName")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerLName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerLName")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerCode" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerTel" Value='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerEmail" Value='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerPointNum" Value='<%# DataBinder.Eval(Container.DataItem, "PointNum")%>' />
                                                 <asp:HiddenField runat="server" ID="hidMerchant" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
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
                                    </div>
                                <br />
                                <br />
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
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button  pagina_btn" ToolTip="First" CommandName="First"
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

                        <!-- Basic Form Inputs card end -->
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-customer">
        <div class="modal-dialog modal-lg" style="max-width: 600px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มสมาชิก</div>

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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card-block">

                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                              
                                            <label class="col-sm-4 col-form-label">รหัสสมาชิก</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCustomerCode_Ins" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:Label ID="lblCustomerCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidCustomerCode_Ins" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hidPointRangeCode_Ins" runat="server"></asp:HiddenField>
                                                <asp:HiddenField runat="server" ID="hidCustomerImgId" />
                                            </div>
                                            <label class="col-sm-4 col-form-label">ชื่อ-นามสกุล<span style="color: red; background-position: right top;">*</span></label>
                                                <div class="col-sm-8">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox  ID="txtCustomerFName_Ins" class="form-control" placeholder="ชื่อ" runat="server" ></asp:TextBox>
                                                        <asp:TextBox ID="txtCustomerLName_Ins" class="form-control" placeholder="นามสกุล" runat="server" ></asp:TextBox>
                                                    </div>
                                                     <asp:Label ID="lblCustomerName_Ins" runat="server" CssClass="validation"></asp:Label>
                                                </div>
                                             <label class="col-sm-4 col-form-label">เบอร์โทรศัพท์<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCustomerTel_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblCustomerTel_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                              <label class="col-sm-4 col-form-label">อีเมล์<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCustomerEmail_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblCustomerEmail_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                             <label class="col-sm-4 col-form-label">พ้อยท์</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPointNum_Ins" runat="server" type="number" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPointNum_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                          
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="text-center m-t-20 center">

                                    <asp:Button ID="btnSubmit" Text="บันทึก" OnClick="btnSubmit_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnCancel" Text="ยกเลิก" OnClick="btnCancel_Click"
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

    <script>
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

</asp:Content>

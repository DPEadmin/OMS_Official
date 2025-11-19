<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="DOMS_TSR.src.Voucher.Voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvVoucher.ClientID %>");

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <input type="hidden" id="hidIdList" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">Voucher Management</div>
                            </div>

                            <div class="card-body">
                                  <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">โค้ดบัตรกำนัล</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtVoucherCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">ชื่อบัตรกำนัล</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtVoucherName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ประเภทบัตรกำนัล</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlVoucherType" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlCampaignCategory" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">จำนวนเงิน</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPrice" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">วันที่เริ่ม</label>
                                    <div class="col-sm-3"> 
                                        <asp:TextBox ID="txtStartDateFrom" Format="dd/MM/yyyy" runat="server" style="width:45%" />
                                    <ajaxToolkit:CalendarExtender ID="carStartDateFrom" Format="dd/MM/yyyy" runat="server" TargetControlID="txtStartDateFrom" 
                                        PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>
                                        ถึง
                                             <asp:TextBox ID="txtStartDateTo" Format="dd/MM/yyyy" runat="server" style="width:45%" />
                                    <ajaxToolkit:CalendarExtender ID="carStartDateTo" Format="dd/MM/yyyy" runat="server" TargetControlID="txtStartDateTo" 
                                        PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>        
                                      
                                    </div>
                                </div>
                                  <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">วันที่สิ้นสุด</label>
                                    <div class="col-sm-3">
                                           <asp:TextBox ID="txtEndDateFrom" Format="dd/MM/yyyy"  runat="server" style="width:45%" />
                                    <ajaxToolkit:CalendarExtender ID="carEndDateFrom" Format="dd/MM/yyyy"  runat="server" TargetControlID="txtEndDateFrom" 
                                        PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>
                                        ถึง
                                             <asp:TextBox ID="txtEndDateTo" Format="dd/MM/yyyy"  runat="server" style="width:45%" />
                                    <ajaxToolkit:CalendarExtender ID="carEndDateTo" Format="dd/MM/yyyy"  runat="server" TargetControlID="txtEndDateTo" 
                                        PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>                     

                                    </div>
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-2 col-form-label">สถานะ</label>
                                    <div class="col-sm-3">
                                           <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" />

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

                        <div class="card">
                            <div class="card-body">
                                <div class="m-b-10">
                                    <asp:LinkButton ID="btnAddDriver" class="btn-add button-active btn-small"
                                        OnClick="btnAddDriver_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                        class="btn-del button-active btn-small" runat="server"><i class="fa fa-minus"></i>Delete</asp:LinkButton>
                                </div>

                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvVoucher" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                                        TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvVoucher_RowCommand"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <center>
                                            <asp:CheckBox ID="chkVoucherAll" OnCheckedChanged="chkVoucherAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <asp:CheckBox ID="chkVoucher" runat="server" />

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">โค้ดบัตรกำนัล</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <asp:Label ID="lblVoucherCode" Text='<%# DataBinder.Eval(Container.DataItem, "VoucherCode")%>' runat="server" />
                                                       
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">ชื่อบัตรกำนัล</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblVoucherName" Text='<%# DataBinder.Eval(Container.DataItem, "VoucherName")%>' runat="server" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">ประเภทบัตรกำนัล</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblVoucherType" Text='<%# DataBinder.Eval(Container.DataItem, "VoucherTypeName")%>' runat="server" />
                                                </ItemTemplate>

                                            </asp:TemplateField>  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">แบรนด์</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblCampaignCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                                </ItemTemplate>

                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">จำนวนเงิน</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" Text='<%#String.Format("{0:#,###0.00}", DataBinder.Eval(Container.DataItem, "Price"))%>' runat="server" />
                                                </ItemTemplate>

                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">จำนวนบัตรกำนัล</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">วันที่เริ่มต้น</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    
                                                <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                             </ItemTemplate>

                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">วันที่สิ้นสุด</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDate" Text='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                <HeaderTemplate>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowVoucher"
                                                        class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                    <asp:HiddenField runat="server" ID="hidVoucherId" Value='<%# DataBinder.Eval(Container.DataItem, "VoucherId")%>' />
                                                    <asp:HiddenField runat="server" ID="hidVoucherCode" Value='<%# DataBinder.Eval(Container.DataItem, "VoucherCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidVoucherName" Value='<%# DataBinder.Eval(Container.DataItem, "VoucherName")%>' />
                                                    <asp:HiddenField runat="server" ID="hidVoucherTypeCode" Value='<%# DataBinder.Eval(Container.DataItem, "VoucherTypeCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidVoucherStatusCode" Value='<%# DataBinder.Eval(Container.DataItem, "StatusCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidCampaignCategoryCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryCode")%>' />
                                                    <asp:HiddenField runat="server" ID="hidPrice" Value='<%# DataBinder.Eval(Container.DataItem, "Price")%>' />
                                                    <asp:HiddenField runat="server" ID="hidAmount" Value='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' />
                                                     <asp:HiddenField runat="server" ID="hidStartDate" Value='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                                     <asp:HiddenField runat="server" ID="hidEndDate" Value='<%# DataBinder.Eval(Container.DataItem, "EndDate")%>' />
                                                     <asp:HiddenField runat="server" ID="hidRemark" Value='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' />
                                                   
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
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="modal-driver">
        <div class="modal-dialog modal-lg" style="max-width: 800px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2 ">
                            <div class="col-sm-11">
                                <div id="exampleModalLongTitle">
                                    Add Voucher
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
                                <asp:UpdatePanel ID="upModal" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">โค้ดบัตรกำนัล
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtVoucherCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblVoucherCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidVoucherCode_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                              <div class="col-sm-2"></div>  
                                         </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">ชื่อบัตรกำนัล
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtVoucherName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblVoucherName_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidVoucherName_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                               <div class="col-sm-2"></div>  
                                         </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">ประเภทบัตรกำนัล
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlVoucherType_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblVoucherType_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidVoucherType_Ins" runat="server"></asp:HiddenField>

                                            </div> 
                                             <div class="col-sm-2"></div>  
                                         </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">แบรนด์
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCampaignCategory_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblCampaignCategory_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidCampaignCategory_Ins" runat="server"></asp:HiddenField>

                                            </div> <div class="col-sm-2">
                                                 
                                             </div>
                                         </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">จำนวนเงิน
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPrice_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPrice_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidPrice_Ins" runat="server"></asp:HiddenField>

                                            </div>  
                                             <div class="col-sm-2">
                                                 บาท
                                             </div>
                                         </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">จำนวนบัตรกำนัล
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAmount_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblAmount_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidAmount_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                               <div class="col-sm-2">
                                                   <asp:CheckBox ID="chkunlimit" runat="server" />ไม่จำกัด

                                               </div>  
                                         </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">วันที่เริ่ม
                                            </label> 
                                            <div class="col-sm-8"> 
                                               <asp:TextBox ID="txtStartDate_Ins" runat="server" Format="dd/MM/yyyy" class="form-control"/>
                                    <ajaxToolkit:CalendarExtender ID="carStartDate_Ins"  runat="server" TargetControlID="txtStartDate_Ins" 
                                        PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>
                                                <asp:Label ID="lblStartDate_Ins" runat="server" class="form-control" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidStartDate_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                               <div class="col-sm-2"></div>  
                                         </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">วันที่สิ้นสุด
                                            </label>
                                            <div class="col-sm-8">
                                              <asp:TextBox ID="txtEndDate_Ins" runat="server" Format="dd/MM/yyyy"  class="form-control" />
                                    <ajaxToolkit:CalendarExtender ID="carEndDate_Ins"  runat="server" TargetControlID="txtEndDate_Ins" 
                                        PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>
                                                <asp:Label ID="lblEndDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidEndDate_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                               <div class="col-sm-2"></div>  
                                         </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">สถานะ
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblStatus_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidStatus_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                              <div class="col-sm-2"></div>  
                                         </div><div class="form-group row">
                                            <label class="col-sm-2 col-form-label">หมายเหตุ
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtRemark_Ins" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblRemark_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidRemark_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                               <div class="col-sm-2"></div>  
                                         </div>

                                        <div class="text-center m-t-12 center">

                                            <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                                runat="server" />
                                            <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                                runat="server" />

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="ReportCustomerPoint.aspx.cs" Inherits="DOMS_TSR.src.Point.ReportCustomerPoint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .validation {
            color: red;
        }
    </style>
    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvPromotion.ClientID %>");

            var cell;
            var sum = 0;
            if (grid.rows.length > 0) {
                alert("length=" + grid.rows.length);
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
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hd" runat="server" />
            <div class="page-body">
                 <div class="card">
                        <div class="card-group">
                            <div class="card" id="cardex1" runat="server">
                                <asp:LinkButton CssClass="btn-8bar-disable" ID="showSection_GetPoint" OnClick="showSection_GetPoint_Click"  runat="server">
                                    <div id="listcard1" runat="server">
                                        <div class="row">
                                            <div class="col-3 text-left p-b-15">
                                                <i class=" ti-key text-c-blue f-30"></i>
                                            </div>
                                            <div class="col-9 text-right">
                                                <h3 class="text-c-blue">
                                                    <asp:Label ID="countSection_GetPoint" runat="server"></asp:Label></h3>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 text-left">
                                                <p class=" m-0">GetPoint</p>
                                            </div>
                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>

                            <div class="card" id="cardex2" runat="server">
                                <asp:LinkButton CssClass="btn-8bar-disable2" ID="showSection_UsePoint" OnClick="showSection_UsePoint_Click"  runat="server">
                                    <div id="listcard2" runat="server">
                                        <div class="row">
                                            <div class="col-3 text-left p-b-15">
                                                <i class=" ti-user text-c-blue f-30"></i>
                                            </div>
                                            <div class="col-9 text-right">
                                                <h3 class="text-c-blue">
                                                    <asp:Label ID="countSection_UsePoint" runat="server"></asp:Label></h3>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 text-left">
                                                <p class=" m-0">UsePoint</p>
                                            </div>

                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                    
                        </div>
                    </div>
                <div id="Section_GetPoint" runat="server">
                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title"> <asp:Label ID="lblTitle" runat="server" /></div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                            <%--        <label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchProductBrand" runat="server" class="form-control"></asp:DropDownList>
                                    </div>--%>
                               
                                    <label class="col-sm-2 col-form-label">วันที่ได้รับ Point</label>
                                    <div class="col-sm-4">
                                        <div class="input-group mb-0">

                                            <asp:TextBox ID="txtSearchStartDateFrom_Get" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateFrom" runat="server" TargetControlID="txtSearchStartDateFrom_Get" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                            <asp:TextBox ID="txtSearchStartDateTo_Get" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled" onkeydown="return false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="carSearchStartDateTo" runat="server" TargetControlID="txtSearchStartDateTo_Get" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                                        </div>
                                            <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                        <asp:HiddenField ID="hidMerchantCode" runat="server" />
                                        <asp:HiddenField ID="hidBu" runat="server" />
                                        <asp:HiddenField ID="hidWfStatus" runat="server" />
                                        <asp:HiddenField ID="hidFinishFlag" runat="server" />
                                        <asp:HiddenField ID="hidLevels" runat="server" />
                                        <asp:HiddenField ID="hidFlagSavedraft" runat="server" />
                                        <asp:HiddenField ID="hidFlagApprove" runat="server" />

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
                                          
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagPromotion" runat="server" />
                                            <div class="table-responsive">
                                            <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" 
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
           

           
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">วัน/เดือน/ปี</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblCreateDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                                    
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                   
                                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อ-นามสกุล</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerFullName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFullName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">รหัสคำสั่งซื้อ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactTel" Text='<%# DataBinder.Eval(Container.DataItem, "OrderCode")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">จำนวนเงิน</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalPrice" Text='<%# DataBinder.Eval(Container.DataItem, "TotalPrice")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                         

                                     

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Pointได้รับ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                           <asp:Label ID="lblPointNum" Text='<%# DataBinder.Eval(Container.DataItem, "PointNum")%>' runat="server" />

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
                                            <div class="m-t-10"></div>
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
                        <!-- Basic Form Inputs card end -->
                    </div>
                </div>
                    </div>
                <div id="Section_UsePoint" runat="server">
                     <div class="row">
                        <div class="col-sm-12">
                          <div class="card">
                            <div class="card-header">
                                <div class="sub-title"> <asp:Label ID="lblTitle_Used" runat="server" /></div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">ประเภท</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchPointType_Used" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                     <label class="col-sm-2 col-form-label">หมวดหมู่</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchPropoint_Used" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <label class="col-sm-2 col-form-label">การกระทำ</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSearchActionCode_Used" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                   
                               
                                

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearchUsePoint" Text="ค้นหา" OnClick="btnSearch_Used_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearGetPoint" Text="ล้าง" OnClick="btnClearSearch_Used_Click"
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
                                          
                                            </div>

                                            <div class="table-responsive">
                                            <asp:GridView ID="gvUsePoint" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" 
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
           

           
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อโปรโมชัน</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="lblPromotionCode_Used" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                                    
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                   
                                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อสินค้า</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPromotionName_Used" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ประเภท</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPointTypeName_Used" Text='<%# DataBinder.Eval(Container.DataItem, "PointTypeName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ร้านค้า</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompanyNameTH_Used" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyNameTH")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">หมวดหมู่</div>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProPointName_Used" Text='<%# DataBinder.Eval(Container.DataItem, "ProPointName")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                             
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">จำนวน</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount_Used" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Pointที่ใช้</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                           <asp:Label ID="lblPointNum_Used" Text='<%# DataBinder.Eval(Container.DataItem, "PointNum")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">การกระทำ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                           <asp:Label ID="lblAction_Used" Text='<%# DataBinder.Eval(Container.DataItem, "ActionName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                    <center>
                                    <asp:Label ID="lblDataEmpty_Used" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            </div>
                                            <br />
                                            <br />
                                            <%-- PAGING CAMPAIGN --%>
                                            <div class="m-t-10"></div>
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
                                                                    <asp:Button ID="lnkbtnFirst_Used" CssClass="Button pagina_btn" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" OnCommand="GetPageIndex_Used"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre_Used" CssClass="Button pagina_btn" ToolTip="Previous" CommandName="Previous"
                                                                        Text="<" runat="server" OnCommand="GetPageIndex_Used"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_Used" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged_Used">
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPages_Used" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnNext_Used" CssClass="Button pagina_btn" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex_Used"></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLast_Used" CssClass="Button pagina_btn" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" OnCommand="GetPageIndex_Used"></asp:Button>
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
                     </div>
                </div>
                    
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    



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

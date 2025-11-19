<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="Campaign.aspx.cs" Inherits="DOMS_TSR.src.Campaign.Campaign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvCampaign.ClientID %>");

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
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="page-body">

        <div class="row">
            <div class="col-sm-12">
                <!-- Basic Form Inputs card start -->
                <div class="card">
                    <div class="card-header">
                        <div class="sub-title">ค้นหาข้อมูลแคมเปญ</div>
                    </div>
                    <div class="card-block">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>


                                <div class="form-group row">

                                    <label class="col-sm-2 col-form-label">รหัสแคมเปญ</label>
                                    <div class="col-sm-3">


                                        <asp:TextBox ID="txtSearchCampaignCode" class="form-control" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อแคมเปญ</label>
                                    <div class="col-sm-3">

                                        <asp:TextBox ID="txtSearchCampaignName" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <%--<label class="col-sm-2 col-form-label">ประเภทแคมเปญ</label>
              <div class="col-sm-3">
                     <asp:DropDownList ID="ddlSearchFlagShowProductPromotion" class="form-control" runat="server">
                                        <asp:ListItem Enabled="true" Text="โปรดเลือกแคมเปญ" Value="-99"></asp:ListItem>
                                        <asp:ListItem Text="Combo Set" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="PROMOTIONSET" Value="PROMOTION"></asp:ListItem>
                                        <asp:ListItem Text="Product" Value="PRODUCT"></asp:ListItem>
                                    </asp:DropDownList>
             </div>--%>
                                    <label class="col-sm-2 col-form-label">แบรนด์</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchCampaignCategory" runat="server" class="form-control"></asp:DropDownList>

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">สถานะ</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSearchCampaignStatus" class="form-control" runat="server">
                                            <asp:ListItem Enabled="true" Text="โปรดเลือกสถานะ" Value="-99"></asp:ListItem>
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
                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click"
                                        class="button-pri button-cancel"
                                        runat="server" />

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
                                    <asp:LinkButton ID="btnAddCampaign" class="button-action button-add m-r-5"
                                        OnClick="btnAddCampaign_Click" runat="server"><i class="fa fa-plus m-r-5 "></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                        class="button-action button-delete m-r-5" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <asp:GridView ID="gvCampaign" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                    TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvCampaign_RowCommand"
                                    ShowHeaderWhenEmpty="true">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                            <asp:CheckBox ID="chkCampaignAll" OnCheckedChanged="chkCampaignAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCampaign" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">รหัสแคมเปญ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetLink(DataBinder.Eval(Container.DataItem, "CampaignCode")) %>
                                                <%--&nbsp;&nbsp;<asp:Label ID="lblCampaignCode" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' runat="server" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ชื่อแคมเปญ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCampaignName" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left">ประเภทแคมเปญ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCampaignType" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">แบรนด์</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCampainCategory" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">สถานะ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCampaignStatus" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGNSTATUS")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">Start Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">Notice Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:Label ID="lblNotifyDate" Text='<%# ((null == Eval("NotifyDate"))||("" == Eval("NotifyDate"))) ? string.Empty : DateTime.Parse(Eval("NotifyDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">End Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblExpireDate" Text='<%# ((null == Eval("ExpireDate"))||("" == Eval("ExpireDate"))) ? string.Empty : DateTime.Parse(Eval("ExpireDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowCampaign"
                                                    class="button-activity m-r-10  "
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidCampaignId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignId")%>' />
                                                <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidCampaignName" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' />
                                                <asp:HiddenField runat="server" ID="hidCampaignCategory" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategory")%>' />
                                                <asp:HiddenField runat="server" ID="hidCampaignType" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignType")%>' />
                                                <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                                <asp:HiddenField runat="server" ID="hidFlagShowProductPromotion" Value='<%# DataBinder.Eval(Container.DataItem, "FlagShowProductPromotion")%>' />
                                                <asp:HiddenField runat="server" ID="hidPictureCampaingUrl" Value='<%# DataBinder.Eval(Container.DataItem, "PictureCampaignUrl")%>' />
                                                <asp:HiddenField runat="server" ID="hidActive" Value='<%# DataBinder.Eval(Container.DataItem, "Active")%>' />
                                                <asp:HiddenField runat="server" ID="hidStartDate" Value='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                                <asp:HiddenField runat="server" ID="hidNotifyDate" Value='<%# DataBinder.Eval(Container.DataItem, "NotifyDate")%>' />
                                                <asp:HiddenField runat="server" ID="hidExpireDate" Value='<%# DataBinder.Eval(Container.DataItem, "ExpireDate")%>' />

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
      <!-- Basic Form Inputs card end -->
                                </div>
  </div>
</div>
          
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                            aria-hidden="true" id="modal-campaign">
                            <div class="modal-dialog modal-lg" style="max-width: 1300px;">
                                <div class="modal-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="modal-header modal-header2  p-l-0 ">
                                                <div class="col-sm-12">
                                                    <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มแคมเปญ</div>

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

                                                                <label class="col-sm-2 col-form-label">แบรนด์</label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="ddlSearchCampaignCategory_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                                    <asp:Label ID="lblCampaignCategory_Ins" runat="server" CssClass="validation"></asp:Label>
                                                                </div>
                                                                <label class="col-sm-1 col-form-label"></label>
                                                                <label class="col-sm-2 col-form-label">รหัสแคมเปญ</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtCampaignCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                                                    <asp:Label ID="lblCampaignCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                                                    <asp:HiddenField ID="hidCampaign_Ins" runat="server"></asp:HiddenField>
                                                                </div>
                                                            </div>
                                                            <!-- <label class="col-sm-1 col-form-label"></label>
                                        <label class="col-sm-2 col-form-label">ประเภทสินค้า</label>
                                        <div class="col-sm-3">
                                            <select name="select" class="form-control" [ngModelOptions]="{standalone: true}">
                                                <option value="opt1">Select One Value Only</option>
                                                <option value="opt2">Type 2</option>
                                                <option value="opt3">Type 3</option>
                                                <option value="opt4">Type 4</option>
                                                <option value="opt5">Type 5</option>
                                                <option value="opt6">Type 6</option>
                                                <option value="opt7">Type 7</option>
                                                <option value="opt8">Type 8</option>
                                            </select>
                                        </div> -->

                                                            <!-- <label class="col-sm-1 col-form-label"></label>
                                        <label class="col-sm-2 col-form-label">ขนาดบรรจุภัณฑ์(ซม)</label>
                                        <div class="col-sm-1">
                                            <input type="text" class="form-control " placeholder="ก" [ngModelOptions]="{standalone: true}">
                                        </div>
                                        <div class="col-sm-1">
                                            <input type="text" class="form-control"placeholder="ย" [ngModelOptions]="{standalone: true}">
                                        </div>
                                        <div class="col-sm-1">
                                            <input type="text" class="form-control"placeholder="ส" [ngModelOptions]="{standalone: true}">
                                            
                                        </div> -->
                                                            <div class="form-group row">
                                                                <label class="col-sm-2 col-form-label">ชื่อแคมเปญ</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtCampaignName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                                    <asp:Label ID="lblCampaignName_Ins" runat="server" CssClass="validation"></asp:Label>
                                                                </div>
                                                                <!-- <label class="col-sm-1 col-form-label"></label>
                                        <label class="col-sm-2 col-form-label">ตัวเลือกเพิ่มเติม</label>
                                        <div class="col-sm-3">
                                            <select name="select" class="form-control" [ngModelOptions]="{standalone: true}">
                                                <option value="opt1">Select One Value Only</option>
                                                <option value="opt2">Type 2</option>
                                                <option value="opt3">Type 3</option>
                                                <option value="opt4">Type 4</option>
                                                <option value="opt5">Type 5</option>
                                                <option value="opt6">Type 6</option>
                                                <option value="opt7">Type 7</option>
                                                <option value="opt8">Type 8</option>
                                            </select>
                                        </div> -->
                                                                <label class="col-sm-1 col-form-label"></label>
                                                                <label class="col-sm-2 col-form-label">สถานะ</label>
                                                                <div class="col-sm-3">
                                                                    <%--<asp:DropDownList ID="ddlFlagShowProductPromotion_Ins" class="form-control" runat="server">
                                        <asp:ListItem Enabled="true" Text="โปรดเลือกแคมเปญ" Value="-99"></asp:ListItem>
                                        <asp:ListItem Text="Combo Set" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="Promotion Set" Value="PROMOTION"></asp:ListItem>
                                        <asp:ListItem Text="Product" Value="PRODUCT"></asp:ListItem>
                                    </asp:DropDownList> --%>

                                                                    <asp:DropDownList ID="ddlActive_Ins" class="form-control" runat="server">
                                                                        <asp:ListItem Enabled="true" Text="โปรดเลือกสถานะ" Value="-99"></asp:ListItem>
                                                                        <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                                                        <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblActive_Ins" runat="server" CssClass="validation"></asp:Label>
                                                                    <%--<asp:Label ID="lblFlagShowProductPromotion_Ins" runat="server" CssClass="validation"></asp:Label>--%>
                                                                </div>

                                                                <%--<label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">Start Date</label>
                                  <div class="col-sm-3">
                                    <!--<input type="text" class="form-control" [(ngModel)]="txtTransportPrice" [ngModelOptions]="{standalone: true}">-->
                                      
                                   <asp:TextBox ID="txtStartDate_Ins" runat="server" style="width:100%" />
                                    <ajaxToolkit:CalendarExtender ID="carStartDate" runat="server" TargetControlID="txtStartDate_Ins" 
                                        PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>
                                      
                                        <asp:Label ID="lblStartDate_Ins" runat="server" CssClass="validation"></asp:Label>
                                
                                  </div>

                                       <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">End Date</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtExpireDate_Ins" runat="server" style="width:100%" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExpireDate_Ins" 
                                        PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>
                                        <asp:Label ID="lblExpireDate_Ins" runat="server" CssClass="validation"></asp:Label>                               
                                  </div>--%>

                                                                <!-- <label class="col-sm-1 col-form-label"></label>
                                        <label class="col-sm-2 col-form-label">ประเภทการขนส่ง
                                        </label>
                                        <div class="col-sm-3">
                                            <select name="select" class="form-control" [ngModelOptions]="{standalone: true}">
                                                <option value="opt1">Select One Value Only</option>
                                                <option value="opt2">Type 2</option>
                                                <option value="opt3">Type 3</option>
                                                <option value="opt4">Type 4</option>
                                                <option value="opt5">Type 5</option>
                                                <option value="opt6">Type 6</option>
                                                <option value="opt7">Type 7</option>
                                                <option value="opt8">Type 8</option>
                                            </select>
                                        </div>    -->

                                                                <!-- <div class="form-group row">
                                           
                                            <label class="col-sm-2 col-form-label">รายละเอียด</label>
                                            <div class="col-sm-3">
                                                <textarea rows="5" cols="5" class="form-control"
                                             placeholder="ใส่รายละเอียดเพิ่มเติม"></textarea>
                                            </div>
                                    </div> -->
                                                            </div>
                                                            <%--<div class="form-group row">
                                  <label class="col-sm-2 col-form-label">สถานะ</label>
                                  <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlActive_Ins" class="form-control" runat="server">
                                        <asp:ListItem Enabled="true" Text="โปรดเลือกสถานะ" Value="-99"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                      <asp:Label ID="lblActive_Ins" runat="server" CssClass="validation"></asp:Label>
                                      <asp:TextBox ID="txtNotifyDate_Ins" runat="server" style="width:100%" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtNotifyDate_Ins" 
                                        PopupButtonID="Image3">
                                    </ajaxToolkit:CalendarExtender> 
                                      <asp:Label ID="lblNotifyDate_Ins" runat="server" CssClass="validation"></asp:Label>
                               
                                  </div>
                                             
                                  </div> --%>
                                                            <input type="hidden" id="hidFileNane" runat="server" />
                                                            <input type="hidden" id="hidPictureCampaignUrl_Ins" runat="server" />

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <div class="form-group row">
                                                        <label class="col-sm-2 col-form-label">รูปภาพ</label>
                                                        <div class="col-sm-8">
                                                            <input type="file" name="files[]" id="filer_input1" multiple="multiple">
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
</asp:Content>

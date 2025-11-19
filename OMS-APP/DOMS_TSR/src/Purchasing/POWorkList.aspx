<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="POWorkList.aspx.cs" Inherits="DOMS_TSR.src.Purchasing.POWorkList" %>
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
            <div class="sub-title" >ค้นหาข้อมูลใบสั่งซื้อ</div>
            </div>
            <div class="card-block">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
               <ContentTemplate>

                 
            <div class="form-group row">
            <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />   
            <asp:HiddenField ID="hidSupplierIdIns" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidApprover1" runat="server" />
            <asp:HiddenField ID="hidworkflowstatus" runat="server" />
            <asp:HiddenField ID="hidwftaskliststatus" runat="server" />
              <label class="col-sm-2 col-form-label">เลขที่ใบสั่งซื้อ</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchPOCode" class="form-control" runat="server"></asp:TextBox>
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">ผู้ขาย</label>
              <div class="col-sm-3">
                  <asp:DropDownList ID="ddlSearchSupplier" runat="server" class="form-control"></asp:DropDownList> 
              </div>

                <label class="col-sm-2 col-form-label">วันที่สร้าง</label>
                 <div class="col-sm-3">
                    <div class="input-group mb-0">

                        <asp:TextBox ID="txtSearchPODateFrom" class="form-control" placeholder="ตั้งแต่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="carSearchStartDateFrom" runat="server" TargetControlID="txtSearchPODateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                        <asp:TextBox ID="txtSearchPODateTo" class="form-control" placeholder="ถึง" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="carSearchStartDateTo" runat="server" TargetControlID="txtSearchPODateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>

                    </div>

                 </div>

                <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">คลังสินค้า</label>
                <div class="col-sm-3">
                     <asp:DropDownList ID="ddlSearchInventory" runat="server" class="form-control"></asp:DropDownList>
                </div>

              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">สร้างโดย</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchCreateByNameTH" class="form-control" runat="server"></asp:TextBox>
    
               </div>
             </div>

                   

              <div class="text-center m-t-20 col-sm-12">
                  <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" CssClass="button-pri button-accept m-r-10" runat="server"/>
                  <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" CssClass="button-pri button-cancel" runat="server" />   
              </div>        
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
      </div>

      <div class="card">
         <div class="card-block">

             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <div class="dt-responsive table-responsive">
                      <asp:GridView ID="gvPOWorkList" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" OnRowCommand="gvPOWorkList_RowCommand"
                            TabIndex="0" Width="100%" CellSpacing="0"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chPOAll" OnCheckedChanged="chkPOAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPO" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">วันที่สร้าง</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPODateDate" Text='<%# Eval("PODate", "{0:D}")%>'  runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">เลขที่ใบสั่งซื้อ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblPOCode" Text='<%# DataBinder.Eval(Container.DataItem, "POCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>                               

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">ชื่อผู้ขาย</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">คลังสินค้า</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblInventoryName" Text='<%# DataBinder.Eval(Container.DataItem, "InventoryName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">จำนวนเงิน</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:n}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">สถานะใบสั่งซื้อ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "StatusName")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">สร้างโดย</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblCreateBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreateByNameTH")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">วันที่แก้ไขล่าสุด</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblUpdateDate" Text='<%# DataBinder.Eval(Container.DataItem, "UpdateDate", "{0:D}")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">แก้ไขโดย</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblUpdateBy" Text='<%# DataBinder.Eval(Container.DataItem, "UpdateByNameTH")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate  >
                                          <asp:LinkButton ID="btnEdit" runat="Server" CommandName="EditPOStatus"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                        

                                        <asp:HiddenField runat="server" ID="hidPOId" Value='<%# DataBinder.Eval(Container.DataItem, "POId")%>' />
                                        <asp:HiddenField runat="server" ID="hidPOCode" Value='<%# DataBinder.Eval(Container.DataItem, "POCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidWFStatusCode" Value='<%# DataBinder.Eval(Container.DataItem, "WFStatusCode")%>' />
                                  
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
             
         </div>
          <div class="card-block">
          <div class="text-center col-md-12  ">              
              <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnRevise" runat="server" OnClick="btnRevise_Click" Text="Revise" class="button-pri button-accept m-r-5" />
              <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" Text="Reject" class="button-pri button-accept m-r-5" />
            </div>
          </div>
      </div>

  </div>
</div>

</asp:Content>
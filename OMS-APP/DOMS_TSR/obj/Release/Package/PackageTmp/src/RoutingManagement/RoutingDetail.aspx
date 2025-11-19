
<%@ Page Language="C#"  MasterPageFile="~/src/MasterPage/Web.master"  AutoEventWireup="true" CodeBehind="RoutingDetail.aspx.cs" Inherits="DOMS_TSR.src.RoutingManagement.RoutingDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

   <script type="text/javascript">

        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvRouting.ClientID %>");

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

    <input type="hidden" id="hidIdList" runat="server" />
    <input type="hidden" id="hidFlagInsert" runat="server" />
    <asp:HiddenField ID="hidFlagDel" runat="server" />
    <input type="hidden" id="hidaction" runat="server" />
    <asp:HiddenField ID="hidMsgDel" runat="server" />
    <asp:HiddenField ID="hidEmpCode" runat="server" />
    <asp:HiddenField ID="hd" runat="server" />

<div class="page-body">

    <div class="row">
      <div class="col-sm-12">
  
         <!-- START 1st Card -->
         <div class="card">
            <div class="card-header">
         ข้อมูลสายส่ง
            </div>
            <div class="card-block">
              <div class="view-info">
                <div class="row">
                  <div class="col-lg-12">
                    <div class="general-info">
    
                      <div class="row">
    
                        <div class="col-sm-12 col-lg-12 col-xl-6">
                          <div class="table-responsive">
                            <table class="table m-0">
                              <tbody>
                                <tr>
                                  <th scope="row">รหัสสายส่ง</th>
                                  <td><asp:Label ID="lblRoutingCode" runat="server"></asp:Label></td>
                              </tbody>
                            </table>
                          </div>
                        </div>
    
                        <div class="col-sm-12 col-lg-12 col-xl-6">
                          <div class="table-responsive">
                            <table class="table m-0">
                              <tbody>
                                <tr>
                                  <th scope="row">ชื่อสายส่ง</th>
                                  <td><asp:Label ID="lblRoutingName" runat="server"></asp:Label></td>
                                </tr>
                              </tbody>
                            </table>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- END 1st Card -->
          
        <!-- Basic Form Inputs card start -->
        <div class="card">
          <div class="card-header">
            <div class="sub-title" >ค้นหาข้อมูลเส้นทาง</div>
          </div>
          <div class="card-block">
            
              <div class="form-group row">
                <label class="col-sm-2 col-form-label">ตำบล/แขวง</label>
                <div class="col-sm-3">
                     <asp:TextBox  ID="txtSearchSubDistrict" class="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">อำเภอ/เขต</label>
                <div class="col-sm-3">
                    <asp:TextBox  ID="txtSearchDistrict" class="form-control" runat="server"></asp:TextBox>              
                </div>

                <label class="col-sm-2 col-form-label">จังหวัด</label>
                <div class="col-sm-3">
                       <asp:TextBox  ID="txtSearchProvince" class="form-control" runat="server"></asp:TextBox>   
                </div>
                <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">รหัสไปรษณีย์</label>
                <div class="col-sm-3">
                    <asp:TextBox  ID="txtSearchPostCode" class="form-control" runat="server"></asp:TextBox>  
                </div>
              </div>
              <div class="form-group row">
                <label class="col-sm-5 col-form-label"></label>
                <div class="text-center m-t-20">
                    <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"
                      class="button-pri button-accept m-r-10"
                      runat="server" />
                     <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click"
                      class="button-pri button-cancel"
                      runat="server" />
                </div>
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
                   <asp:LinkButton id="btnAddProduct" class="button-action button-add" data-backdrop="false"
                       OnClick="btnAddRouting_Click" runat ="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                 <asp:LinkButton ID="btnDelete"  OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"   
                      class="button-action button-delete"    runat="server" ><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                   </div>
                  
                  
                      <asp:GridView ID="gvRouting" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvRouting_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkRoutingAll" OnCheckedChanged="chkRoutingAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkRouting" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

            

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ตำบล/แขวง</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubDistrictName" Text='<%# DataBinder.Eval(Container.DataItem, "SubDistrictName")%>' runat="server" />
                         
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">อำเภอ/เขต</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblDistrictName" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">จังหวัด</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lblProvinceName" Text='<%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>' runat="server" />
                                   
                         
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รหัสไปรษณีย์</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblPostCode" Text='<%# DataBinder.Eval(Container.DataItem, "PostCode")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >

                     <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowRouting"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>



                                        <asp:HiddenField runat="server" ID="hidRoutingDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "RoutingDetailId")%>' />
                                        <asp:HiddenField runat="server" ID="hidProvinceCode" Value='<%# DataBinder.Eval(Container.DataItem, "ProvinceCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidDistrictCode" Value='<%# DataBinder.Eval(Container.DataItem, "DistrictCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidSubDistrictCode" Value='<%# DataBinder.Eval(Container.DataItem, "SubDistrictCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidPostCode" Value='<%# DataBinder.Eval(Container.DataItem, "PostCode")%>' />

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
              <div class="modal-box-s">
                <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                    <div class="modal-header modal-header2 ">
                        <div class="col-sm-11">
                        <div id="exampleModalLongTitle">เพิ่มสายส่ง
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
                                    <label class="col-sm-2 col-form-label">จังหวัด</label>
                                    <div class="col-sm-3">
                                  <asp:DropDownList ID="ddlProvince_Ins" runat="server"  OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                      <asp:Label id="lblProvince_Ins" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">อำเภอ/เขต</label>
                                    <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlDistrict_Ins" runat="server"  OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                      <asp:Label id="lblDistrict_Ins" runat="server"></asp:Label>
                                    </div>

                                    <label class="col-sm-2 col-form-label">ตำบล/แขวง</label>
                                    <div class="col-sm-3">
                                     <asp:DropDownList ID="ddlSubDistrict_Ins" runat="server"  AutoPostBack="true"></asp:DropDownList>
                                      <asp:Label id="lblSubDistrict_Ins" runat="server"></asp:Label>
                                </div>
                                <label class="col-sm-1 col-form-label"></label>
                                <label class="col-sm-2 col-form-label">รหัสไปรษณีย์</label>
                                <div class="col-sm-3">
                                 <asp:TextBox id="txtPostCode_Ins" runat="server"></asp:TextBox>
                                 <asp:Label id="lblPostCode_Ins" runat="server"></asp:Label>
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

                               </ContentTemplate>
                </asp:UpdatePanel>
                   
          
                                     
                    </div>

                </div>
                </div>
               </div>
            </div>
          </div>
            </div>
          </div>

    

</asp:Content>


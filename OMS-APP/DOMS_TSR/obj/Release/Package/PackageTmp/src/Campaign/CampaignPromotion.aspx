<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="CampaignPromotion.aspx.cs" Inherits="DOMS_TSR.src.Campaign.CampaignPromotion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
     <script type="text/javascript">
//    <style>
//        #drop-zone {
//  width: 100%;
//  min-height: 150px;
//  border: 3px dashed rgba(0, 0, 0, .3);
//  border-radius: 5px;
//  font-family: Arial;
//  text-align: center;
//  position: relative;
//  font-size: 20px;
//  color: #7E7E7E;
//}
//#drop-zone input {
//  position: absolute;
//  cursor: pointer;
//  left: 0px;
//  top: 0px;
//  opacity: 0;
//}
///*Important*/

//#drop-zone.mouse-over {
//  border: 3px dashed rgba(0, 0, 0, .3);
//  color: #7E7E7E;
//}
///*If you dont want the button*/

//#clickHere {
//  display: inline-block;
//  cursor: pointer;
//  color: white;
//  font-size: 17px;
//  width: 150px;
//  border-radius: 4px;
//  background-color: #4679BD;
//  padding: 10px;
//}
//#clickHere:hover {
//  background-color: #376199;
//}
//#filename {
//  margin-top: 10px;
//  margin-bottom: 10px;
//  font-size: 14px;
//  line-height: 1.5em;
//}
//.file-preview {
//  background: #ccc;
//  border: 5px solid #fff;
//  box-shadow: 0 0 4px rgba(0, 0, 0, 0.5);
//  display: inline-block;
//  width: 60px;
//  height: 60px;
//  text-align: center;
//  font-size: 14px;
//  margin-top: 5px;
//}
//        .closeBtn:hover {
//            color: red;
//            display: inline-block;
//        }
//    </style>
         function DeleteConfirm() {
            var grid = document.getElementById("<%= gvCampaignPromotion.ClientID %>");

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
         function redirect() {
         window.open('Campaign.aspx');
         }       
</script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
    <input type="hidden" id="hidFlagInsert" runat="server" />
        <asp:HiddenField ID="hidFlagDel" runat="server" />
        <input type="hidden" id="hidaction" runat="server" />
        <asp:HiddenField ID="hidMsgDel" runat="server" />
        <asp:HiddenField ID="hidEmpCode" runat="server" />
        <input type="hidden" id="hidIdList" runat="server" />

  <!-- Page body start -->
          <div class="page-body">
            <div class="row">
              <div class="col-sm-12">

                <div class="card">
                  <div class="card-header">

                    <div class="sub-title ">Campaign Information
                    </div>
                  </div>
                  <div class="card-block">
                       <div class="col-sm-12">
                           
                                  <div class="row">
                                    
                
                                        <div class="col-lg-12 col-xl-6 col-sm-12">
                                                <table class="table-detail m-0" style="width:100%">
                                                          <tbody>
                                                              <tr>
                                                                  <th scope="row">รหัสแคมเปญ</th>
                                                                  <td><asp:Label runat="server" ID="lblCampaignCode"></asp:Label></td>
                                                              </tr>

                                                              <tr style="display:none">
                                                                  <th scope="row">Campaign Type</th>
                                                                  <td><asp:Label runat="server" ID="lblCampaignFormat"></asp:Label>
                                                                  </td>                
                                                              </tr>
                                                              
                                     
                                                              <tr>
                                                                  <th scope="row">ชื่อแคมเปญ</th>
                                                                  <td><asp:Label runat="server" ID="lblCampaignName"></asp:Label>
                                                                      <asp:Label Visible="false" runat="server" ID="lblFlagComboset"></asp:Label>
                                                                      <asp:Label Visible="false" runat="server" ID="lbl"></asp:Label>
                                                                          <asp:Label Visible="false" runat="server" ID="camptype"></asp:Label>
                                                                  </td>
                
                                                              </tr>
                                                                  <tr>
                                                                  <th scope="row">แบรนด์</th>
                                                                  <td><asp:Label runat="server" ID="lblbrand"></asp:Label></td>
                                                              </tr>
                                                                           
                                                              <tr>
                                                                  <th scope="row">สถานะ</th>
                                                                  <td><asp:Label runat="server" ID="lblActive"></asp:Label>
                                                                  </td>                
                                                              </tr>
                                                              <%--<tr>
                                                                  <th scope="row">Notice Date</th>
                                                                  <td><asp:Label runat="server" ID="lblNotifyDate"></asp:Label>
                                                                  </td>
                                                              </tr>                                                             
                                                              <tr>
                                                                  <th scope="row">Status</th>
                                                                  <td><asp:Label runat="server" ID="lblActive"></asp:Label>
                                                                  </td>
                                                              </tr>--%>
                                                          </tbody>
                                                      </table>           
                                                  </div>
                                                  <div class="col-lg-12 col-xl-6 col-sm-12  " id="big_banner port_big_img " >
                                             
                                                        <img class="img thumb-post" src="1" runat="server" id="CampaignPicIm"  style="height: 100%; width: 100%; object-fit: contain" alt="">
                                                   </div> 
                                                  <div class="col-lg-12 col-xl-12 col-sm-12">     
                                                        <div class="text-center m-t-20 col-sm-12 ">
                                                            <asp:Button ID="btnBackLink" runat="server" Text="ย้อนกลับ" class="button-pri button-accept" onclientclick='redirect();' />
                                                                                 
                                                                           </div>
                                                      </div>
                                             
                                             
                                            </div>
                                          </div>
                                        </div>
                                      </div>

                  <div class="card">
        <div class="card-header">
          <div class="sub-title" >ค้นหาโปรโมชัน</div>
        </div>
        <div class="card-block">  
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
               <ContentTemplate>                
            <div class="form-group row">
              <label class="col-sm-2 col-form-label">รหัสโปรโมชั่น</label>
              <div class="col-sm-3">
                <asp:TextBox  ID="txtSearchCampaignPromotionCode" class="form-control" runat="server"></asp:TextBox>                
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">ชื่อโปรโมชั่น</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchCampaignPromotionName" class="form-control" runat="server"></asp:TextBox>            
              </div>
                <label class="col-sm-2 col-form-label">ระดับโปรโมชั่น</label>
              <div class="col-sm-3">
                <asp:DropDownList ID="ddlSearchPromotionLevel" class="form-control" runat="server"></asp:DropDownList>                
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">โปรโมชั่น(เรื่มต้น)</label>
              <div class="col-sm-3">
                  <asp:TextBox ID="txtPromotionStartDate" runat="server" style="width:100%" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPromotionStartDate" 
                                        PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>           
              </div>

                <label class="col-sm-2 col-form-label">โปรโมชั่น(สิ้นสุด)</label>
              <div class="col-sm-3">
                <asp:TextBox ID="txtSearchPromotionEndDate" runat="server" style="width:100%" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSearchPromotionEndDate" 
                                        PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>                
                  </div>
             
              
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label"></label>
              <div class="col-sm-3">
              </div>

            </div>           
              <div class="text-center m-t-20 col-sm-12">
                  <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearchCampaignPromotion_Click"
                      class="button-pri button-accept m-r-10" 
                      runat="server" />
                     <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearchCampaignPromotion_Click"
                      class="button-pri button-accept"
                      runat="server" />              
              </div>          
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
          </div>

<div class="card">
   <div class="card-block">

       <div class="m-b-10">
                    <!--Start modal Add Promotion-->
             
                   <asp:LinkButton id="btnAddPromotion" class="button-action button-add m-r-5" 
                       OnClick="btnAddPromotion_Click" runat ="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                 <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"    
                      class="button-action button-delete m-r-5"    runat="server" ><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                   
               
                      </div>

     
               
                      <asp:GridView ID="gvCampaignPromotion" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvCampaignPromotion_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkCampaignPromotionAll" OnCheckedChanged="chkCampaignPromotionAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkCampaignPromotion" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="Center">รหัสโปรโมชั่น</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        
                                        <!--&nbsp;&nbsp;<asp:Label ID="lblPromotionCode" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="server" />-->
                                        <asp:LinkButton ID="btnPromotionCode" runat="Server" CommandName="ShowPromotionDetail"
                                           style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>'> </asp:LinkButton>  

                                        <asp:HiddenField runat="server" ID="hidCampaignPromotionId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignPromotionId")%>' />
                                        <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="Center">ชื่อโปรโมชั่น</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        
                                        <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="Center">ระดับโปรโมชั่น</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        
                                      <asp:Label ID="lblPromotionLevel" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionLevel")%>' runat="server" />
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">โปรโมชั่น(เรื่มต้น)</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblStartDate" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">โปรโมชั่น(สิ้นสุด)</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblEndDate" Text='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>                                

                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >                       

                                        <asp:HiddenField runat="server" ID="hidCampaignId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignId")%>' />
                                        <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidCampaignName" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' />
                                         <asp:HiddenField runat="server" ID="hidCampaignCategory" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategory")%>' />
                                         <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                         <asp:HiddenField runat="server" ID="hidFlagShowProductPromotion" Value='<%# DataBinder.Eval(Container.DataItem, "FlagShowProductPromotion")%>' />
                                         <asp:HiddenField runat="server" ID="hidPictureCampaingURL" Value='<%# DataBinder.Eval(Container.DataItem, "PictureCampaingURL")%>' />
                                        <asp:HiddenField runat="server" ID="hidStartDate" Value='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                        <asp:HiddenField runat="server" ID="hidNotifyDate" Value='<%# DataBinder.Eval(Container.DataItem, "NotifyDate")%>' />
                                        <asp:HiddenField runat="server" ID="hidExpireDate" Value='<%# DataBinder.Eval(Container.DataItem, "ExpireDate")%>' />
   
                                        <br />
                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>--%>

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
                    aria-hidden="true" id="modal-campaignpromotion">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                            <div class="modal-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="modal-header modal-header2  p-l-0 ">
                                                <div class="col-sm-12">
                                                    <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px; ">เพิ่มโปรโมชั่น</div>
                                                    
                                                </div>
                                               <span><button type="button" class="close  " style="padding-left:0px; padding-right:0px;" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                </button> </span>  
                                            </div>
                                        </div>
                                    </div>
<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
           <div class="modal-body">
        <div class="card-block">
            <div class="form-group row ">
                <label class="col-sm-2 col-form-label">รหัสโปรโมชั่น</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtPromotionCode_Search" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <label class="col-sm-2 offset-1 col-form-label">ชื่อโปรโมชั่น</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtPromotionName_Search" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-2 col-form-label">ระดับโปรโมชั่น</label>
              <div class="col-sm-3">
                <asp:DropDownList ID="ddlSearchPromotionLevelgvAdd" class="form-control" runat="server"></asp:DropDownList>                
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">โปรโมชั่น(เรื่มต้น)</label>
              <div class="col-sm-3">
                  <asp:TextBox ID="txtSearchPromotionStartDategvAdd" runat="server" style="width:100%" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchPromotionStartDategvAdd" 
                                        PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>           
              </div>
                <label class="col-sm-2 col-form-label">โปรโมชั่น(สิ้นสุด)</label>
              <div class="col-sm-3">
                <asp:TextBox ID="txtSearchPromotionEndDategvAdd" runat="server" style="width:100%" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSearchPromotionEndDategvAdd" 
                                        PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>                
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label"></label>
              <div class="col-sm-3">
              </div>
            </div>
          <div class="text-center m-t-20 center">
                          
                                      <asp:Button ID="btnSearchAddPromotion" Text="ค้นหา" OnClick="btnSearchAddPromotion_Click"
                      class="button-pri button-accept m-r-10"
                      runat="server" />
                     <asp:Button ID="btnClearSearchAddPromotion" Text="ล้าง" OnClick="btnClearSearchAddPromotion_Click"
                      class="button-pri button-cancel"
                      runat="server" />

                                </div>
          <div class="m-b-10">
              <%--<asp:button ID="AddPromotionSelect" Text="Add Select Promotion" OnClick="InsertCampaignPromotionfromSelect_Click" runat="server" />--%>
              <asp:Label ID="lblsample" runat="server"></asp:Label>

          </div>          
                            <asp:GridView ID="gvAddPromotion" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvAddPromotionSelect_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                           <asp:CheckBox ID="chkPromotionAll" OnCheckedChanged="chkPromotionAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkPromotion" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="left">Promotion Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       
                                        <asp:Label ID="lblPromotionCode" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="server" />
                                        <asp:HiddenField runat="server" ID="hidPromotionId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionId")%>' />
                                        <asp:HiddenField runat="server" ID="hidPromotionCode" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' />                
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">Promotion Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">Promotion Level</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblPromotionLeveofgvAdd" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionLevel")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">Start Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblStartDategvAdd" Text='<%# ((null == Eval("StartDate"))||("" == Eval("StartDate"))) ? string.Empty : DateTime.Parse(Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">End Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblEndDategvAdd" Text='<%# ((null == Eval("EndDate"))||("" == Eval("EndDate"))) ? string.Empty : DateTime.Parse(Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddPromotion"
                                                                    class="button-activity"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-16"></span></asp:LinkButton>
                                        <br />
                                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >

                     <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowCampaign"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>                       

                                        <asp:HiddenField runat="server" ID="hidCampaignId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignId")%>' />
                                        <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidCampaignName" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' />
                                         <asp:HiddenField runat="server" ID="hidCampaignCategory" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategory")%>' />
                                         <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                         <asp:HiddenField runat="server" ID="hidFlagShowProductPromotion" Value='<%# DataBinder.Eval(Container.DataItem, "FlagShowProductPromotion")%>' />
                                         <asp:HiddenField runat="server" ID="hidPictureCampaingURL" Value='<%# DataBinder.Eval(Container.DataItem, "PictureCampaingURL")%>' />
                                        <asp:HiddenField runat="server" ID="hidStartDate" Value='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                        <asp:HiddenField runat="server" ID="hidNotifyDate" Value='<%# DataBinder.Eval(Container.DataItem, "NotifyDate")%>' />
                                        <asp:HiddenField runat="server" ID="hidExpireDate" Value='<%# DataBinder.Eval(Container.DataItem, "ExpireDate")%>' />
   
                                        <br />
                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>--%>

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
                                                <asp:Button ID="linkbtnPromoFirst" CssClass="Button" ToolTip="PromoFirst" CommandName="PromoFirst"
                                                    Text="<<" runat="server" OnCommand="GetPromoPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="linkbtnPromoPre" CssClass="Button" ToolTip="PromoPrevious" CommandName="PromoPrevious"
                                                    Text="<" runat="server" OnCommand="GetPromoPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPromoPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                      OnSelectedIndexChanged="ddlPromoPage_SelectedIndexChanged" >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblPromoTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="linkbtnPromoNext" CssClass="Button" ToolTip="PromoNext" runat="server" CommandName="PromoNext" Text=">" OnCommand="GetPromoPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="linkbtnPromoLast" CssClass="Button" ToolTip="PromoLast" runat="server" CommandName="PromoLast"
                                                    Text=">>" OnCommand="GetPromoPageIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>


        </div>
      </div>
</ContentTemplate>
    </asp:UpdatePanel>
        
        </div>
      </div>
        </div>

   </div>
</div>

                  <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                    aria-hidden="true" id="modal-promotiondetail">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                      <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle01">Promotion Detail
                                </div>
                              </div>
                              <div class="col-sm-1">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                              </div>
                            </div>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
           <div class="modal-body">
        <div class="card-block">           
                            <asp:GridView ID="gvPromotionDetail" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCreated="gvPromotionDetail_RowCreated"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkPromotionAll" OnCheckedChanged="chkPromotionAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkPromotion" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="left">Comboset Code</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--<%# GetLinktoCombo(DataBinder.Eval(Container.DataItem, "CombosetCode")) %>--%>
                                                                <asp:Label ID="lblCombosetCode" Text='<%# DataBinder.Eval(Container.DataItem, "CombosetCode")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="Center">Comboset Name</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCombosetName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailName")%>' runat="server" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Product Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>                                       
                                       <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                        <asp:HiddenField runat="server" ID="hidPromotionDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "PromotionDetailInfoId")%>' />              
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Product Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Brand</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblProductBrandName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductBrandName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Product Category</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblProductCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Unit</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblUnitName" Text='<%# DataBinder.Eval(Container.DataItem, "UnitName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Price (Baht)</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Channel</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblChannelName" Text='<%# DataBinder.Eval(Container.DataItem, "ChannelName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >

                     <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowCampaign"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>                       

                                        <asp:HiddenField runat="server" ID="hidCampaignId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignId")%>' />
                                        <asp:HiddenField runat="server" ID="hidCampaignCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidCampaignName" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignName")%>' />
                                         <asp:HiddenField runat="server" ID="hidCampaignCategory" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategory")%>' />
                                         <asp:HiddenField runat="server" ID="hidFlagComboSet" Value='<%# DataBinder.Eval(Container.DataItem, "FlagComboSet")%>' />
                                         <asp:HiddenField runat="server" ID="hidFlagShowProductPromotion" Value='<%# DataBinder.Eval(Container.DataItem, "FlagShowProductPromotion")%>' />
                                         <asp:HiddenField runat="server" ID="hidPictureCampaingURL" Value='<%# DataBinder.Eval(Container.DataItem, "PictureCampaingURL")%>' />
                                        <asp:HiddenField runat="server" ID="hidStartDate" Value='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                        <asp:HiddenField runat="server" ID="hidNotifyDate" Value='<%# DataBinder.Eval(Container.DataItem, "NotifyDate")%>' />
                                        <asp:HiddenField runat="server" ID="hidExpireDate" Value='<%# DataBinder.Eval(Container.DataItem, "ExpireDate")%>' />
   
                                        <br />
                                        <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>--%>

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
                                                <asp:Button ID="linkbtnDetailFirst" CssClass="Button" ToolTip="DetailFirst" CommandName="DetailFirst"
                                                    Text="<<" runat="server" OnCommand="GetDetailPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="linkbtnDetailPrevious" CssClass="Button" ToolTip="DetailPrevious" CommandName="DetailPrevious"
                                                    Text="<" runat="server" OnCommand="GetDetailPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlDetailPage" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                      OnSelectedIndexChanged="ddlDetailPage_SelectedIndexChanged" >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblDetailTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="linkbtnDetailNext" CssClass="Button" ToolTip="DetailNext" runat="server" CommandName="DetailNext" Text=">" OnCommand="GetDetailPageIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="linkbtnDetailLast" CssClass="Button" ToolTip="DetailLast" runat="server" CommandName="DetailLast"
                                                    Text=">>" OnCommand="GetDetailPageIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>


        </div>
      </div>
</ContentTemplate>
    </asp:UpdatePanel>
        
        </div>
      </div>
        </div>

   </div>
</div>               

</asp:Content>
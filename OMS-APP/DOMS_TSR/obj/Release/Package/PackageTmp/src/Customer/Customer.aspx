<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="DOMS_TSR.src.Customer.Customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
     <script type="text/javascript">
         function DeleteConfirm() {

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
          <div class="sub-title" >จัดการลูกค้า</div>
        </div>
        <div class="card-block">  
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
               <ContentTemplate>

                 
            <div class="form-group row">
              <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
              <div class="col-sm-3">
                <asp:TextBox  ID="txtSearchCustomerCode" class="form-control" runat="server"></asp:TextBox>
                      <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
             <asp:HiddenField ID="hidEmpCode" runat="server" />

                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">ชื่อ-นามสกุล</label>
              <div class="col-sm-3">
                  <div class="input-group mb-0">
                  <asp:TextBox  ID="txtSearchCustomerFirstName" class="form-control" runat="server" placeholder="ชื่อ"></asp:TextBox>
                  <asp:TextBox  ID="txtSearchCustomerLastName" class="form-control" runat="server" placeholder="นามสกุล"></asp:TextBox>
                  </div>
              </div>
             
           
              <label class="col-sm-2 col-form-label">เพศ</label>
              <div class="col-sm-3">
                    <asp:DropDownList ID="ddlSearchGender" runat="server" class="form-control"></asp:DropDownList> 
             </div>
             <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">อายุ</label>
              <div class="col-sm-3">
                  <div class="input-group mb-0">
                    <asp:TextBox  ID="txtSearchAgeFrom" class="form-control" runat="server"></asp:TextBox>
                    <span class="input-group-addon">ถึง</span>
                    <asp:TextBox  ID="txtSearchAgeTo" class="form-control" runat="server"></asp:TextBox>
                  </div>
              </div>
            
              <label class="col-sm-2 col-form-label">สถานะ</label>
              <div class="col-sm-3">
                    <asp:DropDownList ID="ddlSearchMaritalStatus" runat="server" class="form-control"></asp:DropDownList> 
             </div>
             <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">อาชีพ</label>
              <div class="col-sm-3">
                    <asp:DropDownList ID="ddlSearchOccupation" runat="server" class="form-control"></asp:DropDownList>
              </div>
              
              <label class="col-sm-2 col-form-label">รายได้</label>
              <div class="col-sm-3">
                  <div class="input-group mb-0">
                  <asp:TextBox  ID="txtSearchIncomeFrom" class="form-control" runat="server"></asp:TextBox>
                    <span class="input-group-addon">ถึง</span>
                    <asp:TextBox  ID="txtSearchIncomeTo" class="form-control" runat="server"></asp:TextBox>
             </div>
            </div>
             <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
              <div class="col-sm-3">
                    <asp:TextBox  ID="txtSearchContactTel" class="form-control" runat="server"></asp:TextBox>
              </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label"></label>
              <div class="col-sm-3">
             </div>
            </div>
           
              <div class="text-center m-t-20 col-sm-12">
                  <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"
                      class="button-pri button-accept m-r-10"
                      runat="server" />
                     <asp:Button ID="btnClearSearch" Text="Clear" OnClick="btnClearSearch_Click"
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
                    <!--Start modal Add Customer-->
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
               <ContentTemplate>
                   <asp:LinkButton id="btnAddCustomer" class="button-action button-add " 
                       OnClick="btnAddCustomer_Click" runat ="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                 <asp:LinkButton ID="btnDelete"      OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                      class="button-action button-delete"    runat="server" ><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                   
                </ContentTemplate>
            </asp:UpdatePanel>
                      </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                      <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvCustomer_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkCustomerAll"  AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkCustomer" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

            

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Customer Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        
                                      <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Customer Name</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                      <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                   
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Gender</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                      <asp:Label ID="lblGender" Text='<%# DataBinder.Eval(Container.DataItem, "GenderName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Marital Status</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblMaritalStatus" Text='<%# DataBinder.Eval(Container.DataItem, "MaritalStatusName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Occupation</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblOccupation" Text='<%# DataBinder.Eval(Container.DataItem, "OccupationName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Income</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                     <asp:Label ID="lblIncome" Text='<%# DataBinder.Eval(Container.DataItem, "Income")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">Contact Tel</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblContactTel" Text='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >

                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate  >

                     <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowCustomer"
                                          class="button-activity button-action m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>                       

                                        <asp:HiddenField runat="server" ID="hidCustomerId" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerId")%>' />
                                        <asp:HiddenField runat="server" ID="hidCustomerCode" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidCustomerFName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerFName")%>' />
                                         <asp:HiddenField runat="server" ID="hidCustomerLName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerLName")%>' />
                                         <asp:HiddenField runat="server" ID="hidTitle" Value='<%# DataBinder.Eval(Container.DataItem, "Title")%>' />
                                         <asp:HiddenField runat="server" ID="hidGender" Value='<%# DataBinder.Eval(Container.DataItem, "Gender")%>' />
                                         <asp:HiddenField runat="server" ID="hidBirthDate" Value='<%# DataBinder.Eval(Container.DataItem, "BirthDate")%>' />
                                        <asp:HiddenField runat="server" ID="hidIdentification" Value='<%# DataBinder.Eval(Container.DataItem, "Identification")%>' />
                                        <asp:HiddenField runat="server" ID="hidMaritalStatusCode" Value='<%# DataBinder.Eval(Container.DataItem, "MaritalStatusCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidOccupationCode" Value='<%# DataBinder.Eval(Container.DataItem, "OccupationCode")%>' />
                                        <asp:HiddenField runat="server" ID="hidHomePhone" Value='<%# DataBinder.Eval(Container.DataItem, "HomePhone")%>' />
                                        <asp:HiddenField runat="server" ID="hidMail" Value='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' />
                                        <asp:HiddenField runat="server" ID="hidIncome" Value='<%# DataBinder.Eval(Container.DataItem, "Income")%>' />
                                        <asp:HiddenField runat="server" ID="hidContactTel" Value='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' />
   
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
                                                    Text=">>" ></asp:Button>
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
                    aria-hidden="true" id="modal-customer">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                      <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                              <div class="modal-header modal-header2  p-l-0 ">
                                  <div class="col-sm-12">
                                      <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px; ">เพิ่มลูกค้า</div>
                                      
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
      
                                  <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtCustomerCode_Ins" runat="server" class="form-control"></asp:TextBox> 
                                      <asp:Label ID="lblCustomerCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                       <asp:HiddenField ID="hidCustomer_Ins" runat="server" ></asp:HiddenField>
                               
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">คำนำหน้า</label>
                                  <div class="col-sm-3">

                                        <asp:DropDownList ID="ddlTitle_Ins" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:Label ID="lblTitle_Ins" runat="server" CssClass="validation"></asp:Label>
                                
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
                                  <label class="col-sm-2 col-form-label">ชื่อ</label>
                                  <div class="col-sm-3">
                                        <asp:TextBox ID="txtFirstName_Ins" runat="server" class="form-control"></asp:TextBox>                                     
                                        <asp:Label ID="lblFirstName_Ins" runat="server" CssClass="validation"></asp:Label>                                                                   
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
                                  <label class="col-sm-2 col-form-label">นามสกุล</label>
                                  <div class="col-sm-3">
                                    <asp:TextBox ID="txtLastName_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblLastName_Ins" runat="server" CssClass="validation"></asp:Label>
                                
                                  </div>
                                        
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label"> เพศ</label>
                                  <div class="col-sm-3">

                                        <asp:DropDownList ID="ddlGender_Ins" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:Label ID="lblGender_Ins" runat="server" CssClass="validation"></asp:Label>
                                
                                  </div>

                                       <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">วันเกิด</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtBirthDate_Ins" runat="server" cssclass="form-control" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBirthDate_Ins" 
                                        PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>
                                        <asp:Label ID="lblBirthDate_Ins" runat="server" CssClass="validation"></asp:Label>                               
                                  </div>

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
                                             placeholder="ใส่รายละเอียดเพิ่มเติม" [ngModelOptions]="{standalone: true}"></textarea>
                                            </div>
                                    </div> -->
                                  </div>
                                    <div class="form-group row">
                                  <label class="col-sm-2 col-form-label">อายุ</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtAge_Ins" runat="server" cssclass="form-control" />
                                      <asp:Label ID="lblAge_Ins" runat="server" CssClass="validation"></asp:Label> 
                               
                                  </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">บัตรประจำตัวประชาชน</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtIdentificationNo_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblIdentificationNo_Ins" runat="server" CssClass="validation"></asp:Label>
                                  </div>            
                                  </div>
                            <div class="form-group row">
                                  <label class="col-sm-2 col-form-label">สถานะ</label>
                                  <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlMaritalStatus_Ins" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:Label ID="lblMaritalStatus_Ins" runat="server" CssClass="validation"></asp:Label>                               
                                  </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">อาชีพ</label>
                                  <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlOccupation_Ins" runat="server" class="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblOccupation_Ins" runat="server" CssClass="validation"></asp:Label>
                                  </div>            
                                  </div>
                            <div class="form-group row">
                                  <label class="col-sm-2 col-form-label">รายได้</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtIncome_Ins" runat="server" class="form-control"></asp:TextBox>
                                        <asp:Label ID="lblIncome_Ins" runat="server" CssClass="validation"></asp:Label>                               
                                  </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtContactTel_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblContactTel_Ins" runat="server" CssClass="validation"></asp:Label>
                                  </div>            
                                  </div>
                            <div class="form-group row">
                                  <label class="col-sm-2 col-form-label">เบอร์โทรศัพท์(บ้าน)</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtHomePhone_Ins" runat="server" class="form-control"></asp:TextBox>
                                        <asp:Label ID="lblHomePhone_Ins" runat="server" CssClass="validation"></asp:Label>                               
                                  </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">อีเมล์</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtEmail_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblEmail_Ins" runat="server" CssClass="validation"></asp:Label>
                                  </div>            
                                  </div>                           
                                  </ContentTemplate>
                                  </asp:UpdatePanel>
                                  
                                
<%--                                  <label class="col-sm-2 col-form-label"></label>
                            <div class="col-sm-9">
                                <input type="file" name="files[]" id="filer_input1" multiple="multiple">
                             </div>--%>

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

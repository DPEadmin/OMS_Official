<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="DOMS_TSR.src.UserManagement.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
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
    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvEmp.ClientID %>");

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
                                <div class="sub-title">search for user information</div>
                            </div>

                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee ID</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearchEmpCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2 col-form-label">BU Code</label>
                                    <div class="col-sm-4">
                                           <asp:DropDownList ID="ddlSearchBU" class="form-control" runat="server">
                                       
                  </asp:DropDownList>
                                    </div>
                                    <label class="col-sm-2 col-form-label">Employee Name</label>
                                     <div class="col-sm-4">
                                         <div class="input-group mb-0">
                                            <asp:TextBox  ID="txtSearchEmpFNameTH" class="form-control" runat="server" placeholder = "name"></asp:TextBox>
                                            <asp:TextBox  ID="txtSearchEmpLNameTH" class="form-control" runat="server" placeholder = "last name"></asp:TextBox>
                                         </div>
                                       </div>  
                                 
                                     <input type="hidden" id="Hidden1" runat="server" />
                                    <input type="hidden" id="Hidden2" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <input type="hidden" id="hidaction" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />   
                                    <asp:HiddenField ID="hidSupplierIdIns" runat="server" />
                                    <asp:HiddenField ID="HiddenField3" runat="server" />      
                                 
                                    
                                    
                                </div>

                                <div class="text-center m-t-20 col-sm-12">

    <asp:Button ID="btnSearch" Text="Search" class="button-pri button-accept m-r-10" OnClick="btnSearch_Click" runat="server"/>
                  <asp:Button ID="btnClearSearch" Text="Clear" class="button-pri button-cancel" OnClick="btnClearSearch_Click" runat="server" />   
             

                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="m-b-10">
                                    <asp:LinkButton ID="btnAddEmployee" class="button-action button-add m-r-5"
                                        OnClick="btnAddEmployee_Click" runat="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                                     <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                              
                                </div>

                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                        TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvEmp_RowCommand"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkEmpAll" OnCheckedChanged="chkEmpAll_Changed" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="Center">Employee ID</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                              <%# GetLink(DataBinder.Eval(Container.DataItem, "EmpCode")) %>
                                                           
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" ItemStyle-Width="150px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="left">Employee Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblTitleTH" Text='<%# DataBinder.Eval(Container.DataItem, "TitleName_TH")%>' runat="server" />
                                       <asp:Label ID="lblEmpNameTH" Text=' <%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server" />
                                  
                                      </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">BU</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblBU" Text='<%# DataBinder.Eval(Container.DataItem, "BUName")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>
    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">telephone</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Mobile")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Email</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblMail" Text='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate  >
                                          <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowEmp"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="button-pri button-activity m-r-10 icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                 
                                        <asp:HiddenField runat="server" ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' />
                                         <asp:HiddenField runat="server" ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' />
                                           <asp:HiddenField runat="server" ID="hidTitleTH" Value='<%# DataBinder.Eval(Container.DataItem, "Title_TH")%>' />
                                          <asp:HiddenField runat="server" ID="hidTitleEN" Value='<%# DataBinder.Eval(Container.DataItem, "Title_EN")%>' />
                                          <asp:HiddenField runat="server" ID="hidEmpFNameTH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' />
                                          <asp:HiddenField runat="server" ID="hidEmpLNameTH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' />
                                          <asp:HiddenField runat="server" ID="hidEmpFNameEN" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_EN")%>' />
                                         <asp:HiddenField runat="server" ID="hidEmpLNameEN" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_EN")%>' />
                                         <asp:HiddenField runat="server" ID="hidBUCode" Value='<%# DataBinder.Eval(Container.DataItem, "BUCode")%>' />
                                      <asp:HiddenField runat="server" ID="hidExtensionID" Value='<%# DataBinder.Eval(Container.DataItem, "ExtensionID")%>' />
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
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="modal-emp">
        <div class="modal-dialog modal-lg" style="max-width: 80%">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2 ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle"class="modal-title sub-title " style="font-size: 16px;">
                                      Add User
                                </div>
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
                    
                                <asp:UpdatePanel ID="upModal" runat="server">
                                    <ContentTemplate>
                                         <div class="form-group row">
      
                                  <label class="col-sm-2 col-form-label">Employee ID<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-4">
                                        <asp:HiddenField ID="hidEmpIdIns" runat="server"></asp:HiddenField>                                   
                                     
                                      <asp:TextBox ID="txtEmpCodeIns" runat="server" class="form-control"></asp:TextBox>                                   
                                      <asp:Label ID="lblEmpCodeIns" runat="server" CssClass="validation"></asp:Label>                              
                                  </div>
                                  
                                  <label class="col-sm-2 col-form-label">BU<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-4">
                                             <asp:DropDownList id="ddlBUIns" class="form-control" runat="server"></asp:DropDownList>
                                      <asp:Label ID="lblBUIns" runat="server" CssClass="validation"></asp:Label>                
                                  </div>
                                </div>
                                <div class="form-group row">
                                  <label class="col-sm-2 col-form-label">Name<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-4">                                                                     
                                        <asp:TextBox ID="txtEmpFNameTHIns" runat="server" class="form-control"></asp:TextBox> 
                                        <asp:Label ID="lblEmpFNameTHIns" runat="server" CssClass="validation"></asp:Label>     
                                  </div>
                                  
                                  <label class="col-sm-2 col-form-label">Surname<span style="color: red; background-position: right top;">*</span></label>
                                  <div class="col-sm-4">
                                        <asp:TextBox ID="txtEmpLNameTHIns" runat="server" class="form-control"></asp:TextBox> 
                                      <asp:Label ID="lblEmpLNameTHIns" runat="server" CssClass="validation"></asp:Label>                   
                                   </div>
                                  </div> 
                                
                            <div class="form-group row">      
                                  <label class="col-sm-2 col-form-label">Telephone number</label>
                                  <div class="col-sm-4">
                                       <asp:TextBox id="txtMobileIns" class="form-control" runat="server"></asp:TextBox>
                                      <asp:Label ID="lblMobileIns" runat="server" CssClass="validation"></asp:Label>                             
                                  </div>
                                  
                                  <label class="col-sm-2 col-form-label">Email</label>
                                  <div class="col-sm-4">
                                     <asp:TextBox id="txtEmailIns" class="form-control" runat="server"></asp:TextBox>                                   
                                     <asp:Label ID="lblEmailIns" runat="server" CssClass="validation"></asp:Label>                    
                                  </div>
                                </div>
                            
      <div class="form-group row">      
                                  <label class="col-sm-2 col-form-label">Extention ID</label>
                                  <div class="col-sm-4">
                                       <asp:TextBox id="txtExtensionID_ins" class="form-control" runat="server"></asp:TextBox>
                                      <asp:Label ID="lblExtensionID_ins" runat="server" CssClass="validation"></asp:Label>                             
                                  </div>
                                  
                             
                                </div>
                            



                                        <div class="text-center m-t-20 center">

                                            <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                                  class="button-pri button-accept m-r-10"
                                                runat="server" />
                                            <asp:Button ID="btnCancel" Text="Clear" OnClick="btnCancel_Click"
                                               class="button-pri button-cancel"
                                                runat="server" />

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                   
                </div>

            </div>
        </div>
    </div>
</asp:Content>

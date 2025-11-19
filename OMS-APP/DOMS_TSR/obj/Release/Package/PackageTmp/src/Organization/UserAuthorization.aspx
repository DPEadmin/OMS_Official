<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="UserAuthorization.aspx.cs" Inherits="DOMS_TSR.src.Organization.UserAuthorization" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        #drop-zone {
  width: 100%;
  min-height: 150px;
  border: 3px dashed rgba(0, 0, 0, .3);
  border-radius: 5px;
  font-family: Arial;
  text-align: center;
  position: relative;
  font-size: 20px;
  color: #7E7E7E;
}
#drop-zone input {
  position: absolute;
  cursor: pointer;
  left: 0px;
  top: 0px;
  opacity: 0;
}
/*Important*/

#drop-zone.mouse-over {
  border: 3px dashed rgba(0, 0, 0, .3);
  color: #7E7E7E;
}
/*If you dont want the button*/

#clickHere {
  display: inline-block;
  cursor: pointer;
  color: white;
  font-size: 17px;
  width: 150px;
  border-radius: 4px;
  background-color: #4679BD;
  padding: 10px;
}
#clickHere:hover {
  background-color: #376199;
}
#filename {
  margin-top: 10px;
  margin-bottom: 10px;
  font-size: 14px;
  line-height: 1.5em;
}
.file-preview {
  background: #ccc;
  border: 5px solid #fff;
  box-shadow: 0 0 4px rgba(0, 0, 0, 0.5);
  display: inline-block;
  width: 60px;
  height: 60px;
  text-align: center;
  font-size: 14px;
  margin-top: 5px;
}
        .closeBtn:hover {
            color: red;
            display: inline-block;
        }
    </style>
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
          <div class="sub-title" >Search Employee</div>
          </div>
            <div class="card-block">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
               <ContentTemplate>

                 
            <div class="form-group row">
              <label class="col-sm-2 col-form-label">Employee Code</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchEmpCode" class="form-control" runat="server"></asp:TextBox>
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">First Name</label>
              <div class="col-sm-3">
                  

                  <asp:TextBox  ID="txtSearchEmpFname_TH" class="form-control" runat="server"></asp:TextBox>
                      <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />   
            <asp:HiddenField ID="hidEmpIdIns" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />      
              </div>

                <label class="col-sm-2 col-form-label">Last Name</label>
              <div class="col-sm-3">
                  <asp:TextBox  ID="txtSearchEmpLname_TH" class="form-control" runat="server"></asp:TextBox>
                  </div>
              <label class="col-sm-1 col-form-label"></label>
              <label class="col-sm-2 col-form-label">Status</label>
              <div class="col-sm-3">
                  <asp:DropDownList ID="ddlSearchEmpActiveflag" class="form-control" runat="server">
                                        <asp:ListItem Enabled="true" Text="Please select status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
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
                   <asp:LinkButton id="btnAddEmployee" class="btn-add button-active btn-small" OnClick="btnAddEmployee_Click"
                        runat ="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                 <%--<asp:LinkButton ID="btnDelete"     
                      class="btn-del button-active btn-small"    runat="server" ><i class="fa fa-minus"></i>Delete</asp:LinkButton>--%>
                   
                </ContentTemplate>
            </asp:UpdatePanel>
                      </div>

                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <div class="dt-responsive table-responsive">
                      <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvEmp_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>                               
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Employee Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# GetLink(DataBinder.Eval(Container.DataItem, "EmpCodeTemp")) %>
                                        <!--&nbsp;&nbsp;<asp:Label ID="lblEmpCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCodeTemp")%>' runat="server" />-->
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">EmployeeSync Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblRefCode" Text='<%# DataBinder.Eval(Container.DataItem, "RefCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="300px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">EmpName</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblEmpName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="75px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <div align="center">Status</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblActiveFlag" Text='<%# DataBinder.Eval(Container.DataItem, "ActiveFlagName")%>' runat="server" />                          
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" >
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate  >
                                          <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowEmp"
                                          class="button-active button-submit m-r-10  " style="float: none; border-radius: 5px;     padding: 3px 10px;     padding-top: 5px;"
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>                       

                                        <asp:HiddenField runat="server" ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' />
                                        <asp:HiddenField runat="server" ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCodeTemp")%>' />
                                        <asp:HiddenField runat="server" ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' />
                                        <asp:HiddenField runat="server" ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' />
                                        <asp:HiddenField runat="server" ID="hidEmpName_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' />
                                        <asp:HiddenField runat="server" ID="hidActiveFlag" Value='<%# DataBinder.Eval(Container.DataItem, "ActiveFlag")%>' />
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
                                                    Text="<<" runat="server" OnCommand="GetPageIndex" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                    Text="<" runat="server" OnCommand="GetPageIndex" ></asp:Button>
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
                                                <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                    Text=">>" OnCommand="GetPageIndex" ></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                      <asp:TextBox ID="txtrefcodefrominsert" runat="server" ></asp:TextBox>
                      <asp:TextBox ID="txtempcode" runat="server" ></asp:TextBox>
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
                    aria-hidden="true" id="modal-addemp">
                    <div class="modal-dialog modal-lg" style="max-width:1300px;">
        
                      <div class="modal-content">
                        <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle">Add Employee
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
      
                                  <label class="col-sm-2 col-form-label">Employee Code</label>
                                  <div class="col-sm-3">
                                      <asp:TextBox ID="txtEmpCodeIns" runat="server" class="form-control"></asp:TextBox>                                   
                                      <asp:Label ID="lblEmpCodeIns" runat="server" CssClass="validation"></asp:Label>                              
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">First Name</label>
                                  <div class="col-sm-3">
                                           <asp:TextBox ID="txtEmpFname_THIns" runat="server" class="form-control"></asp:TextBox> 
                                      <asp:Label ID="lblEmpFname_THIns" runat="server" CssClass="validation"></asp:Label>                   
                                  </div>
                                </div>
                                   <div class="form-group row">
                                  <label class="col-sm-2 col-form-label">Last Name</label>
                                  <div class="col-sm-3">                                                                     
                                        <asp:TextBox ID="txtEmpLname_THIns" runat="server" class="form-control"></asp:TextBox> 
                                        <asp:Label ID="lblEmpLname_THIns" runat="server" CssClass="validation"></asp:Label>     
                                  </div>
                                  <label class="col-sm-1 col-form-label"></label>
                                  <label class="col-sm-2 col-form-label">Status</label>
                                  <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlEmpActiveflagIns" class="form-control" runat="server">
                                        <asp:ListItem Enabled="true" Text="Please select Type" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                                    </asp:DropDownList> 
                                  <asp:Label ID="lblEmpActiveflagIns" runat="server" CssClass="validation"></asp:Label>                                
                                  </div>
                                  </div> 
                                  </ContentTemplate>
                                  </asp:UpdatePanel>
                                  
                                
                                  <label class="col-sm-2 col-form-label"></label>

                        <div class="text-center m-t-20 center">
                          
                                      <asp:Button ID="btnSubmit" Text="Submit" Onclick="btnSubmit_Click"
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />
                                     <asp:Button ID="btnCancel" Text="Cancel" Onclick="btnCancel_Click"
                                      class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                      runat="server" />

                                </div>

                                  

                              </div>
                            </div>
                          </div>
                        </div>
        </div>
      </div>
        </div>

      </div>
     </div>
</asp:Content>
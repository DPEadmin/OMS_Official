
<%@ Page Language="C#"  MasterPageFile="~/src/MasterPage/Web.master"  AutoEventWireup="true" CodeBehind="RoutingMappingDetail.aspx.cs" Inherits="DOMS_TSR.src.RoutingManagement.RoutingMappingDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

   <script type="text/javascript">

        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvRoutingVehicle.ClientID %>");

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

        function DeleteConfirm1() {

            var grid = document.getElementById("<%= gvRoutingDriver.ClientID %>");

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
    <input type="hidden" id="hidIdList1" runat="server" />
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
         <div class="sub-title" >ข้อมูลสายส่ง</div>
            </div>
           
                    
                 
            <div class="card-block">
              <div class="view-info">
                <div class="row">
                  <div class="col-lg-12">
                    <div class="general-info">
    
                      <div class="row">
    
                        <div class="col-sm-12 col-lg-12 col-xl-6">
                          <div class="table-responsive">
                            <table class="table m-0" style="width: 100%">
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
          
      
        <div class="page-body">
        <div class="row">
            <style>
            .myButton {
    -moz-box-shadow: none;
    -webkit-box-shadow: none;
    box-shadow: none;
    background-color: #2dabf9;
    border: none;
    -moz-border-radius: 3px;
    -webkit-border-radius: 3px;
    border-radius: 3px;
    display: inline-block;
    cursor: pointer;
    color: #ffffff;
    font-family: Arial;
    font-size: 15px;
    padding: 4px 12px;
    text-decoration: none;
    text-shadow: 0px 1px 0px #263666;
            </style>
          <div class="col-sm-12">
            <!-- Basic Form Inputs card start -->
            <div>
                    <asp:Linkbutton id="tab1" runat="server" OnClick="btntab1_Click" class="myButton">รถขนส่ง</asp:Linkbutton>
                    <asp:Linkbutton id="tab2" runat="server" OnClick="btntab2_Click" class="myButton" style="margin-left:-3px;">พนักงานขนส่ง</asp:Linkbutton>
            </div>
            <div id="VehicleSection" runat="server">
            <div class="card">      
              <div class="card-block">
                 
            
                  <div class="m-b-10">
                    <!--Start modal Add Product-->
                   <asp:LinkButton id="btnAddVehicle" class="button-action button-add" data-backdrop="false"
                       OnClick="btnAddVehicle_Click" runat ="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                 <asp:LinkButton ID="btnDeleteVehicle"  OnClick="btnDeleteVehicle_Click" OnClientClick="return DeleteConfirm();"   
                      class="button-action button-delete"    runat="server" ><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                      <span style="float: right;">
                      <asp:DropDownList ID="ddlFilterVehicle" runat="server"   AutoPostBack="true"></asp:DropDownList>
                      <asp:TextBox id="txtFilterVehicle" runat="server"></asp:TextBox>
                      <asp:Linkbutton id="Carsearch" onClick="btnSearch_Click" runat="server" style="margin-left:-3px;" class="myButton">seach</asp:Linkbutton>
                      </span>
                   </div>
                  
                  <div class="dt-responsive table-responsive">
                      <asp:GridView ID="gvRoutingVehicle" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                            TabIndex="0" Width="100%" CellSpacing="0"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkRoutingVehicleAll" OnCheckedChanged="chkRoutingVehicleAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkRoutingVehicle" runat="server" />
                                        <asp:HiddenField runat="server" ID="hidRoutingVehicleId" Value='<%# DataBinder.Eval(Container.DataItem, "RoutingVechicleId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ทะเบียน</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblVechicle_No" Text='<%# DataBinder.Eval(Container.DataItem, "Vechicle_No")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ยี้ห้อรถ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lblBand_Name" Text='<%# DataBinder.Eval(Container.DataItem, "Band_Name")%>' runat="server" />
                                   
                         
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ประเภทรถ</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblTypeCar_Name" Text='<%# DataBinder.Eval(Container.DataItem, "TypeCar_Name")%>' runat="server" />

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
        <div id="DriverSection" runat="server">
              <div class="card">
              <div class="card-block">
            
                  <div class="m-b-10">
                    <!--Start modal Add Product-->
                   <asp:LinkButton id="btnAddDriver" class="button-action button-add " data-backdrop="false"
                       OnClick="btnAddDriver_Click" runat ="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                 <asp:LinkButton ID="btnDeleteDriver"  OnClick="btnDeleteDriver_Click" OnClientClick="return DeleteConfirm1();"   
                      class="button-action button-delete"    runat="server" ><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                      <span style="float: right;">
                      <asp:DropDownList ID="ddlFilterDriver" runat="server"   AutoPostBack="true"></asp:DropDownList>
                      <asp:TextBox id="txtFilterDriver" runat="server"></asp:TextBox>
                      <asp:Linkbutton id="Driversearch" onClick="btnSearch1_Click" runat="server" style="margin-left:-3px;" class="myButton">seach</asp:Linkbutton>
                      </span>
                   </div>
                  
                
                      <asp:GridView ID="gvRoutingDriver" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                            TabIndex="0" Width="100%" CellSpacing="0" 
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkRoutingDriverAll" OnCheckedChanged="chkRoutingDriverAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkRoutingDriver" runat="server" />
                                        <asp:HiddenField runat="server" ID="hidRoutingDriverId" Value='<%# DataBinder.Eval(Container.DataItem, "RoutingDriverId")%>' />
                                        <asp:HiddenField runat="server" ID="hidDriverNo" Value='<%# DataBinder.Eval(Container.DataItem, "Driver_no")%>' />
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รหัสคนขับ</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblDriver_no" Text='<%# DataBinder.Eval(Container.DataItem, "Driver_no")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ชื่อ-สกุล</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lblFullname" Text='<%# Eval("Fname").ToString() + "  "+Eval("Lname").ToString()  %>' runat="server" />
    
                                    </ItemTemplate>

                                </asp:TemplateField>
 
   
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty1" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
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
                                   
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnFirst1" CssClass="Button" ToolTip="First" CommandName="First"
                                                    Text="<<" runat="server" OnCommand="GetPageIndex1"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnPre1" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                    Text="<" runat="server" OnCommand="GetPageIndex1"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage1" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged1">
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblTotalPages1" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnNext1" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex1"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnLast1" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                    Text=">>" OnCommand="GetPageIndex1"></asp:Button>
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
            aria-hidden="true" id="modal-vehicle">
            <div class="modal-dialog modal-lg" style="max-width:75%">
                <div class="modal-box-s">
                    <div class="modal-content m-b-10">
                        
                        <div class="modal-body">
                            <div class="modal-header">
            <div class="sub-title" >ค้นหาข้อมูลรถขนส่ง</div>
          </div>
               <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                  <ContentTemplate>
                        <div class="form-group row">
                <label class="col-sm-2 col-form-label">ทะเบียนรถ</label>
                <div class="col-sm-3">
                     <asp:TextBox  ID="txtSearchLicenseplate" class="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">ยี่ห้อรถ</label>
                <div class="col-sm-3">
                    <asp:TextBox  ID="txtSearchVehiclebrand" class="form-control" runat="server"></asp:TextBox>              
                </div>
              </div>
             
       
                <div class="text-center m-t-20 col-sm-12">
                    <asp:Button ID="btnSearchVehicle" Text="Search" OnClick="btnSearchVehicle_Click"
                      class="button-pri button-accept m-r-10"
                      runat="server" />
                     <asp:Button ID="btnClearSearchVehicle" Text="Clear" OnClick="btnClearSearchVehicle_Click"
                      class="button-pri button-cancel"
                      runat="server" />
                </div>
                </ContentTemplate>
              </asp:UpdatePanel>
            </div>
                        </div>
                <div class="modal-content">
                <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle">เพิ่มข้อมูลรถยนต์
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
                              
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                <div class="form-group row">
                                  <label class="col-sm-12 col-form-label"></label>
                                  <div class="col-sm-12">
                                   
                        <asp:GridView ID="gvVehicle" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0"  OnRowCommand="gvVehicle_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                     <%--           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkVehicleAll" OnCheckedChanged="chkVehicleAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkVehicle" runat="server" />
                                       
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

            

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ทะเบียน</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblname_Routing" Text='<%# DataBinder.Eval(Container.DataItem, "Vechicle_No")%>' runat="server" />
                                        <asp:HiddenField runat="server" ID="hidVehicleId" Value='<%# DataBinder.Eval(Container.DataItem, "VechicleId")%>' />
                                        <asp:HiddenField runat="server" ID="hidVehicleNo" Value='<%# DataBinder.Eval(Container.DataItem, "Vechicle_No")%>' />
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ยี้ห้อรถ</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                       <asp:Label ID="lblVechicle_No" Text='<%# DataBinder.Eval(Container.DataItem, "Name_Band")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ประเภทรถ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lblBand_Name" Text='<%# DataBinder.Eval(Container.DataItem, "Name_TypeCar")%>' runat="server" />
                                   
                         
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddVehicle"
                                                                    class="button-activity"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-16"></span></asp:LinkButton>
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
                                   
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnFirst2" CssClass="Button" ToolTip="First" CommandName="First"
                                                    Text="<<" runat="server" OnCommand="GetPageIndex2"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnPre2" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                    Text="<" runat="server" OnCommand="GetPageIndex2"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage2" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged2">
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblTotalPages2" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnNext2" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex2"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnLast2" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                    Text=">>" OnCommand="GetPageIndex2"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                         </table>


                                </div>        
                        </div>                    
                        <%--<div class="text-center m-t-20 center">
                                <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" 
                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                runat="server" />
                                <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                runat="server" />
                        </div>--%>
                               </ContentTemplate>
                </asp:UpdatePanel>
                              
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    </div>
                  </div>




     <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
            aria-hidden="true" id="modal-driver">
            <div class="modal-dialog modal-lg" style="max-width:75%;">
                <div class="modal-box-s"
                <div class="modal-content m-b-10">
                        
                        <div class="modal-body">
                            <div class="modal-header">
            <div class="sub-title" >ค้นหาข้อมูลคนขับ</div>
          </div>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                <div class="form-group row">
                <label class="col-sm-2 col-form-label">รหัสคนขับ</label>
                <div class="col-sm-3">
                     <asp:TextBox  ID="txtSearchDriverCode" class="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-1 col-form-label"></label>
                <label class="col-sm-2 col-form-label">ชื่อ-สกุล</label>
                <div class="col-sm-3">
                    <asp:TextBox  ID="txtSearchFullname" class="form-control" runat="server"></asp:TextBox>              
                </div>
              </div>
             
       
                <div class="text-center m-t-20 col-sm-12">
                    <asp:Button ID="btnSearchDriver" Text="Search" OnClick="btnSearchDriver_Click"
                      class="button-pri button-accept m-r-10"
                      runat="server" />
                     <asp:Button ID="btnClearDriver" Text="Clear" OnClick="btnClearSearchDriver_Click"
                      class="button-pri button-cancel"
                      runat="server" />
                </div>
              </ContentTemplate>
             </asp:UpdatePanel>
            </div>
                        </div>
                <div class="modal-content">
                <div class="row">
                          <div class="col-sm-12">
                            <div class="modal-header modal-header2 ">
                              <div class="col-sm-11">
                                <div id="exampleModalLongTitle1">เพิ่มข้อมูลคนขับ
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
                              
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                <div class="form-group row">
                                  <label class="col-sm-12 col-form-label"></label>
                                  <div class="col-sm-12">
                                     <asp:GridView ID="gvDriver" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvDriver_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                              <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkDriverAll" OnCheckedChanged="chkDriverAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkDriver" runat="server" />
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

            

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รหัสคนขับ</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDriverNoIns" Text='<%# DataBinder.Eval(Container.DataItem, "Driver_no")%>' runat="server" />
                                        <asp:HiddenField runat="server" ID="hidDriverId" Value='<%# DataBinder.Eval(Container.DataItem, "DriverId")%>' />
                                        <asp:HiddenField runat="server" ID="hidDriverNo" Value='<%# DataBinder.Eval(Container.DataItem, "Driver_no")%>' />
                                  
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ชื่อ-สกุล</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblFullnameIns" Text='<%# Eval("Fname").ToString() + "  "+Eval("Lname").ToString()  %>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
      
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="Server" CommandName="AddDriver"
                                                                    class="button-activity"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-add f-16"></span></asp:LinkButton>
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
                                   
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnFirst3" CssClass="Button" ToolTip="First" CommandName="First"
                                                    Text="<<" runat="server" OnCommand="GetPageIndex3"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnPre3" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                    Text="<" runat="server" OnCommand="GetPageIndex3"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage3" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged3">
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblTotalPages3" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnNext3" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex3"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtnLast3" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                    Text=">>" OnCommand="GetPageIndex3"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                         </table>

                            </div>
                        </div>        


                                        
                        <%--<div class="text-center m-t-20 center">
                               
                                <asp:Button ID="btnSubmit1" Text="Submit" OnClick="btnSubmit_Click" 
                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                runat="server" />
                                <asp:Button ID="btnCancel1" Text="Cancel" OnClick="btnCancel_Click"
                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                runat="server" />
                        
                        </div>--%>

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


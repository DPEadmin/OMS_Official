<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="DOMS_TSR.src.Organization.UserDetail" %>
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

        .menuTabs
        {
            position:relative;
            top:1px;
            left:10px;
        }
        .tab
        {
            border:none;
            border-bottom:none;
            padding:0px 10px;
            background-color:none;
        }
        .selectedTab
        {
            border:Solid 1px black;
            border-bottom:Solid 1px white;
            padding:0px 10px;
            background-color:white;
        }
        .tabBody
        {
            border:Solid 1px black;
            padding:20px;
            background-color:white;
        }

    </style>
<script type="text/javascript">
    function redirect() {
         window.open('UserAuthorization.aspx');
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
        <asp:HiddenField ID="hidEmpId" runat="server" />
        <asp:HiddenField ID="hidEmpCode" runat="server" />
        <asp:HiddenField ID="hidRefCodefromOneApp" runat="server" />
        <asp:HiddenField ID="hidEmpCodeafterInsert" runat="server" />
        <input type="hidden" id="hidIdList" runat="server" />
<div class="page-body">
     <div class="col-sm-12">
            <!-- Basic Form Inputs card start -->
         <div class="card">
            <div class="card-header">
                <div class="sub-title" >Employee Detail</div>
                </div>
                    <div class="card-block">                         
                          <div class="col-sm-12">
                              <div class="view-info" >
                                  <div class="row">
                                      <asp:Literal ID="litLinkBack" runat="server"></asp:Literal>
                                      <div class="col-lg-12">
                                          <div class="general-info">
                                              <div class="row">                
                                                  <div class="col-lg-12 col-xl-6 col-sm-12">                                                   
                                                      <table class="table m-0">
                                                          <tbody>
                                                              <tr>
                                                                  <th scope="row">Employee Code</th>
                                                                  <td><asp:Label runat="server" ID="lblEmpCode"></asp:Label></td>
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">Status</th>
                                                                  <td><asp:Label runat="server" ID="lblEmpStatus"></asp:Label>
                                                                  </td>                
                                                              </tr>       
                                                          </tbody>
                                                      </table>
                                                  </div>
                                                  <div class="col-lg-12 col-xl-6 col-sm-12">
                
                                                      <table class="table m-0">
                                                          <tbody>
                                                              <tr>
                                                                  <th scope="row">Employee Sync Code</th>
                                                                  <td><asp:Label runat="server" ID="lblrefCode"></asp:Label></td>
                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">Employee Name</th>
                                                                  <td>
                                                                      <asp:Label runat="server" ID="lblEmpName"></asp:Label>
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
                                                    </div>
                                                  </div>
                                                </div>
                                              </div>
                                            </div>
                                          </div>
                                        </div>
        </div>

<div class="card">
    <div class="card-header">
        <div class="sub-title" >

            <asp:Menu
        id="menuTabs"
        CssClass="menuTabs"
        StaticMenuItemStyle-CssClass="tab"
        StaticSelectedStyle-CssClass="selectedTab"
        Orientation="Horizontal"
        OnMenuItemClick="menuTabs_MenuItemClick"
        Runat="server">
        <Items>
            <asp:MenuItem Text="User Login" Value="0" />
            
        </Items>
    </asp:Menu>    

        </div>
    </div>
            <div class="card-block"> 
                <div class="tabBody">
    <asp:MultiView
        id="multiTabs"
        ActiveViewIndex="0"
        Runat="server">
        <asp:View ID="view1" runat="server">
        
        <div class="form-group row">
              <label class="col-sm-2 col-form-label">UserName</label>
              <div class="col-sm-9">
                <asp:TextBox  ID="txtUserNameIns" class="form-control" runat="server"></asp:TextBox>
                  <asp:Label ID="lblUserNameIns" runat="server" CssClass="validation"></asp:Label>
                  </div>
                <label class="col-sm-2 col-form-label">Password</label>
              <div class="col-sm-9">
                <asp:TextBox  ID="txtPasswordIns" class="form-control" runat="server"></asp:TextBox>
                <asp:Label ID="lblPasswordIns" runat="server" CssClass="validation"></asp:Label>  
                  </div>
            </div>
        <div class="text-center m-t-20 col-sm-12">
            <asp:Button ID="btnEdit" Text="Edit" OnClick="btnEdit_Click"
                      class="button-active button-submit m-r-10" 
                      runat="server" />
                  <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                      class="button-active button-submit m-r-10" 
                      runat="server" />  
            <asp:Button ID="btnSyncUser" Text="SyncEmpCodetoOneApp" OnClick="btnSyncUser_Click" 
                      class="button-active btn-synemp m-r-10" 
                      runat="server" /> 
            <asp:Button ID="btnSyncUserSuccess" Text="SyncEmpCodetoOneApp" 
                      class="button-active btn-synempsuccess m-r-10" 
                      runat="server" />
         </div>          
        
        </asp:View>
        <%--<asp:View ID="view2" runat="server">
        
        Contents of second tab
        
        </asp:View>--%>
    </asp:MultiView>    
    </div>
            </div>    
</div>
    </div>
</div>
                            </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
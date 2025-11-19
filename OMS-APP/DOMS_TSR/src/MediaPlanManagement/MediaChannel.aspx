<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="MediaChannel.aspx.cs" Inherits="DOMS_TSR.src.MediaPlanManagement.MediaChannel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvMediaChannel.ClientID %>");

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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hd" runat="server" />
            <div class="page-body">

                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Search for sales channel information</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">sales channel name</label>
                                    <div class="col-sm-4 pcontrol">

                                        <asp:Label ID="lblChannelDis" CssClass="validation" runat="server"> </asp:Label>           
                                    </div>
                             
                                    <label class="col-sm-2 col-form-label" >Channel name</label>
                                    <div class="col-sm-4 pcontrol">
                                           <asp:Label ID="lblActiveDis" CssClass="validation" runat="server"></asp:Label>

                                    </div> 
                                </div> 
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Search for sales channel information</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Password</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtMediaChannelCode_Search" class="form-control" runat="server"> </asp:TextBox>
                                   
                                        <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                        <asp:HiddenField ID="hidMerCode" runat="server"/>

                                    </div>
                                 
                                    <label class="col-sm-2 col-form-label">Status</label>
                                    <div class="col-sm-4">
                                           <asp:DropDownList ID="ddlSearchMediaChannelActive" class="form-control" runat="server"></asp:DropDownList>

                                    </div>

                                
                                    <label class="col-sm-2 col-form-label">Thai name</label>
                                    <div class="col-sm-4">
                                                     <asp:TextBox ID="txtnameTh_search" class="form-control" runat="server"> </asp:TextBox>

                                    </div>
                                  
                                        <label class="col-sm-2 col-form-label">English name</label>
                                    <div class="col-sm-4">
                                          <asp:TextBox ID="txtnameEn_search" class="form-control" runat="server"> </asp:TextBox>

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

                            </div>
                        </div>

                        <div class="page-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Basic Form Inputs card start -->
                                    <div class="card">
                                        <div class="card-block">

                                            <div class="m-b-10">
                                                <!--Start modal Add MediaChannel-->
                                                <asp:LinkButton ID="btnAddMediaChannel" class="button-action button-add m-r-5"
                                                    OnClick="btnAddMediaChannel_Click" runat="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagMediaChannel" runat="server" />
                                            <asp:GridView ID="gvMediaChannel" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvMediaChannel_RowCommand"  OnRowDataBound ="gvMediaChannel_OnRowDataBound"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                            <asp:CheckBox ID="chkMediaChannelAll" OnCheckedChanged="chkMediaChannelAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkMediaChannel" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">media channel code</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                              <%--  <%# GetLink(DataBinder.Eval(Container.DataItem, "Code")) %>--%>
                                                            <asp:Label ID="lblCode" Text='<%# DataBinder.Eval(Container.DataItem, "Code")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ชื่อภาษาไทย</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblname_th" Text='<%# DataBinder.Eval(Container.DataItem, "name_th")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                       
                                                                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">ภาษาอังกฤษ</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblname_en" Text='<%# DataBinder.Eval(Container.DataItem, "name_en")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                       
                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">Active</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                         
                                                            <asp:Label ID="lblActive" Text='<%# (((string)DataBinder.Eval(Container.DataItem,"Active")) == "Y") ? ("Active") : ("Inactive" )%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                       


                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowMediaChannel"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="button-pri button-activity m-r-10 icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                                                                                          
                                                            <asp:HiddenField runat="server" ID="HidMediaChannelId" Value='<%# DataBinder.Eval(Container.DataItem, "MediaChannelId")%>' /> 
                                                            <asp:HiddenField runat="server" ID="HidCode" Value='<%# DataBinder.Eval(Container.DataItem, "Code")%>' />
                                                             <asp:HiddenField runat="server" ID="Hidname_th" Value='<%# DataBinder.Eval(Container.DataItem, "name_th")%>' />
                                                             <asp:HiddenField runat="server" ID="Hidname_en" Value='<%# DataBinder.Eval(Container.DataItem, "name_en")%>' />

                                                            <asp:HiddenField runat="server" ID="hidActive" Value='<%# DataBinder.Eval(Container.DataItem, "Active")%>' />
                                                  
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
                                <!-- Basic Form Inputs card end -->
                            </div>
                        </div>
                        <!-- Basic Form Inputs card end -->
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="modal-MediaChannel" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 40%">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2    ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่ม ช่องทางการขาย</div>

                            </div>
                            <span>
                                <button type="button" class="close  " style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="modal-body ">

                    <asp:UpdatePanel ID="UpModal" runat="server">
                        <ContentTemplate>
                            <div class="form-group row">


                                <label class="col-sm-4 col-form-label">รหัสช่อง</label>
                                <div class="col-sm-8">

                                    <asp:TextBox ID="txtMediaChannelCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblMediaChannelCode_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>                               

                            
                              <label class="col-sm-4 col-form-label">ชื่อภาษาไทย</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMediaChannelNameth_ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblMediaChannelNameTh_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                            
                              <label class="col-sm-4 col-form-label">ชื่ออังกฤษ</label>
                                <div class="col-sm-8">
                                   <asp:TextBox ID="txtMediaChannelNameEN_ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblMediaChannelNameEN_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                            
                              <label class="col-sm-4 col-form-label">สถานะ</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlStatusActive_Ins" name="select" class="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbStatusActive_Ins" runat="server" CssClass="validation"></asp:Label>

                                </div>
                            </div>
                            <div class="text-center m-t-20 col-sm-12">
                                <asp:Button type="button" ID="btnSubmit" Text="บันทึก" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Click" runat="server" />
                                <asp:Button type="button" ID="btnCancel" Text="ยกเลิก" OnClick="btnCancel_Click" class="button-pri button-cancel m-r-10" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>
        </div>
    </div>




</asp:Content>

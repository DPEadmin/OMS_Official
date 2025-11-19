<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="TemplateManagement.aspx.cs" Inherits="DOMS_TSR.src.Promotion.TemplateManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
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
    <link rel="stylesheet" type="text/css" href="http://harvesthq.github.io/chosen/chosen.css">
    <script type="text/javascript" src="http://harvesthq.github.io/chosen/chosen.jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#modal-Template').on('shown.bs.modal', function () {
                $('.chosen-select', this).chosen();
                $('.chosen-select1', this).chosen();
                $('#example1').emojioneArea({ autoHideFilters: true });
            });
            
        });
    </script>
    <script type="text/javascript">
        function DeleteConfirm() {
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

        function DeleteConfirmGV() {

            var grid = document.getElementById("<%= gvTemplate.ClientID %>");

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

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hd" runat="server" />
            <div class="page-body">

                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหา Template</div>
                            </div>
                            <div class="card-block">


                                <div class="form-group row">

                                    <label class="col-sm-2 col-form-label">Platform</label>
                                    <div class="col-sm-4">
                                        <asp:CheckBoxList runat="server" ID="chkPlatform_Search" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                        <asp:HiddenField ID="hidTemplatePicUrl_Ins" runat="server" />
                                        <asp:HiddenField ID="hidMerchantCode" runat="server" />
                                    </div>
                                    
                                    <label class="col-sm-2 col-form-label">Template Name</label>
                                    <div class="col-sm-4">
                                        
                                               <asp:TextBox  ID="txtSearchTemplateName" class="form-control" placeholder="ชื่อ Template" runat="server" ></asp:TextBox>
                                     

                                    </div>
                                    <label class="col-sm-2 col-form-label">Type</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlTemplateType_Search" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                          <label class="col-sm-2 col-form-label">Active</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlActive_Search" runat="server" class="form-control">
                                            <asp:ListItem Text="---- กรุณาเลือก ----" Value="-99" />
                                            <asp:ListItem Text="เปิดใช้งาน" Value="Y" />
                                            <asp:ListItem Text="ปิดใช้งาน" Value="N" />
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click" class="button-pri button-cancel" runat="server" />
                                </div>

                            </div>
                        </div>



                        <div class="card">
                            <div class="card-block">
                                
                                <div class="m-b-10">
                                    <!--Start modal Add Template-->
                                    <asp:LinkButton ID="btnAddTemplate" class="button-action button-add" data-backdrop="false" OnClick="btnAddTemplate_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmGV();" class="button-action button-delete " runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                    
                                </div>

                                <div class="table-responsive">
                                <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " Style="white-space: nowrap" TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvTemplate_RowCommand" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                           <asp:CheckBox ID="chkTemplateAll" OnCheckedChanged="chkTemplateAll_Change" AutoPostBack="true" runat="server"  />
                                                                </center>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkTemplate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">Active</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <div class="hideText">
                                                    <asp:Label ID="lblFlagActive" Text='<%# DataBinder.Eval(Container.DataItem, "FlagActive")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "FlagActive")%>' runat="server" />
                                                </div>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">TemplateCode</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <div class="hideText">
                                                    <asp:Label ID="lblTemplateCode" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateCode")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "TemplateCode")%>' runat="server" />
                                                </div>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                          <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"  HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">TemplateName</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblTemplateName" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateName")%>' runat="server" />

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                            <HeaderTemplate>

                                                <div align="Center">Type</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <div class="hideText">
                                                    <asp:Label ID="lblTemplateType" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateType")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "TemplateType")%>' runat="server" />
                                                </div>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                               
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"  HeaderStyle-CssClass="TDHead">

                                            <HeaderTemplate>

                                                <div align="Center">รายละเอียด</div>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblTemplateBody" Text='<%# DataBinder.Eval(Container.DataItem, "TemplateBody")%>' runat="server" />

                                            </ItemTemplate>
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                       
                                        
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                            <HeaderTemplate>
                                                  <div align="Center">แก้ไข</div>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowTemplate" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="button-pri button-activity m-r-10 icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                <%--<asp:LinkButton ID="buttonDelete" runat="Server" OnClientClick="return DeleteConfirm();" CommandName="DeleteTemplate"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="button-activity  " >  <span class="ti-trash f-14"></span></asp:LinkButton>--%>


                                                <asp:HiddenField runat="server" ID="hidTemplateId" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateId")%>' />
                                                <asp:HiddenField runat="server" ID="hidTemplateName" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateName")%>' />
                                                <asp:HiddenField runat="server" ID="hidTemplateCode" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidTemplateBody" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateBody")%>' />ฃ
                                                <asp:HiddenField runat="server" ID="hidTemplateType" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateType")%>' />
                                                <asp:HiddenField runat="server" ID="hidTemplateImageURL" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateImageURL")%>' />
                                                <asp:HiddenField runat="server" ID="hidTemplateVideoURL" Value='<%# DataBinder.Eval(Container.DataItem, "TemplateVideoURL")%>' />
                                                <asp:HiddenField runat="server" ID="hidFlagActive" Value='<%# DataBinder.Eval(Container.DataItem, "FlagActive")%>' />
                                                 <asp:HiddenField runat="server" ID="hidMerchant" Value='<%# DataBinder.Eval(Container.DataItem, "MerchantCode")%>' />
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
                                    </div>
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
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button  pagina_btn" ToolTip="First" CommandName="First"
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

                        <!-- Basic Form Inputs card end -->
                    </div>
                </div>
            </div>
     
        </ContentTemplate>
         
    </asp:UpdatePanel>

       <div class="modal fade" id="modal-Template" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 80%">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2    ">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มTemplate</div>

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
                                           <label class="col-sm-2 col-form-label">Platform<span style="color: red; background-position: right top;">*</span></label>
                                           <div class="col-sm-4">
                                           <asp:CheckBoxList runat="server" ID="chkPlatform_ins" />
                                               <asp:Label ID="lblPlatformTemplate_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                           <label class="col-sm-2 col-form-label">Type<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlTemplateType_Ins" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:Label ID="lblTemplateType_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                            <label class="col-sm-2 col-form-label">ชื่อ Template<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtTemplateName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblTemplateName_Ins" runat="server" CssClass="validation"></asp:Label>

                                            </div>
                                            <label class="col-sm-2 col-form-label">Active</label>
                                            <div class="col-sm-4">
                                                  <asp:DropDownList ID="ddlActive_ins" runat="server" class="form-control">
                                                    <asp:ListItem Text="---- กรุณาเลือก ----" Value="-99" />
                                                    <asp:ListItem Text="เปิดใช้งาน" Value="Y" />
                                                    <asp:ListItem Text="ปิดใช้งาน" Value="N" /> 
                                                </asp:DropDownList>
                                                <asp:Label ID="lblTemplateActive" runat="server" CssClass="validation"></asp:Label>
                                             </div>
                                            <label class="col-sm-2 col-form-label">Body Attribute<span style="color: red; background-position: right top;">*</span></label>
                                            <div class="col-sm-8">
                                                  <asp:CheckBoxList OnSelectedIndexChanged = "chkTemplateParam_Check" AutoPostBack="True" runat="server" ID="chkTemplateParam" />
                                                  <asp:Label ID="lblTemplateParam" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                             <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">รายละเอียด<span style="color: red; background-position: right top;">*</span></label>
                                             <div class="col-sm-8">
                                                 <textarea id="example1" runat="server"></textarea>
                                                 <asp:Label ID="lblTemplateBody" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">รูปภาพ</label>
                        <div class="col-sm-4">
                             <input type="file" name="filePic[]" id="filer_PIC" multiple="multiple">
                        </div>
                       
                    </div>
                    <%--   <div class="form-group row">
                        <label class="col-sm-2 col-form-label">วีดีโอ</label>
                        <div class="col-sm-4">
                            <input type="file" name="fileVDO[]" id="filer_VDO" multiple="multiple">
                        </div>
                       
                    </div>--%>
                    <div class="text-center m-t-20 col-sm-12">
                         <asp:Button ID="btnSubmit" Text="บันทึก" OnClick="btnSubmit_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnCancel" Text="ยกเลิก" OnClick="btnCancel_Click"
                                        class="button-pri button-cancel"
                                        runat="server" />
                    </div>
                </div>

            </div>
        </div>
    </div>
 
   

    <script>
      

        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

</asp:Content>

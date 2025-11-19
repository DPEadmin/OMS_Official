<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="PointAdjust.aspx.cs" Inherits="DOMS_TSR.src.Point.PointAdjust" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .validation {
            color: red;
        }
    </style>
    <script type="text/javascript">


        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvPointRange.ClientID %>");

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
                                <div class="sub-title">Manage Point</div>
                            </div>
                           <div class="card-block">

                                        <div id="searchSection_NoAnswerOrder" runat="server">

                                            <div class="form-group row">
                                                  <label class="col-sm-2 col-form-label">อัตราการแปลงค่า point</label>
					                    <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />
                                        <asp:HiddenField ID="hidMerchantCode" runat="server" />
                                        <asp:HiddenField ID="hidBu" runat="server" />
                                        <asp:HiddenField ID="hidWfStatus" runat="server" />
                                        <asp:HiddenField ID="hidFinishFlag" runat="server" />
                                        <asp:HiddenField ID="hidLevels" runat="server" />
                                        <asp:HiddenField ID="hidFlagSavedraft" runat="server" />
                                        <asp:HiddenField ID="hidFlagApprove" runat="server" />
                                        
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtCurrencyName" class="form-control" placeholder="จำนวน (บาท)" runat="server" disabled></asp:TextBox>
                                                        <asp:TextBox ID="txtPrice" type="number" class="form-control" runat="server"></asp:TextBox>
                                                        <asp:Label ID="lblPrice_Ins" runat="server" CssClass="validation"></asp:Label>
                                                    </div>
                                                </div>
                                           
 
                                                <label><span class ="icofont icofont-arrow-right f-20"></span></label>
                                                <div class="col-sm-4">
                                                    <div class="input-group mb-0">
                                                        <asp:TextBox ID="txtGetPoint" type="number" class="form-control" runat="server" ></asp:TextBox>
                                                        <asp:TextBox  class="form-control" placeholder="point" runat="server" disabled></asp:TextBox>
                                                        <asp:Label ID="lblGetPoint_Ins" runat="server" CssClass="validation"></asp:Label>

                                                        <input type="hidden" id="hidPointRateID" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btnSavePrice" class="button-pri button-accept m-r-10 " OnClick="btnSavePrice_Click" Text="บันทึก" runat="server" />
                                                </div>
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
                                                <!--Start modal Add Promotion-->
                                                <asp:LinkButton ID="btnAddPointRange" class="button-action button-add m-r-5"
                                                    OnClick="btnAddPointRange_Click" runat="server"><i class="fa fa-plus m-r-5"></i>Add</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>Delete</asp:LinkButton>
                                            </div>

                                            <asp:HiddenField ID="hidMOQFlagPromotion" runat="server" />
                                            <div class="table-responsive">
                                            <asp:GridView ID="gvPointRange" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvPointRange_RowCommand"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                                            <asp:CheckBox ID="chkPointRangeAll" OnCheckedChanged="chkPointRange_Change" AutoPostBack="true" runat="server"  />
                                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPointRange" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PointCode</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblPointCode" Text='<%# DataBinder.Eval(Container.DataItem, "PointCode")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PointName</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPointName" Text='<%# DataBinder.Eval(Container.DataItem, "PointName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PointSequence</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblPointSequence" Text='<%# DataBinder.Eval(Container.DataItem, "PointSequence")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PointBegin</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPointBegin" Text='<%# DataBinder.Eval(Container.DataItem, "PointBegin")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="left">PointEnd</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPointEnd" Text='<%# DataBinder.Eval(Container.DataItem, "PointEnd")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                             


                                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowPointRange"
                                                                class="button-activity   "
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>



                                                            <asp:HiddenField runat="server" ID="hidPointId" Value='<%# DataBinder.Eval(Container.DataItem, "PointId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPointCode" Value='<%# DataBinder.Eval(Container.DataItem, "PointCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPointSequence" Value='<%# DataBinder.Eval(Container.DataItem, "PointSequence")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPointName" Value='<%# DataBinder.Eval(Container.DataItem, "PointName")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPointBegin" Value='<%# DataBinder.Eval(Container.DataItem, "PointBegin")%>' />
                                                            <asp:HiddenField runat="server" ID="hidPointEnd" Value='<%# DataBinder.Eval(Container.DataItem, "PointEnd")%>' />
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
                                            <div class="m-t-10"></div>
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
                                <!-- Basic Form Inputs card end -->
                            </div>
                        </div>
                        <!-- Basic Form Inputs card end -->
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="modal-PointRange" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="max-width: 40%">

            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2">
                            <div class="col-sm-12 p-0">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มชนิด Point</div>

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
                          
                                <label class="col-sm-4 col-form-label">รหัสระดับ<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">

                                    <asp:TextBox ID="txtPromotionCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblPromotionCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:HiddenField ID="hidPromotionCode_Ins" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidSeq_Spare" runat="server"></asp:HiddenField>
                                    <asp:HiddenField runat="server" ID="hidPromotionImgId" />

                                </div>
                                
                                <label class="col-sm-4 col-form-label">ชื่อระดับ<span style="color: red; background-position: right top;">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPromotionName_Ins" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="LbPromotionName_Ins" runat="server" CssClass="validation"></asp:Label>
                                    <asp:Label ID="hidPromotionName_Ins" runat="server" CssClass="validation"></asp:Label>


                                </div>
                                 <label class="col-sm-4 col-form-label" id ="lblBanPointSeq" runat="server">ลำดับ<span style="color: red; background-position: right top;">*</span></label>
                                 <div class="col-sm-8" id ="divPointSeq" runat="server">
                                    <asp:TextBox ID="txtPointSeq" type = "number"  runat="server" class="form-control"></asp:TextBox>
                                    <asp:Label ID="lblPointSeq_Ins" runat="server" CssClass="validation"></asp:Label>
                                     <asp:Button type="button" OnClick="txtPointSeqOnClick" ID="btnCheckSeq" Text="ตรวจสอบ" class="button-pri button-accept m-r-10 " runat="server" />
                                </div>
                                
                                    

                                </div>
                                 <div class="col-sm-12">
                                        <div class="input-group mb-0">
                                            <asp:TextBox ID="txtPointBegin_Ins" type = "number" class="form-control" placeholder="PointBegin" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtPointEnd_Ins" type = "number" class="form-control" placeholder="PointEnd" runat="server"></asp:TextBox>
                                            
                                        </div>
                                     <asp:Label ID="lblPointRange_Ins" runat="server" CssClass="validation"></asp:Label>
                                </div>
                            </div>

                            <input type="hidden" id="hidPointRangeID_Ins" runat="server" />
                            <input type="hidden" id="hidPointBegin_Ins" runat="server" />
                            <input type="hidden" id="hidPointEnd_Ins" runat="server" />
                           

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="text-center m-t-20 col-sm-12">
                        <asp:Button type="button" ID="btnSubmit" Text="สร้าง" class="button-pri button-accept m-r-10 " OnClick="btnSubmit_Click" runat="server" />
                        <asp:Button type="button" ID="btnSavedraft" Text="ฉบับร่าง" class="button-pri button-accept m-r-10 " OnClick="btnSavedraft_Click" runat="server" />
                        <asp:Button type="button" ID="btnCancel" Text="ยกเลิก" OnClick="btnCancel_Click" class="button-pri button-cancel m-r-10" runat="server" />
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

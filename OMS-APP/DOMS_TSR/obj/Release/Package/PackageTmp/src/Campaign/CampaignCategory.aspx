<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="CampaignCategory.aspx.cs" Inherits="DOMS_TSR.src.Campaign.CampaignCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="http://harvesthq.github.io/chosen/chosen.css">
    <script type="text/javascript" src="http://harvesthq.github.io/chosen/chosen.jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#modal-Campaigncategory').on('shown.bs.modal', function () {
                $('.chosen-select', this).chosen();
                $('.chosen-select1', this).chosen();
            });
        });
    </script>
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

        $(function () {
            var dropZoneId = "drop-zone";
            var buttonId = "clickHere";
            var mouseOverClass = "mouse-over";
            var dropZone = $("#" + dropZoneId);
            var inputFile = dropZone.find("input");
            var finalFiles = {};


            var ooleft = dropZone.offset().left;
            var ooright = dropZone.outerWidth() + ooleft;
            var ootop = dropZone.offset().top;
            var oobottom = dropZone.outerHeight() + ootop;

            document.getElementById(dropZoneId).addEventListener("dragover", function (e) {
                e.preventDefault();
                e.stopPropagation();
                dropZone.addClass(mouseOverClass);
                var x = e.pageX;
                var y = e.pageY;

                if (!(x < ooleft || x > ooright || y < ootop || y > oobottom)) {
                    inputFile.offset({
                        top: y - 15,
                        left: x - 100
                    });
                } else {
                    inputFile.offset({
                        top: -400,
                        left: -400
                    });
                }

            }, true);

            if (buttonId != "") {
                var clickZone = $("#" + buttonId);

                var oleft = clickZone.offset().left;
                var oright = clickZone.outerWidth() + oleft;
                var otop = clickZone.offset().top;
                var obottom = clickZone.outerHeight() + otop;

                $("#" + buttonId).mousemove(function (e) {
                    var x = e.pageX;
                    var y = e.pageY;
                    if (!(x < oleft || x > oright || y < otop || y > obottom)) {
                        inputFile.offset({
                            top: y - 15,
                            left: x - 160
                        });
                    } else {
                        inputFile.offset({
                            top: -400,
                            left: -400
                        });
                    }
                });
            }

            document.getElementById(dropZoneId).addEventListener("drop", function (e) {
                $("#" + dropZoneId).removeClass(mouseOverClass);
            }, true);


            inputFile.on('change', function (e) {
                finalFiles = {};
                $('#filename').html("");
                var fileNum = this.files.length,
                    initial = 0,
                    counter = 0;

                $.each(this.files, function (idx, elm) {
                    finalFiles[idx] = elm;
                });

                for (initial; initial < fileNum; initial++) {
                    counter = counter + 1;
                    $('#filename').append('<div id="file_' + initial + '"><span class="fa-stack fa-lg"><i class="fa fa-file fa-stack-1x "></i><strong class="fa-stack-1x" style="color:#FFF; font-size:12px; margin-top:2px;">' + counter + '</strong></span> ' + this.files[initial].name + '&nbsp;&nbsp;<span class="fa fa-times-circle fa-lg closeBtn" onclick="removeLine(this)" title="remove"></span></div>');
                }
            });

            function removeLine(obj) {
                inputFile.val('');
                var jqObj = $(obj);
                var container = jqObj.closest('div');
                var index = container.attr("id").split('_')[1];
                container.remove();

                delete finalFiles[index];
                //console.log(finalFiles);
            }


        })




        function DeleteConfirm() {

            var grid = document.getElementById("<%= gvProduct.ClientID %>");

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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="page-body">

                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูลแบรนด์</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">รหัสแบรนด์</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchCampaignCategoryCode" class="form-control" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                    <label class="col-sm-2 col-form-label">ชื่อแบรนด์</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchCampaignCategoryName" class="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="text-center m-t-20 col-sm-12">

                                    <asp:Button ID="btnSearch" Text="ค้นหา" OnClick="btnSearch_Click"
                                        class="button-pri button-accept m-r-10"
                                        runat="server" />
                                    <asp:Button ID="btnClearSearch" Text="ล้าง" OnClick="btnClearSearch_Click"
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
                                                <!--Start modal Add Product-->
                                                <asp:LinkButton ID="btnAddProduct" class="button-action button-add m-r-10"
                                                    OnClick="btnAddProduct_Click" runat="server"><i class="fa fa-plus m-r-5"></i>เพิ่ม</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="button-action button-delete" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>
                                            </div>


                                            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                                                TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProduct_RowCommand"
                                                ShowHeaderWhenEmpty="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                        <HeaderTemplate>
                                                            <center>
                                            <asp:CheckBox ID="chkProductAll" OnCheckedChanged="chkProductAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:CheckBox ID="chkProduct" runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">รหัสแบรนด์</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblCampaignCategoryCode" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryCode")%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                        <HeaderTemplate>

                                                            <div align="Center">ชื่อแบรนด์</div>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' runat="server" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowProduct"
                                                                class="button-activity m-r-5  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                            <asp:HiddenField runat="server" ID="hidCampaignCategorytId" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryId")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCampaignCategoryCode" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hidCampaignCategoryName" Value='<%# DataBinder.Eval(Container.DataItem, "CampaignCategoryName")%>' />



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
                                                                <td style="font-size: 8.5pt;"></td>
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
        aria-hidden="true" id="modal-Campaigncategory">
        <div class="modal-dialog modal-lg" style="max-width: 650px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0 ">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">เพิ่มแบรนด์</div>

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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card-block">
                                <asp:UpdatePanel ID="upModal" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">รหัสแบรนด์</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtCampaignCategoryCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblCampaignCategoryCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidCampaignCategoryCode_Ins" runat="server"></asp:HiddenField>
                                                <input type="hidden" id="hidPicturePromotionUrl_Ins" runat="server" />
                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">ชื่อแบรนด์</label>
                                            <div class="col-sm-6">

                                                <asp:TextBox ID="txtCamCate_name_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblCamCate_name_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="form-group row">
                                    <label class="col-sm-4 col-form-label">รูปภาพ</label>
                                    <div class="col-sm-8">
                                        <input type="file" name="files[]" id="filer_input1">
                                        <%-- <input type="file" name="files[]" id="filer_input1" multiple="multiple">--%>
                                    </div>
                                </div>

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

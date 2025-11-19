<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="Logistic.aspx.cs" Inherits="DOMS_TSR.src.Logistic.Logistic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
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

            var grid = document.getElementById("<%= gvLogistic.ClientID %>");

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
        function validatenumerics(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57)) {
                alert(" กรุณาระบุตัวเลข ");
                return false;
            }
            else return true;
        }

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
                    <div class="sub-title">จัดการขนส่งสินค้า (Logistic Management)</div>
                </div>
                <div class="card-block">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>


                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">รหัสการขนส่งสินค้า</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtSearchLogisticCode" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchLogisticCode" runat="server" CssClass="validatecolor"></asp:Label>
                                </div>
                                <label class="col-sm-1 col-form-label"></label>
                                <label class="col-sm-2 col-form-label">ชื่อการขนส่งสินค้า</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtSearchLogisticName" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchLogisticName" runat="server" CssClass="validatecolor"></asp:Label>
                                    <input type="hidden" id="hidIdList" runat="server" />
                                    <input type="hidden" id="hidFlagInsert" runat="server" />
                                    <asp:HiddenField ID="hidFlagDel" runat="server" />
                                    <input type="hidden" id="hidaction" runat="server" />
                                    <asp:HiddenField ID="hidMsgDel" runat="server" />
                                    <asp:HiddenField ID="hidEmpCode" runat="server" />
                                </div>

                                <label class="col-sm-2 col-form-label">ประเภทการขนส่ง</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlsearchLogictype" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-1 col-form-label"></label>
                                <label class="col-sm-2 col-form-label"></label>
                                <div class="col-sm-3">
                                </div>

                            </div>
                            <div class="text-center m-t-20 col-sm-12">
                                <asp:Button ID="btnSearch" Text="ค้นหา" 
                                          class="button-pri button-accept m-r-10"
                                    OnClick="btnSearch_Click" runat="server" />
                                <asp:Button ID="btnClearSearch" Text="ล้าง" 
                                        class="button-pri button-cancel"
                                    OnClick="btnClearSearch_Click" runat="server" />
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

                                <asp:LinkButton ID="btnAddProduct" class="button-action button-add m-r-5"
                                    OnClick="btnAddProduct_Click" runat="server"><i class="fa fa-plus m-r-5 "></i>เพิ่ม การขนส่ง</asp:LinkButton>

                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                    class="button-action button-delete m-r-5" runat="server"><i class="fa fa-minus m-r-5"></i>ลบ</asp:LinkButton>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="dt-responsive table-responsive">
                                <asp:GridView ID="gvLogistic" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" OnRowCommand="gvProduct_RowCommand"
                                    TabIndex="0" Width="100%" CellSpacing="0"
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
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">รหัสการขนส่ง</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <!--<%# GetLink(DataBinder.Eval(Container.DataItem, "LogisticCode")) %>-->
                                                <asp:Label ID="lblLogisticCode" Text='<%# DataBinder.Eval(Container.DataItem, "LogisticCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="center">ชื่อการขนส่ง</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierName" Text='<%# DataBinder.Eval(Container.DataItem, "LogisticName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowProduct"
                                                    class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>

                                                <asp:HiddenField runat="server" ID="hidLogisticId" Value='<%# DataBinder.Eval(Container.DataItem, "LogisticId")%>' />
                                                <asp:HiddenField runat="server" ID="hidLogisticCode" Value='<%# DataBinder.Eval(Container.DataItem, "LogisticCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidLogisticName" Value='<%# DataBinder.Eval(Container.DataItem, "LogisticName")%>' />
                                                <asp:HiddenField runat="server" ID="hidEstimatedTime" Value='<%# DataBinder.Eval(Container.DataItem, "EstimatedTime")%>' />
                                                <asp:HiddenField runat="server" ID="hidLogisticType" Value='<%# DataBinder.Eval(Container.DataItem, "LogisticType")%>' />
                                                <asp:HiddenField runat="server" ID="hidLogisticstatus" Value='<%# DataBinder.Eval(Container.DataItem, "status")%>' />
                                                <asp:HiddenField runat="server" ID="hidTypeCalWeight" Value='<%# DataBinder.Eval(Container.DataItem, "TypeCalWeight")%>' />
                                                <asp:HiddenField runat="server" ID="hidTypeCalSize" Value='<%# DataBinder.Eval(Container.DataItem, "TypeCalSize")%>' />
                                                <asp:HiddenField runat="server" ID="hidTypeCalWeightSize" Value='<%# DataBinder.Eval(Container.DataItem, "TypeCalWeightSize")%>' />
                                                <asp:HiddenField runat="server" ID="hidFee" Value='<%# DataBinder.Eval(Container.DataItem, "Fee")%>' />
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
                        aria-hidden="true" id="modal-Logistic">
                        <div class="modal-dialog modal-lg" style="max-width: 1300px;">

                            <div class="modal-content">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="modal-header modal-header2 ">
                                            <div class="col-sm-11">
                                                <div id="exampleModalLongTitle">
                                                    เพิ่มการขนส่ง
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

                                                            <label class="col-sm-2 col-form-label">รหัสการขนส่ง</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtLogisticCode_Ins" runat="server" class="form-control"></asp:TextBox>
                                                                <asp:Label ID="lblLogisticCode_Ins" runat="server" CssClass="validation"></asp:Label>
                                                                <asp:HiddenField ID="hidLogisticCode_Ins" runat="server"></asp:HiddenField>
                                                            </div>
                                                            <label class="col-sm-1 col-form-label"></label>
                                                            <label class="col-sm-2 col-form-label">ชื่อการขนส่ง</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtLogisticName_Ins" runat="server" class="form-control"></asp:TextBox>
                                                                <asp:Label ID="lblLogisticName_Ins" runat="server" CssClass="validation"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-2 col-form-label">ประเภทการขนส่ง</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="ddlLogistype_Ins" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                                                                <asp:Label ID="lblogistype_Ins" runat="server" CssClass="validation"></asp:Label>
                                                            </div>
                                                            <label class="col-sm-1 col-form-label"></label>
                                                            <label class="col-sm-2 col-form-label">EstimatedTime</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtEstimatedTime_Ins" onkeypress="return validatenumerics(event);" runat="server" class="form-control"></asp:TextBox>
                                                                <asp:Label ID="lbEstimatedTime_Ins" runat="server" CssClass="validation"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-2 col-form-label">Active</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="ddlActive_Ins" class="form-control" runat="server">
                                                                    <asp:ListItem Enabled="true" Text="Please select status" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="ใช่" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="ไม่ใช่" Value="N"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblActive_Ins" runat="server" CssClass="validation"></asp:Label>
                                                            </div>
                                                            <label class="col-sm-1 col-form-label"></label>
                                                            <label class="col-sm-2 col-form-label">ค่าธรรมเนียมขนส่ง</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtFee_Ins" runat="server" onkeypress="return validatenumerics(event);" class="form-control"></asp:TextBox>
                                                                <asp:Label ID="lblFee_Ins" runat="server" CssClass="validation"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-2 col-form-label"></label>
                                                            <div class="col-sm-3">
                                                                <!--<asp:CheckBoxList ID="ClCondition_Ins" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>-->
                                                                <!--<asp:Label ID="lblCondition_Ins" runat="server" CssClass="validation"></asp:Label>-->
                                                            </div>
                                                            <label class="col-sm-1 col-form-label"></label>
                                                            <label class="col-sm-2 col-form-label"></label>
                                                            <div class="col-sm-3">
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <label class="col-sm-2 col-form-label"></label>
                                                <div class="text-center m-t-20 center">
                                                    <asp:Button ID="btnSubmit" Text="บันทึก" OnClick="btnSubmit_Click" class="button-pri button-accept m-r-10 " runat="server" />
                                                    <asp:Button ID="btnCancel" Text="ยกเลิก" OnClick="btnCancel_Click" class="button-pri button-cancel m-r-10" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
                        aria-hidden="true" id="modal-EditLogistic">
                        <div class="modal-dialog modal-lg" style="max-width: 1300px;">

                            <div class="modal-content">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="modal-header modal-header2 ">
                                            <div class="col-sm-11">
                                                <div id="exampleModalLongTitle01">
                                                    แก้ไขการขนส่ง
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

                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>

                                                        <div class="form-group row">

                                                            <label class="col-sm-2 col-form-label">รหัสการขนส่ง</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtEditLogisCode" runat="server" class="form-control"></asp:TextBox>
                                                                <asp:Label ID="lblEditLogisCode" runat="server" CssClass="validation"></asp:Label>
                                                                <asp:HiddenField ID="hidEditLogisCode" runat="server"></asp:HiddenField>
                                                            </div>
                                                            <label class="col-sm-1 col-form-label"></label>
                                                            <label class="col-sm-2 col-form-label">ชื่อการขนส่ง</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtEditLogisName" runat="server" class="form-control"></asp:TextBox>
                                                                <asp:Label ID="lblEditLogisName" runat="server" CssClass="validation"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                                <label class="col-sm-2 col-form-label"></label>

                                                <div class="text-center m-t-20 center">

                                                    <asp:Button ID="Button1" Text="Submit" OnClick="btnEditSubmit_Click"
                                                        class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                                        runat="server" />
                                                    <asp:Button ID="Button2" Text="Cancel" OnClick="btnEditCancel_Click"
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

        </div>
    </div>

</asp:Content>

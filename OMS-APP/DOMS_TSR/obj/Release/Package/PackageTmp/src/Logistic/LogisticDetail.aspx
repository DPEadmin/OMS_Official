<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="LogisticDetail.aspx.cs" Inherits="DOMS_TSR.src.Logistic.LogisticDetail" %>

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
                                <div class="sub-title">
                                    Logistic
                                    <asp:Label ID="Lbname" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">

                                    <div class="col-sm-3">

                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />

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
                                                <!--Start modal Add Product-->
                                                <asp:LinkButton ID="btnAddProduct" class="btn-add button-active btn-small"
                                                    OnClick="btnAddProduct_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm();"
                                                    class="btn-del button-active btn-small" runat="server"><i class="fa fa-minus"></i>Delete</asp:LinkButton>
                                            </div>

                                            <div class="dt-responsive table-responsive">
                                                <asp:GridView ID="gvLogistic" runat="server" AutoGenerateColumns="False" CssClass="table-p
                          table-striped table-bordered nowrap"
                                                    TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvProduct_RowCommand"
                                                    ShowHeaderWhenEmpty="true">

                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="95px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <center>
                                            <asp:CheckBox ID="chkProductAll" OnCheckedChanged="chkProductAll_Change" AutoPostBack="true" runat="server"  />
                                        </center>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkProduct" runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Logistic Code</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblLogisticCodeDetail" Text='<%# DataBinder.Eval(Container.DataItem, "LogisticCodeDetail")%>' runat="server" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">Fee</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFee" Text='<%# DataBinder.Eval(Container.DataItem, "Fee", "{0:N2}") %>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">PackageWidth</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPackageWidth" Text='<%# DataBinder.Eval(Container.DataItem, "PackageWidth")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">PackageLength</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPackageLength" Text='<%# DataBinder.Eval(Container.DataItem, "PackageLength")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">PackageHeigth</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPackageHeigth" Text='<%# DataBinder.Eval(Container.DataItem, "PackageHeigth")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">PackageWLH From</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPackageWLHFrom" Text='<%# DataBinder.Eval(Container.DataItem, "PackageWLHFrom")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">PackageWLH To</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPackageWLHTo" Text='<%# DataBinder.Eval(Container.DataItem, "PackageWLHTo")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">WeightFrom</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWeightFrom" Text='<%# DataBinder.Eval(Container.DataItem, "WeightFrom")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                            <HeaderTemplate>

                                                                <div align="center">WeightTo</div>

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWeightTo" Text='<%# DataBinder.Eval(Container.DataItem, "WeightTo")%>' runat="server" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">

                                                            <HeaderTemplate>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="btnEdit" runat="Server" CommandName="ShowProduct"
                                                                    class="button-active button-submit m-r-10  " Style="float: none; border-radius: 5px; padding: 3px 10px; padding-top: 5px;"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-16"></span></asp:LinkButton>
                                                                <asp:HiddenField runat="server" ID="hidLogisticDetailId" Value='<%# DataBinder.Eval(Container.DataItem, "LogisticDetailId")%>' />
                                                                <asp:HiddenField runat="server" ID="hidLogisticCodeDetail" Value='<%# DataBinder.Eval(Container.DataItem, "LogisticCodeDetail")%>' />
                                                                <asp:HiddenField runat="server" ID="hidFee" Value='<%# DataBinder.Eval(Container.DataItem, "Fee")%>' />
                                                                <asp:HiddenField runat="server" ID="HidPackageWidth" Value='<%# DataBinder.Eval(Container.DataItem, "PackageWidth")%>' />
                                                                <asp:HiddenField runat="server" ID="HidPackageLength" Value='<%# DataBinder.Eval(Container.DataItem, "PackageLength")%>' />
                                                                <asp:HiddenField runat="server" ID="HidPackageHeigth" Value='<%# DataBinder.Eval(Container.DataItem, "PackageHeigth")%>' />
                                                                <asp:HiddenField runat="server" ID="HidPackageWLHFrom" Value='<%# DataBinder.Eval(Container.DataItem, "PackageWLHFrom")%>' />
                                                              <asp:HiddenField runat="server" ID="HidPackageWLHTo" Value='<%# DataBinder.Eval(Container.DataItem, "PackageWLHTo")%>' />
                                                                <asp:HiddenField runat="server" ID="HidWeightFrom" Value='<%# DataBinder.Eval(Container.DataItem, "WeightFrom")%>' />
                                                                <asp:HiddenField runat="server" ID="HidWeightTo" Value='<%# DataBinder.Eval(Container.DataItem, "WeightTo")%>' />


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
                                    Add Channel
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
                                <asp:UpdatePanel ID="upModal" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">Fee :</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtFee_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblFee_Ins" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="hidFee_Ins" runat="server"></asp:HiddenField>

                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">PackageWidth :</label>

                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPackageWidth_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPackageWidth_Ins" runat="server" CssClass="validation"></asp:Label>

                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">PackageLength :</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPackageLength_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPackageLength_Ins" runat="server" CssClass="validation"></asp:Label>


                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">PackageHeigth :</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPackageHeigth_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPackageHeigth_Ins" runat="server" CssClass="validation"></asp:Label>
                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">PackageWLH From :</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPackageWLHFrom_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPackageWLHFrom_Ins" runat="server" CssClass="validation"></asp:Label>

                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">PackageWLH TO :</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPackageWLHTo_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblPackageWLHTo_Ins" runat="server" CssClass="validation"></asp:Label>

                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">Weight From :</label>
                                            <div class="col-sm-6">

                                                <asp:TextBox ID="txtWeightFrom_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblWeightFrom_Ins" runat="server" CssClass="validation"></asp:Label>

                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">Weight To :</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtWeightTo_Ins" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="lblWeightTo_Ins" runat="server" CssClass="validation"></asp:Label>


                                            </div>
                                            <label class="col-sm-2 col-form-label"></label>
                                            <label class="col-sm-4 col-form-label">Condition Calculation :</label>
                                            <div class="col-sm-6">
                                            
                                                <asp:Label ID="lb" runat="server" CssClass="validation"></asp:Label>


                                            </div>
                                            <div>
                                                <asp:GridView ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center"
                                                    CssClass=" table table-bordered prpa " TabIndex="0" CellSpacing="0" BorderColor="0">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="WEIGHT" ControlStyle-CssClass="col-sm-5" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="GvTxtWeightFrom" runat="server" Width="70px" class="form-control"></asp:TextBox>

                                                                            To :
                                                                            <asp:TextBox ID="GvTxtWeightTo" runat="server" Width="70px" class="form-control"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>



                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SIZE" ControlStyle-CssClass="col-sm-6" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>W:
                                                            <asp:TextBox ID="GvTxtSizeW" runat="server" Width="50px" class="form-control"></asp:TextBox>
                                                                            L:
                                                             <asp:TextBox ID="GvTxtSizeL" runat="server" Width="50px" class="form-control"></asp:TextBox>
                                                                            H:
                                                             <asp:TextBox ID="GvTxtSizeH" runat="server" Width="50px" class="form-control"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="W+L+H" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="GvTxtWLH" runat="server" Width="100px" class="form-control"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FREE" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="GvTxtFree" runat="server" Width="150px" class="form-control"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="ButtonAdd" runat="server" Text="Add Condition" Class="btn btn-success btnadd waves-effect waves-light m-r-10 btn-colorprimary" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ลบ">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="LinkButton1" runat="server" Class="btn btn-round  btn-sm btn-warning waves-effect waves-light m-r-10 btn-colorprimary">Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>





                                        <div class="text-center m-t-20 center">

                                            <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                                runat="server" />
                                            <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                                runat="server" />

                                        </div>

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
        aria-hidden="true" id="modal-EditLogistic">
        <div class="modal-dialog modal-lg" style="max-width: 1300px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2 ">
                            <div class="col-sm-11">
                                <div id="exampleModalLongTitle2">
                                    Add Logistic
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
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Logistic Code</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtEditLogisCode" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="Label3" runat="server" CssClass="validation"></asp:Label>
                                                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>

                                            </div>
                                            <label class="col-sm-1 col-form-label"></label>
                                            <label class="col-sm-2 col-form-label">Logistic Name</label>
                                            <div class="col-sm-3">

                                                <asp:TextBox ID="txtEditLogisName" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="Label4" runat="server" CssClass="validation"></asp:Label>

                                            </div>
                                            <label class="col-sm-2 col-form-label">Logistic Type</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                                <asp:Label ID="Label5" runat="server" CssClass="validation"></asp:Label>


                                            </div>
                                            <label class="col-sm-6 col-form-label"></label>
                                        </div>
                                        <div class="text-center m-t-20 center">

                                            <asp:Button ID="Button1" Text="Submit" OnClick="btnEditSubmit_Click"
                                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                                runat="server" />
                                            <asp:Button ID="Button2" Text="Cancel" OnClick="btnEditCancel_Click"
                                                class="btn btn-round  btn-sm btn-primary waves-effect waves-light m-r-10 btn-colorprimary"
                                                runat="server" />

                                        </div>

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

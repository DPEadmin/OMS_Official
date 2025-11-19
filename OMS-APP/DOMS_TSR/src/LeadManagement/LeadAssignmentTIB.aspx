<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="LeadAssignmentTIB.aspx.cs" Inherits="DOMS_TSR.src.LeadManagement.LeadAssignmentTIB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
         $('#modal-inventory').on('shown.bs.modal', function () {
             $('.chosen-select', this).chosen();
              $('.chosen-select1', this).chosen();
        });
    });
    </script>

    <script type="text/javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
            {
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

        function isNumberKey(evt, id) {
            try {
                var charCode = (evt.which) ? evt.which : event.keyCode;

                if (charCode == 46) {
                    var txt = document.getElementById(id).value;
                    if (!(txt.indexOf(".") > -1)) {

                        return true;
                    }
                }
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            } catch (w) {
                alert(w);
            }
        }

    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <input type="hidden" id="hidIdList" runat="server" />
            <input type="hidden" id="hidFlagInsert" runat="server" />
            <asp:HiddenField ID="hidFlagDel" runat="server" />
            <input type="hidden" id="hidaction" runat="server" />
            <asp:HiddenField ID="hidMsgDel" runat="server" />
            <asp:HiddenField ID="hidEmpCode" runat="server" />
            <asp:HiddenField ID="hidEmpBUSelected" runat="server" />
            <asp:HiddenField ID="hidEmpGroupBUSelected" runat="server" />
            <asp:HiddenField ID="hidbIdByList" runat="server" />
            <asp:HiddenField ID="hidbEmpGroupByList" runat="server" />
            <asp:HiddenField ID="hidEmpPercentByList" runat="server" />
            <asp:HiddenField ID="hidEmpRoundRobinList" runat="server" />
            <asp:HiddenField ID="hidEmpCodeAssign" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-8">
                         <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Lead Summary </div>
                            </div>
                            <div class="card-block">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <h6>Total</h6>
                                            <p style="vertical-align: middle">
                                                <asp:Label ID="lblsumtotal" runat="server" Text="1000"></asp:Label></p>
                                        </div>
                                        <div class="col-sm-2">
                                            <h6>Assigned</h6>
                                            <p>
                                                <asp:Label ID="lblsumassign" runat="server" Text="500"></asp:Label></p>
                                        </div>
                                        <div class="col-sm-2">
                                            <h6>Remain</h6>
                                            <p>
                                                <asp:Label ID="lblsumremain" runat="server" Text="500"></asp:Label></p>
                                        </div>
                                
                                    </div>
                                </div>



                            </div>

                        </div>
                        <div id="searchsection" runat="server">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="sub-title">Select distribution Lead</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Leadsource</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchCustomerFName" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Assign type</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchCustomerLName" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">BU</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchCustomerCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">quantity</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchCustomerPhone" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Date</label>
                                    <div class="col-sm-3">
                                        <div class="input-group mb-0">
                                                    <asp:TextBox ID="txtSearchCreateDateFrom" class="form-control" placeholder="Start" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchCreateDateFrom" runat="server" TargetControlID="txtSearchCreateDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    <asp:TextBox ID="txtSearchCreateDateTo" class="form-control" placeholder="To" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="carSearchCreateDateTo" runat="server" TargetControlID="txtSearchCreateDateTo" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>

                                  
                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">เวลา</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchTime" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Clicked" Text="Search" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearch" OnClick="btnClearSearch_Clicked" Text="Clear" class="button-pri button-cancel" runat="server" />
                                </div>
                            </div>
                            </div>
                        </div>
                        <div id="ordernoanswersection" runat="server">
                        <div class="card ">                            
                               <div class="col-5 m-t-10 m-b-10" >
                               
                            </div>
                                
                                    <div class="card-block p-t-5">

                                        <div id="Section_NoAnswerOrder" runat="server">

                                            <input type="hidden" id="hidIdList_NoAnswerOrder" runat="server" />

                                            <asp:Button CssClass="button-pri button-print  m-b-10" ID="btnMergeOrder_NoAnswerOrder" runat="server" Text="Export Excel" />

                                            <asp:Button CssClass="button-pri button-delete" ID="btnCancelOrder_NoAnswerOrder" runat="server" Text="Cancal order" Visible="false" />

                                            <asp:Panel ID="Panel_NoAnswerOrder" runat="server" Style="overflow-x: scroll;">
                                                <asp:GridView ID="gvOrder_NoAnswerOrder" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">

                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Order code</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                  <asp:Label ID="lblCustomerContact_NoAnswerOrder" Text='' runat="server" />
                                                         
                                                                              

                                                                <asp:HiddenField runat="server" ID="hidOrderCode_NoAnswerOrder" Value='' />
                                                               

                                                                <asp:HiddenField runat="server" ID="hidCreateDate_NoAnswerOrder" Value='' />

                                                                </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Customer ID</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerCode_NoAnswerOrder" Text='' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">First name last name</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName_NoAnswerOrder" Text='' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">customer phone number</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerContact_NoAnswerOrder" Text='' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">order date</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDate_NoAnswerOrder" Text='' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                            <HeaderTemplate>
                                                                <div align="Center">Delivery date</div>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeliverDate_NoAnswerOrder" Text='' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action" Visible="false">

                                                            <HeaderTemplate>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                              
                                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                    </Columns>

                                                    <EmptyDataTemplate>
                                                        <center>
                                    <asp:Label ID="lblDataEmpty_NoAnswerOrder" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                                <tr height="30" bgcolor="#ffffff">
                                                    <td width="100%" align="right" valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnFirst_NoAnswerOrder" CssClass="Button" ToolTip="First" CommandName="First"
                                                                        Text="<<" runat="server" ></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnPre_NoAnswerOrder" CssClass="Button" ToolTip="Previous" CommandName="Previous"
                                                                        Text="<" runat="server" ></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlPage_NoAnswerOrder" CssClass="textbox" runat="server" AutoPostBack="True"
                                                                                        >
                                                                                    </asp:DropDownList>
                                                                    of
                                                                                    <asp:Label ID="lblTotalPages_NoAnswerOrder" CssClass="fontBlack" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnNext_NoAnswerOrder" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" ></asp:Button>
                                                                </td>
                                                                <td style="width: 6px"></td>
                                                                <td>
                                                                    <asp:Button ID="lnkbtnLast_NoAnswerOrder" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last"
                                                                        Text=">>" ></asp:Button>
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
                        <div class="card">
                            <div class="card-body"> 
                                <div style="width: 100%; height: 500px; overflow: scroll">
                                <asp:GridView ID="gvLeadManagement" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" Style="white-space: nowrap" 
                                     TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" OnRowCreated="gvLeadManagement_RowCreated" OnRowDataBound="gvLeadManagement_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                <asp:CheckBox ID="chkLeadAll" OnCheckedChanged="chkLeadAll_click" AutoPostBack="true" runat="server"  />
                                            </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkLead" OnCheckedChanged="chkLead_CheckedChanged" AutoPostBack="true" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Customer ID</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Customer Name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "FULL_NAME_TH")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">customer phone number</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerPhone" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerPhone")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">LeadCode</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadCode" Text='<%# DataBinder.Eval(Container.DataItem, "LeadCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">callback date</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecontactbackDate" Text='<%# ((null == Eval("RecontactbackDate"))||("" == Eval("RecontactbackDate"))) ? string.Empty : DateTime.Parse(Eval("RecontactbackDate").ToString()).ToString("dd/MM/yyyy") %>' runat="server" />                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">callback time</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecontactbactPeriodTimeName" Text='<%# DataBinder.Eval(Container.DataItem, "RecontactbactPeriodTimeName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="Center">data import date</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreateDate" Text='<%# ((null == Eval("CreateDate"))||("" == Eval("CreateDate"))) ? string.Empty : DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd-MM-yyyy") %>' runat="server" />                                                                                               
                                                <br />
                                                <asp:Label ID="lblmsg" runat="server" CssClass="font12Red"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">contact</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCusReason" Text='<%# DataBinder.Eval(Container.DataItem, "CusReason")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">contact details(other)</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCusReasonOther" Text='<%# DataBinder.Eval(Container.DataItem, "CusReasonOther")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Transaction name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCusTransactionTypeName" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionTypeName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Status</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Name - Last name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                                                                          
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">contact number</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTelephone" Text='<%# DataBinder.Eval(Container.DataItem, "Telephone")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Insurance type</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInsurancetype" Text='<%# DataBinder.Eval(Container.DataItem, "Insurancetype")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">car year</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaryear" Text='<%# DataBinder.Eval(Container.DataItem, "Caryear")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">car model</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCarmodel" Text='<%# DataBinder.Eval(Container.DataItem, "Carmodel")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">car</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCartype" Text='<%# DataBinder.Eval(Container.DataItem, "Cartype")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">sub model</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCarsubmodel" Text='<%# DataBinder.Eval(Container.DataItem, "Carsubmodel")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Out of warranty or need protection</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInsurancedate" Text='<%# DataBinder.Eval(Container.DataItem, "Insurancedate")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Product name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">Promotion name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPromotionName" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hidLeadID" Value='<%# DataBinder.Eval(Container.DataItem, "LeadID")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerCode" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' />
                                                <asp:HiddenField runat="server" ID="hidCustomerName" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' />
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
                                <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                                    <tr height="30" bgcolor="#ffffff">
                                        <td width="100%" align="right" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                                <tr>
                                                    <td style="width: 12px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnFirst" CssClass="Button" ToolTip="First" CommandName="First" Text="<<" runat="server" OnCommand="GetPageIndex" ></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnPre" CssClass="Button" ToolTip="Previous" CommandName="Previous" Text="<" runat="server" OnCommand="GetPageIndex" ></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td style="font-size: 8.5pt;">Page
                                                    <asp:DropDownList ID="ddlPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" ></asp:DropDownList>
                                                        of
                                                    <asp:Label ID="lblTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnNext" CssClass="Button" ToolTip="Next" runat="server" CommandName="Next" Text=">" OnCommand="GetPageIndex" ></asp:Button>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="lnkbtnLast" CssClass="Button" ToolTip="Last" runat="server" CommandName="Last" Text=">>" OnCommand="GetPageIndex" ></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        
                    </div>

                    <div class="col-sm-4">

                        <div id="selectassigntype" class="card" runat="server">
                            <div class="card-header border-0">
                                <div class="sub-title">Assign Type</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                     
                                    <div class="col-sm-8">
                                        <asp:RadioButtonList ID="radAssignType" OnSelectedIndexChanged="radAssignType_SelectedChanged" AutoPostBack="true" runat="server">
                                        <asp:ListItem Selected="True" Value="0">&nbsp;Single Emp</asp:ListItem>
                                        <asp:ListItem Value="1">&nbsp;Multi Emp Average</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp;Multi Emp Percent Average</asp:ListItem>
                                        <asp:ListItem Value="3">&nbsp;Multi Emp Round Robin</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    
                                </div>                                                                                                  
                            </div>
                        </div>
                        
                        <div id="singleemp" class="card" runat="server">
                            <div class="card-header border-0">
                                <div class="sub-title">Select Employee</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">BU</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlEmpBU" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpBU_SelectedIndexChanged" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>   
                                <style>
                                    .nowrap {
                                        white-space:nowrap
                                    }
                                    .button_width_auto {
                                        width:auto
                                    }
                                </style>
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label nowrap">Employee</label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox ID="txtEmpAssigned" class="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblEmpAssigned" runat="server" CssClass="validation"></asp:Label>
                                        <div class = "text-center">
                                        <asp:Button ID="btnAssign" OnClick="btnAssign_Clicked" Text="Continue" class="button-pri button-accept m-r-10 " runat="server" />
                                        </div>
                                    </div>

                                    <%--<label class="col-sm-0 col-form-label"></label>--%>
                                    <div class="col-sm-2 p-l-0">
                                        <asp:Button ID="btnSearchEmpAssign" OnClick="btnSelectEmp_Clicked" Text="เรียกดู" class="button-pri button-accept button_width_auto" runat="server" />
                                    </div>
                                </div>

                
                            </div>
                        </div>

                        <div id="groupemp" class="card" runat="server">
                            <div class="card-header border-0">
                                <div class="sub-title">Select random employee</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">BU</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlEmpBUGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpBUGroup_SelectedIndexChanged" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <label class="col-sm-0 col-form-label"></label>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSelectEmpGroupAssign" OnClick="btnSelectEmpGroupAssign_Clicked" Text="..." class="button-pri button-accept m-r-10" runat="server" />
                                    </div>
                                    
                                </div>   
                                
                                <asp:GridView ID="gvEmpAssignGroup" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             >
                                            <Columns>                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Employee Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpGroupCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ สกุล พนักงาน</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblEmpGroupName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">No. Lead</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:TextBox style="width:40px;text-align:right" ID="txtNumberLeadAssign" OnTextChanged="txtNumberLeadAssign_Clicked" AutoPostBack="true" Text='<%# Eval("NumberLeadAssign") %>' onkeypress="return validatenumerics(event);" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnClose" AutoPostBack="True" OnClick="btnCloseEmp_Click" runat="server"><i class="ti-close"></i></asp:LinkButton>

                                                        <asp:HiddenField ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidrunningNo" Value='<%# DataBinder.Eval(Container.DataItem, "runningNo")%>' runat="server"/>

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

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnAssignLeadEmpGroup" OnClick="btnAssignLeadEmpGroup_Clicked" Text="Assign" class="button-pri button-accept m-r-10" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div id="emppercent" class="card" runat="server">
                            <div class="card-header border-0">
                                <div class="sub-title">Select employees by distribution %</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">BU</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlEmpBUPercent" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <label class="col-sm-0 col-form-label"></label>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSelectEmpPercentAssign" OnClick="btnSelectEmpPercentAssign_Clicked" Text="..." class="button-pri button-accept m-r-10" runat="server" />
                                    </div>
                                    
                                </div>   
                                
                                <asp:GridView ID="gvEmpAssignPercent" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             >
                                            <Columns>                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Employee Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpPercentCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ สกุล พนักงาน</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblEmpGroupName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Percent Lead(%)</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:TextBox style="width:60px;text-align:right" ID="txtPercentLeadAssign" OnTextChanged="txtPercentLeadAssign_TextChanged" onkeypress="return isNumberKey(event,this.id)" AutoPostBack="true" Text='<%# Eval("PercentLeadAssign") %>' runat="server" ></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCloseEmpAssignPercent" AutoPostBack="True" OnClick="btnCloseEmpAssignPercent_Click" runat="server"><i class="ti-close"></i></asp:LinkButton>

                                                        <asp:HiddenField ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidrunningNo" Value='<%# DataBinder.Eval(Container.DataItem, "runningNo")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidNumberLeadAssign" Value='<%# DataBinder.Eval(Container.DataItem, "NumberLeadAssign")%>' runat="server"/>

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

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnAssignLeadPercent" OnClick="btnAssignLeadPercent_Clicked" Text="Assign" class="button-pri button-accept m-r-10" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div id="emproundrobin" class="card" runat="server">
                            <div class="card-header border-0">
                                <div class="sub-title">Select Employee from Round Robin</div>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">BU</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlEmpBURoundRobin" AutoPostBack="true" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <label class="col-sm-0 col-form-label"></label>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSelectEmpRoundRobinAssign" OnClick="btnSelectEmpRoundRobinAssign_Clicked" Text="..." class="button-pri button-accept m-r-10" runat="server" />
                                    </div>
                                    
                                </div>   
                                
                                <asp:GridView ID="gvEmpAssignRoundRobin" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             >
                                            <Columns>                                                
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Employee Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpRoundRobinCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">ชื่อ สกุล พนักงาน</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblEmpGroupName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <%--<asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Percent Lead(%)</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:TextBox style="width:60px;text-align:right" ID="txtPercentLeadAssign" OnTextChanged="txtPercentLeadAssign_TextChanged" onkeypress="return isNumberKey(event,this.id)" AutoPostBack="true" Text='<%# Eval("PercentLeadAssign") %>' runat="server" ></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCloseEmpAssignRoundRobin" OnClick="btnCloseEmpAssignRoundRobin_Clicked" AutoPostBack="True" runat="server"><i class="ti-close"></i></asp:LinkButton>

                                                        <asp:HiddenField ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidrunningNo" Value='<%# DataBinder.Eval(Container.DataItem, "runningNo")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidNumberLeadAssign" Value='<%# DataBinder.Eval(Container.DataItem, "NumberLeadAssign")%>' runat="server"/>

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

                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnAssignLeadRoundRobin" OnClick="btnAssignLeadRoundRobin_Clicked" Text="Assign" class="button-pri button-accept m-r-10" runat="server" />
                                </div>
                            </div>
                        </div>
                        
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade " tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
        aria-hidden="true" id="modal-emp">
        <div class="modal-dialog modal-lg" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle" class="modal-title sub-title " style="font-size: 16px;">ค้นหาพนักงาน</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
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

                                <asp:UpdatePanel ID="UpModal" runat="server">
                                    <ContentTemplate>
                                        
                                        <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpCode" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label"></label>
                                    <div class="col-sm-3">
                                        
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpFName_TH" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Employee Surname</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpLName_TH" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                        <div class="text-center m-t-20 center">
                                    <asp:Button ID="btnSearchEmp" OnClick="btnSearchEmp_Clicked" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearchEmp" OnClick="btnClearSearchEmp_Clicked" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>

                                        <hr />

                                        <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             OnRowCommand="gvEmp_RowCommand">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Employee Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Employee Name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblEmpName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="Server" CommandName="SelectEmp" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>

                                                        <asp:HiddenField ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' runat="server"/>

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
                        <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                            <tr height="30" bgcolor="#ffffff">
                                <td width="100%" align="right" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                        <tr>
                                            <td style="font-size: 8.5pt;">                                               
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpFirst" CssClass="Button" ToolTip="gvEmpFirst" CommandName="gvEmpFirst" OnCommand="GetPagegvEmpIndex"
                                                    Text="<<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpPre" CssClass="Button" ToolTip="gvEmpPrevious" CommandName="gvEmpPrevious" OnCommand="GetPagegvEmpIndex"
                                                    Text="<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlgvEmpPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgvEmpPage_SelectedIndexChanged"
                                                                                      >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblgvEmpTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpNext" CssClass="Button" ToolTip="gvEmpNext" runat="server" CommandName="gvEmpNext" Text=">" OnCommand="GetPagegvEmpIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpLast" CssClass="Button" ToolTip="gvEmpLast" runat="server" CommandName="gvEmpLast"
                                                    Text=">>" OnCommand="GetPagegvEmpIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

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
        aria-hidden="true" id="modal-empgroup">
        <div class="modal-dialog modal-lg" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle01" class="modal-title sub-title " style="font-size: 16px;">ค้นหาพนักงาน</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
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

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        
                                        <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpGroup" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label"></label>
                                    <div class="col-sm-3">
                                        
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpFNameGroup" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Employee Surname</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpLNameGroup" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                        <div class="text-center m-t-20 center">
                                    <asp:Button ID="btnSearchEmpGroup" OnClick="btnSearchEmpGroup_Clicked" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearchEmpGroup" OnClick="btnClearSearchEmpGroup_Clicked" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>

                                        <hr />

                                        <asp:GridView ID="gvEmpGroup" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             >
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                <asp:CheckBox ID="chkEmpGroupAll" OnCheckedChanged="chkEmpGroupAll_click" AutoPostBack="true" runat="server"  />
                                            </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmpGroup" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Employee Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpGroupCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Employee Name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblEmpGroupName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="btnEdit" runat="Server" CommandName="SelectEmp" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>--%>

                                                        <asp:HiddenField ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' runat="server"/>

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
                        <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                            <tr height="30" bgcolor="#ffffff">
                                <td width="100%" align="right" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                        <tr>
                                            <td style="font-size: 8.5pt;">                                               
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpGroupFirst" CssClass="Button" ToolTip="gvEmpGroupFirst" CommandName="gvEmpGroupFirst" OnCommand="GetPagegvEmpGroupIndex"
                                                    Text="<<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpGroupPrevious" CssClass="Button" ToolTip="gvEmpGroupPrevious" CommandName="gvEmpGroupPrevious" OnCommand="GetPagegvEmpGroupIndex"
                                                    Text="<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlgvEmpGroupPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgvEmpGroupPage_SelectedIndexChanged"
                                                                                      >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblgvEmpGroupTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpGroupNext" CssClass="Button" ToolTip="gvEmpGroupNext" runat="server" CommandName="gvEmpGroupNext" Text=">" OnCommand="GetPagegvEmpGroupIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpGroupLast" CssClass="Button" ToolTip="gvEmpGroupLast" runat="server" CommandName="gvEmpGroupLast"
                                                    Text=">>" OnCommand="GetPagegvEmpGroupIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                                        <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSelectEmpGroup" OnClick="btnSelectEmpGroup_Clicked" Text="ตกลง" class="button-pri button-accept m-r-10" runat="server" />
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
        aria-hidden="true" id="modal-emppercent">
        <div class="modal-dialog modal-lg" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle02" class="modal-title sub-title " style="font-size: 16px;">ค้นหาพนักงาน</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
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

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        
                                        <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpPercent" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label"></label>
                                    <div class="col-sm-3">
                                        
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpFNamePercent" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Employee Surname</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpLNamePercent" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                        <div class="text-center m-t-20 center">
                                    <asp:Button ID="btnSearchEmpPercent" OnClick="btnSearchEmpPercent_Clicked" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearchEmpPercent" OnClick="btnClearSearchEmpPercent_Clicked" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>

                                        <hr />

                                        <asp:GridView ID="gvEmpPercent" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             >
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                <asp:CheckBox ID="chkEmpPercentAll" OnCheckedChanged="chkEmpPercentAll_click" AutoPostBack="true" runat="server"  />
                                            </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmpPercent" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Employee Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpPercentCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Employee Name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblEmpPercentName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="btnEdit" runat="Server" CommandName="SelectEmp" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>--%>

                                                        <asp:HiddenField ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' runat="server"/>

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
                        <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                            <tr height="30" bgcolor="#ffffff">
                                <td width="100%" align="right" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                        <tr>
                                            <td style="font-size: 8.5pt;">                                               
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpPercentFirst" CssClass="Button" ToolTip="gvEmpPercentFirst" CommandName="gvEmpPercentFirst" OnCommand="GetPagegvEmpPercentIndex"
                                                    Text="<<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpPercentPrevious" CssClass="Button" ToolTip="gvEmpPercentPrevious" CommandName="gvEmpPercentPrevious" OnCommand="GetPagegvEmpPercentIndex"
                                                    Text="<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlgvEmpPercentPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgvEmpPercentPage_SelectedIndexChanged"
                                                                                      >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblgvEmpPercentTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpPercentNext" CssClass="Button" ToolTip="gvEmpPercentNext" runat="server" CommandName="gvEmpPercentNext" Text=">" OnCommand="GetPagegvEmpPercentIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpPercentLast" CssClass="Button" ToolTip="gvEmpPercentLast" runat="server" CommandName="gvEmpPercentLast"
                                                    Text=">>" OnCommand="GetPagegvEmpPercentIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                                        <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSelectEmpPercent" OnClick="btnSelectEmpPercent_Clicked" Text="ตกลง" class="button-pri button-accept m-r-10" runat="server" />
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
        aria-hidden="true" id="modal-emproundrobin">
        <div class="modal-dialog modal-lg" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
                                <div id="exampleModalLongTitle03" class="modal-title sub-title " style="font-size: 16px;">ค้นหาพนักงาน</div>

                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
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

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        
                                        <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpRoundRobin" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label"></label>
                                    <div class="col-sm-3">
                                        
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Employee Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpFNameRoundRobin" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <label class="col-sm-1 col-form-label"></label>

                                    <label class="col-sm-2 col-form-label">Employee Surname</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearchEmpLNameRoundRobin" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                        <div class="text-center m-t-20 center">
                                    <asp:Button ID="btnSearchEmpRoundRobin_Clicked" Text="ค้นหา" class="button-pri button-accept m-r-10" runat="server" />
                                    <asp:Button ID="btnClearSearchEmpRoundRobin" Text="ล้าง" class="button-pri button-cancel" runat="server" />
                                </div>

                                        <hr />

                                        <asp:GridView ID="gvEmpRoundRobin" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand " TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                             >
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <center>
                                                <asp:CheckBox ID="chkEmpRoundRobinAll" OnCheckedChanged="chkEmpRoundRobinAll_click" AutoPostBack="true" runat="server"  />
                                            </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmpRoundRobin" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="left">Employee Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpRoundRobinCode" Text='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                    <HeaderTemplate>
                                                        <div align="Center">Employee Name</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="hideText">
                                                        <asp:Label ID="lblEmpRoundRobinName_TH" Text='<%# DataBinder.Eval(Container.DataItem, "EmpName_TH")%>' runat="server"/>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail btn-action">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="btnEdit" runat="Server" CommandName="SelectEmp" class="button-activity m-r-5 " CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="icofont icofont-ui-edit f-14"></span></asp:LinkButton>--%>

                                                        <asp:HiddenField ID="hidEmpId" Value='<%# DataBinder.Eval(Container.DataItem, "EmpId")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpCode" Value='<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpFname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpFname_TH")%>' runat="server"/>
                                                        <asp:HiddenField ID="hidEmpLname_TH" Value='<%# DataBinder.Eval(Container.DataItem, "EmpLname_TH")%>' runat="server"/>

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
                        <table width="99%" cellpadding="1" cellspacing="1" bgcolor="#ffffff">
                            <tr height="30" bgcolor="#ffffff">
                                <td width="100%" align="right" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                        <tr>
                                            <td style="font-size: 8.5pt;">                                               
                                            </td>
                                            <td style="width: 12px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpRRFirst" CssClass="Button" ToolTip="gvEmpRRFirst" CommandName="gvEmpRRFirst" OnCommand="GetPagegvEmpRRIndex"
                                                    Text="<<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpRRPrevious" CssClass="Button" ToolTip="gvEmpRRPrevious" CommandName="gvEmpRRPrevious" OnCommand="GetPagegvEmpRRIndex"
                                                    Text="<" runat="server" ></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td style="font-size: 8.5pt;">Page
                                                                                    <asp:DropDownList ID="ddlgvEmpRRPage" CssClass="textbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgvEmpRRPage_SelectedIndexChanged"
                                                                                      >
                                                                                    </asp:DropDownList>
                                                of
                                                                                    <asp:Label ID="lblgvEmpRRTotalPages" CssClass="fontBlack" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpRRPNext" CssClass="Button" ToolTip="gvEmpRRNext" runat="server" CommandName="gvEmpRRNext" Text=">" OnCommand="GetPagegvEmpRRIndex"></asp:Button>
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Button ID="lnkbtngvEmpRRLast" CssClass="Button" ToolTip="gvEmpRRLast" runat="server" CommandName="gvEmpRRLast"
                                                    Text=">>" OnCommand="GetPagegvEmpRRIndex"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                                        <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button ID="btnSelectEmpRoundRobin" OnClick="btnSelectEmpRoundRobin_Clicked" Text="ตกลง" class="button-pri button-accept m-r-10" runat="server" />
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
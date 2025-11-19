<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/src/MasterPage/Web.Master" CodeBehind="ProductSummarizeDashboard.aspx.cs" Inherits="DOMS_TSR.src.Dashboard.ProductSummarizeDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">   
    function PopulateDays() {
        var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
    var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
    var ddlDay = document.getElementById("<%=ddlDay.ClientID%>");
    var y = ddlYear.options[ddlYear.selectedIndex].value;
    var m = ddlMonth.options[ddlMonth.selectedIndex].value != 0;
    if (ddlMonth.options[ddlMonth.selectedIndex].value != 0 && ddlYear.options[ddlYear.selectedIndex].value != 0) {
        var dayCount = 32 - new Date(ddlYear.options[ddlYear.selectedIndex].value, ddlMonth.options[ddlMonth.selectedIndex].value - 1, 32).getDate();
        ddlDay.options.length = 0;
        AddOption(ddlDay, "ALL", "0");
        for (var i = 1; i <= dayCount; i++) {
            AddOption(ddlDay, i, i);
        }
    }
}
 
function AddOption(ddl, text, value) {
    var opt = document.createElement("OPTION");
    opt.text = text;
    opt.value = value;
    ddl.options.add(opt);
}
 
function Validate(sender, args) {
    var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
    var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
    var ddlDay = document.getElementById("<%=ddlDay.ClientID%>");
        args.IsValid = (ddlMonth.selectedIndex != 0 && ddlYear.selectedIndex != 0)
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
                                <div class="sub-title">Product Summary Monthly</div>
                            </div>
                            <div class="card-block">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <h6>Total of Call</h6>
                                            <p style="vertical-align: middle">
                                                <asp:Label ID="lblsumtotalcall" runat="server" Text=""></asp:Label></p>
                                        </div>
                                        <div class="col-sm-2">
                                            <h6>Total Order</h6>
                                            <p>
                                                <asp:Label ID="lblsumtotalorder" runat="server" Text=""></asp:Label></p>
                                        </div>
                                        <div class="col-sm-2">
                                            <h6>Total Amount</h6>
                                            <p>
                                                <asp:Label ID="lblsumtotalamount" runat="server" Text=""></asp:Label></p>
                                        </div>
                                        <div class="col-sm-2">
                                            <h6>% Order</h6>
                                            <p>
                                                <asp:Label ID="lblpercentorder" runat="server"></asp:Label></p>
                                        </div>
                                        <div class="col-sm-2">
                                            <h6>Avg. Call</h6>
                                            <p>
                                                <asp:Label ID="lblavgcall" runat="server" Text=""></asp:Label></p>
                                        </div>
                                        <div class="col-sm-2">
                                            <h6>Avg. Amount Hrs</h6>
                                            <p>
                                                <asp:Label ID="lblavgamounthrs" runat="server"></asp:Label></p>
                                        </div>
                                    </div>
                                </div>



                            </div>

                        </div>


                        <div class="card">
                              <div class="card-header">
                                <div class="sub-title">
                                <asp:Label ID="lblDashboard" runat="server"></asp:Label></div>
                            </div>

                            <div class="card-block">
                                <div class ="container">
                                <div class="row">
                                 
                                    <div class="col-sm-2">
                                        <h6>ViewData</h6>
                                        <asp:DropDownList ID="ddlViewData" class="form-control" runat="server"></asp:DropDownList>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />

                                    </div>
                                    <div class="col-sm-0.5"></div>
                                    <div class="col-sm-2.5">
                                        <h6>Year</h6>
                                        <asp:DropDownList ID="ddlYear" class="form-control" runat="server"  onchange = "PopulateDays()" ></asp:DropDownList>

                                    </div>
                                    <div class="col-sm-1.5">
                                        <h6>Month</h6>
                                        <asp:DropDownList ID="ddlMonth" class="form-control" runat="server"  onchange = "PopulateDays()" ></asp:DropDownList>

                                    </div>

                                    <div class="col-sm-1.5">
                                        <h6>Day</h6>
                                        <asp:DropDownList ID="ddlDay" class="form-control" runat="server"></asp:DropDownList>

                                    </div>
                                    <%--  <asp:CustomValidator ID="Validator" runat="server" ErrorMessage="* Required"
                                    ClientValidationFunction = "Validate" />--%>
                                    <div class="col-sm-1">
                                        <h6>&nbsp;</h6>
                                        <asp:Button ID="btnSearch" Text="Go" OnClick="btnSearch_Click"
                                            class="button-pri button-accept m-r-10"
                                            runat="server" />
                                    </div>
                                    </div>
                                    </div>


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



                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvMonthly" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" OnRowDataBound="gvMonthly_RowDataBound"
                                        TabIndex="0" Width="100%" CellSpacing="0" ShowFooter="True" FooterStyle-HorizontalAlign="Center" 
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                    
                               
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="center">0</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour0" Text='<%# string.Format("{0:0.00}",DataBinder.Eval(Container.DataItem, "Hour0"))%>' runat="server" />

                                                </ItemTemplate>
                                     

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">1</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour1" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour1"))%>' runat="server" />

                                                </ItemTemplate>
                                     
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">2</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour2" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour2"))%>' runat="server" />

                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">3</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour3" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour3"))%>' runat="server" />

                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">4</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour4" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour4"))%>' runat="server" />

                                                </ItemTemplate>
                                          
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">5</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour5" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour5"))%>' runat="server" />

                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">6</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour6" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour6"))%>' runat="server" />

                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">7</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour7" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour7"))%>' runat="server" />

                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">8</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour8" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour8"))%>' runat="server" />

                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">9</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour9" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour9"))%>' runat="server" />

                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">10</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour10" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour10"))%>' runat="server" />

                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">11</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour11" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour11"))%>' runat="server" />

                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">12</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour12" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour12"))%>' runat="server" />

                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">13</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour13" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour13"))%>' runat="server" />

                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">14</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour14" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour14"))%>' runat="server" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">15</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour15" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour15"))%>' runat="server" />

                                                </ItemTemplate>
                                         
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">16</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour16" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour16"))%>' runat="server" />

                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">17</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour17" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour17"))%>' runat="server" />

                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">18</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour18" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour18"))%>' runat="server" />

                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">19</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour19" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour19"))%>' runat="server" />

                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">20</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour20" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour20"))%>' runat="server" />

                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">21</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour21" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour21"))%>' runat="server" />

                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">22</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour22" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour22"))%>' runat="server" />

                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="center">23</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHour23" Text='<%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Hour23"))%>' runat="server" />

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
                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="GvproductAmount" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" OnRowDataBound="GvproductAmount_RowDataBound"
                                        TabIndex="0" Width="100%" CellSpacing="0" ShowFooter="True" FooterStyle-HorizontalAlign="Center" FooterStyle-BackColor="#ADD55C"
                                        ShowHeaderWhenEmpty="true" >

                                        <Columns>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="left">Product Code</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFProductCode" runat="server" Text="GrandTotal"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="LEFT" ItemStyle-HorizontalAlign="LEFT" ItemStyle-Width="40%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div>Product Name</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="LblProductname" Text='<%# DataBinder.Eval(Container.DataItem, "Productname")%>' runat="server" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right"  HeaderText="0" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="right">Quanlity</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuanlity" Text='<%# string.Format("{0:0}",DataBinder.Eval(Container.DataItem, "Quanlity"))%>' runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFQuanlity" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" HeaderText="0" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="right">Amount Item</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                 <%--   <asp:Label ID="lblAmount" Text='<%# string.Format("{0:0.00}",DataBinder.Eval(Container.DataItem, "Amount"))%>' runat="server" />--%>
                                                         <asp:Label ID="lblAmount" Text='<%# string.Format("{0:0.00}",DataBinder.Eval(Container.DataItem, "Amount"))%>' runat="server" />
                                                    <asp:HiddenField ID="HidAmount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                                       <asp:Label ID="LblAmonut1"  runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFAmount" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>



                                        </Columns>

                                        <EmptyDataTemplate  >
                                            <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvPromotionAmount" runat="server" Visible="false" AutoGenerateColumns="False" CssClass="table-p-stand" OnRowDataBound="gvPromotionAmount_RowDataBound"
                                        TabIndex="0" Width="100%" CellSpacing="0" ShowFooter="True" FooterStyle-HorizontalAlign="Center" FooterStyle-BackColor="#ADD55C"
                                        ShowHeaderWhenEmpty="true" >

                                        <Columns>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div align="left">Promotion Code</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionCode")%>' runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFProductCode" runat="server" Text="GrandTotal"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="LEFT" ItemStyle-HorizontalAlign="LEFT" ItemStyle-Width="40%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>

                                                    <div>Promotion Name</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="LblProductname" Text='<%# DataBinder.Eval(Container.DataItem, "PromotionName")%>' runat="server" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right"  HeaderText="0" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="right">Quanlity</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblPromotionQuanlity" Text='<%# string.Format("{0:0}",DataBinder.Eval(Container.DataItem, "Quanlity"))%>' runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFPromotionQuanlity" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" HeaderText="0" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                                <HeaderTemplate>
                                                    <div align="right">Amount Item</div>

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                 <%--   <asp:Label ID="lblAmount" Text='<%# string.Format("{0:0.00}",DataBinder.Eval(Container.DataItem, "Amount"))%>' runat="server" />--%>
                                                         <asp:Label ID="lblPromotionAmount" Text='<%# string.Format("{0:0.00}",DataBinder.Eval(Container.DataItem, "Amount"))%>' runat="server" />
                                                    <asp:HiddenField ID="HidAmount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                                       <asp:Label ID="LblAmonut1"  runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFPromotionAmount" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>



                                        </Columns>

                                        <EmptyDataTemplate  >
                                            <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>



                            </div>
                        </div>
                    </div>
                    <!-- Basic Form Inputs card end -->
                </div>
            </div>
             </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

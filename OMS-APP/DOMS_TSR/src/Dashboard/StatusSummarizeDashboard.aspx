<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="StatusSummarizeDashboard.aspx.cs" Inherits="DOMS_TSR.src.Dashboard.StatusSummarizeDashboard" %>

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
    args.IsValid = (ddlDay.selectedIndex != 0 && ddlMonth.selectedIndex != 0 && ddlYear.selectedIndex != 0)
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="hidEmpCode" runat="server" />

            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Monthly Status Summary -<span class="col-sm-3 col-form-label">
                                        <asp:Label ID="lblDashboard"  runat="server"></asp:Label>
                                    </span></div>
                            </div>
                                   <div class="card-block">
                                <div class ="container">
                                <div class="row">
                                    
                                    <div class="col-md-2">
                                        <h6>ViewData</h6>
                                        <asp:DropDownList ID="ddlViewData" class="form-control" runat="server"></asp:DropDownList>
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                    </div>
                                    <div class="col-sm-0.5"></div>
                                    <div class="col-sm-2.5">
                                        <h6>Year</h6>
                                        <asp:DropDownList ID="ddlYear" class="form-control" runat="server" onchange = "PopulateMonth()"></asp:DropDownList>
                                    </div>
                                    
                                    <div class="col-sm-1.5">
                                        <h6>Month</h6>
                                        <asp:DropDownList ID="ddlMonth" class="form-control" runat="server" onchange = "PopulateDays()" ></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1.5">
                                        <h6>Day</h6>
                                        <asp:DropDownList ID="ddlDay" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        <h6>&nbsp;</h6>
                                        <asp:Button ID="btnSearch" Text="Go" OnClick="btnSearch_Click" class="button-pri button-accept m-r-10" runat="server" />
                                    </div>
                                    </div>
                                    </div>


                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="dt-responsive table-responsive">
                                    <asp:GridView ID="gvMonthly" OnRowDataBound="gvMonthly_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowFooter="True" FooterStyle-HorizontalAlign="Center" FooterStyle-BackColor="#ADD55C" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">Days</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDay" Text='<%# DataBinder.Eval(Container.DataItem, "Day")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTxtDayTotal" runat="server" Text="GrandTotal"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">sales record</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus01" Text='<%# DataBinder.Eval(Container.DataItem, "status01")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum01" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">preparing goods</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus02" Text='<%# DataBinder.Eval(Container.DataItem, "status02")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum02" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">waiting for delivery</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus03" Text='<%# DataBinder.Eval(Container.DataItem, "status03")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum03" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">Shipping is in progress</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus04" Text='<%# DataBinder.Eval(Container.DataItem, "status04")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum04" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">Successful delivery</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus05" Text='<%# DataBinder.Eval(Container.DataItem, "status05")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum05" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">cancel order</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus06" Text='<%# DataBinder.Eval(Container.DataItem, "status06")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum06" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">Change order information</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus07" Text='<%# DataBinder.Eval(Container.DataItem, "status07")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum07" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">Close</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus08" Text='<%# DataBinder.Eval(Container.DataItem, "status08")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum08" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">Export packing lists</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus09" Text='<%# DataBinder.Eval(Container.DataItem, "status09")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum09" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">Neworder</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus10" Text='<%# DataBinder.Eval(Container.DataItem, "status10")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum10" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="0" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">distribute work</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus11" Text='<%# DataBinder.Eval(Container.DataItem, "status11")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSum11" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

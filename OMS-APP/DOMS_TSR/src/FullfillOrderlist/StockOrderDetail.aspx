<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="StockOrderDetail.aspx.cs" Inherits="DOMS_TSR.src.FullfillOrderlist.StockOrderDetail" %>

<%@ Register Src="~/src/UserControl/OrderDetail.ascx" TagPrefix="uc1" TagName="OrderDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/Chart.js-master/css js/Chart.css">
    <link rel="icon" href="favicon.ico">
    <script src="../../Scripts/Chart.js-master/css js/Chart.js"></script>
    <script src="../../Scripts/utils.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
              <asp:HiddenField ID="hidEmpCode" runat="server" />
              <asp:HiddenField ID="hidInventorySelected" runat="server" />
            
            <uc1:OrderDetail runat="server" id="OrderDetail" />
            <div class="card">
        <div class="card-header border-0">
            <div class="sub-title">Change inventory</div>
        </div>

            <div class="card">
                      

                            <div class="card-body">
                                  <div class="form-group row">
                                 
                        <label class="col-sm-2 col-form-label">inventory:</label>
                                    <div class="col-sm-10">
                                      <asp:DropDownList ID="ddlInventory" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlInventory_SelectedChanged" class="form-control"></asp:DropDownList>
                                    </div>
                                      <div class="col-sm-12">
                                           <asp:GridView ID="gvProductInventory" runat="server" AutoGenerateColumns="False" CssClass="table-p-stand"
                            TabIndex="0" Width="100%" CellSpacing="0" class="transport" border="0" OnRowDataBound="gvProductInventory_RowDatabound"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Product code</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Product ์ฟทำ</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">quantity</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Onhand</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY" Text='<%# DataBinder.Eval(Container.DataItem, "QTY")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Reserved</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReserved" Text='<%# DataBinder.Eval(Container.DataItem, "Reserved")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Current</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrent" Text='<%# DataBinder.Eval(Container.DataItem, "Current")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                    <HeaderTemplate>
                                        <center>
                                            <div align="center">Balance</div>
                                        </center>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" Text='<%# DataBinder.Eval(Container.DataItem, "Balance")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty1" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>
                                      </div>
                                </div>

        <div class="text-center m-t-20 col-sm-12">
            <asp:Button ID="Button3" Text="Submit" OnClick="btnsubmit_Click"
                class="button-pri button-accept m-r-10"
                runat="server" />
            <asp:Button ID="Button1" Text="ตี Back Oreder" OnClick="btnsubmit_Click" Visible="false"
                class="button-pri button-accept m-r-10"
                runat="server" />
            <asp:Button ID="Button2" Text="Clear" OnClick="btnClear_Click"
                class="button-pri button-cancel"
                runat="server" />

        </div>
    </div>
                </div>
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>


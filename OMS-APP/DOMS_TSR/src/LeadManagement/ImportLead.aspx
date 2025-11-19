<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="ImportLead.aspx.cs" Inherits="DOMS_TSR.src.LeadManagement.ImportLead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">


  
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>

    <script>
        $(document).ready(function () {
            $("#btnPreviewImport").click(function () {
                $("#modal-ImportMedia").modal();
            });
        });
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

       <input type="hidden" id="hidCodeList" runat="server" />
                                        <input type="hidden" id="hidIdList" runat="server" />
                                        <input type="hidden" id="hidFlagInsert" runat="server" />
                                        <asp:HiddenField ID="hidFlagDel" runat="server" />
                                        <input type="hidden" id="hidaction" runat="server" />
                                        <asp:HiddenField ID="hidMsgDel" runat="server" />
                                        <asp:HiddenField ID="hidEmpCode" runat="server" />

    <asp:HiddenField ID="hd" runat="server" />
    <div class="page-body">
        <div class="row">
            <div class="col-sm-12">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Import Lead </div>
                            </div>
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-4">
                                <asp:FileUpload ID="fiUpload" runat="server" class="form-control"></asp:FileUpload>
                            </div>
                            <div class="col-4">
                                <asp:Button type="button" ID="btnUpload" Text="Upload" class="button-pri button-accept m-r-10" OnClick="btnUpload_Click" runat="server" />
                            </div>
                            <div class="col-4">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="false">
                                    <ContentTemplate> 
                                        <asp:LinkButton ID="btnShowImportFile" OnClick="btnShowImportFile_Click" runat="server" class="button-pri button-accept m-r-10">Show</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            

                           
                        </div>
                    </div>
                </div>


                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-12" runat="server" visible="false" id="DivSubmitUpload" style="width: 100%; height: 400px; overflow: scroll" >
                                <asp:GridView ID="gvMediaPlanImport" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">LINE_NO</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLINE_NO" Text='<%# DataBinder.Eval(Container.DataItem, "LINE_NO")%>' runat="server" />                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">REF_CODE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblREF_CODE" Text='<%# DataBinder.Eval(Container.DataItem, "REF_CODE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">LOT_NAME</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLOT_NAME" Text='<%# DataBinder.Eval(Container.DataItem, "LOT_NAME")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">CHANNEL_FROM</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCHANNEL_FROM" Text='<%# DataBinder.Eval(Container.DataItem, "CHANNEL_FROM")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">CHANNEL_TO</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCHANNEL_TO" Text='<%# DataBinder.Eval(Container.DataItem, "CHANNEL_TO")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MERCHANT_CODE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMERCHANT_CODE" Text='<%# DataBinder.Eval(Container.DataItem, "MERCHANT_CODE")%>' runat="server" />                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">BRAND_NO</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBRAND_NO" Text='<%# DataBinder.Eval(Container.DataItem, "BRAND_NO")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MEDIA_PHONE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMEDIA_PHONE" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PREFIX_TH</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPREFIX_TH" Text='<%# DataBinder.Eval(Container.DataItem, "PREFIX_TH")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">FIRSTNAME_TH</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFIRSTNAME_TH" Text='<%# DataBinder.Eval(Container.DataItem, "FIRSTNAME_TH")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">LASTNAME_TH</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLASTNAME_TH" Text='<%# DataBinder.Eval(Container.DataItem, "LASTNAME_TH")%>' runat="server" />                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">FULL_NAME_TH</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFULL_NAME_TH" Text='<%# DataBinder.Eval(Container.DataItem, "FULL_NAME_TH")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MOBILE_1</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMOBILE_1" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_1")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MOBILE_2</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMOBILE_2" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_2")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MOBILE_3</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMOBILE_3" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_3")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                   
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MOBILE_4</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMOBILE_4" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_4")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MOBILE_5</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMOBILE_5" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_5")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">MOBILE_6</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMOBILE_6" Text='<%# DataBinder.Eval(Container.DataItem, "MOBILE_6")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PHONE_1</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPHONE_1" Text='<%# DataBinder.Eval(Container.DataItem, "PHONE_1")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                                      <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PHONE_2</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPHONE_2" Text='<%# DataBinder.Eval(Container.DataItem, "PHONE_2")%>' runat="server" />                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PHONE_3</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPHONE_3" Text='<%# DataBinder.Eval(Container.DataItem, "PHONE_3")%>' runat="server" />                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_NO</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_NO" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_NO")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PLACE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPLACE" Text='<%# DataBinder.Eval(Container.DataItem, "PLACE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_SUBDISTRICT</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_SUBDISTRICT" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_SUBDISTRICT")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_SUBDISTRICT_ID</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_SUBDISTRICT_ID" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_SUBDISTRICT_ID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_DISTRICT</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_DISTRICT" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_DISTRICT")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                      <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_DISTRICT_ID</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_DISTRICT_ID" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_DISTRICT_ID")%>' runat="server" />                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_PROVINCE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_PROVINCE" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_PROVINCE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_PROVINCE_ID</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_PROVINCE_ID" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_PROVINCE_ID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ADDR_ZIPCODE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDR_ZIPCODE" Text='<%# DataBinder.Eval(Container.DataItem, "ADDR_ZIPCODE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PREVIOUS_SALE_NAME</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPREVIOUS_SALE_NAME" Text='<%# DataBinder.Eval(Container.DataItem, "PREVIOUS_SALE_NAME")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                               <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PREVIOUS_ORDER_DATE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPREVIOUS_ORDER_DATE" Text='<%# DataBinder.Eval(Container.DataItem, "PREVIOUS_ORDER_DATE")%>' runat="server" />                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PREVIOUS_ORDER_BRAND</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPREVIOUS_ORDER_BRAND" Text='<%# DataBinder.Eval(Container.DataItem, "PREVIOUS_ORDER_BRAND")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">PREVIOUS_PRODUCT</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPREVIOUS_PRODUCT" Text='<%# DataBinder.Eval(Container.DataItem, "PREVIOUS_PRODUCT")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">CAMPAIGN_ID</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCAMPAIGN_ID" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_ID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button type="button" ID="btnSubmitImport" Text="submit" class="button-pri button-accept m-r-10" runat="server" OnClick="btnSubmitImport_Click" />
                                    <asp:Button type="button" ID="Button1" Text="Cancel" class="button-pri button-accept m-r-10" runat="server" OnClick="btnCancel_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    


    
</asp:Content>

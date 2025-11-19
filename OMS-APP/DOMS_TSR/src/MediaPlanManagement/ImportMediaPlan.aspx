<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="ImportMediaPlan.aspx.cs" Inherits="DOMS_TSR.src.MediaPlanManagement.ImportMediaPlan" %>

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
                                        <asp:HiddenField ID="hidMerCode" runat="server" />

    <asp:HiddenField ID="hd" runat="server" />
    <div class="page-body">
        <div class="row">
            <div class="col-sm-12">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-header">
                                <div class="sub-title">Import Media Plan</div>
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
                            <div class="col-12" runat="server" visible="false" id="DivSubmitUpload">
                                <div class="table-responsive">
                                <asp:GridView ID="gvMediaPlanImport" runat="server" AutoGenerateColumns="false" CssClass="table-p-stand" TabIndex="0" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ลำดับ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLINE_NO" Text='<%# DataBinder.Eval(Container.DataItem, "LINE_NO")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="HidMEDIA_DATE" Value='<%# DataBinder.Eval(Container.DataItem, "MEDIA_DATE")%>' />
                                                <asp:HiddenField runat="server" ID="HidMEDIA_ENDDATE" Value='<%# DataBinder.Eval(Container.DataItem, "MEDIA_ENDDATE")%>' />
                                                <asp:HiddenField runat="server" ID="HidTIME_START" Value='<%# DataBinder.Eval(Container.DataItem, "TIME_START")%>' />
                                                <asp:HiddenField runat="server" ID="HidTIME_END" Value='<%# DataBinder.Eval(Container.DataItem, "TIME_END")%>' />
                                                <asp:HiddenField runat="server" ID="HidDuration" Value='<%# DataBinder.Eval(Container.DataItem, "Duration")%>' />
                                                <asp:HiddenField runat="server" ID="HidMEDIA_PHONE" Value='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' />
                                                <asp:HiddenField runat="server" ID="HidSALE_CHANNEL" Value='<%# DataBinder.Eval(Container.DataItem, "SALE_CHANNEL")%>' />
                                                  <asp:HiddenField runat="server" ID="HidMEDIA_CHANNEL" Value='<%# DataBinder.Eval(Container.DataItem, "MEDIA_CHANNEL")%>' />
                                                <asp:HiddenField runat="server" ID="HidPROGRAM_NAME" Value='<%# DataBinder.Eval(Container.DataItem, "PROGRAM_NAME")%>' />
                                                <asp:HiddenField runat="server" ID="HidCAMPAIGN_CODE" Value='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_CODE")%>' />
                                                 <asp:HiddenField runat="server" ID="HidMERCHANT_NAME" Value='<%# DataBinder.Eval(Container.DataItem, "MERCHANT_NAME")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">วันที่เริ่ม</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMEDIA_DATE" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_DATE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">วันที่สิ้นสุด</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMEDIA_ENDDATE" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_ENDDATE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">เวลาที่เริ่ม</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTIME_START" Text='<%# DataBinder.Eval(Container.DataItem, "TIME_START")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">เวลาที่สิ้นสุด</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTIME_END" Text='<%# DataBinder.Eval(Container.DataItem, "TIME_END")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ระยะเวลา</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDuration" Text='<%# DataBinder.Eval(Container.DataItem, "Duration")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">เบอร์โทรศัพท์</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMEDIA_PHONE" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_PHONE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ช่องออกอากาศ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCHANNEL_TYPE" Text='<%# DataBinder.Eval(Container.DataItem, "SALE_CHANNEL")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left"่>ช่องทางการขาย</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCHANNEL" Text='<%# DataBinder.Eval(Container.DataItem, "MEDIA_CHANNEL")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">ชื่อโปรแกรม</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPROGRAM_NAME" Text='<%# DataBinder.Eval(Container.DataItem, "PROGRAM_NAME")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                            <HeaderTemplate>
                                                <div align="left">รหัสแคมเปญ</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCAMPAIGN_CODE" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPAIGN_CODE")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">
                                        <HeaderTemplate>
                                                <div align="left">ชื่อร้านค้า</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMERCHANT_NAME" Text='<%# DataBinder.Eval(Container.DataItem, "MERCHANT_NAME")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                <div class="text-center m-t-20 col-sm-12">
                                    <asp:Button type="button" ID="btnSubmitImport" Text="บันทึก" class="button-pri button-accept m-r-10" runat="server" OnClick="btnSubmitImport_Click" />
                                    <asp:Button type="button" ID="Button1" Text="ล้าง" class="button-pri button-accept m-r-10" runat="server" OnClick="btnCancel_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    


    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="OrderStatusChart.aspx.cs" Inherits="DOMS_TSR.src.FullfillOrderlist.OrderStatusChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
        <link rel="stylesheet" type="text/css" href="../../Scripts/Chart.js-master/css js/Chart.css">
    <link rel="icon" href="favicon.ico">
    <script src="../../Scripts/Chart.js-master/css js/Chart.js"></script>
    <script src="../../Scripts/utils.js"></script>

   <style type="text/css">
	.centerDiv
	{
	
		margin: 0 auto;
		
	}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hd" runat="server" />
            <div class="page-body">

                <div class="row">
                    <div class="col-sm-12">
                        <!-- Basic Form Inputs card start -->
                        <div class="card" >
                            <div class="card-header">
                                <div class="sub-title">ค้นหาข้อมูล</div>
                            </div>
                            <div class="card-block">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">เลือกวันที่</label>
                                    <div class="col-sm-3">
                                         <div class="input-group mb-0">
                                                
                                                <asp:TextBox ID="txtSearchStartDateFrom" class="form-control" placeholder="เลือกวันที่" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="txtSearchStartDateFrom" PopupButtonID="Image1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>


                                            </div>
                                        

                                    </div>
                                    <label class="col-sm-1 col-form-label"></label>
                                   
                                  
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

         <div class="centerDiv" >

       <ajaxToolkit:PieChart ID="PieChart1" runat="server" Visible="false" BorderStyle="NotSet"   class="centerDiv"
     
        ></ajaxToolkit:PieChart>

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
    

</asp:Content>


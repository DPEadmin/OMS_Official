<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.Master" AutoEventWireup="true" CodeBehind="SentOrderToBranch.aspx.cs" Inherits="DOMS_TSR.src.OrderManagement.SentOrderToBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
  

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header border-0">
                                    <div class="sub-title">ค้นหาข้อมูลการสั่งซื้อ</div>
                                </div>
                                <div class="card-block">
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">รหัสใบสั่งขาย</label>
                                        <div class="col-sm-3">
                                            <input type="text" class="form-control">
                                        </div>
                                        <label class="col-sm-1 col-form-label"></label>
                                        <label class="col-sm-2 col-form-label">วันที่สั่งซื้อ</label>
                                        <div class="col-sm-3">
                                                <input type="text" class="form-control">
                                            </div>
                                            <label class="col-sm-2 col-form-label">รหัสลูกค้า</label>
                                        <div class="col-sm-3">
                                            <input type="text" class="form-control">
                                        </div>
                                        <label class="col-sm-1 col-form-label"></label>
                                        <label class="col-sm-2 col-form-label">ชื่อ-สกุล</label>
                                        <div class="col-sm-3">
                                                <input type="text" class="form-control">
                                            </div>
                                            <label class="col-sm-2 col-form-label">เบอร์ติดต่อ</label>
                                        <div class="col-sm-3">
                                            <input type="text" class="form-control">
                                        </div>
                                        <label class="col-sm-1 col-form-label"></label>
                                        <label class="col-sm-2 col-form-label">สถานะการสั่งซื้อ</label>
                                        <div class="col-sm-3">
                                                <input type="text" class="form-control">
                                            </div>
                                    </div>
                                    <div class="text-center m-t-20 col-sm-12">
                                        <button class="button-pri button-accept m-r-10">ค้นหา</button>
                                        <button class="button-pri button-cancel ">ล้าง</button>
                                    </div>
                                </div>
                            </div>
                                <div class="card">
                                <div class="card-block">
                                    <table class="table-p-stand w-100" >
                                        <thead>
                                        <tr>
                                        <th style="text-align:center">รหัสใบสั่งขาย</th>
                                        <th style="text-align:center">รหัสลูกค้า</th>
                                        <th style="text-align:center">ชื่อ-สกุล</th>
                                        <th style="text-align:center">เบอร์ติดต่อ</th>
                                        <th style="text-align:center">วันที่สั่งซื้อ</th>
                                        <th style="text-align:center">หมายเหตุ</th>
                                        <th></th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr>
                                        <td style="text-align:left">SO2019020101</td>
                                        <td style="text-align:left">3926190</td>
                                        <td style="text-align:left">อรพรรณ จันทร์เพ็ญ</td>
                                        <td style="text-align:left">0855155210</td>
                                        <td style="text-align:left">01-02-2019  13:00:00</td>
                                        <td style="text-align:left">เลือกสาขาใหม่</td>
                                        <td><button class="button-activity m-r-5 " style="float: none; border-radius: 5px;     padding: 0px 5px;    "><span class="icofont icofont-ui-edit f-14"></span></button></td>
                                    </tr>
                                        <tr>
                                                <td style="text-align:left">SO2019020102</td>
                                                <td style="text-align:left">CM3649494</td>
                                                <td style="text-align:left">ปุณยนุช เอกณรงค์พาณิชย์</td>
                                                <td style="text-align:left">0944445698</td>
                                                <td style="text-align:left">01-02-2019  14:30:00</td>
                                                <td style="text-align:left"></td>
                                                <td><button class="button-activity m-r-5 " style="float: none; border-radius: 5px;     padding: 0px 5px;    "><span class="icofont icofont-ui-edit f-14"></span></button>
                                                <button class="button-activity m-r-5 " style="float: none; border-radius: 5px; border-color: green;    padding: 0px 5px;   background-color:green; ">Send</button></td>
                                                </tr>
                                                <tr>
                                                        <td style="text-align:left">SO2019020103</td>
                                                        <td style="text-align:left">CM3348403</td>
                                                        <td style="text-align:left">สุกัญญา โกแสนตอ</td>
                                                        <td style="text-align:left">0635564215</td>
                                                        <td style="text-align:left">09-02-2019  09:45:00</td>
                                                        <td style="text-align:left"></td>
                                                        <td><button class="button-activity m-r-5 " style="float: none; border-radius: 5px;     padding: 0px 5px;    "><span class="icofont icofont-ui-edit f-14"></span></button>
                                                        <button class="button-activity m-r-5 " style="float: none; border-radius: 5px; border-color: green;    padding: 0px 5px;   background-color:green; ">Send</button></td>
                                                        </tr>
                                                        <tr>
                                                                <td style="text-align:left">SO2019020104</td>
                                                                <td style="text-align:left">CM2360573</td>
                                                                <td style="text-align:left">อรรฆพร กล่ำสนอง  </td>
                                                                <td style="text-align:left">0945514726</td>
                                                                <td style="text-align:left">16-03-2019  16:30:00</td>
                                                                <td style="text-align:left">เลือกสาขาใหม่</td>
                                                                <td><button class="button-activity m-r-5 " style="float: none; border-radius: 5px;     padding: 0px 5px;    "><span class="icofont icofont-ui-edit f-14"></span></button></td>
                                                            </tr>
                                                            <tr>
                                                                    <td style="text-align:left">SO2019020105</td>
                                                                    <td style="text-align:left">CM6635452</td>
                                                                    <td style="text-align:left">ธนาวุฒิ นิจจะรักษ์ </td>
                                                                    <td style="text-align:left">0863698655</td>
                                                                    <td style="text-align:left">17-03-2019  10:00:00</td>
                                                                    <td style="text-align:left">เลือกสาขาใหม่</td>
                                                                    <td><button class="button-activity m-r-5 " style="float: none; border-radius: 5px;     padding: 0px 5px;    "><span class="icofont icofont-ui-edit f-14"></span></button></td>
                                                                </tr>
                                                                <tr>
                                                                        <td style="text-align:left">SO2019020106</td>
                                                                        <td style="text-align:left">CM5564759</td>
                                                                        <td style="text-align:left">สิทธิกร เจริญทรัพย์ </td>
                                                                        <td style="text-align:left">0854651199</td>
                                                                        <td style="text-align:left">18-03-2019  11:30:00</td>
                                                                        <td style="text-align:left">เลือกสาขาใหม่</td>
                                                                        <td><button class="button-activity m-r-5 " style="float: none; border-radius: 5px;     padding: 0px 5px;    "><span class="icofont icofont-ui-edit f-14"></span></button></td>
                                                                    </tr>

                                    </tbody>
                                    </table>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
                
    </ContentTemplate>
    </asp:UpdatePanel>

        

</asp:Content>
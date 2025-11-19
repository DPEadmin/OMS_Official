<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="SupplierDetail.aspx.cs" Inherits="DOMS_TSR.src.Purchasing.SupplierDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="head" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
        <input type="hidden" id="hidFlagInsert" runat="server" />
        <asp:HiddenField ID="hidFlagDel" runat="server" />
        <input type="hidden" id="hidaction" runat="server" />
        <asp:HiddenField ID="hidMsgDel" runat="server" />
        <asp:HiddenField ID="hidEmpId" runat="server" />
        <asp:HiddenField ID="hidSupplierId" runat="server" />
        <asp:HiddenField ID="hidEmpCode" runat="server" />
        <input type="hidden" id="hidIdList" runat="server" />
<div class="page-body">
  <div class="col-sm-12">
      <!-- Basic Form Inputs card start -->
      <div class="card">
          <div class="card-header">
            <div class="sub-title" >รายละเอียดข้อมูลผู้ผลิต (Supplier Detail)</div>
            </div>
                <div class="card-block">
                    <div class="col-sm-12">
                              <div class="view-info" >
                                  <div class="row">
                                      <asp:Literal ID="litLinkBack" runat="server"></asp:Literal>
                                      <div class="col-lg-12">
                                          <div class="general-info">
                                              <div class="row">                
                                                  <div class="col-lg-12 col-xl-6 col-sm-12">                                                   
                                                      <table class="table m-0">
                                                          <tbody>
                                                              <tr>
                                                                  <th scope="row">รหัสผู้ผลิต</th>
                                                                  <td><asp:Label runat="server" ID="lblSupplierCode"></asp:Label></td>
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">เลขประจำตัวผู้เสียภาษี</th>
                                                                  <td><asp:Label runat="server" ID="lblIdNo"></asp:Label>
                                                                  </td>                
                                                              </tr>   
                                                              <tr>
                                                                  <th scope="row">จังหวัด</th>
                                                                  <td><asp:Label runat="server" ID="lblProvinceName"></asp:Label>
                                                                  </td>                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">แขวง/ตำบล</th>
                                                                  <td><asp:Label runat="server" ID="lblSubdistrictName"></asp:Label>
                                                                  </td>                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">เบอร์โทรศัพท์</th>
                                                                  <td><asp:Label runat="server" ID="lblPhoneNumber"></asp:Label>
                                                                  </td>                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">อีเมล์</th>
                                                                  <td><asp:Label runat="server" ID="lblEmail"></asp:Label>
                                                                  </td>                
                                                              </tr>
                                                          </tbody>
                                                      </table>
                                                  </div>
                                                  <div class="col-lg-12 col-xl-6 col-sm-12">
                
                                                      <table class="table m-0">
                                                          <tbody>
                                                              <tr>
                                                                  <th scope="row">ชื่อผู้ผลิต</th>
                                                                  <td><asp:Label runat="server" ID="lblSupplierName"></asp:Label></td>
                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">ที่อยู่</th>
                                                                  <td>
                                                                      <asp:Label runat="server" ID="lblAddress"></asp:Label>
                                                                  </td>                
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">เขต/อำเภอ</th>
                                                                  <td><asp:Label runat="server" ID="lblDistrictName"></asp:Label>
                                                                  </td>
                                                              </tr>                                                             
                                                              <tr>
                                                                  <th scope="row">รหัสไปรษณีย์</th>
                                                                  <td><asp:Label runat="server" ID="lblZipNo"></asp:Label>
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">แฟกซ์</th>
                                                                  <td><asp:Label runat="server" ID="lblFaxNumber"></asp:Label>
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <th scope="row">สถานะ</th>
                                                                  <td><asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                                  </td>
                                                              </tr>
                                                          </tbody>
                                                      </table>           
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
       </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
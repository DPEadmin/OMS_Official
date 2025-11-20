<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Default.aspx.cs" Inherits="DOMS_TSR._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div id="app">
    <section class="section">
      <div class="container mt-5">
        <div class="row">
          <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-6 offset-xl-3" style="padding: 30px;">
            <div class="login-brand">
              <img  style="height: 80px; " src="./assets/img/logo/OMS logo@3x/wdw.png" alt="logo.png">  
              <div style="
              WHITE-SPACE: nowrap;
              margin-top: 1rem;
               font-size: 16px;
          "><b>ORDER MANAGEMENT SYSTEM</b>
          </div>
          <div style="
          WHITE-SPACE: nowrap;
          margin-top: 1rem;
           font-size: 16px;
      "> 
      <b>ENTERPRISE</b></div>
        </div>

            <div class="card card-primary">
              <div class="card-header justify-content-center" style="margin-top: 1rem;"><h3>Log in</h3></div>
              <div class="card-body">
                <form method="POST" action="#" class="needs-validation" novalidate="">
                <div class="form-group" >
                    <div class="d-block">
                    	<label for="Merchant" class="control-label">Merchant</label>
                    
                    </div>
                      <asp:DropDownList ID="ddlMerchant" runat="server" class="form-control"></asp:DropDownList>
                      <asp:TextBox  id="merchantErrorMsg" runat="server" style="color: red; display: none; border: none; width: 100%;">ไม่สามารถเข้าใช้งานระบบได้ กรุณา เลือก Merchant</asp:TextBox>
                   
                  </div>
                  <div class="form-group">
                    <label for="email">Username</label>
                   <asp:TextBox id="txtUserName" class="form-control"  runat="server"></asp:TextBox>         
                       <asp:TextBox  id="usernameErrorMsg" runat="server" style="color: red; display: none; border: none; width: 100%;">ไม่สามารถเข้าใช้งานระบบได้ กรุณา ระบุ USERNAME</asp:TextBox>

                    
                  </div>

                  <div class="form-group" >
                    <div class="d-block">
                    	<label for="password" class="control-label">Password</label>
                    
                    </div>
                       <asp:TextBox TextMode="Password" id="txtPassword" class="form-control"  runat="server"></asp:TextBox> 
                       <asp:TextBox id="passwordErrorMsg" runat="server" style="color: red; display: none;     border: none; width: 100%;">ไม่สามารถเข้าใช้งานระบบได้ กรุณา ระบุ PASSWORD</asp:TextBox>
                     
                  </div>

                  <div class="form-group">
                    <div class="custom-control custom-checkbox">
                      <input type="checkbox" name="remember" class="custom-control-input" tabindex="3" id="remember-me">
                    </div>
                  </div>

                  <div class="form-group">
                    <asp:button ID="btnSubmit" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round"  Text="Sign in"
                                      style="background: #0D4B91; width: 150px; float: right; margin-top: 1rem; font-size: 1rem;
         color:#fff;"    onclick="btnSubmit_Click"> </asp:button>

                          <asp:button ID="Button2" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round"  Text="Userlogin"
                                      style="background: #0D4B91; width: 150px; float: right; margin-top: 1rem; font-size: 1rem;
         color:#fff;"    onclick="btnSubmitMer_Click"> </asp:button>
                            <div id="LoginMsg" ></div>
                      </form>
                <!-- background: -moz-linear-gradient(top, #990305 0%, #ec1c24 100%);
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#990305), color-stop(100%,#ec1c24));
                background: -webkit-linear-gradient(top, #990305 0%,#ec1c24 100%);
                background: -o-linear-gradient(top, #990305 0%,#ec1c24 100%);
                background: -ms-linear-gradient(top, #990305 0%,#ec1c24 100%);
                background: linear-gradient(to bottom, #990305 0%,#ec1c24 100%);
              -->

              </div>
            </div>
            
          </div>
        </div>
      </div>
    </section>
  </div>
 <script>
     document.addEventListener('DOMContentLoaded', function () {
         function hideErrorMessage(elementId) {
             var element = document.getElementById(elementId);
             if (element) {
                 element.style.display = 'none';
             }
         }

         function showErrorMessage(elementId) {
             var element = document.getElementById(elementId);
             if (element) {
                 element.style.display = 'block';
             }
         }

      var ddlMerchant = document.getElementById('<%= ddlMerchant.ClientID %>');
      var txtUserName = document.getElementById('<%= txtUserName.ClientID %>');
      var txtPassword = document.getElementById('<%= txtPassword.ClientID %>');
      var btnSubmit = document.getElementById('<%= btnSubmit.ClientID %>');

      if (ddlMerchant) {
          ddlMerchant.addEventListener('change', function () {
              hideErrorMessage('<%= merchantErrorMsg.ClientID %>');
      });
    }

    if (txtUserName) {
      txtUserName.addEventListener('input', function () {
        hideErrorMessage('<%= usernameErrorMsg.ClientID %>');
      });
    }

    if (txtPassword) {
      txtPassword.addEventListener('input', function () {
        hideErrorMessage('<%= passwordErrorMsg.ClientID %>');
      });
    }

    if (btnSubmit) {
      btnSubmit.addEventListener('click', function (event) {
        let hasError = false;

        if (ddlMerchant && ddlMerchant.value === '-99') {
          showErrorMessage('<%= merchantErrorMsg.ClientID %>');
          hasError = true;
        }

        if (txtUserName && txtUserName.value.trim() === '') {
          showErrorMessage('<%= usernameErrorMsg.ClientID %>');
          hasError = true;
        }

        if (txtPassword && txtPassword.value.trim() === '') {
          showErrorMessage('<%= passwordErrorMsg.ClientID %>');
              hasError = true;
          }

          if (hasError) {
              event.preventDefault();
          }
      });
      }
  });
</script>



</asp:Content>

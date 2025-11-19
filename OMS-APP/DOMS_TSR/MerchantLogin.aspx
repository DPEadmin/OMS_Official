<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MerchantLogin.aspx.cs" Inherits="DOMS_TSR.MerchantLogin" %>

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
      <b> USER LOGIN</b></div>
        </div>

            <div class="card card-primary">
              <div class="card-header justify-content-center" style="margin-top: 1rem;"><h3>Log in</h3></div>
              <div class="card-body">
                <form method="POST" action="#" class="needs-validation" novalidate="">
                <div class="form-group" >
                    
                  </div>
                  <div class="form-group">
                    <label for="email">Username</label>
                   <asp:TextBox id="txtUserName" class="form-control"  runat="server"></asp:TextBox>         
                    <div class="invalid-feedback">
                      Please fill in your email
                    </div>
                  </div>

                  <div class="form-group" >
                    <div class="d-block">
                    	<label for="password" class="control-label">Password</label>
                    
                    </div>
                       <asp:TextBox TextMode="Password" id="txtPassword" class="form-control"  runat="server"></asp:TextBox> 
                    <div class="invalid-feedback">
                      please fill in your password
                    </div>
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
                            <div id="LoginMsg" ></div>
                      </form>

              </div>
            </div>
            
          </div>
        </div>
      </div>
    </section>
  </div>



</asp:Content>

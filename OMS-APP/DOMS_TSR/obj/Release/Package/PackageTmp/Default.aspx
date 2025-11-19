<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DOMS_TSR._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div id="app">
    <section class="section">
      <div class="container mt-5">
        <div class="row">
          <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">
            <div class="login-brand">
                  <img  style="height: 80px; " src="./Image/auth/logologin.png" alt="logo.png"> 
            </div>

            <div class="card card-primary">
              <div class="card-header"><h4>Login</h4></div>
              <div class="card-body">
                <form method="POST" action="#" class="needs-validation" novalidate="">
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
                                style="background: deepskyblue;
   color:#fff;"    onclick="btnSubmit_Click"> </asp:button>
                      <div id="LoginMsg" ></div>
                  </div>
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



</asp:Content>

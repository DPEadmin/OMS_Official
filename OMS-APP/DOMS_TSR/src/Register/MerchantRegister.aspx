<%@ Page Title="Merchant Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MerchantRegister.aspx.cs" Inherits="DOMS_TSR.src.Register.MerchantRegister" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="app">
        <section class="section">
            <div class="container mt-5" id="progressBar">
                <!-- Progress Bar -->
            </div>
            <asp:HiddenField ID="storeImageData" runat="server" />
            <!--Seller Information-->
            <div class="container" id="createStep1" runat="server" visible="true">
                <div class="card card-primary">
                    <div class="card-header justify-content-center" style="margin-top: 1rem;">
                        <h3>Seller Information</h3>
                    </div>
                    <div class="card-body">
                        <form method="POST" action="#" class="needs-validation" novalidate="">

                            <div class="form-group">
                                <label for="firstName">First Name</label>
                                <asp:TextBox ID="txtFirstName" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblFirstName" runat="server" style="color:#dc3545"></asp:Label>
                            </div>

                            <div class="form-group">
                                <div class="d-block">
                                    <label for="lastName" class="control-label">Last Name</label>
                                </div>
                                <asp:TextBox ID="txtLastName" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblLastName" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="citizenId" class="control-label">Citizen ID</label>
                                </div>
                                <asp:TextBox ID="txtCitizenId" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblCitizenId" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="email" class="control-label">Email</label>
                                </div>
                                <asp:TextBox ID="txtEMail" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEMail" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>

                            <div class="form-group">
                                <div class="d-block">
                                    <label for="mobilePhone" class="control-label">Mobile Phone</label>
                                </div>
                                <asp:TextBox ID="txtMobilePhone" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblMobilePhone" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="userName" class="control-label">Username</label>
                                </div>
                                <asp:TextBox ID="txtUserName" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblUserName" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="password" class="control-label">Password</label>
                                </div>
                                <asp:TextBox ID="txtPassword" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblPassword" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="confirmPass" class="control-label">Confirm Password</label>
                                </div>
                                <asp:TextBox ID="txtConfirmPass" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblConfirmPass" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnNextToStore" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round" Text="Next"
                                    Style="background: #0D4B91; width: 100%; margin-top: 1rem; font-size: 1rem; color: #fff;"
                                    OnClick="btnNextToStore_Click"></asp:Button>
                                <div id="LoginMsg"></div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <!-- Store Information -->
            <div class="container" id="createStep2" runat="server" visible="false">
                <div class="card card-primary">
                    <div class="card-header justify-content-center" style="margin-top: 1rem;">
                        <h3>Store Information</h3>
                    </div>
                    <div class="card-body">
                        <form method="POST" action="#" class="needs-validation" novalidate="">

                            <div class="form-group">
                                <label for="businessCat">Select Business Category</label>
                                <asp:RadioButton ID="catCompany" GroupName="businessGroup" Text="Company" Value="company" runat="server" />
                                <asp:RadioButton ID="catIndy" GroupName="businessGroup" Text="Individual" Value="individual" runat="server" />
                                <asp:Label ID="lblBusinessCat" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>

                            <div class="form-group">
                                <div class="d-block">
                                    <label for="sellerName" class="control-label">Seller Name</label>
                                </div>
                                <asp:TextBox ID="txtSellerName" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblSellerName" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="storeTaxCode" class="control-label">Tax Code</label>
                                </div>
                                <asp:TextBox ID="txtStoreTaxCode" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblStoreTaxCode" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="storeAddress" class="control-label">Address</label>
                                </div>
                                <textarea id="txtStoreAddress" class="form-control" runat="server"></textarea>
                                <asp:Label ID="lblStoreAddress" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>

                            <div class="form-group">
                                <div class="d-block">
                                    <label for="storePhone" class="control-label">Mobile Phone</label>
                                </div>
                                <asp:TextBox ID="txtStorePhone" class="form-control" runat="server"></asp:TextBox>
                                <asp:CheckBox ID="storePhoneCheckBox" AutoPostBack="true" Text="Same number as seller information" runat="server" OnCheckedChanged="storePhoneCheckBox_CheckedChanged"></asp:CheckBox>
                                <asp:Label ID="lblStorePhone" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="storeImage" class="control-label">Seller Image</label>
                                </div>
                                <asp:FileUpload ID="storeImageUpload" runat="server"  />
                                <asp:Label ID="lblStoreImage" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>


                            <div class="form-group row">
                                <asp:Button ID="btnBackToSeller" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round" Text="Back"
                                    Style="background: #0D4B91; width: 15%; margin-top: 1rem; font-size: 1rem; margin-right: 1rem; color: #fff;"
                                    OnClick="btnBackToSeller_Click"></asp:Button>
                                <asp:Button ID="btnNextToBank" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round" Text="Next"
                                    Style="background: #0D4B91; width: 15%; margin-top: 1rem; font-size: 1rem; color: #fff;"
                                    OnClick="btnNextToBank_Click"></asp:Button>

                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <!-- Bank Account Information -->
            <div class="container" id="createStep3" runat="server" visible="false">
                <div class="card card-primary">
                    <div class="card-header justify-content-center" style="margin-top: 1rem;">
                        <h3>Bank Account Information</h3>
                    </div>
                    <div class="card-body">
                        <form method="POST" action="#" class="needs-validation" novalidate="">

                            <div class="form-group">
                                <label for="bankType">Select Bank Account Type</label>
                                <asp:RadioButton ID="savingAcc" GroupName="bankTypeGroup" Text="Saving Account" Value="saving" runat="server" />
                                <asp:RadioButton ID="currentAcc" GroupName="bankTypeGroup" Text="Current Account" Value="current" runat="server" />
                                <asp:Label ID="lblBankType" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>

                            <div class="form-group">
                                <div class="d-block">
                                    <label for="bankName" class="control-label">Bank Name</label>
                                </div>
                                <asp:TextBox ID="txtBankName" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblBankName" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="bankAccNum" class="control-label">Bank Account Number</label>
                                </div>
                                <asp:TextBox ID="txtBankAccNum" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblBankAccNum" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="d-block">
                                    <label for="bankAccName" class="control-label">Account Name</label>
                                </div>
                                <asp:TextBox ID="txtAccName" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblAccName" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>

                            <div class="form-group">
                                <div class="d-block">
                                    <label for="bankAccBranch" class="control-label">Branch</label>
                                </div>
                                <asp:TextBox ID="txtBankAccBranch" class="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblBankAccBranch" runat="server" Style="color: #dc3545"></asp:Label>
                            </div>
                            <img id="myImage" runat="server" src=""/>

                            <div class="form-group row">
                                <asp:Button ID="btnBackToStore" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round" Text="Back"
                                    Style="background: #0D4B91; width: 15%; margin-top: 1rem; font-size: 1rem; margin-right: 1rem; color: #fff;"
                                    OnClick="btnBackToStore_Click"></asp:Button>
                                <asp:Button ID="btnNextToSubmit" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round" Text="Next"
                                    Style="background: #0D4B91; width: 15%; margin-top: 1rem; font-size: 1rem; color: #fff;"
                                    OnClick="btnNextToSubmit_Click"></asp:Button>

                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <!-- Submit Document -->
            <div class="container" id="createStepSubmit" runat="server" visible="false">
                <div class="card card-primary">
                    <div class="card-header justify-content-center" style="margin-top: 1rem;">
                        <h3>Submit Document</h3>
                    </div>
                    <div class="card-body">
                        <form method="POST" action="#" class="needs-validation" novalidate="">

                            <div id="submitDocument" class="form-group">
                                <!-- Submit Document Contents-->
                            </div>

                            <div class="form-group row">
                                <asp:Button ID="btnHome" runat="server" class="btn btn-md btn-block waves-effect text-center  btn-colorprimary btn-round" Text="Back"
                                    Style="background: #0D4B91; width: 15%; margin-top: 1rem; font-size: 1rem; margin-right: 1rem; color: #fff;"
                                    OnClick="btnHome_Click"></asp:Button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </section>

    </div>



</asp:Content>
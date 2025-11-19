<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CKE.aspx.cs" Inherits="DOMS_TSR.src.TestCode.CKE" %>
<%@Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CKEditor Tutorial in ASP.NET | YogiHosting Demo</title>
    <link rel="icon" type="image/png" href="http://www.yogihosting.com/wp-content/themes/yogi-yogihosting/Images/favicon.ico" />
    <style>
        body {
            background: #111 no-repeat;
            background-image: -webkit-gradient(radial, 50% 0, 150, 50% 0, 300, from(#444), to(#111));
        }

        h1, h2 {
            text-align: center;
            color: #FFF;
        }

            h2 a {
                color: #0184e3;
                text-decoration: none;
            }

        .container {
            width: 800px;
            margin: auto;
            color: #FFF;
            font-size: 25px;
        }

            .container h3 {
                text-decoration: underline;
                text-align: center;
            }

            .container h4, .container h5 {
                margin: 10px 0;
                padding-left: 190px;
            }

            .container h4 {
                color: #0184e3;
            }

            .container h5, .container a {
                color: #00e8ff;
            }

            .container .studentForm label {
                display: block;
                margin: 0;
                text-transform: capitalize;
            }

            .container .studentForm {
                color: #FFF;
                font-size: 25px;
            }

            .container .validationDiv {
                color: red;
            }

            .container .studentDiv {
                margin: 25px auto 0 auto;
            }

        .red {
            color: red;
        }

        #content {
            border: dashed 2px #CCC;
            padding: 10px;
        }

        #reset {
            padding: 5px 10px;
            background: #4CAF50;
            border: none;
            color: #FFF;
            cursor: pointer;
        }
    </style>
    <script src="/packages/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h1>CKEditor Tutorial in ASP.NET</h1>
        <h2><a href="http://www.yogihosting.com/ckeditor-tutorial-asp-net/">Read the tutorial on YogiHosting »</a> <asp:Button ID="reset" runat="server" Text="Reset »" OnClick="reset_Click" /></h2>
        <div class="container">
            <div id="content">
                <div id="errorDiv" runat="server" class="validationDiv"></div>
                <div id="studentFormDiv" class="studentForm" runat="server">
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/ckeditor" runat="server"></CKEditor:CKEditorControl>
                                    <label>Name</label>
                                    <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
                                    <span id="nameSpan"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Description</label>
                                    <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <script type="text/javascript" lang="javascript">
                                        CKEDITOR.replace('<%=descriptionTextBox.ClientID%>');
                                    </script>
                                    <span id="descriptionSpan"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div id="studentDataDiv" runat="server" class="studentForm">
                </div>
                <asp:Button ID="editButton" runat="server" Text="Edit" OnClick="editButton_Click" Visible="false"/>
            </div>
        </div>
    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#<%= submitButton.ClientID %>").click(function () {
                CKEDITOR.instances["<%= descriptionTextBox.ClientID %>"].updateElement();
                return Validate();
            });

            function Validate() {
                var errorMessage = "";

                //Name
                if ($("#<%= nameTextBox.ClientID %>").val() == "")
                    errorMessage += "☺ Enter Name<br/>";
                //End

                //Description
                if ($("#<%= descriptionTextBox.ClientID %>").val() == "")
                    errorMessage += "☺ Enter Description";
                //End

                if (errorMessage.length == 0)
                    return true;
                else {
                    $("#<%= errorDiv.ClientID %>").html(errorMessage);
                    return false;
                }
            }
        });
    </script>
</body>
</html>

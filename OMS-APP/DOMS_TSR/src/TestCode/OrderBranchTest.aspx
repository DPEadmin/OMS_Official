<%@ Page Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="OrderBranchTest.aspx.cs" Inherits="DOMS_TSR.src.TestCode.OrderBranchTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
 <style type="text/css">
        .container {
            background-color: #99CCFF;
            border: thick solid #808080;
            padding: 20px;
            margin: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
         
  <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

    <div class="container">

        <audio>
     
	    <source src="../../Image/Phone_Ringing.mp3" />
	
        </audio>

        <div id="yourname"></div>
        <asp:HiddenField ID="hiddisplayname" runat="server" />
     <asp:HiddenField ID="hidordermsg" runat="server" />
     <asp:HiddenField ID="hidBranchcode" runat="server" />

          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
               <asp:Button ID="btn01" Text="สร้างใบสั่งขาย" runat="server"  />
            <asp:Button ID="btn02" Text="อนุมัติใบสั่งขาย" runat="server" />
            <asp:Button ID="btn03" Text="จัดส่งสินค้า" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

     <asp:Button ID="btnsubmit" OnClick="btnsubmit_Click" runat="server" style="margin-left:-900px;" />
      <!--  Send to <input type="text" id="to" size="5" /> Message <input type="text" id="message" />
        <input type="button" id="sendmessage" value="Send" />
        <asp:LinkButton id="btnSend" class="btn-add button-active btn-small" data-backdrop="false"
                       OnClick="btnSend_Click" runat ="server"><i class="fa fa-plus"></i>Send</asp:LinkButton>
       
  
        
        <ul id="discussion"></ul> -->    
 
    </div>
   
    <script type="text/javascript">
        $(function () {

            var audio = document.getElementsByTagName("audio")[0];
           
            // Declare a proxy to reference the hub.
            var chat = $.connection.myChatHub;
       
            // Create a function that the hub can call to broadcast messages.
            chat.client.broadcastMessage = function (name, to, message) {
                // Html encode display name and message.
              //  $('#displayname').val("bvc");
                if (to == $('#<%= hiddisplayname.ClientID %>').val()) {
               
                    var encodedName = $('<div />').text(name).html();
                    var encodedMsg = $('<div />').text(message).html();
                    // Add the message to the page.
                    $('#discussion').append('<li><strong>' + encodedName
                       + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
                    $('#<%= hidordermsg.ClientID %>').val(encodedMsg);
                    // alert("ordermsg=" + $('#<%= hidordermsg.ClientID %>').val());
                    document.getElementById('<%= btnsubmit.ClientID %>').click();
                }
            };

               // Create a function that the hub can call to broadcast messages.
            chat.client.broadcasttest = function (name) {
                     alert("New Messege from " + name);

            };

            // Get the user name and store it to prepend to messages.
            //$('#displayname').val(prompt('Enter your name:', ''));
           // $('#yourname').html('Your name = ' + $('#displayname').val());
            // $('#displayname').val('b');
           $('#yourname').html('EmpCode =' + $('#<%= hiddisplayname.ClientID %>').val() + 'BranchCode =' + $('#<%= hidBranchcode.ClientID %>').val());
          
            // Set initial focus to message input box.
            $('#message').focus();
            // Start the connection.
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {
                   
                     
                    //  alert($('#displayname').val());
                    // alert($('#to').val());
                    // alert($('#message').val());
                    // Call the Send method on the hub.
                    chat.server.myChatSend($('#<%= hiddisplayname.ClientID %>').val(), $('#to').val(), $('#message').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                });
            });

         

        });
    </script>

</asp:Content>

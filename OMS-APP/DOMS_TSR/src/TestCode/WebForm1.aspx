<%@ Page Title="" Language="C#" MasterPageFile="~/src/MasterPage/Web.master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DOMS_TSR.src.TestCode.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
 <style type="text/css">
        .container {
            background-color: #99CCFF;
            border: thick solid #808080;
            padding: 20px;
            margin: 20px;
        }
    </style>
    <script>
        function EvalSound(soundobj)
            {
         var thissound = document.getElementById(soundobj);
         thissound.play();
        }
    
    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="testsound" OnClick="testsound_Click" Text="testsound" runat="server" />
       <audio id="audio1" src="call.wav" controls preload="auto" autobuffer HIDDEN=true >

    <div class="container">
        <div id="yourname"></div>
        <input type="hidden" id="displayname" />
        Send to <input type="text" id="to" size="5" /> Message <input type="text" id="message" />
        <input type="button" id="sendmessage" value="Send" />
        <asp:LinkButton id="btnSend" class="btn-add button-active btn-small" data-backdrop="false"
                       OnClick="btnSend_Click" runat ="server"><i class="fa fa-plus"></i>Send</asp:LinkButton>
       
        <ul id="discussion"></ul>
    </div>
   
    <script type="text/javascript">
        $(function () {

       
            // Declare a proxy to reference the hub.
            var chat = $.connection.myChatHub;
       
            // Create a function that the hub can call to broadcast messages.
            chat.client.broadcastMessage = function (name, to, message) {
                // Html encode display name and message.
                if (to == $('#displayname').val()) {
                    var encodedName = $('<div />').text(name).html();
                    var encodedMsg = $('<div />').text(message).html();
                    // Add the message to the page.
                    $('#discussion').append('<li><strong>' + encodedName
                        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
                }
            };

               // Create a function that the hub can call to broadcast messages.
            chat.client.broadcasttest = function (name) {
                     alert("New Messege from " + name);

            };

            // Get the user name and store it to prepend to messages.
            $('#displayname').val(prompt('Enter your name:', ''));
            $('#yourname').html('Your name = ' + $('#displayname').val());
          
            // Set initial focus to message input box.
            $('#message').focus();
            // Start the connection.
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {
                    // Call the Send method on the hub.
                    chat.server.myChatSend($('#displayname').val(), $('#to').val(), $('#message').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                });
            });

         

        });
    </script>

</asp:Content>

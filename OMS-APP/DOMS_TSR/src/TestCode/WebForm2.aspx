<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="DOMS_TSR.src.TestCode.WebForm2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Chart.js-master/css js/Chart.css">
    <link rel="icon" href="favicon.ico">
    <script src="Chart.js-master/css js/Chart.js"></script>
    <script src="utils.js"></script>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <div>
            <div class='tableauPlaceholder' id='viz1592389460452' style='position: relative'>
                <noscript><a href='#'><img alt=' ' src='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;tr&#47;trainday1_15923888735690&#47;Sheet1&#47;1_rss.png' style='border: none' /></a></noscript>
                <object class='tableauViz' style='display: none;'>
                    <param name='host_url' value='https%3A%2F%2Fpublic.tableau.com%2F' />
                    <param name='embed_code_version' value='3' />
                    <param name='site_root' value='' />
                    <param name='name' value='trainday1_15923888735690&#47;Sheet1' />
                    <param name='tabs' value='no' />
                    <param name='toolbar' value='yes' />
                    <param name='static_image' value='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;tr&#47;trainday1_15923888735690&#47;Sheet1&#47;1.png' />
                    <param name='animate_transition' value='yes' />
                    <param name='display_static_image' value='yes' />
                    <param name='display_spinner' value='yes' />
                    <param name='display_overlay' value='yes' />
                    <param name='display_count' value='yes' />
                    <param name='language' value='en' />
                </object>
            </div>
            <script type='text/javascript'>                    var divElement = document.getElementById('viz1592389460452'); var vizElement = divElement.getElementsByTagName('object')[0]; vizElement.style.width = '100%'; vizElement.style.height = (divElement.offsetWidth * 0.75) + 'px'; var scriptElement = document.createElement('script'); scriptElement.src = 'https://public.tableau.com/javascripts/api/viz_v1.js'; vizElement.parentNode.insertBefore(scriptElement, vizElement);                </script>
        </div>--%>
        <div>
               <asp:TextBox ID="TextBox1" runat="server" Height="40%" Width="40%"></asp:TextBox>

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
     <div>

         <asp:Button ID="Button2" runat="server" Text="gendata" OnClick="Button2_Click" />
         <asp:Label ID="lbltracking" runat="server" Text="Label"></asp:Label>
     </div>

    </form>
</body>
</html>

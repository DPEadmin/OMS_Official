using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pchp.Core;
using Pchp.Library;
using Pchp.Library.DateTime;
using System;
using System.Reflection;

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: PhpExtension(new string[]
{

})]
[assembly: TargetPhpLanguage("7.2", false)]
namespace <Root>
{
    [Script("index.php")]
public static class index_php
{
    public static long <Main>(Context<ctx>, PhpArray<locals>, object @this, RuntimeTypeHandle<self>)
        {
            <ctx>.OnInclude<index_php>();
            <locals>.SetItemValue(new IntStringKey("merchant_id"), /*PhpValue*/PhpValue.op_Implicit("JT01"));
            <locals>.SetItemValue(new IntStringKey("secret_key"), /*PhpValue*/PhpValue.op_Implicit("7jYcp4FxFdf0"));
            <locals>.SetItemValue(new IntStringKey("payment_description"), /*PhpValue*/PhpValue.op_Implicit("2 days 1 night hotel room"));
            <locals>.SetItemValue(new IntStringKey("order_id"), /*PhpValue*/PhpValue.op_Implicit(DateTimeFunctions.time()));
            <locals>.SetItemValue(new IntStringKey("currency"), /*PhpValue*/PhpValue.op_Implicit("702"));
            <locals>.SetItemValue(new IntStringKey("amount"), /*PhpValue*/PhpValue.op_Implicit("000000002500"));
            <locals>.SetItemValue(new IntStringKey("version"), /*PhpValue*/PhpValue.op_Implicit("8.5"));
            <locals>.SetItemValue(new IntStringKey("payment_url"), /*PhpValue*/PhpValue.op_Implicit("https://demo2.2c2p.com/2C2PFrontEnd/RedirectV3/payment"));
            <locals>.SetItemValue(new IntStringKey("result_url_1"), /*PhpValue*/PhpValue.op_Implicit("http://localhost/devPortal/V3_UI_PHP_JT01_devPortal/result.php"));
            PhpArray arg_1B7_0 = < locals >;
    IntStringKey arg_1B7_1 = new IntStringKey("params");
    PhpString.Blob expr_100 = new PhpString.Blob();
    expr_100.Add(<locals>.GetItemValue(new IntStringKey("version")), <ctx>);
            PhpString.Blob expr_118 = expr_100;
    expr_118.Add(<locals>.GetItemValue(new IntStringKey("merchant_id")), <ctx>);
            PhpString.Blob expr_130 = expr_118;
    expr_130.Add(<locals>.GetItemValue(new IntStringKey("payment_description")), <ctx>);
            PhpString.Blob expr_148 = expr_130;
    expr_148.Add(<locals>.GetItemValue(new IntStringKey("order_id")), <ctx>);
            PhpString.Blob expr_160 = expr_148;
    expr_160.Add(<locals>.GetItemValue(new IntStringKey("currency")), <ctx>);
            PhpString.Blob expr_178 = expr_160;
    expr_178.Add(<locals>.GetItemValue(new IntStringKey("amount")), <ctx>);
            PhpString.Blob expr_190 = expr_178;
    expr_190.Add(<locals>.GetItemValue(new IntStringKey("result_url_1")), <ctx>);
            arg_1B7_0.SetItemValue(arg_1B7_1, /*PhpValue*/PhpString.op_Implicit(new PhpString(new PhpString(expr_190))));
            PhpArray arg_22D_0 = < locals >;
    IntStringKey arg_22D_1 = new IntStringKey("hash_value");
    PhpString expr_210 = PhpHash.hash_hmac("sha256", < locals >.GetItemValue(new IntStringKey("params")).ToPhpString(< ctx >).ToBytes(< ctx >), < locals >.GetItemValue(new IntStringKey("secret_key")).ToPhpString(< ctx >).ToBytes(< ctx >), false);
    arg_22D_0.SetItemValue(arg_22D_1, (PhpString.IsNull(expr_210)? PhpValue.False : /*PhpValue*/PhpString.op_Implicit(expr_210)).DeepCopy());
            <ctx>.Echo("Payment information:");
            <ctx>.Echo("<html> \r\n\t<body>\r\n\t<form id=\"myform\" method=\"post\" action=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("payment_url")));
            <ctx>.Echo("\">\r\n\t\t<input type=\"hidden\" name=\"version\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("version")));
            <ctx>.Echo("\"/>\r\n\t\t<input type=\"hidden\" name=\"merchant_id\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("merchant_id")));
            <ctx>.Echo("\"/>\r\n\t\t<input type=\"hidden\" name=\"currency\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("currency")));
            <ctx>.Echo("\"/>\r\n\t\t<input type=\"hidden\" name=\"result_url_1\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("result_url_1")));
            <ctx>.Echo("\"/>\r\n\t\t<input type=\"hidden\" name=\"hash_value\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("hash_value")));
            <ctx>.Echo("\"/>\r\n    PRODUCT INFO : <input type=\"text\" name=\"payment_description\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("payment_description")));
            <ctx>.Echo("\"  readonly/><br/>\r\n\t\tORDER NO : <input type=\"text\" name=\"order_id\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("order_id")));
            <ctx>.Echo("\"  readonly/><br/>\r\n\t\tAMOUNT: <input type=\"text\" name=\"amount\" value=\"");
            <ctx>.Echo(<locals>.GetItemValue(new IntStringKey("amount")));
            <ctx>.Echo("\" readonly/><br/>\r\n\t\t<input type=\"submit\" name=\"submit\" value=\"Confirm\" />\r\n\t</form>  \r\n\t\r\n\t<script type=\"text/javascript\">\r\n\t\tdocument.forms.myform.submit();\r\n\t</script>\r\n\t</body>\r\n\t</html>");
            return 1L;
        }

public static PhpValue<Main>(Context<ctx>, PhpArray<locals>, object @this, RuntimeTypeHandle<self>)
        {
            return /*PhpValue*/PhpValue.op_Implicit(index_php.<Main>(<ctx>, <locals>, @this, <self>));
        }
    }
}

namespace DOMS_TSR.src.TestCode
{
    public partial class payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    public class CallInInfo
    {
        //Merchant's account information
        public String merchant_id { get; set; }
        public String secret_key { get; set; }
        //Transaction information
        public String payment_description { get; set; }
        public String order_id { get; set; }
        public String currency { get; set; }
        public String amount { get; set; }
        //Request information
        public String version { get; set; }
        public String payment_url { get; set; }
        public String result_url_1 { get; set; }
        //Construct signature string
        
    public String hash_value { get; set; }
     
    }
}
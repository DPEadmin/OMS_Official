using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DOMS_TSR.src.BranchManagement
{
    public partial class BranchMapProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Functions

        #endregion

        #region Events

        #endregion

        #region Bindings

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"PromotionDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        #endregion

    }
}
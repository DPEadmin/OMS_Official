using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using System.Globalization;
using System.IO;

namespace DOMS_TSR.src.Purchasing
{
    public partial class CreatePO : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;
        protected List<POItemInfo> L_productlist
        {
            get
            {
                if (Session["l_productlist"] == null)
                {
                    return new List<POItemInfo>();
                }
                else
                {
                    return (List<POItemInfo>)Session["l_productlist"];
                }
            }
            set
            {
                Session["l_productlist"] = value;
            }
        }
        protected List<POItemInfo> L_productlistDb
        {
            get
            {
                if (Session["l_productlistdb"] == null)
                {
                    return new List<POItemInfo>();
                }
                else
                {
                    return (List<POItemInfo>)Session["l_productlistdb"];
                }
            }
            set
            {
                Session["l_productlistdb"] = value;
            }
        }
        protected List<POItemInfo> L_productlistInit
        {
            get
            {
                if (Session["l_productlistinit"] == null)
                {
                    return new List<POItemInfo>();
                }
                else
                {
                    return (List<POItemInfo>)Session["l_productlistinit"];
                }
            }
            set
            {
                Session["l_productlistinit"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                hidApprover1.Value = (Request.QueryString["hidApprover1"] != null) ? Request.QueryString["hidApprover1"].ToString() : "";

                if (hidApprover1.Value == "True")
                {
                    lblHeadPOStatus.Text = "PO Detail Approve Task";

                    btnSaveDraft.Visible = false;
                    btnSubmitByRequestor.Visible = false;
                    btnCancel.Visible = false;
                    btnApprove.Visible = true;
                    btnRevise.Visible = true;
                    btnReject.Visible = true;

                    txtSupplierName.ReadOnly = true;
                    SupplierName.Visible = false;
                    txtPODate.ReadOnly = true;
                    txtPOExpectDate.ReadOnly = true;
                    ddlPOPaymentType.Visible = false;
                    txtPOPaymentType.Visible = true;
                    txtPOCredit.ReadOnly = true;
                    txtInventoryName.ReadOnly = true;
                    linkbtnSelectInventory.Visible = false;

                    btnSelectProduct.Visible = false;
                    btnShowProduct.Visible = true;
                }
                else
                {
                    lblHeadPOStatus.Text = "Create PO";

                    btnSaveDraft.Visible = true;
                    btnSubmitByRequestor.Visible = true;
                    btnCancel.Visible = true;
                    btnApprove.Visible = false;
                    btnRevise.Visible = false;
                    btnReject.Visible = false;

                    txtPOPaymentType.Visible = false;
                    btnShowProduct.Visible = false;
                }

                hidFlagInsert.Value = (Request.QueryString["hidFlagInsert"] != null) ? Request.QueryString["hidFlagInsert"].ToString() : "";

                bindddlddlPOPaymentType();
                loadPOCodeandDateInsert();
            }            
        }
        #region event        
        protected void SelectSupplierName(object sender, EventArgs e)
        {
            txtSearchSupplierCode.Text = "";
            txtSearchSupplierName.Text = "";
            loadSupplier();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal();", true);
        }
        protected void SelectInventoryName(object sender, EventArgs e)
        {
            txtSearchInventoryCode.Text = "";
            txtSearchInventoryName.Text = "";
            loadInventory();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addinventory').modal();", true);
        }
        protected void btnSelectProduct_Click(object sender, EventArgs e)
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
            loadProduct();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addproduct').modal();", true);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            loadSupplier();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal();", true);
        }
        protected void btnSearchInventory_Click(object sender, EventArgs e)
        {
            loadInventory();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addinventory').modal();", true);
        }
        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            loadProduct();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addproduct').modal();", true);
        }
        protected void btnClearSupplier_Click(object sender, EventArgs e)
        {
            txtSearchSupplierCode.Text = "";
            txtSearchSupplierName.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal();", true);
        }
        protected void btnClearSearchInventory_Click(object sender, EventArgs e)
        {
            txtSearchInventoryCode.Text = "";
            txtSearchInventoryName.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addinventory').modal();", true);
        }
        protected void btnClearSearchProduct_Click(object sender, EventArgs e)
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addproduct').modal();", true);
        }
        protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvSupplier.Rows[index];

            HiddenField hidSupplierId = (HiddenField)row.FindControl("hidSupplierId");
            HiddenField hidSupplierCode = (HiddenField)row.FindControl("hidSupplierCode");
            HiddenField hidSupplierName = (HiddenField)row.FindControl("hidSupplierName");
            HiddenField hidPhoneNumber = (HiddenField)row.FindControl("hidPhoneNumber");
            HiddenField hidFaxNumber = (HiddenField)row.FindControl("hidFaxNumber");
            HiddenField hidMail = (HiddenField)row.FindControl("hidMail");
            HiddenField hidAddress = (HiddenField)row.FindControl("hidAddress");
            HiddenField hidSubDistrictName = (HiddenField)row.FindControl("hidSubDistrictName");
            HiddenField hidDistrictName = (HiddenField)row.FindControl("hidDistrictName");
            HiddenField hidProvinceName = (HiddenField)row.FindControl("hidProvinceName");
            HiddenField hidZipNo = (HiddenField)row.FindControl("hidZipNo");
            HiddenField hidTaxIdNo = (HiddenField)row.FindControl("hidTaxIdNo");
            HiddenField hidContactor = (HiddenField)row.FindControl("hidContactor");

            if (e.CommandName == "SelectSupplier")
            {
                txtSupplierName.Text = hidSupplierName.Value;
                txtSupplierTaxIdNo.Text = hidTaxIdNo.Value;
                txtSupplierAddress.Text = hidAddress.Value + " " + hidSubDistrictName.Value + " " + hidDistrictName.Value + " " + hidProvinceName.Value + " " + hidZipNo.Value;
                txtSupplierContactor.Text = hidContactor.Value;
                txtSupplierPhoneNumber.Text = hidPhoneNumber.Value;
                txtSupplierFaxNumber.Text = hidFaxNumber.Value;
                txtSupplierMail.Text = hidMail.Value;

                hidSupplierCodeIns.Value = hidSupplierCode.Value;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal('hide');", true);
            }
        }
        protected void gvInventory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvInventory.Rows[index];

            HiddenField hidInventoryId = (HiddenField)row.FindControl("hidInventoryId");
            HiddenField hidInventoryCode = (HiddenField)row.FindControl("hidInventoryCode");
            HiddenField hidInventoryName = (HiddenField)row.FindControl("hidInventoryName");
            HiddenField hidAddress = (HiddenField)row.FindControl("hidAddress");
            HiddenField hidProvinceName = (HiddenField)row.FindControl("hidProvinceName");
            HiddenField hidDistrictName = (HiddenField)row.FindControl("hidDistrictName");
            HiddenField hidSubDistrictName = (HiddenField)row.FindControl("hidSubDistrictName");
            HiddenField hidPostCode = (HiddenField)row.FindControl("hidPostCode");
            HiddenField hidContactTel = (HiddenField)row.FindControl("hidContactTel");
            HiddenField hidFax = (HiddenField)row.FindControl("hidFax");

            if (e.CommandName == "SelectInventory")
            {
                txtInventoryName.Text = hidInventoryName.Value;
                txtInventoryAddress.Text = hidAddress.Value + " " + hidSubDistrictName.Value + " " + hidDistrictName.Value + " " + hidProvinceName.Value + " " + hidPostCode.Value;
                txtInventoryContactTel.Text = hidContactTel.Value;
                txtInventoryFax.Text = hidFax.Value;

                hidInventoryCodeIns.Value = hidInventoryCode.Value;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addinventory').modal('hide');", true);
            }
        }
        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProduct.Rows[index];

            HiddenField hidProductId = (HiddenField)row.FindControl("hidProductId");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");

            if (e.CommandName == "SelectProduct")
            {
                loadgvProductSelected(hidProductCode.Value);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addproduct').modal('hide');", true);
            }
        }
        protected void gvProductSelected_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProductSelected.Rows[index];

            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidRunningNo = (HiddenField)row.FindControl("hidRunningNo");

            if (e.CommandName == "DeleteProduct")
            {
                    L_productlist.RemoveAll(x => x.RunningNo == Convert.ToInt32(hidRunningNo.Value));

                    loadTotal();
                    gvProductSelected.DataSource = L_productlist;
                    gvProductSelected.DataBind();                
            }
        }
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            POItemInfo pdInfo = new POItemInfo();
            List<POItemInfo> lpdInfo = new List<POItemInfo>();

            Double? sumprice = 0;

            for (int i = 0; i < gvProductSelected.Rows.Count; i++)
            {
                Label lblProductCodeSelected = (Label)gvProductSelected.Rows[i].FindControl("lblProductCodeSelected");
                Label lblProductNameSelected = (Label)gvProductSelected.Rows[i].FindControl("lblProductNameSelected");
                Label lblUnitName = (Label)gvProductSelected.Rows[i].FindControl("lblUnitName");
                TextBox txtPrice = (TextBox)gvProductSelected.Rows[i].FindControl("txtPrice");
                TextBox txtAmount = (TextBox)gvProductSelected.Rows[i].FindControl("txtAmount");
                TextBox txtDiscountAmount = (TextBox)gvProductSelected.Rows[i].FindControl("txtDiscountAmount");
                TextBox txtDiscountPercent = (TextBox)gvProductSelected.Rows[i].FindControl("txtDiscountPercent");
                Label lblSumPrice = (Label)gvProductSelected.Rows[i].FindControl("lblSumPrice");
                HiddenField hidRunningNo = (HiddenField)gvProductSelected.Rows[i].FindControl("hidRunningNo");

                sumprice = (Convert.ToDouble(txtPrice.Text) - Convert.ToDouble(txtDiscountAmount.Text) - ((Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtDiscountPercent.Text)) / 100)) * Convert.ToInt32(txtAmount.Text);

                pdInfo = new POItemInfo();

                pdInfo.SupplierCode = hidSupplierCodeIns.Value;
                pdInfo.InventoryCode = hidInventoryCodeIns.Value;
                pdInfo.ProductCode = lblProductCodeSelected.Text;
                pdInfo.ProductName = lblProductNameSelected.Text;
                pdInfo.UnitName = lblUnitName.Text;
                pdInfo.Price = (txtPrice.Text != "") ? Convert.ToDouble(txtPrice.Text) : 0;
                pdInfo.QTY = (txtAmount.Text != "") ? Convert.ToInt32(txtAmount.Text) : 0;
                pdInfo.DiscountAmount = (txtDiscountAmount.Text != "") ? Convert.ToDouble(txtDiscountAmount.Text) : 0;
                pdInfo.DiscountPercent = (txtDiscountPercent.Text != "") ? Convert.ToInt32(Convert.ToDouble(txtDiscountPercent.Text)) : 0;
                pdInfo.SumPrice = sumprice;
                pdInfo.TotPrice = sumprice;
                pdInfo.RunningNo = Convert.ToInt32(hidRunningNo.Value);

                lpdInfo.Add(pdInfo);                
            }

            L_productlist = lpdInfo;

            loadTotal();
            gvProductSelected.DataSource = L_productlist;
            gvProductSelected.DataBind();
        }
        protected void txtDiscountAmount_Textchanged(object sender, EventArgs e)
        {
            txtAmount_TextChanged(null, null);
        }
        protected void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            txtAmount_TextChanged(null, null);
        }
        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            txtAmount_TextChanged(null, null);
        }
        protected void gvProductSelected_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtPrice = (TextBox)e.Row.FindControl("txtPrice");
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                TextBox txtDiscountAmount = (TextBox)e.Row.FindControl("txtDiscountAmount");
                Label lblDiscountAmount = (Label)e.Row.FindControl("lblDiscountAmount");
                TextBox txtDiscountPercent = (TextBox)e.Row.FindControl("txtDiscountPercent");
                Label lblDiscountPercent = (Label)e.Row.FindControl("lblDiscountPercent");
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                LinkButton buttonDelete = (LinkButton)e.Row.FindControl("buttonDelete");

                Double? price = 0;
                Double? discountamount = 0;
                Double? discountpercent = 0;

                price = Convert.ToDouble(txtPrice.Text);
                discountamount = (txtDiscountAmount.Text !="") ? Convert.ToDouble(txtDiscountAmount.Text) : 0;
                discountpercent = (txtDiscountPercent.Text != "") ? Convert.ToDouble(txtDiscountPercent.Text) : 0;

                txtPrice.Text = string.Format("{0:n}", (price));
                txtDiscountAmount.Text = string.Format("{0:n}", (discountamount));
                txtDiscountPercent.Text = string.Format("{0:n}", (discountpercent));

                if(hidApprover1.Value == "True")
                {
                    txtAmount.Visible = false;
                    lblAmount.Visible = true;
                    txtPrice.Visible = false;
                    lblPrice.Visible = true;
                    txtDiscountAmount.Visible = false;
                    lblDiscountAmount.Visible = true;
                    txtDiscountPercent.Visible = false;
                    lblDiscountPercent.Visible = true;
                    buttonDelete.Visible = false;
                    txtBillDiscount.ReadOnly = true;
                }
                else
                {
                    txtAmount.Visible = true;
                    lblAmount.Visible = false;
                    txtPrice.Visible = true;
                    lblPrice.Visible = false;
                    txtDiscountAmount.Visible = false;
                    lblDiscountAmount.Visible = true;
                    txtDiscountPercent.Visible = false;
                    lblDiscountPercent.Visible = true;
                    buttonDelete.Visible = true;
                    txtBillDiscount.ReadOnly = false;
                }
            }            
        }
        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            if (hidwfstatus.Value == "")
            {
                hidwfstatus.Value = StaticField.WfStatus_Savedraft; 
                hidwftaskliststatus.Value = StaticField.WfStatus_100; 
            }
            else if (hidwfstatus.Value == StaticField.WfStatus_SubmitByRequestor) 
            {
                hidwftaskliststatus.Value = StaticField.WfStatus_200; 
            }
            else if (hidwfstatus.Value == StaticField.WfStatus_Approve) 
            {
                hidwftaskliststatus.Value = StaticField.WfStatus_1200; 
            }
            else if (hidwfstatus.Value == StaticField.WfStatus_Revise) 
            {
                hidwftaskliststatus.Value = StaticField.WfStatus_400; 
            }
            else if (hidwfstatus.Value == StaticField.WfStatus_Reject) 
            {
                hidwftaskliststatus.Value = StaticField.WfStatus_500; 
            }
            else if (hidwfstatus.Value == StaticField.WfStatus_Savedraft) 
            {
                hidwftaskliststatus.Value = StaticField.WfStatus_100; 
            }
            string insertsuccess = "";
            string updateworkflowstatussuccess = "";
            int? poid = 0;

            EmpInfo empInfo = new EmpInfo();
            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (ValidateInsertUpdatePO()) 
                {
                    if (hidFlagInsert.Value == "True") // Insert
                    {
                        // Insert PO
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/Insertpo";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["POCode"] = txtPOCode.Text;
                            data["PODate"] = txtPODate.Text;
                            data["SupplierCode"] = hidSupplierCodeIns.Value;
                            data["InventoryCode"] = hidInventoryCodeIns.Value;
                            data["Price"] = Convert.ToDecimal( txtTotalPrice.Text).ToString();

                            if (hidwfstatus.Value == StaticField.WfStatus_Savedraft) 
                            {
                                data["StatusCode"] = StaticField.CreatePO_StatusCode01; 
                            }
                            else if (hidwfstatus.Value == StaticField.WfStatus_SubmitByRequestor) 
                            {
                                data["StatusCode"] = StaticField.CreatePO_StatusCode02; 
                            }

                            data["RequestDate"] = DateTime.ParseExact(txtPODate.Text, "dd/MM/yyyy", null).ToString();
                            data["ExpectDate"] = DateTime.ParseExact(txtPOExpectDate.Text, "dd/MM/yyyy", null).ToString();
                            data["Description"] = txtPODescription.Text;
                            data["PaymentMethodCode"] = ddlPOPaymentType.SelectedValue;
                            data["Credit"] = txtPOCredit.Text;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);                            
                        }

                        // Insert POItem
                        foreach (var lpV in L_productlist)
                        {
                            string respstringpoitem = "";
                            APIpath = APIUrl + "/api/support/InsertpoItem";

                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();

                                data["POCode"] = txtPOCode.Text;
                                data["ProductCode"] = lpV.ProductCode;
                                data["SupplierCode"] = lpV.SupplierCode;
                                data["InventoryCode"] = lpV.InventoryCode;
                                data["Price"] = (Convert.ToDouble(lpV.Price)).ToString();
                                data["DiscountAmount"] = (Convert.ToDouble(lpV.DiscountAmount)).ToString();
                                data["DiscountPercent"] = (Convert.ToDouble(lpV.DiscountPercent)).ToString();
                                data["DiscountBill"] = (Convert.ToDouble(txtBillDiscount.Text)).ToString();
                                data["RunningNo"] = lpV.RunningNo.ToString();
                                data["QTY"] = (Convert.ToInt32(lpV.QTY)).ToString();
                                data["TotPrice"] = (Convert.ToDouble(lpV.TotPrice)).ToString();
                                data["CreateBy"] = empInfo.EmpCode;
                                data["UpdateBy"] = empInfo.EmpCode;
                                data["FlagDelete"] = "N";
                                data["Active"] = "Y";

                                var response = wb.UploadValues(APIpath, "POST", data);

                                respstringpoitem = Encoding.UTF8.GetString(response);
                                insertsuccess = Encoding.UTF8.GetString(response);
                            }
                        }

                        // Insert Find PO Id After Insert
                        string respstrPOId = "";

                        APIpath = APIUrl + "/api/support/ListPONopagingByCriteria";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["POCode"] = txtPOCode.Text;

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstrPOId = Encoding.UTF8.GetString(response);
                        }

                        List<POInfo> lpoInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstrPOId);

                        if (lpoInfo.Count > 0)
                        {
                            poid = lpoInfo[0].POId;
                        }

                        // Insert Workflow
                        string respstrwf = "";

                        APIpath = APIUrl + "/api/support/InsertWFTaskList";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["OMSId"] = poid.ToString();
                            data["WFType"] = StaticField.WF_Type_20; // for PO Work Flow
                            data["Status"] = hidwftaskliststatus.Value;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstrwf = Encoding.UTF8.GetString(response);
                        }
                    }
                    else // Update
                    {
                        // Update PO
                        string respstrupd = "";

                        APIpath = APIUrl + "/api/support/UpdatePO";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["POId"] = hidPOIdUpd.Value.ToString();
                            data["PODate"] = txtPODate.Text;
                            data["SupplierCode"] = hidSupplierCodeIns.Value;
                            data["InventoryCode"] = hidInventoryCodeIns.Value;
                            data["Price"] = Convert.ToDecimal(txtTotalPrice.Text).ToString();

                            if (hidwfstatus.Value == "Savedraft")
                            {
                                data["StatusCode"] = StaticField.CreatePO_StatusCode01; 
                            }
                            else if (hidwfstatus.Value == "SubmitByRequestor")
                            {
                                data["StatusCode"] = StaticField.CreatePO_StatusCode02; 
                            }

                            data["RequestDate"] = txtPODate.Text;
                            data["ExpectDate"] = txtPOExpectDate.Text;
                            data["Description"] = txtPODescription.Text;
                            data["PaymentMethodCode"] = ddlPOPaymentType.SelectedValue;
                            data["Credit"] = txtPOCredit.Text;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstrupd = Encoding.UTF8.GetString(response);
                        }

                        if (hidApprover1.Value != "True")
                        {
                            // Update POItem (Delete and Re-Insert POItem)
                            // Delete POItem from POCode
                            string resdel = "";

                            APIpath = APIUrl + "/api/support/DeletePOItem";

                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();

                                data["POCode"] = txtPOCode.Text;

                                var response = wb.UploadValues(APIpath, "POST", data);

                                resdel = Encoding.UTF8.GetString(response);
                            }

                            // Re Insert POItem
                            // Insert POItem
                            foreach (var lpV in L_productlist)
                            {
                                string respstringpoitem = "";
                                APIpath = APIUrl + "/api/support/InsertpoItem";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["POCode"] = txtPOCode.Text;
                                    data["ProductCode"] = lpV.ProductCode;
                                    data["SupplierCode"] = lpV.SupplierCode;
                                    data["InventoryCode"] = lpV.InventoryCode;
                                    data["Price"] = (Convert.ToDouble(lpV.Price)).ToString();
                                    data["DiscountAmount"] = (Convert.ToDouble(lpV.DiscountAmount)).ToString();
                                    data["DiscountPercent"] = (Convert.ToDouble(lpV.DiscountPercent)).ToString();
                                    data["DiscountBill"] = (Convert.ToDouble(txtBillDiscount.Text)).ToString();
                                    data["RunningNo"] = lpV.RunningNo.ToString();
                                    data["QTY"] = (Convert.ToInt32(lpV.QTY)).ToString();
                                    data["TotPrice"] = (Convert.ToDouble(lpV.TotPrice)).ToString();
                                    data["CreateBy"] = empInfo.EmpCode;
                                    data["UpdateBy"] = empInfo.EmpCode;
                                    data["FlagDelete"] = "N";
                                    data["Active"] = "Y";

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstringpoitem = Encoding.UTF8.GetString(response);
                                    insertsuccess = Encoding.UTF8.GetString(response);
                                }
                            }
                        }

                        // Update Workflow Status
                        string respstrwf = "";

                        APIpath = APIUrl + "/api/support/UpdateWFTaskList";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["OMSId"] = hidPOIdUpd.Value;
                            data["Status"] = hidwftaskliststatus.Value;
                            data["UpdateBy"] = empInfo.EmpCode;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstrwf = Encoding.UTF8.GetString(response);
                            updateworkflowstatussuccess = respstrwf;
                        }

                        if (respstrupd != "")
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "เปลี่ยนแปลงข้อมูลเสร็จสิ้น เลขที่ใบ PO : " + txtPOCode.Text + "');", true);
                        }
                    }
                }

                if (hidApprover1.Value == "")
                {
                    if (insertsuccess != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "บันทึกข้อมูลเสร็จสิ้น เลขที่ใบ PO : " + txtPOCode.Text + "');", true);
                        
                        Response.Redirect("/src/Purchasing/PO.aspx");
                    }
                }
                else
                {
                    if (updateworkflowstatussuccess != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "บันทึกข้อมูลเสร็จสิ้น เลขที่ใบ PO : " + txtPOCode.Text + "');", true);
                        
                        Response.Redirect("/src/Purchasing/POWorkList.aspx");
                    }
                }
            }            
        }
        protected void btnSubmitByRequestor_Click(object sender, EventArgs e)
        {
            hidwfstatus.Value = StaticField.WfStatus_SubmitByRequestor; 

            btnSaveDraft_Click(null, null);
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            hidwfstatus.Value = StaticField.WfStatus_Approve; 

            btnSaveDraft_Click(null, null);
        }
        protected void btnRevise_Click(object sender, EventArgs e)
        {
            hidwfstatus.Value = StaticField.WfStatus_Revise; 

            btnSaveDraft_Click(null, null);
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            hidwfstatus.Value = StaticField.WfStatus_Reject; 

            btnSaveDraft_Click(null, null);
        }
        #endregion

        #region function
        protected void loadPOCodeandDateInsert()
        {
            if (hidFlagInsert.Value == "True") // Insert Flow
            {
                string POCode = "PO0000" + runningNo().ToString();
                txtPOCode.Text = POCode;
                txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else // Update Flow
            {
                txtPOCode.Text = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";

                // Load PO Information Section

                List<POInfo> lpoInfo = new List<POInfo>();
                lpoInfo = GetPOMaster();

                if (lpoInfo.Count > 0)
                {
                    txtPODate.Text = lpoInfo[0].PODate;
                    txtPOExpectDate.Text = lpoInfo[0].ExpectDate;
                    txtPOCredit.Text = lpoInfo[0].Credit;
                    txtPODescription.Text = lpoInfo[0].Description;
                    ddlPOPaymentType.SelectedValue = lpoInfo[0].PaymentMethodCode;
                    txtPOPaymentType.Text = lpoInfo[0].PaymentMethodName.Trim();
                    hidPOIdUpd.Value = lpoInfo[0].POId.ToString();

                    string[] s = txtPODate.Text.Split(' ');
                    string[] v = txtPOExpectDate.Text.Split(' ');

                    for (int i = 0; i < s.Length; i++)
                    {
                        if (i == 0)
                        {
                            txtPODate.Text = s[0];
                        }
                        else if (i == 1)
                        {
                        }
                    }

                    for (int i = 0; i < v.Length; i++)
                    {
                        if (i == 0)
                        {
                            txtPOExpectDate.Text = v[0];
                        }
                        else if (i == 1)
                        {
                        }
                    }
                }

                // Load Supplier Section
                List<SupplierInfo> lsupInfo = new List<SupplierInfo>();
                lsupInfo = GetPOMapSupplier();

                if (lsupInfo.Count > 0)
                {
                    for (int i = 0; i < lsupInfo.Count; i++)
                    {
                        txtSupplierName.Text = lsupInfo[0].SupplierName;
                        txtSupplierTaxIdNo.Text = lsupInfo[0].TaxIdNo;
                        txtSupplierAddress.Text = lsupInfo[0].Address + " " + lsupInfo[0].SubDistrictName + " " + lsupInfo[0].DistrictName + " " + lsupInfo[0].ProvinceName + " " + lsupInfo[0].ZipNo;
                        txtSupplierContactor.Text = lsupInfo[0].Contactor;
                        txtSupplierPhoneNumber.Text = lsupInfo[0].PhoneNumber;
                        txtSupplierFaxNumber.Text = lsupInfo[0].FaxNumber;
                        txtSupplierMail.Text = lsupInfo[0].Mail;
                    }
                }

                // Load Inventory Section
                List<InventoryInfo> linvInfo = new List<InventoryInfo>();
                linvInfo = GetPOmapInventory();

                if (linvInfo.Count > 0)
                {
                    for (int i = 0; i < linvInfo.Count; i++)
                    {
                        txtInventoryName.Text = linvInfo[0].InventoryName;
                        txtInventoryAddress.Text = linvInfo[0].Address + " " + linvInfo[0].SubDistrictName + " " + linvInfo[0].DistrictName + " " + linvInfo[0].ProvinceName + " " + linvInfo[0].PostCode;
                        txtInventoryContactTel.Text = linvInfo[0].ContactTel;
                        txtInventoryFax.Text = linvInfo[0].Fax;
                    }
                }

                // Load POItem Section
                List<POItemInfo> lpoitem = new List<POItemInfo>();
                lpoitem = GetPOItem();

                if (lpoitem.Count > 0)
                {
                    foreach (var lpoV in lpoitem.ToList())
                    {
                        lpoV.SumPrice = lpoV.TotPrice;
                    }
                }

                L_productlist = lpoitem;
                L_productlistDb = lpoitem;
                L_productlistInit = lpoitem;

                loadTotalfromPOItemList(L_productlistDb);
                gvProductSelected.DataSource = L_productlistDb;
                gvProductSelected.DataBind();
            }
        }
        public int? runningNo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountPOCodeRunningNumber";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        protected void loadSupplier()
        {
            List<SupplierInfo> lsupplierInfo = new List<SupplierInfo>();
            lsupplierInfo = GetSupplierMasterNoPagingByCriteria();

            gvSupplier.DataSource = lsupplierInfo;
            gvSupplier.DataBind();            
        }
        protected void loadInventory()
        {
            List<InventoryInfo> linventoryInfo = new List<InventoryInfo>();
            linventoryInfo = GetInventoryMasterNoPagingByCriteria();

            gvInventory.DataSource = linventoryInfo;
            gvInventory.DataBind();
        }
        protected void loadProduct()
        {
            List<ProductInfo> lproducInfo = new List<ProductInfo>();
            lproducInfo = GetProductMasterNoPagingCriteria();


            if (L_productlist.Count > 0)
            {
                foreach (var LpV in L_productlist.ToList())
                {
                    lproducInfo.RemoveAll(x => x.ProductCode == LpV.ProductCode);
                }

                gvProduct.DataSource = lproducInfo;
                gvProduct.DataBind();
            }
            if (L_productlistDb.Count > 0)
            {
                foreach (var LpB in L_productlistDb.ToList())
                {
                    lproducInfo.RemoveAll(x => x.ProductCode == LpB.ProductCode);
                }

                gvProduct.DataSource = lproducInfo;
                gvProduct.DataBind();
            }
            else
            {
                gvProduct.DataSource = lproducInfo;
                gvProduct.DataBind();
            }            
        }
        protected void loadgvProductSelected(String productcode)
        {
            List<POItemInfo> lproducInfo = new List<POItemInfo>();
            List<POItemInfo> lpoitemtemp = new List<POItemInfo>();
            lproducInfo = GetProductMasterSelectedNoPagingCriteria(productcode);

            foreach (var lpV in lproducInfo.ToList())
            {
                lpV.SupplierCode = hidSupplierCodeIns.Value;
                lpV.InventoryCode = hidInventoryCodeIns.Value;
                lpV.QTY = 1;
                lpV.DiscountAmount = 0;
                lpV.DiscountPercent = 0;
                lpV.SumPrice = (lpV.Price - lpV.DiscountAmount - (((lpV.Price * lpV.DiscountPercent) / 100))) * lpV.QTY;
                lpV.TotPrice = lpV.SumPrice;

                int? max = L_productlist.Max(r => r.RunningNo);
                max = (max == null) ? 0 : max;
                lpV.RunningNo = max + 1;
            }

            if (hidFlagInsert.Value == "True")
            {
                if (L_productlist.Count > 0)
                {
                    L_productlist.AddRange(lproducInfo);
                }
                else
                {
                    L_productlist = lproducInfo;
                }
            }
            else
            {
                if (L_productlist.Count == 0)
                {
                    L_productlist = new List<POItemInfo>();
                    L_productlist.AddRange(L_productlistDb);
                    L_productlist.AddRange(lproducInfo);
                }
                else
                {
                    L_productlist.AddRange(lproducInfo);
                }
            }

            loadTotal();
            gvProductSelected.DataSource = L_productlist;
            gvProductSelected.DataBind();
        }
        protected void bindddlddlPOPaymentType()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListPaymentTypeNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PaymentTypeCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<PaymentTypeInfo> lpaymenttypeInfo = JsonConvert.DeserializeObject<List<PaymentTypeInfo>>(respstr);

            ddlPOPaymentType.DataSource = lpaymenttypeInfo;
            ddlPOPaymentType.DataTextField = "PaymentTypeName";
            ddlPOPaymentType.DataValueField = "PaymentTypeCode";
            ddlPOPaymentType.DataBind();
            ddlPOPaymentType.Items.Insert(0, new ListItem("กรุณาเลือก-------------------------------", "-99"));            
        }
        protected void loadTotal()
        {
            Double? sumprice = 0;
            Double? billdiscount = 0;
            Double? netprice = 0;
            Double? vat = 0;
            Double? totalprice = 0;

            foreach (var lpV in L_productlist.ToList())
            {
                sumprice += lpV.SumPrice;                
            }

            totaltext.Text = string.Format("{0:n}", (sumprice));
            billdiscount = (txtBillDiscount.Text != "") ? Convert.ToDouble(txtBillDiscount.Text) : 0;

            netprice = sumprice - billdiscount;
            vat = netprice * 7 / 100;
            totalprice = netprice + vat;

            txtBillDiscount.Text = string.Format("{0:n}", (billdiscount));
            txtvat.Text = string.Format("{0:n}", (vat));
            txtTotalPrice.Text = string.Format("{0:n}", (totalprice));
        }
        protected void loadTotalfromPOItemList(List<POItemInfo> lpoitemInfo)
        {
            Double? sumprice = 0;
            Double? billdiscount = 0;
            Double? netprice = 0;
            Double? vat = 0;
            Double? totalprice = 0;

            foreach (var lpV in lpoitemInfo.ToList())
            {
                sumprice += lpV.TotPrice;
                billdiscount = lpV.DiscountBill;
            }

            totaltext.Text = string.Format("{0:n}", (sumprice));
            

            netprice = sumprice - billdiscount;
            vat = netprice * 7 / 100;
            totalprice = netprice + vat;

            txtBillDiscount.Text = string.Format("{0:n}", (billdiscount));
            txtvat.Text = string.Format("{0:n}", (vat));
            txtTotalPrice.Text = string.Format("{0:n}", (totalprice));
        }
        protected void txtBillDiscount_TextChanged(object sender, EventArgs e)
        {
            Double? billdiscount = 0;

            billdiscount = (txtBillDiscount.Text != "") ? Convert.ToDouble(txtBillDiscount.Text) : 0;
            txtBillDiscount.Text = string.Format("{0:n}", (billdiscount));

            loadTotal();
        }
        protected Boolean ValidateInsertUpdatePO()
        {
            Boolean flag = true;
            string error = "";
            int counterr = 0;

            if (hidFlagInsert.Value == "True")
            {
                if (L_productlist.Count == 0)
                {
                    flag = false;
                    error += "กรุณาเลือกสินค้า \\n";
                    counterr++;
                }
                else
                {
                    flag = (!flag) ? false : true;
                }
            }
            else // From Update
            {

            }
            if (txtPODate.Text == "")
            {
                flag = false;
                lblPODate.Text = "กรุณาระบุ PO Date";
                
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }
            if (txtSupplierName.Text == "")
            {
                flag = false;
                lblSupplierNameIns.Text = "กรุณาระบุ ชื่อผู้ขาย ";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }
            if (txtPOExpectDate.Text == "")
            {
                flag = false;
                lblPOExpectDate.Text = "กรุณาระบุ วันที่คาดว่าจะส่งสินค้า";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }
            if (ddlPOPaymentType.SelectedValue == "-99")
            {
                flag = false;
                lblPOPaymentType.Text = "กรุณาระบุ วิธีชำระเงิน";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }
            if (txtPOCredit.Text == "")
            {
                flag = false;
                lblPOCredit.Text = "กรุณาระบุ เครดิต(วัน)";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }
            if (txtInventoryName.Text == "")
            {
                flag = false;
                lblInventoryName.Text = "สถานที่จัดส่งสินค้า";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');displayPayment();", true);
            }
            return flag;
        }

        protected List<POInfo> GetPOMaster()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPONopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<POInfo> lpoInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);
            return lpoInfo;
        }
        protected List<SupplierInfo> GetPOMapSupplier()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOMapSupplier";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lsupInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);
            return lsupInfo;
        }
        protected List<InventoryInfo> GetPOmapInventory()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/POMapInventoryNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> linvInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);
            return linvInfo;
        }
        protected List<POItemInfo> GetPOItem()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOItemNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<POItemInfo> lpoitemInfo = JsonConvert.DeserializeObject<List<POItemInfo>>(respstr);
            return lpoitemInfo;
        }
        #endregion

        #region binding
        protected List<SupplierInfo> GetSupplierMasterNoPagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSupplierNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["SupplierCode"] = txtSearchSupplierCode.Text.Trim();
                data["SupplierName"] = txtSearchSupplierName.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lsupInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);
            return lsupInfo;
        }
        protected List<InventoryInfo> GetInventoryMasterNoPagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = txtSearchInventoryCode.Text.Trim();
                data["InventoryName"] = txtSearchInventoryName.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> linvInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);
            return linvInfo;
        }
        protected List<ProductInfo> GetProductMasterNoPagingCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = txtSearchProductCode.Text.Trim();
                data["ProductName"] = txtSearchProductName.Text.Trim();
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lproductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);
            return lproductInfo;
        }
        protected List<POItemInfo> GetProductMasterSelectedNoPagingCriteria(String productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = productcode;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<POItemInfo> lproductInfo = JsonConvert.DeserializeObject<List<POItemInfo>>(respstr);
            return lproductInfo;
        }
        #endregion
    }
}
using APPCOREMODEL.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;
using System.Security.Claims;
using System.Configuration;
using System.Web.Helpers;
using System.IO;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class CustomerController : ApiController
    {
        protected static string appUrl = ConfigurationManager.AppSettings["appUrl"];



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerListPagingByCriteria")]
        public IHttpActionResult ListCustomerListPagingByCriteria([FromBody] CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.ListCustomerListPagingByCriteria(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCustomerListPagingByCriteria")]
        public IHttpActionResult CountCustomerListPagingByCriteria([FromBody] CustomerInfo cInfo)
        {
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.CountCustomerListPagingByCriteria(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerByCriteria")]
        public IHttpActionResult ListCusByCriteria([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.ListCustomerByCriteria(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerMapCusPhoneByCriteria")]
        public IHttpActionResult ListCustomerMapCusPhoneByCriteria([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.ListCustomerMapCusPhoneByCriteria(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerNoPagingByCriteria")]
        public IHttpActionResult ListCustomerNoPagingByCriteria([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.ListCustomerNoPagingByCriteria(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerDetailByCriteria")]
        public IHttpActionResult ListCusDetailByCriteria([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.ListCustomerDetailByCriteria(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerByCriteria_showgv")]
        public IHttpActionResult ListCusByCriteria_showgv([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.ListCustomerByCriteria_showgv(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CustomerCheck")]
        public IHttpActionResult CustomerChk([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.CustomerCheck(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCustomerListByCriteria")]
        public IHttpActionResult CountCusListByCriteria([FromBody]CustomerInfo cInfo)
        {
            //CustomerListReturn customerList = new CustomerListReturn();
            //List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.CountCustomerListByCriteria(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCustomerofOMS")]
        public IHttpActionResult InsCustomer([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int i = 0;
            i = cDAO.InsertCustomer(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CustomerAPI")]
        public IHttpActionResult InsertCus([FromBody]CustomerInfo cInfo)
        {
            List<CustomerListReturn> lCusListReturn = new List<CustomerListReturn>();
            List<CustomerListOneAppReturn> CusreturnOneApp = new List<CustomerListOneAppReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            String customercode = "";
            String contacttel = "";
            String uniqueid = "";
            String merchantCode = "";
            String address = "";
            string callinfoid = "";

            if (cInfo.UniqueID == "" || cInfo.UniqueID == null || cInfo.RefCode == "" || cInfo.RefCode == null || cInfo.Channel != "01" || cInfo.BrandNo == "" || cInfo.BrandNo == null || cInfo.ContactTel == "" || cInfo.ContactTel == null)
            {
                CusreturnOneApp.Add(new CustomerListOneAppReturn() { order_url = "No result data", result_code = "101", result_message = "Data not found" });               
            }
            else // return Massege Incompleted Data No UniqueID //
            {
                lCusListReturn = cDAO.CheckUniqueIDofCustomer(cInfo.UniqueID);

                if (lCusListReturn.Count > 0)
                {
                    String a = cDAO.UpdateCusfromOneApp(cInfo);
                    customercode = lCusListReturn[0].CustomerCode;
                    contacttel = cInfo.ContactTel;
                    uniqueid = lCusListReturn[0].UniqueID;
                    //address = lCusListReturn[0].address1 + " " + lCusListReturn[0].address2;
                }
                else
                {
                    customercode = cDAO.InsertCusfromOneApp(cInfo);
                    contacttel = cInfo.ContactTel;
                    uniqueid = cInfo.UniqueID;
                }

                merchantCode = cInfo.MerchantCode;
                callinfoid = cInfo.CallInfoID;

                CustomerAddressInfo caInfo = new CustomerAddressInfo();
                List<CustomerAddressListReturn> lcaInfo = new List<CustomerAddressListReturn>();
                CustomerAddressDAO caDAO = new CustomerAddressDAO();

                caInfo.CustomerCode = customercode;
                caInfo.Address = cInfo.address1 + " " + cInfo.address2;
                //caInfo.Address2 = cInfo.address2;
                caInfo.Province = cInfo.province;
                caInfo.District = cInfo.district;
                caInfo.Subdistrict = cInfo.subdistrict;
                caInfo.ZipCode = cInfo.postcode;
                caInfo.FlagActive = "Y";
                caInfo.AddressType = "01";

                lcaInfo = caDAO.GetLatestUpdatedCustomerAddress(caInfo);

                if (lcaInfo.Count > 0)
                {
                    caDAO.UpdateCustomerAddressDateByAPI(caInfo);
                    address = lcaInfo[0].Address + " " + " แขวง" + lcaInfo[0].SubdistrictName + " เขต" + lcaInfo[0].DistrictName + " " + lcaInfo[0].ProvinceName + " " + lcaInfo[0].ZipCode;
                }
                else
                {
                    caDAO.InsertCustomerAddress(caInfo);
                    lcaInfo = caDAO.GetLatestUpdatedCustomerAddress(caInfo);
                    address = lcaInfo[0].Address + " แขวง" + lcaInfo[0].SubdistrictName + " เขต" + lcaInfo[0].DistrictName + " " + lcaInfo[0].ProvinceName + " " + lcaInfo[0].ZipCode;
                }

                CustomerPhoneInfo cpInfo = new CustomerPhoneInfo();
                List<CustomerPhoneListReturn> lcpInfo = new List<CustomerPhoneListReturn>();
                CustomerPhoneDAO cpDAO = new CustomerPhoneDAO();

                lcpInfo = cpDAO.ValidateCusPhoneInfo(cInfo.ContactTel);

                if (lcpInfo.Count > 0)
                {
                    cpInfo.CustomerPhone = cInfo.ContactTel;
                    cpInfo.CustomerCode = customercode;

                    int i = cpDAO.UpdateCustomerPhonefromOneApp(cpInfo);
                }
                else
                {
                    cpInfo.CustomerPhone = cInfo.ContactTel;
                    cpInfo.CustomerCode = customercode;
                    cpInfo.CustomerPhoneType = "01";
                    cpInfo.CreateBy = cInfo.RefCode;
                    cpInfo.UpdateBy = cInfo.RefCode;
                    cpInfo.FlagDelete = "N";

                    int i = cpDAO.InsertCustomerPhone(cpInfo);
                }                
            }

            if(customercode != "")
            {
                //CusreturnOneApp.Add(new CustomerListOneAppReturn() { order_url = "http://doublep.dlinkddns.com:2701/src/TakeOrderMK/TakeOrder.aspx?UniqueID=" + uniqueid + "&CustomerCode=" + customercode + "&RefCode=" + cInfo.RefCode + "&CustomerPhone=" + contacttel + "&BrandNo=" + cInfo.BrandNo, result_code = "200", result_message = "Create success", unique_id = cInfo.UniqueID });
                //CusreturnOneApp.Add(new CustomerListOneAppReturn() { order_url = appUrl + "/src/TakeOrderRetail/TakeOrder.aspx?UniqueID=" + uniqueid + "&CustomerCode=" + customercode + "&RefCode=" + cInfo.RefCode + "&CustomerPhone=" + contacttel + "&BrandNo=" + cInfo.BrandNo + "&MerchantCode=" + merchantCode + "&Username="+ username, result_code = "200", result_message = "Create success", unique_id = cInfo.UniqueID, address = address });
                CusreturnOneApp.Add(new CustomerListOneAppReturn() { order_url = appUrl + "/src/TakeOrderRetail/TakeOrder.aspx?UniqueID=" + uniqueid + "&CustomerCode=" + customercode + "&RefCode=" + cInfo.RefCode +"&Firstname="+ cInfo.CustomerFName + "&Lastname=" + cInfo.CustomerLName + "&CustomerPhone=" + contacttel + "&CalllnNumber=" + contacttel + "&BrandNo=" + cInfo.BrandNo + "&MerchantCode=" + merchantCode + "&Refusername=" + cInfo.RefUsername + "&MediaPhone=" + cInfo.MediaPhone + "&CallInfoID=" + callinfoid + "&MerchantCustomer=" + cInfo.MerchantCustomer, result_code = "200", result_message = "Create success", unique_id = cInfo.UniqueID, customercode = customercode, address = address });


            }
            String CustomerMessageReturn = CusreturnOneApp[0].order_url;
            return Ok<List<CustomerListOneAppReturn>>(CusreturnOneApp);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCustomerPhoneByCustomerInfo")]
        public IHttpActionResult InsertCusPhoneByCustomerInfo([FromBody]CustomerInfo cInfo)
        {
            //CustomerListReturn customerList = new CustomerListReturn();
            //List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int i = 0;
            i = cDAO.InsertCustomerPhoneByCustomerInfo(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomer")]
        public IHttpActionResult UpdCustomer([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.UpdateCustomer(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerNoteProfile")]
        public IHttpActionResult UpdCustomerNote([FromBody]CustomerInfo cInfo)
        {
            //CustomerListReturn customerList = new CustomerListReturn();
            //List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerNoteProfile(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerPhoneByCustomerInfo")]
        public IHttpActionResult UpdCustomerPhoneByCustomerInfo([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerPhoneByCustomerInfo(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerPhoneByCustomer")]
        public IHttpActionResult UpdCustomerPhoneByCustomer([FromBody] CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerPhoneByCustomer(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCustomer")]
        public IHttpActionResult DelCustomer([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int i = 0;
            i = cDAO.DeleteCustomer(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CustomerCodeValidation")]
        public IHttpActionResult CusCodeValidate([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO mDAO = new CustomerDAO();

            listCustomer = mDAO.CustomerCodeValidation(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerByCriteriaMaster")]
        public IHttpActionResult ListCusByCriteriaMaster([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.ListCustomerByCriteriaMaster(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCustomerListByCriteriaMaster")]
        public IHttpActionResult CountCusListByCriteriaMaster([FromBody]CustomerInfo cInfo)
        {
            //CustomerListReturn customerList = new CustomerListReturn();
            //List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.CountCustomerListByCriteriaMaster(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCustomerPhone")]
        public IHttpActionResult CountCustomerPhone([FromBody]CustomerInfo cInfo)
        {
            //CustomerListReturn customerList = new CustomerListReturn();
            //List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.CountCustomerPhone(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerTaxId")]
        public IHttpActionResult UpdateCustomerTaxId([FromBody]CustomerInfo cInfo)
        {
            //CustomerListReturn customerList = new CustomerListReturn();
            //List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerTaxId(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertUpdateCustomerTIBAPI")]
        public IHttpActionResult InsertUpdateCustomerTIBAPI([FromBody] CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();

            CustomerInfo cuscheckcustomercode = new CustomerInfo();
            cuscheckcustomercode.CustomerCode = cInfo.CustomerCode;

            CustomerDAO cDAO = new CustomerDAO();
            listCustomer = cDAO.ListCustomerNoPagingByCriteria(cuscheckcustomercode);

            int? i = 0;

            if (listCustomer.Count > 0) // Update Customer
            {
                i = cDAO.UpdateCustomer(cInfo);
            }
            else // Insert Customer
            {
                cInfo.FlagDelete = "N";

                i = cDAO.InsertCustomerofOMS(cInfo);
            }

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertUpdateUserPasswordLogInCustomerTIBAPI")]
        public IHttpActionResult InsertUpdateUserPasswordLogInCustomerTIBAPI([FromBody] CustomerInfo cInfo)
        {
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerUserLogin(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerMailAPI")]
        public IHttpActionResult UpdateCustomerMailAPI([FromBody] CustomerInfo cInfo)
        {
            CustomerDAO cDAO = new CustomerDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerMail(cInfo);

            return Ok<int?>(i);
        }




        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/EmailAuthenticationEcommerce")]
        public IHttpActionResult EmailAuthenticationEcommerce([FromBody] CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO mDAO = new CustomerDAO();

            listCustomer = mDAO.EmailAuthenticationEcommerce(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }
    }
}

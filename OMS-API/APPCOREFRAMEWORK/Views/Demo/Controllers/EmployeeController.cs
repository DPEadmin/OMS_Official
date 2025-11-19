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
    public class EmployeeController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmp")]
        public IHttpActionResult LstEmp([FromBody]EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmp(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpByCriteria")]
        public IHttpActionResult LstEmpByCriteria([FromBody]EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpByCriteria(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpValidateInsert")]
        public IHttpActionResult ListEmpValidateInsert([FromBody]EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpValidateInsert(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetLogin")]
        public IHttpActionResult GetLogIn([FromBody]EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.GetLogin(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetLoginTakeOrder")]
        public IHttpActionResult GetLoginTakeOrder([FromBody]EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.GetLoginTakeOrder(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetEmpByUnitCode")]
        public IHttpActionResult GetEmpByUnitcode([FromBody]EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.GetEmpByUnitCode(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListUserLoginByCriteria")]
        public IHttpActionResult ListUserLogInByCriteria([FromBody]UserLoginInfo eInfo)
        {
            UserLoginListReturn EmployeeList = new UserLoginListReturn();
            List<UserLoginListReturn> listEmployee = new List<UserLoginListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListUserLoginByCriteria(eInfo);

            return Ok<List<UserLoginListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpRoleByCriteria")]
        public IHttpActionResult ListErByCriteria([FromBody]EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpRoleByCriteria(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpBranchByCriteria")]
        public IHttpActionResult ListEmpBranchByCriteria([FromBody]EmpBranch eInfo)
        {
            EmpBranchListReturn EmployeeList = new EmpBranchListReturn();
            List<EmpBranchListReturn> listEmployee = new List<EmpBranchListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpBranchByCriteria(eInfo);

            return Ok<List<EmpBranchListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpBranchNoPagingByCriteria")]
        public IHttpActionResult LstEmpBranchNoPagingByCriteria([FromBody]EmpBranch eInfo)
        {
            EmpBranchListReturn EmployeeList = new EmpBranchListReturn();
            List<EmpBranchListReturn> listEmployee = new List<EmpBranchListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpBranchNoPagingByCriteria(eInfo);

            return Ok<List<EmpBranchListReturn>>(listEmployee);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoleNotInEmpRoleByCriteria")]
        public IHttpActionResult ListRoleNotInEmpRoleByCriteria([FromBody]EmpRole eInfo)
        {
            RoleListReturn EmployeeList = new RoleListReturn();
            List<RoleListReturn> listEmployee = new List<RoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListRoleNotInEmpRoleByCriteria(eInfo);

            return Ok<List<RoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpRoleNoPagingByCriteria")]
        public IHttpActionResult ListEmpRoleNoPagingByCriteria([FromBody]EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpRoleNoPagingByCriteria(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteEmpInventory")]
        public IHttpActionResult DelEmpInventory([FromBody]EmpInventoryInfo eInfo)
        {
           
            EmployeeDAO cDAO = new EmployeeDAO();
            int i = 0;
            i = cDAO.DeleteEmpInventory(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpRoleByCriteria_01")]
        public IHttpActionResult ListErByCriteria_01([FromBody]EmpRole Info)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpRoleByCriteria_01(Info);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpInventoryByCriteria")]
        public IHttpActionResult ListEmpInventoryByCriteria([FromBody]EmpInventoryInfo eInfo)
        {

            EmpInventoryListReturn EmployeeList = new EmpInventoryListReturn();
            List<EmpInventoryListReturn> listEmployee = new List<EmpInventoryListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpInventoryByCriteria(eInfo);

            return Ok<List<EmpInventoryListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/RoleCodeValidate")]
        public IHttpActionResult RoleCodeValid([FromBody]EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.RoleCodeValidate(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/EmpInventoryValidateInsert")]
        public IHttpActionResult EmpInventoryValidateIns([FromBody]EmpInventoryInfo eInfo)
        {

            EmpInventoryListReturn EmployeeList = new EmpInventoryListReturn();
            List<EmpInventoryListReturn> listEmployee = new List<EmpInventoryListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.EmpInventoryValidateInsert(eInfo);

            return Ok<List<EmpInventoryListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/BindDdlNotInbyCriteria")]
        public IHttpActionResult BindDdlNotInByCriteria([FromBody]EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.BindDdlNotInbyCriteria(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/BindDdlbyCriteria")]
        public IHttpActionResult BindDdlByCriteria([FromBody]EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.BindDdlbyCriteria(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/BindDdlInventorybyCriteria")]
        public IHttpActionResult BindDdlInvenbyCriteria([FromBody]InventoryInfo eInfo)
        {
            InventoryListReturn EmployeeList = new InventoryListReturn();
            List<InventoryListReturn> listEmployee = new List<InventoryListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.BindDdlInventorybyCriteria(eInfo);

            return Ok<List<InventoryListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/BindDdlUpdatebyCriteria")]
        public IHttpActionResult BindDdlUpdbyCriteria([FromBody]EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.BindDdlUpdatebyCriteria(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/BindDdlPlusbyCriteria")]
        public IHttpActionResult BndDdlPlusbyCriteria([FromBody]EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.BindDdlPlusbyCriteria(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountEmployeeListByCriteria")]
        public IHttpActionResult CountEmpListByCriteria([FromBody]EmpInfo eInfo)
        {
            int? i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.CountEmployeeListByCriteria(eInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountUserLoginByCriteria")]
        public IHttpActionResult CountUserLogInByCriteria([FromBody]UserLoginInfo eInfo)
        {
            int? i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.CountUserLoginByCriteria(eInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountEmpRoleByCriteria")]
        public IHttpActionResult CountEmpRoleByCriteria([FromBody]EmpRole eInfo)
        {
            int? i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.CountEmpRoleByCriteria(eInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountEmpInventoryByCriteria")]
        public IHttpActionResult CountEmpInventoryByCriteria([FromBody]EmpInventoryInfo eInfo)
        {
            int? i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.CountEmpInventoryByCriteria(eInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertEmployee")]
        public IHttpActionResult InsEmployee([FromBody]EmpInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.InsertEmployee(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertEmpRole")]
        public IHttpActionResult InsEmpRole([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.InsertEmpRole(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertUserLogin")]
        public IHttpActionResult InsUserLogin([FromBody]UserLoginInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.InsertUserLogin(eInfo);
            if (i > 0) 
            {
                i = cDAO.InsertUserLogin_LC(eInfo);
            }

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteEmpRole")]
        public IHttpActionResult DelEmpRole([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.DeleteEmpRole(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteEmpRoleByRoleCodeList")]
        public IHttpActionResult DeleteEmpRoleByRoleCodeList([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.DeleteEmpRoleByRoleCodeList(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteEmpRoleList")]
        public IHttpActionResult DelEmpRoleList([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.DeleteEmpRoleList(eInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteUserlogin")]
        public IHttpActionResult DelUserlogin([FromBody]UserLoginInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.DeleteUserlogin(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateEmployee")]
        public IHttpActionResult UpdateEmployee([FromBody]EmpInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateEmployee(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCreateRefCodefromOneApp")]
        public IHttpActionResult UpdateCreateRefCodefromOneApptoOMSEmp([FromBody]EmpInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateCreateRefCodefromOneApp(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateEmpRole")]
        public IHttpActionResult UpdEmpRole([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateEmpRole(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateEmpMapRole")]
        public IHttpActionResult UpdEmpMapRole([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateEmpMapRole(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertEmpMapRole")]
        public IHttpActionResult InsEmpMapRole([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.InsertEmpMapRole(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateEmpInventory")]
        public IHttpActionResult UpdEmpInventory([FromBody]EmpInventoryInfo eInfo)
        {
            int? i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateEmpInventory(eInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertEmpInventory")]
        public IHttpActionResult InsEmpInventory([FromBody]EmpInventoryInfo eInfo)
        {
            int? i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.InsertEmpInventory(eInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteEmployeeList")]
        public IHttpActionResult DeleteEmployeeList([FromBody]EmpInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();
            RoleDAO rDAO = new RoleDAO();
            
            i = cDAO.DeleteEmployeeList(eInfo);
            i = rDAO.DeleteEmpRole(eInfo);
            i = rDAO.DeleteUserlogin(eInfo);
            i= rDAO.DeleteMerchantMapUser(eInfo);
            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteEmployee")]
        public IHttpActionResult DelEmployee([FromBody]EmpInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.DeleteEmployee(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertWFMapEmpRole")]
        public IHttpActionResult InsWFMapEmpRole([FromBody]EmpRole_EmpIdModel eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.InsertWFMapEmpRole(eInfo.ERole, eInfo.EmpId);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateWFMapEmpRole")]
        public IHttpActionResult UpdWFMapEmpRole([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateWFMapEmpRole(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteWFMapEmpRole")]
        public IHttpActionResult DelWFMapEmpRole([FromBody]EmpRole eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.DeleteWFMapEmpRole(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateUserLogin")]
        public IHttpActionResult UpdUserLogin([FromBody]UserLoginInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateUserLogin(eInfo);
            if (i > 0) 
            {
                i = cDAO.UpdateUserLogin_LC(eInfo);
            }

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateEmpCodefromRefCodefromOneApp")]
        public IHttpActionResult UpdateEmpCodefromRefCodefromOneApptoOMSUserLogin([FromBody]UserLoginInfo eInfo)
        {
            int i = 0;
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.UpdateEmpCodefromRefCodefromOneApp(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/EmployeeAPI")]
        public IHttpActionResult InsertEmpfromOneApp([FromBody]EmpInfo eInfo)
        {
            List<EmpListReturn> leInfo = new List<EmpListReturn>();
            List<EmpListOneAppReturn> empListReturnOneApp = new List<EmpListOneAppReturn>();
            EmployeeDAO eDAO = new EmployeeDAO();
            String empcode = "";

            if (eInfo.RefCode != "" || eInfo.RefCode != null)
            {
                leInfo = eDAO.CheckRefCodeofEmployee(eInfo.RefCode);

                if(leInfo.Count > 0)
                {
                    String a = eDAO.UpdateEmpfromOneApp(eInfo);
                    empcode = leInfo[0].RefCode;
                }
                else
                {
                    empcode = eDAO.InsertEmpfromOneApp(eInfo);
                }
            }
            else // RefCode incompleted from OneApp
            {
                empListReturnOneApp.Add(new EmpListOneAppReturn() { result_data = "No result data", result_code = "101", result_message = "Data not found" });
            }

            if(empcode != "")
            {
                empListReturnOneApp.Add(new EmpListOneAppReturn() { result_data = "http://10.10.22.30/TakeOrder.aspx?UniqueID=" + eInfo.RefCode + "&EmpCode=" + empcode, result_code = "200", result_message = "Create success" });
            }

            String EmpMessageReturn = empListReturnOneApp[0].result_data;

            return Ok<List<EmpListOneAppReturn>>(empListReturnOneApp);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/EmpCodeValidate")]
        public IHttpActionResult EmpCodeValid([FromBody]EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO eDAO = new EmployeeDAO();

            listEmployee = eDAO.EmpCodeValidate(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertEmployeewithRefCode")]
        public IHttpActionResult InsEmployeewithRefCode([FromBody]EmpInfo eInfo)
        {
            String i = "";
            EmployeeDAO cDAO = new EmployeeDAO();

            i = cDAO.InsertEmployeewithRefCode(eInfo);

            return Ok<String>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpByCriteriaUserDetail")]
        public IHttpActionResult ListEmpByCriteriaUserDatail([FromBody] EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpByCriteriaUserDatail(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpMapBuNoPaging")]
        public IHttpActionResult ListEmpMapBuNoPaging([FromBody] EmpMapBuInfo eInfo)
        {
            EmpMapBuListReturn empmapbu = new EmpMapBuListReturn();
            List<EmpMapBuListReturn> lempmapbu = new List<EmpMapBuListReturn>();
            EmpMapBUDAO eDAO = new EmpMapBUDAO();

            lempmapbu = eDAO.ListEmpMapBuNoPaging(eInfo);

            return Ok<List<EmpMapBuListReturn>>(lempmapbu);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetMaxLevelsEmpMapBu")]
        public IHttpActionResult GetMaxLevelsEmpMapBu([FromBody] EmpMapBuInfo eInfo)
        {
            EmpMapBuListReturn empmapbu = new EmpMapBuListReturn();
            List<EmpMapBuListReturn> lempmapbu = new List<EmpMapBuListReturn>();
            EmpMapBUDAO eDAO = new EmpMapBUDAO();

            lempmapbu = eDAO.GetMaxLevelsEmpMapBu(eInfo);

            return Ok<List<EmpMapBuListReturn>>(lempmapbu);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpMapBuPaging")]
        public IHttpActionResult ListEmpMapBuPaging([FromBody] EmpMapBuInfo eInfo)
        {
            EmpMapBuListReturn empmapbu = new EmpMapBuListReturn();
            List<EmpMapBuListReturn> lempmapbu = new List<EmpMapBuListReturn>();
            EmpMapBUDAO eDAO = new EmpMapBUDAO();

            lempmapbu = eDAO.ListEmpMapBuPaging(eInfo);

            return Ok<List<EmpMapBuListReturn>>(lempmapbu);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountEmpMapBuPaging")]
        public IHttpActionResult CountEmpMapBuPaging([FromBody] EmpMapBuInfo iInfo)
        {
            EmpMapBUDAO iDAO = new EmpMapBUDAO();
            int? i = 0;
            i = iDAO.CountEmpMapBuPaging(iInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpNoPagingByCriteria")]
        public IHttpActionResult ListEmpNoPagingByCriteria([FromBody] EmpInfo eInfo)
        {
            EmpListReturn EmployeeList = new EmpListReturn();
            List<EmpListReturn> listEmployee = new List<EmpListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpNoPagingByCriteria(eInfo);

            return Ok<List<EmpListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpRoleNoPagingforDDLByCriteria")]
        public IHttpActionResult ListEmpRoleNoPagingforDDLByCriteria([FromBody] EmpRole eInfo)
        {
            EmpRoleListReturn EmployeeList = new EmpRoleListReturn();
            List<EmpRoleListReturn> listEmployee = new List<EmpRoleListReturn>();
            EmployeeDAO cDAO = new EmployeeDAO();

            listEmployee = cDAO.ListEmpRoleNoPagingforDDLByCriteria(eInfo);

            return Ok<List<EmpRoleListReturn>>(listEmployee);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertEmpMapBu")]
        public IHttpActionResult InsertEmpMapBu([FromBody] EmpMapBuInfo eInfo)
        {
            EmpMapBUDAO eDAO = new EmpMapBUDAO();
            int i = 0;
            i = eDAO.InsertEmpMapBu(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateEmpMapBu")]
        public IHttpActionResult UpdateInven([FromBody] EmpMapBuInfo eInfo)
        {
            EmpMapBUDAO eDAO = new EmpMapBUDAO();
            int i = 0;
            i = eDAO.UpdateEmpMapBu(eInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListEmpMapRoleValidateDuplicateInsert")]
        public IHttpActionResult ListEmpMapRoleValidateDuplicateInsert([FromBody] EmpMapBuInfo eInfo)
        {
            EmpMapBuListReturn List = new EmpMapBuListReturn();
            List<EmpMapBuListReturn> listEmpMapBu = new List<EmpMapBuListReturn>();
            EmpMapBUDAO eDAO = new EmpMapBUDAO();

            listEmpMapBu = eDAO.ListEmpMapRoleValidateDuplicateInsert(eInfo);

            return Ok<List<EmpMapBuListReturn>>(listEmpMapBu);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteEmpMapBu")]
        public IHttpActionResult DeleteEmpMapBu([FromBody] EmpMapBuInfo eInfo)
        {
            EmpMapBUDAO eDAO = new EmpMapBUDAO();
            int i = 0;
            i = eDAO.DeleteEmpMapBu(eInfo);

            return Ok<int>(i);
        }
    }
  
}

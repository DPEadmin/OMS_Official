using APPCOREMODEL.Datas.Authens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPCOREMODEL.DAO;
using APPCOREMODEL.Datas;

namespace APPCOREMODEL.Services.authens
{
    public class AuthenicatedServices
    {
        public AuthenResult authenicated(string username,string password)
        {
            // return null;

            AuthenResult result = new AuthenResult();
            EmpLoginn lLogin = new EmpLoginn();
     
            lLogin.username = username;
            lLogin.password = password;
            List<Emp> lEMP = new List<Emp>();
            MenuMobileDAO Menumobile = new MenuMobileDAO();
            lEMP = Menumobile.GetEmpLogin(lLogin);
            foreach (var EmpV in lEMP)
            {
                result.Empployee.Add(new Emp() { EmpUsername = EmpV.EmpUsername, EmpPassword = EmpV.EmpPassword, EmpVendor = EmpV.EmpVendor, EmpRole = EmpV.EmpRole,EmpCode=EmpV.EmpCode });
               
            }
            return result;
        }
        
    }
}

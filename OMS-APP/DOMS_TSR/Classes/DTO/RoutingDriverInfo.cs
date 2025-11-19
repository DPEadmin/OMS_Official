using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class RoutingDriverInfo
    {
        public int? RoutingDriverId { get; set; }
        public String Routing_code { get; set; }
        public String FName { get; set; }
        public String LName { get; set; }
        public String Driver_no { get; set; }
        public String FlagDelete { get; set; }
        public String RoleCode { get; set; }
        public int? countRoutingDriver{ get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String RoleName { get; set; }
    }
}
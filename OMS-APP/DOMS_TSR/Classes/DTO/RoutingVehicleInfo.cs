using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class RoutingVehicleInfo
    {
        public int? RoutingVechicleId { get; set; }
        public String Routing_code { get; set; }
        public String Vechicle_No { get; set; }
        public String Band_Name { get; set; }
        public String TypeCar_Name { get; set; }
        public String name_Routing { get; set; }
        public String FlagDelete { get; set; }
        public int? countRoutingVehicle { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Vechicle_Active { get; set; }
        public String Vechicle_Model { get; set; }
    }
}
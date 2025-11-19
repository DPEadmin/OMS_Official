using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class RoutingMapDriverInfo
    {
        public int? RoutingDriverId { get; set; }
        public String Routing_code { get; set; }
        public String FName { get; set; }
        public String LName { get; set; }
        public String Driver_no { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String RoleCode { get; set; }
            public String RoleName { get; set; }
    }

    public class RoutingMapDriverListReturn
    {
        public int? RoutingDriverId { get; set; }
        public String Routing_code { get; set; }
        public String Driver_no { get; set; }
        public String FName { get; set; }
        public String LName { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countRoutingDriver { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Vechicle_Active { get; set; }
        public String Vechicle_Model { get; set; }
        public String RoleCode { get; set; }
        public String RoleName { get; set; }
    }

    public class RoutingMapVehicleInfo
    {
        public int? RoutingVechicleId { get; set; }
        public String Routing_code { get; set; }
        public String Vechicle_No { get; set; }
        public String Vechicle_Lookup { get; set; }
        public String Vechicle_Band { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String Vechicle_Active { get; set; }
        public String Vechicle_Model { get; set; }
    }

    public class RoutingMapVehicleListReturn
    {
        public int? RoutingVechicleId { get; set; }
        public String Routing_code { get; set; }
        public String name_Routing { get; set; }
        public String Vechicle_No { get; set; }
        public String Band_Name { get; set; }
        public String TypeCar_Name { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countRoutingVechicle { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Vechicle_Active { get; set; }
        public String Vechicle_Model { get; set; }
    }
    public class RoutingMapInventoryInfo
    {
        public int? RoutinginventoryId { get; set; }
        public String Routing_code { get; set; }
        public String Inventory_Code { get; set; }
        
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
    }
    public class RoutingMapInventoryDetailInfo
    {
        public int? RoutinginventoryId { get; set; }
        public String Routing_code { get; set; }
        public String Inventory_Code { get; set; }
        public String Inventory_name { get; set; }
        public String Routing_name { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countRoutingInventory { get; set; }

    }
}

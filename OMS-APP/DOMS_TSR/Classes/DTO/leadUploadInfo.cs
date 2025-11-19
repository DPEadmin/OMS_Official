using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class LeadUploadInfo
    {
        public int? id { get; set; }
        public String Lead_id { get; set; }
        public String Lot { get; set; }
        public String Agent_Name { get; set; }
        public String Customer_Name { get; set; }
        public String Telephone_No { get; set; }
        public String Product_Name { get; set; }
        public String FlagDelete { get; set; }    
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        
        public int? countLeadUpload { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}

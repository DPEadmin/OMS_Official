using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class ThaiPostInfo
    {
   
        public String expire { get; set; }
        public String token { get; set; }
        public String OrderCode { get; set; }

    }

    public class LThaiPostInfo
    {
        public String status { get; set; }
        public String language { get; set; }
        public Array barcode { get; set; }
    }
    public class EG327058400TH
    {
        public string barcode { get; set; }
        public string status { get; set; }
        public string status_description { get; set; }
        public string status_date { get; set; }
        public string location { get; set; }
        public string postcode { get; set; }
        public string delivery_status { get; set; }
        public string delivery_description { get; set; }
        public string delivery_datetime { get; set; }
        public string receiver_name { get; set; }
        public string signature { get; set; }
    }

    public class Items
    {
        public IList<EG327058400TH> EG327058400TH { get; set; }
    }

    public class TrackCount
    {
        public string track_date { get; set; }
        public int count_number { get; set; }
        public int track_count_limit { get; set; }
    }

    public class Response
    {
        public Items items { get; set; }
        public TrackCount track_count { get; set; }
    }

    public class thaipost
    {
        public Response response { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }

}

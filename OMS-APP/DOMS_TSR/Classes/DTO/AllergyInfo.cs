using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class AllergyInfo
    {
        public int? AllergyId { get; set; }
        public String AllergyCode { get; set; }
        public String AllergyName { get; set; }
        public String AllergyImageUrl { get; set; }
        public String AllergyImageName { get; set; }
        public String FlagDelete { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countAllergy { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

    }
}

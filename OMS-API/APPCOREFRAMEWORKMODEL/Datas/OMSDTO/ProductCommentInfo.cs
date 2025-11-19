using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ProductCommentInfo
    {
        public String ProductCode { get; set; }
        public String CustomerCode { get; set; }
        public String ProductComment { get; set; }
        public String FlagDelete { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public int Rating { get; set; }

    }
    public class ProductCommentRatingInfo
    {
        public String ProductCode { get; set; }
        public int Rating { get; set; }
        public String Active { get; set; } 
        public String ProductComment { get; set; }
        public String FlagDelete { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CustomerCode { get; set; }
        public String CommentId { get; set; }

         
    }

}


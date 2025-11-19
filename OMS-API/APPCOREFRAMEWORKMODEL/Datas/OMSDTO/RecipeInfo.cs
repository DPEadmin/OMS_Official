using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
  
    public class RecipeInfo
    {
        public int? RecipeId { get; set; }
        public String RecipeIdDelete { get; set; }
        public String RecipeCode { get; set; }
        public String RecipeName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countRecipe { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }

    }

    public class RecipeListReturn
    {
        public int? RecipeId { get; set; }
        public String RecipeIdDelete { get; set; }
        public String RecipeCode { get; set; }
        public String RecipeName { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public String RecipeCodeCheck { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int? countRecipe { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public string FlagDelete { get; set; }
     
    }
 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class L_ProductMapRecipeInfodata
    {
        public List<ProductMapRecipeInfo> ProductRecipeInfo { get; set; }
        public int? countProductMapRecipe { get; set; }
    }
    public class ProductMapRecipeInfo
    {
        public int? ProductMapRecipeId { get; set; }
        public String ProductMapRecipeIdDelete { get; set; }
        public String RecipeCode { get; set; }
        public String ProductCode { get; set; }
        public String CampaignCode { get; set; }
        public String RecipeName { get; set; }
        public String ProductName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countProductMapRecipe { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }

    }

    public class ProductMapRecipeReturnInfo
    {
        public int? ProductMapRecipeId { get; set; }
        public String ProductMapRecipeIdDelete { get; set; }
        public String RecipeCode { get; set; }
        public String ProductCode { get; set; }
        public String CampaignCode { get; set; }
        public String RecipeName { get; set; }
        public String ProductName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countProductMapRecipe { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class L_Recipedata
    {
        public List<ProductMapRecipeInfo> RecipeListInfo { get; set; }
    }
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
    public class ProductMapRecipeInfo
    {
        public int? ProductMapRecipeId { get; set; }
        public String ProductMapRecipeIdDelete { get; set; }
        public String RecipeCode { get; set; }
        public String ProductCode { get; set; }
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
        public String RecipeCodeCheck { get; set; }
    }
}

using APPCOREMODEL.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;
using System.Security.Claims;
using System.Configuration;
using System.Web.Helpers;
using System.IO;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class RecipeController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRecipe")]
        public IHttpActionResult UpdCampaign([FromBody]RecipeInfo cInfo)
        {
            //RecipeListReturn RecipeList = new RecipeListReturn();
            //List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();
            int i = 0;
            i = cDAO.UpdateRecipe(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRecipe")]
        public IHttpActionResult DelCampaign([FromBody]RecipeInfo cInfo)
        {
            //RecipeListReturn RecipeList = new RecipeListReturn();
            //List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();
            int i = 0;
            i = cDAO.DeleteRecipe(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRecipe")]
        public IHttpActionResult InsCampaign([FromBody]RecipeInfo cInfo)
        {
        
            RecipeDAO cDAO = new RecipeDAO();
            int i = 0;
            i = cDAO.InsertRecipe(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRecipeNoPagingByCriteria")]
        public IHttpActionResult ListCpNoPagingByCriteria([FromBody]RecipeInfo cInfo)
        {
            RecipeListReturn RecipeList = new RecipeListReturn();
            List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();

            listCampaign = cDAO.ListRecipeNoPagingByCriteria(cInfo);

            return Ok<List<RecipeListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRecipeListByCriteria")]
        public IHttpActionResult CountCpListByCriteria([FromBody]RecipeInfo cInfo)
        {
            //RecipeListReturn RecipeList = new RecipeListReturn();
            //List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();
            int? i = 0;
            i = cDAO.CountRecipeListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRecipePagingByCriteria")]
        public IHttpActionResult CpListByCriteria([FromBody]RecipeInfo cInfo)
        {
            RecipeListReturn RecipeList = new RecipeListReturn();
            List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();

            listCampaign = cDAO.ListRecipePagingByCriteria(cInfo);

            return Ok<List<RecipeListReturn>>(listCampaign);
        }
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/support/InsertRecipeImport")]
        //public IHttpActionResult InsertCampImport([FromBody]L_CampaignCategorydata lRecipedata)
        //{
        //    RecipeDAO cDAO = new RecipeDAO();
        //    int sum = 0;

        //    sum = cDAO.InsertCampaignImport(lRecipedata.RecipeInfo.ToList());

        //    return Ok<int>(sum);
        //}
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductMapRecipeNoPagingByCriteria")]
        public IHttpActionResult ListProductMapRecipeNoPagingByCriteria([FromBody]ProductMapRecipeInfo cInfo)
        {
            ProductMapRecipeReturnInfo RecipeList = new ProductMapRecipeReturnInfo();
            List<ProductMapRecipeReturnInfo> listCampaign = new List<ProductMapRecipeReturnInfo>();
            RecipeDAO cDAO = new RecipeDAO();

            listCampaign = cDAO.ListProductMapRecipeNoPagingByCriteria(cInfo);

            return Ok<List<ProductMapRecipeReturnInfo>>(listCampaign);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCountProductMapRecipeNoPagingByCriteria")]
        public IHttpActionResult ListCountProductMapRecipeNoPagingByCriteria([FromBody]ProductMapRecipeInfo cInfo)
        {

            RecipeDAO cDAO = new RecipeDAO();
            int? i = 0;
            i = cDAO.CountProductMapRecipeListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRecipeByproductNoPagingByCriteria")]
        public IHttpActionResult ListRecipeByproductNoPagingByCriteria([FromBody]ProductMapRecipeInfo cInfo)
        {
            RecipeListReturn RecipeList = new RecipeListReturn();
            List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();

            listCampaign = cDAO.ListRecipeProductDetailNoPagingByCriteria(cInfo);

            return Ok<List<RecipeListReturn>>(listCampaign);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProductMapRecipe")]
        public IHttpActionResult UpdDeleteProductMapRecipe([FromBody]ProductMapRecipeInfo cInfo)
        {
            //RecipeListReturn RecipeList = new RecipeListReturn();
            //List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();
            int i = 0;
            i = cDAO.UpdateProductMapRecipe(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteProductMapRecipe")]
        public IHttpActionResult DelDeleteProductMapRecipe([FromBody]ProductMapRecipeInfo cInfo)
        {
            //RecipeListReturn RecipeList = new RecipeListReturn();
            //List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();
            int i = 0;
            i = cDAO.DeleteProductMapRecipe(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductMapRecipe")]
        public IHttpActionResult InsProductMapRecipe([FromBody]ProductMapRecipeInfo cInfo)
        {

            RecipeDAO cDAO = new RecipeDAO();
            int i = 0;
            i = cDAO.InsertProductMapRecipe(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateClearProductMapRecipe")]
        public IHttpActionResult DelDeleteAllByProductMapRecipe([FromBody]ProductMapRecipeInfo cInfo)
        {
            //RecipeListReturn RecipeList = new RecipeListReturn();
            //List<RecipeListReturn> listCampaign = new List<RecipeListReturn>();
            RecipeDAO cDAO = new RecipeDAO();
            int i = 0;
            i = cDAO.DeleteClearProductMapRecipe(cInfo);

            return Ok<int>(i);
        }
    }
}

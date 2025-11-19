using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class TemplateInfo
    {
        public int? TemplateId { get; set; }
        public String TemplateCode { get; set; }
        public String TemplateName { get; set; }
        public String TemplateBody { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
       
        public String FlagDelete { get; set; }
        public String FlagActive { get; set; }
        public String TemplateImageURL { get; set; }
        public String TemplateVideoURL { get; set; }
        public String MerchantCode { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

        public String TemplatePlatform { get; set; }

        public String TemplateType { get; set; }
    }
    public class TemplateParamInfo
    {
        public int? TemplateParamID { get; set; }
        public String TemplateParamCode { get; set; }
        public String TemplateParamName { get; set; }
        public String TemplateParamFieldName { get; set; }
        public String TemplateParamTable { get; set; }
        public String TemplateParamTableLookup { get; set; }
        public String TemplateParamFieldLookup { get; set; }
        public String TemplateParamTableCondition { get; set; }
        public String TemplateParamTableCondition2 { get; set; }
        public String ConditionCode { get; set; }
        public String LookUpConditionCode { get; set; }
        public String Data { get; set; }

    }
    public class TemplateFieldInfo
    {
        public int? TemplateFieldID { get; set; }
        public String TemplateCode { get; set; }
        public String TemplateParamCode { get; set; } 
        public String TemplateParamName { get; set; }
        public String TemplateParamFieldName { get; set; }
        public String TemplateParamTable { get; set; }
        public String TemplateParamTableLookup { get; set; }
        public String TemplateParamTableCondition { get; set; }
        public String TemplateParamFieldLookup { get; set; }
        public String TemplateParamTableCondition2 { get; set; }
        public int? TemplateFieldParamSeq { get; set; }
    }
    public class TemplatePlatformInfo
    {
        public int? TemplatePlatformID { get; set; }
        public String TemplateCode { get; set; }
        public String TemplatePlatformCode { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }

    }
    public class PromotionTemplateInfo
    {
        public int? Id { get; set; }
        public String PromotionCode { get; set; }
        public String TemplateCode { get; set; }
        public String TemplateName { get; set; }
        public String TemplateImageURL { get; set; }
        public String TemplateBody { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; } 
        public String FlagDelete { get; set; } 
        public String FlagLine { get; set; } 
        public String FlagFacebook { get; set; }
    }
}

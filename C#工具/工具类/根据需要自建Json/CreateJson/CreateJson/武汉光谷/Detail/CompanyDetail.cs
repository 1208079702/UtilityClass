using System.Collections.Generic;
namespace CreateJson.武汉光谷
{
    public class CompanyDetail
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        public string CompanyIntroduction { get; set; }
 
        /// <summary>
        /// 图片
        /// </summary>
        public Dictionary<int, TextureDetail> TextureDetails { get; set; }

        /// <summary>
        /// 备用1
        /// </summary>
        public string Standby1 { get; set; }
        /// <summary>
        /// 备用2
        /// </summary>
        public string Standby2 { get; set; }
        /// <summary>
        /// 备用3
        /// </summary>
        public string Standby30 { get; set; }


        public CompanyDetail()
        {

        }
    }
}

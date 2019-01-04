using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Detail
    {
        /// <summary>
        /// 图片的唯一Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        public string CompanyIntroduction { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 部门简介
        /// </summary>
        public string DepartmentIntroduction { get; set; }

        #region 介绍
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }//
        /// <summary>
        /// 班组
        /// </summary>
        public string Group { get; set; }//
        /// <summary>
        /// 职务
        /// </summary>
        public string Work { get; set; }// 
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string Politics { get; set; }// 
        /// <summary>
        /// 荣誉
        /// </summary>
        public List<string> Honors { get; set; }
        /// <summary>
        /// 岗位承诺
        /// </summary>
        public string Introduction { get; set; }


        #endregion

        /// <summary>
        /// 是否是详情页Id
        /// </summary>
        public bool IsImportant { get; set; }
        
        /// <summary>
        /// 图片名字
        /// </summary>
        public string TextureName { get; set; }

        /// <summary>
        /// 音频名字
        /// </summary>
        public string AudioName { get; set; }

        /// <summary>
        /// 视频路径
        /// </summary>
        public string VideoName { get; set; }

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
        public string Standby3 { get; set; }


        public Detail()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateJson.武汉光谷
{
    public class IndustryDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }

        //public Dictionary<string, CompanyDetail> CompanyDetails { get; set; }
        public List<CompanyDetail> CompanyDetails { get; set; }
    }
}

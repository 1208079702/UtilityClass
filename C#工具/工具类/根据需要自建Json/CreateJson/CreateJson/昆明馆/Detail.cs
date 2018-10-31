using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateJson.昆明馆
{
    class Detail
    {
        public int Id { get; set; } // Id
        public string Location { get; set; } //地点
        public List<string> TextureName { get; set; } // 图片名字

        public Detail()
        {

        }

        public Detail(int id, string location, List<string> textureName)
        {
            Id = id;
            Location = location;
            TextureName = textureName;
        }
    }
}

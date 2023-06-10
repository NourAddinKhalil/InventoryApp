using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Category : InterFaces.ICategory_Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Parent_ID { get; set; }
    }
}

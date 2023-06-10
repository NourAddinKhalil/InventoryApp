using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFinalSalesProject.Classes;

namespace TheFinalSalesProject.DBModels
{
    public class Full_Product_Model : Product
    {
        public new CategoryModel Cate_ID { get; set; } //= new List<CategoryModel();
        public List<Full_Product_Unit_Model> Pro_Unit { get; set; }
    }
    public class CategoryModel : InterFaces.ICategory_Model
    {
        public int CateID { get; set; }
        public string Cate_Name { get; set; }
        public int Parent_ID { get ; set ; }
        public string Number { get ; set ; }
    }
    public class Full_Product_Unit_Model : InterFaces.IProduct_Unit_Model
    {
        public int ProUnID { get; set; }
        public int Pro_ID { get; set; }
        public List<UnitModel> Unit_ID { get; set; } 
        public double Factor { get; set; }
        public double Buy_Price { get; set; }
        public double Sell_Price { get; set; }
        public double Sell_Discount { get; set; }
        public string BarCode { get; set; }
    }
    public class UnitModel
    {
        public int UnitID { get; set; }
        public string UnitName { get; set; }
    }
}

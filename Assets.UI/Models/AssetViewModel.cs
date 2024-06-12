using System.ComponentModel.DataAnnotations;

namespace Assets.UI.Models
{
    public class AssetViewModel
    {
        public required string Segment { get; set; }
        public required string Country { get; set; }
        public required string Product { get; set; }
        public required string DiscountBand { get; set; }
        public double UnitsSold { get; set; }

        [DataType(DataType.Currency)]
        public decimal ManufacturingPrice { get; set; }
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}

namespace Assets.Common.Dtos
{
    public class AssetDto
    {
        public required string Segment { get; set; }
        public required string Country { get; set; }
        public required string Product { get; set; }
        public DiscountBandDto DiscountBand { get; set; }
        public double UnitsSold { get; set; }
        public decimal ManufacturingPrice { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime Date { get; set; }
    }
}
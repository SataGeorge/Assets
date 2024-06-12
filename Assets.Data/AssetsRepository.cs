using Assets.Common.Dtos;
using Common;

namespace Assets.Data
{
    public class AssetsRepository : IRepository<AssetDto>
    {
        private readonly ICsvDataReader _dataReader;

        public AssetsRepository(ICsvDataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public async Task<IEnumerable<AssetDto>> GetAll()
        {
            IEnumerable<IEnumerable<string>> allData = await _dataReader.ReadAllLines();
            return allData.Skip(1).Select(x =>
            {
                _ = double.TryParse(x.ElementAt(4).TrimSafe(), out double unitSold);
                _ = decimal.TryParse(x.ElementAt(5).TrimSafe()[1..], out decimal manufacturePrice);
                _ = decimal.TryParse(x.ElementAt(6).TrimSafe()[1..], out decimal salePrice);
                _ = DateTime.TryParse(x.ElementAt(7).TrimSafe(), out DateTime dateSold);
                return new AssetDto
                {
                    Segment = x.ElementAt(0).Trim(),
                    Country = x.ElementAt(1).Trim(),
                    Product = x.ElementAt(2).Trim(),
                    DiscountBand = !string.IsNullOrWhiteSpace(x.ElementAt(3)) ? Enum.Parse<DiscountBandDto>(x.ElementAt(3).Trim()) : Enum.Parse<DiscountBandDto>(DiscountBandDto.None.ToString()),
                    UnitsSold = unitSold,
                    ManufacturingPrice = manufacturePrice,
                    SalePrice = salePrice,
                    Date = dateSold
                };
            });

        }
    }
}

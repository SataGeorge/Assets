using Common;
using Moq;
using Xunit;

namespace Assets.Data.Tests
{
    public class AssetsRepositoryTests
    {
        public readonly Mock<ICsvDataReader> _csvDataReader;

        public AssetsRepositoryTests()
        {
            _csvDataReader = new Mock<ICsvDataReader>();
            _ = _csvDataReader.Setup(csv => csv.ReadAllLines())
            .ReturnsAsync(new string[]
            {
                "Segment,Country, Product , Discount Band ,Units Sold,Manufacturing Price,Sale Price,Date",
                "Government,Canada, Carretera , None ,1618.5,£3.00,£20.00,01/01/2014",
                "Government,Germany, Carretera , None ,1321,£3.00,£20.00,01/01/2014",
                "Midmarket,France, Carretera , None ,2178,£3.00,£15.00,01/06/2014"
            }.Select(l => l.Split(',')));
        }

        [Fact]
        public async Task GetAll_ReturnsAllData_WithoutHeaderRow()
        {
            AssetsRepository repository = new(_csvDataReader.Object);
            IEnumerable<Common.Dtos.AssetDto> data = await repository.GetAll();
            Assert.NotNull(data);
            Assert.Equal(3, data.Count());
            Assert.Equal("Government", data.ElementAtOrDefault(1).Segment);
            Assert.Equal("Germany", data.ElementAtOrDefault(1).Country);
            Assert.Equal("Carretera", data.ElementAtOrDefault(1).Product);
            Assert.Equal("None", data.ElementAtOrDefault(1).DiscountBand.ToString());
            Assert.Equal("1321", data.ElementAtOrDefault(1).UnitsSold.ToString());
            Assert.Equal("3.00", data.ElementAtOrDefault(1).ManufacturingPrice.ToString());
            Assert.Equal("20.00", data.ElementAtOrDefault(1).SalePrice.ToString());
            Assert.Equal("01/01/2014", data.ElementAtOrDefault(1).Date.ToString("dd/MM/yyyy"));
        }
    }
}

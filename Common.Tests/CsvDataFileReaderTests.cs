using Microsoft.Extensions.Options;
using Moq;

namespace Common.Tests
{
    public class CsvDataFileReaderTests
    {
        private readonly Mock<IFileReader> _fileReader;
        private readonly CsvDataFileReader _reader;

        public CsvDataFileReaderTests()
        {
            _fileReader = new Mock<IFileReader>();
            _reader = new CsvDataFileReader(_fileReader.Object, Options.Create<CsvFile>(new CsvFile { Path = string.Empty }));
        }

        [Fact]
        public async Task ReadAllLines_WhenNormalQuteSeparatedAndQuoteWrappingCommaSeparatedData_CorrectEntriesAreReturned()
        {

            List<string> data =
            [
                @"Segment,Country, Product , Discount Band ,Units Sold,Manufacturing Price,Sale Price,Date",
                @"Government,""Canada, Ontarion"", Carretera , None ,1618.5,£3.00,£20.00,01/01/2014",
                @"Government,Germany, Carretera , None ,1321,£3.00,£20.00,01/01/2014",
                @"Midmarket,France, Carretera , ""6 Victoria Way, Reading"" ,2178,£3.00,£15.00,01/06/2014"
            ];

            _ = _fileReader.Setup(fr => fr.ReadAllLines(It.IsAny<string>())).ReturnsAsync(data);
            IEnumerable<IEnumerable<string>> lines = await _reader.ReadAllLines();
            Assert.All(lines, l => Assert.Equal(8, l.Count()));
        }
    }
}

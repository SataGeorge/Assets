using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Common
{
    public class CsvDataFileReader : ICsvDataReader
    {
        private readonly string commaEscapeCharacters = Guid.NewGuid().ToString();
        private readonly IFileReader _fileReader;
        private readonly IOptions<CsvFile> _csvFile;

        public CsvDataFileReader(IFileReader fileReader, IOptions<CsvFile> csvFile)
        {
            _fileReader = fileReader;
            _csvFile = csvFile;
        }
        public async Task<IEnumerable<IEnumerable<string>>> ReadAllLines()
        {
            List<IEnumerable<string>> csvEntries = [];
            IEnumerable<string> lines = await _fileReader.ReadAllLines(_csvFile.Value.Path);
            lines.ToList().ForEach(line =>
            {
                string escapedCommaLine = Regex.Replace(line, "\".*?\"", mev => mev.Result(commaEscapeCharacters));
                csvEntries.Add(escapedCommaLine.Split(','));
            });

            return csvEntries.Select(x => x.Select(x => x.Replace(commaEscapeCharacters, ",")));
        }
    }
}

namespace Common
{
    public class FileReader : IFileReader
    {
        public async Task<IEnumerable<string>> ReadAllLines(string path)
        {
            return await File.ReadAllLinesAsync(path);
        }
    }
}

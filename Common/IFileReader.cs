namespace Common
{
    public interface IFileReader
    {
        public Task<IEnumerable<string>> ReadAllLines(string path);
    }
}

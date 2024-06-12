namespace Common
{
    public interface ICsvDataReader
    {
        public Task<IEnumerable<IEnumerable<string>>> ReadAllLines();
    }
}

namespace Common
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();
    }
}

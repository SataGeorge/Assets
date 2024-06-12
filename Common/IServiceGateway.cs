namespace Common
{
    public interface IServiceGateway
    {
        public Task<IEnumerable<T>> GetAll<T>();
    }
}

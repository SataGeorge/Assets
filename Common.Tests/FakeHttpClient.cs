namespace Common.Tests
{
    public class FakeHttpClient<T> : HttpClient
    {
        public FakeHttpClient(HttpMessageHandler messageHandler) : base(messageHandler)
        {
        }
    }
}
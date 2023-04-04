namespace SQSService.SQS
{
    public interface ISQSService
    {
        public Task Listen();
        public Task GetMessage();
    }
}

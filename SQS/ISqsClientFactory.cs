using Amazon.SQS;

namespace SQSService.SQS
{
    public interface ISqsClientFactory
    {
        public IAmazonSQS GetSqsClient();
        public string GetSqsQueue();
    }
}
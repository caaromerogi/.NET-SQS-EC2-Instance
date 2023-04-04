using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.Options;
using SQSService.Receiver;
using System.Reflection.Metadata.Ecma335;

namespace SQSService.SQS
{
    public class SQSClientFactory : ISqsClientFactory
    {
        private readonly IOptions<SQSOptions> _options;

        public SQSClientFactory(IOptions<SQSOptions> options)
        {
            _options = options;
        }

        public IAmazonSQS GetSqsClient()
        {
            var config = new AmazonSQSConfig()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_options.Value.SqsRegion),
                ServiceURL = $"https://sqs.{_options.Value.SqsRegion}.amazonaws.com"
            };

            return new AmazonSQSClient(_options.Value.IamAccessKey, _options.Value.IamSecretKey, config);
        }

        public string GetSqsQueue() =>
        $"https://sqs.{_options.Value.SqsRegion}.amazonaws.com/{_options.Value.SqsQueueId}/{_options.Value.SqsQueueName}";

        
        
    }
}

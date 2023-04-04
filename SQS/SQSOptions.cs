namespace SQSService.Receiver
{
    public class SQSOptions
    {
        public string SqsRegion { get; set; }
        public string SqsQueueId { get; set; }
        public string SqsQueueName { get; set; }
        public string IamAccessKey { get; set; }
        public string IamSecretKey { get; set; }
    }
}

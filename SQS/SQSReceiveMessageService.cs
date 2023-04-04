using Amazon.Auth.AccessControlPolicy;
using Amazon.SQS.Model;

namespace SQSService.SQS
{
    public class SQSService : ISQSService
    {
        private readonly ISqsClientFactory _sqsClientFactory;
        private bool _isPolling;
        private CancellationTokenSource _source;


        public SQSService(ISqsClientFactory sqsClientFactory)
        {
            _sqsClientFactory = sqsClientFactory;
        }

        public async Task GetMessage()
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _sqsClientFactory.GetSqsQueue(),
                MaxNumberOfMessages = 10,
                VisibilityTimeout = 10,
                WaitTimeSeconds = 10,
            };

            var receiveMessageResponse = await _sqsClientFactory.GetSqsClient().ReceiveMessageAsync(request);


            if (receiveMessageResponse.Messages.Count != 0)
            {
                for (int i = 0; i < receiveMessageResponse.Messages.Count; i++)
                {
                    string messageBody = receiveMessageResponse.Messages[i].Body;

                    Console.WriteLine("Message Received: " + messageBody);

                    await DeleteMessageAsync(receiveMessageResponse.Messages[i].ReceiptHandle);
                }
            }
            else
            {
                Console.WriteLine("No Messages to process");
            }
        }

        private async Task DeleteMessageAsync(string recieptHandle)
        {

            DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest();
            deleteMessageRequest.QueueUrl = _sqsClientFactory.GetSqsQueue();
            deleteMessageRequest.ReceiptHandle = recieptHandle;

            DeleteMessageResponse response = await _sqsClientFactory.GetSqsClient().DeleteMessageAsync(deleteMessageRequest);

        }
        public async Task Listen()
        {
            _isPolling = true;

            int i = 0;
            try
            {
                _source = new CancellationTokenSource();
                var _token = _source.Token;

                while (_isPolling)
                {
                    i++;
                    Console.Write(i + ": ");
                    await GetMessage();
          
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("Application Terminated: " + ex.Message);
            }
            finally
            {
                _source.Dispose();
            }
        }
    }
}

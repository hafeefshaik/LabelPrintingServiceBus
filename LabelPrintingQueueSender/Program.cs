using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrintingQueueSender
{
    class Program
    {


        const string ServiceBusConnectionString = "Endpoint=sb://qs-poc-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=3h5yQa5mlt+H+e93pS/zTMA0msMC631rfV/hKjtOUDs=";
        const string QueueName = "label-printing-queue";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Sending a message to the print label queue...");

                SendSalesMessageAsync().GetAwaiter().GetResult();

                Console.WriteLine("Message was sent successfully.");
            }
        }

        static async Task SendSalesMessageAsync()
        {
            // Create a Queue Client here
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            // Send messages.
            try
            {
                // Create and send a message here
                Console.WriteLine("Enter the message that you wanna send.");
                string messageBody = $"{Console.ReadLine()}";
                Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
                Console.WriteLine($"Sending message: {messageBody}");
                await queueClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }

            // Close the connection to the queue here
            await queueClient.CloseAsync();
        }
    }
}

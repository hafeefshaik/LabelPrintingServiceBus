using Microsoft.Azure.ServiceBus;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrintingTopicSender
{
    class Program
    {
        const string ServiceBusConnectionString = "";
        const string TopicName = "label-printing-topic";
        static ITopicClient topicClient;
        static string Subscriber = string.Empty;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Who do you want send a message?");
                Subscriber = Console.ReadLine();
                Console.WriteLine("Sending a message to the label printing topic...");

                SendPerformanceMessageAsync().GetAwaiter().GetResult();

                Console.WriteLine("Message was sent successfully.");
            }

        }

        static async Task SendPerformanceMessageAsync()
        {
            // Create a Topic Client here
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            // Send messages.
            try
            {
                Console.WriteLine("Please enter your message.");
                string messageBody = $"{Console.ReadLine()}";
                Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
                message.UserProperties.Add("Name", Subscriber);
                Console.WriteLine($"Sending message: {messageBody}");
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }

            // Close the connection to the topic here
            await topicClient.CloseAsync();
        }
    }
}

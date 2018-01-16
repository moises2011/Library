using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace MessagePuller
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }

        public static void ProcessTopicMessage([ServiceBusTrigger("topic001", "subscription1")] string message,
        TextWriter logger)
        {
            logger.WriteLine("Topic message: " + message);
        }
    }
}

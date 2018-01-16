using Library.Data.IRepositories;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Library.Data.Tests
{
    [TestClass]
    public class ServiceBusRepositoryTest
    {

        private IServiceBusRepository serviceBusRepository;
        private byte[] messageText;
        private Message message;

        [TestInitialize]
        public void Initialize()
        {
            serviceBusRepository = new Library.Data.Repository.ServiceBusRepository();
            messageText = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new { abc = 123456 }));
            message = new Microsoft.Azure.ServiceBus.Message(messageText);
        }

        [TestMethod]
        public void TestServiceBusQueueMessageSuccess()
        {            
                    
            var result = serviceBusRepository.SendMessage("q1", message);
            result.Wait();
            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void TestServiceBusTopicMessageSuccess()
        {           
            var result = serviceBusRepository.SendMessageToTopic("t1", message);
            result.Wait();
            Assert.IsTrue(result.IsCompleted);
        }
    }
}

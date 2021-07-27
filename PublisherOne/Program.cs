using System;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DeviceMonitor
{
    class Program
    {      

        static void Main(string[] args)
        {
            MqttClient client;
            string clientId;
            
        // use a ready mqtt broker with no encryption, no authentication, no certificate
        // no intent to make you install some service :)

            string BrokerAddress = "test.mosquitto.org";

            client = new MqttClient(BrokerAddress);                

            // use a unique id as device name, each time clients are created.
            clientId = Guid.NewGuid().ToString();

            client.Connect(clientId);

            string topic = "/KutluIot/test";
            
            // generate a random number between 1-10
            Random rnd = new Random();           

            while (true)
            {
                Thread.Sleep(2000);

                // generate dummy measurements       
                string publishMessage = rnd.Next(1, 1000).ToString();

                client.Publish(topic, Encoding.UTF8.GetBytes(publishMessage), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

                Console.WriteLine("Message Sent from client : " + client.ClientId + " as : " + publishMessage);
            }

        }

     

    }
}

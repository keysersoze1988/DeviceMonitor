using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace PublisherTwo
{
    class Program
    {
        internal static int MessageCount = 0;
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

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string topic = "/KutluIot/+";
            // subscribe to the topic 
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
           
                byte[] buffer = e.Message;
            
                string s = "Message Recieved " + System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
               MessageCount++;    

            Console.WriteLine(s);
            Console.WriteLine("Total Message Count = " + (MessageCount -1) .ToString());
        }
    }
}

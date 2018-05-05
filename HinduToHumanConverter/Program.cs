using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace Lecture2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //start deserialization - from XML-file to object
                var xmlClientSerializer = new XmlSerializer(typeof(cl));
                cl clientInfoBad;

                using (var reader = new StreamReader("clientinfo_input.xml"))
                {
                    clientInfoBad = (cl)xmlClientSerializer.Deserialize(reader);
                }
                //end deserialization

                //convert from Hindu to human code
                var clientInfo = Convert(clientInfoBad);

                //start serialization - from object to XML-file
                var xmlSerializer = new XmlSerializer(typeof(ClientInfo));

                using (var writer = new StreamWriter("clientinfo_output.xml"))
                {
                    xmlSerializer.Serialize(writer, clientInfo);
                }

                //start serialization - from object to JSON-file
                JsonSerializer jsonSerializer = new JsonSerializer();

                using (StreamWriter sw = new StreamWriter(@"clientinfo_output.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    jsonSerializer.Serialize(sw, clientInfo);
                }

                //start serialization for work address
                var xmlSerializerAddress = new XmlSerializer(typeof(Address));

                using (var writer = new StreamWriter("workaddress_output.xml"))
                {
                    xmlSerializerAddress.Serialize(writer, clientInfo.WorkAddress);
                }

                //start serialization for home address
                JsonSerializer jsonSerializerAddress = new JsonSerializer();

                using (StreamWriter sw = new StreamWriter(@"homeaddress_output.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    jsonSerializer.Serialize(sw, clientInfo.HomeAddress);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static ClientInfo Convert(cl clientInfoBad)
        {
            ClientInfo clientInfo = new ClientInfo()
            {
                FirstName = clientInfoBad.fn,
                LastName = clientInfoBad.ln,
                MiddleName = clientInfoBad.mn,
                Phone = clientInfoBad.p,
                Email = clientInfoBad.e,
                Birthday = new DateTime(clientInfoBad.by, clientInfoBad.bm, clientInfoBad.bd)
            };

            Address homeAddress = new Address()
            {
                AddressLine = clientInfoBad.hl1,
                City = clientInfoBad.hc,
                State = clientInfoBad.hs,
                Zip = clientInfoBad.hz
            };

            Address workAddress = new Address()
            {
                AddressLine = clientInfoBad.wl1,
                City = clientInfoBad.wc,
                State = clientInfoBad.ws,
                Zip = clientInfoBad.wz
            };

            clientInfo.HomeAddress = homeAddress;
            clientInfo.WorkAddress = workAddress;

            return clientInfo;
        }
    }
}

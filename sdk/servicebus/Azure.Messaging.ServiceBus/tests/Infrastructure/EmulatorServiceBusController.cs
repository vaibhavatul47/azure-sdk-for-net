// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Azure.Messaging.ServiceBus.Tests.Infrastructure.EmulatorServiceBusConfig;

#if NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Net.Http;
#endif
#if NETFRAMEWORK
using Newtonsoft.Json;
#endif

namespace Azure.Messaging.ServiceBus.Tests.Infrastructure
{
    public class EmulatorServiceBusController
    {
        public static async Task CreateServiceBusOnExternalHost(List<NamespaceConfigObject.QueueConfig> queues = null,List<NamespaceConfigObject.TopicConfig> topics = null)
            {
            var namespaceObj = new NamespaceConfigObject() {Name = "sbemulatorns", Queues = queues ?? new List<NamespaceConfigObject.QueueConfig>() ,Topics = topics ?? new List<NamespaceConfigObject.TopicConfig>()};
            EmulatorServiceBusConfig config = new EmulatorServiceBusConfig() { Namespaces = new List<NamespaceConfigObject>() { namespaceObj } };
#if NETFRAMEWORK
            // Create a request object with the target URL
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:8090/Eventhub/Emulator/Create");

            // Set the request method to POST
            request.Method = "POST";

            // Set the content type to JSON
            request.ContentType = "application/json";

            // Create a JSON object with some data
            var json = JsonConvert.SerializeObject(config);

            // Convert the JSON object to a byte array
            var data = Encoding.UTF8.GetBytes(json);

            // Set the content length to the size of the data
            request.ContentLength = data.Length;

            // Write the data to the request stream
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            // Get the response from the server
            var response = (HttpWebResponse)request.GetResponse();
#endif

#if NET6_0_OR_GREATER
            HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:8090") };
            var response = await httpClient.PostAsJsonAsync("/ServiceBus/Emulator/Create", config);
#endif
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
    }
}

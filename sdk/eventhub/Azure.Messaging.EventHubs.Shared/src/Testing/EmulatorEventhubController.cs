// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#if NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Net.Http;
#endif
#if NETFRAMEWORK
using Newtonsoft.Json;
#endif

namespace Azure.Messaging.EventHubs.Tests
{
    /// <summary>
    /// EH Emulator Controller to Create EH in Runtime
    /// </summary>
    public class EmulatorEventhubController
    {
        /// <summary>
        /// Creates EH for emulator
        /// </summary>
        /// <param name="eventHubName"></param>
        /// <param name="consumerGroups"></param>
        /// <param name="paritionCount"></param>
        /// <returns></returns>
        public static async Task CreateEventHubOnExternalHost(string eventHubName, List<string> consumerGroups, int paritionCount)
        {
            //Add Default CG
            consumerGroups.Add("$Default");
            //Prepare CGs
            var cgList = new List<EmulatorEventhubConfig.NamespaceConfigObject.EventHubInfo.ConsumerGroupInfo>();
            consumerGroups.ForEach(cg => {
                cgList.Add(new EmulatorEventhubConfig.NamespaceConfigObject.EventHubInfo.ConsumerGroupInfo { Name = cg });
            });

            //Prepare EH
            var eventHubInfo = new EmulatorEventhubConfig.NamespaceConfigObject.EventHubInfo();
            eventHubInfo.Name = eventHubName;
            eventHubInfo.PartitionCount = paritionCount;
            eventHubInfo.ConsumerGroups = cgList;

            //Prepare Namespace
            var namespaceConfig = new EmulatorEventhubConfig.NamespaceConfigObject();
            namespaceConfig.Name = "contoso";
            namespaceConfig.Entities = new List<EmulatorEventhubConfig.NamespaceConfigObject.EventHubInfo>() { eventHubInfo };
            var ehRequest = new EmulatorEventhubConfig();
            ehRequest.NamespaceConfig = new List<EmulatorEventhubConfig.NamespaceConfigObject>() { namespaceConfig };

#if NETFRAMEWORK
            // Create a request object with the target URL
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:8090/Eventhub/Emulator/Configure");

            // Set the request method to POST
            request.Method = "POST";

            // Set the content type to JSON
            request.ContentType = "application/json";

            // Create a JSON object with some data
            var json = JsonConvert.SerializeObject(ehRequest);

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
            await httpClient.PostAsJsonAsync("/Eventhub/Emulator/Configure", ehRequest);
#endif
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}

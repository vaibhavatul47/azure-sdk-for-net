// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.Messaging.ServiceBus.Primitives;
using Microsoft.Azure.Management.ServiceBus.Models;

namespace Azure.Messaging.ServiceBus.Tests.Infrastructure
{
    public class EmulatorServiceBusConfig
    {
        public List<NamespaceConfigObject> NamespaceConfig { get; set; }
        public class NamespaceConfigObject
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public List<ServiceBusEntityDetails> Entities { get; set; }
            public class ServiceBusEntityDetails
            {
                public string Name { get; set; }
                public EmulatorServiceBusEntityType Type { get; set; }
                public string Parent { get; set; }
                public FilterType FilterType { get; set; }
                public CorrelationFilterDetails FilterDetails { get; set; }

                public enum EmulatorServiceBusEntityType
                {
                    Queue,
                    Topic,
                    Subscription
                }
                public class CorrelationFilterDetails
                {
                    //Metadata to link filter to parent entities ie. Topic->Sub->Filter
                    public string Topic { get; set; }

                    public string Subscription { get; set; }

                    //AMQP Property Headers
                    public string CorrelationId { get; set; }

                    public string MessageId { get; set; }

                    public string To { get; set; }
                    public string ReplyTo { get; set; }

                    public string Subject { get; set; }

                    public string SessionId { get; set; }

                    public string ReplyToSessionId { get; set; }

                    public string ContentType { get; set; }

                    //Customer user Application Headers
                    //PropertyDictionary is avalible in Track-2 SDK as a ServiceBus Primitive ; No need to copy same while creating a test API Client
                    public IDictionary<string, object> ApplicationProperties { get; internal set; } = new PropertyDictionary();
                }
            }
        }
    }
}

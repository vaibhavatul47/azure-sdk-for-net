// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.Messaging.ServiceBus.Primitives;
using Microsoft.Azure.Management.ServiceBus.Models;

namespace Azure.Messaging.ServiceBus.Tests.Infrastructure
{
    public class EmulatorServiceBusConfig
    {
        public List<NamespaceConfigObject> Namespaces { get; set; }
        public class NamespaceConfigObject
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public List<QueueConfig> Queues { get; set; }

            public List<TopicConfig> Topics { get; set; }
            public class QueueConfig
            {
                public string Name { get; set; }
                public QueueProperties Properties { get; set; }
            }

            public class QueueProperties
            {
                public string AutoDeleteOnIdle { get; set; }
                public bool DeadLetteringOnMessageExpiration { get; set; }

                public string DefaultMessageTimeToLive { get; set; } = "P365D";

                public string DuplicateDetectionHistoryTimeWindow { get; set; }

                public bool EnableBatchedOperations { get; set; }

                public bool EnablePartitioning { get; set; } = false;

                public bool EnableExpress { get; set; }

                public string ForwardDeadLetteredMessagesTo { get; set; }

                public string ForwardTo { get; set; }

                public string LockDuration { get; set; } = "PT1M";

                public int MaxDeliveryCount { get; set; }

                public int MaxMessageSizeInKilobytes { get; set; }

                public string MaxSizeInMegabytes { get; set; }

                public bool RequiresDuplicateDetection { get; set; }

                public bool RequiresSession { get; set; } = false;

                public string Status { get; set; }
            }

            public class TopicConfig
            {
                public string Name { get; set; }
                public TopicProperties Properties { get; set; }
                public List<SubscriptionConfig> Subscriptions { get; set; }
            }

            public class TopicProperties
            {
                public string AutoDeleteOnIdle { get; set; }
                public string DefaultMessageTimeToLive { get; set; } = "P365D";
                public string DuplicateDetectionHistoryTimeWindow { get; set; }
                public bool EnableBatchedOperations { get; set; }
                public bool EnableExpress { get; set; }
                public bool EnablePartitioning { get; set; } = false;
                public int MaxMessageSizeInKilobytes { get; set; }
                public int MaxSizeInMegabytes { get; set; }
                public bool RequiresDuplicateDetection { get; set; }
                public string Status { get; set; }
                public bool SupportOrdering { get; set; }
            }

            public class SubscriptionConfig
            {
                public string Name { get; set; }
                public SubscriptionProperties Properties { get; set; }
                public List<FilterConfig> Filters { get; set; }
            }

            public class SubscriptionProperties
            {
                public string AutoDeleteOnIdle { get; set; }
                public ClientAffineProperties ClientAffineProperties { get; set; }
                public bool DeadLetteringOnFilterEvaluationExceptions { get; set; }
                public bool DeadLetteringOnMessageExpiration { get; set; }
                public string DefaultMessageTimeToLive { get; set; } = "P365D";
                public string DuplicateDetectionHistoryTimeWindow { get; set; }
                public bool EnableBatchedOperations { get; set; }
                public string ForwardDeadLetteredMessagesTo { get; set; }
                public string ForwardTo { get; set; }
                public bool IsClientAffine { get; set; }
                public string LockDuration { get; set; } = "PT1M";
                public int MaxDeliveryCount { get; set; }
                public bool RequiresSession { get; set; } = false;
                public string Status { get; set; }
            }

            public class ClientAffineProperties
            {
                public string ClientId { get; set; }
                public bool IsDurable { get; set; }
                public bool IsShared { get; set; }
            }

            public class FilterConfig
            {
                public string Name { get; set; }
                public FilterProperties Properties { get; set; }
            }

            public class FilterProperties
            {
                public EmulatorFilterAction Action { get; set; }
                public EmulatorCorrelationFilter CorrelationFilter { get; set; }
                public EmulatorFilterType FilterType { get; set; }
                public EmulatorSqlFilter SqlFilter { get; set; }
            }

            public class EmulatorFilterAction
            {
                public int CompatibilityLevel { get; set; }
                public bool RequiresPreprocessing { get; set; }
                public string SqlExpression { get; set; }
            }

            public class EmulatorCorrelationFilter
            {
                public string ContentType { get; set; }
                public string CorrelationId { get; set; }
                public string Label { get; set; }
                public string MessageId { get; set; }
                public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
                public string ReplyTo { get; set; }
                public string ReplyToSessionId { get; set; }
                public bool RequiresPreprocessing { get; set; }
                public string SessionId { get; set; }
                public string To { get; set; }
            }
            public class EmulatorSqlFilter
            {
                public int CompatibilityLevel { get; set; }
                public bool RequiresPreprocessing { get; set; }
                public string SqlExpression { get; set; }
            }

            public enum EmulatorServiceBusEntityType
            {
                Queue,
                Topic,
                Subscription,
                Filter
            }

            public enum EmulatorFilterType
            {
                Correlation,
                Sql
            }
        }
    }
}

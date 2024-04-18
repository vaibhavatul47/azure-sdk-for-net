// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.Messaging.EventHubs.Tests
{
    /// <summary>
    /// EmulatorEventhubConfig
    /// </summary>
    public class EmulatorEventhubConfig
    {
        /// <summary>
        /// Namespace
        /// </summary>
        public List<NamespaceConfigObject> NamespaceConfig { get; set; }

        /// <summary>
        /// NamespaceConfig
        /// </summary>
        public class NamespaceConfigObject
        {
            /// <summary>
            /// Name
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// EventHub
            /// </summary>
            public List<EventHubInfo> Entities { get; set; }

            /// <summary>
            /// EventHubInfo
            /// </summary>
            public class EventHubInfo
            {
                /// <summary>
                /// Name
                /// </summary>
                public string Name { get; set; }
                /// <summary>
                /// PartitionCount
                /// </summary>
                public int PartitionCount { get; set; }
                /// <summary>
                /// ConsumerGroups
                /// </summary>
                public List<ConsumerGroupInfo> ConsumerGroups { get; set; }
                /// <summary>
                /// ConsumerGroupInfo
                /// </summary>
                public class ConsumerGroupInfo
                {
                    /// <summary>
                    /// Name
                    /// </summary>
                    public string Name { get; set; }
                }
            }
        }
    }
}

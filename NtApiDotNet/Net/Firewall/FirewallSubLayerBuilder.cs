//  Copyright 2021 Google LLC. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Linq;

namespace NtApiDotNet.Net.Firewall
{
    /// <summary>
    /// A builder to create a new firewall filter.
    /// </summary>
    public sealed class FirewallSubLayerBuilder
    {
        #region Public Properties
        /// <summary>
        /// The name of the filter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the filter.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The sub layer key. If empty will be automatically assigned.
        /// </summary>
        public Guid SubLayerKey { get; set; }
        
        /// <summary>
        /// Flags for the filter.
        /// </summary>
        public FirewallSubLayerFlags Flags { get; set; }

        /// <summary>
        /// Specify the initial weight.
        /// </summary>
        /// <remarks>You need to specify an EMPTY, UINT64 or UINT8 value.</remarks>
        public ushort Weight { get; set; }

        /// <summary>
        /// Specify provider key GUID.
        /// </summary>
        public Guid ProviderKey { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public FirewallSubLayerBuilder()
        {
            Name = string.Empty;
            Description = string.Empty;
            Weight = 0;
        }
        #endregion

        #region Internal Members
        internal FWPM_SUBLAYER0 ToStruct(DisposableList list)
        {
            FWPM_SUBLAYER0 ret = new FWPM_SUBLAYER0();
            ret.subLayerKey = SubLayerKey;
            ret.displayData.name = Name;
            ret.displayData.description = Description;
            ret.flags = Flags;
            ret.weight = Weight;
            if (ProviderKey != Guid.Empty)
            {
                ret.providerKey = list.AddStructureRef(ProviderKey).DangerousGetHandle();
            }

            return ret;
        }
        #endregion
    }
}

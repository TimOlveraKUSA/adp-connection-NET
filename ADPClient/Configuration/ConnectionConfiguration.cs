/*# This file is part of adp-api-library.
# https://github.com/adplabs/adp-connection-NET

# Copyright © 2015-2016 ADP, LLC.

# Licensed under the Apache License, Version 2.0 (the “License”);
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at

# http://www.apache.org/licenses/LICENSE-2.0

# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an “AS IS” BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
# express or implied.  See the License for the specific language
# governing permissions and limitations under the License.
*/
using System.Runtime.Serialization;


namespace ADPClient
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public abstract class ConnectionConfiguration
    {
        /// <summary>
        ///
        /// </summary>
        public abstract ConnectionConfiguration init(string JSONConfigObject);

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string clientID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string clientSecret { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string sslCertPath { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string sslKeyPath { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string sslKeyPass { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string tokenServerURL { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string apiRequestURL { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string tokenExpiration { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string accessScope { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public abstract string grantType { get; set; }
    }
}


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
using System;
using System.Runtime.Serialization;


namespace ADPClient
{
	/// <summary>
	/// 
	/// </summary>
	[DataContract]
	public class ADPAccessToken
	{
		/// <summary>
		///
		/// </summary>
		/// 
		int _expiresIn;

		/// <summary>
		///
		/// </summary>
		/// 
		[DataMember(Name = "access_token")]
		public string AccessToken { get; set; }

		/// <summary>
		///
		/// </summary>
		/// 
		[DataMember(Name = "token_type")]
		public string TokenType { get; set; }

		/// <summary>
		///
		/// </summary>
		/// 
		[DataMember(Name = "expires_in")]
		public int ExpiresIn
		{
			get { return _expiresIn; }
			set
			{
				_expiresIn = value;
				this.ExpiresOn = DateTime.Now.AddSeconds(value);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// 
		[DataMember(Name = "scope")]
		public string Scope { get; set; }

		/// <summary>
		///
		/// </summary>
		/// 
		public DateTime? ExpiresOn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{
			if (ExpiresOn.HasValue && DateTime.Now.CompareTo(ExpiresOn) < 0)
				return true;
			return false;
		}
	}
}
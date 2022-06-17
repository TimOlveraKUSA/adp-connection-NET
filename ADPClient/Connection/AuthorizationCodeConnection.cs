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
using ADPClient.ADPException;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Web;


namespace ADPClient
{

    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationCodeConnection : ADPApiConnection
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connectionConfiguration"></param>
        public AuthorizationCodeConnection(ConnectionConfiguration _connectionConfiguration)
            : base((AuthorizationCodeConfiguration)_connectionConfiguration)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ADPProductURL"></param>
        /// <returns></returns>
        public override string getADPData(string ADPProductURL)
        {
            string serverResponse = null;
            ADPAccessToken token = getAccessToken();
            Dictionary<string, string> data = null;

            if (isConnectedIndicator() && (token != null))
            {
                data = new Dictionary<string, string>();

                data.Add("client_id", ((AuthorizationCodeConfiguration)connectionConfiguration).clientID);
                data.Add("client_secret", ((AuthorizationCodeConfiguration)connectionConfiguration).clientSecret);
                data.Add("grant_type", ((AuthorizationCodeConfiguration)connectionConfiguration).grantType);
                data.Add("code", ((AuthorizationCodeConfiguration)connectionConfiguration).authorizationCode);
                data.Add("redirect_uri", ((AuthorizationCodeConfiguration)connectionConfiguration).redirectURL);


                // send the data to ADP server/s
                // since we have a valid token
                serverResponse = SendWebRequest(ADPProductURL, data, new AuthenticationHeaderValue(token.TokenType, token.AccessToken), "application/json", "GET");
            }
            else {
                throw new ADPConnectionException("Connection Exception: connection not established.");
            }
            return serverResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ADPAccessToken getAccessToken()
        {
            ADPAccessToken token = accessToken;
            AuthorizationCodeConfiguration conconfig = (AuthorizationCodeConfiguration)connectionConfiguration;
            Dictionary<string, string> data = null;
            AuthenticationHeaderValue credentials = null; 

            if (!isConnectedIndicator())
            {
                data = new Dictionary<string, string>();

                data.Add("client_id", conconfig.clientID);
                data.Add("client_secret", conconfig.clientSecret);
                data.Add("grant_type", conconfig.grantType);
                data.Add("code", conconfig.authorizationCode);
                data.Add("redirect_uri", conconfig.redirectURL);

                var encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", connectionConfiguration.clientID, connectionConfiguration.clientSecret)));
                var result = SendWebRequest(conconfig.tokenServerURL, data, credentials);

                if (!String.IsNullOrEmpty(result))
                {
                    token = JSONUtil.Deserialize<ADPAccessToken>(result);
                    Status = "connected";
                }
            }

            return token;
        }

        /// <summary>
        ///
        /// </summary>
        public void refreshToken()
        {
            // not yet
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>String</returns>
        public string getAuthorizationURL()
        {
            AuthorizationCodeConfiguration config = (AuthorizationCodeConfiguration)connectionConfiguration;

            string authorizationurl = null;

            if (config != null)
            {

                if (String.IsNullOrEmpty(config.baseAuthorizationURL))
                {
                    throw new Exception("Missing authorization url.");
                }

                if (String.IsNullOrEmpty(config.clientID) || String.IsNullOrEmpty(config.clientSecret))
                {
                    throw new Exception("Missing client information.");
                }

                if (String.IsNullOrEmpty(config.redirectURL))
                {
                    throw new Exception("Missing callback/redirect url information");
                }

                state = Guid.NewGuid().ToString();

                authorizationurl = String.Format("{0}?client_id={1}&response_type={2}&redirect_uri={3}&scope=openid&state={4}",
                    config.baseAuthorizationURL, 
                    HttpUtility.UrlEncode(config.clientID),
                    HttpUtility.UrlEncode(config.responseType),
                    HttpUtility.UrlEncode(config.redirectURL),
                    HttpUtility.UrlEncode(state) );
            }
            else
            {
                throw new ADPConnectionException("Authorization code configuration not available");
            }

            return authorizationurl;
        }
        
        /// <summary>
        /// 
        /// </summary>
        protected string Status { get; set; }  
    }
}


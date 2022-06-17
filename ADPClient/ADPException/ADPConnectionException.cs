using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADPClient.ADPException
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class ADPConnectionException : System.Exception
    {

        /// <summary>
        /// 
        /// </summary>
        public ADPConnectionException() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ADPConnectionException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ADPConnectionException(string message, System.Exception inner) : base(message, inner) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="jsondata"></param>
        public ADPConnectionException(string message, string jsondata) : base(message) {
            extraData = jsondata;
        }

        /// <summary>
        /// 
        /// </summary>
        public string extraData { get; set; }


        /// <summary>
        /// 
        /// A constructor is needed for serialization when an
        /// exception propagates from a remoting server to the client. 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ADPConnectionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
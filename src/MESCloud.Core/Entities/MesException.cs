using Abp.Logging;
using Abp.UI;
using System;
using System.Runtime.Serialization;

namespace MESCloud.Entities
{
    public class MesException : UserFriendlyException
    {
        public MesException(object ex) : base(Newtonsoft.Json.JsonConvert.SerializeObject(ex)) { }
        public MesException() : base() { }
        public MesException(string message) : base(message) { }
        public MesException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context) { }
        public MesException(string message, LogSeverity severity) : base(message, severity) { }
        public MesException(int code, string message) : base(code, message) { }
        public MesException(string message, string details) : base(message, details) { }
        public MesException(string message, Exception innerException) : base(message, innerException) { }
        public MesException(int code, string message, string details) : base(code, message, details) { }
        public MesException(string message, string details, Exception innerException) : base(message, details, innerException) { }

    }
}

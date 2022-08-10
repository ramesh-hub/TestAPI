using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TestAPI
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class ResponseMessage
    {
        private bool _bSuccess = true;
        private List<string> _messages;

        public bool Success 
        {
            get { return _bSuccess; }
            set { _bSuccess = value; }
        }

        public List<string> Messages
        {
            get { return _messages ?? new List<string>(); }
            set { _messages = value; }
        }

        public void AddMessage(string sMessage)
        {
            if (_messages == null)
                _messages = new List<string>();
            _messages.Add(sMessage);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace dotNet5781_03B_0771_7072
{
    //exception- if the user put in invalid input..
    [Serializable]
    public class invalidInputException : Exception
    {
        public invalidInputException() : base() {}
        public invalidInputException(string message) : base(message) { }
        public invalidInputException(string message, Exception inner): base(message, inner) { }
        protected invalidInputException(SerializationInfo info, StreamingContext context):base(info,context) { }


    }
}

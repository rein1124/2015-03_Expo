using System;

namespace Hdc.Mv.Inspection.Mil.Interop
{
    public class MilInteropException : Exception
    {
        public string FunctionName { get; set; }

        public int ErrorCode { get; set; }

        public MilInteropException()
        {
        }

        public MilInteropException(string message) : base(message)
        {
        }

        public MilInteropException(string message, string functionName, int errorCode) : base(message)
        {
            FunctionName = functionName;
            ErrorCode = errorCode;
        }

        public MilInteropException(string functionName, int errorCode)
        {
            FunctionName = functionName;
            ErrorCode = errorCode;
        }
    }
}
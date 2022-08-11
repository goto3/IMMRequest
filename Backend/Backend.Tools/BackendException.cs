using System;

namespace Backend.Tools
{
    [Serializable]
    public class BackendException : Exception
    {
        public string[] Args { get; }

        public BackendException() { }
        public BackendException(string msg) : base(msg) { }

        public BackendException(string msg, params string[] args) : base(msg)
        {
            this.Args = args;
        }

    }
}

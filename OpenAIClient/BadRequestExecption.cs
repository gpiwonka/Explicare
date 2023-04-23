using System.Runtime.Serialization;

namespace Explicare.OpenAIClient
{
    [Serializable]
    internal class BadRequestExecption : Exception
    {
        public BadRequestExecption()
        {
        }

        public BadRequestExecption(string message) : base(message)
        {
        }

        public BadRequestExecption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadRequestExecption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
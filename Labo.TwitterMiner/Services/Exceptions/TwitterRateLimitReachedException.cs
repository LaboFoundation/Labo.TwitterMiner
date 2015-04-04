namespace Labo.TwitterMiner.Services.Twitter.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class TwitterRateLimitReachedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRateLimitReachedException"/> class.
        /// </summary>
        public TwitterRateLimitReachedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRateLimitReachedException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public TwitterRateLimitReachedException(Exception innerException)
            : base(null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRateLimitReachedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TwitterRateLimitReachedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRateLimitReachedException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected TwitterRateLimitReachedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRateLimitReachedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TwitterRateLimitReachedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

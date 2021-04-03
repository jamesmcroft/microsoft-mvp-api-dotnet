namespace MVP.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines an attribute for a message from an API exception.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal class ApiExceptionMessageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionMessageAttribute"/> class.
        /// </summary>
        /// <param name="exceptionMessage">
        /// The message to display.
        /// </param>
        public ApiExceptionMessageAttribute(string exceptionMessage)
        {
            this.ExceptionMessage = exceptionMessage;
        }

        /// <summary>
        /// Gets the message to display.
        /// </summary>
        public string ExceptionMessage { get; }

        /// <summary>
        /// Gets the exception message from an object where the object has the <see cref="ApiExceptionMessageAttribute"/> applied.
        /// </summary>
        /// <param name="obj">
        /// The object to search for the exception message on.
        /// </param>
        /// <returns>
        /// Returns the value from the exception message.
        /// </returns>
        public static string GetMessage(object obj)
        {
            Type objType = obj.GetType();
            IEnumerable<MemberInfo> memberInfos = objType.GetTypeInfo().DeclaredMembers;

            MemberInfo memberInfo = memberInfos.FirstOrDefault(x => x.Name == obj.ToString());
            CustomAttributeData attribute =
                memberInfo?.CustomAttributes.FirstOrDefault(
                    x => x.AttributeType == typeof(ApiExceptionMessageAttribute));

            return attribute != null ? attribute.ConstructorArguments[0].Value.ToString() : obj.ToString();
        }
    }
}
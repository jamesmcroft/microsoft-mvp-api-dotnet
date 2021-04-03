namespace MVP.Api.Models.MicrosoftAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines an attribute for the actual scope name for an MSA scope.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal class MSAScopeNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSAScopeNameAttribute"/> class.
        /// </summary>
        /// <param name="scopeName">
        /// The actual scope name.
        /// </param>
        public MSAScopeNameAttribute(string scopeName)
        {
            this.ScopeName = scopeName;
        }

        /// <summary>
        /// Gets the scope name.
        /// </summary>
        public string ScopeName { get; }

        /// <summary>
        /// Gets the scope name from an object where the object has the <see cref="MSAScopeNameAttribute"/> applied.
        /// </summary>
        /// <param name="obj">
        /// The object to search for the scope attribute on.
        /// </param>
        /// <returns>
        /// Returns the value from the scope attribute.
        /// </returns>
        public static string GetScopeName(object obj)
        {
            Type objType = obj.GetType();
            IEnumerable<MemberInfo> memberInfos = objType.GetTypeInfo().DeclaredMembers;

            MemberInfo memberInfo = memberInfos.FirstOrDefault(x => x.Name == obj.ToString());
            CustomAttributeData attribute =
                memberInfo?.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(MSAScopeNameAttribute));

            return attribute != null ? attribute.ConstructorArguments[0].Value.ToString() : obj.ToString();
        }
    }
}
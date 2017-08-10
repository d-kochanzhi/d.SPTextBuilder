using System;
using System.Collections.Generic;
using System.Reflection;

namespace d.SPTextBuilder.Extensions
{
    /// <summary>
    /// Reflection Extension
    /// </summary>
    static class ReflectionExtension
    {
        #region Methods

        /// <summary>
        /// Invoke value from mamber
        /// </summary>
        /// <param name="memberInfo">Member</param>
        /// <param name="forObject">Object</param>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public static object GetValueExt(this MemberInfo memberInfo, object forObject, object[] parameters = null)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo)?.GetValue(forObject);

                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo)?.GetValue(forObject);

                case MemberTypes.Method:
                    return ((MethodInfo)memberInfo)?.Invoke(forObject, parameters);

                default:
                    throw new NotImplementedException();
            }
        }


        public static IDictionary<string, MemberInfo> GetMembersByAttribute<T>(this object dataSource)
        {
            Type type = dataSource.GetType();
            IDictionary<string, MemberInfo> result = new Dictionary<string, MemberInfo>();
            MemberInfo[] members = type.GetMembers();
            for (int i = 0; i < members.Length; i++)
            {
                MemberInfo memberInfo = members[i];
                Attribute[] customAttributes = Attribute.GetCustomAttributes(memberInfo, typeof(T));
                if (memberInfo.MemberType == MemberTypes.Property
                    || memberInfo.MemberType == MemberTypes.Field
                    || memberInfo.MemberType == MemberTypes.Method)
                {
                    string key = memberInfo.Name.ToLower();
                    result.Add(key, memberInfo);
                }
            }

            return result;
        }


        #endregion Methods
    }
}
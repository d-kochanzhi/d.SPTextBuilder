using System;

namespace d.SPTextBuilder.Extensions
{
    /// <summary>
    /// Convert Extension
    /// </summary>
    static class ConvertExtension
    {
        #region Methods

        /// <summary>
        /// Convert object to specific type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">Object</param>
        /// <returns></returns>
        public static T To<T>(this IConvertible obj)
        {
            Type typeFromHandle = typeof(T);
            bool isNullabe = typeFromHandle.IsGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(Nullable<>);
            T result;
            if (isNullabe)
            {
                if (obj == null)
                {
                    result = (T)((object)null);
                }
                else
                {
                    result = (T)((object)Convert.ChangeType(obj, Nullable.GetUnderlyingType(typeFromHandle)));
                }
            }
            else
            {
                result = (T)((object)Convert.ChangeType(obj, typeFromHandle));
            }
            return result;
        }

        #endregion Methods
    }
}
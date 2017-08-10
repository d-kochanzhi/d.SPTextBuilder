using System;

namespace d.SPTextBuilder.Attributes
{
    /// <summary>
    /// Attribute for mapping
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class TextBuilderMethodAttribute : Attribute
    {
        #region Constructors

        public TextBuilderMethodAttribute()
        {
        }

        #endregion Constructors

    }
}
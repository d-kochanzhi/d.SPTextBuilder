using System;
using System.Collections.Generic;
using System.Text;

namespace d.SPTextBuilder.Exeptions
{
    /// <summary>
    /// Parse Exception
    /// </summary>
    [Serializable]
    public class ParseException : Exception
    {
        #region Properties

        /// <summary>
        /// List of errors
        /// </summary>
        public IList<string> Errors
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        public ParseException(IEnumerable<string> errors)
        {
            this.Errors = new List<string>(errors).AsReadOnly();
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string current in this.Errors)
            {
                stringBuilder.Append(current + Environment.NewLine);
            }
            return string.Concat(new object[]
            {
                base.ToString(),
                Environment.NewLine,
                "Errors:",
                Environment.NewLine,
                stringBuilder
            });
        }

        #endregion Methods
    }
}
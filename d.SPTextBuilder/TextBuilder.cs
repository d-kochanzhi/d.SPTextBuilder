using d.SPTextBuilder.Attributes;
using d.SPTextBuilder.Exeptions;
using d.SPTextBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace d.SPTextBuilder
{

    /// <summary>
    /// Template parser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TextBuilder<T>
    {
        #region Fields

        private T _listItemProvider;
        private string _template;

        #endregion Fields

        #region Properties

        public T Provider
        {
            get
            {
                return this._listItemProvider;
            }
            protected set
            {
                this._listItemProvider = value;
            }
        }

        public string Template
        {
            get
            {
                return this._template;
            }
            set
            {
                this._template = value;
            }
        }

        #endregion Properties

        #region Constructors

        public TextBuilder(T provider, string template = "")
        {
            this.Provider = provider;
            this.Template = template;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parse text template and fill with SPListItem values
        /// </summary>
        /// <param name="template">Text/Html template</param>
        /// <returns></returns>
        public string Parse(string template = "")
        {
            if (!string.IsNullOrEmpty(template))
                this.Template = template;

            return ParseTemplate(this.Template);
        }


        private string ParseTemplate(string template)
        {
            StringBuilder stringBuilder = new StringBuilder(template.Length);
            List<string> listErrors = new List<string>();
            Regex regexTemplate = new Regex("{%((?<paramName>.*?)(\\((?<addParams>.*?)\\))?(:(?<format>.*?))?)%}", RegexOptions.Multiline | RegexOptions.Singleline);
            Regex regexParams = new Regex("\\s*,\\s*", RegexOptions.Multiline | RegexOptions.Singleline);
            MatchCollection matchCollection = regexTemplate.Matches(template);
            int num = 0;
            IDictionary<string, MemberInfo> textBuilderMethods = this.Provider.GetMembersByAttribute<TextBuilderMethodAttribute>();

            foreach (Match match in matchCollection)
            {
                string paramName = match.Groups["paramName"].Value.ToLower().Trim();
                string addParams = match.Groups["addParams"].Value.Trim();
                string format = match.Groups["format"].Value.Trim();

                try
                {                    
                    if (!textBuilderMethods.ContainsKey(paramName))
                    {
                        listErrors.Add($"Can not find parameter named {paramName} ");
                    }
                    else
                    {
                        MemberInfo memberInfo = textBuilderMethods[paramName];
                        MethodInfo methodInfo = memberInfo as MethodInfo;
                        IList<object> methodParams = null;
                        string[] parameters = regexParams.Replace(addParams, ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (methodInfo != null)
                        {
                            ParameterInfo[] pi = methodInfo.GetParameters();

                            if (pi.Length != parameters.Length)
                            {
                                listErrors.Add($"Parameter {paramName} has too many arguments specified");
                                continue;
                            }
                            if (parameters.Count() > 0)
                                methodParams = new List<object>();

                            int i = 0;
                            foreach (ParameterInfo p in pi)
                            {
                                var newVal = Convert.ChangeType(parameters[i], p.ParameterType);
                                methodParams.Add(newVal);
                                i++;
                            }
                        }
                        else if (!string.IsNullOrEmpty(addParams))
                        {
                            listErrors.Add($"Parameter named {paramName} do not have additional parameters");
                            continue;
                        }
                        if (num < match.Index)
                        {
                            stringBuilder.Append(template.Substring(num, match.Index - num));
                        }

                        object v = memberInfo.GetValueExt(this.Provider, methodParams?.ToArray());
                        format = "{0" + ((!string.IsNullOrEmpty(format)) ? ":" + format : "") + "}";
                        stringBuilder.Append(string.Format(format, v));

                        num = match.Index + match.Length;
                    }

                }
                catch (Exception ex)
                {
                    listErrors.Add($"Exception while retrieving data. Parameter {paramName}. \r\n {ex.Message}");
                }
            }
            if (listErrors.Count > 0)
            {
                throw new ParseException(listErrors);
            }
            stringBuilder.Append(template.Substring(num));
            return stringBuilder.ToString();
        }

        #endregion Methods
    }
}
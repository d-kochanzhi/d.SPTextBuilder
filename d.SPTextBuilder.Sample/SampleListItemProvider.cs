using d.SPTextBuilder.Attributes;
using System;

namespace d.SPTextBuilder.Sample
{
    internal class SampleListItemProvider : d.SPTextBuilder.Providers.SPListItemBaseProvider
    {
        #region Properties

        [TextBuilderMethod]
        public int CustomProperty
        {
            get
            {
                return 45;
            }
        }

        [TextBuilderMethod]
        public DateTime CustomFormatProperty
        {
            get
            {
                return DateTime.Now;
            }
        }

        #endregion Properties

        #region Constructors

        public SampleListItemProvider(Microsoft.SharePoint.SPListItem item) : base(item)
        {
        }

        #endregion Constructors

        #region Methods

        [TextBuilderMethod]
        public int CustomMethodNoParams()
        {
            return 1;
        }

        [TextBuilderMethod]
        public int CustomMethodWithParams(int a, int b)
        {
            return a + b;
        }

        #endregion Methods
    }
}
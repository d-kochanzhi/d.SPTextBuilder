using d.SPTextBuilder.Attributes;
using Microsoft.SharePoint;

namespace d.SPTextBuilder.Providers
{
    /// <summary>
    /// Base class for sharepoint list item
    /// </summary>
    public class SPListItemBaseProvider
    {
        #region Properties

        [TextBuilderMethod]
        public string Id
        {
            get
            {
                return this.Item.ID.ToString();
            }
        }

        public SPListItem Item
        {
            get;
            private set;
        }

        [TextBuilderMethod]
        public string ListTitle
        {
            get
            {
                return this.Item.ParentList.Title;
            }
        }

        [TextBuilderMethod]
        public string ListUrl
        {
            get
            {
                return this.Item.ParentList.DefaultView.ServerRelativeUrl;
            }
        }

        [TextBuilderMethod]
        public string SiteUrl
        {
            get
            {
                return this.Item.Web.Site.Url;
            }
        }

        [TextBuilderMethod]
        public string Title
        {
            get
            {
                return this.Item.Title;
            }
        }

        [TextBuilderMethod]
        public string WebTitle
        {
            get
            {
                return this.Item.Web.Title;
            }
        }

        [TextBuilderMethod]
        public string WebUrl
        {
            get
            {
                return this.Item.Web.Url;
            }
        }

        #endregion Properties

        #region Constructors

        public SPListItemBaseProvider(SPListItem item)
        {
            this.Item = item;
        }

        #endregion Constructors



        #region Methods

        /// <summary>
        /// Access to SPListItem fields
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns></returns>
        [TextBuilderMethod]
        public string Field(string fieldName)
        {
            return this.Item.GetFormattedValue(fieldName);
        }

        #endregion Methods
    }
}
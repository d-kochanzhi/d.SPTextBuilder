# Sharepoint SPListItem to text template

Basic useful feature list: 

* Parsing SPListItem
* Additional methods/properties
* Formating values

Template tags:

* Accessing to list item {%Field(Title)%}
* Accessing to property {%CustomProperty%}
* Accessing to method with parameters {%CustomMethodWithParams(6,9)%}
* Formating values {%CustomFormatProperty:dd/MM/yyyy%}

Sample code


```csharp
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
```
	
```csharp

TextBuilder<SampleListItemProvider> textBuilder = 
new TextBuilder<SampleListItemProvider>(new SampleListItemProvider(item));

string mailTemplate = 
    "ItemTitle: {%Field(Title)%}" + Environment.NewLine +
    "NoteFld: {%Field(NoteFld)%}" + Environment.NewLine +
    "ChoiceFld: {%Field(ChoiceFld)%}" + Environment.NewLine +
    "NumberFld: {%Field(NumberFld)%}" + Environment.NewLine +
    "CurrencyFld: {%Field(CurrencyFld)%}" + Environment.NewLine +
    "DateTimeFld: {%Field(DateTimeFld)%}" + Environment.NewLine +
    "LookupFld: {%Field(LookupFld)%}" + Environment.NewLine +
    "BooleanFld: {%Field(BooleanFld)%}" + Environment.NewLine +
    "UserFld: {%Field(UserFld)%}" + Environment.NewLine +
    "URLFld: {%Field(URLFld)%}" + Environment.NewLine +
    "CustomProperty: {%CustomProperty%}" + Environment.NewLine +
    "CustomMethodNoParams: {%CustomMethodNoParams%}" + Environment.NewLine +
    "CustomMethodWithParams: {%CustomMethodWithParams(6,9)%}" + Environment.NewLine +
    "CustomFormatProperty: {%CustomFormatProperty:dd/MM/yyyy%}";
    
	string output = textBuilder.Parse(mailTemplate);

```

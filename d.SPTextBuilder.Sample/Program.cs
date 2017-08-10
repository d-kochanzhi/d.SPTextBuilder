using Microsoft.SharePoint;
using System;

namespace d.SPTextBuilder.Sample
{
    internal class Program
    {
        #region Methods

        private static void Main(string[] args)
        {
            //Console.WriteLine("Enter Web Url:");
            //string webUrl = Console.ReadLine();
            string webUrl = "http://msvo-sppsvs/MatrixPoll";
            using (SPSite site = new SPSite(webUrl))
            using (SPWeb web = site.OpenWeb())
            {
                SPList list = web.Lists["test"];
                SPListItem item = list.GetItemById(1);

                TextBuilder<SampleListItemProvider> textBuilder = new TextBuilder<SampleListItemProvider>(new SampleListItemProvider(item));

                string mailTemplate = "Dear customer, " + Environment.NewLine +
                    "We are pleased to announce: " + Environment.NewLine +

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

                Console.WriteLine(output);
            }
        }

        #endregion Methods
    }
}
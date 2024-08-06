using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UriComposer : IUriComposer
    {
        private readonly CatalogSettings catalogSettings;

        public UriComposer(CatalogSettings catalogSettings)
        {
            this.catalogSettings = catalogSettings;
        }
        public string ComposePicUri(string uriTemplate)
        {
            return uriTemplate.Replace("http://productbaseurltobereplaced", catalogSettings.CatalogBaseUrl);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Documentation.Example
{
    public class Pages
    {
        private string _baseUrl;
        private string _path;

        public void Override(string host = null, int? port = null)
        {
            var uri = new Uri(_baseUrl);
            var builder = new UriBuilder(uri);

            if (host.HasValue())
                builder.Host = host;
            if (port.HasValue)
                builder.Port = port.Value;

            _baseUrl = builder.Uri.ToString();
        }

        public string MapUrl(string page)
        {
            string url = $"{_baseUrl}{_path}/";
            var pages = _config.Pages.Cast<PageElement>().Where(p =>
p.Name.ToLower() == page.ToLower());
            if (pages.Any())
                return url += pages.First().Url;
            else
                throw new ArgumentException($"unknown page: {page}");
        }

    }
}

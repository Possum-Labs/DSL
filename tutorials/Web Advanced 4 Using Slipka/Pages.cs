using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Documentation.Example
{
    public class Pages
    {
        public Pages()
        {
            Paths = new Dictionary<string, string>();
            //we set them all to lower
            Paths.Add("home", "home");

        }
        public Dictionary<string, string> Paths { get; }
        private string _baseUrl;

        public void Override(Uri uri)
        {
            var builder = new UriBuilder(uri);
            _baseUrl = builder.Uri.ToString();
        }

        public string MapUrl(string page)
        {
            string url = $"{_baseUrl}/";
            var path = Paths[page.ToLower()];
            return url += path;
        }

    }
}

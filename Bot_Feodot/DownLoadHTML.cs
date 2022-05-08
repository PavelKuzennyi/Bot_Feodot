using System.Collections.Generic;
using System.Linq;

namespace Bot_Feodot
{
    using System;
    using System.Net;
    using System.IO;

    static class DownLoadHtml
    {
        public static List<string> HtmlRead(int number)
        {
            List<string> quotes = new();
            WebRequest req = WebRequest.
                Create($@"https://mybook.ru/author/karl-genrih-marks/kapital-tom-pervyj/citations/?page={number}");
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new(stream);
            string s = sr.ReadToEnd();
            var parts = s.Split("<div class=\"sc-2aegk7-2 bzpNIu\">");
            foreach (var part in parts.Skip(1))
            {
                quotes.Add(part.Split("</div>")[0]);
            }

            return quotes;

        }
        
        public static List<string> HtmlRead(string link)
        {
            List<string> links = new();
            WebRequest req = WebRequest.
                Create(link);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new(stream);
            string s = sr.ReadToEnd();
            var parts = s.Split("/watch");
            foreach (var part in parts.Skip(1))
            {
                links.Add("/watch"+part.Split("\"")[0]);
            }

            return links;
        }
    }
}
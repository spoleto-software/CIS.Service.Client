﻿using System.Text;

namespace CIS.Service.Client
{
    public static class DefaultSettings
    {
        public const string ContentType = ContentTypes.ApplicationJson;
        public const string Charset = "utf-8";
        public static readonly Encoding Encoding = Encoding.GetEncoding(Charset);
    }
}

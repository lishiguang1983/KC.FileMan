using System;
using System.Collections.Generic;
using System.Text;

namespace KC.FileMan.Domain
{
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 所有
        /// </summary>
        All=0,

        /// <summary>
        /// 文件夹
        /// </summary>
        Folder=1,

        /// <summary>
        /// 文件
        /// </summary>
        File=2,

        /// <summary>
        /// 其它类型
        /// </summary>
        Other=10
    }

    public static class MimeType
    {
        #region application/*

        public const string ApplicationForceDownload = "application/force-download";

        public const string ApplicationJson = "application/json";

        public const string ApplicationOctetStream = "application/octet-stream";

        public const string ApplicationPdf = "application/pdf";

        public const string ApplicationRssXml = "application/rss+xml";

        public const string ApplicationXWwwFormUrlencoded = "application/x-www-form-urlencoded";

        public const string ApplicationXZipCo = "application/x-zip-co";

        #endregion application/*


        #region image/*

        public const string ImageBmp = "image/bmp";

        public const string ImageGif = "image/gif";

        public const string ImageJpeg = "image/jpeg";

        public const string ImagePJpeg = "image/pjpeg";

        public const string ImagePng = "image/png";

        public const string ImageTiff = "image/tiff";

        #endregion image/*


        #region text/*

        public const string TextCss = "text/css";

        public const string TextCsv = "text/csv";

        public const string TextJavascript = "text/javascript";

        public const string TextPlain = "text/plain";

        public const string TextXlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        #endregion text/*
    }
}

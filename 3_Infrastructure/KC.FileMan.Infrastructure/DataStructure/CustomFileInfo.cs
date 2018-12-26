namespace KC.FileMan.Infrastructure.DataStructure
{
    public class CustomFileInfo
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string p { get; set; }

        /// <summary>
        /// 时间戳（创建时间）
        /// </summary>
        public string t { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string s { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public string w { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public string h { get; set; }
    }
}

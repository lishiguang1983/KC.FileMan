namespace KC.FileMan.Infrastructure.DataStructure
{
    public class CustomFolderInfo
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
        /// 子文件数量
        /// </summary>
        public string f { get; set; }

        /// <summary>
        /// 子文件夹数量
        /// </summary>
        public string d { get; set; }
    }
}

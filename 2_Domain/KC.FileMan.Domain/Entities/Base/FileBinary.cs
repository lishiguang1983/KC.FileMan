namespace KC.FileMan.Domain.Entities
{
    public partial class FileBinary : PersistentObject
    {
        /// <summary>
        /// 文件byte数组
        /// </summary>
        public virtual byte[] Binary { get; set; }
    }
}

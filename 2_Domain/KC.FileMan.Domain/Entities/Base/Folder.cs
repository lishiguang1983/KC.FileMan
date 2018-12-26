using System;

namespace KC.FileMan.Domain.Entities
{
    public partial class Folder : PersistentObject
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public virtual int ParentId { get; set; }

        /// <summary>
        /// 文件夹名
        /// </summary>
        public virtual string FolderName { get; set; }

        /// <summary>
        /// 文件夹实际大小字符串形式
        /// </summary>
        public virtual string FolderSize { get; set; }

        /// <summary>
        /// 文件夹大小（字节）
        /// </summary>
        public virtual long FolderSizeByte { get; set; }

        /// <summary>
        /// 文件夹路径
        /// </summary>
        public virtual string FolderPath { get; set; }

        /// <summary>
        /// 子文件数量
        /// </summary>
        public virtual int FilesCount { get; set; }

        /// <summary>
        /// 子文件夹数量
        /// </summary>
        public virtual int FoldersCount { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDelete { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdatedTime { get; set; }
    }
}

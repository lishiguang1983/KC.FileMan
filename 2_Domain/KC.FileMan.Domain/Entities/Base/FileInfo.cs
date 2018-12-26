using System;

namespace KC.FileMan.Domain.Entities
{
    public partial class FileInfo : PersistentObject
    {
        /// <summary>
        /// 所在文件夹ID
        /// </summary>
        public virtual int FolderId { get; set; }

        /// <summary>
        /// 文件基本信息ID
        /// </summary>
        public virtual int FileBinaryId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public virtual string FilePath { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDelete { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public virtual string FileExtension { get; set; }

        /// <summary>
        /// Mime-Type互联网媒体类型
        /// </summary>
        public virtual string ContentType { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public virtual string ExtendInfo { get; set; }

        /// <summary>
        /// 文件的MD5值
        /// </summary>
        public virtual string MD5 { get; set; }

        /// <summary>
        /// 文件实际大小字符串形式
        /// </summary>
        public virtual string FileSize { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public virtual long FileSizeByte { get; set; }

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

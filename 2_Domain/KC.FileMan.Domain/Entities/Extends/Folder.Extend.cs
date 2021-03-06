﻿using System.Collections.Generic;

namespace KC.FileMan.Domain.Entities
{
    public partial class Folder
    {
        public Folder()
        {
            this.FileInfos = new List<FileInfo>();
        }
        /// <summary>
        /// 文件
        /// </summary>
        public virtual IList<FileInfo> FileInfos { get; set; }
    }
}

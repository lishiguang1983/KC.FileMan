using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KC.FileMan.Domain.Entities
{
    public partial class FileInfo
    {
        /// <summary>
        /// 文件流信息
        /// </summary>
        public virtual FileBinary FileBinary { get; set; }

        /// <summary>
        /// 扩展信息对象
        /// </summary>
        public virtual ExtendInfoObj ExtendInfoObj
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ExtendInfo))
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<ExtendInfoObj>(this.ExtendInfo);
                    }
                    catch (Exception)
                    {
                        return new ExtendInfoObj();
                    }
                }
                else
                {
                    return new ExtendInfoObj();
                }
            }
        }
    }

    /// <summary>
    /// 扩展信息对象
    /// </summary>
    public class ExtendInfoObj
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
    }
}

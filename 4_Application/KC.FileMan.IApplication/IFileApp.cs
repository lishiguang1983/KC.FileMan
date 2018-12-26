using KC.FileMan.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace KC.FileMan.IApplication
{
    public interface IFileApp
    {
        /// <summary>
        /// 文件上传至数据库
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="folderId">文件夹ID</param>
        bool Upload(IFormFileCollection files, int folderId);

        /// <summary>
        /// 根据文件ID获取文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FileBinary GetFileBinaryByInfoId(int id);
    }
}

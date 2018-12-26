using KC.FileMan.Infrastructure.DataStructure;
using System.Collections.Generic;

namespace KC.FileMan.IApplication
{
    public interface IFolderApp
    {
        /// <summary>
        /// 更新文件夹内文件数量
        /// </summary>
        /// <param name="folderId">文件夹ID</param>
        void UpdateFilesCount(int folderId);

        /// <summary>
        /// 文件夹列表
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        List<CustomFolderInfo> FolderInfoList(int parentId);

        /// <summary>
        /// 获取文件夹下的文件列表
        /// </summary>
        /// <param name="folderId">文件夹ID</param>
        /// <returns></returns>
        List<CustomFileInfo> FileInfoList(int folderId);

        /// <summary>
        /// 将文件夹，复制到新的文件夹下面
        /// </summary>
        /// <param name="id">文件夹ID</param>
        /// <param name="parentId">父文件夹ID</param>
        void CopyFolder(int id, int parentId);

        /// <summary>
        /// 将文件，复制到新的文件夹下面
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <param name="parentId">父文件夹ID</param>
        void CopyFile(int id, int parentId);

        /// <summary>
        /// 添加新的文件夹
        /// </summary>
        /// <param name="parentId">父文件夹ID</param>
        /// <param name="folderName">文件夹名称</param>
        void AddFolder(int parentId, string folderName);

        /// <summary>
        /// 根据文件夹ID，删除文件夹及子文件/子文件夹
        /// </summary>
        /// <param name="id">文件夹ID</param>
        void DeleteFolder(int id);

        /// <summary>
        /// 根据文件ID，删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        void DeleteFile(int id);

        /// <summary>
        /// 将文件夹，移动到新的文件夹下面
        /// </summary>
        /// <param name="id">文件夹ID</param>
        /// <param name="parentId">父文件夹ID</param>
        void MoveFolder(int id, int parentId);

        /// <summary>
        /// 将文件，移动到新的文件夹下面
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <param name="parentId">父文件夹ID</param>
        void MoveFile(int id, int parentId);

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="id">文件夹ID</param>
        /// <param name="folderName">文件夹新名称</param>
        void RenameFolder(int id, string folderName);

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <param name="fileName">文件新名称</param>
        void RenameFile(int id, string fileName);
    }
}

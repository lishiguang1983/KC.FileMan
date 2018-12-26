using KC.FileMan.Domain.Entities;
using KC.FileMan.IApplication;
using KC.FileMan.Infrastructure;
using KC.FileMan.Infrastructure.DataStructure;
using KC.FileMan.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.IO;

namespace KC.FileMan.Application
{
    public class FolderApp : AppBase, IFolderApp
    {

        #region 属性
        private IFolderRepository folderRepository;
        #endregion

        #region 构造函数
        public FolderApp(IFolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
        }
        #endregion

        /// <summary>
        /// 更新文件夹内文件数量
        /// </summary>
        /// <param name="folderId">文件夹ID</param>
        [DataSession]
        public void UpdateFilesCount(int folderId)
        {
            var folder = folderRepository.Find(x => x.Id == folderId && x.IsDelete == false);
            if (folder != null)
            {
                folder.FilesCount = folder.FileInfos.Count;
                folder.UpdatedTime = DateTime.Now;
                //更新文件夹
                folderRepository.Update(folder);
            }
        }

        /// <summary>
        /// 文件夹列表
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        [DataSession]
        public List<CustomFolderInfo> FolderInfoList(int parentId)
        {
            var result = new List<CustomFolderInfo>();
            IList<Folder> folderList = new List<Folder>();
            if (parentId > 0)
            {
                folderList = folderRepository.GetList(x => x.ParentId == parentId && x.IsDelete == false);
            }
            else
            {
                folderList = folderRepository.GetList(x => x.IsDelete == false);
            }
            //如果不存在文件夹，则创建初始父文件夹，并保存
            if (folderList == null || folderList.Count == 0)
            {
                var currentTime = DateTime.Now;
                var folder = new Folder();
                folder.ParentId = 0;
                folder.FolderName = "我的文件";
                folder.FolderPath = "/我的文件";
                folder.CreatedTime = currentTime;
                folder.UpdatedTime = currentTime;
                folderRepository.Save(folder);
                folderList = new List<Folder>();
                folderList.Add(folder);
            }

            result = this.GetList(folderList.Where(x => x.ParentId == 0).ToList(), folderList, result);
            return result;
        }

        /// <summary>
        /// 文件列表
        /// </summary>
        /// <param name="folderId">文件夹ID</param>
        /// <returns></returns>
        [DataSession]
        public List<CustomFileInfo> FileInfoList(int folderId)
        {
            var result = new List<CustomFileInfo>();
            var folder = folderRepository.GetFolderAndFileInfos(folderId);
            if (folder != null)
            {
                foreach (var fileInfo in folder.FileInfos)
                {
                    var customFileInfo = new CustomFileInfo();
                    customFileInfo.id = fileInfo.Id.ToString();
                    customFileInfo.p = fileInfo.FilePath;
                    DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    customFileInfo.t = ((long)(fileInfo.CreatedTime.AddHours(-8) - Jan1st1970).TotalMilliseconds).ToString().Substring(0, 10);
                    customFileInfo.s = fileInfo.FileSizeByte.ToString();
                    customFileInfo.w = fileInfo.ExtendInfoObj.Width.ToString();
                    customFileInfo.h = fileInfo.ExtendInfoObj.Height.ToString();
                    result.Add(customFileInfo);
                }
            }
            return result;
        }

        /// <summary>
        /// 将文件夹，复制到新的文件夹下面
        /// </summary>
        /// <param name="id">文件夹ID</param>
        /// <param name="parentId">父文件夹ID</param>
        [Transaction]
        [DataSession]
        public void CopyFolder(int id, int parentId)
        {
            var currentTime = DateTime.Now;
            var folder = folderRepository.Find(x => x.Id == id && x.IsDelete == false);
            if (folder != null)
            {
                var parentFolder = folderRepository.Find(x => x.Id == parentId && x.IsDelete == false);
                if (parentFolder != null)
                {
                    var newFolder = new Folder()
                    {
                        ParentId = parentFolder.Id,
                        FolderName = folder.FolderName,
                        FolderSize = folder.FolderSize,
                        FolderSizeByte = folder.FolderSizeByte,
                        FolderPath = parentFolder.FolderPath + "/" + folder.FolderName,
                        FilesCount = 0,
                        FoldersCount = 0,
                        IsDelete = false,
                        CreatedTime = currentTime,
                        UpdatedTime = currentTime
                    };
                    parentFolder.FoldersCount++;
                    parentFolder.UpdatedTime = currentTime;
                    folderRepository.Update(parentFolder);
                    folderRepository.Save(newFolder);
                }
            }
        }

        /// <summary>
        /// 将文件，复制到新的文件夹下面
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <param name="parentId">父文件夹ID</param>
        [DataSession]
        public void CopyFile(int id, int parentId)
        {
            var currentTime = DateTime.Now;
            var folder = folderRepository.GetFolderByInfoId(id);
            if (folder != null)
            {
                var fileInfos = folder.FileInfos;
                var parentFolder = folderRepository.Find(x => x.Id == parentId && x.IsDelete == false);
                if (parentFolder != null)
                {
                    var fileInfo = new FileInfo()
                    {
                        FolderId = parentFolder.Id,
                        FileBinaryId = fileInfos[0].FileBinaryId,
                        FileName = fileInfos[0].FileName,
                        FilePath = parentFolder.FolderPath + "/" + fileInfos[0].FileName,
                        IsDelete = false,
                        FileExtension = fileInfos[0].FileExtension,
                        ContentType = fileInfos[0].ContentType,
                        ExtendInfo = fileInfos[0].ExtendInfo,
                        MD5 = fileInfos[0].MD5,
                        FileSize = fileInfos[0].FileSize,
                        FileSizeByte = fileInfos[0].FileSizeByte,
                        CreatedTime = currentTime,
                        UpdatedTime = currentTime
                    };
                    parentFolder.FilesCount++;
                    parentFolder.UpdatedTime = currentTime;
                    parentFolder.FileInfos.Add(fileInfo);
                    folderRepository.Update(parentFolder);
                }
            }
        }

        /// <summary>
        /// 添加新的文件夹
        /// </summary>
        /// <param name="parentId">父文件夹ID</param>
        /// <param name="folderName">文件夹名称</param>
        [Transaction]
        [DataSession]
        public void AddFolder(int parentId, string folderName)
        {
            var currentTime = DateTime.Now;
            var parentFolder = folderRepository.Find(x => x.Id == parentId && x.IsDelete == false);
            if (parentFolder != null)
            {
                parentFolder.FoldersCount++;
                parentFolder.UpdatedTime = currentTime;
                folderRepository.Update(parentFolder);
                var folder = new Folder();
                folder.ParentId = parentId;
                folder.FolderName = folderName;
                folder.FolderPath = this.GetFilePath(folderName, parentFolder.FolderPath);
                folder.CreatedTime = currentTime;
                folder.UpdatedTime = currentTime;
                folderRepository.Save(folder);
            }
        }

        /// <summary>
        /// 根据文件夹ID，删除文件夹及子文件/子文件夹
        /// </summary>
        /// <param name="id">文件/文件夹ID</param>
        [Transaction]
        [DataSession]
        public void DeleteFolder(int id)
        {
            var currentTime = DateTime.Now;
            var folder = folderRepository.Find(x => x.Id == id && x.IsDelete == false);
            this.DeleteFolder(folder);
            if (folder != null)
            {
                var parentFolder = folderRepository.Find(x => x.Id == folder.ParentId && x.IsDelete == false);
                if (parentFolder != null)
                {
                    parentFolder.FoldersCount = parentFolder.FoldersCount > 0 ? parentFolder.FoldersCount - 1 : 0;
                    parentFolder.UpdatedTime = currentTime;
                    folderRepository.Update(parentFolder);
                }
            }
        }

        /// <summary>
        /// 根据文件ID，删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        [DataSession]
        public void DeleteFile(int id)
        {
            var currentTime = DateTime.Now;
            var folder = folderRepository.GetFolderByInfoId(id);
            var fileInfos = folder.FileInfos;
            fileInfos[0].IsDelete = true;
            fileInfos[0].UpdatedTime = currentTime;
            folder.FilesCount = folder.FilesCount > 0 ? folder.FilesCount - 1 : 0;
            folder.UpdatedTime = currentTime;
            folderRepository.Update(folder);
        }

        /// <summary>
        /// 将文件夹，移动到新的文件夹下面
        /// </summary>
        /// <param name="id">文件夹ID</param>
        /// <param name="parentId">父文件夹ID</param>
        [Transaction]
        [DataSession]
        public void MoveFolder(int id, int parentId)
        {
            var currentTime = DateTime.Now;
            var folder = folderRepository.Find(x => x.Id == id && x.IsDelete == false);
            if (folder != null)
            {
                var oldParentFolder = folderRepository.Find(x => x.Id == folder.ParentId && x.IsDelete == false);
                var parentFolder = folderRepository.Find(x => x.Id == parentId && x.IsDelete == false);
                if (parentFolder != null)
                {
                    folder.ParentId = parentFolder.Id;
                    parentFolder.FoldersCount++;
                    oldParentFolder.FoldersCount = oldParentFolder.FoldersCount > 0 ? oldParentFolder.FoldersCount - 1 : 0;
                    oldParentFolder.UpdatedTime = currentTime;
                    parentFolder.UpdatedTime = currentTime;
                    folderRepository.Update(oldParentFolder);
                    folderRepository.Update(parentFolder);
                    this.MoveFolderPath(folder, parentFolder);
                }
            }
        }

        /// <summary>
        /// 将文件，移动到新的文件夹下面
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <param name="parentId">父文件夹ID</param>
        [Transaction]
        [DataSession]
        public void MoveFile(int id, int parentId)
        {
            var currentTime = DateTime.Now;
            var oldParentFolder = folderRepository.GetFolderByInfoId(id);
            if (oldParentFolder != null)
            {
                var fileInfo = oldParentFolder.FileInfos[0];
                var parentFolder = folderRepository.Find(x => x.Id == parentId && x.IsDelete == false);
                if (parentFolder != null)
                {
                    //更新原父文件夹
                    oldParentFolder.FilesCount = oldParentFolder.FilesCount > 0 ? oldParentFolder.FilesCount - 1 : 0;
                    oldParentFolder.UpdatedTime = currentTime;
                    folderRepository.Update(oldParentFolder);
                    //更新新的父文件夹
                    fileInfo.FolderId = parentFolder.Id;
                    fileInfo.FilePath = parentFolder.FolderPath + "/" + fileInfo.FileName;
                    parentFolder.FileInfos.Add(fileInfo);
                    parentFolder.FilesCount++;
                    parentFolder.UpdatedTime = currentTime;
                    folderRepository.Update(parentFolder);
                }
            }
        }

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="id">文件夹ID</param>
        /// <param name="folderName">文件夹新名称</param>
        [Transaction]
        [DataSession]
        public void RenameFolder(int id, string folderName)
        {
            var folder = folderRepository.Find(x => x.Id == id && x.IsDelete == false);
            if (folder != null)
            {
                folder.FolderName = folderName;
                var parentFolder = folderRepository.Find(x => x.Id == folder.ParentId && x.IsDelete == false);
                this.RenameFolder(folder, parentFolder);
            }
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <param name="fileName">文件新名称</param>
        [Transaction]
        [DataSession]
        public void RenameFile(int id, string fileName)
        {
            var currentTime = DateTime.Now;
            var folder = folderRepository.GetFolderByInfoId(id);
            if (folder != null)
            {
                var fileInfo = folder.FileInfos[0];
                fileInfo.FileName = fileName;
                fileInfo.FilePath = folder.FolderPath + "/" + fileName;
                fileInfo.UpdatedTime = currentTime;
                folder.UpdatedTime = currentTime;
                folderRepository.Update(folder);
            }
        }

        #region 私有方法
        /// <summary>
        /// 递归重命名文件/文件夹
        /// </summary>
        /// <param name="file">文件/文件夹</param>
        /// <param name="parentFolder">父文件/父文件夹</param>
        private void RenameFolder(Folder folder, Folder parentFolder)
        {
            var currentTime = DateTime.Now;
            if (folder != null && parentFolder != null)
            {
                folder.UpdatedTime = currentTime;
                folder.FolderPath = parentFolder.FolderPath + "/" + folder.FolderName;
                var childFolderList = folderRepository.GetList(x => x.ParentId == folder.Id && x.IsDelete == false);
                foreach (var child in childFolderList)
                {
                    this.RenameFolder(child, folder);
                }
                foreach (var fileInfo in folder.FileInfos)
                {
                    fileInfo.UpdatedTime = currentTime;
                    fileInfo.FilePath = folder.FolderPath + "/" + fileInfo.FileName;
                }
                folderRepository.Update(folder);
            }
        }

        /// <summary>
        /// 递归获取子文件夹，已使顺序正确，适应roxyfileman的前端展现
        /// </summary>
        /// <param name="folderList">文件夹列表</param>
        /// <param name="list">总列表</param>
        /// <param name="result">结果列表</param>
        /// <returns></returns>
        private List<CustomFolderInfo> GetList(IList<Folder> folderList, IList<Folder> list, List<CustomFolderInfo> result)
        {
            foreach (var item in folderList)
            {
                var folderInfo = new CustomFolderInfo();
                folderInfo.id = item.Id.ToString();
                folderInfo.p = item.FolderPath;
                folderInfo.f = item.FilesCount.ToString();
                folderInfo.d = item.FoldersCount.ToString();
                result.Add(folderInfo);
                this.GetList(list.Where(x => x.ParentId == item.Id).ToList(), list, result);
            }
            return result;
        }

        /// <summary>
        /// 递归重命名文件/文件夹
        /// </summary>
        /// <param name="file">文件/文件夹</param>
        /// <param name="parentFolder">父文件/父文件夹</param>
        private void MoveFolderPath(Folder folder, Folder parentFolder)
        {
            var currentTime = DateTime.Now;
            if (folder != null && parentFolder != null)
            {
                folder.FolderPath = parentFolder.FolderPath + "/" + folder.FolderName;
                folder.UpdatedTime = currentTime;
                foreach (var fileInfo in folder.FileInfos)
                {
                    fileInfo.FilePath = folder.FolderPath + "/" + fileInfo.FileName;
                    fileInfo.UpdatedTime = currentTime;
                }
                folderRepository.Update(folder);
                var childFolderList = folderRepository.GetList(x => x.ParentId == folder.Id && x.IsDelete == false);
                foreach (var child in childFolderList)
                {
                    this.MoveFolderPath(child, folder);
                }
            }
        }

        /// <summary>
        /// 递归删除文件/文件夹
        /// </summary>
        /// <param name="file">文件/文件夹</param>
        private void DeleteFolder(Folder folder)
        {
            var currentTime = DateTime.Now;
            if (folder != null)
            {
                folder.IsDelete = true;
                folder.UpdatedTime = currentTime;
                foreach (var fileInfo in folder.FileInfos)
                {
                    fileInfo.IsDelete = true;
                    fileInfo.UpdatedTime = currentTime;
                }
                folderRepository.Update(folder);
                var childFolderList = folderRepository.GetList(x => x.ParentId == folder.Id && x.IsDelete == false);
                foreach (var child in childFolderList)
                {
                    this.DeleteFolder(child);
                }
            }
        }

        /// <summary>
        /// 获取文件虚拟路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="folderPath">文件夹路径</param>
        /// <returns></returns>
        private string GetFilePath(string fileName, string folderPath)
        {
            var filePath = "/我的文件" + "/" + fileName;
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                filePath = folderPath + "/" + fileName;
            }
            return filePath;
        }
        #endregion
    }
}

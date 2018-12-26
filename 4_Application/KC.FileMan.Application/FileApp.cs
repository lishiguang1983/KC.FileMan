using KC.FileMan.Domain.Entities;
using KC.FileMan.IApplication;
using KC.FileMan.Infrastructure;
using KC.FileMan.IRepository;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;

namespace KC.FileMan.Application
{
    public class FileApp : AppBase, IFileApp
    {
        #region 属性
        private IFolderRepository folderRepository;
        private IFileRepository fileRepository;
        #endregion

        #region 构造函数
        public FileApp(IFolderRepository folderRepository, IFileRepository fileRepository)
        {
            this.folderRepository = folderRepository;
            this.fileRepository = fileRepository;
        }
        #endregion

        /// <summary>
        /// 文件上传至数据库
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="folderId">文件夹ID</param>
        [Transaction(IsolationLevel.ReadCommitted)]
        [DataSession]
        public bool Upload(IFormFileCollection files, int folderId)
        {
            var currentTime = DateTime.Now;
            //获取父文件夹
            Folder folder = null;
            var folderPath = string.Empty;
            if (folderId > 0)
            {
                folder = folderRepository.Find(x => x.Id == folderId && x.IsDelete == false);
                if (folder != null)
                {
                    folderPath = folder.FolderPath;
                    foreach (var file in files)
                    {
                        //读取文件流
                        var stream = file.OpenReadStream();
                        byte[] bytes = new byte[file.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        var fileName = file.FileName;
                        var fileNameExtension = new System.IO.FileInfo(fileName).Extension;
                        //创建并保存文件流
                        var fileBinary = new FileBinary();
                        fileBinary.Binary = bytes;
                        //创建并保存文件信息
                        var fileInfo = new FileInfo();
                        fileInfo.FolderId = folderId;
                        fileInfo.FileName = fileName;
                        fileInfo.FilePath = this.GetFilePath(fileName, folderPath);
                        fileInfo.IsDelete = false;
                        fileInfo.FileExtension = fileNameExtension;
                        fileInfo.ContentType = file.ContentType;
                        fileInfo.ExtendInfo = this.GetExtendInfo(fileNameExtension, stream);
                        fileInfo.MD5 = string.Empty;
                        fileInfo.FileSize = this.GetFileSizeStr(file.Length);
                        fileInfo.FileSizeByte = file.Length;
                        fileInfo.CreatedTime = currentTime;
                        fileInfo.UpdatedTime = currentTime;
                        fileBinary.FileInfos.Add(fileInfo);
                        fileRepository.Save(fileBinary);
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// 根据文件ID获取文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataSession]
        public FileBinary GetFileBinaryByInfoId(int id)
        {
            var fileBinary = fileRepository.GetFileBinaryByInfoId(id);
            return fileBinary;
        }
        

        #region 私有方法
        /// <summary>
        /// 获取文件扩展信息字符串
        /// </summary>
        /// <param name="fileNameExtension">文件扩展名</param>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        private string GetExtendInfo(string fileNameExtension, System.IO.Stream stream)
        {
            string extendInfo = string.Empty;
            //如果文件类型是图片类型，则获取图片的宽高
            if (this.GetFileType(fileNameExtension) == "image")
            {
                var extendInfoObj = new ExtendInfoObj();
                var imgBmp = new Bitmap(stream);
                extendInfoObj.Width = imgBmp.Width;
                extendInfoObj.Height = imgBmp.Height;
                extendInfo = JsonConvert.SerializeObject(extendInfoObj);
            }
            return extendInfo;
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        private string GetFileType(string ext)
        {
            string ret = "file";
            ext = ext.ToLower();
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                ret = "image";
            else if (ext == ".swf" || ext == ".flv")
                ret = "flash";
            return ret;
        }

        /// <summary>
        /// 将文件实际大小转换为字符串形式
        /// </summary>
        /// <param name="fileSize">文件字节流大小</param>
        /// <returns></returns>
        private string GetFileSizeStr(long fileSize)
        {
            if (1024 * 1024 * 1024 <= fileSize)
            {
                return string.Format("{0}GB", (fileSize / (float)(1024 * 1024 * 1024)).ToString("#.##"));
            }
            else if (1024 * 1024 <= fileSize)
            {
                return string.Format("{0}MB", (fileSize / (float)(1024 * 1024)).ToString("#.##"));
            }
            else if (1024 <= fileSize)
            {
                return string.Format("{0}KB", (fileSize / (float)(1024)).ToString("#.##"));
            }
            return string.Format("{0}B", fileSize);
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

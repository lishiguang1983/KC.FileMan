using KC.FileMan.Common.General;
using KC.FileMan.Domain;
using KC.FileMan.IApplication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KC.FileMan.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class FileController : ControllerBase
    {
        #region 属性
        private IFileApp fileApp;
        private IFolderApp folderApp;
        #endregion

        #region 构造函数
        public FileController(IFileApp fileApp,
            IFolderApp folderApp)
        {
            this.fileApp = fileApp;
            this.folderApp = folderApp;
        }
        #endregion


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        [Route("{id}")]
        [Route("{id}/{isDownLoad}")]
        public ActionResult Get(int id, bool isDownLoad)
        {
            var fileBinary = fileApp.GetFileBinaryByInfoId(id);
            if (isDownLoad)
            {
                return File(fileBinary.Binary, MimeType.ApplicationForceDownload, fileBinary.FileInfos[0].FileName);
            }
            else
            {
                return File(fileBinary.Binary, fileBinary.FileInfos[0].ContentType);
            }
        }

        // POST api/values
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public ActionResult Post()
        {
            var result = new ResponseResultBase();
            try
            {
                var action = HttpContext.Request.Query["a"].ToString();
                var fileId = 0;
                var pFileId = 0;
                var n = string.Empty;
                if (HttpContext.Request.Form != null)
                {
                    if (!string.IsNullOrWhiteSpace(HttpContext.Request.Form["fileId"]))
                    {
                        fileId = Convert.ToInt32(HttpContext.Request.Form["fileId"]);
                    }
                    if (!string.IsNullOrWhiteSpace(HttpContext.Request.Form["pFileId"]))
                    {
                        pFileId = Convert.ToInt32(HttpContext.Request.Form["pFileId"]);
                    }
                    if (!string.IsNullOrWhiteSpace(HttpContext.Request.Form["n"]))
                    {
                        n = HttpContext.Request.Form["n"];
                    }
                }
                if (!string.IsNullOrWhiteSpace(action))
                {
                    switch (action)
                    {
                        case "DirList":
                            var dirList = folderApp.FolderInfoList(fileId);
                            return Content(JsonConvert.SerializeObject(dirList));
                        case "FileList":
                            var fileList = folderApp.FileInfoList(fileId);
                            return Content(JsonConvert.SerializeObject(fileList));
                        case "CopyDir":
                            folderApp.CopyFolder(fileId, pFileId);
                            break;
                        case "CopyFile":
                            folderApp.CopyFile(fileId, pFileId);
                            break;
                        case "CreateFolder":
                            folderApp.AddFolder(fileId, n);
                            break;
                        case "DeleteFolder":
                            folderApp.DeleteFolder(fileId);
                            break;
                        case "DeleteFile":
                            folderApp.DeleteFile(fileId);
                            break;
                        //    case "DOWNLOADDIR":
                        //        DownloadDir(_context.Request["d"]);
                        //        break;
                        case "MoveDir":
                            folderApp.MoveFolder(fileId, pFileId);
                            break;
                        case "MoveFile":
                            folderApp.MoveFile(fileId, pFileId);
                            break;
                        case "RenameFolder":
                            folderApp.RenameFolder(fileId, n);
                            break;
                        case "RenameFile":
                            folderApp.RenameFile(fileId, n);
                            break;
                        //    case "GENERATETHUMB":
                        //        int w = 140, h = 0;
                        //        int.TryParse(_context.Request["width"].Replace("px", ""), out w);
                        //        int.TryParse(_context.Request["height"].Replace("px", ""), out h);
                        //        ShowThumbnail(_context.Request["f"], w, h);
                        //        break;
                        case "Upload":
                            var isSuccess = fileApp.Upload(HttpContext.Request.Form.Files, fileId);
                            if (isSuccess)
                            {
                                folderApp.UpdateFilesCount(fileId);
                            }
                            break;
                        default:
                            result.IsSuccess = false;
                            result.Message = "该方法不存在";
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
            }
            return Content(JsonConvert.SerializeObject(result));
        }
    }
}

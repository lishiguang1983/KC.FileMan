using KC.FileMan.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace KC.FileMan.WebApi
{
    public class ModelConfig
    {
        public static void BuildSessionFactories(IHostingEnvironment _env,string configFile)
        {
            string webRootPath = _env.WebRootPath;
            string contentRootPath = _env.ContentRootPath;

            string rootdir = AppContext.BaseDirectory;
            DirectoryInfo Dir = Directory.GetParent(rootdir);
            string root = Dir.Parent.Parent.FullName;

            string filePath = contentRootPath + configFile;
            ModelBuilder.ConfigFiles.Add("KC_FileMan", filePath);
            ModelBuilder.BuildSessionFactory();
        }
    }
}
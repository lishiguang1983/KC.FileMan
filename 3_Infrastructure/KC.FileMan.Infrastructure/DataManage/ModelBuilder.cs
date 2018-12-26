using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace KC.FileMan.Infrastructure
{
    /// <summary>
    /// 数据库模型生成器
    /// </summary>
    public sealed class ModelBuilder
    {
        public static Dictionary<string, string> ConfigFiles;

        private static Dictionary<string, ISessionFactory> factories;

        public static bool ExportEnabled { get; set; }

        public static string ExportPath { get; set; }

        static ModelBuilder()
        {
            ConfigFiles = new Dictionary<string, string>();
            factories = new Dictionary<string, ISessionFactory>();
        }

        public static void BuildSessionFactory()
        {
            if (ConfigFiles.Count == 0)
            {
                return;
            }

            foreach (var key in ConfigFiles.Keys)
            {
                string[] configData = ConfigFiles[key].Split(';');
                if (configData.Length < 2)
                {
                    continue;
                }
                string configFileName = configData[0];
                string domainAssemblyName = configData[1];

                var config = new Configuration().Configure(configFileName);
                config = Fluently.Configure(config).Mappings(m =>
                {
                    var container = m.FluentMappings.AddFromAssembly(Assembly.Load(domainAssemblyName));
                    if (ExportEnabled)
                    {
                        ExportPath = ExportPath ?? AppDomain.CurrentDomain.BaseDirectory;
                        string exportFolder = ExportPath + "\\" + key;
                        if (!Directory.Exists(exportFolder))
                        {
                            Directory.CreateDirectory(exportFolder);
                        }
                        container.ExportTo(exportFolder);
                    }
                }).BuildConfiguration();

                var sessionFactory = config.BuildSessionFactory();
                factories[key] = sessionFactory;
            }
        }

        public static ISessionFactory GetSessionFactory(string key)
        {
            if (!factories.ContainsKey(key))
            {
                throw new Exception("Can't found any session factory");
            }

            return factories[key];
        }
    }
}

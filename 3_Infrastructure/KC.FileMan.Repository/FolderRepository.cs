using KC.FileMan.Domain;
using KC.FileMan.Domain.Entities;
using KC.FileMan.IRepository;
using NHibernate;
using System.Collections.Generic;

namespace KC.FileMan.Repository
{

    public class FolderRepository : RepositoryBase<Folder>, IFolderRepository
    {
        public FolderRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        /// <summary>
        /// 根据子文件ID获取文件夹
        /// </summary>
        /// <param name="fileInfoId">子文件ID</param>
        /// <returns></returns>
        public Folder GetFolderByInfoId(int fileInfoId)
        {
            FileInfo fileInfo = null;
            var folder = DataSession.QueryOver<Folder>()
                .JoinAlias(x => x.FileInfos, () => fileInfo, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                .Where(x => x.IsDelete == false)
                .And(() => fileInfo.Id == fileInfoId && fileInfo.IsDelete == false)
                .SingleOrDefault();
            return folder;
        }

        /// <summary>
        /// 根据文件夹ID获取文件夹及子文件信息
        /// </summary>
        /// <param name="folderId">文件夹ID</param>
        /// <returns></returns>
        public Folder GetFolderAndFileInfos(int folderId)
        {
            FileInfo fileInfo = null;
            var folder = DataSession.QueryOver<Folder>()
               .JoinAlias(x => x.FileInfos, () => fileInfo, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
               .Where(x =>x.Id== folderId&& x.IsDelete == false)
               .And(() =>fileInfo.IsDelete == false)
               .SingleOrDefault();
            return folder;
        }
    }
}

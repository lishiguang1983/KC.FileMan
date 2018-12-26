using KC.FileMan.Domain.Entities;
using KC.FileMan.IRepository;
using NHibernate;

namespace KC.FileMan.Repository
{

    public class FileRepository : RepositoryBase<FileBinary>, IFileRepository
    {
        public FileRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public FileBinary GetFileBinaryByInfoId(int fileInfoId)
        {
            FileInfo fileInfo = null;
            var fileBase = DataSession.QueryOver<FileBinary>()
                .JoinAlias(x => x.FileInfos, () => fileInfo, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                .Where(() => fileInfo.Id == fileInfoId && fileInfo.IsDelete == false)
                .SingleOrDefault();
            return fileBase;
        }
    }
}

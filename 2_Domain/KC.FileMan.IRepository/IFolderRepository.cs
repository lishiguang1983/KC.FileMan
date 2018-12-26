using KC.FileMan.Domain.Entities;

namespace KC.FileMan.IRepository
{
    public interface IFolderRepository : IRepositoryBase<Folder>
    {
        /// <summary>
        /// 根据子文件ID获取文件夹及子文件
        /// </summary>
        /// <param name="fileInfoId">子文件ID</param>
        Folder GetFolderByInfoId(int fileInfoId);

        /// <summary>
        /// 根据文件夹ID获取文件夹及子文件信息
        /// </summary>
        /// <param name="folderId">文件夹ID</param>
        /// <returns></returns>
        Folder GetFolderAndFileInfos(int folderId);
    }
}

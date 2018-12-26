using KC.FileMan.Domain.Entities;

namespace KC.FileMan.IRepository
{
    public interface IFileRepository : IRepositoryBase<FileBinary>
    {
        FileBinary GetFileBinaryByInfoId(int fileInfoId);
    }
}

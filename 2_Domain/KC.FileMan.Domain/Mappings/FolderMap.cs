using FluentNHibernate.Mapping;
using KC.FileMan.Domain.Entities;

namespace KC.FileMan.Domain.Mappings
{
    public class FolderMap : ClassMap<Folder>
    {
        public FolderMap()
        {
            LazyLoad();

            Table("`Folder`");
            Id(x => x.Id, "Id").GeneratedBy.Native();
            Map(x => x.ParentId, "ParentId");
            Map(x => x.FolderName, "FolderName");
            Map(x => x.FolderSize, "FolderSize");
            Map(x => x.FolderSizeByte, "FolderSizeByte");
            Map(x => x.FolderPath, "FolderPath");
            Map(x => x.FilesCount, "FilesCount");
            Map(x => x.FoldersCount, "FoldersCount");
            Map(x => x.IsDelete, "IsDelete");
            Map(x => x.CreatedTime, "CreatedTime");
            Map(x => x.UpdatedTime, "UpdatedTime");

            HasMany(x => x.FileInfos)
                .KeyColumn("FolderId")
                .Cascade.All()
                .LazyLoad();
        }
    }
}

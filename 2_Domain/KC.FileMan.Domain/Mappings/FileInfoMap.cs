using FluentNHibernate.Mapping;
using KC.FileMan.Domain.Entities;

namespace KC.FileMan.Domain.Mappings
{
    public class FileInfoMap : ClassMap<FileInfo>
    {
        public FileInfoMap()
        {
            LazyLoad();

            Table("`FileInfo`");
            Id(x => x.Id, "Id").GeneratedBy.Native();
            Map(x => x.FolderId, "FolderId");
            Map(x => x.FileBinaryId, "FileBinaryId");
            Map(x => x.FileName, "FileName");
            Map(x => x.FilePath, "FilePath");
            Map(x => x.IsDelete, "IsDelete");
            Map(x => x.FileExtension, "FileExtension");
            Map(x => x.ContentType, "ContentType");
            Map(x => x.ExtendInfo, "ExtendInfo");
            Map(x => x.MD5, "MD5");
            Map(x => x.FileSize, "FileSize");
            Map(x => x.FileSizeByte, "FileSizeByte");
            Map(x => x.CreatedTime, "CreatedTime");
            Map(x => x.UpdatedTime, "UpdatedTime");

            References(x => x.FileBinary, "FileBinaryId")
                .ReadOnly()
                .LazyLoad();
        }
    }
}

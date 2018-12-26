using FluentNHibernate.Mapping;
using KC.FileMan.Domain.Entities;

namespace KC.FileMan.Domain.Mappings
{
    public class FileBinaryMap : ClassMap<FileBinary>
    {
        public FileBinaryMap()
        {
            LazyLoad();

            Table("`FileBinary`");
            Id(x => x.Id, "Id").GeneratedBy.Native();
            Map(x => x.Binary, "Binary").Length(int.MaxValue);

            HasMany(x => x.FileInfos)
               .KeyColumn("FileBinaryId")
               .Cascade.All()
               .LazyLoad();
        }
    }
}

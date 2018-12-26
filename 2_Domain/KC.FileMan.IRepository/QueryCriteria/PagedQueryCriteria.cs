namespace KC.FileMan.IRepository.QueryCriteria
{
    public class PagedQueryCriteria
    {
        public PagedQueryCriteria()
        {
            this.Page = 1;
            this.PageSize = 15;
        }
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}

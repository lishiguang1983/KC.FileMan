using System.ComponentModel.DataAnnotations;

namespace KC.FileMan.Web.ViewModels
{
    /// <summary>
    /// 文章新增
    /// </summary>
    public class ArticleViewModel
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(14, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 5)]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string ArticleContent { get; set; }
    }
}

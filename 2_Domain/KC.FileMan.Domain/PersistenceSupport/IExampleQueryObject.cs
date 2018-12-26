using System.Collections.Generic;

namespace KC.FileMan.Domain
{
    /// <summary>
    /// Interface of persistent object which is used as example for query.
    /// </summary>
    public interface IExampleQueryObject
    {
        /// <summary>
        /// Gets the list of property which value of null is included as query criteria.
        /// </summary>
        /// <value>The list of property which value of null is included as query criteria.</value>
        IList<string> IncludeNullPropertyList { get; }
    }
}

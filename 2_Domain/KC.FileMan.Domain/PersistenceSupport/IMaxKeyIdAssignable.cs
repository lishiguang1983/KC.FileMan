using System;

namespace KC.FileMan.Domain
{
    /// <summary>
    /// Interface of persistent object which key ID can be generated.
    /// </summary>
    public interface IMaxKeyIdAssignable
    {
        /// <summary>
        /// Gets the type for max key ID generation.
        /// </summary>
        /// <value>The type for max key ID generation.</value>
        Type TypeForMaxKeyId { get; }
    }
}

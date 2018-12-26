using System;

namespace KC.FileMan.Domain
{
    /// <summary>
    /// Track state of persistent object.
    /// </summary>
    [Serializable]
    public class PersistentTrackState
    {
        /// <summary>
        /// Gets or sets the state of the previous.
        /// </summary>
        /// <value>The state of the previous.</value>
        public object[] PreviousState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state property names.
        /// </summary>
        /// <value>The state property names.</value>
        public string[] StatePropertyNames
        {
            get;
            set;
        }
    }
}

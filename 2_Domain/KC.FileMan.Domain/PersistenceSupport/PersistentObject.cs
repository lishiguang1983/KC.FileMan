using System;
using System.Collections.Generic;

namespace KC.FileMan.Domain
{
    /// <summary>
    /// Domain objects with 32-bit ID.
    /// </summary>
    public abstract class PersistentObject : PersistentObjectWithTypedId<int> { }

    /// <summary>
    /// Domain objects with 64-bit ID.
    /// Since nearly all of the domain objects you create will have a type of int ID, this 
    /// most freqently used base PersistentObject32 leverages this assumption.  If you want a persistent 
    /// object with a type other than int, such as string, then use 
    /// <see cref="PersistentObjectwithTypedId" />.
    /// </summary>
    public abstract class PersistentObject64 : PersistentObjectWithTypedId<long> { }

    /// <summary>
    /// For a discussion of this object, see 
    /// http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
    /// </summary>
    public abstract class PersistentObjectWithTypedId<IdT> : IMaxKeyIdAssignable, IExampleQueryObject
    {
        protected IdT id = default(IdT);

        protected PersistentTrackState trackState;

        protected string objectTypeName;

        /// <summary>
        /// The list of property which value of null is included as query criteria.
        /// </summary>
        protected List<string> includeNullPropertyList = null;

        /// <summary>
        /// ID may be of type string, int, custom type, etc.
        /// Setter is protected to allow unit tests to set this property via reflection and to allow 
        /// domain objects more flexibility in setting this for those objects with assigned IDs.
        /// It's virtual to allow NHibernate-backed objects to be lazily loaded.
        /// </summary>
        public virtual IdT Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets the type for max key ID generation.
        /// </summary>
        /// <value>The type for max key ID generation.</value>
        Type IMaxKeyIdAssignable.TypeForMaxKeyId
        {
            get
            {
                return this.TypeForMaxKeyId;
            }
        }

        /// <summary>
        /// Gets the list of property which value of null is included as query criteria.
        /// </summary>
        /// <value>
        /// The list of property which value of null is included as query criteria.
        /// </value>
        IList<string> IExampleQueryObject.IncludeNullPropertyList
        {
            get
            {
                if (this.includeNullPropertyList == null)
                {
                    this.includeNullPropertyList = new List<string>();
                }

                return this.includeNullPropertyList;
            }
        }

        /// <summary>
        /// Gets the type for max key ID generation.
        /// </summary>
        /// <value>The type for max key ID generation.</value>
        protected virtual Type TypeForMaxKeyId
        {
            get
            {
                return this.GetType();
            }
        }

        /// <summary>
        /// Gets the name of the object type.
        /// </summary>
        /// <value>The name of the object type.</value>
        protected virtual string ObjectTypeName
        {
            get
            {
                if (this.objectTypeName == null)
                {
                    this.objectTypeName = this.GetType().Name;
                }

                return this.objectTypeName;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/> for logging.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/> for logging.
        /// </returns>
        public virtual string ToLogString()
        {
            return this.ObjectTypeName + " #" + this.id.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Entities
{
    /// <summary>
    /// Base class for all Blacksmith Press Diabetes entities.
    /// </summary>
    /// <typeparam name="KeyType">The type of the primary key.</typeparam>
    /// <seealso cref="BlacksmithPress.Diabetes.Types.IEntity{KeyType}" />
    public abstract class Entity<KeyType> : IEntity<KeyType> where KeyType : struct
    {
        /// <summary>
        /// Gets or sets the entity's identifier (primary key).
        /// </summary>
        /// <value>The identifier.</value>
        public virtual KeyType Id { get; set; }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            var that = obj as IEntity<KeyType>;
            if (that == null) return false;

            return ToString() == that.ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{this.GetType().Name}/{Id}";
        }
    }
}

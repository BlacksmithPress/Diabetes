using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Entities
{
    public abstract class Entity<KeyType> : IEntity<KeyType> where KeyType : struct
    {
        public virtual KeyType Id { get; set; }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            var that = obj as IEntity<KeyType>;
            if (that == null) return false;

            return ToString() == that.ToString();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}/{Id}";
        }
    }
}

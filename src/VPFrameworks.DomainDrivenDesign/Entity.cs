using System;
using System.Collections.Generic;
using System.Text;

namespace Frameworks.DDD
{
    public class Entity<TId> : IEqualityComparer<Entity<TId>>
    {

        private DateTime? updated;

        public Entity(HashSet<string> tags)
        {
            this.Created = DateTime.UtcNow;
            Tags = new HashSet<string>();
            Version = 0;
        }

        public TId Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated {
            get
            {

                return updated;
            }
            set
            {
                updated = value;
                Version++;
            }
        }

        public IEnumerable<string> Tags { get; set; }

        public long Version { get; set; }

        public bool Equals(Entity<TId> x, Entity<TId> y)
        {
            if (x == null || y == null)
                return false;

            if (!x.GetType().Equals(y.GetType()))
                return false;

            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Entity<TId> obj)
        {
            int hascode = obj.Id.GetHashCode();
            foreach (var tag in Tags)
            {
                hascode = hascode + tag.GetHashCode();
            }

            return hascode;
        }
    }
}

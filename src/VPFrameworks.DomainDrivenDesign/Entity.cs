using System;
using System.Collections.Generic;
using System.Text;

namespace Frameworks.DDD
{
    /// <summary>
    /// Entity DDD
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class Entity<TId> : IEqualityComparer<Entity<TId>>
    {

        private DateTime? updated;

        /// <summary>
        /// Creates a nem instance of Entity
        /// </summary>
        
        public Entity()
        {
            this.Created = DateTime.UtcNow;
            Tags = new HashSet<string>();
            Version = 0;
        }


        /// <summary>
        /// Gets or Sets the Id
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? Updated
        {
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
        /// <summary>
        /// Gets and sets Tags
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Gets and sets version
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// Compares two entities
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(Entity<TId> x, Entity<TId> y)
        {
            if (x == null || y == null)
                return false;

            if (!x.GetType().Equals(y.GetType()))
                return false;

            return x.Id.Equals(y.Id);
        }

        /// <summary>
        /// Calculates the hascode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

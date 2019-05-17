using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Framework
{
    //[Serializable]
    public class EntityNotFoundException : EntityException
    {

        public string Id { get; set; }

        public string Type { get; set; }
        

        public EntityNotFoundException(string id, string type) : this(id, type, "Entity not found")
        {
            
        }

        public EntityNotFoundException(string id, string type, string message)
            : base(null, message)
        {
            this.Id = id;
            this.Type = type;
        }

        public EntityNotFoundException(string id, string type, string message, Exception inner) : base(null, message, inner)
        {
            this.Id = id;
            this.Type = type;
        }

        
    }
}

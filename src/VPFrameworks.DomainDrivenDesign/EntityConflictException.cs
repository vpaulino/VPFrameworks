using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Framework
{
    //[Serializable]
    public class EntityConflictException : EntityException
    {
         
       

        public EntityConflictException(object entity) : base(entity)
        {
        
        }

        public EntityConflictException(object entity, string message)
            : base(entity, message)
        {

        }

        public EntityConflictException(object entity, string message, Exception inner) : base(entity, message, inner)
        {
        
        }

        //protected EntityConflictException(
        //  System.Runtime.Serialization.SerializationInfo info,
        //  System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context) 
        //{
        
        //}
    }
}

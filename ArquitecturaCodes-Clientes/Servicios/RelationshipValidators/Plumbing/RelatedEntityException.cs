using ARQ.Recursos;
using System;

namespace ARQ.Servicios.RelationshipValidators
{
    public class RelatedEntityException : Exception
    {
        public RelatedEntityException(string entityName, string entityDetails) : base(Global.RelatedEntityExceptionMessage + entityName + " " + entityDetails)
        { 

        }
    }
}

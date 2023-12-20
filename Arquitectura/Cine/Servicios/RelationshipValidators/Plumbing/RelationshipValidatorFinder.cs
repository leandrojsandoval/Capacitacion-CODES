using System;
using System.Linq;
using ARQ.Entidades;
using System.Reflection;

namespace ARQ.Servicios.RelationshipValidators
{
    public static class RelationshipValidatorFinder<T> where T: EntidadBase
    {
        public static RelationshipValidator<T> GetValidator(T entidad)
        {   
            foreach (Type validatorType in Assembly.GetAssembly(typeof(RelationshipValidator<T>)).GetTypes()
                .Where(aType => aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(RelationshipValidator<T>))))
            {
                return (RelationshipValidator<T>) Activator.CreateInstance(validatorType, new object[] { } );
            }

            return null;
        }
    }

}

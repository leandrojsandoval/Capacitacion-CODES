using ARQ.Datos.Interfaces;
using ARQ.Entidades;
using ARQ.Recursos;

namespace ARQ.Servicios.RelationshipValidators {
    public class ClienteRelationshipValidator : RelationshipValidator<Cliente> {

        public override void Validate (Cliente cliente, IDatosGenerico datosGenerico) {
            ///Se deja como ejemplo
            /*
            var rod = datosGenerico.Get<Rodillo>(x => x.MaterialRodillo != null && x.MaterialRodillo.Id == material.Id);

            if (rod != null){
                throw new RelatedEntityException(Global.Rodillo, rod.Nombre);
            }*/
        }

    }

}

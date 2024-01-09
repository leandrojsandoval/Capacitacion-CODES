using ARQ.Web.Controllers;
using ARQ.Web.Models.Material;
using Framework.Web;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ARQ.Test
{
    public class MaterialTest
    {

        private MaterialController materialController = new MaterialController(
            TestContextBuilder.GetServicioGenerico(TestContextBuilder.GetLocalContext())
        );

        [Fact]
        public void InactivarIdInvalido ()
        {
            //Arrange
            int idInvalido = 400;


            // Act and Assert
            Assert.ThrowsAsync<Exception>(() => materialController.Inactivar(idInvalido));
        }

        [Fact]
        public void MaterialExistente()
        {
            //Arrange
            MaterialViewModel materialConNombreExistente = new() {
                nombre = "Material 1",
                descripcion = "Material con nombre existente",
                multiplicadorToneladas = 10,
                activo = true,
            };

            // Act
            Task<JsonData> resultado = materialController.Guardar(materialConNombreExistente);

            // Assert
            Assert.Equal(resultado.Result.content, JsonData.Result.ModelValidation);
        }

    }

}

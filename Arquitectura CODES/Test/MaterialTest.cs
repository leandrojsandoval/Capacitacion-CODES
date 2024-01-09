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

        private MaterialController materialController = new(
            TestContextBuilder.GetServicioGenerico(TestContextBuilder.GetLocalContext())
        );

        [Fact]
        public void InactivarIdInvalido ()
        {
            //Arrange
            int idInvalido = 400;

            // Act - Assert
            Assert.ThrowsAsync<Exception>(() => materialController.Inactivar(idInvalido));
        }

        [Fact]
        public void MaterialExistente()
        {
            //Arrange
            var resultadoEsperado = JsonData.Result.ModelValidation;
            MaterialViewModel materialConNombreExistente = new() {
                nombre = "Material 1",
                descripcion = "Material con nombre existente",
                multiplicadorToneladas = 10,
                activo = true,
            };

            // Act
            Task<JsonData> resultado = materialController.Guardar(materialConNombreExistente);
            var resultadoObtenido = resultado.Result.result;

            // Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

    }

}

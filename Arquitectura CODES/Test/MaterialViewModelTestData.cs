using ARQ.Web.Models.Material;
using Framework.Web;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ARQ.Test
{
    public class MaterialViewModelTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new()
        {
            //Material con nombre repetido
            new object[]
            {
                new MaterialViewModel
                {
                    nombre = "Material 1",
                    descripcion = "Material con nombre existente",
                    multiplicadorToneladas = 10,
                    activo = true
                },
                (int)JsonData.Result.ModelValidation
            },
            //Material posible de agregar a la base de datos (En realidad lo agrega y lo elimina)
            new object[]
            {
                new MaterialViewModel
                {
                    nombre = "Madera",
                    descripcion = "Madera de Roble",
                    multiplicadorToneladas = 50,
                    activo = true
                },
                (int)JsonData.Result.Ok
            }
        };

        public IEnumerator<object[]> GetEnumerator ()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator () => GetEnumerator();
    }

}

using Newtonsoft.Json;

namespace ProyectoSerializacion {
    internal class Program {
        static void Main ()
        {
            IList<Item> lista = new List<Item>();
            Random rand = new();

            for (int i = 0; i < 10; i++) {
                lista.Add(new Item()
                {
                    Id = rand.Next(),
                    Descripcion = string.Format("Item {0}", i),
                    FechaAlta = DateTime.Now.AddDays(i * -1)
                }) ;
            }
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
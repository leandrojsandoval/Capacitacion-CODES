using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Utils
{
    public static class MigrationUtils
    {
        public static void RunScripts(MigrationBuilder migrationBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames =
                        assembly.GetManifestResourceNames().
                        Where(str => str.EndsWith(".sql")).OrderBy(str=> str);

            foreach (string resourceName in resourceNames)
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new(stream))
                {
                    string sql = reader.ReadToEnd();
                    migrationBuilder.Sql(sql);
                }
            }
        }
    }
}

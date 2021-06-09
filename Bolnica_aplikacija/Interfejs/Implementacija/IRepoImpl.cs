using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Interfejs.Implementacija
{
    class IRepoImpl<T> : IRepo<T>
    {
        public List<T> ucitajSve(String putanja)
        {
            List<T> lista;
            try
            {
                lista = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(putanja));
            }
            catch (Exception e)
            {
                lista = new List<T>();
            }

            return lista;
        }

        public void upisi(List<T> lista,  String putanja)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(lista, formatiranje);
            File.WriteAllText(putanja, jsonString);
        }
    }
}

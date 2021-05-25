using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class KonvertStavka : JsonConverter<Stavka>
    {
        public override Stavka Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Expected StartObject token");

			Stavka s = new Stavka();
			var startDepth = reader.CurrentDepth;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject && startDepth == reader.CurrentDepth)
					return s;
				Trace.WriteLine(reader.TokenType);
				/*if (reader.TokenType != JsonTokenType.PropertyName)
					throw new JsonException("Expected PropertyName token");*/
				if (reader.TokenType == JsonTokenType.EndObject)
					continue;
				var propName = reader.GetString();
				reader.Read();

				switch (propName)
				{
					case nameof(Stavka.id):
						s.id = reader.GetString();
						break;
					case nameof(Stavka.naziv):
						s.naziv = reader.GetString();
						break;
					case nameof(Stavka.kolicina):
						s.kolicina = reader.GetInt32();
						break;
					case nameof(Stavka.proizvodjac):
						s.proizvodjac = reader.GetString();
						break;
					case nameof(Stavka.prostorija):
						s.prostorija = new Prostorija { id = reader.GetString() };
						break;
					case nameof(Stavka.idBolnice):
						s.idBolnice = reader.GetString();
						break;
					case nameof(Stavka.jeStaticka):
						s.jeStaticka = reader.GetBoolean();
						break;
					case nameof(Stavka.jeLogickiObrisana):
						s.jeLogickiObrisana = reader.GetBoolean();
						break;
					case nameof(Stavka.jePotrosnaRoba):
						s.jePotrosnaRoba = reader.GetBoolean();
						break;
				}
			}

			throw new JsonException("Expected EndObject token");
		}

        public override void Write(Utf8JsonWriter writer, Stavka value, JsonSerializerOptions options)
        {
			writer.WriteStartObject();

			writer.WriteString(nameof(value.id), value.id);
			writer.WriteString(nameof(value.naziv), value.naziv);

			writer.WriteNumber(nameof(value.kolicina), value.kolicina);

			writer.WriteString(nameof(value.proizvodjac), value.proizvodjac);
			writer.WriteString(nameof(value.prostorija), value.prostorija.id);
			writer.WriteString(nameof(value.idBolnice), value.idBolnice);

			writer.WriteBoolean(nameof(value.jeStaticka), value.jeStaticka);
			writer.WriteBoolean(nameof(value.jeLogickiObrisana), value.jeLogickiObrisana);
			writer.WriteBoolean(nameof(value.jePotrosnaRoba), value.jePotrosnaRoba);

			writer.WriteEndObject();
		}
    }
}

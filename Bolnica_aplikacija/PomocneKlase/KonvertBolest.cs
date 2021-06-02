using Bolnica_aplikacija.Model;
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
    class KonvertBolest : JsonConverter<Bolest>
    {
        public override Bolest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Expected StartObject token");

			Bolest b = new Bolest();
			var startDepth = reader.CurrentDepth;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject && startDepth == reader.CurrentDepth)
					return b;
				Trace.WriteLine(reader.TokenType);
				/*if (reader.TokenType != JsonTokenType.PropertyName)
					throw new JsonException("Expected PropertyName token");*/
				if (reader.TokenType == JsonTokenType.EndObject)
					continue;
				var propName = reader.GetString();
				reader.Read();

				switch (propName)
				{
					case nameof(Bolest.id):
						b.id = reader.GetString();
						break;
					case nameof(Bolest.naziv):
						b.naziv = reader.GetString();
						break;
					case nameof(Bolest.terapija):
						b.terapija = new Terapija { id = reader.GetString() };
						break;
					case nameof(Bolest.pacijent):
						b.pacijent = new Pacijent { id = reader.GetString() };
						break;
				}
			}

			throw new JsonException("Expected EndObject token");
		}

        public override void Write(Utf8JsonWriter writer, Bolest value, JsonSerializerOptions options)
        {

			writer.WriteStartObject();
			writer.WriteString(nameof(value.id), value.id);
			writer.WriteString(nameof(value.naziv), value.naziv);

			writer.WriteString(nameof(value.terapija), value.terapija.id);
			writer.WriteString(nameof(value.pacijent), value.pacijent.id);
			
			writer.WriteEndObject();
		}
    }
}

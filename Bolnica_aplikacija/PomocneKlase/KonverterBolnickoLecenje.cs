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
    class KonverterBolnickoLecenje : JsonConverter<BolnickoLecenje>
    {
        public override BolnickoLecenje Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Expected StartObject token");

			BolnickoLecenje bLecenje = new BolnickoLecenje();
			var startDepth = reader.CurrentDepth;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject && startDepth == reader.CurrentDepth)
					return bLecenje;
				Trace.WriteLine(reader.TokenType);
				/*if (reader.TokenType != JsonTokenType.PropertyName)
					throw new JsonException("Expected PropertyName token");*/
				if (reader.TokenType == JsonTokenType.EndObject)
					continue;
				var propName = reader.GetString();
				reader.Read();

				switch (propName)
				{
					case nameof(BolnickoLecenje.id):
						bLecenje.id = reader.GetString();
						break;
					case nameof(BolnickoLecenje.datumPocetka):
						bLecenje.datumPocetka = DateTime.Parse(reader.GetString());
						break;
					case nameof(BolnickoLecenje.trajanje):
						bLecenje.trajanje = reader.GetInt32();
						break;
					case nameof(BolnickoLecenje.jeZavrsen):
						bLecenje.jeZavrsen = reader.GetBoolean();
						break;
					case nameof(BolnickoLecenje.pacijent):
						bLecenje.pacijent = new Pacijent { id = reader.GetString() };
						break;
					case nameof(BolnickoLecenje.bolnickaSoba):
						bLecenje.bolnickaSoba = new Prostorija { id = reader.GetString()};
						break;
				}
			}

			throw new JsonException("Expected EndObject token");
		}

        public override void Write(Utf8JsonWriter writer, BolnickoLecenje value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString(nameof(value.id), value.id);
            writer.WriteString(nameof(value.datumPocetka), value.datumPocetka);

            writer.WriteNumber(nameof(value.trajanje), value.trajanje);

			writer.WriteBoolean(nameof(value.jeZavrsen), value.jeZavrsen);

            writer.WriteString(nameof(value.pacijent), value.pacijent.id);
            writer.WriteString(nameof(value.bolnickaSoba), value.bolnickaSoba.id);

            writer.WriteEndObject();
        }
    }
}

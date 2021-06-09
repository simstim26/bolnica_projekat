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
    class KonverterProstorije : JsonConverter<Prostorija>
	{
		public override Prostorija Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Expected StartObject token");

			Prostorija b = new Prostorija();
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
					case nameof(Prostorija.id):
						b.id = reader.GetString();
						break;
					case nameof(Prostorija.idBolnice):
						b.idBolnice = reader.GetString();
						break;
                    case nameof(Prostorija.tipProstorije):
						b.tipProstorije = new OsnovniTipProstorije();
						break;
					case nameof(Prostorija.broj):
						b.broj = reader.GetString();
						break;
					case nameof(Prostorija.sprat):
						b.sprat = reader.GetInt32();
						break;
					case nameof(Prostorija.dostupnost):
						b.dostupnost = reader.GetBoolean();
						break;
					case nameof(Prostorija.logickiObrisana):
						b.logickiObrisana = reader.GetBoolean();
						break;
					case nameof(Prostorija.brojZauzetihKreveta):
						b.brojZauzetihKreveta = reader.GetInt32();
						break;
					/*case nameof(Prostorija.Stavka):
						b.Stavka = reader.*/
					/*case nameof(Prostorija.Stavka):
						b.Stavka = reader.();
						break;*/
						/*case nameof(Bolest.naziv):
							b.naziv = reader.GetString();
							break;
						case nameof(Bolest.terapija):
							b.terapija = new Terapija { id = reader.GetString() };
							break;
						case nameof(Bolest.pacijent):
							b.pacijent = new Pacijent { id = reader.GetString() };
							break;*/
				}
			}

			throw new JsonException("Expected EndObject token");
		}

		public override void Write(Utf8JsonWriter writer, Prostorija value, JsonSerializerOptions options)
		{

			writer.WriteStartObject();
			writer.WriteString(nameof(value.id), value.id);
			writer.WriteString(nameof(value.idBolnice), value.idBolnice);

			//writer.WriteString(nameof(value.tipProstorije), value.tipProstorije.tip);
			writer.WriteString(nameof(value.broj), value.broj);
			writer.WriteNumber(nameof(value.sprat), value.sprat);
			writer.WriteBoolean(nameof(value.dostupnost), value.dostupnost);
			writer.WriteBoolean(nameof(value.logickiObrisana), value.logickiObrisana);
			writer.WriteNumber(nameof(value.brojZauzetihKreveta), value.brojZauzetihKreveta);

			writer.WriteEndObject();
		}
	}
}

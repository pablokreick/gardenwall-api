using OverTheGardenWallAPI.Models;

namespace OverTheGardenWallAPI
{
    public class SeedData
    {
        public static void Initialize(OverTheGardenWallDbContext db)
        {
            if (db.Characters.Any()) return; // Ya hay datos

            var characters = new List<Character>
        {
            new Character { Nombre = "Greg", Especie = "Humano" },
            new Character { Nombre = "Wirt", Especie = "Humano" },
            new Character { Nombre = "Beatrice", Especie = "Pájaro" },
            new Character { Nombre = "The Beast", Especie = "Entidad" },
            new Character { Nombre = "Adelaide", Especie = "Bruja" },
            new Character { Nombre = "The Woodsman", Especie = "Humano" },
            new Character { Nombre = "Auntie Whispers", Especie = "Criatura" },
            new Character { Nombre = "Lorna", Especie = "Humana" }
        };

            db.Characters.AddRange(characters);
            db.SaveChanges();
        }

    }
}

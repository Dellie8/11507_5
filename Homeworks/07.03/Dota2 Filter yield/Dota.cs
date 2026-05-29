using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dota2
{
    public abstract class Hero
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }

        public Hero(string name)
        {
            Name = name;
            Level = 1;
        }
    }

    public override string ToString()
        {
            return $@"Имя: {Name}
Уровень: {Level}
Сила: {Strength}
Ловкость: {Agility}
Интеллект: {Intelligence}
Здоровье: {Health}
Мана: {Mana}";
        }
    }


    public class StrengthHero : Hero
    {
        public StrengthHero(string name) : base(name)
        {
            Strength = 76;
            Agility = 27;
            Intelligence = 19;

            Health = Strength * 10;
            Mana = Intelligence * 10;
        }
    }

    public class IntelligenceHero : Hero
    {
        public IntelligenceHero(string name) : base(name)
        {
            Strength = 33;
            Agility = 27;
            Intelligence = 48;

            Health = Strength * 10;
            Mana = Intelligence * 10;
        }
    }

    public class AgilityHero : Hero
    {
        public AgilityHero(string name) : base(name)
        {
            Strength = 54;
            Agility = 79;
            Intelligence = 32;

            Health = Strength * 10;
            Mana = Intelligence * 10;
        }
    }

   class Program
    {
        static IEnumerable<Hero> GetHeroes()
        {
            yield return new StrengthHero("Destroyer");
            yield return new IntelligenceHero("Herta");
            yield return new AgilityHero("Joker");
        }

        static void Main(string[] args)
        {
            string path = "heroes.txt";
            IEnumerable<Hero> heroes = GetHeroes();

            var strongHeroes = heroes.Where(h => h.Health > 500);

            var smartHeroes = heroes.Where(h => h.Intelligence > 30);

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Hero hero in strongHeroes)
                {
                    writer.WriteLine(hero);
                }
            }

            Console.WriteLine("Файл создан!\n");
            Console.WriteLine("Герои с интеллектом > 30:\n");

            foreach (Hero hero in smartHeroes)
            {
                Console.WriteLine(hero);
            }

            Console.WriteLine("Содержимое файла:\n");

            using (StreamReader reader = new StreamReader(path))
            {
                string text;

                while ((text = reader.ReadLine()) != null)
                {
                    Console.WriteLine(text);
                }
            }
        }
    }
}
    }
} 

/*using System;
using System.Collections.Generic;

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
        public List<Ability> Abilities { get; set; }
        
        public Hero(string name)
        {
            Name = name;
            Level = 1;
            Abilities = new List<Ability>();
        }
    }
    
    public class Ability
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int ManaCost { get; set; }
        public string Description { get; set; }

        public Ability(string name, int damage, int manaCost, string description = "")
        {
            Name = name;
            Damage = damage;
            ManaCost = manaCost;
            Description = description;
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
        static void Main(string[] args)
        {
            
            var destroyer = new StrengthHero("Destroyer");
            var joker = new AgilityHero("Joker");
            var herta = new IntelligenceHero("Herta");
            
            var berserkersCall = new Ability("Берсерк", 0, 80);
            var stiflingDagger = new Ability("Кинжал", 120, 65);
            var frostNova = new Ability("Ледяная Вспышка", 200, 120);

            // Наделяем способностями
            destroyer.Abilities.Add(berserkersCall);
            joker.Abilities.Add(stiflingDagger);
            herta.Abilities.Add(frostNova);

            // Просто выводим в консоль, чтобы убедиться что всё работает
            Console.WriteLine($"Создан герой: {destroyer.Name}, ХП: {destroyer.Health}, Способностей: {destroyer.Abilities.Count}");
            Console.WriteLine($"Создан герой: {joker.Name}, ХП: {joker.Health}, Способностей: {joker.Abilities.Count}");
            Console.WriteLine($"Создан герой: {herta.Name}, ХП: {herta.Health}, Способностей: {herta.Abilities.Count}");
        }
    }
} */

using System;
using System.Collections.Generic;
using System.IO;

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
        static void Main(string[] args)
        {
            List<Hero> heroes = new List<Hero>()
            {
                new StrengthHero("Destroyer"),
                new IntelligenceHero("Herta"),
                new AgilityHero("Joker")
            };

            string path = "heroes.txt";

            // СОЗДАНИЕ И ЗАПИСЬ В ФАЙЛ
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Hero hero in heroes)
                {
                    writer.WriteLine($"Имя: {hero.Name}");
                    writer.WriteLine($"Уровень: {hero.Level}");
                    writer.WriteLine($"Сила: {hero.Strength}");
                    writer.WriteLine($"Ловкость: {hero.Agility}");
                    writer.WriteLine($"Интеллект: {hero.Intelligence}");
                    writer.WriteLine($"Здоровье: {hero.Health}");
                    writer.WriteLine($"Мана: {hero.Mana}");
                    writer.WriteLine("------------------------");
                }
            }

            Console.WriteLine("Файл создан!");

            // СЧИТЫВАНИЕ ФАЙЛА
            Console.WriteLine("\nСодержимое файла:\n");

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
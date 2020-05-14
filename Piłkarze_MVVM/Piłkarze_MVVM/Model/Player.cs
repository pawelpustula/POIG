using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze_MVVM.Model
{
    class Player
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }

        public Player() { }

        public Player(string name, string surname, int age, double weight)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Weight = Math.Round(weight, 1);
        }

        public bool isTheSameAs(Player player)
        {
            if (player.Name == Name && player.Surname == Surname
                && player.Age == Age && player.Weight == Weight)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}, wiek: {Age}, waga: {Weight} kg";
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze
{
    public class Player
    {
        private string imie;
        private string nazwisko;
        private int wiek;
        private double waga;

        public Player(string imie = "imie", string nazwisko = "nazwisko", int wiek = 15, double waga = 55)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.wiek = wiek;
            this.waga = Math.Round(waga, 1);
        }

        public Player(Player p)
        {
            this.imie = p.imie;
            this.nazwisko = p.nazwisko;
            this.wiek = p.wiek;
            this.waga = p.waga;
        }

        public bool isTheSame(Player player)
        {
            if (player.Imie == this.Imie && player.Nazwisko == this.nazwisko
                && player.Wiek == this.wiek && player.Waga == this.waga)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string p = this.imie + " " + this.nazwisko + ", wiek: " + this.wiek + ", waga: " + this.waga + " kg";
            return p;
        }

        public string ToFileFormat()
        {
            string p = this.imie + " " + this.nazwisko + " " + this.wiek + " " + this.waga;
            return p;
        }

        public static Player CreateFromString(string sPlayer)
        {
            string imie, nazwisko;
            int wiek;
            double waga;
            string[] fields = sPlayer.Split(' ');
            if (fields.Length == 4)
            {
                imie = fields[0];
                nazwisko = fields[1];
                if (!Int32.TryParse(fields[2], out wiek))
                {
                    throw new Exception("Błędny format wieku");
                }
                if (!Double.TryParse(fields[3], out waga))
                {
                    throw new Exception("Błędny format wagi");
                }
                Player player = new Player(imie, nazwisko, wiek, waga);
                return player;
            }
            throw new Exception("Błędny format danych");
        }

        public string Imie { get { return imie; } set { imie = value; } }
        public string Nazwisko { get { return nazwisko; } set { nazwisko = value; } }
        public int Wiek { get { return wiek; } set { wiek = value; } }
        public double Waga { get { return waga; } set { waga = value; } }
    }
}


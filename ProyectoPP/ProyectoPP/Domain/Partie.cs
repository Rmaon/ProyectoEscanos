using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoPP.Domain
{
    public class Partie
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public string President { get; set; }
        public long Votes { get; set; }
        public int Seats { get; set; }

        public Partie(string acronym, string name, string president, int posicion, Info info)
        {
            this.Acronym = acronym;
            this.Name = name;
            this.President = president;
            this.Votes = (long) getRelativeVotes(posicion, info);
            Seats = 0;
        }

        public long getRelativeVotes(int posicion, Info info)
        {
            double[] numeros = { 35.25, 24.75, 15.75, 14.25, 3.75, 3.25, 1.5, 0.5, 0.25, 0.25, 0.5 };

            return (long) numeros[posicion] * (info.Population-info.NullVotes);
        }

        public static List<Partie> CalculateSeats(List<Partie> parties, int totalSeats)
        {
            
            parties = parties.OrderByDescending(p => p.Votes).ToList();

            for (int i = 1; i <= totalSeats; i++)
            {
                Partie winningPartie = parties.First();
                winningPartie.Seats++;
                parties = parties.OrderByDescending(p => (double)p.Votes / (p.Seats + 1)).ToList();
            }

            return parties;
        }
    }
}

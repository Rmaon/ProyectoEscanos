using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProyectoPP.Domain
{
    public class Partie
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public string President { get; set; }
        public int Votes { get; set; }
        public int Seats { get; set; }

        public Partie(string acronym, string name, string president, int position, Info info)
        {
            this.Acronym = acronym;
            this.Name = name;
            this.President = president;
            this.Votes = (int)GetRelativeVotes(position, info);
            Seats = 0;
        }

        // Calculate relative votes based on position and information
        public int GetRelativeVotes(int position, Info info)
        {
            double[] numbers = { 35.25, 24.75, 15.75, 14.25, 3.75, 3.25, 1.5, 0.5, 0.25, 0.25, 0.5 };

            double votes = ((info.avalibleVotes) * numbers[position] / 100);

            return (int)votes;
        }

        // Calculate seats based on votes and the total number of seats
        public static List<Partie> CalculateSeats(List<Partie> parties, int totalSeats)
        {
            // Order parties based on votes in descending order
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

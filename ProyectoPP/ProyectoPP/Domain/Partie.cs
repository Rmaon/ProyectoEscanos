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

        public Partie(string acronym, string name, string president)
        {
            this.Acronym = acronym;
            this.Name = name;
            this.President = president;
        }
    }
}

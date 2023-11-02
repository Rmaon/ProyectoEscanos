using ProyectoPP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPP.Persistence
{
    public class InfoManager
    {
        public Info GetInfo()
        {
            // Aquí puedes implementar la lógica para calcular NullVotes u obtener datos de alguna fuente externa.
            var info = new Info
            {
                Population = 6921267,
                AbstentionVotes = 0,
                NullVotes = 0 // Aquí debes calcular los NullVotes o asignar un valor inicial.
            };

            return info;
        }

        internal int CalculateNullVotes(int population, int abstentionVotes)
        {
            if (population >= abstentionVotes)
            {
                return (population - abstentionVotes) / 20;
            }
            else
            {
                // Maneja el caso en el que la población sea menor que la abstención de votos.
                return 0; // O puedes manejarlo de otra manera según tus requerimientos.
            }
        }   
    }
}
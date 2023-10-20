using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPP.Domain
{
    public class Info : INotifyPropertyChanged
    {
        public Info()
        {
            population = 6921267;
            nullVotes = 0;
            abstentionVotes = 0;
        }

        private int population;
        public int Population
        {
            get { return population; }
            set
            {
                population = value;
                OnPropertyChanged(nameof(Population));
            }
        }

        private int abstentionVotes;
        public int AbstentionVotes
        {
            get { return abstentionVotes; }
            set
            {
                abstentionVotes = value;
                OnPropertyChanged(nameof(AbstentionVotes));
            }
        }

        private int nullVotes;
        public int NullVotes
        {
            get { return nullVotes; }
            set
            {
                nullVotes = value;
                OnPropertyChanged(nameof(NullVotes));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using ProyectoPP.Domain;
using ProyectoPP.Persistence;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProyectoPP
{
    public partial class MainWindow : Window
    {
        private Info info;
        private InfoManager infoManager;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            btnMandar.Click += BtnMandar_Click;
        }

        private void InitializeData()
        {
            info = new Info();
            infoManager = new InfoManager();
            DataContext = info;

            if (FindName("txtAbsten") is TextBox txtAbsten)
            {
                txtAbsten.Text = "0";
                txtAbsten.TextChanged += TextBox_TextChanged;
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (int.TryParse(textBox.Text, out int abstentionVotes) && abstentionVotes >= 0)
                {
                    int nullVotesValue = infoManager.CalculaNullVotes(info.Population, abstentionVotes);
                    info.NullVotes = nullVotesValue;
                }
            }
        }

        private void BtnMandar_Click(object sender, RoutedEventArgs e)
        {
            tb.SelectedIndex = 1;

            MessageBox.Show("Datos guardados correctamente y cambiado a la pestaña PARTIES MANAGMENT.");
        }
    }
}

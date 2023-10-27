using ProyectoPP.Domain;
using ProyectoPP.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProyectoPP
{
    public partial class MainWindow : Window
    {

        private int nCreados=0;

        private Info info;
        private InfoManager infoManager;

        private List<Partie> partieList = new List<Partie>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            btnMandar.Click += BtnMandar_Click;

            partyDataGrid.ItemsSource = partieList;

            ti1.IsEnabled = false;
            ti2.IsEnabled = false;

        }

        // Method to add an item to the list and update the DataGrid
        private void AddItem(string acronym, string name, string president)
        {
            partieList.Add(new Partie(acronym, name, president));
            partyDataGrid.Items.Refresh(); // Update the DataGrid
        }

        // Method to remove an item from the list and update the DataGrid
        private void RemoveItem(int index)
        {
            if (index >= 0 && index < partieList.Count)
            {
                partieList.RemoveAt(index);
                partyDataGrid.Items.Refresh(); // Update the DataGrid
            }
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void partyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddItem(txtAcronym.Text, txtPartyName.Text, txtPresidentName.Text);
            nCreados++;
            ti1.IsEnabled = true;

            if (nCreados == 10)
            {
                ti2.IsEnabled = true;
                btnSave.IsEnabled = false;
            }
            else
            {
                ti2.IsEnabled = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var selectedItems = partyDataGrid.SelectedItems.Cast<Partie>().ToList();

            foreach (var item in selectedItems)
            {
                nCreados--;
                int index = partieList.IndexOf(item);
                RemoveItem(index);
            }

            btnSave.IsEnabled = true;

        }

        private void btnMandar_Click_1(object sender, RoutedEventArgs e)
        {
            ti1.IsEnabled = true;
        }
    }
}

using ProyectoPP.Domain;
using ProyectoPP.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;

namespace ProyectoPP
{
    public partial class MainWindow : Window
    {
        private int nCreated = 0; // Counter for created items

        private Info info; // Info object
        private InfoManager infoManager; // InfoManager object

        private List<Partie> partieList = new List<Partie>(); // List of parties

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            btnMandar.Click += BtnMandar_Click;

            partyDataGrid.ItemsSource = partieList; // Set the DataGrid source

            ti1.IsEnabled = false; // Disable tabs
            ti2.IsEnabled = false;
        }

        // Method to add an item to the list and update the DataGrid
        private void AddItem(string acronym, string name, string president)
        {
            partieList.Add(new Partie(acronym, name, president, partieList.Count, info));
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
            // Handle tab selection change if needed
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (int.TryParse(textBox.Text, out int abstentionVotes) && abstentionVotes >= 0)
                {
                    int nullVotesValue = infoManager.CalculateNullVotes(info.Population, abstentionVotes);
                    info.NullVotes = nullVotesValue;
                }
            }
        }

        private void BtnMandar_Click(object sender, RoutedEventArgs e)
        {
            bool noCorrectInfo = txtAbsten.Text == "0" || txtNull.Text == "0";
            bool invalidFormat = int.TryParse(txtAbsten.Text, out _) && int.TryParse(txtNull.Text, out _);

            if (invalidFormat)
            {
                if (noCorrectInfo)
                {
                    MessageBox.Show("Please enter numeric data");
                }
                else
                {
                    tb.SelectedIndex = 1;

                    MessageBox.Show("Data saved successfully and switched to PARTIES MANAGEMENT tab.");
                }
            }
            else
            {
                MessageBox.Show("Please enter all data");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool noFill = string.IsNullOrEmpty(txtAcronym.Text) || string.IsNullOrEmpty(txtPartyName.Text) || string.IsNullOrEmpty(txtPresidentName.Text);
            if (noFill)
            {
                MessageBox.Show("Please fill all the fields");
            }
            else
            {
                AddItem(txtAcronym.Text, txtPartyName.Text, txtPresidentName.Text);
                nCreated++;
                ti1.IsEnabled = true;

                if (nCreated == 10)
                {
                    ti2.IsEnabled = true;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    ti2.IsEnabled = false;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedItems = partyDataGrid.SelectedItems.Cast<Partie>().ToList();
            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    nCreated--;
                    int index = partieList.IndexOf(item);
                    RemoveItem(index);
                }
                ti2.IsEnabled = false;
                btnSave.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Please select a party");
            }
        }

        private void btnMandar_Click_1(object sender, RoutedEventArgs e)
        {
            ti1.IsEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (Partie partie in partieList)
            {
                partie.Seats = 0;
            }

            seatsDataGrid.IsEnabled = true;

            int totalSeats = 37;

            partieList = Partie.CalculateSeats(partieList, totalSeats);

            seatsDataGrid.ItemsSource = partieList;

            ICollectionView view = CollectionViewSource.GetDefaultView(seatsDataGrid.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Seats", ListSortDirection.Descending));
        }

        private void partyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change in the partyDataGrid if needed
        }
    }
}

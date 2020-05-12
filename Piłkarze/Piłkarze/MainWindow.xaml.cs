using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Piłkarze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string archivingFile = "archiwum.txt";

        public MainWindow()
        {
            InitializeComponent();
            addValues();
            List<int> ageList = addValues();
            ageComboBox.ItemsSource = ageList;

        }

        private List<int> addValues()
        {
            List<int> ageList = new List<int>();

            for (int i = 15; i <= 55; i++)
            {
                ageList.Add(i);
            }
            return ageList;
        }

        private void Name_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (nameTextBox.Text.Equals("Podaj imię"))
            {
                nameTextBox.Clear();
            }
        }

        private void Surname_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (surnameTextBox.Text.Equals("Podaj nazwisko"))
            {
                surnameTextBox.Clear();
            }
        }

        private void setColors(TextBox textBox, ToolTip toolTip)
        {
            textBox.Foreground = Brushes.Black;
            if (String.IsNullOrEmpty(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(3);
                toolTip.Visibility = Visibility.Visible;
                toolTip.Content = "Uzupełnij dane";
            }
            else
            {
                textBox.ClearValue(BorderBrushProperty);
                textBox.ClearValue(BorderThicknessProperty);
                if (toolTip != null)
                {
                    toolTip.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            setColors(nameTextBox, nameTooltip);
        }

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            setColors(surnameTextBox, surnameTooltip);
        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            string surname = surnameTextBox.Text.Trim();
            int age = (int)ageComboBox.SelectedValue;
            double weight = weightSlider.Value;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(surname)
                || name.Equals("Podaj imię") || surname.Equals("Podaj nazwisko"))
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
            }
            else
            {
                Player newPlayer = new Player(name, surname, age, weight);
                bool isOnList = false;

                foreach (Player player in playersListBox.Items)
                {
                    if (player.isTheSame(newPlayer))
                    {
                        isOnList = true;
                        break;
                    }
                }
                if (!isOnList)
                {
                    playersListBox.Items.Add(newPlayer);
                }
                else
                {
                    MessageBox.Show(newPlayer + " już znajduje się na liście", "Dodawanie");
                    ClearForm();
                }
            }
        }

        private void DeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (playersListBox.Items.Count == 0)
            {
                MessageBox.Show("Lista jest pusta!");
            }
            else if (playersListBox.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć " + playersListBox.SelectedItem + "?", "Usuwanie", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    playersListBox.Items.Remove(playersListBox.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Wybierz gracza, którego chcesz usunąć");
            }
        }

        private void ModifyPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (playersListBox.Items.Count == 0)
            {
                MessageBox.Show("Lista jest pusta!");
            }
            else if (playersListBox.SelectedItem != null)
            {
                string name = nameTextBox.Text.Trim();
                string surname = surnameTextBox.Text.Trim();
                int age = (int)ageComboBox.SelectedValue;
                double weight = weightSlider.Value;
                if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(surname))
                {
                    MessageBox.Show("Uzupełnij wszystkie pola");
                }
                else
                {
                    Player modifiedPlayer = new Player(name, surname, age, weight);
                    bool isOnList = false;

                    foreach (Player player in playersListBox.Items)
                    {
                        if (player.isTheSame(modifiedPlayer))
                        {
                            isOnList = true;
                            break;
                        }
                    }
                    if (!isOnList)
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Czy na pewno chcesz zmodyfikować " + playersListBox.SelectedItem + "?", "Modyfikacja", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            int currentIndex = playersListBox.SelectedIndex;
                            playersListBox.Items[currentIndex] = modifiedPlayer;
                        }
                    }
                    else
                    {
                        MessageBox.Show(modifiedPlayer + " już znajduje się na liście", "Modyfikacja");
                        ClearForm();
                    }
                }
            }
            else
            {
                MessageBox.Show("Wybierz gracza, którego chcesz zmodyfikować");
            }
        }

        private void Player_Selected(object sender, RoutedEventArgs e)
        {
            if (playersListBox.SelectedItem != null)
            {
                Player selectedPlayer = (Player)playersListBox.SelectedItem;
                nameTextBox.Text = selectedPlayer.Imie;
                surnameTextBox.Text = selectedPlayer.Nazwisko;
                ageComboBox.SelectedValue = selectedPlayer.Wiek;
                weightSlider.Value = selectedPlayer.Waga;
            }

        }

        private void ClearForm()
        {
            nameTextBox.Text = "Podaj imię";
            nameTextBox.Foreground = Brushes.Gray;

            surnameTextBox.Text = "Podaj nazwisko";
            surnameTextBox.Foreground = Brushes.Gray;

            ageComboBox.SelectedValue = 15;
            weightSlider.Value = 55;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = playersListBox.Items.Count;
            Player[] players = null;
            if (n > 0)
            {
                players = new Player[n];
                int index = 0;
                foreach (Player player in playersListBox.Items)
                {
                    players[index++] = player;
                }
                Archiving.SavePlayersToFile(archivingFile, players);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Player[] players = Archiving.ReadPlayersFromFile(archivingFile);
            if (players != null)
            {
                foreach (var player in players)
                {
                    playersListBox.Items.Add(player);
                }
            }
        }
    }
}

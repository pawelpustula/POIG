using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;


namespace Piłkarze_MVVM.VievModel
{
    using Model;
    using BaseClass;

    internal class Operations : ViewModelBase
    {
        private Team team = new Team();

        public List<string> ListOfPlayers
        {
            get => ShowPlayers(team.GetPlayers);
        }

        public List<double> ListOfYears
        {
            get => AddYears();
        }

        public string CurrentName { get; set; }
        public string CurrentSurname { get; set; }
        public int CurrentAge { get; set; } = 15;
        public double CurrentWeight { get; set; } = 55;
        public int CurrentIndex { get; set; } = -1;

        private ICommand _AddPlayer = null;
        private ICommand _ModifyPlayer = null;
        private ICommand _LoadPlayerData = null;
        private ICommand _DeletePlayer = null;
        private ICommand _SavePlayersToFile = null;

        public ICommand AddPlayer
        {
            get
            {
                if (_AddPlayer == null)
                {
                    _AddPlayer = new RelayCommand(
                        arg =>
                        {
                            Player newPlayer = new Player(CurrentName, CurrentSurname, CurrentAge, CurrentWeight);
                            if (team.CheckIfPlayerIsOnList(newPlayer))
                            {
                                MessageBox.Show($"{newPlayer} już znajduje się na liście", "Dodawanie");
                            }
                            else
                            {
                                team.AddPlayer(new Player(CurrentName, CurrentSurname, CurrentAge, CurrentWeight));
                                onPropertyChanged(nameof(ListOfPlayers));
                            }
                        },
                        arg => (!string.IsNullOrEmpty(CurrentName)) && (!string.IsNullOrEmpty(CurrentSurname))
                        && (CurrentName != null) && (CurrentSurname != null)
                        );
                }
                return _AddPlayer;
            }
        }

        public ICommand ModifyPlayer
        {
            get
            {
                if (_ModifyPlayer == null)
                {
                    _ModifyPlayer = new RelayCommand(
                        arg =>
                        {
                            MessageBoxResult msgBoxRes = MessageBox.Show($"Czy na pewno chcesz zmienić dane zawodnika?", "Edycja", MessageBoxButton.YesNo);
                            if (msgBoxRes == MessageBoxResult.Yes)
                            {
                                Player modifiedPlayer = new Player(CurrentName, CurrentSurname, CurrentAge, CurrentWeight);
                                if (team.CheckIfPlayerIsOnList(modifiedPlayer))
                                {
                                    MessageBox.Show($"{modifiedPlayer} już znajduje się na liście", "Edycja");
                                }
                                else
                                {
                                    team.EditPlayer(new Player(CurrentName, CurrentSurname, CurrentAge, CurrentWeight), CurrentIndex);
                                    onPropertyChanged(nameof(ListOfPlayers));
                                }
                            }
                        },
                        arg => CurrentIndex != -1 && (!string.IsNullOrEmpty(CurrentName)) && (!string.IsNullOrEmpty(CurrentSurname))
                        && (CurrentName != null) && (CurrentSurname != null)
                        );
                }
                return _ModifyPlayer;
            }
        }

        public ICommand LoadPlayerData
        {
            get
            {
                if (_LoadPlayerData == null)
                {
                    _LoadPlayerData = new RelayCommand(

                        arg =>
                        {
                            Player player = team.GetPlayers[CurrentIndex];
                            CurrentName = player.Name;
                            CurrentSurname = player.Surname;
                            CurrentAge = player.Age;
                            CurrentWeight = player.Weight;

                            onPropertyChanged(nameof(CurrentName), nameof(CurrentSurname),
                                nameof(CurrentAge), nameof(CurrentWeight));
                        },

                        arg => CurrentIndex != -1
                        );
                }
                return _LoadPlayerData;
            }
        }

        public ICommand DeletePlayer
        {
            get
            {
                if (_DeletePlayer == null)
                {
                    _DeletePlayer = new RelayCommand(
                        arg =>
                        {
                            MessageBoxResult msgBoxRes = MessageBox.Show($"Czy na pewno chcesz usunąć zawodnika?", "Usuwanie", MessageBoxButton.YesNo);
                            if (msgBoxRes == MessageBoxResult.Yes)
                            {
                                team.DeletePlayer(CurrentIndex);
                                onPropertyChanged(nameof(ListOfPlayers));
                            }

                        },

                        arg => CurrentIndex != -1
                        );

                }
                return _DeletePlayer;
            }
        }

        public ICommand SavePlayersToFile
        {
            get
            {
                if (_SavePlayersToFile == null)
                {
                    _SavePlayersToFile = new RelayCommand(arg => team.SavePlayersToFile(@"Players.json"), arg => true);
                }
                return _SavePlayersToFile;
            }
        }


        public static List<string> ShowPlayers(List<Player> players)
        {
            List<string> listOFPlayers = new List<string>();

            foreach (Player player in players)
            {
                listOFPlayers.Add(player.ToString());
            }
            return listOFPlayers;
        }

        public static List<double> AddYears()
        {
            List<double> years = new List<double>();

            for (int age = 15; age <= 55; age++)
            {
                years.Add(age);
            }
            return years;
        }
    }
}

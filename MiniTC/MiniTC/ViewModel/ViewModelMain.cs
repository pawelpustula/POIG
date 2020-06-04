using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiniTC.ViewModel.BaseClass;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MiniTC.ViewModel
{
    using Model;
    class ViewModelMain : ViewModelBase
    {
        Model model = new Model();

        private string _currentLeftDirectory = null;
        private string _currentRightDirectory = null;

        public string CurrentLeftDirectory
        {
            get
            {
                return _currentLeftDirectory;
            }
            set
            {
                _currentLeftDirectory = value;
                onPropertyChanged(nameof(CurrentLeftFiles));
                onPropertyChanged(nameof(CurrentLeftDirectory));
            }
        }

        public ObservableCollection<string> CurrentLeftFiles
        {
            get
            {
                return new ObservableCollection<string>(model.GetFiles(CurrentLeftDirectory));
            }
        }

        public string CurrentRightDirectory
        {
            get
            {
                return _currentRightDirectory;
            }
            set
            {
                _currentRightDirectory = value;
                onPropertyChanged(nameof(CurrentRightFiles));
                onPropertyChanged(nameof(CurrentRightDirectory));
            }
        }

        public ObservableCollection<string> CurrentRightFiles
        {
            get
            {
                return new ObservableCollection<string>(model.GetFiles(CurrentRightDirectory));
            }
        }

        public ObservableCollection<string> CurrentDrives
        {
            get
            {
                return new ObservableCollection<string>(model.GetDrives());
            }
        }

        public string SelectedFile { get; set; }

        private ICommand _leftClick = null;
        public ICommand LeftClick
        {
            get
            {
                if (_leftClick == null)
                {
                    _leftClick = new RelayCommand(
                        x =>
                        {
                            CurrentLeftDirectory = model.ChangePath(CurrentLeftDirectory, SelectedFile);
                        },
                        x => true);
                }
                return _leftClick;
            }
        }

        private ICommand _rightClick = null;
        public ICommand RightClick
        {
            get
            {
                if (_rightClick == null)
                {
                    _rightClick = new RelayCommand(
                    (arg) =>
                    {
                        CurrentRightDirectory = model.ChangePath(CurrentRightDirectory, SelectedFile);
                    },
                    (arg) => true);
                }
                return _rightClick;
            }
        }

        private ICommand _copy = null;
        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                {
                    _copy = new RelayCommand(x =>
                    {
                        if (CurrentRightDirectory != null)
                        {
                            string source = CurrentLeftDirectory + @"\" + SelectedFile;
                            string destination = _currentRightDirectory + @"\" + SelectedFile;
                            model.CopyFile(source, destination);
                        }
                        onPropertyChanged(nameof(CurrentRightFiles));
                    },
                    x => true);
                }
                return _copy;
            }
        }
    }
}

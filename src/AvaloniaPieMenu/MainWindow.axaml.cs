using System;
using System.Collections.Generic;
using Avalonia.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Input;
using AvaloniaPieMenu.Controls;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaPieMenu
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            this.PointerPressed += MainWindow_PointerPressed;
            InitializeComponent();
            var mdIcon = new[] { "ab-testing", "abacus", "abjad-arabic", "account-box-multiple", "account-child-circle", "account-details", "airplane", "alpha-a-box-outline" };
            var r = new Random();
            var items = new List<RadialMenuItem>();
            for (int i = 0; i < 6; i++)
            {
                var item = new RadialMenuItem()
                {
                    Icon = new Projektanker.Icons.Avalonia.Icon()
                    {
                        Value = $"mdi-{mdIcon[r.Next(0,mdIcon.Length)]}",
                        FontSize = 24
                    },
                    Content = new TextBlock() { Text = $"Item {i}" }
                };
                for (int j = 0; j < 6; j++)
                {
                    var subItem = new RadialMenuItem()
                    {
                        ParentMenuItem = item,
                        Icon = new Projektanker.Icons.Avalonia.Icon()
                        {
                            Value = $"mdi-{mdIcon[r.Next(0, mdIcon.Length)]}",
                            FontSize = 24
                        },
                        Content = new TextBlock() { Text = $"S{i}" }
                    };
                    item.SubMenuItems.Add(subItem);
                }

                RadialMenu.Items.Add(item);
            }

        }



        private bool _isOpen1 = false;
        public bool IsOpen1
        {
            get
            {
                return _isOpen1;
            }
            set
            {
                _isOpen1 = value;
                RaisePropertyChanged();
            }
        }

        private bool _isOpen2 = false;
        public bool IsOpen2
        {
            get
            {
                return _isOpen2;
            }
            set
            {
                _isOpen2 = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CloseRadialMenu1
        {
            get
            {
                return new RelayCommand(() => IsOpen1 = false);
            }
        }
        public ICommand OpenRadialMenu1
        {
            get
            {
                return new RelayCommand(() => { IsOpen1 = !IsOpen1; IsOpen2 = false; });
            }
        }

        public ICommand CloseRadialMenu2
        {
            get
            {
                return new RelayCommand(() => IsOpen2 = false);
            }
        }
        public ICommand OpenRadialMenu2
        {
            get
            {
                return new RelayCommand(() => { IsOpen2 = !IsOpen2; IsOpen1 = false; });
            }
        }

        public ICommand Test1
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("1"));
            }
        }

        public ICommand Test2
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("2"));
            }
        }

        public ICommand Test3
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("3"));
            }
        }

        public ICommand Test4
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("4"));
            }
        }

        public ICommand Test5
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("5"));
            }
        }

        public ICommand Test6
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        System.Diagnostics.Debug.WriteLine("6");
                    },
                    () =>
                    {
                        return false; // To disable the 6th item
                    }
                );
            }
        }


        private void MainWindow_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            var p = e.GetCurrentPoint(this);
            if (p.Properties.IsRightButtonPressed)
            {
                OpenRadialMenu1.Execute(null);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Bor___OnSizeChanged(object? sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine($"new size : {e.NewSize}");
        }
    }
}
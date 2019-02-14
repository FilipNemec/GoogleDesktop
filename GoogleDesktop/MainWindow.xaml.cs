using GoogleDesktop.ViewModels;
using MahApps.Metro.Controls;
using System;

namespace GoogleDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            MainVM.Instance.Window = this;
            DataContext = MainVM.Instance;
            InitializeComponent();
        }

    }
}

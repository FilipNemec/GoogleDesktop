using Common.ViewModels;
using GoogleDesktop.Views;
using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GoogleDesktop.ViewModels
{
    public class MainVM : VMBase
    {
        #region Instance
        private static readonly Lazy<MainVM> lazyInstace = new Lazy<MainVM>(() => new MainVM());
        public static MainVM Instance => lazyInstace.Value;
        #endregion

        #region Properties
        public ObservableCollection<TabVM> Tabs { get; set; } = new ObservableCollection<TabVM>();
        public TabVM SelectedTab
        {
            get => selectedTab;
            set
            {
                if (selectedTab == value) return;
                selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
            }
        }
        public ThemeVM SelectedTheme
        {
            get
            {
                return selectedTheme;
            }
            set
            {
                selectedTheme = value;
                SetTheme(value);
            }
        }
        public bool IsMaximized
        {
            get => isMaximized;
            set
            {
                isMaximized = value;
                OnPropertyChanged(nameof(IsMaximized));
            }
        }
        public List<ThemeVM> Themes { get; set; } = new List<ThemeVM>();
        public MainWindow Window { get; set; }
        #endregion

        #region Fields
        private TabVM selectedTab;
        private bool isMaximized;
        private ThemeVM selectedTheme;
        #endregion

        #region Constructor
        private MainVM()
        {
            Init();
        }
        #endregion

        #region CommandMethods
        public ICommand CloseCommand => new RelayCommand(Close);
        public ICommand MinimizeCommand => new RelayCommand(Minimize);
        public ICommand MaximizeCommand => new RelayCommand(Maximize);
        public ICommand OpenSettingsCommand => new RelayCommand(OpenSettings);
        #endregion

        #region CommandMethods
        private void Close() => App.Current.Shutdown();
        private void Minimize() => Window.WindowState = WindowState.Minimized;
        private void Maximize()
        {
            IsMaximized = !IsMaximized;

            if (Window.WindowState == WindowState.Maximized)
                Window.WindowState = WindowState.Normal;
            else
                Window.WindowState = WindowState.Maximized;

        }
        private void OpenSettings() => SettingsView.ShowWindow();


        #endregion

        #region SetMethods
        private void Init()
        {
            AddTab(new SearchTabVM());
            InitThemes();
        }
        private void InitThemes()
        {

            foreach (var accent in ThemeManager.Accents)
                foreach (var theme in ThemeManager.AppThemes)
                    Themes.Add(new ThemeVM(theme, accent));

            var currentTheme = ThemeManager.DetectAppStyle();
            foreach (var theme in Themes)
                if (theme.Accent.Name == currentTheme.Item2.Name && theme.Theme.Name == currentTheme.Item1.Name)
                {
                    selectedTheme = theme;
                    break;
                }
        }
        private static void SetTheme(ThemeVM value)
        {
            ThemeManager.ChangeAppStyle(System.Windows.Application.Current, value.Accent, value.Theme);
        }
        #endregion

        #region TabMethods
        public void RemoveTab(TabVM tab)
        {
            Tabs.Remove(tab);
            SelectedTab = Tabs.FirstOrDefault();
        }
        public void AddTab(TabVM tab)
        {
            Tabs.Add(tab);
            SelectedTab = tab;
        }
        #endregion

    }
}

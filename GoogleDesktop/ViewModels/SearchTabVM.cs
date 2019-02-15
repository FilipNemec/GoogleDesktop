using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Common.ViewModels;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using GoogleDesktop.Helpers;
using GoogleDesktop.Models;

namespace GoogleDesktop.ViewModels
{
    class SearchTabVM : TabVM
    {
        #region Properties
        public ObservableCollection<ResultVM> Results { get; set; } = new ObservableCollection<ResultVM>();
        public string SearchTerm { get; set; }
        public bool IsSearching
        {
            get => isSearching;
            set
            {
                if (isSearching == value) return;
                isSearching = value;
                OnPropertyChanged(nameof(IsSearching));
            }
        }
        public string NumOfFoundResults => ResultsNum > 0 ? $"About {ResultsNum} results" : string.Empty;
        private int ResultsNum
        {
            get => resultsNum;
            set
            {
                if (resultsNum == value) return;
                resultsNum = value;
                OnPropertyChanged(nameof(NumOfFoundResults));
                OnPropertyChanged(nameof(CurrentPageFormated));
            }
        }
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage == value || value < 1) return;
                currentPage = value;
                PageChanged();
            }
        }
        public int TotalPages => (int)Math.Ceiling((decimal)ResultsNum / 10);
        public bool HasPrivious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public string CurrentPageFormated => TotalPages > 0 ? $"{currentPage} / {TotalPages}" : "0 / 0";
        public int Progress
        {
            get => progress;
            set
            {
                if (progress == value) return;
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        private string LastSearchedTerm { get; set; }
        private CustomsearchService SearchService => new CustomsearchService(new BaseClientService.Initializer { ApiKey = Settings.ApiKey });
        #endregion

        #region Fields
        private bool isSearching;
        private int resultsNum;
        private int currentPage = 1;
        private int progress;
        #endregion

        #region Commands
        public ICommand SearchCommand => new RelayCommand(Search, (o) => !IsSearching);
        public ICommand FirstPageCommand => new RelayCommand(GoToFirstPage, (o) => HasPrivious);
        public ICommand PreviousPageCommand => new RelayCommand(GoToPreviousPage, (o) => HasPrivious);
        public ICommand NextPageCommand => new RelayCommand(GoToNextPage, (o) => HasNext);
        #endregion

        #region Constructor
        public SearchTabVM() : base("Search", false)
        {
        }
        #endregion

        #region CommandMethods
        private async void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm)) return;
            CurrentPage = 1;
            IsSearching = true;
            await Search(SearchTerm);
            IsSearching = false;
        }
        private void GoToFirstPage() => CurrentPage = 1;
        private void GoToPreviousPage() => CurrentPage = CurrentPage - 1;
        private void GoToNextPage() => CurrentPage = CurrentPage + 1;
        private void GoToLastPage(object obj) => CurrentPage = TotalPages - 1;
        #endregion

        #region SearchMethods
        private void Search(IProgress<ProgressModel> progress, string searchTerm)
        {
            CancellationTokenSource cs = new CancellationTokenSource();
            try
            {
                Task.Run(() => ProgressBar(cs.Token));
                var listRequest = SearchService.Cse.List(searchTerm);
                listRequest.Cx = Settings.SearchEngineId;
                listRequest.Start = (CurrentPage - 1) * 10 + 1;

                var result = listRequest.Execute();

                ResultsNum = (int)(result?.SearchInformation?.TotalResults ?? 0);
                int i = 0;

                if (result?.Items != null)
                    foreach (Result item in result.Items)
                        progress.Report(new ProgressModel(++i, item.ToResultVM()));
                cs.Cancel();
            }
            catch (Exception e)
            {
                IsSearching = false;
                cs.Cancel();
                MessageBox.Show(e.Message, "Error");
            }

        }

        private async void ProgressBar(CancellationToken token)
        {
            Progress = 0;
            for (int i = 1; i < 50; i++)
            {
                if (token.IsCancellationRequested) return;
                Progress = i;
                await Task.Delay(50);
            }
            ProgressBar(token);
        }

        private async Task Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return;
            IsSearching = true;
            LastSearchedTerm = searchTerm;
            Progress<ProgressModel> progress = new Progress<ProgressModel>((o) => SetProgress(o));
            Results.Clear();
            await Task.Run(() => Search(progress, searchTerm));
            IsSearching = false;
        }


        #endregion

        #region Methods
        private async void PageChanged()
        {
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(HasPrivious));
            OnPropertyChanged(nameof(HasNext));
            OnPropertyChanged(nameof(CurrentPageFormated));
            if (currentPage > 1)
            {
                await Search(LastSearchedTerm);
            }
        }
        private void SetProgress(ProgressModel model)
        {
            Progress = model.Percentage;
            Results.Add(model.SearchResult);
        }
        #endregion

    }
}

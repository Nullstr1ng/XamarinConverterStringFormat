using ConverterStringFormat.Command;
using ConverterStringFormat.Helper;
using ConverterStringFormat.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConverterStringFormat.ViewModels
{
    public class ViewModel_MainPage : INotifyPropertyChanged
    {
        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region vars

        #endregion

        #region properties
        public ObservableCollection<DTO_RSS_Body> Items { get; set; } = new ObservableCollection<DTO_RSS_Body>();

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    NotifyPropertyChanged(nameof(IsBusy));
                }
            }
        }

        private bool _isViewSmall = true;
        public bool IsViewSmall
        {
            get { return _isViewSmall; }
            set
            {
                if(this._isViewSmall != value)
                {
                    this._isViewSmall = value;
                    NotifyPropertyChanged(nameof(IsViewSmall));
                }
            }
        }
        #endregion

        #region commands
        public ICommand Command_Refresh { get; set; }
        public ICommand Command_ChangeView { get; set; }
        #endregion

        #region ctors
        public ViewModel_MainPage()
        {
            InitCommands();
        }
        #endregion

        #region command methods
        void Command_Refresh_Click()
        {
            RefreshRSS();
        }

        void Command_ChangeView_Click()
        {
            this.IsViewSmall = !this.IsViewSmall;
        }
        #endregion

        #region methods
        void InitCommands()
        {
            if (Command_Refresh == null) Command_Refresh = new RelayCommand(Command_Refresh_Click);
            if (Command_ChangeView == null) Command_ChangeView = new RelayCommand(Command_ChangeView_Click);
        }

        void DesignData()
        {

        }

        void RuntimeData()
        {

        }

        public async Task RefreshData()
        {
            await RefreshRSS();
        }

        async Task RefreshRSS()
        {
            this.IsBusy = true;
            Items.Clear();
            
            var rssfeed = await SimpleWebClient.Instance.RequestAsync<DTO_RSS>("https://api.rss2json.com/v1/api.json?rss_url=https%3A%2F%2Fwww.planetxamarin.com%2Ffeed");

            if (rssfeed != null && rssfeed.items != null)
            {
                foreach (var feed in rssfeed.items)
                {
                    feed.Title = RemoveChars(feed.Title);
                    feed.Description = string.IsNullOrEmpty(feed.Description) ? string.Empty : feed.Description.Substring(0, 50);
                    feed.Description = RemoveChars(feed.Description);

                    Items.Add(feed);
                }
            }

            this.IsBusy = false;
        }

        string RemoveChars(string text, string toRemove = "\r,\n,<p>")
        {
            if(string.IsNullOrEmpty(text) == false) return text;

            string result = text;
            string[] toremove = toRemove.Split(',');
            for (int i = 0; i <= toremove.Length - 1; i++)
            {
                result = result.Replace(toremove[i], null);
            }

            return result;
        }
        #endregion

        #region INotifyPropertyChanged implementation
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
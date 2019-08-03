using ConverterStringFormat.Command;
using ConverterStringFormat.Helper;
using ConverterStringFormat.Models;

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
        #endregion

        #region commands
        public ICommand Command_Refresh { get; set; }
        // move this command methods

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
        #endregion

        #region methods
        void InitCommands()
        {
            if (Command_Refresh == null) Command_Refresh = new RelayCommand(Command_Refresh_Click);
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

            // use RSS feed for the mean time
            var rssfeed = await SimpleWebClient.Instance.RequestAsync<DTO_RSS>("https://api.rss2json.com/v1/api.json?rss_url=https%3A%2F%2Fwww.planetxamarin.com%2Ffeed");

            foreach (var feed in rssfeed.items)
            {
                feed.title = RemoveChars(feed.title);
                feed.description = feed.description.Substring(0, 50);
                feed.description = RemoveChars(feed.description);

                Items.Add(feed);
            }
            this.IsBusy = false;
        }

        string RemoveChars(string text, string toRemove = "\r,\n,<p>")
        {
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
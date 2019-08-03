using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConverterStringFormat.Models
{
    public class Model_Base
    {
        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChanged implementation
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class DTO_RSS : Model_Base
    {
        public string status { get; set; } = string.Empty;
        public DTO_RSS_Body feed { get; set; } = new DTO_RSS_Body();

        private List<DTO_RSS_Body> _items = new List<DTO_RSS_Body>();
        public List<DTO_RSS_Body> items = new List<DTO_RSS_Body>();
    }

    public class DTO_RSS_Body : Model_Base
    {
        #region properties
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public DateTime pubDate { get; set; } = DateTime.Now;
        public string thumbnail { get; set; } = string.Empty;
        #endregion
    }
}

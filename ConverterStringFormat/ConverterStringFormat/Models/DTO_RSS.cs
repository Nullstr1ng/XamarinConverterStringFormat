using Newtonsoft.Json;
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
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("author")]
        public string Author { get; set; } = string.Empty;

        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        [JsonProperty("pubDate")]
        public DateTime PubDate { get; set; } = DateTime.Now;

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;
        #endregion
    }
}

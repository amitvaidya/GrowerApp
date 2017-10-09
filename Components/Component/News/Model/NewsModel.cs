using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Component.News.Model
{
    public class NewsModel
    {
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }

        public string NewsIcon { get; set; }


        public NewsModel(string newsTitle, string newsContent, string newsIcon)
        {
            NewsTitle = newsTitle;
            NewsContent = newsContent;
            NewsIcon = newsIcon;
        }
    }
}

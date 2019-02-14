using GoogleDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDesktop.Models
{
    public class ProgressModel
    {
        public int Percentage { get; set; }
        public ResultVM SearchResult { get; set; }

        public ProgressModel(int percentage, ResultVM result)
        {
            Percentage = percentage;
            SearchResult = result;
        }
    }
}

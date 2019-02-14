using Google.Apis.Customsearch.v1.Data;
using GoogleDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDesktop.Helpers
{
    public static class Converters
    {
        public static ResultVM ToResultVM(this Result val)
             => new ResultVM()
             {
                 Header = val.Title,
                 Description = val.Snippet,
                 Link = val.Link,
                 Title = val.Title
             };

    }
}

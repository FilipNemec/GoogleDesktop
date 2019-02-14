using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDesktop.ViewModels
{
   public class ThemeVM
    {
        public Accent Accent { get; set; }
        public AppTheme Theme { get; set; }
        public string Title => Accent.Name + " " + Theme.Name.Replace("Base", "");

        public ThemeVM(AppTheme theme, Accent accent)
        {
            this.Theme = theme;
            this.Accent = accent;
        }

    }
}

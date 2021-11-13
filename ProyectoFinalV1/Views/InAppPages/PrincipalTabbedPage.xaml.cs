using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1.Views.InAppPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalTabbedPage : Xamarin.Forms.TabbedPage
    {
        public PrincipalTabbedPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSmoothScrollEnabled(true);
        }
    }
}
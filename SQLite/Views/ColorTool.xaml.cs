



using SQLiteTool.ViewModels;
using System.Windows.Controls;

namespace SQLiteTool.Views
{
    public partial class ColorTool : UserControl
    {
        public ColorTool()
        {
            DataContext = new ColorToolViewModel();
            InitializeComponent();
        }
    }
}

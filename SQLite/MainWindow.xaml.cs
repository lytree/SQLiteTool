using MaterialDesignThemes.Wpf;
using SQLiteTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(() => Thread.Sleep(2500)).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue?.Enqueue("Welcome to Material Design In XAML Toolkit");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue!);
        }

        private void OnCopy(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string stringValue)
            {
                try
                {
                    Clipboard.SetDataObject(stringValue);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }
        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
            => ItemsToolListBox.Focus();
        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
            => ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private void FlowDirectionButton_Click(object sender, RoutedEventArgs e)
            => FlowDirection = FlowDirectionToggleButton.IsChecked.GetValueOrDefault(false)
                ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;

        private static void ModifyTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
            => MainScrollViewer.ScrollToHome();
    }
}

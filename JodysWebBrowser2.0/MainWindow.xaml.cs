using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp.Wpf;
using CefSharp.Wpf.Internals;
using JodysWebBrowser2._0.controls;

namespace JodysWebBrowser2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _currentUrl;
        string _oldUrl;
        int _tabCount = 1;
        TabItem _currentTab;
        List<string> _urls = new List<string>();
        Window _window = new Window();

        public MainWindow()
        {
            InitializeComponent();
            //Sets object to main window
            _window = this.JBrowser;
            //Adds two tabs initally
            tabControl.Items.Add(new ClosableTab() { Title = "AddTabItem", Content = new ChromiumWebBrowser() { Name = "NewAddTab"  } });
            tabControl.Items.Add(new ClosableTab() { Title = "New Page", Content = new ChromiumWebBrowser() { Name = "NewPage" } });
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {   
        }

        private void btn_forward_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            NavigateSite();
        }
        //Swaps urls to correct format, and then Runs Current tabs browser to text
        public void NavigateSite()
        {
            string webPage = tb_search.Text.Trim();
            var currentBrowser = (ChromiumWebBrowser)_currentTab.Content;

            if (!webPage.Contains("http://"))
            {
                if (!webPage.Contains("www"))
                {
                    webPage = "http://www." + webPage;
                }
                else
                {
                    webPage = "http://" + webPage;
                }
            }

            if (currentBrowser != null)
            {
                if (_currentUrl == null)
                {
                    _currentUrl = webPage;
                    _urls.Add(webPage);
                    currentBrowser.Load(webPage);
                }
                else
                {
                    _oldUrl = _currentUrl;
                    currentBrowser.Load(webPage);
                    _urls.Add(webPage);
                    _currentUrl = _oldUrl;
                }
            }
        }

        //Need to attach still
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigateSite();
            }
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            var tab = (TabControl)sender;
            
            var selectedTab = (ClosableTab)tab.SelectedItem;
            if (selectedTab != null)
            {
                var header = (ClosableHeader)selectedTab.Header;
                if (header != null)
                {
                    if (header.label_TabTitle.Content.ToString() == "New Page")
                    {

                        if (tab.Items.Count == 1)
                        {
                            this.Close();
                        }
                        var newTab = new ClosableTab() { Title = "AddTabItem", Content = new ChromiumWebBrowser() { Name = "NewAddTab" + _tabCount, Address = "htpp://www.google.com" } };

                        tabControl.Items.Add(newTab);
                        tabControl.Items.Add(new ClosableTab() { Title = "New Page", Content = new ChromiumWebBrowser() { Name = "NewPage" } });
                        tabControl.Items.Remove(selectedTab);
                    }
                    else
                    {
                        _currentTab = selectedTab;
                    }
                }
            }
        }
        private void btn_close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

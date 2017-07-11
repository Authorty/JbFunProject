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
            _window = this.JBrowser;
            tabControl.Items.Add(new ClosableTab() { Title = "AddTabItem", Content = new ChromiumWebBrowser() { Name = "NewAddTab"  } });
            tabControl.Items.Add(new ClosableTab() { Title = "New Page", Content = new ChromiumWebBrowser() { Name = "NewPage" } });
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            //    webBrowser.WebBrowser.("eval", "document.execCommand('Stop');");
            //webBrowser.
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            //webBrowser.W
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            //webBrowser.Load("http://www.google.com");
            
        }

        private void btn_forward_Click(object sender, RoutedEventArgs e)
        {
            //if (webBrowser.CanGoForward)
            //{
            //    //webBrowser.WebBrowser.;
            //}
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            //if (webBrowser.CanGoBack)
            //{
            //    //webBrowser.GoBack();

            //}
        }
        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            NavigateSite();
        }
        public void NavigateSite()
        {
            string webPage = tb_search.Text.Trim();
            ChromiumWebBrowser currentBrowser = new ChromiumWebBrowser();
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

            currentBrowser = (ChromiumWebBrowser)_currentTab.Content;
                
            

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
                    //webBrowser.Address = webPage;
                    currentBrowser.Load(webPage);
                    // webBrowser.
                    _urls.Add(webPage);
                    _currentUrl = _oldUrl;
                }
            }

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigateSite();
            }

        }

        private void newTab(object sender, RoutedEventArgs e)
        {
            //tabControl.Items.Add(new TabItem() { Header = "TabItem", Content = new Grid() { } });

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
                        //var currentTabIndex = selectedTab.TabIndex;


                        //var tabtest = new ClosableTab();
                        //tabtest.Title = "smallTitle";
                        //tabtest.Header = "smallTitle";
                        //tabControl.Items.Add(tabtest);
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
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}

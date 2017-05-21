using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace SimpleVideoPlayer
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public bool IsLoding = false;
        public object obj = new object(); //下拉刷新的线程锁

        int page; //请求的初始页面为1

        private ViewModel.ViewModel viewmodel;
        private Model.VideoModel getVideoModel;

        private ScrollViewer listview_sc;

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.VisibilityChanged += WindowVisibilityChangedEventHandler;
            viewmodel = new ViewModel.ViewModel();
            this.DataContext = viewmodel;
            listview.ContainerContentChanging += listview_ContainerContentChanging;
            page = 1;

        }
        void WindowVisibilityChangedEventHandler(System.Object sender, Windows.UI.Core.VisibilityChangedEventArgs e)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    string json = await HttpRequest.HttpRequst.MainPageRequest(1, "C402758D1A773C4C70F160A11C1172E1");
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        getVideoModel = JsonConvert.DeserializeObject<Model.VideoModel>(json);
                    }
                    viewmodel.VideoModel = getVideoModel;
                }
                catch (Exception)
                {

                }
            });
            Task.Delay(1000);
        }
        private async void DEAm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = await HttpRequest.HttpRequst.MainPageRequest(1, "C402758D1A773C4C70F160A11C1172E1");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    getVideoModel = JsonConvert.DeserializeObject<Model.VideoModel>(json);
                }
                viewmodel.VideoModel = getVideoModel;
            }
            catch (Exception)
            {

            }
        }

        private async Task FirstStep(int page, string sign)
        {
            try
            {
                await Task.Factory.StartNew(async () =>
                {
                    string json = await HttpRequest.HttpRequst.MainPageRequest(page, "C402758D1A773C4C70F160A11C1172E1");
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        getVideoModel = JsonConvert.DeserializeObject<Model.VideoModel>(json);
                    }
                    foreach (Model.VideoModel.Contentlist f in getVideoModel.showapi_res_body.pagebean.contentlist)
                    {
                        viewmodel.VideoModel.showapi_res_body.pagebean.contentlist.Add(f);
                    }
                });
            }
            catch (Exception)
            {

            }
        }

        void listview_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            lock (obj)
                if (!IsLoding) //没有在读取中都能进
                    if (args.ItemIndex == listview.Items.Count - 1)
                    {
                        IsLoding = true;
                        Task.Factory.StartNew(async () =>
                        {
                            string json = await HttpRequest.HttpRequst.MainPageRequest(++page, "C402758D1A773C4C70F160A11C1172E1");
                            if (!string.IsNullOrWhiteSpace(json))
                            {
                                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    getVideoModel = JsonConvert.DeserializeObject<Model.VideoModel>(json);

                                    foreach (Model.VideoModel.Contentlist f in getVideoModel.showapi_res_body.pagebean.contentlist)
                                    {
                                        viewmodel.VideoModel.showapi_res_body.pagebean.contentlist.Add(f);
                                    }
                                    IsLoding = false;


                                });
                            }
                        });
                    }

        }
        private async Task Refresh()
        {
            try
            {
                string json = await HttpRequest.HttpRequst.MainPageRequest(1, "C402758D1A773C4C70F160A11C1172E1");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    getVideoModel = JsonConvert.DeserializeObject<Model.VideoModel>(json);
                }
                viewmodel.VideoModel = getVideoModel;
            }
            catch (Exception)
            {
            }
            await Task.Factory.StartNew(async () =>
            {
                progressRing1.Visibility = Visibility.Visible;
                await Task.Delay(100000);
               // progressRing1.Visibility = Visibility.Collapsed;
            }); 
            }
        private async void listview_Loaded(object sender, RoutedEventArgs e)
        {/*
            Get_Child(listview, 0);
           
            await Refresh();*/
        }

        private void Get_Child(DependencyObject o, int n)
        {
            try
            {
                int count = VisualTreeHelper.GetChildrenCount(o);
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var child = VisualTreeHelper.GetChild(o, count - 1);
                        if (n == 0)
                        {
                            if (child is ScrollViewer)
                            {
                                listview_sc = child as ScrollViewer;
                            }
                            else
                            {
                                Get_Child(VisualTreeHelper.GetChild(o, count - 1), n);
                            }
                        }
                        else if (n == 1)
                        {

                        }
                    }
                }
            }
            catch (Exception) { }

            }
       
    }
}

using Digiterra.DragDrop.Demo.Controls;
using Digiterra.DragDrop.Demo.Effects;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Digiterra.DragDrop.Demo
{
    public partial class App : Application
    {
        public static Dictionary<DraggableImage, DragInformation> AppDragDictionary = new Dictionary<DraggableImage, DragInformation>();
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

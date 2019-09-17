using Digiterra.DragDrop.Demo.Controls;
using Digiterra.DragDrop.Demo.Effects.Touch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Digiterra.DragDrop.Demo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Dictionary<DraggableImage, DragInformation> dragDictionary = new Dictionary<DraggableImage, DragInformation>();

        public MainPage()
        {
            InitializeComponent();
            //AddFirstImage();
            this.BindingContext = new MainViewModel();

            LoadSavedImages();
        }
       
        void LoadSavedImages()
        {
            if (App.AppDragDictionary.Any())
            {
                foreach (var item in App.AppDragDictionary)
                {
                    TouchEffect touchEffect = new TouchEffect();
                    touchEffect.TouchAction += OnTouchEffectAction;
                    item.Key.Effects.Add(touchEffect);
                    absoluteLayout.Children.Add(item.Key);
                }
            }
          

           
        }
        void OnTouchEffectAction(object sender, TouchEventArgs args)
        {
            DraggableImage draggableImage = sender as DraggableImage;

            switch (args.Type)
            {
                case TouchType.Pressed:
                    // Don't allow a second touch on an already touched BoxView
                    if (!dragDictionary.ContainsKey(draggableImage))
                    {
                        dragDictionary.Add(draggableImage, new DragInformation(args.Id, args.Location));
                        if (!App.AppDragDictionary.ContainsKey(draggableImage))
                        {
                            App.AppDragDictionary.Add(draggableImage, new DragInformation(args.Id, args.Location));

                        }
                        // Set Capture property to true
                        TouchEffect touchEffect = (TouchEffect)draggableImage.Effects.FirstOrDefault(e => e is TouchEffect);
                        touchEffect.Capture = true;
                    }
                    break;

                case TouchType.Moved:
                    if (dragDictionary.ContainsKey(draggableImage) && dragDictionary[draggableImage].Id == args.Id)
                    {
                        Rectangle rect = AbsoluteLayout.GetLayoutBounds(draggableImage);
                        Point initialLocation = dragDictionary[draggableImage].Point;
                        rect.X += args.Location.X - initialLocation.X;
                        rect.Y += args.Location.Y - initialLocation.Y;
                        AbsoluteLayout.SetLayoutBounds(draggableImage, rect);
                    }
                    break;

                case TouchType.Released:
                    if (dragDictionary.ContainsKey(draggableImage) && dragDictionary[draggableImage].Id == args.Id)
                    {
                        
                        dragDictionary.Remove(draggableImage);
                    }
                    break;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var draggableImage = sender as DraggableImage;
            if (draggableImage != null)
            {
                var newInstance = new DraggableImage() { Source = draggableImage.Source };
                TouchEffect touchEffect = new TouchEffect();
                touchEffect.TouchAction += OnTouchEffectAction;
                newInstance.Effects.Add(touchEffect);
                absoluteLayout.Children.Add(newInstance);
            }
        }
    }
}

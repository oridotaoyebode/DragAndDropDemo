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
        Dictionary<DraggableFontImage, DragInformation> dragDictionary = new Dictionary<DraggableFontImage, DragInformation>();

        public MainPage()
        {
            InitializeComponent();
            //AddFirstImage();
            this.BindingContext = new MainViewModel();

            LoadSavedImages();
        }
       
        void LoadSavedImages()
        {
            if (App.customModels.Any())
            {
                foreach (var item in App.customModels)
                {
                    TouchEffect touchEffect = new TouchEffect();
                    touchEffect.TouchAction += OnTouchEffectAction;
                    DraggableFontImage draggableFontImage = new DraggableFontImage()
                    {
                        FontFamily = item.DraggableFontImage.FontFamily,
                        FontSize = item.DraggableFontImage.FontSize,
                        TextColor = item.DraggableFontImage.TextColor,
                        Text = item.DraggableFontImage.Text,
                       

                    };
                    draggableFontImage.Effects.Add(touchEffect);
                    
                    absoluteLayout.Children.Add(draggableFontImage, item.DragInformation.Point);
                }
            }
          

           
        }
        void OnTouchEffectAction(object sender, TouchEventArgs args)
        {
            DraggableFontImage draggableFontImage = sender as DraggableFontImage;

            switch (args.Type)
            {
                case TouchType.Pressed:
                    // Don't allow a second touch on an already touched BoxView
                    if (!dragDictionary.ContainsKey(draggableFontImage))
                    {
                        dragDictionary.Add(draggableFontImage, new DragInformation(args.Id, args.Location));
                        if (!App.customModels.Any(r=> r.DraggableFontImage == draggableFontImage))
                        {
                            //App.AppDragDictionary.Add(draggableFontImage, new DragInformation(args.Id, args.Location));
                            App.customModels.Add(new CustomModel()
                            {
                                DraggableFontImage = draggableFontImage,
                                DragInformation = new DragInformation(args.Id, args.Location)
                            });

                        }
                        
                        // Set Capture property to true
                        TouchEffect touchEffect = (TouchEffect)draggableFontImage.Effects.FirstOrDefault(e => e is TouchEffect);
                        touchEffect.Capture = true;
                    }
                    break;

                case TouchType.Moved:
                    if (dragDictionary.ContainsKey(draggableFontImage) && dragDictionary[draggableFontImage].Id == args.Id)
                    {
                        Rectangle rect = AbsoluteLayout.GetLayoutBounds(draggableFontImage);
                        Point initialLocation = dragDictionary[draggableFontImage].Point;
                        rect.X += args.Location.X - initialLocation.X;
                        rect.Y += args.Location.Y - initialLocation.Y;
                        Console.WriteLine(rect.X);
                        Console.WriteLine(rect.Y);

                        var s = App.customModels.FirstOrDefault(r => r.DragInformation.Id == args.Id);
                        s.DragInformation = new DragInformation(args.Id, new Point(rect.X, rect.Y));
                        AbsoluteLayout.SetLayoutBounds(draggableFontImage, rect);
                    }
                    break;

                case TouchType.Released:
                    if (dragDictionary.ContainsKey(draggableFontImage) && dragDictionary[draggableFontImage].Id == args.Id)
                    {
                        
                        dragDictionary.Remove(draggableFontImage);
                    }
                    break;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var draggableImage = sender as DraggableFontImage;
            if (draggableImage != null)
            {
                var newInstance = new DraggableFontImage() { Text = draggableImage.Text, FontFamily = draggableImage.FontFamily, FontSize = draggableImage.FontSize };
                TouchEffect touchEffect = new TouchEffect();
                touchEffect.TouchAction += OnTouchEffectAction;
                newInstance.Effects.Add(touchEffect);
                absoluteLayout.Children.Add(newInstance);
            }
        }
    }
}

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
        string guidString = string.Empty;
        bool _savedImagesLoaded = false;
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
                _savedImagesLoaded = true;
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
                        ClassId = item.DragInformation.Id
                        
                       

                    };
                    draggableFontImage.Effects.Add(touchEffect);
                    
                    absoluteLayout.Children.Add(draggableFontImage, item.DragInformation.Point);
                }
            }
          

           
        }
        void OnTouchEffectAction(object sender, TouchEventArgs args)
        {
            DraggableFontImage draggableFontImage = sender as DraggableFontImage;
            if (!string.IsNullOrEmpty(draggableFontImage.ClassId))
            {
                guidString = draggableFontImage.ClassId;

            }
            switch (args.Type)
            {
                case TouchType.Pressed:
                    // Don't allow a second touch on an already touched BoxView
                    if (!dragDictionary.ContainsKey(draggableFontImage))
                    {
                        
                        Console.WriteLine("Pressed : Id" + args.Id);
                       
                        dragDictionary.Add(draggableFontImage, new DragInformation(guidString, args.Location));

                        
                        
                        // Set Capture property to true
                        TouchEffect touchEffect = (TouchEffect)draggableFontImage.Effects.FirstOrDefault(e => e is TouchEffect);
                        touchEffect.Capture = true;

                        //var doesExist = App.customModels.Any() && App.customModels.Where(r => r.DragInformation.Id == guidString) != null;
                        //if (!doesExist)
                        //{
                        //    App.customModels.Add(new CustomModel()
                        //    {
                        //        DraggableFontImage = draggableFontImage,
                        //        DragInformation = new DragInformation(guidString, args.Location)
                        //    });

                        //}
                    }
                    break;

                case TouchType.Moved:
                    //if (App.customModels.Any() && !_savedImagesLoaded)
                    //{
                    //    if (!string.IsNullOrEmpty(draggableFontImage.ClassId))
                    //    {
                    //        guidString = draggableFontImage.ClassId;

                    //    }
                    //}
                    if (dragDictionary.ContainsKey(draggableFontImage) && dragDictionary[draggableFontImage].Id == guidString)
                    {
                        Rectangle rect = AbsoluteLayout.GetLayoutBounds(draggableFontImage);
                        Point initialLocation = dragDictionary[draggableFontImage].Point;
                       

                        rect.X += args.Location.X - initialLocation.X;
                        rect.Y += args.Location.Y - initialLocation.Y;
                        Console.WriteLine("Moved :  X" + rect.X);
                        Console.WriteLine("Moved :  Y" + rect.Y);

                        App.customModels.FirstOrDefault(r => r.DragInformation.Id == guidString).DragInformation = new DragInformation(guidString, new Point(rect.X, rect.Y));
                        AbsoluteLayout.SetLayoutBounds(draggableFontImage, rect);
                       
                    }
                    break;

                case TouchType.Released:
                    //if (App.customModels.Any() && !_savedImagesLoaded)
                    //{
                    //    if (!string.IsNullOrEmpty(draggableFontImage.ClassId))
                    //    {
                    //        guidString = draggableFontImage.ClassId;

                    //    }
                    //}
                    if (dragDictionary.ContainsKey(draggableFontImage) && dragDictionary[draggableFontImage].Id == guidString)
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
                guidString = Guid.NewGuid().ToString();
                var newInstance = new DraggableFontImage() { TextColor = draggableImage.TextColor, Text = draggableImage.Text, FontFamily = draggableImage.FontFamily, FontSize = draggableImage.FontSize, InputTransparent = false };
                TouchEffect touchEffect = new TouchEffect();
                touchEffect.TouchAction += OnTouchEffectAction;
                newInstance.Effects.Add(touchEffect);
                absoluteLayout.Children.Add(newInstance);
                App.customModels.Add(new CustomModel()
                {
                    DraggableFontImage = draggableImage,
                    DragInformation = new DragInformation(guidString, new Point())
                });

            }
        }
    }
}

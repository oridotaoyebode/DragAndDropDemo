using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Digiterra.DragDrop.Demo.Effects.Touch
{
    public class TouchEffect : RoutingEffect
    {
        public event TouchEventHandler TouchAction;

        public TouchEffect() : base("Digiterra.TouchEffect")
        {
        }

        public bool Capture { set; get; }

        public void OnTouchAction(Element element, TouchEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }
}

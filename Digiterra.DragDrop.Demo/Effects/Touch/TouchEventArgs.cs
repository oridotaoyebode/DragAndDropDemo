using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Digiterra.DragDrop.Demo.Effects.Touch
{

    public class TouchEventArgs: EventArgs
    {
        public TouchEventArgs(long id, TouchType type, Point location, bool isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public TouchType Type { private set; get; }

        public Point Location { private set; get; }

        public bool IsInContact { private set; get; }
    }
}

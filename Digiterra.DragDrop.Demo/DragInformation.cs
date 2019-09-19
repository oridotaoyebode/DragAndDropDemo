using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Digiterra.DragDrop.Demo
{
    public class DragInformation
    {
        public DragInformation(string id, Point point)
        {
            Id = id;
            Point = point;
        }

        public string Id { get; private set; }

        public Point Point { get; private set; }
    }
}

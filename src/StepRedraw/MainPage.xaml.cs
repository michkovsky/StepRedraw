using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StepRedraw
{
    public partial class MainPage : ContentPage
    {
        SKPath basePath = Enumerable.Range(100, 300)
            .Select(x => new SKPoint(x, (float)Math.Pow(x, 1.5) / 100))
            .Aggregate(new SKPath(), (acc, point) => {
                Action<SKPoint> action = (acc.Points.Length > 0) ? (Action<SKPoint>)acc.LineTo : acc.MoveTo;
                action.Invoke(point);
                return acc;
            });
        public MainPage()
        {
            InitializeComponent();
        }
        List<SKPath> paths = new List<SKPath>();
        SKPath path = new SKPath();
        bool ShowBase = true;
        private void Canvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Violet.ToSKColor(),
                StrokeWidth = 25,
            };

            if (ShowBase) canvas.DrawPath(this.basePath, paint);

            paths.ForEach(p => canvas.DrawPath(p, paint));
            if(null!=path) canvas.DrawPath(path, paint);
            //canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);

        }


        private void Canvas_Touch(object sender, SKTouchEventArgs e)
        {
            if (Debugger.IsLogging()) Console.WriteLine(e);
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    // start of a stroke
                    ShowBase = false;
                    path = new SKPath();
                    path.MoveTo(e.Location);
                    break;
                case SKTouchAction.Moved:
                    // the stroke, while pressed
                    if (e.InContact)
                        path.LineTo(e.Location);
                    break;
                case SKTouchAction.Released:
                    // end of a stroke
                    ShowBase = true;
                    paths.Add(path);
                    path = null;
                    //path.Close();
                    break;
                case SKTouchAction.Cancelled:
                    // we don't want that stroke
                    ShowBase = true;
                    path = new SKPath();
                    break;
            }

            // we have handled these events
            e.Handled = true;

            // update the UI
            ((SKCanvasView)sender).InvalidateSurface();
        }
    }
}

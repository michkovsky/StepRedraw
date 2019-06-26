using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace StepRedraw.GTK
{
    public class Program
    {
        // This is the main entry point of the application.
        [STAThread]
        static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();

            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("Step Redraw");
            window.Show();

            Gtk.Application.Run();
        }
    }
}

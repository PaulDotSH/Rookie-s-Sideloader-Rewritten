using System;
using Gtk;

namespace RSL
{
    class Program
    {
        //Without making a new settings object, it wouldn't be serializable with JSON
        public static Settings settings = new Settings();
        public static string Name = "Rookie's Sideloader";
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            var app = new Application("org.RSLR.RSLR", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}

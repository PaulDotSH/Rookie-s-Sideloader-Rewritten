using System;
using Gtk;

namespace RSL
{
    class Program
    {
        public static Settings settings = new Settings();
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

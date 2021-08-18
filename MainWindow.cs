using System;
using Gdk;
using GLib;
using Gtk;
using Application = Gtk.Application;
using UI = Gtk.Builder.ObjectAttribute;
using Window = Gtk.Window;
using System.Threading;
using Thread = System.Threading.Thread;

namespace RSL
{
    class MainWindow : Window
    {
        [UI] private Label _label1 = null;
        [UI] private Button _button1 = null;
        [UI] private ColorButton _GtkColorButton = null;

        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            Gtk.Settings.Default.ApplicationPreferDarkTheme = true;
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
            _GtkColorButton.Title = "test";
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
        static ADB adb = new ADB();
        private void Button1_Clicked(object sender, EventArgs a)
        {
            _label1.Text = adb.RunCommand("version").Output;
        }
    }
}
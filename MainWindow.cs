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
        static ADB adb = new ADB();
        #region UI
        [UI] private Label _label1 = null;
        [UI] private Button _button1 = null;
        [UI] private ColorButton _GtkColorButton = null;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
            
        }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            //Before Load
            BeforeUILoaded();
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
            _GtkColorButton.Title = "test";
            AfterUILoaded();
        }
        #endregion
        #region UI_Events
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
        private void Button1_Clicked(object sender, EventArgs a)
        {
            Program.settings.Save();
            _label1.Text = adb.RunCommand("version").Output;
        }

        private void BeforeUILoaded()
        {
            Program.settings.Initialise();
        }

        private void AfterUILoaded()
        {
            
        }
        #endregion
    }
}
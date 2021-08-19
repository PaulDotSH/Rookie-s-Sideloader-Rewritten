using System;
using Gdk;
using GLib;
using Gtk;
using Application = Gtk.Application;
using UI = Gtk.Builder.ObjectAttribute;
using Window = Gtk.Window;
using System.Threading;
using Thread = System.Threading.Thread;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RSL
{
    class MainWindow : Window
    {
        static ADB adb = new ADB();
        #region UI
        [UI] private Label _label1 = null;
        [UI] private Button _button1 = null;
        [UI] private Notebook _notebook = null;
        [UI] private Button _sideloadButton = null;
        [UI] private LevelBar _levelBar = null;
        [UI] private Spinner _spinner = null;
        [UI] private ComboBoxText _devicesComboBox = new ComboBoxText();
        [UI] private Button _refreshDevicesButton = null;
        [UI] private TextView _infoTextView = new TextView();
        [UI] private Button _clearConsoleButton = null;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }
        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            //Before Load
            BeforeUILoaded();
            builder.Autoconnect(this);
            
            //Add events
            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
            _sideloadButton.Clicked += SideloadButton_Clicked;
            _notebook.DragDrop += SideloadLayout_DragBegin;
            _refreshDevicesButton.Clicked += RefreshDevicesButton_Click;
            _clearConsoleButton.Clicked += (o, e) => _infoTextView.Buffer.Text = "";

            _levelBar.Visible = false;
            //_spinner.Active = true;
            //After UI was loaded
            AfterUILoaded();
        }

        private void RefreshDevicesButton_Click(object? sender, EventArgs e)
        {
            CheckForDevices();
        }

        public void Log(string text)
        {
            _infoTextView.Buffer.Text += text + "\n";
        }
        private void CheckForDevices()
        {
            _devicesComboBox.RemoveAll();
            foreach (string device in adb.GetAllDevices())
            {
                Log($"{device} is connected.");
                _devicesComboBox.AppendText(device);
            }
        }
        #endregion
        #region UI_Events
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void SideloadButton_Clicked(object sender, EventArgs e)
        {
            string path = "";
            Gtk.FileChooserDialog filechooser =
                new Gtk.FileChooserDialog("Choose the file to open",
                    this,
                    FileChooserAction.Open,
                    "Cancel",ResponseType.Cancel,
                    "Open",ResponseType.Accept);

            if (filechooser.Run() == (int)ResponseType.Accept)
            {
                path = filechooser.Filename;
                MessageBoxShow(path);
            }
            filechooser.Destroy();
        }

        // private void a()
        // {
        //     Dialog dialog = null;
        //     ResponseType response = ResponseType.None;
        //
        //     try {
        //         dialog = new Dialog (
        //             "Dialog Title",
        //             this,
        //             DialogFlags.DestroyWithParent | DialogFlags.Modal,
        //             "Overwrite file", ResponseType.Yes,
        //             "Cancel", ResponseType.No
        //         );
        //         //dialog. (new Label ("Dialog contents"));
        //         dialog.ShowAll ();
        //
        //         response = (ResponseType) dialog.Run ();
        //     } finally {
        //         if (dialog != null)
        //             dialog.Destroy ();
        //     }
        // }
        private void MessageBoxShow(string message)
        {
            Gtk.MessageDialog messageDialog = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,message);
            messageDialog.ShowAll();
            
            var response = (ResponseType) messageDialog.Run ();
            if (messageDialog != null)
                messageDialog.Destroy();
        }
        
        private void SideloadLayout_DragBegin(object sender, Gtk.DragDropArgs a)
        {
            Console.WriteLine("DRAG STARTED!");
            TitleHandler.ChangeTitle(this,"DragStarted!");
        }
        private void Button1_Clicked(object sender, EventArgs a)
        {
            Program.settings.Save();
            _label1.Text = adb.RunCommand("version").Output;
        }

        private void BeforeUILoaded()
        {
            Program.settings.Initialise();
            if (!Program.settings.DebugMode)
                Updater.IsUpdateAvailable();
        }



        private static void LevelBarTimerCallBack()
        {
            
        }

        private void AfterUILoaded()
        {
            TitleHandler.ChangeTitle(this,TitleHandler.DefaultTitle);
            CheckForDevices();
        }
        #endregion
    }
}
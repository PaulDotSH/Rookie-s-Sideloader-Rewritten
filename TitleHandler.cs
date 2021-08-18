using Gtk;

namespace RSL
{
    public class TitleHandler
    {
        public static string DefaultTitle = $"{Program.Name} v{Updater.CurrentVersion}";
        public static void ChangeTitle(Window window, string title)
        {
            window.Title = title;
        }
    }
}
using DownloadData.Model;
using DownloadData.View;
using DownloadDefect;
using DownloadData._Repositories;
using DownloadData.Presenter;

namespace DownloadData
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new MainForm());
            ApplicationConfiguration.Initialize();
            IMainForm mainView = new MainForm();
            IDefectRepository repository = new DefectRepository();
            new MainFormPresenter(mainView, repository);
            Application.Run((Form)mainView);
        }
    }
}
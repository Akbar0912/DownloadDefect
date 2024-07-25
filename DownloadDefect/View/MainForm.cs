using DownloadData.Model;
using DownloadData.Presenter;
using DownloadData.View;
using DownloadData._Repositories;
using DownloadDefect.View;

namespace DownloadDefect
{
    public partial class MainForm : Form, IMainForm
    {
        private TabControlPresenter tabControlPresenter;
        private readonly DefectModel _defectModel;
        private TabControlView _tabControlView;
        public MainForm()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            InitializeTabControl();
        }

        private void InitializeTabControl()
        {
            _tabControlView = new TabControlView();
            TabControlDataPresenter presenterData = new TabControlDataPresenter(_tabControlView, new DefectRepository(), new WarrantyRepository());
            tabControlPresenter = new TabControlPresenter(presenterData);
            splitContainer1.Panel2.Controls.Add(_tabControlView);
            _tabControlView.Dock = DockStyle.Fill;
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnDefect.Click += delegate
            {
                int selectedTabPageIndex = 0;
                tabControlPresenter.ChangeTabPage(selectedTabPageIndex);
                btnDefect.BackColor = Color.FromArgb(0, 133, 181);
                btnWarranty.BackColor = Color.Teal;
            };

            btnWarranty.Click += delegate
            {
                int selectedTabPageIndex = 1;
                tabControlPresenter.ChangeTabPage(selectedTabPageIndex);
                btnWarranty.BackColor = Color.FromArgb(0, 133, 181);
                btnDefect.BackColor = Color.Teal;
            };
        }


        private static MainForm instance;
        public static MainForm GetInstance()
        {
            // Dispose the old instance if it exists and is not disposed
            if (instance != null && !instance.IsDisposed)
            {
                instance.Dispose();
            }

            // Create a new instance
            //instance = new MainForm(loginModel);

            // Set window state and bring to front if necessary
            if (instance.WindowState == FormWindowState.Minimized)
                instance.WindowState = FormWindowState.Normal;

            //if (instance._user != loginModel)
            //{
            //    instance._user = loginModel;
            //}
                instance.InitializeTabControl();

            return instance;
        }
    }
}

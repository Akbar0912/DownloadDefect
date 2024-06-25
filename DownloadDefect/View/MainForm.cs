using DownloadDefect.View;

namespace DownloadDefect
{
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        public string Search
        {
            get { return textBoxSearch.Text; }
            set { textBoxSearch.Text = value; }
        }

        public DateTime SelectedDate => dtFromDate.Value;

        public event EventHandler SearchFilter;
        public event EventHandler<object> ExportDataGridView;

        public void SetDefectListBindingSource(BindingSource defectList)
        {
            dataGridView1.DataSource = defectList;
        }


        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate
            {
                SearchFilter?.Invoke(this, EventArgs.Empty);
            };

            btnClear.Click += delegate
            {
                textBoxSearch.Text = "";
                textBoxSearch.Focus();
                dtFromDate.Text = DateTime.Now.ToString();
                SearchFilter?.Invoke(this, EventArgs.Empty);
            };

            btnDwn.Click += (sender, e) =>
            {
                ExportDataGridView?.Invoke(this, dataGridView1.DataSource);
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

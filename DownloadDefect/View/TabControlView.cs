using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadDefect.View
{
    public partial class TabControlView : UserControl, ITabControlView
    {
        public TabControlView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        public string Search
        {
            get => textBoxSearch.Text;
            set => textBoxSearch.Text = value;
        }

        public DateTime SelectedDate => dtFromDate.Value;

        public event EventHandler SearchFilter;
        public event EventHandler<object> ExportDataGridView;

        public DataGridView GetDataGridView1()
        {
            return dataGridView1;
        }

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

            dataGridView1.RowPostPaint += (sender, e) =>
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                row.Cells["No"].Value = (e.RowIndex + 1).ToString();
                row.Cells["No"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchFilter?.Invoke(this, EventArgs.Empty);
            }
        }

        private void dtFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchFilter?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SelectTabPageByIndex(int data)
        {
            if (data >= 0 && data < tabControl1.TabPages.Count)
            {
                tabControl1.SelectedIndex = data;
            }
        }
    }
}

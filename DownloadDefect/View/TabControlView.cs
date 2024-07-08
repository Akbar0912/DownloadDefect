using DownloadData.View;
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
        public DateTime SelectedDate2 => dtFromDate2.Value;
        public DateTime SelectedDate3 => dtFromDate3.Value;

        public string SearchWarranty
        {
            get => textBoxSearch2.Text;
            set => textBoxSearch2.Text = value;
        }
        public string SearchPacking
        {
            get => textBoxSearch3.Text;
            set => textBoxSearch3.Text = value;
        }


        public event EventHandler<object> ExportDataGridView;
        public event EventHandler<object> ExportDataGridView2;
        public event EventHandler<object> ExportDataGridView3;
        public event EventHandler SearchFilter;
        public event EventHandler SearchFilter2;
        public event EventHandler SearchFilter3;

        public DataGridView GetDataGridView3()
        {
            return dataGridView3;
        }

        public DataGridView GetDataGridView2()
        {
            return dataGridView2;
        }

        public DataGridView GetDataGridView1()
        {
            return dataGridView1;
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate
            {
                SearchFilter?.Invoke(this, EventArgs.Empty);
            };

            btnSearch2.Click += delegate
            {
                SearchFilter2?.Invoke(this, EventArgs.Empty);
            };

            btnSearch3.Click += delegate
            {
                SearchFilter3?.Invoke(this, EventArgs.Empty);
            };

            btnClear.Click += delegate
            {
                textBoxSearch.Text = "";
                textBoxSearch.Focus();
                dtFromDate.Text = DateTime.Now.ToString();
                SearchFilter?.Invoke(this, EventArgs.Empty);
            };

            btnClear2.Click += delegate
            {
                textBoxSearch2.Text = "";
                textBoxSearch2.Focus();
                dtFromDate2.Text = DateTime.Now.ToString();
                SearchFilter2?.Invoke(this, EventArgs.Empty);
            };

            btnClear3.Click += delegate
            {
                textBoxSearch3.Text = "";
                textBoxSearch3.Focus();
                dtFromDate3.Text = DateTime.Now.ToString();
                SearchFilter3?.Invoke(this, EventArgs.Empty);
            };

            btnDwn.Click += (sender, e) =>
            {
                ExportDataGridView?.Invoke(this, dataGridView1.DataSource);
            };

            btnDwn2.Click += (sender, e) =>
            {
                ExportDataGridView2?.Invoke(this, dataGridView2.DataSource);
            };

            btnDwn3.Click += (sender, e) =>
            {
                ExportDataGridView3?.Invoke(this, dataGridView2.DataSource);
            };

            dataGridView1.RowPostPaint += (sender, e) =>
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                row.Cells["No"].Value = (e.RowIndex + 1).ToString();
                row.Cells["No"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersHeight = 40;

            dataGridView2.RowPostPaint += (sender, e) =>
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                row.Cells["No2"].Value = (e.RowIndex + 1).ToString();
                row.Cells["No2"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };

            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.ColumnHeadersHeight = 40;

            dataGridView3.RowPostPaint += (sender , e) =>
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                row.Cells["No3"].Value = (e.RowIndex + 1).ToString();
                row.Cells["No3"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };

            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.ColumnHeadersHeight = 40;
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

        public void SetDefectListBindingSource1(BindingSource defectList)
        {
            dataGridView1.DataSource = defectList;
        }

        public void SetDefectListBindingSource2(BindingSource defectList)
        {
            dataGridView2.DataSource = defectList;
        }

        public void SetDefectListBindingSource3(BindingSource defectList)
        {
            dataGridView3.DataSource = defectList;
        }

        private void textBoxSearch2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchFilter2?.Invoke(this, EventArgs.Empty);
            }
        }

        private void dtFromDate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchFilter2?.Invoke(this, EventArgs.Empty);
            }
        }

        private void textBoxSearch3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchFilter3?.Invoke(this, EventArgs.Empty);
            }
        }

        private void dtFromDate3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchFilter3?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

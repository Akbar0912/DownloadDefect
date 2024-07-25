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
using System.Windows.Forms;

namespace DownloadDefect.View
{
    public partial class TabControlView : UserControl, ITabControlView
    {
        public TabControlView()
        {
            InitializeComponent();
            ConfigureTabControl();
            AssociateAndRaiseViewEvents();       
        }


        public string Search
        {
            get => textBoxSearch.Text;
            set => textBoxSearch.Text = value;
        }

        public DateTime SelectedDate => dtFromDate.Value;
        public DateTime SelectedDate2 => dtFromDate2.Value;

        public string SearchWarranty
        {
            get => textBoxSearch2.Text;
            set => textBoxSearch2.Text = value;
        }

        public event EventHandler<object> ExportDataGridView;
        public event EventHandler<object> ExportDataGridView2;
        public event EventHandler<object> ExportDataGridView3;
        public event EventHandler SearchFilter;
        public event EventHandler SearchFilter2;
        public event EventHandler SearchFilter3;

        public DataGridView GetDataGridView1() => dataGridView1;
        public DataGridView GetDataGridView2() => dataGridView2;

        private void ConfigureTabControl()
        {
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += (s, e) => SearchFilter?.Invoke(this, EventArgs.Empty);
            btnSearch2.Click += (s, e) => SearchFilter2?.Invoke(this, EventArgs.Empty);

            btnClear.Click += (s, e) => ClearSearch(textBoxSearch, dtFromDate, SearchFilter);
            btnClear2.Click += (s, e) => ClearSearch(textBoxSearch2, dtFromDate2, SearchFilter2);

            btnDwn.Click += (s, e) => ExportDataGridView?.Invoke(this, dataGridView1.DataSource);
            btnDwn2.Click += (s, e) => ExportDataGridView2?.Invoke(this, dataGridView2.DataSource);

            AddRowPostPaintHandler(dataGridView1, "No");
            AddRowPostPaintHandler(dataGridView2, "No2");

            ConfigureDataGridView(dataGridView1);
            ConfigureDataGridView(dataGridView2);

            textBoxSearch.KeyDown += (s, e) => HandleEnterKey(e, SearchFilter);
            textBoxSearch2.KeyDown += (s, e) => HandleEnterKey(e, SearchFilter2);

            dtFromDate.KeyDown += (s, e) => HandleEnterKey(e, SearchFilter);
            dtFromDate2.KeyDown += (s, e) => HandleEnterKey(e, SearchFilter2);
        }

        private void HandleEnterKey(KeyEventArgs e, EventHandler eventHandler)
        {
            if (e.KeyCode == Keys.Enter)
            {
                eventHandler?.Invoke(this, EventArgs.Empty);
            }
        }

        private void ConfigureDataGridView(DataGridView dataGridView)
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeight = 40;
        }

        private void AddRowPostPaintHandler(DataGridView dataGridView, string columnName)
        {
            dataGridView.RowPostPaint += (s, e) =>
            {
                var row = dataGridView.Rows[e.RowIndex];
                row.Cells[columnName].Value = (e.RowIndex + 1).ToString();
                row.Cells[columnName].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };
        }

        private void ClearSearch(TextBox textBox, DateTimePicker dateTimePicker, EventHandler eventHandler)
        {
            textBox.Text = string.Empty;
            textBox.Focus();
            dateTimePicker.Value = DateTime.Now;
            eventHandler?.Invoke(this, EventArgs.Empty);
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

        public void SetDefectListBindingSource1(BindingSource defectList) => dataGridView1.DataSource = defectList;
        public void SetDefectListBindingSource2(BindingSource defectList) => dataGridView2.DataSource = defectList;

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

        public void ShowProgressBar()
        {
            progressBar1.Visible = true;
            progressBar2.Visible = true;
        }

        public void HideProgressBar()
        {
            progressBar1.Visible = false;
            progressBar2.Visible = false;
        }

        public void InitializeProgressBar(int max)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = max;
            progressBar1.Value = 0;
        }

        public void UpdateProgressBar(int value)
        {
            if (progressBar1.InvokeRequired && progressBar2.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = Math.Min(value, progressBar1.Maximum);
                }));
                progressBar2.Invoke(new Action(() =>
                {
                    progressBar2.Value = Math.Min(value, progressBar2.Maximum);
                }));
            }
            else
            {
                progressBar1.Value = Math.Min(value, progressBar1.Maximum);
                progressBar2.Value = Math.Min(value, progressBar2.Maximum);
            }
        }
    }
}

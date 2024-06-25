using DownloadDefect._Repositories;
using DownloadDefect.Model;
using DownloadDefect.View;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadDefect.Presenter
{
    public class MainFormPresenter
    {
        private IMainForm _view;
        private IDefectRepository _repository;
        private IEnumerable<DefectModel> defectList;
        private BindingSource defectsBindingSource;

        public MainFormPresenter(IMainForm view, IDefectRepository repository)
        {
            _view = view;
            _repository = repository;
            defectsBindingSource = new BindingSource();

            _view.SetDefectListBindingSource(defectsBindingSource);

            LoadAllResultDefect();

            _view.SearchFilter += SearchFilter;
            _view.ExportDataGridView += ExportDataGridView;

            _view.Show();
        }

        public void LoadAllResultDefect()
        {
            //_repository.TestDatabaseConnection();
            defectList = _repository.GetAllResult();
            defectsBindingSource.DataSource = defectList;
            _view.SetDefectListBindingSource(defectsBindingSource);
        }

        public void SearchFilter(object sender, EventArgs e)
        {
            defectList = _repository.GetFilter(_view.Search, _view.SelectedDate);
            defectsBindingSource.DataSource = defectList;
            _view.SetDefectListBindingSource(defectsBindingSource);
        }

        public void ExportDataGridView(object sender, object dataSource)
        {
            if (sender is DataGridView dataGridView)
            {
                SaveDataGridViewToCSV(dataGridView);
            }
        }

        private void SaveDataGridViewToCSV(DataGridView dataGridView)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "DataExport.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportDataGridViewToCSV(dataGridView, sfd.FileName);
                }
            }
        }

        private void ExportDataGridViewToCSV(DataGridView dgv, string fileName)
        {
            try
            {
                StringBuilder csv = new StringBuilder();

                // Add header row
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    csv.Append(dgv.Columns[i].HeaderText);
                    if (i < dgv.Columns.Count - 1)
                    {
                        csv.Append(",");
                    }
                }
                csv.AppendLine();

                // Add rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            csv.Append(row.Cells[i].Value?.ToString());
                            if (i < dgv.Columns.Count - 1)
                            {
                                csv.Append(",");
                            }
                        }
                        csv.AppendLine();
                    }
                }

                // Write to file
                System.IO.File.WriteAllText(fileName, csv.ToString());
                //_view.ShowMessage("Data exported successfully.", "Export", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //_view.ShowMessage($"Error exporting data: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
    }
}

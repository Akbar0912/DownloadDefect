using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using DownloadData.Model;
using DownloadData.View;
using Microsoft.Office.Interop.Excel;

namespace DownloadData.Presenter
{
    public class TabControlPresenter
    {
        private ITabControlView _tabControl;
        private IDefectRepository _defectRepository;
        private IWarrantyRepository _warrantyRepository;
        private BindingSource defectsBindingSource;
        private BindingSource warrantyBindingSource;

        public TabControlPresenter(TabControlDataPresenter data)
        {
            _tabControl = data.View;
            _defectRepository = data._defectRepository;
            _warrantyRepository = data._warrantyRepository;

            defectsBindingSource = new BindingSource();
            warrantyBindingSource = new BindingSource();

            _tabControl.SetDefectListBindingSource1(defectsBindingSource);
            _tabControl.SetDefectListBindingSource2(warrantyBindingSource);

            LoadAllResults();
            SubscribeToEvents();

            _tabControl.Show();
        }

        private void LoadAllResults()
        {
            LoadAllResultDefect();
            LoadAllResultWarranty();
        }

        private void SubscribeToEvents()
        {
            _tabControl.SearchFilter += (s, e) => ApplyFilter(_defectRepository.GetFilter, _tabControl.Search, _tabControl.SelectedDate, defectsBindingSource);
            _tabControl.SearchFilter2 += (s, e) => ApplyFilter(_warrantyRepository.GetFilter, _tabControl.SearchWarranty, _tabControl.SelectedDate2, warrantyBindingSource);

            _tabControl.ExportDataGridView += (s, e) => ExportDataGridView(_tabControl.GetDataGridView1(), "Data Defect");
            _tabControl.ExportDataGridView2 += (s, e) => ExportDataGridView(_tabControl.GetDataGridView2(), "Data Warranty Card");
        }

        private void ApplyFilter<TModel>(Func<string, DateTime, IEnumerable<TModel>> getFilterMethod, string search, DateTime date, BindingSource bindingSource)
        {
            var filterResult = getFilterMethod(search, date);
            bindingSource.DataSource = filterResult.ToList();
        }


        private void ExportDataGridView(DataGridView dataGridView, string defaultFileName)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                sfd.FileName = $"{defaultFileName} {DateTime.Now:dd-MM-yyyy}_at_{DateTime.Now:HH.mm.ss}";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportDataGridViewToExcel(dataGridView, sfd.FileName);
                }
            }
        }

        private void ExportDataGridViewToExcel(DataGridView dgv, string fileName)
        {
            Excel.Application excelApp = null;
            Workbook workbook = null;

            try
            {
                _tabControl.ShowProgressBar();
                excelApp = new Excel.Application { Visible = false };
                workbook = excelApp.Workbooks.Add(Type.Missing);
                var worksheet = (Excel.Worksheet)workbook.Sheets["Sheet1"];

                ExportDataGridViewWorksheet(workbook, defectsBindingSource, _tabControl.GetDataGridView1(), "Data Defect");
                ExportDataGridViewWorksheet(workbook, warrantyBindingSource, _tabControl.GetDataGridView2(), "Data Warranty Card");

                workbook.SaveAs(fileName);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show("Data successfully exported!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //catch (Exception ex)
            //{
            //    //MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            finally
            {

                workbook = null;
                excelApp = null;
                _tabControl.HideProgressBar();

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void ExportDataGridViewWorksheet(Workbook workbook, BindingSource bindingSource, DataGridView dataGridView, string baseName)
        {
            var worksheet = (Excel.Worksheet)workbook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            worksheet.Name = baseName;

            WriteColumnHeaders(dataGridView, worksheet); // Menggunakan DataGridView untuk header
            WriteDataRows(bindingSource, worksheet, dataGridView);
        }

        private void FormatWorksheet(Worksheet worksheet)
        {
            Excel.Range usedRange = worksheet.UsedRange;

            // Format the header row
            Excel.Range headerRow = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, usedRange.Columns.Count]];
            headerRow.Font.Bold = true;
            headerRow.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            headerRow.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            headerRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRow.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            // Format the data rows
            Excel.Range dataRange = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[usedRange.Rows.Count, usedRange.Columns.Count]];
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            dataRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            dataRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            // Set all columns to a fixed width
            double columnWidth = 15; // You can adjust this value as needed
            usedRange.Columns.ColumnWidth = columnWidth;

            // Set all rows to a fixed height
            double rowHeight = 20; // You can adjust this value as needed
            usedRange.Rows.RowHeight = rowHeight;
        }

        private void WriteColumnHeaders(DataGridView dgv, Worksheet worksheet)
        {
            for (int i = 1; i <= dgv.Columns.Count; i++)
            {
                worksheet.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
            }
        }

        private void WriteDataRows(BindingSource bindingSource, Worksheet worksheet, DataGridView dataGridView)
        {
            var dataSource = (IEnumerable<object>)bindingSource.DataSource;
            var properties = dataSource.FirstOrDefault()?.GetType().GetProperties();

            if (properties == null)
                return;

            int rowIndex = 2; // Dimulai dari baris kedua (setelah header)
            foreach (var item in dataSource)
            {
                int columnIndex = 1;
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    var columnName = dataGridView.Columns[j].Name;
                    if (columnName == "ID")
                    {
                        worksheet.Cells[rowIndex, columnIndex] = (rowIndex - 1).ToString(); // Nomor urut dimulai dari 1
                    }
                    else
                    {
                        var prop = properties.FirstOrDefault(p => p.Name == columnName);
                        worksheet.Cells[rowIndex, columnIndex] = prop?.GetValue(item)?.ToString();
                    }
                    columnIndex++;
                }
                rowIndex++;

                _tabControl.UpdateProgressBar(rowIndex - 2);
            }
        }

        private void LoadAllResultDefect()
        {
            var defects = _defectRepository.GetAllResult().ToList();
            defectsBindingSource.DataSource = defects;
        }

        private void LoadAllResultWarranty()
        {
            var warranties = _warrantyRepository.GetAll().ToList();
            warrantyBindingSource.DataSource = warranties;
        }

        public void ChangeTabPage(int index)
        {
            _tabControl.SelectTabPageByIndex(index);
        }
    }
}

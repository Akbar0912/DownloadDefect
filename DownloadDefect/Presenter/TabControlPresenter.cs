using DownloadDefect.Model;
using DownloadDefect.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace DownloadDefect.Presenter
{
    public class TabControlPresenter
    {
        private ITabControlView _tabControl;
        private IDefectRepository _defectRepository;
        private IEnumerable<DefectModel> defectList;
        private BindingSource defectsBindingSource;

        public TabControlPresenter(TabControlDataPresenter data)
        {
            _tabControl = data.View;
            _defectRepository = data._defectRepository;

            defectsBindingSource = new BindingSource();

            _tabControl.SetDefectListBindingSource(defectsBindingSource);
            LoadAllResultDefect();

            _tabControl.SearchFilter += SearchFilter;
            _tabControl.ExportDataGridView += ExportDataGridView;

            _tabControl.Show();
        }

        private void ExportDataGridView(object? sender, object e)
        {
            var dataGridView = _tabControl.GetDataGridView1();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                string day = DateTime.Now.Day.ToString();
                string month = DateTime.Now.Month.ToString();
                string year = DateTime.Now.Year.ToString();

                string hour = DateTime.Now.Hour.ToString();
                string minute = DateTime.Now.Minute.ToString();
                string second = DateTime.Now.Second.ToString();

                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                sfd.FileName = "Data Defect " + day + "-" + month + "-" + year + "_at_" + hour + "." + minute + "." + second;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportDataGridViewToExcel(dataGridView, sfd.FileName);
                }
            }
        }

        private void ExportDataGridViewToExcel(DataGridView dgv, string fileName)
        {

            try
            {
                // Create a new Excel application instance
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false; // We don't need to show the Excel window

                // Create a new workbook
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Defect Data";

                // Add column headers to the worksheet
                for (int i = 1; i <= dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                }

                // Add rows to the worksheet
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Format the header row
                Excel.Range headerRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, dgv.Columns.Count]];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = Excel.XlRgbColor.rgbLightGray;
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Autofit columns
                worksheet.Columns.AutoFit();

                // Set row height for all rows to be the same
                for (int i = 1; i <= dgv.Rows.Count + 1; i++) // +1 to include the header row
                {
                    Excel.Range row = worksheet.Rows[i];
                    row.RowHeight = 20; // Set the desired row height here
                }

                // Disable text wrap for all cells
                Excel.Range entireRange = worksheet.UsedRange;
                entireRange.WrapText = false;

                // Save the workbook
                workbook.SaveAs(fileName);
                workbook.Close(false, Type.Missing, Type.Missing);
                excelApp.Quit();

                // Release COM objects
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Data exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchFilter(object? sender, EventArgs e)
        {
            defectList = _defectRepository.GetFilter(_tabControl.Search, _tabControl.SelectedDate);
            defectsBindingSource.DataSource = defectList;
            _tabControl.SetDefectListBindingSource(defectsBindingSource);
        }

        private void LoadAllResultDefect()
        {
            defectList = _defectRepository.GetAllResult();
            defectsBindingSource.DataSource = defectList;
            _tabControl.SetDefectListBindingSource(defectsBindingSource);
        }

        public void ChangeTabPage(int index)
        {
            _tabControl.SelectTabPageByIndex(index);
        }
    }
}

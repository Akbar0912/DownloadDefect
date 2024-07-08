using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadData.View
{
    public interface ITabControlView
    {
        public string Search { get; set; }
        public string SearchWarranty { get; set; }
        public string SearchPacking { get; set; }
        DateTime SelectedDate { get; }
        DateTime SelectedDate2 { get; }
        DateTime SelectedDate3 { get; }

        event EventHandler SearchFilter;
        event EventHandler SearchFilter2;
        event EventHandler SearchFilter3;

        event EventHandler<object> ExportDataGridView;
        event EventHandler<object> ExportDataGridView2;
        event EventHandler<object> ExportDataGridView3;
        void SetDefectListBindingSource1(BindingSource defectList);
        void SetDefectListBindingSource2(BindingSource defectList);
        void SetDefectListBindingSource3(BindingSource defectList);
        void SelectTabPageByIndex(int data);
        DataGridView GetDataGridView1();
        DataGridView GetDataGridView2();
        DataGridView GetDataGridView3();
        void Show();
    }
}

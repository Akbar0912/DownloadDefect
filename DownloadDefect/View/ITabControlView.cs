using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadDefect.View
{
    public interface ITabControlView
    {
        public string Search { get; set; }
        DateTime SelectedDate { get; }

        event EventHandler SearchFilter;
        event EventHandler<object> ExportDataGridView;
        void SetDefectListBindingSource(BindingSource defectList);
        void SelectTabPageByIndex(int data);
        DataGridView GetDataGridView1();
        void Show();
    }
}

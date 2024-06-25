using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadDefect.View
{
    public interface IMainForm
    {
        public string Search {  get; set; }
        DateTime SelectedDate { get; }

        event EventHandler SearchFilter;
        event EventHandler<object> ExportDataGridView;
        void SetDefectListBindingSource(BindingSource defectList);
        void Show();
    }
}

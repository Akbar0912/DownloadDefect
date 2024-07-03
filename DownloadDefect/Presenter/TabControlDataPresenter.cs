using DownloadDefect._Repositories;
using DownloadDefect.Model;
using DownloadDefect.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadDefect.Presenter
{
    public class TabControlDataPresenter
    {
        public ITabControlView View { get; set; }
        public IDefectRepository _defectRepository { get; set; }
        public DefectModel _defectModel { get; }

        public TabControlDataPresenter()
        {
            View = new TabControlView();
            _defectRepository = new DefectRepository();
            _defectModel = new DefectModel();
        }
    }
}

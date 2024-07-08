using DownloadData.Model;
using DownloadData.View;
using DownloadData._Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadData.Presenter
{
    public class TabControlDataPresenter
    {
        public ITabControlView View { get; set; }
        public IDefectRepository _defectRepository { get; set; }
        public IWarrantyRepository _warrantyRepository { get; set; }
        public IPackingRepository _packingRepository { get; set; }
        //public DefectModel _defectModel { get; }

        public TabControlDataPresenter(ITabControlView view, IDefectRepository defectRepository, IWarrantyRepository warrantyRepository, IPackingRepository packingRepository)
        {
            View = view;
            _defectRepository = defectRepository;
            _warrantyRepository = warrantyRepository;
            _packingRepository = packingRepository;
            //_defectModel = model;
        }
    }
}

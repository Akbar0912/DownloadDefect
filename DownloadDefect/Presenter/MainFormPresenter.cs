﻿using DownloadData._Repositories;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using DownloadData.Model;
using DownloadData.View;

namespace DownloadData.Presenter
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

            _view.Show();
        }
    }
}

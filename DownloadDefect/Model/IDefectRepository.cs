using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadDefect.Model
{
    public interface IDefectRepository
    {
        IEnumerable<DefectModel> GetAllResult();
        IEnumerable<DefectModel> GetFilter(string defectName, DateTime selectedDate);
    }
}

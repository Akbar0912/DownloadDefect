using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadData.Model
{
    public interface IPackingRepository
    {
        IEnumerable<PackingModel> GetAll();
        public IEnumerable<PackingModel> GetFilter(DateTime selectDate);
    }
}

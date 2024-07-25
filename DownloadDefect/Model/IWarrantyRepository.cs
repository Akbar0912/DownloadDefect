using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadData.Model
{
    public interface IWarrantyRepository
    {
        IEnumerable<WarrantyModel> GetAll();
        IEnumerable<WarrantyModel> GetFilter(string search, DateTime date);
    }
}

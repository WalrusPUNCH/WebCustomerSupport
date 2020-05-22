using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomerSupport.BL.DTOs;

namespace CustomerSupport.BL.Abstract
{
    public interface IRequestManagementService : IRequestInfoService
    {
        int CreateRequest(string subject, string iniMessage);
        RequestDTO GetById(int id);
        IEnumerable<RequestDTO> GetAllWithDetails();
        void Update(RequestDTO request);
        void Delete(int id);
        int Count();
    }
}

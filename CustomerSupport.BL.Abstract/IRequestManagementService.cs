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
        void CreateRequest(RequestDTO request);
        RequestDTO GetById(int id);
    }
}

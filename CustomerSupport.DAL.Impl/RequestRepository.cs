using System;
using System.Collections.Generic;
using System.Threading;

using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Abstract;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.DAL.Impl
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        private readonly CustomerSupportContext context;
        public RequestRepository(CustomerSupportContext context) : base(context)
        {
            this.context = context;
        }

        public override void Update(Request updatedRequest)
        {
            context.Entry(updatedRequest).State = EntityState.Modified;

            /*Request requestDB = FindByID(updatedRequest.Id);
            requestDB.ApplicationDate = updatedRequest.ApplicationDate;
            requestDB.SpecialistId = updatedRequest.SpecialistId;
            requestDB.Status = updatedRequest.Status;
            requestDB.Subject = updatedRequest.Subject;*/
        }
        public override IEnumerable<Request> GetAll(int page, int pageSize)
        {
            return context.Requests.Include(rq => rq.Specialist).GetPaged(page, pageSize).ToList();
        }
        public override IEnumerable<Request> GetAll()
        {
            var requests = context.Requests.Include(r => r.Messages).ToList();
            return requests;
        }

        public override Request FindByID(int id)
        {
            return context.Requests.Include(r => r.Messages).Include(r => r.Specialist).FirstOrDefault(req => req.Id == id);        
        }

        public int GetLastId()
        {
            return 8; // context.Requests.LastOrDefault().Id;
        }
    }
}

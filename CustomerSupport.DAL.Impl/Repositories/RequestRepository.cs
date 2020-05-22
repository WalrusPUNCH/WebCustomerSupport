using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Abstract;

namespace CustomerSupport.DAL.Impl
{
    public class RequestRepository : BaseRepository<int, Request>, IRequestRepository
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
            return context.Requests.Include(rq => rq.Specialist).Include(rq => rq.Messages).GetPaged(page, pageSize).ToList();
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
    }
}

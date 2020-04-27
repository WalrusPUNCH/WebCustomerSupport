using System;
using System.Collections.Generic;
using System.Threading;

using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Abstract;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.DAL.Impl
{
    public class RequestRepository : IRequestRepository
    {
        private readonly CustomerSupportContext context;
        public RequestRepository(CustomerSupportContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            Request reqeust = GetById(id);
            if (reqeust != null)
            {
                context.Requests.Remove(reqeust);
                return true;
            }
            else
                return false;
        }

        public IEnumerable<Request> GetAll()
        {
            var requests = context.Requests.Include("Specialist").Include("Messages");
            return requests;
        }

        public Request GetById(int id)
        {
            Request request = context.Requests.Include("Specialist").Include("Messages").Where(req => req.Id == id).FirstOrDefault();
            if (request != null)
                return request;
            else
                return null;         
        }

        public void Create(Request request)
        {
            context.Requests.Add(request);
        }

        public void Update(Request request)
        {
            // WILL IT WORK ?
            context.Entry<Request>(request).State = EntityState.Modified;
            
        }
    }
}

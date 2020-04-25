using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DAL.Entities;
using DAL.Storage;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class RequestsRepository : IRequestRepository
    {
        private static volatile int maxRequestId = 0;

        public bool Delete(int id)
        {
            return StaticStorage.Requests.TryRemove(id, out var request); // what is specialst init val would be
        }

        public IEnumerable<Request> GetAll()
        {
            return StaticStorage.Requests.Values;
        }

        public Request GetById(int id)
        {
            if (StaticStorage.Requests.TryGetValue(id, out Request request) == true)
                return request;
            else
                return null;
        }

        public int Insert(Request request)
        {
            var ID = Interlocked.Increment(ref maxRequestId);
            request.Id = ID;
            StaticStorage.Requests.TryAdd(request.Id, request);
            return ID;
        }

        public void Update(Request request)
        {
            StaticStorage.Requests[request.Id] = request;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using DAL.Entities;


namespace DAL.Storage
{
    public static class StaticStorage
    {
        public static ConcurrentDictionary<int, Specialist> Specialists
                        = new ConcurrentDictionary<int, Specialist>();

        public static ConcurrentDictionary<int, Request> Requests
                        = new ConcurrentDictionary<int, Request>();
        static StaticStorage()
        {
            Specialists.TryAdd(0, new Specialist() { Id = 0, Name = "Vadym", Surname = "Mulish", NumberOfProcessedRequests=5});
        }
    }
}

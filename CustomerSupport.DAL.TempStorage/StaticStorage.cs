using System;
using System.Collections.Concurrent;

using CustomerSupport.DAL.Entities;

namespace CustomerSupport.DAL.TempStorage
{
    public static class StaticStorage
    {
        public static ConcurrentDictionary<int, Specialist> Specialists
                        = new ConcurrentDictionary<int, Specialist>();

        public static ConcurrentDictionary<int, Request> Requests
                        = new ConcurrentDictionary<int, Request>();
        static StaticStorage()
        {
            Specialists.TryAdd(0, new Specialist() { Id = 0, Name = "Vadym", Surname = "Mulish", NumberOfProcessedRequests = 5 });
        }
    }
}

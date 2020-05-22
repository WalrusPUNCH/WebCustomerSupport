using System;
using System.Collections.Generic;
using System.Text;

using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Specifications.Abstract;

namespace CustomerSupport.DAL.Specifications
{
    public class RequestsWithMessagesSpecification : BaseSpecification<Request>
    {
        public RequestsWithMessagesSpecification()
            : base(r => r.Id != -1)
        {
            AddInclude(r => r.Messages);
        }
    }
}

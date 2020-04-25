using System;

namespace CustomerSupport.BL.DTOs
{
    public enum StatusModel
    {
        Queued = 0,         // there are no avaliable specialists
        Processing = 1,     // specialist is curently working on request
        Processed = 2       // request is no more active
    }
}

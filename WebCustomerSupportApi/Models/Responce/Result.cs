using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCustomerSupportApi.Models.Responce
{
    public class Result<T>
    {
        public static readonly Result<T> InvalidData = new Result<T> { MessageType = MessageType.InvalidData, MessageText = "Invalid data was passed" };
        public MessageType MessageType { get; set; }
        public string MessageText { get; set; }
        public T Data { get; set; }
        public Result()
        {

        }
        public Result(T value)
        {
            Data = value;
        }
    }

    public enum MessageType
    {
        Ok = 200,
        Created = 201,
        NotFound = 404,
        InvalidData = 400
    }
}

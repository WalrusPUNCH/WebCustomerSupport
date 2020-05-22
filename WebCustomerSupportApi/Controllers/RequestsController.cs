using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.DTOs;
using WebCustomerSupportApi.Models;
using WebCustomerSupportApi.Mapper.Abstract;
using WebCustomerSupportApi.Models.Responce;

namespace WebCustomerSupportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestManagementService requestManagementService;
        private readonly IMapFrom<RequestDTO, RequestForUpdateModel> requestUpdateMapper;
        public RequestsController(IRequestManagementService requestManagementService,
                                   IMapFrom<RequestDTO, RequestForUpdateModel> requestUpdateMapper)
        {
            this.requestManagementService = requestManagementService;
            this.requestUpdateMapper = requestUpdateMapper;
        }

        // GET: api/Requests
        [HttpGet]
        public Result<IEnumerable<RequestDTO>> Get()
        {
            return new Result<IEnumerable<RequestDTO>>(requestManagementService.GetAllWithDetails()) { MessageType = MessageType.Ok, MessageText = "All requests with details"};
        }

        // GET: api/Requests/GetByID/5
        [HttpGet("GetByID/{id}")]
        public Result<RequestDTO> GetByID(int id)
        {
            var request = requestManagementService.GetById(id);
            if (request == null)
                return new Result<RequestDTO>() {MessageType = MessageType.NotFound, MessageText=$"Request with id {id} was not found" };
            else
                return new Result<RequestDTO>(request) { MessageType = MessageType.Ok, MessageText = "OK" };

        }

        // POST: api/Requests
        [HttpPost]
        public Result<int> Post([FromBody] RequestForAddModel newRequest)
        {
            if (newRequest == null)
                return Result<int>.InvalidData;
            int requestId = requestManagementService.CreateRequest(newRequest.Subject, newRequest.InitMessage);
            return new Result<int>(requestId) { MessageType = MessageType.Ok, MessageText = $"Request was successfully created with id {requestId}" };
           // return CreatedAtRoute(nameof(GetByID), new { id = requestId }, newRequest);
        }

        // PUT: api/Requests/5
        [HttpPatch("{id}")]
        public Result<int> Patch(int id, [FromBody] RequestForUpdateModel updatedRequest)
        {
            if (updatedRequest == null)
                return Result<int>.InvalidData;
            RequestDTO request = requestManagementService.GetById(id);
            if (request == null)
                return new Result<int>() { MessageType = MessageType.NotFound, MessageText = $"Request with id {id} was not found" };

            updatedRequest.Id = id;
            requestManagementService.Update(requestUpdateMapper.MapFrom(updatedRequest));
            return new Result<int>(id) { MessageType = MessageType.Ok, MessageText = $"Request with ID {id} was successfully updated" };
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Result<int> Delete(int id)
        {
            RequestDTO request = requestManagementService.GetById(id);
            if (request == null)
                return new Result<int>() { MessageType = MessageType.NotFound, MessageText = $"Request with id {id} was not found" };

            requestManagementService.Delete(id);
            return new Result<int>(id) { MessageType = MessageType.Ok, MessageText = $"Request with ID {id} was successfully deleted" };
        }
    }
}

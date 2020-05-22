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
using CustomerSupport.DAL.Entities;
using WebCustomerSupportApi.Models.Responce;

namespace WebCustomerSupportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private readonly ISpecialistManagementService specialistManagementService;
        private readonly IMapFrom<SpecialistDTO, SpecialistForUpdateModel> specialistUpdateMapper;
        private readonly IMapFrom<SpecialistDTO, SpecialistForAddModel> specialistAddMapper;
        public SpecialistsController(ISpecialistManagementService specialistManagementService,
                                    IMapFrom<SpecialistDTO, SpecialistForAddModel> specialistAddMapper,
                                    IMapFrom<SpecialistDTO, SpecialistForUpdateModel> specialistUpdateMapper)
        {
            this.specialistManagementService = specialistManagementService;
            this.specialistAddMapper = specialistAddMapper;
            this.specialistUpdateMapper = specialistUpdateMapper;
        }
        // GET: api/Specialists
        [HttpGet]
        public Result<IEnumerable<SpecialistDTO>> Get()
        {
            return new Result<IEnumerable<SpecialistDTO>>(specialistManagementService.GetAll()) { MessageType = MessageType.Ok, MessageText = "All specialists without details" };
        }

        // GET: api/Specialists/WithDetails
        [HttpGet("WithDetails")]
        public Result<IEnumerable<SpecialistDTO>> GetWithDetails()
        {
            return new Result<IEnumerable<SpecialistDTO>>(specialistManagementService.GetAllSpecialistsWithRequestsInformation()) 
                        { MessageType = MessageType.Ok, 
                          MessageText = "All specialists with details" 
                        };
        }

        // GET: api/Specialists/GetByID/5
        [HttpGet("GetByID/{id}", Name = "GetByID")]
        public Result<SpecialistDTO> GetByID(int id)
        {
            var specialist = specialistManagementService.GetById(id);
            if (specialist == null)
                return new Result<SpecialistDTO>(null) { MessageType = MessageType.NotFound, MessageText = $"Specialist with id {id} was not found" };
            return new Result<SpecialistDTO>(specialist) { MessageType = MessageType.Ok, MessageText ="OK" };
        }

        // POST: api/Specialists
        [HttpPost]
        public Result<int> Post([FromBody] SpecialistForAddModel specialist)
        {
            if (specialist == null)
                return Result<int>.InvalidData;
            if (!ModelState.IsValid)
            {
                string errors = string.Join(" | ", ModelState.Values
                                                                    .SelectMany(v => v.Errors)
                                                                    .Select(e => e.ErrorMessage));
                return new Result<int>() { MessageType = MessageType.InvalidData, MessageText = errors };
            }

            int specialistID = specialistManagementService.AddSpecialist(specialistAddMapper.MapFrom(specialist));
            return new Result<int>(specialistID) { MessageType = MessageType.Created, MessageText = $"Specialist was successfully created with id {specialistID}" };
           // return CreatedAtRoute(nameof(GetByID), new { id = specialistID }, specialist);
        }

        // PUT: api/Specialists/5
        [HttpPatch("{id}")]
        public Result<int> Patch(int id, [FromBody] SpecialistForUpdateModel specialistUpdateModel)
        {
            if (specialistUpdateModel == null)
                return Result<int>.InvalidData;
            SpecialistDTO foundSpecialist = specialistManagementService.GetById(id);
            if (foundSpecialist == null)
                return new Result<int>() { MessageType = MessageType.NotFound, MessageText = $"Specialist with id {id} was not found" };

            if (!ModelState.IsValid)
            {
                string errors = string.Join(" | ", ModelState.Values
                                                                    .SelectMany(v => v.Errors)
                                                                    .Select(e => e.ErrorMessage));
                return new Result<int>() { MessageType = MessageType.InvalidData, MessageText = errors };
            }

            specialistUpdateModel.Id = id;
            specialistManagementService.Update(specialistUpdateMapper.MapFrom(specialistUpdateModel));

            return new Result<int>(id) { MessageType = MessageType.Ok, MessageText = $"Specialist with ID {id} was successfully updated" };
        }

        // DELETE: api/Specialists/5
        [HttpDelete("{id}")]
        public Result<int> Delete(int id)
        {
            SpecialistDTO specialist = specialistManagementService.GetById(id);
            if (specialist == null)
                return new Result<int>() { MessageType = MessageType.NotFound, MessageText = $"Specialist with id {id} was not found" };

            specialistManagementService.Delete(id);
            return new Result<int>(id) { MessageType = MessageType.Ok, MessageText = $"Specialist with ID {id} was successfully deleted" };
        }
    }
}

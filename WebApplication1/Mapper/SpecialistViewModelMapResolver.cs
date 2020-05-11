using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mapper
{
    public class SpecialistViewModelMapResolver : IValueResolver<SpecialistDTO, SpecialistViewModel, string>
    {
        public string Resolve(SpecialistDTO source, SpecialistViewModel destination, string destMember, ResolutionContext context)
        {
            string activeRequestsInfo = string.Join("\n", source.ActiveRequests.Select(req =>
            {
                if (req.Status == StatusModel.Processed)
                    return $"[+]{req.Id}: {req.Subject}".Trim();
                else
                    return
                    $"{req.Id}: {req.Subject}".Trim();
            }));
            return activeRequestsInfo;
        }
    }
}
﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities.Custom;
using VietTravelBE.RequestHelpers;



namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiWithSpecController<TEntity, TCreateDto, TDto, TSpecParams>
    : BaseApiController<TEntity, TCreateDto, TDto>
        where TEntity : BaseEntity, new()
        where TCreateDto : class
        where TDto : class
        where TSpecParams : class

    {
        protected BaseApiWithSpecController(IGenericRepository<TEntity> repository,IUnitOfWork unitOfWork,IMapper mapper)
            : base(repository, unitOfWork, mapper)
        {
        }

        //[HttpGet("filter")]
        //public virtual async Task<ActionResult<Pagination<TDto>>> GetWidthSpec([FromQuery] TSpecParams specParams)
        //{
        //    try
        //    {
        //        var spec = CreateSpecification(specParams);
        //        var entities = await _repo.ListAsync(spec);
        //        var data = _mapper.Map<IReadOnlyList<TDto>>(entities);
        //        return Ok(new ApiResponse<IReadOnlyList<TDto>>(200, data: data));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
        //    }
        //}
    }
}

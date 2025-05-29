using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public abstract class BaseApiController<TEntity, TCreateDto, TDto> : ControllerBase
            where TEntity : BaseEntity, new ()
            where TCreateDto: class
            where TDto : class
    {
        protected readonly IGenericRepository<TEntity> _repo;
        protected readonly IUnitOfWork _unit;
        protected readonly IMapper _mapper;
        public BaseApiController(IGenericRepository<TEntity> repo, IUnitOfWork unit, IMapper mapper)
        {
            _repo = repo;
            _unit = unit;
            _mapper = mapper;
        }
        [HttpGet]
        public virtual async Task<ActionResult<ApiResponse<IReadOnlyList<TDto>>>> GetAll()
        {
            try
            {
                var entities = await _repo.ListAllAsync();
                var data = _mapper.Map<IReadOnlyList<TDto>>(entities);
                return Ok(new ApiResponse<IReadOnlyList<TDto>>(200, data: data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> GetById(int id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null)
                    return NotFound(new ApiResponse<TCreateDto>(404, "Not found"));
                var data = _mapper.Map<TCreateDto>(entity);
                return Ok(new ApiResponse<TCreateDto>(200, data: data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Create([FromBody] TCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = errors
                });
            }
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                _repo.Add(entity);
                await _unit.Complete();
                var created = _mapper.Map<TCreateDto>(entity);
                return CreatedAtAction(nameof(GetById), new { id = GetEntityId(entity) }, 
                    new ApiResponse<TCreateDto>(201, data: created));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Update(int id, [FromBody] TCreateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new ApiResponse<TDto>(404, "Not found"));
            try
            {
                _mapper.Map(dto, entity);
                await _unit.Complete();
                var updated = _mapper.Map<TDto>(entity);
                return Ok(new ApiResponse<TDto>(200, data: updated));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new ApiResponse<string>(404, "Not found"));
            try
            {
                _repo.Delete(entity);
                await _unit.Complete();
                return Ok(new ApiResponse<string>(200, message: "Deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
            }
        }

        protected virtual object? GetEntityId(TEntity entity)
        {
            var prop = typeof(TEntity).GetProperty("Id");
            return prop?.GetValue(entity);
        }

        //protected virtual ISpecification<TEntity> CreateSpecification(TourSpecParams? specParams)
        //{
        //    return specParams == null
        //        ? new BaseSpecification<TEntity>()
        //        : new Specification<TEntity>(specParams); 
        //}


    }
    
}

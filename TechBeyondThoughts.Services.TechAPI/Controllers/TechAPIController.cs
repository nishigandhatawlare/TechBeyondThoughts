using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using TechBeyondThoughts.Services.TechAPI.Data;
using TechBeyondThoughts.Services.TechAPI.Models;
using TechBeyondThoughts.Services.TechAPI.Models.Dto;
using static Azure.Core.HttpHeader;

namespace TechBeyondThoughts.Services.TechAPI.Controllers
{
    [Route("api/tech")]
    [ApiController]
    public class TechAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponceDto _responce;
        private IMapper _mapper;
		private readonly ISieveProcessor _sieveProcessor;

		public TechAPIController(AppDbContext db, ISieveProcessor sieveProcessor,  IMapper mapper) {
            _db = db;
            _mapper = mapper;
            _responce = new ResponceDto();
            _sieveProcessor = sieveProcessor;
        }
		[HttpGet]
		public IActionResult Get([FromQuery] SieveModel sieveModel)
		{
			try
			{
				// Create a queryable object
				var query = _db.Techstacks.AsQueryable();

				// Apply Sieve filtering and sorting
				var paginatedData = _sieveProcessor.Apply<TechData>(sieveModel, query);

				// Map the paginated result to IEnumerable<TechDataDto>
				var mappedResult = _mapper.Map<IEnumerable<TechDataDto>>(paginatedData);

				// Return the mapped result within the Ok response
				return Ok(mappedResult);
			}
			catch (Exception ex)
			{
				// Handle exceptions
				var errorResponse = new ResponceDto
				{
					IsSuccess = false,
					Message = ex.Message
				};

				// Return an error response
				return BadRequest(errorResponse);
			}
		}


		[HttpGet]
        [Route("id:int")]
        public ResponceDto Get(int id)
        {
            try
            {
                TechData objList = _db.Techstacks.First(u=>u.Id==id);
                _responce.Result = _mapper.Map<TechDataDto>(objList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message=ex.Message;
            }
            return _responce;

        }
        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponceDto Get(string name)
        {
            try
            {
                TechData objList = _db.Techstacks.FirstOrDefault(u => u.Title == name);
                if (objList == null)
                {
                    _responce.IsSuccess = false;
                }
                _responce.Result = _mapper.Map<TechDataDto>(objList);

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }

        [HttpPost]
        public ResponceDto Post([FromBody] TechDataDto techDataDto)
        {
            try
            {
                TechData objList = _mapper.Map<TechData>(techDataDto);
                _db.Techstacks.Add(objList);
                _db.SaveChanges();
                _responce.Result = _mapper.Map<TechDataDto>(objList);

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }

        [HttpPut]
        public ResponceDto Put([FromBody] TechDataDto techdataDto)
        {
            try
            {
                TechData objList = _mapper.Map<TechData>(techdataDto);
                _db.Techstacks.Update(objList);
                _db.SaveChanges();
                _responce.Result = _mapper.Map<TechDataDto>(objList);

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }
        [HttpDelete]
        [Route("id:int")]
        public ResponceDto Delete(int id)
        {
            try
            {
                TechData objList = _db.Techstacks.First(u => u.Id == id);
                _db.Techstacks.Remove(objList);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }
    }
}

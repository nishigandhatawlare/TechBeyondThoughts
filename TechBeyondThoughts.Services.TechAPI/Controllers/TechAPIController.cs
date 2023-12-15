using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Services;
using TechBeyondThoughts.Services.TechAPI.Data;
using TechBeyondThoughts.Services.TechAPI.Models;
using TechBeyondThoughts.Services.TechAPI.Models.Dto;

namespace TechBeyondThoughts.Services.TechAPI.Controllers
{
    [Route("api/tech")]
    [ApiController]
    [Authorize]   
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
        public ResponceDto Get()
        {
            try
            {
                IEnumerable<TechData> objList = _db.Techstacks.ToList();
                _responce.Result = _mapper.Map<IEnumerable<TechDataDto>>(objList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;

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
                List<TechData> objList = _db.Techstacks.Where(u => u.Category == name).ToList();

                if (objList == null || objList.Count == 0)
                {
                    _responce.IsSuccess = false;
                }

                _responce.Result = _mapper.Map<List<TechDataDto>>(objList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }

            return _responce;
        }


        [HttpPost]
        [Authorize(Roles ="ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Services;
using TechBeyondThoughts.MessageBus;
using TechBeyondThoughts.Services.ContactAPI.Data;
using TechBeyondThoughts.Services.ContactAPI.Models;
using TechBeyondThoughts.Services.ContactAPI.Models.Dto;

namespace TechBeyondThoughts.Services.ContactAPI.Controllers
{
    [Route("api/contact")]
    [ApiController]
    [Authorize]   
    public class ContactAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponceDto _responce;
        private IMapper _mapper;
        private IConfiguration _configuration;
        private readonly IMessageBus _messageBus;


        public ContactAPIController(AppDbContext db,  IMapper mapper, IMessageBus messageBus, IConfiguration configuration = null)
        {
            _db = db;
            _mapper = mapper;
            _responce = new ResponceDto();
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpPost("SubmitContactForm")]
        public  async Task<object> SubmitContactForm([FromBody] ContactFormModelDto contactDataDto)
        {
            try
            {
                // Publish the message to Azure Service Bus
                await _messageBus.PublishMessage(contactDataDto, _configuration.GetValue<string>("TopicAndQueueNames:EmailContact"));
                _responce.Result = true;
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.API.Extensions;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.Controllers
{
    public class UrlController : BaseApiController
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public UrlController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UrlDto>> GetUrls()
        {
            return new List<UrlDto>();
        }
    }
}
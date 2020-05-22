using AutoMapper;
using ExpenseTracker.Domain.Adapters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private INameDao _nameDao;
        private IMapper _mapper;

        public NameController(INameDao nameDao, IMapper mapper)
        {
            _nameDao = nameDao;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var names = _nameDao.GetAll().ToList();
                return Ok(names);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
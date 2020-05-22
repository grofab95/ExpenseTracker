using AutoMapper;
using ExpenseTracker.API.Dto.CostDto;
using ExpenseTracker.Common;
using ExpenseTracker.Domain.Adapters;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private ICostDao _costDao;
        private IMapper _mapper;

        public CostController(ICostDao costDao, IMapper mapper)
        {
            _costDao = costDao;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddCost(AddCostDto addCostDto)
        {
            try
            {
                if (string.IsNullOrEmpty(addCostDto.Token) || addCostDto.Token != Config.Get().Token)
                    throw new Exception("Access Denied!");

                _costDao.AddCost(_mapper.Map<Cost>(addCostDto));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fort.Models;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace Fort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {

        private ICityRepository cityRepository;
        public CityController(ICityRepository _cityRepository)
        {
            this.cityRepository = _cityRepository;
        }

        [HttpPost("AddCountry")]
        public async Task<IActionResult> AddCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                int LoginUserId = int.Parse(this.User.Claims.First(i => i.Type == "Id").Value);

                ServiceResponse<int> response = await cityRepository.AddCountry(country);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("GetCityList")]
        public IActionResult GetCityList()
        {
            int LoginUserId = int.Parse(this.User.Claims.First(i => i.Type == "Id").Value);

            try
            {
                var messages = cityRepository.GetAll(LoginUserId);
                if (messages == null)
                    return NotFound();
                return Ok(messages);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetCity")]
        public IActionResult GetCity(int? cityId)
        {
            if (cityId == null)
            {
                return BadRequest();
            }

            int LoginUserId = int.Parse(this.User.Claims.First(i => i.Type == "Id").Value);
            try
            {
                var messages = cityRepository.GetCity(LoginUserId, cityId);
                if (messages == null)
                    return NotFound();
                return Ok(messages);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddCity")]
        public async Task<IActionResult> AddCity(City city)
        {
            if (ModelState.IsValid)
            {
                int LoginUserId = int.Parse(this.User.Claims.First(i => i.Type == "Id").Value);

                ServiceResponse<int> response = await cityRepository.AddCity(LoginUserId, city);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("UpdateCity")]
        public async Task<IActionResult> UpdateCity(City city)
        {
            if (ModelState.IsValid)
            {
                int LoginUserId = int.Parse(this.User.Claims.First(i => i.Type == "Id").Value);

                ServiceResponse<int> response = await cityRepository.UpdateCity(LoginUserId, city);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("DeleteCity")]
        public async Task<IActionResult> DeleteCity(int? cityId)
        {
            if (cityId == null)
            {
                return BadRequest();
            }

            ServiceResponse<int> response = await cityRepository.DeleteCity(cityId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
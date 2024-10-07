using anisTraining.Entities.DbSet;
using anisTraining.Entities.Dtos.Requests;
using anisTraining.Entities.Dtos.Responses;
using anisTraining.Services.Repositories;
using anisTraining.Services.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anisTraining.Controllers
{

    public class AchievementsController : BaseController
    {
        public AchievementsController(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper) { }

        [HttpGet]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriverAchievements(Guid driverId)
        {
            var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId);

            if(driverAchievements == null)
                return NotFound("Achievement Not Found");

            var result = _mapper.Map<DriverAchievementResponse>(driverAchievements);

            return Ok(result);
        }


        [HttpPost("")]
        public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievement)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = _mapper.Map<Achievement>(achievement);

            await _unitOfWork.Achievements.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriverAchievements), new {driverId = result.DriverId}, result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateAchievements([FromBody] UpdateDriverAchievementRequest achievement)
        {
            if (ModelState.IsValid)
                return BadRequest();

            var result = _mapper.Map<Achievement>(achievement);

            await _unitOfWork.Achievements.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }

}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LogiTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
       
        private static readonly List<object> GpsData = new List<object>
        {
            new { TruckId = "TRK-101", Latitude = 28.6139, Longitude = 77.2090, Status = "Moving" },
            new { TruckId = "TRK-202", Latitude = 19.0760, Longitude = 72.8777, Status = "Idle" }
        };

        [Authorize(Roles = "Manager")]
        [HttpGet("gps")]
        public IActionResult GetGpsData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var email = User.FindFirst(ClaimTypes.Email)?.Value
                        ?? User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

            var name = User.FindFirst("name")?.Value;
            var department = User.FindFirst("department")?.Value;

            var permissions = User.FindAll("permission").Select(p => p.Value).ToList();

            if (!permissions.Contains("view_gps"))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "You do not have permission to view GPS data." });
            }

            var response = new
            {
                message = "GPS data fetched successfully",
                requestedBy = new
                {
                    userId,
                    name,
                    email,
                    department
                },
                totalRecords = GpsData.Count,
                data = GpsData,
                timestamp = DateTime.Now
            };

            return Ok(response);
        }
    }
}
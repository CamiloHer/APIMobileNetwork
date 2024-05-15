using ApiMobileNetwork.Entities;
using ApiMobileNetwork.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiMobileNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private readonly INetworkService _networkService;
        public NetworkController(INetworkService networkService)
        {
            _networkService = networkService;
        } 
        // GET: api/<NetworkController>
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable>> GetSignalData()
        {
            return await _networkService.GetSignals();
        }

        // GET api/<NetworkController>/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> GetSignalById(string id)
        {
            var response = await _networkService.GetSignalData(new Guid(id));
            if (response == null) return NotFound();
            return Ok(response);
        }

        // POST api/<NetworkController>
        [HttpPost("[action]")]
        public async Task<ActionResult> SaveSignal([FromBody] SignalData signal)
        {
            var response = await _networkService.SaveSignalData(signal);
            if (response == false) return BadRequest(response);
            return Ok(response);

        }

    }
}

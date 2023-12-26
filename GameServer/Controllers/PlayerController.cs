using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;
        private readonly GameDbContext _gameDbContext;

        public PlayerController(PlayerService playerService, GameDbContext gameDbContext)
        {
            _playerService = playerService;
            _gameDbContext = gameDbContext;
        }

        [HttpGet("{id}/{name}")]
        public Player Get([FromRoute] int id, [FromRoute] string name)
        {
            _playerService.DoSmth();
            return new Player() { Id = id,Name = name };
        }

        [HttpPost]

        public Player Post(Player player)
        {

            return player;
        }
    }
}

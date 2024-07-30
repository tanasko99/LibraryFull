using Microsoft.AspNetCore.Mvc;

namespace FullLibrary.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        public readonly ILogger<T> _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        public long CurrentUserId
        {
            get
            {
                return long.Parse(User.Claims.First(x => x.Type.Contains("name")).Value);
            }
        }
    }
}

using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    [Component]
    public class DefaultOrderService : IOrderService
    {
        private readonly IUserService userService;

        public DefaultOrderService(IUserService userService)
        {
            this.userService = userService;
        }

        [Logger]
        public void Create()
        {
            userService.Register("a");   
        }
    }
}

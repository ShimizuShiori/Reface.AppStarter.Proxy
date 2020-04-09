using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    [Component]
    public class DefaultUserService : IUserService
    {
        [Logger]
        public void Another()
        {
            
        }

        [Logger]
        public User Register(string name)
        {
            Debug.WriteLine($"This.GetType() = {this.GetType().FullName}");
            User user = new User() { Name = name };
            this.Another();
            return user;
        }
    }
}

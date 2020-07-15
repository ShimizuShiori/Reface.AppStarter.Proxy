using System;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CleanCache : Attribute
    {
        public string Key { get; private set; }

        public CleanCache(string key)
        {
            Key = key;
        }
    }
}

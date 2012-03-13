using System;

namespace ReleaseManager.Jira
{
    public interface IJiraConfig
    {
        string UserName { get; }
        string Password { get; }
        TimeSpan CacheExpireTime { get; }
        Uri BaseUri { get; }
        IStatusMap StatusMap { get; }
    }
}
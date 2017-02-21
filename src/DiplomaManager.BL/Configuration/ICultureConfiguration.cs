using System.Collections.Generic;

namespace DiplomaManager.BLL.Configuration
{
    public interface ILocaleConfiguration
    {
        IEnumerable<string> LocaleNames { get; set; }
        string DefaultLocaleName { get; set; }
    }
}

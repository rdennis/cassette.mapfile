using System;
using System.IO;
using System.Linq;
using Cassette;

namespace Albatross.Cassette.MapFile
{
    public class MapFileAccessConfigurer : IConfiguration<IFileAccessAuthorization>
    {
        public void Configure(IFileAccessAuthorization configurable)
        {
            configurable.AllowAccess(path => {
                var extension = Path.GetExtension(path);
                return extension.Equals(".map", StringComparison.InvariantCultureIgnoreCase);
            });
        }
    }
}
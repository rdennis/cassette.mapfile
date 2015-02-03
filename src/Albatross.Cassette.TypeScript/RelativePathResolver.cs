using System;
using System.Linq;
using System.Web;

namespace Albatross.Cassette.TypeScript
{
    public class RelativePathResolver : IRelativePathResolver
    {
        public string ToAbsolute(string relativePath)
        {
            return VirtualPathUtility.ToAbsolute(relativePath);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Cassette;

namespace Albatross.Cassette.TypeScript
{
    public class MapFileRewriter : IMapFileRewriter
    {
        public static readonly Regex MapFileReplacement = new Regex(@"^(\s*//# sourceMappingURL=)(.+\.map)\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private readonly IRelativePathResolver pathResolver;
        
        public MapFileRewriter(IRelativePathResolver resolver)
        {
            this.pathResolver = resolver;
        }

        public CompileResult Compile(string source, CompileContext context)
        {
            var test = this.pathResolver.ToAbsolute(context.SourceFilePath);

            var dirRegex = new Regex(@"([^/\\]+[/\\])");

            var sourcePath = context.SourceFilePath;
            var bundleEscape = dirRegex
                                       .Replace(string.Format("cassettePlaceholder/{0}", sourcePath), "../")
                                       .Replace(Path.GetFileName(sourcePath), String.Empty);

            var bundleRelativeDirectory = sourcePath
                                                    .Replace("~/", bundleEscape)
                                                    .Replace(Path.GetFileName(sourcePath), String.Empty);

            var result = MapFileReplacement.Replace(source, String.Format("$1{0}$2", bundleRelativeDirectory));

            return new CompileResult(result, new List<string>());
        }
    }
}
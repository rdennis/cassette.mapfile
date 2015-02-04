using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Cassette;

namespace Albatross.Cassette.MapFile
{
    public class MapFileRewriter : IMapFileRewriter
    {
        private static readonly Regex directoryReplacement = new Regex(@"([^/\\]+[/\\])");

        private static readonly Regex mapFileReplacement = new Regex(@"^(\s*//# sourceMappingURL=)(.+\.map)\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private readonly CassetteSettings settings;

        public MapFileRewriter(CassetteSettings settings)
        {
            this.settings = settings;
        }

        public CompileResult Compile(string source, CompileContext context)
        {
            if(this.settings != null && this.settings.IsDebuggingEnabled)
            {
                return new CompileResult(source, Enumerable.Empty<string>());
            }

            string sourcePath = context.SourceFilePath;
            string bundleEscape = directoryReplacement
                                                      .Replace(string.Format("cassettePlaceholder/{0}", sourcePath), "../")
                                                      .Replace(Path.GetFileName(sourcePath), String.Empty);

            string bundleRelativeDirectory = sourcePath
                                                       .Replace("~/", bundleEscape)
                                                       .Replace(Path.GetFileName(sourcePath), String.Empty);

            string result = mapFileReplacement.Replace(source, String.Format("$1{0}$2", bundleRelativeDirectory));

            return new CompileResult(result, Enumerable.Empty<string>());
        }
    }
}
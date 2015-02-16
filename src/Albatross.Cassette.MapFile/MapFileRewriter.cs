using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Cassette;

namespace Albatross.Cassette.MapFile
{
    public class MapFileRewriter : IMapFileRewriter
    {
        private static readonly Regex sourceMapReplacement = new Regex(@"^(/[/|\*]# sourceMappingURL=)(.+\.map)\s*( \*/)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private readonly CassetteSettings settings;

        public MapFileRewriter(CassetteSettings settings)
        {
            this.settings = settings;
        }

        public CompileResult Compile(string source, CompileContext context)
        {
            if(this.settings != null && !this.settings.IsDebuggingEnabled)
            {
                return new CompileResult(source, Enumerable.Empty<string>());
            }

            var relativePath = this.GetRawDirectoryRelativePath(context.SourceFilePath);
            var result = sourceMapReplacement.Replace(source, String.Format("$1{0}$2$3", relativePath));

            return new CompileResult(result, Enumerable.Empty<string>());
        }

        private string GetRawDirectoryRelativePath(string sourcePath)
        {
            sourcePath = sourcePath
                                   .Replace('\\', '/')
                                   .Replace("~/", "../");

            if(sourcePath[0] == '/')
            {
                sourcePath = sourcePath.Substring(1);
            }

            for(var depth = sourcePath.Count(c => c == '/'); depth > 0; depth--)
            {
                sourcePath = sourcePath.Insert(0, "../");
            }

            var directoryPath = Path.GetDirectoryName(sourcePath).Replace('\\', '/');
            return string.Format("../file/{0}/", directoryPath); // make RawFileRequestRewriter see this as a raw file
        }
    }
}
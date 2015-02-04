using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Cassette;

namespace Albatross.Cassette.MapFile
{
   public class MapFileRewriter : IMapFileRewriter
   {
      public static readonly Regex MapFileReplacement = new Regex(@"^(\s*//# sourceMappingURL=)(.+\.map)\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
      public static readonly Regex DirectoryReplacement = new Regex(@"([^/\\]+[/\\])");

      public CompileResult Compile(string source, CompileContext context)
      {
         string sourcePath = context.SourceFilePath;
         string bundleEscape = DirectoryReplacement
                                                   .Replace(string.Format("cassettePlaceholder/{0}", sourcePath), "../")
                                                   .Replace(Path.GetFileName(sourcePath), String.Empty);

         string bundleRelativeDirectory = sourcePath
                                                    .Replace("~/", bundleEscape)
                                                    .Replace(Path.GetFileName(sourcePath), String.Empty);

         string result = MapFileReplacement.Replace(source, String.Format("$1{0}$2", bundleRelativeDirectory));

         return new CompileResult(result, new List<string>());
      }
   }
}
﻿using System;
using System.Linq;
using Cassette;
using Cassette.IO;
using Moq;
using Xbehave;
using Shouldly;

namespace Albatross.Cassette.MapFile.Test
{
    public class MapFileRewriter
    {
        [Scenario]
        [Example("some-file.js.map", "~/content/scripts/some-file.js", "../file/../../../../content/scripts/some-file.js.map")]
        [Example("some-file.js.map", "~/content/some-file.js", "../file/../../../content/some-file.js.map")]
        [Example("some-file.js.map", "~/some-file.js", "../file/../../some-file.js.map")]
        public void CompileRewritesRelativePath(string mapfileName, string relativePath, string expectedUrl, Mock<IFile> file, Mock<IDirectory> directory, CompileContext compileContext, string code, CompileResult result, CassetteSettings settings, MapFile.MapFileRewriter rewriter)
        {
            "Given some code with a mapFile reference {0}".
            f(() => {
                code = string.Format("{0}{1}", @"
$(document).ready(function($) {
});
//# sourceMappingURL=", mapfileName);
            });

            "In a given location {1}".
            f(() => {
                file = new Mock<IFile>();
                directory = new Mock<IDirectory>();

                file.SetupGet(f => f.FullPath).Returns(relativePath);
                file.SetupGet(f => f.Directory).Returns(directory.Object);
                directory.Setup(d => d.GetDirectory(It.IsAny<string>())).Returns(directory.Object);
                directory.Setup(d => d.GetFile("some-file.js")).Returns(file.Object);
            });

            "And given a MapFileRewriter with a virtual directory {2}".
            f(() => {
               settings = new CassetteSettings(Enumerable.Empty<IConfiguration<CassetteSettings>>())
               {
                  IsDebuggingEnabled = true
               };
                rewriter = new MapFile.MapFileRewriter(settings);
            });

            "And given a compile context".
            f(() => {
                compileContext = new CompileContext {
                    RootDirectory = directory.Object,
                    SourceFilePath = relativePath
                };
            });

            "When the file is compiled".
            f(() => {
                result = rewriter.Compile(code, compileContext);
            });

            "The sourcemap reference should be {3}".
            f(() => {
                result.Output.ShouldContain(string.Format("//# sourceMappingURL={0}", expectedUrl));
            });
        }
    }
}
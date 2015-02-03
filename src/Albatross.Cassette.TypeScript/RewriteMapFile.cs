using System;
using System.Linq;
using Cassette;
using Cassette.BundleProcessing;
using Cassette.Scripts;

namespace Albatross.Cassette.TypeScript
{
    public class RewriteMapFile : IBundleProcessor<ScriptBundle>
    {
        private readonly IMapFileRewriter mapFileRewriter;
        private readonly CassetteSettings settigns;

        public RewriteMapFile(IMapFileRewriter mapFileRewriter, CassetteSettings settigns)
        {
            this.mapFileRewriter = mapFileRewriter;
            this.settigns = settigns;
        }

        public void Process(ScriptBundle bundle)
        {
            foreach(var asset in bundle.Assets)
            {
                asset.AddAssetTransformer(new CompileAsset(this.mapFileRewriter, this.settigns.SourceDirectory));
            }
        }
    }
}
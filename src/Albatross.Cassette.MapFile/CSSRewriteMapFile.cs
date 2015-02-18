using System;
using System.Linq;
using Cassette;
using Cassette.BundleProcessing;
using Cassette.Stylesheets;

namespace Albatross.Cassette.MapFile
{
    public class CSSRewriteMapFile : IBundleProcessor<StylesheetBundle>
    {
        private readonly IMapFileRewriter mapFileRewriter;
        private readonly CassetteSettings settigns;

        public CSSRewriteMapFile(IMapFileRewriter mapFileRewriter, CassetteSettings settigns)
        {
            this.mapFileRewriter = mapFileRewriter;
            this.settigns = settigns;
        }

        public void Process(StylesheetBundle bundle)
        {
            foreach(var asset in bundle.Assets)
            {
                asset.AddAssetTransformer(new CompileAsset(this.mapFileRewriter, this.settigns.SourceDirectory));
            }
        }
    }
}
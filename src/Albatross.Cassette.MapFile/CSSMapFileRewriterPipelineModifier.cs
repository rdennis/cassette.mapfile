using Cassette.BundleProcessing;
using Cassette.Stylesheets;

namespace Albatross.Cassette.MapFile
{
    public class CSSMapFileRewriterPipelineModifier : IBundlePipelineModifier<StylesheetBundle>
    {
        public IBundlePipeline<StylesheetBundle> Modify(IBundlePipeline<StylesheetBundle> pipeline)
        {
            var index = pipeline.IndexOf<ParseCssReferences>();
            pipeline.Insert<CSSRewriteMapFile>(index + 1);

            return pipeline;
        }
    }
}
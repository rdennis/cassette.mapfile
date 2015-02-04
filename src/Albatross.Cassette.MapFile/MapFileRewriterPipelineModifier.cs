using Cassette.BundleProcessing;
using Cassette.Scripts;

namespace Albatross.Cassette.MapFile
{
    public class MapFileRewriterPipelineModifier : IBundlePipelineModifier<ScriptBundle>
    {
        public IBundlePipeline<ScriptBundle> Modify(IBundlePipeline<ScriptBundle> pipeline)
        {
            var index = pipeline.IndexOf<ParseJavaScriptReferences>();
            pipeline.Insert<RewriteMapFile>(index + 1);

            return pipeline;
        }
    }
}
using System;
using System.Linq;
using Cassette;
using Cassette.TinyIoC;

namespace Albatross.Cassette.MapFile
{
   public class MapFileRewriterConfigurer : IConfiguration<TinyIoCContainer>
   {
      [ConfigurationOrder(20)]
      public void Configure(TinyIoCContainer container)
      {
         container.Register<IMapFileRewriter>((c, p) => new MapFileRewriter());
      }
   }
}
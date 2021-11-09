using UmbraCare.ProtectUsedMedia.Components;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace UmbraCare.ProtectUsedMedia.Composers
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class ProtectUsedMediaComponentComposer : ComponentComposer<ProtectUsedMediaComponent>
    {
    }
}

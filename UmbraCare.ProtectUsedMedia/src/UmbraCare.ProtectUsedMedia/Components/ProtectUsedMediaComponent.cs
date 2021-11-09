using System;
using UmbraCare.Core.Extensions.UmbracoExtensions;
using UmbraCare.ProtectUsedMedia.Helpers;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace UmbraCare.ProtectUsedMedia.Components
{
    public class ProtectUsedMediaComponent : IComponent
    {
        readonly ILogger _logger;
        readonly IRelationService _relationService;

        public ProtectUsedMediaComponent(ILogger logger, IRelationService relationService)
        {
            _logger = logger;
            _relationService = relationService;
        }

        public void Initialize()
        {
            MediaService.Trashing += MediaService_Trashing;
        }

        public void Terminate()
        {
            MediaService.Trashing -= MediaService_Trashing;
        }

        private void MediaService_Trashing(IMediaService sender, MoveEventArgs<IMedia> e)
        {
            try
            {
                var helper = new ProtectUsedMediaHelper();

                foreach (MoveEventInfo<IMedia> moveInfo in e.MoveInfoCollection)
                {
                    if (!helper.IsPossibleToTrash(moveInfo.Entity, _relationService))
                    {
                        e.Cancel = true;

                        e.Messages.AddError("Trashing canceled", "At least one media is associated with existing content. Please remove the content or reference.");

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error<ProtectUsedMediaComponent>(ex, "Filed to protect media.");
            }
        }
    }
}
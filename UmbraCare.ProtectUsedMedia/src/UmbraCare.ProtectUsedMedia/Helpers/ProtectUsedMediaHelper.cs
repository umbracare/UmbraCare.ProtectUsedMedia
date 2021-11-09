using System;
using UmbraCare.Core.Helpers.UmbracoHelpers;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace UmbraCare.ProtectUsedMedia.Helpers
{
    internal class ProtectUsedMediaHelper
    {
        readonly IMediaService _mediaService;

        public ProtectUsedMediaHelper(ServiceContext serviceContext)
        {
            _mediaService = serviceContext.MediaService;
        }

        public ProtectUsedMediaHelper()
        {
            _mediaService = UmbracoServicesHelper.Media;
        }

        public bool IsPossibleToTrash(IMedia entity, IRelationService relationService)
        {
            var helper = new UmbracoRelatedMediaTrashingHelper(Constants.Conventions.RelationTypes.RelatedMediaAlias, _mediaService, relationService);

            return helper.IsPossibleToTrash(entity, true, null);
        }
    }
}

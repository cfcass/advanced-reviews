﻿using System.Web.Routing;
using AdvancedExternalReviews.ReviewLinksRepository;
using EPiServer;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Routing;

namespace AdvancedExternalReviews
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class PreviewRouterInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var locator = context.Locate.Advanced;
            var editRouter = new PageEditPartialRouter
            (
                locator.GetInstance<IContentLoader>(),
                locator.GetInstance<IExternalReviewLinksRepository>()
            );
            RouteTable.Routes.RegisterPartialRouter(editRouter);

            var previewRouter = new PagePreviewPartialRouter
            (
                locator.GetInstance<IContentLoader>(),
                locator.GetInstance<IExternalReviewLinksRepository>(),
                locator.GetInstance<ExternalReviewOptions>()
            );
            RouteTable.Routes.RegisterPartialRouter(previewRouter);

        }

        void IInitializableModule.Uninitialize(InitializationEngine context) { }
    }
}
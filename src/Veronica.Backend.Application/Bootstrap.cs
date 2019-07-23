using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Veronica.Backend.Application.Infrastructure;
using Veronica.Backend.Domain;
using Veronica.Backend.Domain.EF;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Application
{
    public class Bootstrap : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterDataLayer(builder);
            RegisterMediatR(builder);

            builder.RegisterType<SystemClock>()
                .As<ISystemClock>()
                .SingleInstance();
        }

        private void RegisterDataLayer(ContainerBuilder builder)
        {
            builder.Register(c => 
                {
                    var config = c.Resolve<IConfiguration>();
                    var connectionString = config["ConnectionStrings:veronica"];

                    return new DbContextOptionsBuilder<VeronicaDbContext>()
                        .UseSqlServer(connectionString, o => o.EnableRetryOnFailure())
                        .UseLoggerFactory(c.Resolve<ILoggerFactory>())
                        .Options;
                })
                .As<DbContextOptions<VeronicaDbContext>>()
                .SingleInstance();

            builder.Register(c => new VeronicaDbContext(c.Resolve<DbContextOptions<VeronicaDbContext>>()).AsQueryContext())
                .As<IQueryContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VeronicaDbContext>()
                .AsSelf()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            // Register all data store and data reader types
            builder.RegisterAssemblyTypes(typeof(Domain.EF.Repositories.Users.UserStore).GetTypeInfo().Assembly)
                .Where(x => x.Namespace.Contains("Repositories"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void RegisterMediatR(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IAsyncRequestHandler<,>),
                typeof(ICancellableAsyncRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IAsyncNotificationHandler<>),
                typeof(ICancellableAsyncNotificationHandler<>)
            };

            // Find command and query handlers in this assembly and register them
            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(ThisAssembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(CommandPipeline<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(QueryPipeline<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerLifetimeScope();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });
        }
    }
}
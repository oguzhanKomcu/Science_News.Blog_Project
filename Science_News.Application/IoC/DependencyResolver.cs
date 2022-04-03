using Autofac;
using AutoMapper;
using Science_News.Application.AutoMapper;
using Science_News.Application.Services.CategoryService;
using Science_News.Domain.Repositories;
using Science_News.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.IoC
{
    public class DependencyResolver : Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            //bunları burada register ediyoruz 
            builder.RegisterType<CategoryRepo>().As<ICategoryRepo>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepo>().As<IAuthorRepo>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepo>().As<IAppUserRepo>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepo>().As<IPostRepo>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            //builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            //builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            //builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();


            builder.Register(context => new MapperConfiguration(cfg =>
            {

                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();


            base.Load(builder);
        }




    }
}

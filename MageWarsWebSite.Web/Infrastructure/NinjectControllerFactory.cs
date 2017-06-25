using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Concrete;

namespace MageWarsWebSite.Web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _nKernel;
        public NinjectControllerFactory() : base()
        {
            _nKernel = new StandardKernel();
            ApplyBindings();
        }

        private void ApplyBindings()
        {
            _nKernel.Bind<IBaseRepository>().To<BaseRepository>();

            _nKernel.Bind<ICardRepository>().To<CardRepository>();
            _nKernel.Bind<IDeskRepository>().To<DeskRepository>();
            _nKernel.Bind<ISchoolRepository>().To<SchoolRepository>();
            _nKernel.Bind<ISubTypeRepository>().To<SubTypeRepository>();
            _nKernel.Bind<ICardTypeRepository>().To<CardTypeRepository>();
            _nKernel.Bind<IMageRepository>().To<MageRepository>();
            _nKernel.Bind<IGameRepository>().To<GameRepository>();
            _nKernel.Bind<IUserRepository>().To<UserRepository>();

            _nKernel.Bind<IRepository>().To<Repository>();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)_nKernel.Get(controllerType);
        }
    }
}
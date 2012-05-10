using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Chirpy.Domain.Repository;
using Chirpy.Controllers;

namespace Chirpy
{
    
    public class ChirpControllerFactory : DefaultControllerFactory
    {
        private readonly Dictionary<string, Func<RequestContext, IController>> _controllerMap;

        public ChirpControllerFactory(IChirpRepository chirpRepository)
        {
            if (chirpRepository == null)
            {
                throw new ArgumentNullException("documentStore");
            }
            this._controllerMap = new Dictionary<string, Func<RequestContext, IController>>();
            this._controllerMap["Home"] = context => new HomeController(chirpRepository);
            this._controllerMap["Administration"] = context => new AdministrationController(chirpRepository);
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (this._controllerMap.ContainsKey(controllerName))
            {
                return this._controllerMap[controllerName](requestContext);
            }
            else
            {
                return null;
            }
        }

        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        }
    }
}
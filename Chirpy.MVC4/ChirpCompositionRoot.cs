using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Chirpy.Domain.Repository;
//using Raven.Client.Embedded;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Chirpy
{

    public class ChirpCompositionRoot
    {
        private readonly IControllerFactory _controllerFactory;

        public ChirpCompositionRoot()
        {
            this._controllerFactory = ChirpCompositionRoot.CreateControllerFactory();
        }

        public IControllerFactory ControllerFactory
        {
            get
            {
                return _controllerFactory;
            }
        }

        private static IControllerFactory CreateControllerFactory()
        {
            string assemblyName = ConfigurationManager.AppSettings["chirpRepositoryAssemblyName"];
            string typeName = ConfigurationManager.AppSettings["chirpRepositoryTypeName"];
            string databaseName = ConfigurationManager.AppSettings["databaseName"];
            DocumentStore documentStore = new DocumentStore();
            documentStore.ConnectionStringName = "RavenDB";
            documentStore.Initialize();
            documentStore.DatabaseCommands.EnsureDatabaseExists(databaseName);
            var cacheRepository = Activator.CreateInstance(Type.GetType(typeName, true, true), new object[] { 
                documentStore, databaseName 
            });
            var controllerFactory = new ChirpControllerFactory((IChirpRepository)cacheRepository);
            return controllerFactory;
        }

    }
}
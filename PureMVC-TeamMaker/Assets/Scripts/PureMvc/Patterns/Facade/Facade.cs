

using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PureMVC
{



    public class Facade : IFacade
    {
        /// <summary>References to Controller</summary>
        protected IController controller;
        /// <summary>Reference to Model</summary>
        protected IModel model;
        /// <summary>References to View</summary>
        protected IView view;

        protected static IFacade instance;

        protected const string SingletonMsg = "Facade Singleton already constructed!";

        public Facade()
        {

            if (instance != null) throw new Exception(SingletonMsg);
            instance = this;
            InitializeFacade();
        }

        protected virtual void InitializeFacade()
        {
            InitializeModel();
            InitializeController();
            InitializeView();
        }

        public static IFacade GetInstance(Func<IFacade> facadeFunc)
        {
            if (instance == null)
            {
                instance = facadeFunc();
            }
            return instance;
        }

        protected virtual void InitializeController()
        {
            controller = Controller.GetInstance(() => new Controller());
        }

        protected virtual void InitializeModel()
        {
            model = Model.GetInstance(() => new Model());
        }

 
        protected virtual void InitializeView()
        {
            view = View.GetInstance(() => new View());
        }

        public virtual void RegisterCommand(string notificationName, Func<ICommand> factory)
        {
            controller.RegisterCommand(notificationName, factory);
        }


        public virtual void RemoveCommand(string notificationName)
        {
            controller.RemoveCommand(notificationName);
        }

        public virtual bool HasCommand(string notificationName)
        {
            return controller.HasCommand(notificationName);
        }

        public virtual void RegisterProxy(IProxy proxy)
        {
            model.RegisterProxy(proxy);
        }

        public virtual IProxy RetrieveProxy(string proxyName)
        {
            return model.RetrieveProxy(proxyName);
        }

        public virtual IProxy RemoveProxy(string proxyName)
        {
            return model.RemoveProxy(proxyName);
        }

        public virtual bool HasProxy(string proxyName)
        {
            return model.HasProxy(proxyName);
        }

        public virtual void RegisterMediator(IMediator mediator)
        {
            view.RegisterMediator(mediator);
        }

        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            return view.RetrieveMediator(mediatorName);
        }

        public virtual IMediator RemoveMediator(string mediatorName)
        {
            return view.RemoveMediator(mediatorName);
        }

        public virtual bool HasMediator(string mediatorName)
        {
            return view.HasMediator(mediatorName);
        }

        public virtual void SendNotification(string notificationName, object body = null, string type = null)
        {
            NotifyObservers(new Notification(notificationName, body, type));
        }

      
        public virtual void NotifyObservers(INotification notification)
        {
            view.NotifyObservers(notification);
        }

       
    }
}

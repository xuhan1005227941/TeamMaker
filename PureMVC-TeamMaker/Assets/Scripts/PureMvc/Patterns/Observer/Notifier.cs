


namespace PureMVC
{
  
    public class Notifier : INotifier
    {
        protected IFacade mFacade
        {
            get
            {
                return Facade.GetInstance(() => new Facade());
            }
        }

        public virtual void SendNotification(string notificationName, object body = null, string type = null)
        {
 
            mFacade.SendNotification(notificationName, body, type);
        }


      
    }
}

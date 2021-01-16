using Google.Protobuf;
using PureMVC;
using PureMVC.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UISelectWorkerCommand:MacroCommand
{
    protected override void InitializeMacroCommand()
    {
        AddSubCommand(delegate { return new UISelectWorkerRequestCommand(); });
        AddSubCommand(delegate { return new UISelectWorkerResponseCommand(); });
    }

}

public class UISelectWorkerRequestCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)Enum.Parse(typeof(CmdType), notification.Type);
        if (type == CmdType.Request)
        {
            Message message = notification.Body as Message;
            Notify notify = new Notify();
            notify.Protocol = mainproto;
            switch (mainproto)
            {
                case Protocol.Friend_Check:
                    Friend_CheckReq Req = ReferencePool.Require<Friend_CheckReq>();
                    Req.Uid = int.Parse(message.args[0]);
                    notify.message = Req.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;

            }
        }
    }
}

public class UISelectWorkerResponseCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)Enum.Parse(typeof(CmdType), notification.Type);
        Notify notify = notification.Body as Notify;
        if (type == CmdType.Response)
        {
            switch (mainproto)
            {
                case Protocol.Friend_Check:
                    GameFacade.RetrieveProxy<UISelectWorkerProxy>().HandleProject_Check(notify);
                    break;

            }
        }
    }
}




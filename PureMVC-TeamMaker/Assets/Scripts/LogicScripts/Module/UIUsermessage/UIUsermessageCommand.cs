using Google.Protobuf;
using PureMVC;
using PureMVC.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UIUsermessageCommand : MacroCommand
{
    protected override void InitializeMacroCommand()
    {
        AddSubCommand(delegate { return new UIUsermessageRequestCommand(); });
        AddSubCommand(delegate { return new UIUsermessageResponseCommand(); });
    }
}
public class UIUsermessageRequestCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);
        Message message = notification.Body as Message;
        Notify notify = new Notify();
        notify.Protocol = mainproto;
        if (type == CmdType.Request)
        {
            switch (mainproto)
            {
                case Protocol.UserMessage_Update:
                    UserMessage_UpdateReq userUpdate = ReferencePool.Require<UserMessage_UpdateReq>();
                    userUpdate.Uid = int.Parse(message.args[0]);
                    userUpdate.UserName = message.args[1].ToString();
                    userUpdate.UserAge = int.Parse(message.args[2]);
                    userUpdate.UserProfession = message.args[3].ToString();
                    userUpdate.UserScore = int.Parse(message.args[4]);
                    notify.message = userUpdate.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
            }
        }
    }
}

public class UIUsermessageResponseCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);
        Notify notify = notification.Body as Notify;
        if (type == CmdType.Response)
        {
            switch (mainproto)
            {
                case Protocol.UserMessage_Update:
                    GameFacade.RetrieveProxy<PlayerDataProxy>().UpdateUser(notify);
                    break;
            }
        }
    }

}


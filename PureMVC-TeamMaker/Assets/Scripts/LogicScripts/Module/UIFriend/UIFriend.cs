
public class UIFriend : IUI
{
    public void OnCreate()
    {
        GameFacade.RegisterCommand<UIFriendCommand>(Protocol.Friend_Check,
            Protocol.Friend_Add,Protocol.Friend_Delete,Protocol.UserMessage_Look,
            Protocol.FriendApply_Check,Protocol.FriendApply_Agree,
            Protocol.FriendApply_Disagree,Protocol.BlackList_Add,
            Protocol.BlackList_Check,Protocol.BlackList_Delete);
        GameFacade.RegisterProxy<UIFriendProxy>();
        GameFacade.RegisterMediator<UIFriendMediator>();
    }

    public void OnClose()
    {
        GameFacade.RemoveCommand(Protocol.Friend_Check,
            Protocol.Friend_Add, Protocol.Friend_Delete,Protocol.UserMessage_Look,
            Protocol.FriendApply_Check, Protocol.FriendApply_Agree,
            Protocol.FriendApply_Disagree, Protocol.BlackList_Add,
            Protocol.BlackList_Check, Protocol.BlackList_Delete);
        GameFacade.RemoveProxy<UIFriendProxy>();
        GameFacade.RemoveMediator<UIFriendMediator>();
    }

    public void OnFrozen(bool isF)
    {
        if (isF)
        {

        }
        else
        {

        }
    }


}

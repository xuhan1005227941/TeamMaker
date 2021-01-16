using Google.Protobuf;
/// <summary>
/// 网络消息结构
/// </summary>
public class Notify
{
    public int Protocol;
    public int Feedback;
    public byte[] message;
}

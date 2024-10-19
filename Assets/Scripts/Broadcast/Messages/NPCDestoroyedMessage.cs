public class NPCDestoroyedMessage : IMessage
{
    public string Message { get; private set; }

    public NPCDestoroyedMessage(string message)
    {
        Message = message;
    }
}

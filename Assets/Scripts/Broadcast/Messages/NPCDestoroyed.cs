public class NPCDestoroyed : IMessage
{
    public string Message { get; private set; }

    public NPCDestoroyed(string message)
    {
        Message = message;
    }
}

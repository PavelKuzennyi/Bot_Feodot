using Telegram.Bot.Types;

namespace Bot_Feodot;

public class Messages
{
    public string name { get; set; }
    public string type { get; set; }
    public int id { get; set; }
    public CustomMessage[] messages { get; set; }
}
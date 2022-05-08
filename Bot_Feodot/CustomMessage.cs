namespace Bot_Feodot;

public class CustomMessage
{
    public string from_id { get; set; }
    
    public string media_type { get; set; }

    public string file { get; set; }
    
    private string _text;
    public object text
    {
        get => _text;
        set
        {
            if (!value.ToString()!.Contains('['))
            {
                _text = value.ToString()!;
            }
            else
            {
                _text = "";
            }
        }
    }
}
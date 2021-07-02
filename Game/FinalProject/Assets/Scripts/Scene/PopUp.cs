using System;

[System.Serializable]
public class PopUp 
{
    public String Title;
    public String Message;

    public PopUp()
    {
        Title = "Title";
        Message = "Message text";
    }

    public PopUp(string title, string message)
    {
        Title = title;
        Message = message;
    }
}
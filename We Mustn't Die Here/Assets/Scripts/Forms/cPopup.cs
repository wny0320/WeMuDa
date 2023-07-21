using UnityEngine.Events;
public class cPopup
{
    public cPopup(string _title, string _value, UnityAction _action)
    {
        Title = _title;
        Value = _value;
        Action = _action;
    }

    public string Title;
    public string Value;
    public UnityAction Action;
}

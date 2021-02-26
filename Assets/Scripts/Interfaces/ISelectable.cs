using UnityEngine;

public interface ISelectable
{
    bool Hovering { get; set; }
    void Hover(bool selected);
    void Clicked();
}

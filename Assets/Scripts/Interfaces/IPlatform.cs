using UnityEngine;

public interface IPlatform
{
    Vector2[] Points { get; set; }
    int Size { get; set; }
    void PlacePlatform();
}

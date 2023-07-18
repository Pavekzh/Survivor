using UnityEngine;

public interface IPooledWaveObject
{
    bool InPool { get; set; }
    bool IsActive { get; set; }
    string Type { get; set; }

    void OnGet();
    void OnRelease();    
    void Locate(Vector2 position);
}

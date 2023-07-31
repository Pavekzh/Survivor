using UnityEngine;

public interface IPooledWaveObject
{
    bool InPool { get; set; }
    bool IsActive { get; }
    string Type { get; set; }

    void OnGet(Vector2 position);
    void OnRelease();    
}

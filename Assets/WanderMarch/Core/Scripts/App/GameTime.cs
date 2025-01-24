using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : Singleton<GameTime>
{
    public static float ScaledTime { private set; get; }
    public static float FixedScaledTime { private set; get; }

    [Range(-3.0f,3.0f)]public float timeScale = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScaledTime = UnityEngine.Time.deltaTime * timeScale;
    }

    private void FixedUpdate()
    {
        FixedScaledTime = UnityEngine.Time.fixedDeltaTime * timeScale;
    }
}

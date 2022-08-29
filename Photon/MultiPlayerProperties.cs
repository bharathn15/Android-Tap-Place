using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerProperties
{

    #region Private Static Fields
    readonly private static string gameVersion = "1";

    readonly private static byte maxPlayerPerRoom = 4;

    #endregion


    #region Public Methods

    /// <summary>
    /// Game Version
    /// </summary>
    public static string GameVersion
    {
        get { return gameVersion; }
    }

    /// <summary>
    /// Maximum Players Per Room
    /// </summary>
    public static byte MaxPlayerPerRoom
    {
        get { return maxPlayerPerRoom; }
    }

    #endregion


}

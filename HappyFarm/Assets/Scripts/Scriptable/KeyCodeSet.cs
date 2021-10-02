using UnityEngine;

[CreateAssetMenu(fileName = "KeyCodeSet")]
public class KeyCodeSet : ScriptableObject
{
    public MyKeyCode upKey;
    public MyKeyCode downKey;
    public MyKeyCode leftKey;
    public MyKeyCode rightKey;
    public MyKeyCode rangeAttackKey;
    public MyKeyCode meleeAttackKey;
    public MyKeyCode eatKey;
}

public enum MyKeyCode
{
    None = 0,
    Space = 32,
    Alpha0 = 48,
    Alpha1 = 49,
    Alpha2 = 50,
    Alpha3 = 51,
    Alpha4 = 52,
    Alpha5 = 53,
    Alpha6 = 54,
    Alpha7 = 55,
    Alpha8 = 56,
    Alpha9 = 57,
    A = 97,
    B = 98,
    C = 99,
    D = 100,
    E = 101,
    F = 102,
    G = 103,
    H = 104,
    I = 105,
    J = 106,
    K = 107,
    L = 108,
    M = 109,
    N = 110,
    O = 111,
    P = 112,
    Q = 113,
    R = 114,
    S = 115,
    T = 116,
    U = 117,
    V = 118,
    W = 119,
    X = 120,
    Y = 121,
    Z = 122
}

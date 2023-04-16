using UnityEngine;
[CreateAssetMenu(fileName = "SOPlayer", menuName = "SO/Player", order = 0)]
public class SOPlayer : SOUnit
{
    [SerializeField] float invincibilityTime;
    public float InvincibilityTime => invincibilityTime;

}

using UnityEngine;
using UnityEngine.Events;

public class EntityHurtPlayer : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    public void HurtPlayer(int newDamage)
    {
        GameManager.playerTrans.GetComponent<PlayerHealth>().DoDamage(newDamage);
    }
}

using UnityEngine;
using System.Collections;

public class BarrierBuoy : MonoBehaviour {

    public GameObject barrier;

	public void ScaleBarrier(Vector3 relativePos, float distance)
    {
        Instantiate(barrier, transform.position, Quaternion.LookRotation(relativePos));
        barrier.transform.localScale = new Vector3(1, 1, distance * 5);
    }
}

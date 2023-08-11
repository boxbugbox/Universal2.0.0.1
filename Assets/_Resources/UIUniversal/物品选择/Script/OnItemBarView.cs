using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnItemBarView : MonoBehaviour
{
    public Button left;
    public Button right;
    public int child;
    public int count;
    public GameObject content;
    private void Start()
    {

        count = 7;
        right.onClick.AddListener(OnRight);
        left.onClick.AddListener(OnLeft);
    }
    private void OnRight()
    {
        child = content.transform.childCount;
        if (child > 7 && child!= count)
        {
            count++;
            var x = content.transform.localPosition.x;
            var y = content.transform.localPosition.y;
            var z = content.transform.localPosition.z;
            content.transform.localPosition = new Vector3(x + (-145.3974f), y, z);
        }
    }
    private void OnLeft()
    {
        if(count > 7)
        {
            count--;
            var x = content.transform.localPosition.x;
            var y = content.transform.localPosition.y;
            var z = content.transform.localPosition.z;
            content.transform.localPosition = new Vector3(x + 145.3974f, y, z);
        }
    }

}

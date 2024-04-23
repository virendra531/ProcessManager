using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitByte;
public class SubProcess : MonoBehaviour
{
    public Process process;
    [Header("Active This When ..")]
    [Range(0f,100f)]
    public float percentage;
    [Range(0f,100f)]
    public float completePercentage;
    public UnityEvent2[] WhenEnable;
    public UnityEvent2[] WhenComplete;
    private void Awake() {
        if(process != null)
        {
            process.valueChanged2 += CheckCondition;
            process.isActiveChanged += WhenProcessActive;
        }
    }
    void WhenProcessActive(Process process)
    {
        if(process.IsActive && percentage == 0f){
            WhenEnable.Invoke();
            Debug.Log(gameObject.name + " Enabled");
        } 
    }
    void CheckCondition()
    {
        if(process.IsActive && process.CurrentPercentage == percentage)
        {
            WhenEnable.Invoke();
            Debug.Log(gameObject.name + " Enabled");
        }
        if(process.IsActive && process.CurrentPercentage == completePercentage)
        {
            WhenComplete.Invoke();
            Debug.Log(gameObject.name + " Compeleted");
        }

        // StartCoroutine( Check() );
    }

    // IEnumerator Check(){
    //     yield return new WaitForEndOfFrame();

    //     if(process.IsActive && process.CurrentPercentage == percentage)
    //     {
    //         WhenEnable.Invoke();
    //     }
    //     if(process.IsActive && process.CurrentPercentage == completePercentage)
    //     {
    //         WhenComplete.Invoke();
    //     }
    // }
    [DrawButton]
    public void _SubProcessCompeleted()
    {
        if(process.IsActive)
        {
            StartCoroutine(SetPercentage(completePercentage));
        }
    }
    [DrawButton]
    public void _SubProcessStart()
    {
        if(process.IsActive)
        {
            StartCoroutine(SetPercentage(percentage));
        }
    }

    IEnumerator SetPercentage(float value)
    {
        process.CurrentPercentage = value;
        yield return new WaitForEndOfFrame();
        process.CurrentPercentage = value;
    }
    #if UNITY_EDITOR
    private void OnValidate() {
        if(!Application.isPlaying)
            StartCoroutine(SetProcess());
    }
    IEnumerator SetProcess()
    {
        Transform parent = transform.parent;
        Process getProcess;
        while (true)
        {
            yield return null;
            getProcess = parent.GetComponent<Process>();
            if( getProcess != null) {
                process = getProcess;
                break;
            }
            else
            {
                if(parent.parent == null) break;
                parent = parent.parent;
            }
        }

        int childCount = transform.parent.childCount;
        int index = transform.GetSiblingIndex();

        completePercentage = Mathf.Floor ( (100f / childCount) * (index + 1) );
        if(index == 0) percentage = 0f;
        else
            percentage = Mathf.Floor ( (100f / childCount) * index );
    }
    #endif
}

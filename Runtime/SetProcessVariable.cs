using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// #if UNITY_EDITOR
// 	using UnityEditor;
// #endif
public class SetProcessVariable : MonoBehaviour
{
    public bool setProcessName;
    public bool setPrevProcess;
    public bool setNextProcess;
    public bool setDependOnProcess;
    [DrawButton("Set Variable for Guided")]
    public void SetVariableForGuided()
    {
        Process[] allChildProcess = GetComponentsInChildren<Process>(true);

        for (int i = 0; i < allChildProcess.Length; i++)
        {
            allChildProcess[i].prevProcess.Clear();
            allChildProcess[i].nextProcess.Clear();
            allChildProcess[i].DependOnProcess.Clear();

            if(setProcessName){
                allChildProcess[i].ProcessName = allChildProcess[i].gameObject.name;
            }
            if(setPrevProcess)
            {
                if( (i-1) >= 0)
                {
                    // allChildProcess[i].prevProcess.Clear();
                    allChildProcess[i].prevProcess.Add( allChildProcess[i-1] );
                }
            }
            if(setNextProcess)
            {
                if( (i+1) < allChildProcess.Length)
                {
                    // allChildProcess[i].nextProcess.Clear();
                    allChildProcess[i].nextProcess.Add( allChildProcess[i+1] );
                }
            }
            if(setDependOnProcess)
            {
                // if( (i-1) >= 0)
                //     allChildProcess[i].DependOnProcess.Add( allChildProcess[i-1] );

                // allChildProcess[i].DependOnProcess.Clear();

                allChildProcess[i].DependOnProcess.AddRange( allChildProcess[i].nextProcess );
                allChildProcess[i].DependOnProcess.AddRange( allChildProcess[i].prevProcess );
            }
        }
    }
}
// #if UNITY_EDITOR
// [CustomEditor(typeof(SetProcessVariable))]
// public class AddButton : Editor
// {
// 	public override void OnInspectorGUI()
//     {
//     	SetProcessVariable myScript = (SetProcessVariable)target;
//         if(GUILayout.Button("Set Variable for Guided"))
//         {
// 			myScript.SetVariableForGuided();
//         }
// 		DrawDefaultInspector();
//     }
// }
// #endif
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
public class FreezeControls : MonoBehaviour
{
    public FirstPersonController fpController;

        public void FirstPersonControllerEnabled(bool isEnabled)
        {
            fpController.enabled = isEnabled;
        }
}

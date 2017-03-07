using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public FilingCabinet[] filingCabinetScripts;
    public SafeLocks safeScript;
    public SingleSwitch[] switchScripts;
    public ThreeSwitches threeSwitches;
    public SpecificDoor[] specificDoors;
    public SwitchesInfoScientist switchInfoScientist;
    public DisableCamera[] disableCameras;
    public CameraButton[] cameraButtons;
    public LightController lightController;
    public LightToggleInstantiate lightToggleInstantiateScript;
    public LightButton[] lightButtons;
    public AudioSource[] lightSwitchesAS;
    public ScientistComputer scientistComputerScript;

    public GameObject panicButton;
    private AudioSource m_panicButtonAS;
    private Renderer m_panicButtonRenderer;
    public Material switchedOffBlackMat;
    private bool m_panicButtonPressed = false;

    public ScientistRaycast scientistRaycast;

    void Start()
    {
        m_panicButtonAS = panicButton.GetComponent<AudioSource>();
        m_panicButtonRenderer = panicButton.GetComponent<Renderer>();
    }

    //0 - safe/1,2,3 - Filing Cabinets/4,5,6 - Switches/7 - SwitchesButton/8 - Switches info/9 - PanicButton(Scientist)/10...19 - Doors/20...29 - Lights/30...39 - Cameras/40 - Scientist Computer
    public void InteractedWith(int objectID)
    {
        if(objectID == 0)
        {
            safeScript.Interact();
        }

        if(objectID >= 1 && objectID <= 3)
        {
            filingCabinetScripts[objectID - 1].Interact();
        }

        if(objectID >= 4 && objectID <= 6)
        {
            switchScripts[objectID - 4].Interact();
        }

        if(objectID == 7)
        {
            threeSwitches.Interact();
        }

        if(objectID == 8)
        {
            switchInfoScientist.Interact();
        }

        if(objectID == 9)
        {
            if (!m_panicButtonPressed)
            {
                gameObject.GetComponent<PanicButton>().Interact();
                m_panicButtonRenderer.material = switchedOffBlackMat;
                panicButton.tag = "Untagged";
                m_panicButtonAS.Play();
                m_panicButtonPressed = true;
            }
        }
        if(objectID >= 10 && objectID <= 19)
        {
            specificDoors[objectID - 10].Interact();
        }
        if(objectID >= 20 && objectID <= 29)
        {
            lightButtons[objectID - 20].Power();
            //lightToggleInstantiateScript.LightToggled(objectID - 20);
            lightSwitchesAS[objectID - 20].Play();
        }
        if (objectID >= 30 && objectID <= 39)
        {
            cameraButtons[objectID - 30].Power();
            //disableCameras[objectID - 30].Interact();
        }
        if(objectID == 40)
        {
            scientistComputerScript.Interact();
        }
        if(objectID == 41)
        {
            scientistRaycast.CheckMap();
        }
    }

    public void AIInteractedWith(int number)
    {
        InteractedWith(number);
    }
}

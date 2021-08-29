using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showHand = true;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Didn't find a corresponding controller, using the default one");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }

        spawnedHandModel = Instantiate(handModelPrefab, transform);
        handAnimator = spawnedHandModel.GetComponent<Animator>();
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }


        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showHand)
        {
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);
            UpdateHandAnimation();
        }
        else
        {
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
        }
        
        /*
        
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
        {
            Debug.Log("Presionaste la A");
        }

        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1)
        {
            Debug.Log("Le aplastaste al trigger esta cantidad: " + triggerValue);
        }

        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Le moviste a la palanquita así: " + primary2DAxisValue);
        }

        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System;

public class ARInteractionsManager : MonoBehaviour
{
    [SerializeField] private Camera aRCamera;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject aRPointer;
    private GameObject item3DModel;
    private GameObject itemSelected;
    
    private bool isInitialPosition;
    private bool isOverUI;
    private bool isOver3DModel;

    // Variables para Transformaciones
    private Vector2 initialTouchPos;
    private float initialPinchDistance;
    private Vector3 initialScale;

    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
            item3DModel.transform.position = aRPointer.transform.position;
            item3DModel.transform.parent = aRPointer.transform;
            isInitialPosition = true;
        }
    }

    void Start()
    {
        aRPointer = transform.GetChild(0).gameObject;
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        
        // Suscripción al evento de menú para limpiar el modelo actual
        if (ARManager.instance != null)
        {
            ARManager.instance.OnARMainMenu += SetItemPosition;
        }
    }

    void Update()
    {
        // 1. Posicionamiento inicial del Pointer en el centro
        HandleInitialPlacement();

        // 2. Control de Interacciones Táctiles
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);
            Vector2 touchPosition = touchOne.position;

            // FASE INICIAL: Determinamos qué estamos tocando
            if (touchOne.phase == TouchPhase.Began)
            {
                isOverUI = isTapOverUI(touchPosition);
                isOver3DModel = isTapOver3DModel(touchPosition);
            }

            // DESPLAZAMIENTO (Un solo dedo)
            if (touchOne.phase == TouchPhase.Moved && Input.touchCount == 1)
            {
                // Solo desplazamos si el toque NO es sobre UI y SI es sobre el modelo
                if (!isOverUI && isOver3DModel)
                {
                    if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
                    {
                        Pose hitPose = hits[0].pose;
                        transform.position = hitPose.position;
                    }
                }
            }

            // ROTACIÓN Y ESCALA (Dos dedos)
            if (Input.touchCount == 2 && item3DModel != null)
            {
                HandleTwoFingerGestures();
            }

            // RE-SELECCIÓN (Si el objeto estaba suelto y lo volvemos a tocar)
            if (isOver3DModel && item3DModel == null && !isOverUI)
            {
                ReSelectModel();
            }
        }
    }

    private void HandleTwoFingerGestures()
    {
        Touch t1 = Input.GetTouch(0);
        Touch t2 = Input.GetTouch(1);

        if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
        {
            initialTouchPos = t2.position - t1.position;
            initialPinchDistance = Vector2.Distance(t1.position, t2.position);
            initialScale = item3DModel.transform.localScale;
        }

        if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
        {
            Vector2 currentTouchPos = t2.position - t1.position;
            float currentDistance = Vector2.Distance(t1.position, t2.position);

            float angleDelta = Mathf.Abs(Vector2.SignedAngle(initialTouchPos, currentTouchPos));
            float distanceDelta = Mathf.Abs(currentDistance - initialPinchDistance);

            // Discriminador para evitar que rote y escale al mismo tiempo de forma errática
            if (angleDelta > 2f || distanceDelta > 15f) 
            {
                if (angleDelta > (distanceDelta * 0.3f)) 
                {
                    // ROTACIÓN
                    float angle = Vector2.SignedAngle(initialTouchPos, currentTouchPos);
                    item3DModel.transform.rotation = Quaternion.Euler(0, item3DModel.transform.eulerAngles.y - angle, 0);
                    initialTouchPos = currentTouchPos;
                }
                else 
                {
                    // ESCALADO con límites de seguridad
                    float factor = currentDistance / initialPinchDistance;
                    Vector3 targetScale = initialScale * factor;

                    float minS = initialScale.x * 0.2f;
                    float maxS = initialScale.x * 5.0f;

                    item3DModel.transform.localScale = new Vector3(
                        Mathf.Clamp(targetScale.x, minS, maxS),
                        Mathf.Clamp(targetScale.y, minS, maxS),
                        Mathf.Clamp(targetScale.z, minS, maxS)
                    );
                }
            }
        }
    }

    private void HandleInitialPlacement()
    {
        if (isInitialPosition)
        {
            Vector2 middlePoint = new Vector2(Screen.width / 2, Screen.height / 2);
            if (aRRaycastManager.Raycast(middlePoint, hits, TrackableType.Planes))
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                aRPointer.SetActive(true);
                isInitialPosition = false;
            }
        }
    }

    private void ReSelectModel()
    {
        if (ARManager.instance != null) ARManager.instance.ARPosition();
        item3DModel = itemSelected;
        itemSelected = null;
        aRPointer.SetActive(true);
        transform.position = item3DModel.transform.position;
        item3DModel.transform.parent = aRPointer.transform;
    }

    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        Ray ray = aRCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit3DModel))
        {
            if (hit3DModel.collider.CompareTag("Item"))
            {
                itemSelected = hit3DModel.transform.gameObject;
                return true;
            }
        }
        return false;
    }

    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = touchPosition };
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);
        return result.Count > 0;
    }

    private void SetItemPosition()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            aRPointer.SetActive(false);
            item3DModel = null;
        }
    }

    public void DeleteItem()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        Destroy(item3DModel);
        aRPointer.SetActive(false);
        ARManager.instance.ARMainMenu();
    }
}



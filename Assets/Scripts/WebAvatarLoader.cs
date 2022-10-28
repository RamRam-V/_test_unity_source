using ReadyPlayerMe;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WebAvatarLoader : MonoBehaviour
{
    private string AvatarURL = "";
    private AvatarLoader avatarLoader;

    public Transform spawnPoint;
    private Vector3 originPosition;
    private Vector3 originRotate;
    Vector3 uiPos = Vector3.zero;

    // public Animator animator;
    public RuntimeAnimatorController animController;
    public Avatar animAvatar;

    private Dictionary<string, GameObject> dicAvatar = new Dictionary<string, GameObject>();

    private int mouseDownCount = 0;

    private IDisposable mouseUpDisposable;
    private IDisposable mouseDownDisposable;
    private IDisposable mouseMoveDisposable;

    private Animator curAnimator;
    private Coroutine coroutine;

    public void Awake()
    {
        avatarLoader = new AvatarLoader();

        originPosition = spawnPoint.transform.position;
        originRotate = spawnPoint.transform.eulerAngles;

        UISystem.Instance.Initialize();

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PartnerSO partner = Resources.Load<PartnerSO>("Partner");
        WebInterface.SetupRpmFrame(partner.Subdomain);
    }

    //public void OnWebViewAvatarGenerated(string avatarUrl)
    //{
    //    LoadAvatar(avatarUrl);
    //}

    public void LoadAvatar(string avatarUrl)
    {
        AvatarURL = avatarUrl;
        Debug.Log("LoadAvatar : " + avatarUrl);

        if (uiPos == Vector3.zero)
        {
            uiPos = Camera.main.WorldToScreenPoint(new Vector3(originPosition.x, originPosition.y + 0.5f, originPosition.z));
        }

        UISystem.Instance.SetLoading(true, uiPos);

        CancelSubscribe();

        if (dicAvatar.ContainsKey(AvatarURL))
        {
            DisplayReset();

            dicAvatar[AvatarURL].SetActive(true);
            dicAvatar[AvatarURL].transform.position = originPosition;
            dicAvatar[AvatarURL].transform.eulerAngles = originRotate;

            dicAvatar[AvatarURL].transform.parent = spawnPoint;
            curAnimator = dicAvatar[AvatarURL].GetComponent<Animator>();

            Subscribe();

            UISystem.Instance.SetLoading(false, Vector3.zero);
            StartCoroutine(AvatarLoadComplete());

            StartAvatarRandomAction();
        }
        else
        {
            CreateAvatar(AvatarURL);
        }
    }

    private void CreateAvatar(string url)
    {
        avatarLoader.LoadAvatar(AvatarURL, OnAvatarImported, OnAvatarLoaded);
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }


        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private IEnumerator AvatarLoadComplete()
    {
        yield return new WaitForSeconds(0.5f);

        WebPageBridge.Instance.AvatarLoadingCompletedWebPage();
    }


    private void DisplayReset()
    {
        foreach (var character in dicAvatar.Values)
        {
            character.transform.parent = spawnPoint.transform.parent;
            character.SetActive(false);
        }
    }

    private void OnAvatarImported(GameObject avatar)
    {
        Debug.Log($"Avatar imported. [{Time.timeSinceLevelLoad:F2}]");
    }

    private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
    {
        //this.avatar = avatar;
        Debug.Log($"Avatar loaded. [{Time.timeSinceLevelLoad:F2}]\n\n{metaData}");

        DisplayReset();

        GameObject character = avatar;
        character.transform.position = originPosition;
        character.transform.eulerAngles = originRotate;
        character.transform.localScale = Vector3.one * 0.55f;
        character.transform.parent = spawnPoint;
        SetLayerRecursively(character, 8);

        spawnPoint.transform.position = originPosition;
        spawnPoint.transform.eulerAngles = originRotate;

        curAnimator = character.GetComponent<Animator>();
        // curAnimator.runtimeAnimatorController = animator.runtimeAnimatorController;
        curAnimator.avatar = animAvatar;
        curAnimator.runtimeAnimatorController = animController;
        curAnimator.applyRootMotion = true;

        dicAvatar.Add(AvatarURL, character);

        Debug.Log("OnAvatarLoaded : " + AvatarURL);
        Subscribe();

        UISystem.Instance.SetLoading(false, Vector3.zero);
        StartCoroutine(AvatarLoadComplete());
        AvatarLoadComplete();
        // StartAvatarRandomAction();
    }

    private void Subscribe()
    {
        mouseUpDisposable = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            .Subscribe(_ =>
            {
                mouseDownCount--;
            });
        mouseDownDisposable = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            .Subscribe(_ =>
            {
                mouseDownCount++;
                StartAvatarRandomAction();
            });

        mouseMoveDisposable = Observable.EveryUpdate()
            .Where(_ => mouseDownCount > 0)
            .Select(x => Input.GetAxis("Mouse X"))
            .Subscribe(x =>
            {
                if (x != 0)
                {
                    spawnPoint.transform.RotateAround(spawnPoint.transform.position, -Vector3.up, x * 500 * Time.deltaTime);
                }
            });
    }

    private void CancelSubscribe()
    {
        mouseDownCount = 0;

        mouseUpDisposable?.Dispose();
        mouseDownDisposable?.Dispose();
        mouseMoveDisposable?.Dispose();
    }

    private void StartAvatarRandomAction()
    {
        curAnimator.gameObject.SetActive(false);
        curAnimator.gameObject.SetActive(true);
        curAnimator.SetInteger("Animation_int", 8);

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(ActionEnd());
    }

    private IEnumerator ActionEnd()
    {
        yield return new WaitForSeconds(0.5f);

        curAnimator.SetInteger("Animation_int", 0);
    }
}

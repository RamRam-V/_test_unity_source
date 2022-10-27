using System.Collections.Generic;

using UnityEngine;

using ReadyPlayerMe;
using System;
using UniRx;
using System.Collections;
using System.Text;
using System.Linq;


public class ReadyPlayerMeController : MonoBehaviour
{
    GameObject avatar;

    public Transform spawnPoint;
    private Vector3 originPosition;
    private Vector3 originRotate;
    Vector3 uiPos = Vector3.zero;

    //public AnimatorController animatorController;
    public Animator animator;

    private Dictionary<string, GameObject> dicAvatar = new Dictionary<string, GameObject>();

    private int mouseDownCount = 0;

    private IDisposable mouseUpDisposable;
    private IDisposable mouseDownDisposable;
    private IDisposable mouseMoveDisposable;

    private Animator curAnimator;
    private Coroutine coroutine;

    public void Awake()
    {
        originPosition = spawnPoint.transform.position;
        originRotate = spawnPoint.transform.eulerAngles;

        UISystem.Instance.Initialize();

        DontDestroyOnLoad(this.gameObject);

        //Debug.Log(Test3(13));
        //Debug.Log(Test4(new int[] { 5, 5 }));
        //Debug.Log(Test5(626331));
        //Debug.Log(Test6(2, 5));
        //Debug.Log(Test7(4));
        //Debug.Log(Test8(new int[] { 10 }));
        //Debug.Log(Test9(3));
        //Debug.Log(Test10(118372));
        //Debug.Log(Test11(12345));
        //Debug.Log(Test12(987));
        //Debug.Log(Test13("try hello world"));
        //Debug.Log(Test14(5));
        //Debug.Log(Test15("AB", 1));
        //Debug.Log(Test16("-1234"));
        //Debug.Log(Test17(5));
    }

    public void LoadAvatar(string url)
    {
        Debug.Log("url : " + url);
        if (uiPos == Vector3.zero)
        {
            uiPos = Camera.main.WorldToScreenPoint(new Vector3(originPosition.x, originPosition.y + 0.5f, originPosition.z));
        }

        UISystem.Instance.SetLoading(true, uiPos);

        CancelSubscribe();

        if (dicAvatar.ContainsKey(url))
        {
            DisplayReset();

            dicAvatar[url].SetActive(true);
            dicAvatar[url].transform.position = originPosition;
            dicAvatar[url].transform.eulerAngles = originRotate;

            dicAvatar[url].transform.parent = spawnPoint;
            curAnimator = dicAvatar[url].GetComponent<Animator>();

            Subscribe();

            UISystem.Instance.SetLoading(false, Vector3.zero);
            StartCoroutine(AvatarLoadComplete());
        }
        else
        {
            CreateAvatar(url);
        }
    }

    private void DisplayReset()
    {
        foreach (var character in dicAvatar.Values)
        {
            character.transform.parent = spawnPoint.transform.parent;
            character.SetActive(false);
        }
    }

    private void CreateAvatar(string url)
    {
        var avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(url, OnAvatarImported, OnAvatarLoaded);
        //avatarLoader.OnCompleted += AvatarLoadingCompleted;
        //avatarLoader.OnFailed += AvatarLoadingFailed;
        //avatarLoader.OnProgressChanged += AvatarLoadingProgressChanged;
        //avatarLoader.LoadAvatar(url);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
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

    private void OnAvatarImported(GameObject avatar)
    {
        Debug.Log($"Avatar imported. [{Time.timeSinceLevelLoad:F2}]");
    }


    private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
    {
        this.avatar = avatar;
        Debug.Log($"Avatar loaded. [{Time.timeSinceLevelLoad:F2}]\n\n{metaData}");
    }

    //private void AvatarLoadingCompleted(object sender, CompletionEventArgs args)
    //{
    //    Debug.Log($"{args.Avatar.name} is imported!");

    //    DisplayReset();

    //    GameObject character = GameObject.Find(args.Avatar.name);
    //    character.transform.position = originPosition;
    //    character.transform.eulerAngles = originRotate;
    //    character.transform.localScale = Vector3.one * 0.55f;
    //    character.transform.parent = spawnPoint;
    //    SetLayerRecursively(character, 8);

    //    spawnPoint.transform.position = originPosition;
    //    spawnPoint.transform.eulerAngles = originRotate;

    //    curAnimator = character.GetComponent<Animator>();
    //    curAnimator.runtimeAnimatorController = animator.runtimeAnimatorController;
    //    curAnimator.applyRootMotion = false;
    //    dicAvatar.Add(args.Url, character);

    //    Subscribe();

    //    UISystem.Instance.SetLoading(false, Vector3.zero);
    //    StartCoroutine(AvatarLoadComplete());
    //}

    private IEnumerator AvatarLoadComplete()
    {
        yield return new WaitForSeconds(0.5f);

        WebPageBridge.Instance.AvatarLoadingCompletedWebPage();
    }

    //private void AvatarLoadingFailed(object sender, FailureEventArgs args)
    //{
    //    Debug.Log($"Failed with {args.Type}: {args.Message}");

    //    WebPageBridge.Instance.AvatarLoadFailWebPage();
    //}


    //private void AvatarLoadingProgressChanged(object sender, ProgressChangeEventArgs args)
    //{
    //    Debug.Log($"Progress: {args.Progress * 100}%");

    //    int n = 5;
    //    int m = 3;
    //    StringBuilder stringBuilder = new StringBuilder();
    //    for (int i = 0; i < m; i++)
    //    {
    //        if (i < m - 1)
    //            stringBuilder.Insert(0, "*", n).AppendLine();
    //        else
    //            stringBuilder.Insert(0, "*", n);
    //    }

    //    Debug.Log(stringBuilder.ToString());


    //    long x = 2;
    //    long sum = x;
    //    long[] arr = new long[n];
    //    for (int i = 0; i < n; i++)
    //    {
    //        arr[i] = sum;
    //        sum += x;
    //    }
    //}

    public int[,] Test(int[,] arr1, int[,] arr2)
    {
        // row 행
        // column 열
        int row = arr1.GetLength(0);
        int column = arr1.GetLength(1);
        int[,] result = new int[row, column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                result[i, j] = arr1[i, j] + arr2[i, j];
            }
        }
        return result;
    }

    public string Test2(string phoneNumber)
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (phoneNumber.Length > 4)
        {
            stringBuilder.Insert(0, "*", phoneNumber.Length - 4);
            stringBuilder.Insert(phoneNumber.Length - 4, phoneNumber);
        }
        else
        {
            stringBuilder.Append(phoneNumber);
        }

        return stringBuilder.ToString();
    }

    public bool Test3(int num)
    {
        int sum = 0;
        int num2 = num;
        while (num2 > 0)
        {// 123
            sum += num2 % 10;
            num2 = num2 / 10;
        }
        return (num % sum) == 0;
    }

    public double Test4(int[] arr)
    {
        double result = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            result += arr[i];
        }
        return result / arr.Length;
    }

    public int Test5(long num)
    {
        long num2 = num;
        int count = 0;

        if (num == 1)
            return 0;

        while (num2 != 1)
        {
            if (num2 % 2 == 0)
            {
                num2 = num2 / 2;
            }
            else
            {
                num2 = num2 * 3 + 1;
            }
            count++;

            if (count >= 500)
            {
                return -1;
            }
        }
        return count;
    }

    public int[] Test6(int n, int m)
    {
        int min = TestTest(n, m);// 최대공약수
        int max = n * m / min;// 최대공배수
        return new int[2] { min, max };
    }

    public int TestTest(int n, int m)
    {
        if (m == 0)
            return n;
        else
            return TestTest(m, n % m);
    }

    public string Test7(int num)
    {
        if (num % 2 == 0)
            return "Even";
        else
            return "Odd";
    }

    public int[] Test8(int[] arr)
    {
        int min = arr[0];
        int[] result = new int[arr.Length - 1];

        if (arr.Length <= 1)
        {
            return new int[] { -1 };
        }

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < min)
            {
                min = arr[i];
            }
        }

        int index = 0;
        for (int j = 0; j < arr.Length; j++)
        {
            if (min == arr[j])
                continue;
            else
                result[index++] = arr[j];
        }

        return result;
    }

    public long Test9(long n)
    {
        double test = Math.Sqrt(n);
        if (test % 1 > 0)
        {
            return -1;
        }
        else
        {
            return ((long)test + 1) * ((long)test + 1);
        }
    }

    public long Test10(long n)
    {
        string m = n.ToString();
        char[] result = new char[m.Length];

        for (int i = 0; i < m.Length; i++)
        {
            result[i] = m[i];
        }
        Array.Sort(result);
        Array.Reverse(result);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            sb.Append(result[i]);
        }

        return long.Parse(sb.ToString());
    }

    public int[] Test11(long n)
    {
        string m = n.ToString();
        int[] arr = new int[m.Length];
        for (int i = 0; i < m.Length; i++)
        {
            arr[i] = int.Parse(m[m.Length - i - 1].ToString());
        }
        return arr;
    }

    public int Test12(int n)
    {
        int nn = n;
        int sum = 0;
        while (nn > 0)
        {
            sum += nn % 10;
            nn = nn / 10;
        }

        return sum;
    }

    public string Test13(string n)
    {
        string[] split = n.Split(' ');
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < split.Length; i++)
        {
            for (int j = 0; j < split[i].Length; j++)
            {
                if (j % 2 == 0)
                {
                    sb.Append(split[i][j].ToString().ToUpper());
                }
                else
                {
                    sb.Append(split[i][j]);
                }
            }
            if (i < split.Length - 1)
            {
                sb.Append(" ");
            }
        }
        return sb.ToString();
    }

    public int Test14(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            if (n % i == 0)
                sum += i;
        }
        return sum;
    }

    public string Test15(string s, int n)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ' ')
            {
                sb.Append(' ');
            }
            else
            {
                int ascii;
                if (s[i] == 'z')
                {
                    ascii = Convert.ToInt32('a') - 1;
                }
                else if (s[i] == 'Z')
                {
                    ascii = Convert.ToInt32('A') - 1;
                }
                else
                {
                    ascii = Convert.ToInt32(s[i]);
                }
                sb.Append(Convert.ToChar(ascii + n));
            }
        }
        return sb.ToString();
    }

    private int Test16(string s)
    {
        return Convert.ToInt32(s);
    }

    private string Test17(int n)
    {
        string templete = "수박";
        StringBuilder sb = new StringBuilder();
        int repeat = n / templete.Length;
        sb.Insert(0, templete, repeat);

        if (n % templete.Length > 0)
        {
            sb.Insert(sb.ToString().Length, templete.ToCharArray(), 0, n % templete.Length);
        }
        return sb.ToString();
    }

    //private int Test18(int n)
    //{

    //}

    //private int Test18()
    //{
    //    int[] tree = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    //    int fixMin = 5;
    //    int fixMax = 7;
    //    for (int i = fixMin; i > 0; i--)
    //    {

    //    }
    //}

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

    private void OnDestroy()
    {
        CancelSubscribe();   
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
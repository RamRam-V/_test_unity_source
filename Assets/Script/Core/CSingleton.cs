public class CSingleton<T> where T : class, new()
{
    protected static T _instace = null;

    public static T Instance
    {
        get
        {
            if (_instace == null)
            {
                _instace = new T();
            }

            return _instace;
        }
    }
}
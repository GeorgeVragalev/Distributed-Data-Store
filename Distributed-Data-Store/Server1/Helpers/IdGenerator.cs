namespace Server1.Helpers;

public static class IdGenerator
{
    private static Mutex _mutex = new();
    private static int Id = 0;

    public static int GenerateId()
    {
        _mutex.WaitOne();
        Id++;
        _mutex.ReleaseMutex();
        return Id;
    }
}
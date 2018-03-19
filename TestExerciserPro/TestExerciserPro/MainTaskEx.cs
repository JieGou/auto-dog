using System.Threading.Tasks;

namespace TestExerciserPro
{
    /// <summary>
    /// This is only a helper class for the NET45, cause in 4.5 exists no TaskEx
    /// </summary>
    public static class MainTaskEx
    {
        public static Task Delay(int dueTime)
        {
            return Task.Delay(dueTime);
        }
    }
}
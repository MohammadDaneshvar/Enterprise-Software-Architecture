namespace Framework.Application
{
    public static class HaveResultExtentions
    {
        public static T As<T>(this IHaveResult result)
        {
            return (T)result.Result;
        }
    }
}
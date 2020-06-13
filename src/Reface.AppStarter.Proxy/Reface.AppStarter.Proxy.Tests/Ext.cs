namespace Reface.AppStarter.Proxy.Tests
{
    public static class Ext
    {
        public static void SetTestText(this App app, string text)
        {
            app.Context[Constant.APP_CONTEXT_TEST_TEXT] = text;
        }

        public static string GetTestText(this App app)
        {
            return (string)app.Context[Constant.APP_CONTEXT_TEST_TEXT];
        }
    }
}

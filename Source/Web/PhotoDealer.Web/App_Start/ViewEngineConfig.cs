namespace PhotoDealer.Web
{
    using System.Web.Mvc;

    public class ViewEngineConfig
    {
        public static void RegisterEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}

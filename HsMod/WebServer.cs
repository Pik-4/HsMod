using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static HsMod.PluginConfig;


namespace HsMod
{
    public static class WebServer
    {
        public static HttpListener httpListener = new HttpListener
        {
            AuthenticationSchemes = AuthenticationSchemes.Anonymous
        };

        public static Task listenerTask;

        public static void Restart()
        {
            try
            {
                httpListener.Stop();
                listenerTask = null;
            }
            catch (Exception ex)
            {
                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
            }
            finally
            {
                try
                {
                    httpListener.Prefixes.Remove($"http://+:{webServerPort.Value}/");
                    Start();
                }
                catch (Exception ex)
                {
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
                }
            }
        }

        public static void Start()
        {
            httpListener.Prefixes.Add($"http://+:{webServerPort.Value}/");
            httpListener.Start();
            listenerTask = Task.Factory.StartNew(delegate ()
            {
                while (httpListener.IsListening)
                {
                    HttpListenerContext httpListenerContext = httpListener.GetContext();
                    HttpListenerRequest httpListenerRequest = httpListenerContext.Request;
                    httpListenerContext.Response.StatusCode = 200;
                    httpListenerContext.Response.ContentType = "text/html; charset=UTF-8";
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, httpListenerRequest.RawUrl);
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, httpListenerRequest.Url);
                    using (StreamWriter streamWriter = new StreamWriter(httpListenerContext.Response.OutputStream))
                    {
                        streamWriter.WriteLine(Route(httpListenerRequest.RawUrl.ToString().ToLower()));
                        streamWriter.Close();
                    }
                }
            });
        }

        public static StringBuilder Route(string uri = "")
        {
            switch (uri)
            {
                case "/info":
                    return WebPage.InfoPage();
                case "/collection":
                    return WebPage.CollectionPage();
                case "/pack":
                    return WebPage.PackPage();
                case "/skins":
                    return WebPage.SkinsPage();
                case "/lettuce":
                    return WebPage.MercenariesLettucePage();
                case "/mercenaries":
                    return WebPage.MercenariesPage();
                case "/matchlog":
                    return WebPage.MatchLogPage();
                case "/alive":
                    return WebPage.AlivePage();
                case "":
                case "/":
                case "/home.html":
                case "/home":
                    return WebPage.HomePage();
                case "/about":
                case "/about.html":
                    return WebPage.AboutPage();
                default:
                    return new StringBuilder();
            }
        }
    }
}

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

        public static string shellCommand;

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
                    httpListener.Prefixes.Remove($"http://+:{CommandConfig.webServerPort}/");
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
            httpListener.Prefixes.Add($"http://+:{CommandConfig.webServerPort}/");
            httpListener.Start();
            listenerTask = Task.Factory.StartNew(delegate ()
            {
                while (httpListener.IsListening)
                {
                    HttpListenerContext httpListenerContext = httpListener.GetContext();
                    HttpListenerRequest httpListenerRequest = httpListenerContext.Request;
                    httpListenerContext.Response.StatusCode = 200;
                    string rawUrLower = httpListenerRequest.RawUrl.ToString().ToLower();


                    if (rawUrLower.EndsWith(".js"))
                        httpListenerContext.Response.ContentType = "text/javascript; charset=UTF-8";
                    else if (rawUrLower.EndsWith(".jpg") || rawUrLower.EndsWith(".jpge") || rawUrLower == "/safeimg")
                        httpListenerContext.Response.ContentType = "image/jpeg";
                    else if (rawUrLower == "/webshell" && isWebshellEnable.Value)
                    {
                        try
                        {
                            System.IO.StreamReader reader = new System.IO.StreamReader(httpListenerContext.Request.InputStream, httpListenerContext.Request.ContentEncoding);
                            shellCommand = reader?.ReadToEnd();
                            if (shellCommand != null && shellCommand.Length <= "command=".Length)
                            {
                                shellCommand = "";
                            }
                            else
                            {
                                shellCommand = shellCommand.Substring("command=".Length);
                                shellCommand = Uri.UnescapeDataString(shellCommand);
                                shellCommand = shellCommand.Replace('+', ' ');
                            }
                        }
                        catch (Exception ex)
                        {
                            Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
                        }
                        Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, shellCommand);
                        httpListenerContext.Response.ContentType = "text/html; charset=GBK";

                    }
                    else
                        httpListenerContext.Response.ContentType = "text/html; charset=UTF-8";

                    Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, httpListenerRequest.RawUrl);
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, httpListenerRequest.Url);

                    string preUrl = rawUrLower.Substring(1);
                    switch (preUrl)
                    {
                        case "hslog": preUrl = hsLogPath.Value; break;
                        // case "beplog": preUrl = "BepInEx/LogOutput.log"; break;
                        default: preUrl = "website/" + rawUrLower.Substring(1); break;
                    }


                    if (File.Exists(preUrl))   // 用于移除/ ，优先查找本地文件
                    {
                        var file = File.ReadAllBytes(preUrl);
                        httpListenerContext.Response.OutputStream.Write(file, 0, file.Length);
                        httpListenerContext.Response.OutputStream.Close();
                        file = null;
                    }
                    else if (rawUrLower == "/safeimg")
                    {
                        var safeimg = Convert.FromBase64String(WebPage.SafeImg);
                        httpListenerContext.Response.OutputStream.Write(safeimg, 0, safeimg.Length);
                        httpListenerContext.Response.OutputStream.Close();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(webPageBackImg.Value))
                        {
                            webPageBackImg.Value = $"http://{httpListenerRequest.UserHostName}/safeimg";
                        }

                        using (StreamWriter streamWriter = new StreamWriter(httpListenerContext.Response.OutputStream))
                        {
                            streamWriter.WriteLine(Route(rawUrLower));
                            streamWriter.Close();
                        }
                    }
                }
            });
        }

        public static StringBuilder Route(string url = "")
        {
            switch (url)
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
                case "/home":
                    return WebPage.HomePage();
                case "/about":
                    return WebPage.AboutPage();
                case "/shell":
                    return WebPage.ShellPage();
                case "/webshell":
                    return WebPage.Webshell();
                case "/jquery.min.js":
                    return new StringBuilder(WebPage.jQuery);
                default:
                    return new StringBuilder();
            }
        }
    }
}

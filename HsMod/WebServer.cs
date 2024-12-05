using System;
using System.Collections.Generic;
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
        public static bool shellCommandLock;
        public static bool pluginConfigLock;
        public static bool updateLock;

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
            listenerTask = Task.Run(ListenAsync);
        }

        private static async Task ListenAsync()
        {
            while (httpListener.IsListening)
            {
                var context = await httpListener.GetContextAsync(); // Fully asynchronous, non-blocking
                _ = Task.Run(() => ProcessRequestAsync(context));   // Start processing each request in a separate task
            }
        }

        private static async Task ProcessRequestAsync(HttpListenerContext context)
        {
            var request = context.Request;
            context.Response.StatusCode = 200;
            string rawUrLower = request.RawUrl.ToLower();

            Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, $"{request.RemoteEndPoint.ToString()} => {request.RawUrl}");
            Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, $"{DateTime.Now.ToString("yyyy/MM/dd_HH:mm:ss")} {request.Url}");

            if (rawUrLower == "/webshell" && request.HttpMethod == "POST" && !shellCommandLock)
            {
                shellCommandLock = true;

                string output = string.Empty;
                try
                {
                    // Read the JSON from the request body
                    using (var reader = new StreamReader(request.InputStream))
                    {
                        string requestBody = await reader.ReadToEndAsync();
                        Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, $"POST: {requestBody}");
                        // Parse JSON and get the "command" field
                        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

                        if (json != null && json.TryGetValue("command", out string command))
                        {
                            // Execute the command asynchronously
                            output = await WebApi.RunShellCommandAsync(command);
                        }
                        else
                        {
                            context.Response.StatusCode = 400; // Bad Request
                            output = "Invalid request: 'command' field is required.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500; // Internal Server Error
                    output = $"Error executing command: {ex.Message}";
                }
                finally
                {
                    shellCommandLock = false;
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        await writer.WriteLineAsync(output);
                    }
                }
            }
            else if (rawUrLower == "/config" && request.HttpMethod == "POST" && !pluginConfigLock)
            {
                pluginConfigLock = true;

                string output = string.Empty;
                try
                {
                    // Read the JSON from the request body
                    using (var reader = new StreamReader(request.InputStream))
                    {
                        string requestBody = await reader.ReadToEndAsync();
                        Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, $"POST: {requestBody}");
                        // Parse JSON and get the "key" field
                        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

                        if (json != null && json.TryGetValue("key", out string key) && json.TryGetValue("value", out string value))
                        {
                            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                            {

                                context.Response.StatusCode = 400; // Bad Request
                                output = "Invalid request: key or value not found.";
                            }
                            else
                            {
                                string realConfigKey = key.EndsWith(".name") ? key : LocalizationManager.GetLangKey(key);
                                if (!realConfigKey.EndsWith(".name"))
                                {
                                    context.Response.StatusCode = 400; // Bad Request
                                    output = "Invalid request: key not found.";
                                }
                                else
                                {
                                    context.Response.StatusCode = WebApi.RunPluginConfigAsync(realConfigKey, value, out string newValue);
                                    output = newValue;
                                }
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = 400; // Bad Request
                            output = "Invalid request: 'key' and 'value' field is required.";
                        }

                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500; // Internal Server Error
                    output = $"Error config: {ex.Message}";
                }
                finally
                {
                    pluginConfigLock = false;
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        await writer.WriteLineAsync($"{{\"status\":{context.Response.StatusCode},\"output\":\"{output}\"}}");
                    }
                }
            }
            else if (rawUrLower == "/update" && request.HttpMethod == "POST" && !updateLock)
            {
                updateLock = true;

                string output = string.Empty;
                try
                {
                    // Read the JSON from the request body
                    using (var reader = new StreamReader(request.InputStream))
                    {
                        string requestBody = await reader.ReadToEndAsync();
                        Utils.MyLogger(BepInEx.Logging.LogLevel.Debug, $"POST: {requestBody}");
                        // Parse JSON and get the "key" field
                        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

                        if (json != null && json.TryGetValue("key", out string key) && json.TryGetValue("value", out string value))
                        {
                            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                            {

                                context.Response.StatusCode = 400; // Bad Request
                                output = "Invalid request: key or value not found.";
                            }
                            else
                            {
                                if (key.ToLower().Equals("hsskins.cfg") || key.ToLower().Equals("hsskins"))
                                {
                                    context.Response.StatusCode = WebApi.UpdateHsSkinsCfg(value, out string newValue);
                                    output = newValue;
                                }
                                else
                                {
                                    context.Response.StatusCode = 400; // Bad Request
                                    output = "Invalid request: 'key' not support.";
                                }
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = 400; // Bad Request
                            output = "Invalid request: 'key' and 'value' field is required.";
                        }

                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500; // Internal Server Error
                    output = $"Error update: {ex.Message}";
                }
                finally
                {
                    updateLock = false;
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        await writer.WriteLineAsync($"{{\"status\":{context.Response.StatusCode},\"output\":\"{output}\"}}");
                    }
                }
            }
            else
            {
                context.Response.ContentType = DetermineContentType(rawUrLower);

                string preUrl = DetermineFilePath(rawUrLower);
                if (File.Exists(preUrl))   // 用于移除/ ，优先查找本地文件
                {
                    context.Response.ContentType = GetMimeType(Path.GetExtension(preUrl));
                    var file = await File.ReadAllBytesAsync(preUrl);
                    await context.Response.OutputStream.WriteAsync(file, 0, file.Length);
                }
                else if (rawUrLower == "/safeimg")
                {
                    //var safeimg = Convert.FromBase64String(WebPage.SafeImg);
                    //context.Response.OutputStream.Write(safeimg, 0, safeimg.Length);
                }
                else
                {
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        await writer.WriteLineAsync(Route(rawUrLower).ToString());
                    }
                }
            }
            context.Response.OutputStream.Close();
        }


        private static string DetermineContentType(string rawUrl)
        {
            if (rawUrl.EndsWith(".js"))
                return "text/javascript; charset=UTF-8";
            if (rawUrl.EndsWith(".jpg") || rawUrl.EndsWith(".jpeg") || rawUrl == "/safeimg")
                return "image/jpeg";
            if (rawUrl.EndsWith(".txt") || rawUrl.EndsWith(".log") || rawUrl.EndsWith(".cfg"))
                return "text/plain; charset=UTF-8";
            return "text/html; charset=UTF-8";
        }

        private static string DetermineFilePath(string rawUrl)
        {
            string preUrl = rawUrl.Substring(1);
            switch (preUrl)
            {
                case "hslog": preUrl = hsLogPath.Value; break;
                // case "beplog": preUrl = "BepInEx/LogOutput.log"; break;
                default: preUrl = Path.Combine(PluginConfig.HsModWebSite, rawUrl.Substring(1)); break;
            }
            return preUrl;
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
                case "/bepinex.min.log":
                    return WebPage.BepInExLogPage(666);
                case "/bepinex.log":
                    return WebPage.BepInExLogPage();
                case "/hsmod.cfg":
                    return WebPage.HsModCfgPage("HsMod.cfg");
                case "/hsskins.cfg":
                    return WebPage.HsModCfgPage("HsSkins.cfg");
                case "":
                case "/":
                case "/home":
                    return WebPage.HomePage();
                case "/about":
                    return WebPage.AboutPage();
                case "/shell":
                    return WebPage.ShellPage();
                case "/jquery.min.js":
                    return new StringBuilder(FileManager.ReadEmbeddedFile("./WebResources/jquery.min.js"));
                default:
                    return new StringBuilder();
            }
        }

        static string GetMimeType(string extension)
        {
            var mimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".cfg", "text/plain; charset=UTF-8" },
                { ".txt", "text/plain; charset=UTF-8" },
                { ".log", "text/plain; charset=UTF-8" },
                { ".html", "text/html; charset=UTF-8" },
                { ".htm", "text/html; charset=UTF-8" },
                { ".css", "text/css; charset=UTF-8" },
                { ".js", "application/javascript; charset=UTF-8" },
                { ".json", "application/json; charset=UTF-8" },
                { ".xml", "application/xml; charset=UTF-8" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".svg", "image/svg+xml" },
                { ".bmp", "image/bmp" },
                { ".mp3", "audio/mpeg" },
                { ".wav", "audio/wav" },
                { ".mp4", "video/mp4" },
                { ".pdf", "application/pdf" },
                { ".zip", "application/zip" },
            };

            return mimeTypes.TryGetValue(extension, out var mimeType) ? mimeType : "application/octet-stream";
        }

    }
}

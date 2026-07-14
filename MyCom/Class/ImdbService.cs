using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts.Designer.Native;
using DevExpress.XtraEditors;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using MyCom.Class;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

public class SearchResult
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Actors { get; set; }
    public string PosterUrl { get; set; }
}

public class MovieInfo
{
    public string ImdbId { get; set; }
    public string TitleEn { get; set; }
    public string TitleFa { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Actors { get; set; }
    public string Director { get; set; }
    public string ImdbRating { get; set; }
    public string PosterUrl { get; set; }
    public Image Poster { get; set; }
    public string TrailerUrl { get; set; }
    public string Country { get; set; }


    public bool Sinema { get; set; }
    public bool Serial { get; set; }
    public bool Animation { get; set; }
    public bool Anime { get; set; }
}

public class ImdbService
{
    private TileControl tileControl;
    private SimpleButton btnCancel;
    private LabelControl lblTitle;
    public MovieInfo SelectedMovie { get; private set; }

    private List<MovieInfo> movies;
    private readonly ImdbService _service;
    private WebView2 _webView2 = new WebView2 { Width = 1000, Height = 1000, Visible = false };
    private WebView2 _webView3 = new WebView2 { Width = 1000, Height = 1000, Visible = false };



    private async Task LoadMovieCardsAsync()
    {
        var group = tileControl.Groups[0];

        foreach (var movie in movies)
        {
            var item = new TileItem
            {
                Tag = movie,
                ItemSize = TileItemSize.Wide,
                AllowAnimation = true,
                ContentAnimation = TileItemContentAnimationType.Fade,
                AppearanceItem =
                {
                    Normal = { BackColor = Color.FromArgb(30, 30, 30) }
                }
            };

            var imageElement = new TileItemElement
            {
                ImageAlignment = TileItemContentAlignment.MiddleRight,
                ImageScaleMode = TileItemImageScaleMode.Stretch,
                AnimateTransition = DefaultBoolean.True,

                ImageSize = new Size(90, 120),
            };

            var googleTranslate = await GoogleTranslate(movie.TitleEn.ToLower());
            var titleElement = new TileItemElement
            {
                Text = $"{movie.TitleEn}\n{googleTranslate}",
                TextAlignment = TileItemContentAlignment.TopLeft,
                AnimateTransition = DefaultBoolean.True,
                Width = 175,
                Appearance =
                {
                    Normal =
                    {
                        Font = new Font("Samim FD", 10, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextOptions = { WordWrap = WordWrap.Wrap }
                    }
                }
            };

            var yearElement = new TileItemElement
            {
                //Text = "📅 " + (movie.Year ?? ""),
                Text = "\n" + $"📅 " + (movie.Year ?? ""),
                TextAlignment = TileItemContentAlignment.MiddleLeft,
                Appearance = { Normal = { ForeColor = Color.LightGray } }
            };

            var actorsElement = new TileItemElement
            {
                Text = movie.Actors ?? "",
                TextAlignment = TileItemContentAlignment.BottomLeft,
                Width = 175,
                AnimateTransition = DefaultBoolean.True,
                Appearance =
                {
                    Normal =
                    {
                        Font = new Font("Samim FD", 9),
                        ForeColor = Color.Silver,
                        TextOptions = { WordWrap = WordWrap.Wrap }
                    }
                }
            };

            item.Elements.Add(imageElement);
            item.Elements.Add(titleElement);
            item.Elements.Add(yearElement);
            item.Elements.Add(actorsElement);

            group.Items.Add(item);

            // تصویر رو پس‌زمینه دانلود کن - بلاک نکن
            if (!string.IsNullOrEmpty(movie.PosterUrl))
            {
                var capturedMovie = movie;
                var capturedElement = imageElement;

                _ = Task.Run(async () =>
                {
                    capturedMovie.Poster = await DownloadPosterAsync(capturedMovie.PosterUrl);
                    if (capturedMovie.Poster == null) return;

                    await tileControl.InvokeAsync(() =>
                    {
                        capturedElement.Image = capturedMovie.Poster;
                        tileControl.Invalidate(); // به جای item.Invalidate()
                    });
                });
            }
        }
    }

    private XtraForm form;
    public async Task ShowResult(RibbonForm ribbonForm = null)
    {
        form = new XtraForm();
        form.Text = @"انتخاب فیلم";
        form.Size = new Size(910, 550);
        form.StartPosition = FormStartPosition.CenterParent;
        form.RightToLeft = RightToLeft.Yes;
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.MaximizeBox = false;

        // ── عنوان ──
        lblTitle = new LabelControl
        {
            Text = @"فیلم مورد نظر را انتخاب کنید",
            Location = new Point(12, 12),
            Font = new Font("Samim FD", 14, FontStyle.Bold),
            AutoSizeMode = LabelAutoSizeMode.None,
            Height = 40,
            //Size = new Size(860, 30),
            Dock = DockStyle.Top,
            Appearance = { TextOptions = { HAlignment = HorzAlignment.Center } }
        };

        // ── TileControl ──
        tileControl = new TileControl
        {
            //Location = new Point(12, 50),
            //Size = new Size(1000, 560),
            Dock = DockStyle.Fill,
            Padding = new Padding(0, 10, 0, 0),
            //ItemSize = 180,
            ItemSize = 140,
            IndentBetweenItems = 10,
            AllowDrag = false,
            RightToLeft = RightToLeft.Yes
            //AllowItemDragging = DevExpress.XtraEditors.TileItemDraggingMode.Disabled,
        };

        // یه Group برای تایل ها
        var group = new TileGroup();
        tileControl.Groups.Add(group);
        tileControl.ItemClick += async (s1, e1) =>
        {



            tileControl.Enabled = false;
            var movieInfo = (MovieInfo)e1.Item.Tag;
            SelectedMovie = await GetDetailsVideo(movieInfo.ImdbId);
            SelectedMovie.Year = movieInfo.Year;

            #region Triler

            var getQues = ClassMessageBox.ShowMSGQues("تریلر فیلم دانلود شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
            if (getQues)
            {
                // new Task(async void () =>
                //  {
                try
                {
                    var getStatus = await DownloadTrailerAsync(SelectedMovie, @"C:\Users\Mojtaba\Desktop\فیلم\تیزر");
                    if (getStatus == false)
                        ClassMessageBox.ShowMSG("تریلر یافت نشد", Class_Text.Msg_Name,
                            ClassMessageBox.enumIcon.بستن_مربع);
                }
                catch (Exception exception)
                {
                    ClassMessageBox.ShowMSG("تریلر یافت نشد", Class_Text.Msg_Name,
                        ClassMessageBox.enumIcon.بستن_مربع);
                }
                form.DialogResult = DialogResult.OK;
                form.Close();


                //  }).Start();
            }
            else
            {
                form.DialogResult = DialogResult.OK;
                form.Close();
            }

            #endregion


            // });

        };

        // ── دکمه لغو ──
        btnCancel = new SimpleButton
        {
            Text = @"انصراف",
            Location = new Point(12, 525),
            Size = new Size(100, 30),
        };

        btnCancel.Click += (s, e) =>
        {
            SelectedMovie = new MovieInfo();
            form.DialogResult = DialogResult.Cancel;
            form.Close();
        };

        form.Shown += async (s, e) =>
        {
            form.Controls.AddRange(new Control[]
            {
                lblTitle, tileControl, btnCancel, _webView2, _webView3
            });
            // هر دو رو initialize کن
            await _webView2.EnsureCoreWebView2Async();
            await _webView3.EnsureCoreWebView2Async();
            await LoadMovieCardsAsync();
        };
        if (ribbonForm is null)
            form.ShowDialog();
        else
        {
            //ClsCollect

            form.OverShowWait<XtraForm>(ribbonForm);
            //form.MdiParent = ribbonForm;
            //form.Show();
        }
    }
    // ─────────────────────────────────────────
    // ساخت HttpClient با همه header های لازم
    // ─────────────────────────────────────────
    private HttpClient CreateHttpClient()
    {
        var handler = new HttpClientHandler
        {
            UseCookies = true,
            CookieContainer = new System.Net.CookieContainer(),
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
                                   | System.Net.DecompressionMethods.Deflate
        };

        var client = new HttpClient(handler);
        client.Timeout = TimeSpan.FromSeconds(30);

        client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/124.0.0.0 Safari/537.36");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept",
            "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Dest", "document");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Mode", "navigate");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Site", "none");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-User", "?1");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

        return client;
    }

    // ─────────────────────────────────────────
    // مرحله ۱: جستجو
    // ─────────────────────────────────────────
    private async Task<List<MovieInfo>> SearchAsync(string query)
    {
        using var http = CreateHttpClient();

        var url = $"https://v3.sg.media-imdb.com/suggestion/x/{Uri.EscapeDataString(query)}.json";

        var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.TryAddWithoutValidation("Referer", "https://www.imdb.com/");

        var response = await http.SendAsync(req);
        var json = JObject.Parse(await response.Content.ReadAsStringAsync());

        return json["d"]?
            // movies = new List<MovieInfo>();
            //movies.AddRange(json["d"]?
            .Where(x =>
            {
                int s = Convert.ToInt32(x["y"]?.ToString());
                return s > 2010 && x["id"]?.ToString().StartsWith("tt") == true;
            })
            .Select(x => new MovieInfo
            {
                ImdbId = x["id"].ToString(),
                TitleEn = x["l"]?.ToString(),
                Year = x["y"]?.ToString(),
                Actors = x["s"]?.ToString(),
                PosterUrl = x["i"]?["imageUrl"]?.ToString()
            })
            .ToList() ?? new List<MovieInfo>();
    }

    //public async Task<List<MovieInfo>> Search(string filmNameFa, string filmNameEn)
    public async Task Search(string filmNameFa, string filmNameEn)
    {
        if (string.IsNullOrEmpty(filmNameFa))
        {
            filmNameFa = await GoogleTranslate(filmNameEn, "en", "fa");
        }

        if (string.IsNullOrEmpty(filmNameEn))
        {
            filmNameEn = await GoogleTranslate(filmNameFa, "fa", "en");
        }
        var task1 = SearchAsync(filmNameFa);
        var task2 = SearchAsync(filmNameEn);
        //var task2 = GoogleTranslate(filmNameFa, "auto", "en")
        //    .ContinueWith(t => SearchAsync(t.Result)).Unwrap(); 

        await Task.WhenAll(task1, task2);
        movies = new List<MovieInfo>();
        if (task1.Result.Any())
            movies.AddRange(task1.Result);
        if (task2.Result.Any())
            movies.AddRange(task2.Result);

        //return new List<MovieInfo>();
    }

    public async Task<string> GoogleTranslate(string text, string sourceLang = "en", string targetLang = "fa")
    {
        using var http = CreateHttpClient();

        var url = $"https://translate.googleapis.com/translate_a/single" +
                  $"?client=gtx&sl={sourceLang}&tl={targetLang}&" +
                  //$"dt=te" +
                  //$"dt=rm" +
                  $"dt=t&dt=te&dt=rm" +  // ترکیبی از ترجمه‌های مختلف
                                         //$"dt=t" +
                  $"&q={Uri.EscapeDataString(text)}";

        var response = await http.GetStringAsync(url);
        var json = JArray.Parse(response);

        return string.Concat(json[0].Select(segment => segment[0]?.ToString()));
    }


    // ─────────────────────────────────────────
    // مرحله ۲: اطلاعات کامل با WebView2
    // ─────────────────────────────────────────
    public async Task<MovieInfo> GetDetailsVideo(string imdbId)
    {
        try
        {
            var tcs = new TaskCompletionSource<string>();

            async void OnNavigationCompleted(object s, CoreWebView2NavigationCompletedEventArgs e)
            {
                _webView2.NavigationCompleted -= OnNavigationCompleted;
                await Task.Delay(3000);

                var script = @"
                (function() {
                    var ldJson = document.querySelector('script[type=""application/ld+json""]');

                    var countryNodes = document.querySelectorAll('li[data-testid=""title-details-origin""] a');
                    var country = Array.from(countryNodes).map(r => r.innerText).join(', ');

                    return JSON.stringify({
                        ldJson:  ldJson ? ldJson.innerText : '',
                        country: country
                    });
                })()
            ";

                var result = await _webView2.ExecuteScriptAsync(script);
                tcs.TrySetResult(result);
            }

            _webView2.NavigationCompleted += OnNavigationCompleted;
            _webView2.Source = new Uri($"https://www.imdb.com/title/{imdbId}/");

            var timeout = Task.Delay(30000);
            var completed = await Task.WhenAny(tcs.Task, timeout);
            _webView2.NavigationCompleted -= OnNavigationCompleted;
            if (completed == timeout) return null;

            // parse نتیجه
            var raw = await tcs.Task;
            raw = Regex.Unescape(raw.Trim('"'));
            var resultObj = JObject.Parse(raw);

            var ldJsonRaw = resultObj["ldJson"]?.ToString();
            var country = resultObj["country"]?.ToString();

            if (string.IsNullOrWhiteSpace(ldJsonRaw) || ldJsonRaw == "null")
                return null;

            var ld = JObject.Parse(ldJsonRaw);

            // نوع
            string mediaType = ld["@type"]?.ToString() switch
            {
                "Movie" => "سینمایی",
                "TVSeries" => "سریال",
                _ => "نامشخص"
            };

            // ژانر
            bool anime = false;
            bool animation = false;
            string genre = ld["genre"] is JArray genreArr
                ? string.Join(", ", genreArr.Select(g => g.ToString()))
                : ld["genre"]?.ToString() ?? "";

            if (genre.Contains("Animation", StringComparison.OrdinalIgnoreCase))
            {
                // اگه کشور ژاپن بود انیمه، وگرنه انیمیشن
                bool isAnime = !string.IsNullOrEmpty(country) &&
                               country.Contains("Japan", StringComparison.OrdinalIgnoreCase);
                if (isAnime)
                    anime = true;
                else
                    animation = true;
            }

            string actors = ld["actor"] is JArray actorArr
                ? string.Join(", ", actorArr.Take(5).Select(a => a["name"]?.ToString()))
                : "";

            string director = ld["director"] is JArray dirArr
                ? dirArr[0]?["name"]?.ToString()
                : ld["director"]?["name"]?.ToString();

            string trailerUrl = null;
            if (ld["trailer"] is JToken trailerToken)
            {
                var t = trailerToken["url"]?.ToString()
                        ?? trailerToken["embedUrl"]?.ToString();
                if (!string.IsNullOrEmpty(t))
                    trailerUrl = t.StartsWith("http") ? t : "https://www.imdb.com" + t;
            }
            string titleEn = System.Net.WebUtility.HtmlDecode(ld["name"]?.ToString());
            return new MovieInfo
            {
                ImdbId = imdbId,
                TitleEn = titleEn,
                TitleFa = await GoogleTranslate(titleEn),
                Description = await GoogleTranslate(ld["description"]?.ToString()),
                //Description = ld["description"]?.ToString(),
                Genre = genre,
                ImdbRating = ld["aggregateRating"]?["ratingValue"]?.ToString(),
                Director = director,
                Actors = actors,
                PosterUrl = ld["image"]?.ToString(),
                Poster = movies.First(f => f.ImdbId == imdbId).Poster,
                TrailerUrl = trailerUrl,
                Sinema = mediaType == "سینمایی",
                Serial = mediaType == "سریال",
                Animation = animation,
                Anime = anime,
                Country = country
            };
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }

        return new MovieInfo();
    }

    // ─────────────────────────────────────────
    // دانلود پوستر
    // ─────────────────────────────────────────
    //public async Task<Image> DownloadPosterAsync(string url)
    //{
    //    if (string.IsNullOrEmpty(url)) return null;
    //    using var http = CreateHttpClient();
    //    var bytes = await http.GetByteArrayAsync(url);
    //    using var ms = new MemoryStream(bytes);
    //    return Image.FromStream(ms);
    //}
    public async Task<Image> DownloadPosterAsync(string url)
    {
        if (string.IsNullOrEmpty(url)) return null;

        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

            var bytes = await http.GetByteArrayAsync(url);

            // چک کن واقعاً تصویره
            if (bytes == null || bytes.Length < 100) return null;

            var ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"خطا: {ex.Message}\nURL: {url}");
            return null;
        }
    }
    public async Task<bool> DownloadTrailerAsync(MovieInfo movie, string saveFolder)
    {
        //_webView3.Visible = true;
        //_webView3.BringToFront();
        try
        {

            // ── مرحله ۱: لینک مستقیم mp4 رو از صفحه ویدیو بگیر ──
            var tcs = new TaskCompletionSource<string>();

            async void OnNavigationCompleted(object s, CoreWebView2NavigationCompletedEventArgs e)
            {

                _webView3.NavigationCompleted -= OnNavigationCompleted;
                await Task.Delay(3000);
                var script = @"
    (function() {
        var scripts = document.querySelectorAll('script');
        for (var i = 0; i < scripts.length; i++) {
            var text = scripts[i].textContent;
            if (text.indexOf('videoPlaybackData') > -1) {
                var match = text.match(/https:\/\/imdb-video\.media-imdb\.com\/[^""]+270p\.mp4/);
                if (match) return match[0].split('?')[0];
                match = text.match(/https:\/\/imdb-video\.media-imdb\.com\/[^""]+480p\.mp4/);
                if (match) return match[0].split('?')[0];
                match = text.match(/https:\/\/imdb-video\.media-imdb\.com\/[^""]+720p\.mp4/);
                if (match) return match[0].split('?')[0];
            }
        }
        return '';
    })()
";

                var result = await _webView3.ExecuteScriptAsync(script);
                result = result?.Trim('"') ?? "";
                tcs.TrySetResult(result);
            }

            _webView3.NavigationCompleted += OnNavigationCompleted;
            if (movie.TrailerUrl == null)
                return false;

            _webView3.Source = new Uri(movie.TrailerUrl);

            var timeout = Task.Delay(7000);
            var completed = await Task.WhenAny(tcs.Task, timeout);
            _webView3.NavigationCompleted -= OnNavigationCompleted;

            if (completed == timeout)
                return false;

            var mp4Url = await tcs.Task;
            if (string.IsNullOrEmpty(mp4Url)) return false;

            // ── مرحله ۲: دانلود فایل ──
            var fileName = $"{movie.TitleEn} - {movie.Year}.mp4";
            // کاراکترهای غیرمجاز رو حذف کن
            foreach (var c in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, '_');

            var savePath = Path.Combine(saveFolder, fileName);

            using var http = CreateHttpClient();
            var bytes = await http.GetByteArrayAsync(mp4Url);
            await File.WriteAllBytesAsync(savePath, bytes);

            return true;
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
            return false;
        }
    }
}
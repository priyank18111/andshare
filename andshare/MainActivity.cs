using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace andshare
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        Button _text, _link, _mutipleattachment, _attachment;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            UIReferences();
            UiClickEvents();
        }

        private void UiClickEvents()
        {
            _text.Click += _btn1_Click;
            _link.Click += _btn2_Click;
            _mutipleattachment.Click += _btn4_Click;
            _attachment.Click += _btn3_Click;
        }

        
        private void _btn4_Click(object sender, EventArgs e)
        {
            ShareMultipleFiles();
        }

        private void ShareMultipleFiles()
        {
            var file1 = Path.Combine(FileSystem.CacheDirectory, "Attachment1.txt");
            File.WriteAllText(file1, "Content 1");
            var file2 = Path.Combine(FileSystem.CacheDirectory, "Attachment2.txt");
            File.WriteAllText(file2, "Content 2");

            Share.RequestAsync(new ShareMultipleFilesRequest
            {
                Title = "Title",
                Files = new List<ShareFile> { new ShareFile(file1), new ShareFile(file2) }
            });
        }

        

        private void _btn3_Click(object sender, EventArgs e)
        {
           ShareFile();
        }

        private void ShareFile()
        {
            var fn = "Attachment.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, "Hello World");

            Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(file)
            });
        }

      

        private void _btn2_Click(object sender, EventArgs e)
        {
             ShareUri("https://www.google.co.in/");
        }

        private void ShareUri(string uri)
        {
            Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            }); ;
        }

        private void _btn1_Click(object sender, EventArgs e)
        {
          ShareText("Hello World");
        }

        private void ShareText(string text)
        {

            {
                Share.RequestAsync(new ShareTextRequest
                {
                    Text = text,
                    Title = "Share Text"
                });
            }
        }

        private void UIReferences()
        {
            _text = FindViewById<Button>(Resource.Id.button1);
            _link = FindViewById<Button>(Resource.Id.button2);
            _mutipleattachment = FindViewById<Button>(Resource.Id.button3);
            _attachment = FindViewById<Button>(Resource.Id.button4);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
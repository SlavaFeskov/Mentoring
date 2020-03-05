using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using NetStandardCL;
using AlertDialog = Android.App.AlertDialog;

namespace XamarinAndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            SetContentView(Resource.Layout.activity_main);

            Button btn = FindViewById<Button>(Resource.Id.button1);
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, System.EventArgs e)
        {
            EditText tv = FindViewById<EditText>(Resource.Id.editText1);
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Hello");
            dialog.SetMessage(Shared.GetMessage(tv.Text));
            dialog.SetPositiveButton("Ok", (c, ev) => { });
            dialog.Create().Show();
        }
    }
}
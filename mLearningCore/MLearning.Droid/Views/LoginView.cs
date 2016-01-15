using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using Core.ViewModels;
using System;
using Android.Graphics.Drawables;
using Android.Views.Animations;
using Android.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Android.Content.PM;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Core.Repositories;
using Cirrious.CrossCore;

namespace MLearning.Droid.Views
{
	[Activity(Label = "View for FirstViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
	public class LoginView : MvxActivity
	{
		int widthInDp;
		int heightInDp;



		RelativeLayout mainLayout;
		ProgressDialog _dialog;

		#region SignUp_views
		RelativeLayout relSingup;
		RelativeLayout relLogin;

		LinearLayout linearLogo;
		LinearLayout linearLicencia;
		LinearLayout linearLogin;
		LinearLayout linearSingup;

		LinearLayout linearTxta;
		LinearLayout linearTxtb;
		LinearLayout linearTxtc;
		LinearLayout linearContentText;

		ImageButton btnLogin;
		ImageButton btnSingUp;
		ImageButton btnFacebook;

		ImageView imgLogo;

		CheckBox chkLogin;

		TextView txtLicencia_a;
		TextView txtLicencia_b;

		TextView txtlogin_a1;
		TextView txtlogin_a2;
		TextView txtlogin_b1;
		TextView txtlogin_b2;
		TextView txtlogin_c1;
		TextView txtlogin_c2;
		#endregion
		#region Login_views
		LinearLayout linearButtonLogin;
		LinearLayout linearEditTextLogin;
		LinearLayout linearTextLogin;

		EditText etxtUser;
		EditText etxtPassword;

		ImageButton btnLoginInto;

		TextView txtLogin_a;
		TextView txtLogin_b;
		TextView txtInicioSesion;
		#endregion

		protected override void OnCreate(Bundle bundle)
		{
			this.Window.AddFlags (WindowManagerFlags.Fullscreen);
			base.OnCreate (bundle);



			var metrics = Resources.DisplayMetrics;

			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);

			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			initUi ();

			SetContentView (mainLayout);
		}

		private void initUi_SignUp()
		{
			#region initialize
			relSingup = new RelativeLayout (this);
			relLogin = new RelativeLayout (this);

			linearLicencia = new LinearLayout (this);
			linearLogin = new LinearLayout (this);
			linearSingup = new LinearLayout (this);
			linearLogo = new LinearLayout (this);

			linearTxta = new LinearLayout (this);
			linearTxtb = new LinearLayout (this);
			linearTxtc = new LinearLayout (this);
			linearContentText = new LinearLayout (this);

			imgLogo = new ImageView (this);
			btnLogin = new ImageButton (this);
			btnSingUp = new ImageButton (this);
			btnFacebook = new ImageButton (this);

			chkLogin = new CheckBox (this);

			txtLicencia_a = new TextView (this);
			txtLicencia_b = new TextView (this);

			txtlogin_a1 = new TextView (this);
			txtlogin_a2 = new TextView (this);
			txtlogin_b1 = new TextView (this);
			txtlogin_b2 = new TextView (this);
			txtlogin_c1 = new TextView (this);
			txtlogin_c2 = new TextView (this);
			#endregion

			relLogin.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);
			relSingup.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			linearTxta.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearTxtb.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearTxtc.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearContentText.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);

			linearLicencia.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearLicencia.Orientation = Orientation.Horizontal;
			linearLicencia.SetGravity (GravityFlags.Center);
			linearLogin.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearLogin.Orientation = Orientation.Horizontal;
			linearLogin.SetGravity (GravityFlags.Right);
			linearSingup.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearSingup.Orientation = Orientation.Vertical;
			linearSingup.SetGravity (GravityFlags.Center);
			linearLogo.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearLogo.Orientation = Orientation.Vertical;
			linearLogo.SetGravity (GravityFlags.Center);


			initText (txtlogin_a1, "Connect ", "#00c6ff");
			txtlogin_a1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			initText (txtlogin_a2, " width yout classmates ", "#ffffff");
			txtlogin_a2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			initText (txtlogin_b1, "Get help ", "#00c6ff");
			txtlogin_b1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			initText (txtlogin_b2, " on homework ", "#ffffff");
			txtlogin_b2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			initText (txtlogin_c1, "Get better graces ", "#00c6ff");
			txtlogin_c1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			initText (txtlogin_c2, " for real", "#ffffff");
			txtlogin_c2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			linearTxta.Orientation = Orientation.Horizontal;
			linearTxta.SetGravity (GravityFlags.Center);
			linearTxtb.Orientation = Orientation.Horizontal;
			linearTxtb.SetGravity (GravityFlags.Center);
			linearTxtc.Orientation = Orientation.Horizontal;
			linearTxtc.SetGravity (GravityFlags.Center);

			linearContentText.Orientation = Orientation.Vertical;
			linearContentText.SetGravity (GravityFlags.Center);

			linearTxta.AddView (txtlogin_a1); linearTxta.AddView (txtlogin_a2);
			linearTxtb.AddView (txtlogin_b1); linearTxtb.AddView (txtlogin_b2);
			linearTxtc.AddView (txtlogin_c1); linearTxtc.AddView (txtlogin_c2);

			linearContentText.AddView (linearTxta);
			linearContentText.AddView (linearTxtb);
			linearContentText.AddView (linearTxtc);

			var textFormat = Android.Util.ComplexUnitType.Px;

			txtLicencia_a.Text = "TO REGISTER, ACCEPT THE";
			txtLicencia_a.SetTextSize(textFormat,Configuration.getHeight(25));
			txtLicencia_a.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txtLicencia_a.SetTextColor (Color.ParseColor ("#ffffff"));
			txtLicencia_b.Text = " TERMS OF USE";
			txtLicencia_b.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtLicencia_b.SetTextSize(textFormat,Configuration.getHeight(25));
			txtLicencia_b.SetTextColor (Color.ParseColor ("#00c6ff"));
			chkLogin.Checked = false;

			chkLogin.SetBackgroundColor(Color.ParseColor("#ffffff"));

			imgLogo.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/logo.png"), Configuration.getWidth(317), Configuration.getHeight(70),true));
			btnLogin.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/login.png"), Configuration.getWidth(210), Configuration.getHeight(60),true));
			btnFacebook.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signupface.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));
			btnSingUp.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signupnolisto.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));

			Drawable dc = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/toregister.png"), Configuration.getWidth(47), Configuration.getHeight(47), true));
			chkLogin.SetBackgroundDrawable (dc);

			initButtonColor (btnLogin);
			initButtonColor (btnFacebook);
			initButtonColor (btnSingUp);
			linearLicencia.AddView (chkLogin);
			linearLicencia.AddView (txtLicencia_a);
			linearLicencia.AddView (txtLicencia_b);

			linearLogin.AddView (btnLogin);
			linearSingup.AddView (linearLicencia);
			linearSingup.AddView (btnSingUp);
			linearSingup.AddView (btnFacebook);

			linearLogo.AddView (imgLogo);

			linearLogo.SetX (0); linearLogo.SetY (Configuration.getHeight(447));
			linearContentText.SetX (0); linearContentText.SetY (Configuration.getHeight(557));
			linearLogin.SetX (0); linearLogin.SetY (Configuration.getHeight(30));
			linearSingup.SetX (0); linearSingup.SetY (Configuration.getHeight(785));

			relSingup.AddView (linearLogo);
			relSingup.AddView (linearContentText);
			relSingup.AddView (linearLogin);
			relSingup.AddView (linearSingup);
			mainLayout.AddView (relSingup);


			#region buttons_events

			btnLogin.Click+= BtnLogin_Click;

			btnSingUp.Click+= delegate {

				if(chkLogin.Checked==true){
					var com = ((LoginViewModel)this.DataContext).SignUpCommand;
					com.Execute(null);
				}
			} ;

			btnFacebook.Click += async delegate {
				await Authenticate(MobileServiceAuthenticationProvider.Facebook);
			} ;

			chkLogin.CheckedChange += delegate {
				if(chkLogin.Checked==true){
					btnSingUp.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signuplisto.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));
				}
				if(chkLogin.Checked==false){
					btnSingUp.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signupnolisto.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));
				}

			} ;
			#endregion


		}
		private void initUi_Login()
		{

		}

		public void initUi(){

			_dialog = new ProgressDialog(this);
			_dialog.SetMessage("Espere por favor...");
			_dialog.SetCancelable(false);

			mainLayout = new RelativeLayout (this);

			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);
			Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/afondo.png"), 768, 1024, true));
			mainLayout.SetBackgroundDrawable (d);

			initUi_SignUp();
			initUi_Login ();

		}

		void BtnLogin_Click (object sender, EventArgs e)
		{
			AlphaAnimation animation = new AlphaAnimation(1.0f,0.0f);
			animation.Duration = 350;
			animation.AnimationEnd+= Animation_AnimationEnd;               

			relSingup.StartAnimation(animation);     
		}

		void Animation_AnimationEnd (object sender, Animation.AnimationEndEventArgs e)
		{
			relSingup.Alpha = 0.0f;
			setItemLogin ();
		}

		public void setItemLogin(){

			var txtFormat = Android.Util.ComplexUnitType.Px;

			linearButtonLogin = new LinearLayout (this);
			linearEditTextLogin = new LinearLayout (this);
			linearTextLogin = new LinearLayout (this);

			etxtUser = new EditText (this);
			etxtPassword = new EditText (this);
			btnLoginInto = new ImageButton (this);
			txtLogin_a = new TextView (this);
			txtLogin_b = new TextView (this);
			txtInicioSesion = new TextView (this);

			linearButtonLogin.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearEditTextLogin.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearTextLogin.LayoutParameters = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);

			etxtUser.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (507), Configuration.getHeight (78));
			etxtPassword.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (507), Configuration.getHeight (78));

			linearButtonLogin.Orientation = Orientation.Horizontal;
			linearButtonLogin.SetGravity (GravityFlags.Center);
			linearEditTextLogin.Orientation = Orientation.Vertical;
			linearEditTextLogin.SetGravity (GravityFlags.Center);
			linearTextLogin.Orientation = Orientation.Vertical;
			linearTextLogin.SetGravity (GravityFlags.Center);

			etxtUser.Hint = "  Usuario"; 
			etxtUser.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			etxtPassword.Hint = "  Contraseñ";
			etxtPassword.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			etxtPassword.InputType = InputTypes.TextVariationVisiblePassword;

			txtLogin_a.Text = "FORGOT PASSWORD?";
			txtLogin_a.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtLogin_b.Text = "            CHANGE";
			txtLogin_b.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtLogin_a.SetTextSize (txtFormat,Configuration.getHeight(30));
			txtLogin_b.SetTextSize (txtFormat, Configuration.getHeight (30));

			txtInicioSesion.Text = "Iniciar Sesió";
			txtInicioSesion.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txtInicioSesion.SetTextColor (Color.ParseColor("#ffffff"));
			txtInicioSesion.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (36));

			btnLoginInto.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/otherlogin.png"),Configuration.getWidth (242), Configuration.getHeight (78),true));
			etxtUser.SetTextColor (Color.ParseColor ("#ffffff"));
			etxtPassword.SetTextColor (Color.ParseColor ("#ffffff"));

			btnLoginInto.Click += delegate {
				_dialog.Show();
				var com = ((LoginViewModel)this.DataContext).LoginCommand;
				com.Execute(null);
			} ;

			initButtonColor (btnLoginInto);

			etxtPassword.InputType = InputTypes.TextVariationVisiblePassword;
			etxtPassword.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;

			txtLogin_a.SetTextColor (Color.ParseColor ("#ffffff"));
			txtLogin_b.SetTextColor (Color.ParseColor ("#00c6ff"));


			Drawable drawableEditText = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/cajatexto.png"), Configuration.getWidth(507), Configuration.getHeight(80), true));
			etxtUser.SetBackgroundDrawable (drawableEditText);
			etxtPassword.SetBackgroundDrawable (drawableEditText);

			etxtUser.SetSingleLine (true);
			etxtPassword.SetSingleLine (true);

			LinearLayout space = new LinearLayout (this);
			space.LayoutParameters = new LinearLayout.LayoutParams (-1, 20);

			linearTextLogin.AddView (txtLogin_a);
			linearTextLogin.AddView (txtLogin_b);

			linearEditTextLogin.AddView (etxtUser);
			linearEditTextLogin.AddView (space);
			linearEditTextLogin.AddView (etxtPassword);

			txtInicioSesion.SetX (Configuration.getWidth(75)); txtInicioSesion.SetY (Configuration.getHeight(680));
			linearEditTextLogin.SetX (0); linearEditTextLogin.SetY (Configuration.getHeight(741));

			btnLoginInto.SetX (Configuration.getWidth (45));btnLoginInto.SetY (Configuration.getHeight (980));
			linearTextLogin.SetX (Configuration.getWidth (345));linearTextLogin.SetY (Configuration.getHeight(995));

			relLogin.AddView (txtInicioSesion);
			relLogin.AddView (linearEditTextLogin);
			//relLogin.AddView (linearButtonLogin);
			relLogin.AddView(btnLoginInto);
			relLogin.AddView (linearTextLogin);

			((LoginViewModel)this.ViewModel).PropertyChanged += Login_propertyChanged;;

			var set = this.CreateBindingSet<LoginView, LoginViewModel>();
			set.Bind(etxtUser).To(vm => vm.Username);
			set.Bind(etxtPassword).To(vm => vm.Password);
			set.Apply(); 

			mainLayout.AddView (relLogin);
		}

		void Login_propertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{

			var vm = (LoginViewModel)this.ViewModel;
			if (e.PropertyName == "LoginOK")
			{
				if (!vm.LoginOK) {

					_dialog.Hide ();
					AlertDialog.Builder builder = new AlertDialog.Builder (this);
					AlertDialog alertDialog = builder.Create ();
					alertDialog.SetTitle ("");
					alertDialog.SetMessage ("Ingrese datos correctos");

					alertDialog.SetButton ("Ok", (s, ev) => {

					} );
					alertDialog.Show ();

				}  else {

				}
			}

			if (e.PropertyName == "ConnectionOK")
			{
				if (!vm.ConnectionOK)
				{
					_dialog.Hide ();
					AlertDialog.Builder builder = new AlertDialog.Builder (this);
					AlertDialog alertDialog = builder.Create ();
					alertDialog.SetTitle ("");
					alertDialog.SetMessage ("Verifique su conexió de Internet");

					alertDialog.SetButton ("Ok", (s, ev) => {
					} );
					alertDialog.Show ();

				}
			}
		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public  void initButtonColor(ImageButton btn){
			btn.Alpha = 255;
			btn.SetBackgroundColor(Color.Transparent);
		}

		public void initText(TextView txt, String texto, String colores){

			var txtf = Android.Util.ComplexUnitType.Px;

			txt.Text = texto;
			txt.SetTextColor(Color.ParseColor(colores));
			txt.SetTextSize(txtf,Configuration.getHeight(30));	
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			_dialog.Hide ();
		}

		MobileServiceUser user ;

		private async Task Authenticate(MobileServiceAuthenticationProvider provider)
		{
			while (user == null)
			{  
				try
				{
					WAMSRepositoryService service = Mvx.Resolve<IRepositoryService>() as WAMSRepositoryService;
					user = await service.MobileService.LoginAsync(this, provider); 
					Console.WriteLine ("Facebook : " + user.UserId + "  " + user.MobileServiceAuthenticationToken ) ;
				}
				catch (InvalidOperationException e)
				{}  
			}  
			var vm = ViewModel as LoginViewModel;
			vm.CreateUserCommand.Execute(user);
		}
	}
}

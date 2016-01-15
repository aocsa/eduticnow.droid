using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Gcm.Client;

using MLearning.Core.ViewModels;
using System.ComponentModel;
using Android.Widget;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Graphics;
using System;
using Android.Support.V4.Widget;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using System.Collections.ObjectModel;
using TaskView;
using Android.Content.PM;
using MLearning.Core.Entities;
using MLearningDB;

namespace MLearning.Droid.Views
{

	[Activity(Label = "View for FirstViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainView : MvxActionBarActivity
    {

		private SupportToolbar mToolbar;
		private MyActionBarDrawerToggle mDrawerToggle;
		private DrawerLayout mDrawerLayout;

		private LinearLayout mLeftDrawer;
		private LinearLayout mRightDrawer;

		private List<ChatDataRow> mItemsChat;
		private ListView mListViewChat;
		private LOContainerView _foro;

		private int currentIndexLO;
		public int currentUnidad=0;

		TextView title_view;
		TextView title_list;
		TextView info1;
		TextView info2;



		LinearLayout ln_chat_row;


		//private SupportToolbar mToolbar;

		RelativeLayout mainLayout;
		TextView txtUserName;
		TextView txtSchoolName;
		TextView txtCurse;
		TextView txtUserRol;
		TextView txtPorcentaje;
		TextView txtCurseTitle;
		TextView txtTaskTitle;
		TextView txtPendiente;


		ImageView imgUser;
		ImageView imgSchool;
		ImageView imgNotificacion;
		ImageView imgChat;
		ImageView imgCurse;
		ImageView imgTask;

		ProgressBar progressBar;
		LinearLayout linearTxtValorBarra;
		TextView txtValorBarra;
		LinearLayout linearCurse;
		LinearLayout linearTask;
		LinearLayout linearUserData;
		LinearLayout linearBarraCurso;
		LinearLayout linearSchool;
		LinearLayout linearListCurso;
		LinearLayout linearListTask;
		LinearLayout linearList;
		LinearLayout linearPendiente;
		LinearLayout linearUser;

		ProgressDialog _dialogDownload;


		RelativeLayout main_ContentView;
		TaskView task;
		int widthInDp;
		int heightInDp;
		int PositionLO =0;
		List<CursoItem> _currentCursos = new List<CursoItem>();
		ListView  listCursos;

		List<TaskItem> _currentTask = new List<TaskItem>();
		ListView  listTasks;

		FrameLayout frameLayout;


		MainViewModel vm;
		WallView loWallView;


		ScrollView scrollIndice;
		LinearLayout linearContentIndice;

        protected override void OnCreate(Bundle bundle)
        {
			this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);

			vm = this.ViewModel as MainViewModel;
			vm.PropertyChanged += Vm_PropertyChanged;
			loWallView = new WallView(this);
			currentIndexLO = 0;

			LinearLayout linearMainLayout = FindViewById<LinearLayout>(Resource.Id.left_drawer);

			/*scrollIndice = new ScrollView (this);
			scrollIndice.LayoutParameters = new ScrollView.LayoutParams (-1,400);
			linearContentIndice = new LinearLayout (this);
			linearContentIndice.LayoutParameters = new LinearLayout.LayoutParams (-1, 800);
			linearContentIndice.Orientation = Orientation.Vertical;
			linearContentIndice.SetBackgroundColor (Color.Red);

			scrollIndice.AddView (linearContentIndice); */


			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			task = new TaskView (this);

			iniMenu ();

			mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			SetSupportActionBar(mToolbar);
			mToolbar.SetNavigationIcon (Resource.Drawable.hamburger);

			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			mLeftDrawer = FindViewById<LinearLayout>(Resource.Id.left_drawer);
			mRightDrawer = FindViewById<LinearLayout>(Resource.Id.right_drawer);

			mLeftDrawer.Tag = 0;
			mRightDrawer.Tag = 1;

			frameLayout = FindViewById<FrameLayout> (Resource.Id.content_frame);

			main_ContentView = new RelativeLayout (this);
			main_ContentView.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);


			// main_ContentView.AddView (scrollIndice);
			//main_ContentView.AddView(linearContentIndice);

			LOContainerView LOContainer = new LOContainerView (this);

			//main_ContentView.AddView (LOContainer);
			//WallView wallview = new WallView(this);

			//wallview.OpenLO.Click += Lo_ImagenLO_Click;
			//lo.OpenTasks.Click += ListTasks_ItemClick;

			//wallview.OpenChat.Click += imBtn_Chat_Click;
			//wallview.OpenUnits.Click += imBtn_Units_Click;

			loWallView.OpenChat.Click += imBtn_Chat_Click;
			loWallView.OpenUnits.Click += imBtn_Units_Click;

			main_ContentView.AddView (loWallView);


			frameLayout.AddView (main_ContentView);


			RelativeLayout RL = FindViewById<RelativeLayout> (Resource.Id.main_view_relativeLayoutCL);

			Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/nubeactivity.png"), 768, 1024, true));
			RL.SetBackgroundDrawable (dr);

			dr = null;

			//seting up chat view content


			title_view = FindViewById<TextView> (Resource.Id.chat_view_title);


			info1= FindViewById<TextView> (Resource.Id.chat_view_info1);
			info2 = FindViewById<TextView> (Resource.Id.chat_view_info2);
			title_list = FindViewById<TextView> (Resource.Id.chat_list_title);

			mListViewChat = FindViewById<ListView> (Resource.Id.chat_list_view);

			title_view.SetX (Configuration.getWidth(74));
			title_view.SetY (Configuration.getHeight (202));

			title_view.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			title_view.SetTypeface (null, TypefaceStyle.Bold);

			info1.SetX (Configuration.getWidth (76));
			info1.SetY (Configuration.getHeight (250));
			info1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			info2.SetX (Configuration.getWidth (76));
			info2.SetY (Configuration.getHeight (285));
			info2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			title_list.SetX (Configuration.getWidth (76));
			title_list.SetY (Configuration.getHeight (391));

			title_list.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			title_list.SetTypeface (null, TypefaceStyle.Bold);

			mListViewChat.SetX (0);
			mListViewChat.SetY (Configuration.getHeight (440));

			//end setting



			linearMainLayout.AddView (mainLayout);

            vm.PropertyChanged += new PropertyChangedEventHandler(logout_propertyChanged);

            RegisterWithGCM();

			mDrawerToggle = new MyActionBarDrawerToggle(
				this,							//Host Activity
				mDrawerLayout,					//DrawerLayout
				Resource.String.openDrawer,		//Opened Message
				Resource.String.closeDrawer		//Closed Message
			);

			mDrawerLayout.SetDrawerListener(mDrawerToggle);
			SupportActionBar.SetHomeButtonEnabled (true);
			SupportActionBar.SetDisplayShowTitleEnabled(false);

			mDrawerToggle.SyncState();

			if (bundle != null)
			{
				if (bundle.GetString("DrawerState") == "Opened")
				{
					SupportActionBar.SetTitle(Resource.String.openDrawer);
				}

				else
				{
					SupportActionBar.SetTitle(Resource.String.closeDrawer);
				}
			}
			else
			{
				SupportActionBar.SetTitle(Resource.String.closeDrawer);
			}
				
			initListCursos ();
			iniPeoples ();
			initListTasks ();

			//main_ContentView.AddView (scrollIndice);

        }




		private void iniMenu(){
			mainLayout = new RelativeLayout (this);
	



			_foro = new LOContainerView (this);

			_dialogDownload = new ProgressDialog (this);
			_dialogDownload.SetCancelable (false);
			_dialogDownload.SetMessage ("Downloading...");

			txtUserName = new TextView (this);
			txtCurse = new TextView (this);
			txtSchoolName = new TextView (this);
			txtUserRol = new TextView (this);
			txtPorcentaje = new TextView (this);
			txtCurseTitle = new TextView (this);
			txtTaskTitle = new TextView (this);
			txtPendiente = new TextView (this);
			txtValorBarra = new TextView (this);

			imgChat = new ImageView (this);
			imgUser = new ImageView (this);
			imgSchool = new ImageView (this);
			imgNotificacion = new ImageView (this);
			imgCurse = new ImageView (this);
			imgTask = new ImageView (this);

			linearBarraCurso = new LinearLayout (this);
			linearCurse= new LinearLayout (this);
			linearSchool= new LinearLayout (this);
			linearTask= new LinearLayout (this);
			linearUserData= new LinearLayout (this);
			linearUser = new LinearLayout (this);
			linearListCurso = new LinearLayout (this);
			linearListTask = new LinearLayout (this);
			linearList = new LinearLayout (this);
			linearPendiente = new LinearLayout (this);

			linearTxtValorBarra = new LinearLayout (this);

			listCursos = new ListView (this);
			listTasks = new ListView (this);	
			mItemsChat = new List<ChatDataRow> ();

			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);
			Drawable d = new BitmapDrawable (getBitmapFromAsset ("icons/fondo.png"));
			mainLayout.SetBackgroundDrawable (d);
			d = null;

			linearBarraCurso.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearCurse.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(50));
			linearTask.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(50));
			linearSchool.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearUserData.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearUser.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearListCurso.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(250));
			linearListTask.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearList.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearPendiente.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (30), Configuration.getWidth (30));
			linearTxtValorBarra.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);

			linearBarraCurso.Orientation = Orientation.Vertical;
			linearBarraCurso.SetGravity (GravityFlags.Center);

			linearTxtValorBarra.Orientation = Orientation.Vertical;
			linearTxtValorBarra.SetGravity (GravityFlags.Center);
			txtValorBarra.Gravity = GravityFlags.Center;

			linearCurse.Orientation = Orientation.Horizontal;
			linearCurse.SetGravity (GravityFlags.CenterVertical);

			linearTask.Orientation = Orientation.Horizontal;
			linearTask.SetGravity (GravityFlags.CenterVertical);

			linearSchool.Orientation = Orientation.Horizontal;
			//linearSchool.SetGravity (GravityFlags.CenterVer);

			linearUserData.Orientation = Orientation.Vertical;
			linearUserData.SetGravity (GravityFlags.Center);

			linearUser.Orientation = Orientation.Vertical;
			linearUser.SetGravity (GravityFlags.CenterHorizontal);

			linearListCurso.Orientation = Orientation.Vertical;
			linearListTask.Orientation = Orientation.Vertical;

			linearList.Orientation = Orientation.Vertical;

			linearPendiente.Orientation = Orientation.Horizontal;
			linearPendiente.SetGravity (GravityFlags.Center);
			//linearList.SetGravity (GravityFlags.CenterVertical);

			progressBar = new ProgressBar (this,null,Android.Resource.Attribute.ProgressBarStyleHorizontal);
			progressBar.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (274), Configuration.getHeight (32));
			progressBar.ProgressDrawable = Resources.GetDrawable (Resource.Drawable.progressBackground);
			progressBar.Progress = 60;
			txtValorBarra.Text = "60%";
			//progressBar.text
			txtValorBarra.SetY(13);

			txtCurse.Text = "Cursos del 2015";
			txtCurse.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");


		//	txtUserName.Text ="David Spencer";
			txtUserName.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtUserRol.Text ="Alumno";
			txtUserRol.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtSchoolName.Text ="Colegio Sagrados Corazones";
			txtSchoolName.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtPorcentaje.Text = "60%";
			txtPorcentaje.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtCurseTitle.Text = "CURSOS";
			txtCurseTitle.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtTaskTitle.Text = "TAREAS";	
			txtTaskTitle.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtPendiente.Text = "1";
			txtPendiente.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txtPendiente.SetY (-10);


			txtPendiente.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));
			txtUserName.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (35));
			txtUserRol.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));


			txtCurseTitle.SetPadding (Configuration.getWidth (48), 0, 0, 0);
			txtTaskTitle.SetPadding (Configuration.getWidth (48), 0, 0, 0);

			txtCurse.SetTextColor (Color.ParseColor ("#ffffff"));
			txtUserName.SetTextColor (Color.ParseColor ("#ffffff"));
			txtUserRol.SetTextColor (Color.ParseColor ("#999999"));
			txtSchoolName.SetTextColor (Color.ParseColor ("#ffffff"));
			txtPorcentaje.SetTextColor (Color.ParseColor ("#ffffff"));
			txtPendiente.SetTextColor (Color.ParseColor ("#ffffff"));
			txtTaskTitle.SetTextColor (Color.ParseColor ("#ffffff"));
			txtCurseTitle.SetTextColor (Color.ParseColor ("#ffffff"));
			txtValorBarra.SetTextColor (Color.ParseColor ("#ffffff"));

			txtUserName.Gravity = GravityFlags.CenterHorizontal;
			txtUserRol.Gravity = GravityFlags.CenterHorizontal;
			txtCurse.Gravity = GravityFlags.CenterHorizontal;


			imgChat.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/chat.png"),Configuration.getWidth (45), Configuration.getWidth (40),true));
			imgUser.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/user.png"),Configuration.getWidth (154), Configuration.getHeight (154),true));
			imgSchool.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/colegio.png"),Configuration.getWidth (29), Configuration.getHeight (29),true));
			imgNotificacion.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/notif.png"),Configuration.getWidth (35), Configuration.getWidth (40),true));
			imgCurse.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/curso.png"),Configuration.getWidth (23), Configuration.getHeight (28),true));
			imgTask.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/vertareas.png"),Configuration.getWidth (23), Configuration.getHeight (28),true));

			Drawable drPendiente = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/pendiente.png"), Configuration.getWidth(30), Configuration.getWidth(30), true));
			linearPendiente.SetBackgroundDrawable (drPendiente);
			drPendiente = null;

			imgCurse.SetPadding (Configuration.getWidth (68), 0, 0, 0);
			imgTask.SetPadding(Configuration.getWidth(68),0,0,0);


			linearCurse.SetBackgroundColor(Color.ParseColor("#0d1216"));
			linearTask.SetBackgroundColor (Color.ParseColor ("#0d1216"));

			linearBarraCurso.AddView (txtCurse);
			linearBarraCurso.AddView (progressBar);

			linearTxtValorBarra.AddView (txtValorBarra);

			linearCurse.AddView (imgCurse);
			linearCurse.AddView (txtCurseTitle);
			linearTask.AddView (imgTask);
			linearTask.AddView (txtTaskTitle);
			linearPendiente.AddView (txtPendiente);



			imgSchool.SetPadding (Configuration.getWidth(68),0,0,0);
			txtSchoolName.SetPadding (Configuration.getWidth(40),0,0,0);
			linearSchool.AddView (imgSchool);
			linearSchool.AddView (txtSchoolName);

			linearUser.AddView (txtUserName);
			linearUser.AddView (txtUserRol);

			linearUserData.AddView (imgUser);
			linearUserData.AddView (linearUser);

			linearListCurso.AddView (listCursos);
			linearListTask.AddView (listTasks);

			linearList.AddView (linearCurse);
			linearList.AddView (linearListCurso);
			linearList.AddView (linearTask);
			linearList.AddView (linearListTask);


			imgChat.SetX (Configuration.getWidth(59)); imgChat.SetY (Configuration.getHeight(145));
			imgChat.Click += delegate {
				mDrawerLayout.CloseDrawer (mLeftDrawer);
				mDrawerLayout.OpenDrawer (mRightDrawer);
			};
				
			imgNotificacion.SetX (Configuration.getWidth(59));  imgNotificacion.SetY (Configuration.getHeight(233)); 
			imgNotificacion.Click += delegate {
				mDrawerLayout.CloseDrawer (mLeftDrawer);
				main_ContentView.RemoveAllViews ();
				main_ContentView.AddView(new NotificationView(this));
			};


			linearPendiente.SetX (Configuration.getWidth(94));  linearPendiente.SetY (Configuration.getHeight(217)); 

			linearUserData.SetX (0); linearUserData.SetY (Configuration.getHeight(130));
			linearBarraCurso.SetX (0); linearBarraCurso.SetY (Configuration.getHeight(412));
			linearTxtValorBarra.SetX (0); linearTxtValorBarra.SetY (Configuration.getHeight(443));
			linearSchool.SetX (0); linearSchool.SetY (Configuration.getHeight(532));
			linearList.SetX (0); linearList.SetY (Configuration.getHeight(583));

			Bitmap bm;
			txtUserName.Text = vm.UserFirstName + " "+ vm.UserLastName;

			if (vm.UserImage != null) {
				bm = BitmapFactory.DecodeByteArray (vm.UserImage, 0, vm.UserImage.Length);

				Bitmap newbm = Configuration.GetRoundedCornerBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (154), Configuration.getHeight (154),true));
				imgUser.SetImageBitmap (newbm);
			
				newbm = null;
			}
			bm = null;



			mainLayout.AddView (imgChat);
			mainLayout.AddView (imgNotificacion);
			mainLayout.AddView (linearPendiente);
			mainLayout.AddView (linearUserData);
			mainLayout.AddView (linearBarraCurso);
			mainLayout.AddView (linearTxtValorBarra);
			mainLayout.AddView (linearSchool);
			mainLayout.AddView (linearList);

			imgChat = null;
			imgNotificacion = null;
			imgCurse = null;
			imgTask = null;
			imgSchool = null;
			imgUser = null;
		}



		private void initListCursos(){		
		//	resetMLOs(0); 
			populateCircleScroll(0);
				
		}

		private void iniPeoples (){
			populatePeopleScroll(0);
		}

		void Vm_PropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			//var vm = this.ViewModel as MainViewModel;

			string property = e.PropertyName;
			switch(property){
			case "UserFirstName":
				txtUserName.Text = vm.UserFirstName + " " + vm.UserLastName;
				break;
			case "UserLastName":
				txtUserName.Text = vm.UserFirstName + " " + vm.UserLastName;
				break;
			case "CirclesList":
				populateCircleScroll (0);
				(ViewModel as MainViewModel).CirclesList.CollectionChanged+= CirclesList_CollectionChanged;
				break;
			case "UserImage":
				if (vm.UserImage != null) {
					
					Bitmap bm = BitmapFactory.DecodeByteArray (vm.UserImage, 0, vm.UserImage.Length);

					Bitmap newbm = Configuration.GetRoundedCornerBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (154), Configuration.getHeight (154),true));
					imgUser.SetImageBitmap (newbm);
					bm = null;
					newbm = null;
				}
				break;
			case "LearningOjectsList":
				resetMLOs ();
				//(ViewModel as MainViewModel).LearningOjectsList.CollectionChanged += _learningObjectsList_CollectionChanged;
				break;
			case "UsersList":
				populatePeopleScroll(0);
				(ViewModel as MainViewModel).UsersList.CollectionChanged+=  UsersList_CollectionChanged;
				break;

			case "PendingQuizzesList":
				resetPendingQuizzes();
				break;
			case "CompletedQuizzesList":
				loadCompleteQuizzes();
				//(ViewModel as MainViewModel).CompletedQuizzesList.CollectionChanged+= CompletedQuizzesList_CollectionChanged;

				break;
			case "LOsectionList":
				loadSection ();
				break;
			case "ContentByUnit":
				loadContentByUnit ();

				//(ViewModel as MainViewModel).LOsectionList.CollectionChanged += LOsection_CollectionChanged;
				break;
			case "PostsList":
				//resetComments();
				break;
			default:

				break;

			}
		
		
		}

		void LOsection_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
				foreach (LOsection item in e.NewItems) {
					TextView text = new TextView (this);
					text.SetTextColor (Color.White);
					text.Text = item.name;
					linearContentIndice.AddView (text);			
				}
			
		}


		void CirclesList_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			
			populateCircleScroll (e.NewStartingIndex);
		}


		void populateCircleScroll(int index){
			_currentCursos = new List<CursoItem> ();

			Console.WriteLine ("POPULATE_CIRCLE_SCROLL");

			if (vm.CirclesList != null)
			{
				for (int i = 0; i < vm.CirclesList.Count; i++)
				{
					var newinfo = new CursoItem()
					{
						CursoName = vm.CirclesList[i].name,
						Index =  i					
					};
					_currentCursos.Add(newinfo);
				}

				listCursos.Adapter = new CursoAdapter(this, _currentCursos);
				listCursos.ItemClick+= ListCursos_ItemClick;
			}
		}

		void CompletedQuizzesList_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			loadCompleteQuizzes ();
		}

		void populatePeopleScroll(int indice){
			mItemsChat = new List<ChatDataRow> ();

			if (vm.UsersList!= null)
			{
				for (int i = 0; i < vm.UsersList.Count; i++)
				{
					var newinfo = new ChatDataRow()
					{
						name = vm.UsersList[i].user.name + " " + vm.UsersList[i].user.lastname,
						state = true,//vm.UsersList[i].user.is_online,
						index = i,
						imageProfile = vm.UsersList[i].user.image_url
							
					};
					mItemsChat.Add(newinfo);
				}

				mListViewChat.Adapter = new ChatListViewAdapter(this, mItemsChat);
				mListViewChat.DividerHeight = 0;
	
			}
			
		}

	

		void resetMLOs(){
			
			main_ContentView.RemoveAllViews ();
			main_ContentView.AddView (loWallView);

			//scrollIndice.SetX (0);
			//scrollIndice.SetY (600);
			//main_ContentView.AddView(linearContentIndice);


			mDrawerLayout.CloseDrawer (mLeftDrawer);


			if (vm.LearningOjectsList != null) {
				//lo.Author= vm.LearningOjectsList[].lo.name +" "+vm.LearningOjectsList[e.Position].lo.lastname;
				//lo.Title = vm.LearningOjectsList [e.Position].lo.title;
				//lo.Chapter = "Flora y Fauna ";
				List<ImageLOView> list = new List<ImageLOView> ();

				for (int i = 0; i < vm.LearningOjectsList.Count; i++) {
					ImageLOView imgLO = new ImageLOView (this);

					imgLO.Title = vm.LearningOjectsList [i].lo.title;
					imgLO.Author = vm.LearningOjectsList [i].lo.name + " " + vm.LearningOjectsList [i].lo.lastname;
					//Bitmap bm = BitmapFactory.DecodeByteArray (vm.LearningOjectsList [i].cover_bytes, 0, vm.LearningOjectsList [i].cover_bytes.Length);
					//imgLO.ImageBitmap=Configuration.GetImageBitmapFromUrl(vm.LearningOjectsList[i].lo.url_cover);
					imgLO.Url = vm.LearningOjectsList [i].lo.url_background;
					imgLO.ImagenUsuario = getBitmapFromAsset ("icons/imgautor.png");
					imgLO.Chapter = "Flora y Fauna";
					imgLO.index = i;


					list.Add (imgLO);

				}

				//lo.getWorkSpaceLayout.Visibility = ViewStates.Invisible;

				for (int i = 0; i < list.Count; i++) {
					list [i].Click += setIndex;
				}

				loWallView.ListImages = list;
				loWallView.OpenLO.Click += Lo_ImagenLO_Click;
				loWallView.OpenTasks.Click += Lo_OpenTasks_Click;
				loWallView.OpenChat.Click += Lo_OpenChat_Click;
				//lo.OpenUnits.Click += Lo_OpenUnits_Click;
				loWallView.OpenComments.Click += Lo_OpenComments_Click;


				//lo.im= Configuration.GetImageBitmapFromUrl(vm.LearningOjectsList [e.Position].lo.url_cover);
				//lo.ListImages = vm.LearningOjectsList[e.Position].lo.



				//PositionLO = e.Position;


			}

		}

		void Lo_OpenComments_Click (object sender, EventArgs e)
		{
			loWallView.getWorkSpaceLayout.RemoveAllViews ();
			loWallView.getWorkSpaceLayout.AddView (_foro);
			LoadForo ();
		}


		void Lo_OpenChat_Click (object sender, EventArgs e)
		{
			mDrawerLayout.CloseDrawer (mLeftDrawer);
			mDrawerLayout.CloseDrawer (mRightDrawer);
			mDrawerLayout.OpenDrawer (mRightDrawer);
		}

		void Lo_OpenTasks_Click (object sender, EventArgs e)
		{
			LoadQuiz ();
			mDrawerLayout.CloseDrawer (mLeftDrawer);	
		}


		void _learningObjectsList_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{

			resetMLOs();
		}

		void UsersList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			populatePeopleScroll(e.NewStartingIndex);
		}


		public void initListTasks (){
			TaskItem item1 = new TaskItem();
			TaskItem item2 = new TaskItem();
			TaskItem item3 = new TaskItem ();

			item1.Name = "Ver Tareas";
			item1.Asset = "icons/tareas.png";
			item2.Name = "Hacer una pregunta";
			item2.Asset = "icons/pregunta.png";

			item3.Name = "Salir";
			item3.Asset = "icons/salir.png";

			_currentTask.Add (item1);
			_currentTask.Add (item2);
			_currentTask.Add (item3);

			listTasks.Adapter = new TaskAdapter (this, _currentTask);
			listTasks.ItemClick+= ListTasks_ItemClick;

		}


		void ListTasks_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			

			Console.WriteLine ("POSITION!!! = "+ e.Position);



			if (e.Position == 0) {
				LoadQuiz ();
			}
			if (e.Position == 2) {
				//salir command
				vm.LogoutCommand.Execute(null);
			}
			
				
			mDrawerLayout.CloseDrawer (mLeftDrawer);

		}

		void LoadForo(){
			loWallView.getWorkSpaceLayout.RemoveAllViews();
			loWallView.getWorkSpaceLayout.AddView (_foro);

			_foro.Author=vm.LearningOjectsList[loWallView.currentLOImageIndex].lo.name +" "+vm.LearningOjectsList[loWallView.currentLOImageIndex].lo.lastname;
			_foro.NameLO = vm.LearningOjectsList [loWallView.currentLOImageIndex].lo.title;
			_foro.Chapter = vm.LearningOjectsList [loWallView.currentLOImageIndex].lo.description;
			_foro.CoverUrl = vm.LearningOjectsList [loWallView.currentLOImageIndex].lo.url_background;


			int i = 0;
			List<CommentDataRow> _currentComments = new List<CommentDataRow> ();
			foreach (var quiz in vm.PostsList) 
			{
				var newinfo = new CommentDataRow ();
				newinfo.comment = vm.PostsList [i].post.text;
				newinfo.date = vm.PostsList [i].post.created_at.ToString ();
				newinfo.im_profile = vm.PostsList [i].post.image_url;
				newinfo.name = vm.PostsList [i].post.name;

				_currentComments.Add(newinfo);
				i++;
			}

			_foro.ListaComentarios = _currentComments;
		

		}

		void LoadQuiz(){
			//main_ContentView.RemoveAllViews ();
			//main_ContentView.AddView (task);

			//var vm = ViewModel as MainViewModel;
			loWallView.getWorkSpaceLayout.RemoveAllViews();
			loWallView.getWorkSpaceLayout.AddView (task);


			task.Author=vm.LearningOjectsList[loWallView.currentLOImageIndex].lo.name +" "+vm.LearningOjectsList[loWallView.currentLOImageIndex].lo.lastname;
			task.NameLO = vm.LearningOjectsList [loWallView.currentLOImageIndex].lo.title;
			task.Chapter = vm.LearningOjectsList [loWallView.currentLOImageIndex].lo.description;
			task.CoverUrl = vm.LearningOjectsList [loWallView.currentLOImageIndex].lo.url_background;
		

			loadCompleteQuizzes ();
			resetPendingQuizzes ();
		}

		void loadCompleteQuizzes(){
			String icon = "icons/tareacompleta.png";

			List<TaskViewItem> _currentTask = new List<TaskViewItem> ();
			//var vm = ViewModel as MainViewModel;


			int i = 0;
			foreach (var quiz in vm.CompletedQuizzesList) 
			{
				var newinfo = new TaskViewItem()
				{
					Icon = icon,
					Tarea= vm.CompletedQuizzesList[i].content,
					Index = i

				};
				_currentTask.Add(newinfo);
				i++;
			}
			task.ListaTareasCompletas = _currentTask;
		}

		void resetPendingQuizzes(){
			String icon = "icons/tareaincompleta.png";
			List<TaskViewItem> _currentTask = new List<TaskViewItem> ();
			//var vm = ViewModel as MainViewModel;

			int i = 0;
			foreach (var quiz in vm.PendingQuizzesList) 
			{
				var newinfo = new TaskViewItem()
				{
					//CursoName = vm.LearningOjectsList[i].lo.title,
					//Index =  i					
					Icon = icon,
					Tarea= vm.PendingQuizzesList[i].content,
					Index = i

				};
				_currentTask.Add(newinfo);
				i++;
			}
			task.ListaTareasIncompletas = _currentTask;
		}

		void ListCursos_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{


			var circle = _currentCursos [e.Position];

			//var vm = this.ViewModel as MainViewModel;
			//vm.SelectCircleCommand.Execute(vm.CirclesList[circle.Index]);

			vm.SelectCircleCommand.Execute(vm.CirclesList[circle.Index]);
			PositionLO = e.Position;


		}

		void imBtn_Units_Click(object sender, EventArgs e)
		{
			loWallView.getWorkSpaceLayout.RemoveAllViews ();
		}

		void imBtn_Comments_Click(object sender, EventArgs e)
		{
			//vm.LogoutCommand.Execute (null);

		}

		void imBtn_Chat_Click(object sender, EventArgs e)
		{
			

			mDrawerLayout.CloseDrawer (mLeftDrawer);
			mDrawerLayout.CloseDrawer (mRightDrawer);
			mDrawerLayout.OpenDrawer (mRightDrawer);
		}

		void setIndex(object sender, EventArgs e){
			var imgview = sender as ImageLOView;
			currentUnidad = imgview.index;


			MainViewModel viewModel = vm as MainViewModel;

			Console.WriteLine ("loading index for nitems  = " + viewModel.LearningOjectsList.Count);
			Console.WriteLine(" position LO = "+ currentUnidad);
			MLearning.Core.ViewModels.MainViewModel.lo_by_circle_wrapper currentLearningObject = viewModel.LearningOjectsList [currentUnidad];

			int circleID = currentLearningObject.lo.Circle_id;


			vm.OpenLOSectionListCommand.Execute(currentLearningObject);

		
			Android.Util.Log.Debug("indexList", " course_id (lo_id)  = " + circleID);

			//var sectionList = await _mLearningService.GetSectionsByLO (LOID);
			//foreach (var item in sectionList) {
			//	var sectionPages = await _mLearningService.GetPagesByLOSection (item.id);

			// Debug.WriteLine(" section_id ()  = "+ unit_id);


			 
		}

		void Lo_ImagenLO_Click (object sender, EventArgs e)
		{
			
			_dialogDownload.Show ();
			Configuration.IndiceActual = PositionLO;



			Console.WriteLine ("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS count  = " + vm.LearningOjectsList.Count);
			Console.WriteLine(" position LO = "+ currentUnidad);
			vm.OpenLOCommand.Execute(vm.LearningOjectsList[currentUnidad]);
 
			//vm.SelectLOCommand.Execute
		}
		void loadContentByUnit() {
			if (vm.ContentByUnit != null) 
			{

				this.loWallView._index_list.RemoveAllViews ();
				foreach (var pair in vm.ContentByUnit) {
				
					TextView text = new TextView (this);
					text.SetTextColor (Color.White);

					text.Text = pair.Key + "" ;

					this.loWallView._index_list.AddView (text);		

					for (int i = 0; i < pair.Value.Count; i++) {
					
						TextView text2 = new TextView (this);
						text2.SetTextColor (Color.White);

						text2.Text = pair.Value[i].title  +  pair.Value[i].description;

						this.loWallView._index_list.AddView (text2);		

					}

				}
					  

			}
		}
		void loadSection(){
			if (vm.LOsectionList != null) 
			{
 
				this.loWallView._index_list.RemoveAllViews ();
				for (int i = 0; i < vm.LOsectionList.Count; i++) {
					TextView text = new TextView (this);
					text.SetTextColor (Color.White);

					text.Text = vm.LOsectionList [i].name ;

					this.loWallView._index_list.AddView (text);		


				}
				MainViewModel viewModel = vm as MainViewModel;
				MLearning.Core.ViewModels.MainViewModel.lo_by_circle_wrapper currentLearningObject = viewModel.LearningOjectsList [currentUnidad];
				int circleID = currentLearningObject.lo.Circle_id;
				 
				vm.OpenFirstSlidePageCommand.Execute (currentLearningObject);

			}

	
		}

		/*
		void resetComments(){
			
		}

       
		*/

		//toolbar codes requisites

		public override bool OnOptionsItemSelected (IMenuItem item)
		{		
			switch (item.ItemId)
			{

			case Android.Resource.Id.Home:
				//The hamburger icon was clicked which means the drawer toggle will handle the event
				//all we need to do is ensure the right drawer is closed so the don't overlap
				mDrawerLayout.CloseDrawer (mRightDrawer);
				mDrawerToggle.OnOptionsItemSelected(item);
				return true;

			case Resource.Id.action_chat:
				if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
				{
					//Right Drawer is already open, close it
					mDrawerLayout.CloseDrawer(mRightDrawer);
				}

				else
				{
					//Right Drawer is closed, open it and just in case close left drawer
					mDrawerLayout.OpenDrawer (mRightDrawer);
					mDrawerLayout.CloseDrawer (mLeftDrawer);
				}

				return true;

			default:
				return base.OnOptionsItemSelected (item);
			}
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.action_menu, menu);



			return base.OnCreateOptionsMenu (menu);
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
			{
				outState.PutString("DrawerState", "Opened");
			}

			else
			{
				outState.PutString("DrawerState", "Closed");
			}

			base.OnSaveInstanceState (outState);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}
		private void RegisterWithGCM()
		{


			if (!GcmClient.IsRegistered(this))
			{
				GcmClient.CheckDevice(this);
				GcmClient.CheckManifest(this);

				// Register for push notifications
				System.Diagnostics.Debug.WriteLine("Registering...");


				GcmClient.Register(this, Core.Configuration.Constants.SenderID);

			}





		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}



		void logout_propertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsLoggingOut" && (sender as MainViewModel).IsLoggingOut)
			{
				GcmClient.UnRegister(this);
			}
		}


		protected override void OnPause ()
		{
			base.OnPause ();
			_dialogDownload.Hide ();
		}
    }
}
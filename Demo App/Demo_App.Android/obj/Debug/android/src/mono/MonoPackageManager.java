package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	static android.content.Context Context;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (context instanceof android.app.Application) {
				Context = context;
			}
			if (!initialized) {
				android.content.IntentFilter timezoneChangedFilter  = new android.content.IntentFilter (
						android.content.Intent.ACTION_TIMEZONE_CHANGED
				);
				context.registerReceiver (new mono.android.app.NotifyTimeZoneChanges (), timezoneChangedFilter);
				
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();
				java.io.File external0 = android.os.Environment.getExternalStorageDirectory ();
				String externalDir = new java.io.File (
							external0,
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();
				String externalLegacyDir = new java.io.File (
							external0,
							"../legacy/Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new String[] {
							externalDir,
							externalLegacyDir
						},
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				
				mono.android.app.ApplicationRegistration.registerApplications ();
				
				initialized = true;
			}
		}
	}

	public static void setContext (Context context)
	{
		// Ignore; vestigial
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		/* We need to ensure that "Demo_App.Android.dll" comes first in this list. */
		"Demo_App.Android.dll",
		"AndHUD.dll",
		"Plugin.CurrentActivity.dll",
		"Syncfusion.Core.XForms.dll",
		"Syncfusion.DataSource.Portable.dll",
		"Syncfusion.GridCommon.Portable.dll",
		"Syncfusion.SfListView.XForms.Android.dll",
		"Syncfusion.SfListView.XForms.dll",
		"Syncfusion.SfPicker.Android.dll",
		"Syncfusion.SfPicker.XForms.Android.dll",
		"Syncfusion.SfPicker.XForms.dll",
		"Syncfusion.SfSchedule.Android.dll",
		"Syncfusion.SfSchedule.XForms.Android.dll",
		"Syncfusion.SfSchedule.XForms.dll",
		"ExifLib.dll",
		"FormsViewGroup.dll",
		"Newtonsoft.Json.dll",
		"Rg.Plugins.Popup.dll",
		"Rg.Plugins.Popup.Droid.dll",
		"Rg.Plugins.Popup.Platform.dll",
		"SkiaSharp.dll",
		"SkiaSharp.Views.Android.dll",
		"SkiaSharp.Views.Forms.dll",
		"Telerik.Xamarin.Android.Common.dll",
		"Telerik.Xamarin.Android.Data.dll",
		"Telerik.Xamarin.Android.Input.dll",
		"Telerik.Xamarin.Android.List.dll",
		"Telerik.Xamarin.Android.Primitives.dll",
		"Telerik.XamarinForms.Common.dll",
		"Telerik.XamarinForms.Input.dll",
		"Telerik.XamarinForms.Primitives.dll",
		"Telerik.XamarinForms.SkiaSharp.dll",
		"Xamarin.Android.Support.Animated.Vector.Drawable.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.CardView.dll",
		"Xamarin.Android.Support.v7.MediaRouter.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Android.Support.v8.RenderScript.dll",
		"Xamarin.Android.Support.Vector.Drawable.dll",
		"Xamarin.Forms.Core.dll",
		"Xamarin.Forms.Platform.Android.dll",
		"Xamarin.Forms.Platform.dll",
		"Xamarin.Forms.Xaml.dll",
		"Xamarin.iOS.dll",
		"XamForms.Controls.Calendar.dll",
		"XamForms.Controls.Calendar.Droid.dll",
		"XLabs.Core.dll",
		"XLabs.Forms.dll",
		"XLabs.Forms.Droid.dll",
		"XLabs.IOC.dll",
		"XLabs.Platform.dll",
		"XLabs.Platform.Droid.dll",
		"XLabs.Serialization.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = null;
}

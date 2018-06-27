package md5f427ae3157ea4ab771d43fda9dc8722a;


public class CalendarPickerView_OnPageChangeListener
	extends android.support.v4.view.ViewPager.SimpleOnPageChangeListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPageSelected:(I)V:GetOnPageSelected_IHandler\n" +
			"";
		mono.android.Runtime.register ("XLabs.Forms.Controls.MonoDroid.TimesSquare.CalendarPickerView+OnPageChangeListener, XLabs.Forms.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", CalendarPickerView_OnPageChangeListener.class, __md_methods);
	}


	public CalendarPickerView_OnPageChangeListener ()
	{
		super ();
		if (getClass () == CalendarPickerView_OnPageChangeListener.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.MonoDroid.TimesSquare.CalendarPickerView+OnPageChangeListener, XLabs.Forms.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public CalendarPickerView_OnPageChangeListener (md5f427ae3157ea4ab771d43fda9dc8722a.CalendarPickerView p0)
	{
		super ();
		if (getClass () == CalendarPickerView_OnPageChangeListener.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.MonoDroid.TimesSquare.CalendarPickerView+OnPageChangeListener, XLabs.Forms.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", "XLabs.Forms.Controls.MonoDroid.TimesSquare.CalendarPickerView, XLabs.Forms.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onPageSelected (int p0)
	{
		n_onPageSelected (p0);
	}

	private native void n_onPageSelected (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

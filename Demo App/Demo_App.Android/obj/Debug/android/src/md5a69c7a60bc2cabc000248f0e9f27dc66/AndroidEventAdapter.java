package md5a69c7a60bc2cabc000248f0e9f27dc66;


public class AndroidEventAdapter
	extends com.telerik.widget.calendar.events.EventAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getEventsForDate:(J)Ljava/util/List;:GetGetEventsForDate_JHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventAdapter, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AndroidEventAdapter.class, __md_methods);
	}


	public AndroidEventAdapter (com.telerik.widget.calendar.RadCalendarView p0)
	{
		super (p0);
		if (getClass () == AndroidEventAdapter.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventAdapter, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Com.Telerik.Widget.Calendar.RadCalendarView, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public AndroidEventAdapter (com.telerik.widget.calendar.RadCalendarView p0, java.util.List p1)
	{
		super (p0, p1);
		if (getClass () == AndroidEventAdapter.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventAdapter, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Com.Telerik.Widget.Calendar.RadCalendarView, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null:System.Collections.Generic.IList`1<Com.Telerik.Widget.Calendar.Events.Event>, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public java.util.List getEventsForDate (long p0)
	{
		return n_getEventsForDate (p0);
	}

	private native java.util.List n_getEventsForDate (long p0);

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

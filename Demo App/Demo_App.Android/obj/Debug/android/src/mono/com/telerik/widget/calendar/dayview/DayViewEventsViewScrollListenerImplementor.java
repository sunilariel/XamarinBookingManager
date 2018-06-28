package mono.com.telerik.widget.calendar.dayview;


public class DayViewEventsViewScrollListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.dayview.DayViewEventsViewScrollListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollChange:(Landroid/view/View;IIII)V:GetOnScrollChange_Landroid_view_View_IIIIHandler:Com.Telerik.Widget.Calendar.Dayview.IDayViewEventsViewScrollListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.Dayview.IDayViewEventsViewScrollListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", DayViewEventsViewScrollListenerImplementor.class, __md_methods);
	}


	public DayViewEventsViewScrollListenerImplementor ()
	{
		super ();
		if (getClass () == DayViewEventsViewScrollListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.Dayview.IDayViewEventsViewScrollListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onScrollChange (android.view.View p0, int p1, int p2, int p3, int p4)
	{
		n_onScrollChange (p0, p1, p2, p3, p4);
	}

	private native void n_onScrollChange (android.view.View p0, int p1, int p2, int p3, int p4);

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

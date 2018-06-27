package mono.com.telerik.widget.calendar.dayview;


public class CalendarDayView_EventViewTapListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.dayview.CalendarDayView.EventViewTapListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEventViewTap:(Lcom/telerik/widget/calendar/events/Event;)V:GetOnEventViewTap_Lcom_telerik_widget_calendar_events_Event_Handler:Com.Telerik.Widget.Calendar.Dayview.CalendarDayView/IEventViewTapListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.Dayview.CalendarDayView+IEventViewTapListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", CalendarDayView_EventViewTapListenerImplementor.class, __md_methods);
	}


	public CalendarDayView_EventViewTapListenerImplementor ()
	{
		super ();
		if (getClass () == CalendarDayView_EventViewTapListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.Dayview.CalendarDayView+IEventViewTapListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onEventViewTap (com.telerik.widget.calendar.events.Event p0)
	{
		n_onEventViewTap (p0);
	}

	private native void n_onEventViewTap (com.telerik.widget.calendar.events.Event p0);

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

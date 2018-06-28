package md5a69c7a60bc2cabc000248f0e9f27dc66;


public class AndroidEventRenderer
	extends com.telerik.widget.calendar.events.EventRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_renderEvents:(Landroid/graphics/Canvas;Lcom/telerik/widget/calendar/CalendarDayCell;)V:GetRenderEvents_Landroid_graphics_Canvas_Lcom_telerik_widget_calendar_CalendarDayCell_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventRenderer, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AndroidEventRenderer.class, __md_methods);
	}


	public AndroidEventRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == AndroidEventRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventRenderer, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void renderEvents (android.graphics.Canvas p0, com.telerik.widget.calendar.CalendarDayCell p1)
	{
		n_renderEvents (p0, p1);
	}

	private native void n_renderEvents (android.graphics.Canvas p0, com.telerik.widget.calendar.CalendarDayCell p1);

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

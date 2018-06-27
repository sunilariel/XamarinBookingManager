package md5a69c7a60bc2cabc000248f0e9f27dc66;


public class AndroidEvent
	extends com.telerik.widget.calendar.events.Event
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getEventColor:()I:GetGetEventColorHandler\n" +
			"n_setEventColor:(I)V:GetSetEventColor_IHandler\n" +
			"n_getStartDate:()J:GetGetStartDateHandler\n" +
			"n_setStartDate:(J)V:GetSetStartDate_JHandler\n" +
			"n_getEndDate:()J:GetGetEndDateHandler\n" +
			"n_setEndDate:(J)V:GetSetEndDate_JHandler\n" +
			"n_getTitle:()Ljava/lang/String;:GetGetTitleHandler\n" +
			"n_setTitle:(Ljava/lang/String;)V:GetSetTitle_Ljava_lang_String_Handler\n" +
			"n_getDetails:()Ljava/lang/String;:GetGetDetailsHandler\n" +
			"n_setDetails:(Ljava/lang/String;)V:GetSetDetails_Ljava_lang_String_Handler\n" +
			"n_isAllDay:()Z:GetIsAllDayHandler\n" +
			"n_setAllDay:(Z)V:GetSetAllDay_ZHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.AndroidEvent, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AndroidEvent.class, __md_methods);
	}


	public AndroidEvent (java.lang.String p0, long p1, long p2)
	{
		super (p0, p1, p2);
		if (getClass () == AndroidEvent.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AndroidEvent, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int64, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int64, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public int getEventColor ()
	{
		return n_getEventColor ();
	}

	private native int n_getEventColor ();


	public void setEventColor (int p0)
	{
		n_setEventColor (p0);
	}

	private native void n_setEventColor (int p0);


	public long getStartDate ()
	{
		return n_getStartDate ();
	}

	private native long n_getStartDate ();


	public void setStartDate (long p0)
	{
		n_setStartDate (p0);
	}

	private native void n_setStartDate (long p0);


	public long getEndDate ()
	{
		return n_getEndDate ();
	}

	private native long n_getEndDate ();


	public void setEndDate (long p0)
	{
		n_setEndDate (p0);
	}

	private native void n_setEndDate (long p0);


	public java.lang.String getTitle ()
	{
		return n_getTitle ();
	}

	private native java.lang.String n_getTitle ();


	public void setTitle (java.lang.String p0)
	{
		n_setTitle (p0);
	}

	private native void n_setTitle (java.lang.String p0);


	public java.lang.String getDetails ()
	{
		return n_getDetails ();
	}

	private native java.lang.String n_getDetails ();


	public void setDetails (java.lang.String p0)
	{
		n_setDetails (p0);
	}

	private native void n_setDetails (java.lang.String p0);


	public boolean isAllDay ()
	{
		return n_isAllDay ();
	}

	private native boolean n_isAllDay ();


	public void setAllDay (boolean p0)
	{
		n_setAllDay (p0);
	}

	private native void n_setAllDay (boolean p0);

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

package md5f99e21c4152ba4667fdfd0c23b3ffce5;


public class StrokedDrawable
	extends android.graphics.drawable.ColorDrawable
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.Common.Android.StrokedDrawable, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", StrokedDrawable.class, __md_methods);
	}


	public StrokedDrawable ()
	{
		super ();
		if (getClass () == StrokedDrawable.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.Common.Android.StrokedDrawable, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public StrokedDrawable (int p0)
	{
		super (p0);
		if (getClass () == StrokedDrawable.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.Common.Android.StrokedDrawable, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Graphics.Color, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void draw (android.graphics.Canvas p0)
	{
		n_draw (p0);
	}

	private native void n_draw (android.graphics.Canvas p0);

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

package mono.com.telerik.android.primitives.widget.tooltip.contracts;


public class DrawListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.primitives.widget.tooltip.contracts.DrawListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_notifyDraw:(Landroid/graphics/Canvas;)V:GetNotifyDraw_Landroid_graphics_Canvas_Handler:Com.Telerik.Android.Primitives.Widget.Tooltip.Contracts.IDrawListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Primitives.Widget.Tooltip.Contracts.IDrawListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", DrawListenerImplementor.class, __md_methods);
	}


	public DrawListenerImplementor ()
	{
		super ();
		if (getClass () == DrawListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Primitives.Widget.Tooltip.Contracts.IDrawListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void notifyDraw (android.graphics.Canvas p0)
	{
		n_notifyDraw (p0);
	}

	private native void n_notifyDraw (android.graphics.Canvas p0);

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

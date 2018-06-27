package md5f99e21c4152ba4667fdfd0c23b3ffce5;


public abstract class RadViewContainerBase
	extends com.xamarin.forms.platform.android.FormsViewGroup
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.Common.Android.RadViewContainerBase, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", RadViewContainerBase.class, __md_methods);
	}


	public RadViewContainerBase (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RadViewContainerBase.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.Common.Android.RadViewContainerBase, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public RadViewContainerBase (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RadViewContainerBase.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.Common.Android.RadViewContainerBase, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public RadViewContainerBase (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RadViewContainerBase.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.Common.Android.RadViewContainerBase, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);

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

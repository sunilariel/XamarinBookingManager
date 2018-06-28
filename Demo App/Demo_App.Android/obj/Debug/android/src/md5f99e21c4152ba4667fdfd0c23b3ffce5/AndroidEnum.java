package md5f99e21c4152ba4667fdfd0c23b3ffce5;


public class AndroidEnum
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_toString:()Ljava/lang/String;:GetToStringHandler\n" +
			"n_equals:(Ljava/lang/Object;)Z:GetEquals_Ljava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.Common.Android.AndroidEnum, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AndroidEnum.class, __md_methods);
	}


	public AndroidEnum ()
	{
		super ();
		if (getClass () == AndroidEnum.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.Common.Android.AndroidEnum, Telerik.XamarinForms.Common, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.String toString ()
	{
		return n_toString ();
	}

	private native java.lang.String n_toString ();


	public boolean equals (java.lang.Object p0)
	{
		return n_equals (p0);
	}

	private native boolean n_equals (java.lang.Object p0);

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

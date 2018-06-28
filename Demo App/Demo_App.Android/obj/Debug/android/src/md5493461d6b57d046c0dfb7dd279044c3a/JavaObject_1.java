package md5493461d6b57d046c0dfb7dd279044c3a;


public class JavaObject_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XLabs.Platform.JavaObject`1, XLabs.Platform.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", JavaObject_1.class, __md_methods);
	}


	public JavaObject_1 ()
	{
		super ();
		if (getClass () == JavaObject_1.class)
			mono.android.TypeManager.Activate ("XLabs.Platform.JavaObject`1, XLabs.Platform.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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

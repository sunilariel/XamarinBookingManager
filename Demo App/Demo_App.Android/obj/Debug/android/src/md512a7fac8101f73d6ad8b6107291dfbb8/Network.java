package md512a7fac8101f73d6ad8b6107291dfbb8;


public class Network
	extends md5a828262bd4017cc71bc13dd2dd9f1463.BroadcastMonitor
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("XLabs.Platform.Services.Network, XLabs.Platform.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", Network.class, __md_methods);
	}


	public Network ()
	{
		super ();
		if (getClass () == Network.class)
			mono.android.TypeManager.Activate ("XLabs.Platform.Services.Network, XLabs.Platform.Droid, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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

package md5a69c7a60bc2cabc000248f0e9f27dc66;


public class GroupLayoutManager
	extends com.telerik.widget.dataform.visualization.DataFormGroupLayoutManager
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_createEditorGroup:(Ljava/lang/String;)Lcom/telerik/widget/dataform/visualization/EditorGroup;:GetCreateEditorGroup_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.GroupLayoutManager, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", GroupLayoutManager.class, __md_methods);
	}


	public GroupLayoutManager (android.content.Context p0)
	{
		super (p0);
		if (getClass () == GroupLayoutManager.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.GroupLayoutManager, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public GroupLayoutManager (android.content.Context p0, int p1)
	{
		super (p0, p1);
		if (getClass () == GroupLayoutManager.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.GroupLayoutManager, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public com.telerik.widget.dataform.visualization.EditorGroup createEditorGroup (java.lang.String p0)
	{
		return n_createEditorGroup (p0);
	}

	private native com.telerik.widget.dataform.visualization.EditorGroup n_createEditorGroup (java.lang.String p0);

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

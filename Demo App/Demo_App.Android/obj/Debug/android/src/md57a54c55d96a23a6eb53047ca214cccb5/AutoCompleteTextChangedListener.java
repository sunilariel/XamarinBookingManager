package md57a54c55d96a23a6eb53047ca214cccb5;


public class AutoCompleteTextChangedListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.text.TextWatcher,
		android.text.NoCopySpan
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_afterTextChanged:(Landroid/text/Editable;)V:GetAfterTextChanged_Landroid_text_Editable_Handler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_beforeTextChanged:(Ljava/lang/CharSequence;III)V:GetBeforeTextChanged_Ljava_lang_CharSequence_IIIHandler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onTextChanged:(Ljava/lang/CharSequence;III)V:GetOnTextChanged_Ljava_lang_CharSequence_IIIHandler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.AutoComplete.AutoCompleteTextChangedListener, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AutoCompleteTextChangedListener.class, __md_methods);
	}


	public AutoCompleteTextChangedListener ()
	{
		super ();
		if (getClass () == AutoCompleteTextChangedListener.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AutoComplete.AutoCompleteTextChangedListener, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void afterTextChanged (android.text.Editable p0)
	{
		n_afterTextChanged (p0);
	}

	private native void n_afterTextChanged (android.text.Editable p0);


	public void beforeTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3)
	{
		n_beforeTextChanged (p0, p1, p2, p3);
	}

	private native void n_beforeTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3);


	public void onTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3)
	{
		n_onTextChanged (p0, p1, p2, p3);
	}

	private native void n_onTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3);

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

package crc64555c6d53d34d7a84;


public class CustomWebViewHandler_MyWebChromeClient
	extends android.webkit.WebChromeClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPermissionRequest:(Landroid/webkit/PermissionRequest;)V:GetOnPermissionRequest_Landroid_webkit_PermissionRequest_Handler\n" +
			"";
		mono.android.Runtime.register ("BTAI.Handlers.CustomWebViewHandler+MyWebChromeClient, BTAI", CustomWebViewHandler_MyWebChromeClient.class, __md_methods);
	}


	public CustomWebViewHandler_MyWebChromeClient ()
	{
		super ();
		if (getClass () == CustomWebViewHandler_MyWebChromeClient.class) {
			mono.android.TypeManager.Activate ("BTAI.Handlers.CustomWebViewHandler+MyWebChromeClient, BTAI", "", this, new java.lang.Object[] {  });
		}
	}


	public void onPermissionRequest (android.webkit.PermissionRequest p0)
	{
		n_onPermissionRequest (p0);
	}

	private native void n_onPermissionRequest (android.webkit.PermissionRequest p0);

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

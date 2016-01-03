using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Nfc;

namespace HelloWorld
{
	[Activity (Label = "HelloWorld", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		NfcAdapter nfc;
		NfcReaderCallback readerCallback = new NfcReaderCallback();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.theButton);
			button.Click += Start;
		}

		void Start(object sender, EventArgs args)
		{
			try
			{
				nfc = NfcAdapter.GetDefaultAdapter (this);

				readerCallback.TagDiscovered += TagDiscovered;

				//			NfcReaderFlags flags = NfcReaderFlags.NfcA | NfcReaderFlags.NfcB | NfcReaderFlags.NfcBarcode | NfcReaderFlags.NfcF | NfcReaderFlags.NfcV | NfcReaderFlags.SkipNdefCheck;
				NfcReaderFlags flags = NfcReaderFlags.NfcA | NfcReaderFlags.SkipNdefCheck;
				nfc.EnableReaderMode (this, readerCallback, flags, null);
			}
			catch(Exception ex) {
				TextView textViewTag = FindViewById<TextView> (Resource.Id.textViewTag);
				textViewTag.Text = ex.Message;
			}
		}
			
		void TagDiscovered(Tag tag)
		{
			string id = "";

			if (tag.GetId () == null)
				id = "<NULL>";
			else {
				foreach (byte b in tag.GetId())
					id += b.ToString ("X2") + " ";
			}

			TextView textViewTag = FindViewById<TextView> (Resource.Id.textViewTag);
			RunOnUiThread(() => {
				textViewTag.Text = id;
			});
		}
	}
}



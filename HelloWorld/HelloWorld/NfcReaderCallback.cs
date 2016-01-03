using System;
using Android.Nfc;

namespace HelloWorld
{
	public class NfcReaderCallback : Java.Lang.Object, NfcAdapter.IReaderCallback
	{
		public event Action<Tag> TagDiscovered;

		public void OnTagDiscovered(Tag tag)
		{
			if (TagDiscovered != null)
				TagDiscovered(tag);
		}
	}
}


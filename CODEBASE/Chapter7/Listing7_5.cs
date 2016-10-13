using System;

public class Listing7_5
{
    static void HandleTagReadEvent(object sender, NotificationEventArgs args)
    {
        TagReadEvent tre = args.Notification.Event as TagReadEvent;
        if (tre != null)
        {
            string hex = HexUtilities.HexEncode(tre.GetId());
            MessageBox.Show("Read tag " + hex);
        }
    }
}

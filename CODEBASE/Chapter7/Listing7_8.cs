using System;

public class Listing7_8
{
    static void Translate(byte[] rawValue)
    {
        IdentityEncoding decodedValue;
        if (IdentityEncoding.TryParse(rawValue, out decodedValue))
        {
            EpcEncoding epc = decodedValue as EpcEncoding;
            if (epc != null)
            {
                string uriEncodedString = epc.ToString();
            }
        }
    }

}

using System.Text;

namespace Server2.Helpers;

public static class StreamConverter{

    static Encoding encoding = Encoding.UTF8;

    public static byte[] MessageToByteArray(string message)
    {
        // get the size of original message
        var messageBytes = encoding.GetBytes(message);
        var messageSize = messageBytes.Length;
        
        // add content length bytes to the original size
        var completeSize = messageSize + 4;
        
        // create a buffer of the size of the complete message size
        var completemsg = new byte[completeSize];
 
        // convert message size to bytes
        var sizeBytes = BitConverter.GetBytes(messageSize);
        
        // copy the size bytes and the message bytes to our overall message to be sent 
        sizeBytes.CopyTo(completemsg, 0);
        messageBytes.CopyTo(completemsg, 4);
        return completemsg;
    }

    public static string StreamToMessage(Stream stream)
    {
        // size bytes have been fixed to 4
        var sizeBytes = new byte[4];
        
        // read the content length
        stream.Read(sizeBytes, 0, 4);
        var messageSize = BitConverter.ToInt32(sizeBytes, 0);
        
        // create a buffer of the content length size and read from the stream
        var messageBytes = new byte[messageSize];
        stream.Read(messageBytes, 0, messageSize);
        
        // convert message byte array to the message string using the encoding
        var message = encoding.GetString(messageBytes);
        string result = null;
        foreach (var c in message)
            if (c != '\0')
                result += c;
 
        return result;
    }
}
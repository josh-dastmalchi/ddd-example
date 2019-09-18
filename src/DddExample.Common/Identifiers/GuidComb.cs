using System;

namespace DddExample.Common.Identifiers
{
    public static class GuidComb
    {
        public static Guid Generate()
        {
            var byteArray = Guid.NewGuid().ToByteArray();
            var dateTime = new DateTime(1900, 1, 1);
            var now = DateTime.Now;
            var timeSpan = new TimeSpan(now.Ticks - dateTime.Ticks);
            var timeOfDay = now.TimeOfDay;
            var bytes1 = BitConverter.GetBytes(timeSpan.Days);
            var bytes2 = BitConverter.GetBytes((long) (timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes1);
            Array.Reverse(bytes2);
            Array.Copy(bytes1, bytes1.Length - 2, byteArray, byteArray.Length - 6, 2);
            Array.Copy(bytes2, bytes2.Length - 4, byteArray, byteArray.Length - 4, 4);
            return new Guid(byteArray);
        }
    }
}
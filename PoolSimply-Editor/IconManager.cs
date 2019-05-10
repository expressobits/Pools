using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ExpressoBits.PoolSimply
{
    /// <summary>
    /// Icon manager.
    /// </summary>
    public static class IconManager
    {
        /// <summary>
        /// Read all bytes in this stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>All bytes in the stream.</returns>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        private static readonly Dictionary<string, Texture2D> _embeddedIcons = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Get the embedded icon with the given resource name.
        /// </summary>
        /// <param name="resourceName">The resource name.</param>
        /// <returns>The embedded icon with the given resource name.</returns>
        public static Texture2D GetEmbeddedIcon(string resourceName, byte[] data)
        {
            //var assembly = Assembly.GetExecutingAssembly();

            Texture2D icon;
            if (!_embeddedIcons.TryGetValue(resourceName, out icon) || icon == null)
            {
                byte[] iconBytes;
                //using (var stream = assembly.GetManifestResourceStream(resourceName))
                iconBytes = data;//stream.ReadAllBytes();
                icon = new Texture2D(128, 128);
                icon.LoadImage(iconBytes);
                icon.name = resourceName;

                _embeddedIcons[resourceName] = icon;
            }

            return icon;
        }



        /// <summary>
        /// Set the icon for this object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="texture">The icon.</param>
        public static void SetIcon(this UnityEngine.Object obj, Texture2D texture)
        {
            var ty = typeof(EditorGUIUtility);
            var mi = ty.GetMethod("SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);
            mi.Invoke(null, new object[] { obj, texture });
        }

        /// <summary>
        /// Set the icon for this object from an embedded resource.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="texture">The icon.</param>
        public static void SetIcon(this UnityEngine.Object obj, string resourceName, byte[] data)
        {
            SetIcon(obj, GetEmbeddedIcon(resourceName, data));
        }

        /// <summary>
        /// Get the icon for this object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The icon for this object.</returns>
        public static Texture2D GetIcon(this UnityEngine.Object obj)
        {
            var ty = typeof(EditorGUIUtility);
            var mi = ty.GetMethod("GetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);
            return mi.Invoke(null, new object[] { obj }) as Texture2D;
        }

        /// <summary>
        /// Remove this icon's object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void RemoveIcon(this UnityEngine.Object obj)
        {
            SetIcon(obj, (Texture2D)null);
        }
    }
}